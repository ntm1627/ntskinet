using System.IO;
using System.Threading.Tasks;
using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using Order = Core.Entities.OrderAggregate.Order;

namespace API.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;

       //The webhook helps  to track the payment status, pending, failed, succeeded
        private readonly string _WebhookSecret; //this is to receive a status from our stripe webhook event and this has to be replaced by yours
         private readonly ILogger<IPaymentService> _logger;

        public PaymentsController(IPaymentService paymentService, ILogger<IPaymentService> logger,IConfiguration config)
        {
            _paymentService = paymentService; 
            _logger = logger;
            _WebhookSecret=config.GetSection("StripeSettings:WebhookSecret").Value;
            
        }

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket= await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if(basket == null) return BadRequest (new ApiResponse(400, "Problem with yourbasket"));

            return basket;
        }

        //This is a method when we receive an event from Stripe webhook
        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _WebhookSecret);

            PaymentIntent intent;
            Order order;        //This order is our order not strip and an alias is there at the top

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":                    //spelling should be with care, these are events from stripe
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeeded {Id}: ", intent.Id);
                    order= await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                    _logger.LogInformation("Order updated to payment received:", order.Id);
                    break;
                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Failed {Id}: ", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                    _logger.LogInformation("Payment Failed: ", order.Id);
                    break;
            }

            return new EmptyResult();
        }
    }
}