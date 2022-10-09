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
    public class WagonRepository : IWagonRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public WagonRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<WagonDTO> Create(WagonDTO wagonDTO)
        {
            var wagon = _mapper.Map<WagonDTO, Wagon>(wagonDTO);
            _db.Wagons.Add(wagon);
            await _db.SaveChangesAsync();
            return _mapper.Map<Wagon, WagonDTO>(wagon);
        }

        public async Task<IEnumerable<WagonDTO>> CreateRange(IEnumerable<WagonDTO> wagonDTOs)
        {
            var wagons = _mapper.Map<IEnumerable<WagonDTO>, IEnumerable<Wagon>>(wagonDTOs);
            _db.Wagons.AddRange(wagons);
            await _db.SaveChangesAsync();
            return _mapper.Map<IEnumerable<Wagon>, IEnumerable<WagonDTO>>(wagons);
        }

        public async Task<int> Delete(int id)
        {
            var wagon = await _db.Wagons.FirstOrDefaultAsync(u => u.Id == id);
            if (wagon != null)
            {
                _db.Wagons.Remove(wagon);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteRange(int trainId)
        {
            var wagons = _db.Wagons.Where(u => u.TrainId == trainId);
            if (wagons.Any())
            {
                _db.Wagons.RemoveRange(wagons);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<WagonDTO>> Get(int trainId)
        {
            var wagon = _db.Wagons.Include(u => u.Train).Where(u => u.TrainId == trainId);
            if (wagon != null)
            {
                return _mapper.Map<IEnumerable<Wagon>, IEnumerable<WagonDTO>>(wagon);
            }
            return new List<WagonDTO>();
        }

        public async Task<IEnumerable<WagonDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Wagon>, IEnumerable<WagonDTO>>(_db.Wagons.Include(u => u.Train));
        }

        public async Task<WagonDTO> Update(WagonDTO wagonDTO)
        {
            var wagonFromDb = await _db.Wagons.FirstOrDefaultAsync(u => u.Id == wagonDTO.Id);
            if (wagonFromDb != null)
            {
                wagonFromDb.Price = wagonDTO.Price;
                wagonFromDb.NumberOfSeats = wagonFromDb.NumberOfSeats;
                wagonFromDb.Type = wagonDTO.Type;
                wagonFromDb.Number = wagonDTO.Number;
                _db.Wagons.Update(wagonFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<Wagon, WagonDTO>(wagonFromDb);
            }
            return wagonDTO;
        }
    }
}
