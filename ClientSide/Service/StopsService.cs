using ClientSide.Service.IService;
using Models;
using Newtonsoft.Json;

namespace ClientSide.Service
{
    public class StopsService : IStopsService
    {
        private readonly HttpClient _httpClient;

        public StopsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<StopDTO>> Get(int trainId)
        {
            var response = await _httpClient.GetAsync($"/api/stops/{trainId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var stops = JsonConvert.DeserializeObject<IEnumerable<StopDTO>>(content);
                return stops;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                //show error message
                return new List<StopDTO>();
            }
        }

        public async Task<IEnumerable<StopDTO>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/stops");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var stops = JsonConvert.DeserializeObject<IEnumerable<StopDTO>>(content);
                return stops;
            }

            return new List<StopDTO>();
        }
    }
}
