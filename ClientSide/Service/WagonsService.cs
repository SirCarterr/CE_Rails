using ClientSide.Service.IService;
using Models;
using Newtonsoft.Json;

namespace ClientSide.Service
{
    public class WagonsService : IWagonService
    {
        private readonly HttpClient _httpClient;

        public WagonsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<WagonDTO>> Get(int trainId)
        {
            var response = await _httpClient.GetAsync($"/api/wagons/{trainId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var wagons = JsonConvert.DeserializeObject<IEnumerable<WagonDTO>>(content);
                return wagons;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                //show error message
                return new List<WagonDTO>();
            }
        }

        public async Task<IEnumerable<WagonDTO>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/wagons");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var wagons = JsonConvert.DeserializeObject<IEnumerable<WagonDTO>>(content);
                return wagons;
            }

            return new List<WagonDTO>();
        }
    }
}
