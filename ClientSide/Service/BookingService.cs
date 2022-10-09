using Blazored.LocalStorage;
using ClientSide.Service.IService;
using ClientSide.ViewModels;
using Common;

namespace ClientSide.Service
{
    public class BookingService : IBookingService
    {
        private readonly ILocalStorageService _localstorage;

        public BookingService(ILocalStorageService localstorage)
        {
            _localstorage = localstorage;
        }

        public async Task AddBooking(SearchResultVM result)
        {
            var booking = await _localstorage.GetItemAsync<List<SearchResultVM>>(SD.Booking);
            if (booking == null)
            {
                booking = new List<SearchResultVM>();
            }
            booking.Clear();

            booking.Add(new SearchResultVM()
            {
                ArrivalStop = result.ArrivalStop,
                DepatureStop = result.DepatureStop,
                Train = result.Train,
                TravelTime = result.TravelTime,
                Cost = result.Cost,
                Adults = result.Adults,
                Children = result.Children,
                Babies = result.Babies,
                Date = result.Date
            });

            await _localstorage.SetItemAsync(SD.Booking, booking);
        }

        public async Task<SearchResultVM> GetBooking()
        {
            var booking = await _localstorage.GetItemAsync<List<SearchResultVM>>(SD.Booking);

            return booking.FirstOrDefault();
        }

        public async Task RemoveBooking(SearchResultVM result)
        {
            var booking = await _localstorage.GetItemAsync<List<SearchResultVM>>(SD.Booking);
            
            booking.Remove(result);

            await _localstorage.SetItemAsync(SD.Booking, booking);
        }
    }
}
