using ClientSide.Service.IService;
using Models;
using Newtonsoft.Json;
using System.Text;

namespace ClientSide.Service
{
    public class BookingsService : IBookingsService
    {
        private readonly HttpClient _httpClient;

        public BookingsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BookingDTO> Create(BookingDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Booking/Create", bodyContent);
            string responseResult = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<BookingDTO>(responseResult);
                return result;
            }
            return new BookingDTO();
        }

        public async Task<BookingDTO> Update(BookingDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Booking/update", bodyContent);
            string responseResult = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<BookingDTO>(responseResult);
                return result;
            }
            return new BookingDTO();
        }

        public async Task<bool> StatusPaid(BookingDTO dto)
        { 
            var content = JsonConvert.SerializeObject(dto.BookingHeaderDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/api/Booking/statuspaid", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ChangeStatus(BookingDTO dto, string status)
        {
            var statusChange = new StatusChangeDTO()
            {
                BookingId = dto.BookingHeaderDTO.Id,
                Status = status
            };

            var content = JsonConvert.SerializeObject(statusChange);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/api/Booking/updatestatus", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<BookingDTO> Get(int id)
        {
            var response = await _httpClient.GetAsync($"api/booking/get/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var booking = JsonConvert.DeserializeObject<BookingDTO>(content);
                return booking;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<BookingDTO>> GetAll()
        {
            var response = await _httpClient.GetAsync("api/booking/getall");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var bookings = JsonConvert.DeserializeObject<IEnumerable<BookingDTO>>(content);
                return bookings;
            }

            return new List<BookingDTO>();
        }

        public async Task<IEnumerable<BookingDTO>> GetSpecific(int trainId)
        {
            var response = await _httpClient.GetAsync($"api/booking/getspecific/{trainId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var bookings = JsonConvert.DeserializeObject<IEnumerable<BookingDTO>>(content);
                return bookings;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }

            return new List<BookingDTO>();
        }

        public async Task<IEnumerable<BookingDTO>> GetUser(string userId)
        {
            var response = await _httpClient.GetAsync($"api/booking/getuser/{userId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var bookings = JsonConvert.DeserializeObject<IEnumerable<BookingDTO>>(content);
                return bookings;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }

            return new List<BookingDTO>();
        }
    }
}
