using ClientSide.ViewModels;

namespace ClientSide.Service.IService
{
    public interface IBookingService
    {
        Task AddBooking(SearchResultVM result);
        Task RemoveBooking(SearchResultVM result);
        Task<SearchResultVM> GetBooking();
    }
}
