using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IStopRepository
    {
        public Task<StopDTO> Create(StopDTO stopDTO);
        public Task<IEnumerable<StopDTO>> CreateRange(IEnumerable<StopDTO> stopDTOs);
        public Task<StopDTO> Update(StopDTO stopDTO);
        public Task<int> Delete(int id);
        public Task<int> DeleteRange(int trainId);
        public Task<IEnumerable<StopDTO>> Get(int trainId);
        public Task<IEnumerable<StopDTO>> GetAll();
    }
}
