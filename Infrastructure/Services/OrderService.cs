using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;
        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;
            _paymentService = paymentService;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {

            /*Task list, first inject the necessary repositories in the const
            1.Get basket from the repo
            2.Get items from the product repo
            3.Get delivery method from repo
            4.Calculate subtotal
            5.Create Order/repo
            6.Save to the database
            7.Return the order
            */
            //1.
            var basket = await _basketRepo.GetBasketAsync(basketId);
            //2.
            var items = new List<OrderItem>(); //first create a new list of  items
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemordered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var Orderitem = new OrderItem(itemordered, productItem.Price, item.Quantity);  //only we take the quantity from the customer ordered but we pick the price from the database
                items.Add(Orderitem);
            }

            //3.
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            //4.
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            //check if there is an existing order
            var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
            var existingOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if (existingOrder != null)
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basket.PaymentIntentId);
            }
            //5.
            var Order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal, basket.PaymentIntentId);
            //6.
            _unitOfWork.Repository<Order>().Add(Order);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null; //this means the order is failed/not saved to the database

            //7. 
            return Order;

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethods()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingMethodSpecification(id, buyerEmail);

            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingMethodSpecification(buyerEmail);

            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}