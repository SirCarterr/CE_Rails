﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Stripe.Checkout;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [ActionName("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] StripePaymentDTO paymentDTO)
        {
            try
            {
                var domain = _configuration.GetValue<string>("Client_Url");

                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain + paymentDTO.ReturnUrl,
                    CancelUrl = domain + paymentDTO.ReturnUrl + $"/{paymentDTO.Booking.BookingHeaderDTO.Id}",
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                foreach (var item in paymentDTO.Booking.BookingDetailDTOs)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Cost * 100), //20.00 -> 2000
                            Currency = "eur",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.TicketType,
                                Description = $"Wagon {item.WagonNumber}, seat {item.SeatNumber}"
                            }
                        },
                        Quantity = 1
                    };
                    options.LineItems.Add(sessionLineItem);
                }



                var service = new SessionService();
                Session session = service.Create(options);
                return Ok(new SuccessModelDTO()
                {
                    Data = session.Id + ";" + session.PaymentIntentId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}