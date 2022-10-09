using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IWagonRepository
    {
        public Task<WagonDTO> Create(WagonDTO stopDTO);
        public Task<IEnumerable<WagonDTO>> CreateRange(IEnumerable<WagonDTO> wagonDTOs);
        public Task<WagonDTO> Update(WagonDTO stopDTO);
        public Task<int> Delete(int id);
        public Task<int> DeleteRange(int trainId);
        public Task<IEnumerable<WagonDTO>> Get(int trainId);
        public Task<IEnumerable<WagonDTO>> GetAll();
    }
}
