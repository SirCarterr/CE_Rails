using Business.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WagonsController : Controller
    {
        private readonly IWagonRepository _wagonRepository;

        public WagonsController(IWagonRepository wagonRepository)
        {
            _wagonRepository = wagonRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _wagonRepository.GetAll());
        }

        [HttpGet("{trainId}")]
        public async Task<IActionResult> Get(int? trainId)
        {
            if (trainId == null || trainId == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var wagons = await _wagonRepository.Get(trainId.Value);

            if (!wagons.Any())
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(wagons);
        }
    }
}
