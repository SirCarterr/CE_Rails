using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IBookingRepository
    {
        public Task<IEnumerable<BookingDTO>> GetAll();
        public Task<IEnumerable<BookingDTO>> GetSpecific(int trainId);
        public Task<BookingDTO> Get(int id);
        public Task<IEnumerable<BookingDTO>> GetUser(string userId);
        public Task<BookingDTO> Create(BookingDTO dto);
        public Task<BookingDTO> Update(BookingDTO dto);
        public Task<int> Delete(int id);
        public Task ChangeStatus(int id, string status);
    }
}
