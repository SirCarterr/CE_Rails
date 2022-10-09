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
    public class StopRepository : IStopRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public StopRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<StopDTO> Create(StopDTO stopDTO)
        {
            var stop = _mapper.Map<StopDTO, Stop>(stopDTO);
            _db.Stops.Add(stop);
            await _db.SaveChangesAsync();
            return _mapper.Map<Stop, StopDTO>(stop);
        }

        public async Task<IEnumerable<StopDTO>> CreateRange(IEnumerable<StopDTO> stopDTOs)
        {
            var stops = _mapper.Map<IEnumerable<StopDTO>, IEnumerable<Stop>>(stopDTOs);
            _db.Stops.AddRange(stops);
            await _db.SaveChangesAsync();
            return _mapper.Map<IEnumerable<Stop>, IEnumerable<StopDTO>>(stops);
        }

        public async Task<int> Delete(int id)
        {
            var stop = await _db.Stops.FirstOrDefaultAsync(u => u.Id == id);
            if (stop != null)
            {
                _db.Stops.Remove(stop);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteRange(int trainId)
        {
            var stops = _db.Stops.Where(u => u.TrainId == trainId);
            if (stops.Any())
            {
                _db.Stops.RemoveRange(stops);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<StopDTO>> Get(int trainId)
        {
            var stop = _db.Stops.Include(u => u.Train).Where(u => u.TrainId == trainId);
            if (stop != null)
            {
                return _mapper.Map<IEnumerable<Stop>, IEnumerable<StopDTO>>(stop);
            }
            return new List<StopDTO>();
        }

        public async Task<IEnumerable<StopDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Stop>, IEnumerable<StopDTO>>(_db.Stops.Include(u => u.Train));
        }

        public async Task<StopDTO> Update(StopDTO stopDTO)
        {
            var stopFromDb = await _db.Stops.FirstOrDefaultAsync(u => u.Id == stopDTO.Id);
            if (stopFromDb != null)
            {
                stopFromDb.Name = stopDTO.Name;
                stopDTO.ArrivalTime = stopFromDb.ArrivalTime;
                stopDTO.DepatureTime = stopFromDb.DepatureTime;
                _db.Stops.Update(stopFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<Stop, StopDTO>(stopFromDb);
            }
            return stopDTO;
        }
    }
}
