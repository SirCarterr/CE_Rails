using Business.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StopsController : Controller
    {
        private readonly IStopRepository _stopRepository;

        public StopsController(IStopRepository stopRepository)
        {
            _stopRepository = stopRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _stopRepository.GetAll());
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

            var stops = await _stopRepository.Get(trainId.Value);

            if (!stops.Any())
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(stops);
        }
    }
}
