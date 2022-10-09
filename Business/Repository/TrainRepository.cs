using AutoMapper;
using Business.Repository.IRepository;
using DataAccess;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class TrainRepository : ITrainRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public TrainRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<TrainDTO> Create(TrainDTO trainDTO)
        {
            var train = _mapper.Map<TrainDTO, Train>(trainDTO);
            _db.Trains.Add(train);
            await _db.SaveChangesAsync();
            return _mapper.Map<Train, TrainDTO>(train);
        }

        public async Task<int> Delete(int id)
        {
            var train = await _db.Trains.FirstOrDefaultAsync(u => u.Id == id);
            if (train != null)
            {
                _db.Trains.Remove(train);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<TrainDTO> Get(int id)
        {
            var train = await _db.Trains.FirstOrDefaultAsync(u => u.Id == id);
            if (train != null)
            {
                return _mapper.Map<Train, TrainDTO>(train);
            }
            return new TrainDTO();
        }

        public async Task<IEnumerable<TrainDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Train>, IEnumerable<TrainDTO>>(_db.Trains);
        }

        public async Task<TrainDTO> Update(TrainDTO trainDTO)
        {
            var trainFromDb = await _db.Trains.FirstOrDefaultAsync(u => u.Id == trainDTO.Id);
            if (trainFromDb != null)
            {
                trainFromDb.Name = trainDTO.Name;
                trainFromDb.Schedule = trainDTO.Schedule;
                trainFromDb.Type = trainDTO.Type;
                _db.Trains.Update(trainFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<Train, TrainDTO>(trainFromDb);
            }
            return trainDTO;
        }
    }
}
