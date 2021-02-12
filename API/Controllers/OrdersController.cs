using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _mapper = mapper;
            _orderService = orderService;

        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress); //_mapper.Map<source,destination>(sourceObject)
            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));

            return Ok(order);
        }

        //A HttpContext object holds information about the current HTTP request. ... Yes each time it is created it creates a server current state of a HTTP request and response. 
        //It can hold information like, Request, Response, Server, Session, Item, Cache, User's information like authentication and authorization and much more
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersForUser()
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var orders=await _orderService.GetOrdersForUserAsync(email);

            return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(orders)); //will not work without ok as the return type is IReadonlyList
        }
         [HttpGet("{id:int}")]  //if the  int part is not added it will be considered as a string and postman considers this to be the same delivery method and release an error
         public async Task<ActionResult<OrderToReturnDto>> GetOrdersByIdForUser(int id)
         { 
            var email= HttpContext.User.RetrieveEmailFromPrincipal();
            var order=await _orderService.GetOrderByIdAsync(id,email);

            if(order==null) return NotFound (new ApiResponse(404));

            
            return _mapper.Map<Order,OrderToReturnDto>(order);
         }

         [HttpGet("{deliveryMethods}")]
         public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
         {
             return Ok(await _orderService.GetDeliveryMethods());
         }

    }
}