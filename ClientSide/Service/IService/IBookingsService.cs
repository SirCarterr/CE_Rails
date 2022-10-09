using Models;

namespace ClientSide.Service.IService
{
    public interface IBookingsService
    {
        public Task<IEnumerable<BookingDTO>> GetAll();
        public Task<IEnumerable<BookingDTO>> GetSpecific(int trainId);
        public Task<IEnumerable<BookingDTO>> GetUser(string userId);
        public Task<BookingDTO> Get(int id);
        public Task<BookingDTO> Create(BookingDTO dto);
        public Task<BookingDTO> Update(BookingDTO dto);
        public Task<bool> ChangeStatus(BookingDTO dto, string status);
        public Task<bool> StatusPaid(BookingDTO dto);
    }
}
