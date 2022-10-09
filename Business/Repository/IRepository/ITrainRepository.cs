using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface ITrainRepository
    {
        public Task<TrainDTO> Create(TrainDTO trainDTO);
        public Task<TrainDTO> Update(TrainDTO trainDTO);
        public Task<int> Delete(int id);
        public Task<TrainDTO> Get(int id);
        public Task<IEnumerable<TrainDTO>> GetAll();
    }
}
