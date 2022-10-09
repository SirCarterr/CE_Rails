using Business.Repository.IRepository;
using Common;
using Microsoft.AspNetCore.Mvc;
using Models;
using Stripe.Checkout;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _bookingRepository.GetAll());
        }

        [HttpGet("{trainId}")]
        [ActionName("GetSpecific")]
        public async Task<IActionResult> GetSpecific(int? trainId)
        {
            if (trainId == null || trainId == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var bookings = await _bookingRepository.GetSpecific(trainId.Value);

            return Ok(bookings);
        }

        [HttpGet("{userId}")]
        [ActionName("GetUser")]
        public async Task<IActionResult> GetUser(string? userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var bookings = await _bookingRepository.GetUser(userId!);

            return Ok(bookings);
        }

        [HttpGet("{id}")]
        [ActionName("Get")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            
            var booking = await _bookingRepository.Get(id.Value);

            if (booking == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(booking);
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingDTO bookingDto)
        {
            bookingDto.BookingHeaderDTO.BookedDate = DateTime.Now;
            var result = await _bookingRepository.Create(bookingDto);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> UpdateBooking([FromBody] BookingDTO bookingDto)
        {
            var result = await _bookingRepository.Update(bookingDto);
            return Ok(result);
        }

        [HttpPut]
        [ActionName("UpdateStatus")]
        public async Task<IActionResult> UpdateBookingStatus([FromBody] StatusChangeDTO changeDto)
        {
            await _bookingRepository.ChangeStatus(changeDto.BookingId, changeDto.Status);
            return Ok();
        }

        [HttpPut]
        [ActionName("StatusPaid")]
        public async Task<IActionResult> StatusPaid([FromBody] BookingHeaderDTO bookingDto)
        {
            var service = new SessionService();
            var sessionDetails = service.Get(bookingDto.SessionId);
            if (sessionDetails.PaymentStatus == "paid")
            {
                await _bookingRepository.ChangeStatus(bookingDto.Id, SD.Status_Paid);
                return Ok();
            }
            return BadRequest();
        }
    }
}
