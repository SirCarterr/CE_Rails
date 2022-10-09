using Business.Repository.IRepository;
using Common;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using ServerSide.Service.IService;

namespace ServerSide.Service
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITrainRepository _trainRepository;
        private readonly IStopRepository _stopRepository;
        private readonly IWagonRepository _wagonRepository;
        private readonly ApplicationDbContext _db;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db, IStopRepository stopRepository, IWagonRepository wagonRepository, ITrainRepository trainRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _trainRepository = trainRepository;
            _stopRepository = stopRepository;
            _wagonRepository = wagonRepository;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
                if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Manager)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                }
                else
                {
                    return;
                }

                //Create default admin
                IdentityUser user = new IdentityUser()
                {
                    UserName = "admin321@admin.com",
                    Email = "admin321@admin.com",
                    EmailConfirmed = true
                };

                _userManager.CreateAsync(user, "Admin321*").GetAwaiter().GetResult();

                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();

                //Create default Train 1
                TrainDTO train1 = new TrainDTO()
                {
                    Name = "SNFC 017",
                    Schedule = "Odd days",
                    Type = "high-speed"
                };

                TrainDTO train1Db = _trainRepository.Create(train1).GetAwaiter().GetResult();

                List<StopDTO> stops1 = new List<StopDTO>()
                {
                    new StopDTO() { ArrivalTime = new DateTime(), DepatureTime = new DateTime(1, 1, 1, 11, 45, 0), Name = "London", TrainId = train1Db.Id },
                    new StopDTO() { ArrivalTime = new DateTime(1, 1, 1, 14, 20, 0), DepatureTime = new DateTime(), Name = "Paris", TrainId = train1Db.Id },
                };

                _stopRepository.CreateRange(stops1).GetAwaiter().GetResult();

                List<WagonDTO> wagons1 = new List<WagonDTO>()
                {
                    new WagonDTO() { Number = 1, NumberOfSeats = 50, Price = 30.0, TrainId = train1Db.Id, Type = "elite" },
                    new WagonDTO() { Number = 2, NumberOfSeats = 66, Price = 20.0, TrainId = train1Db.Id, Type = "business" },
                    new WagonDTO() { Number = 3, NumberOfSeats = 66, Price = 20.0, TrainId = train1Db.Id, Type = "business" },
                    new WagonDTO() { Number = 4, NumberOfSeats = 82, Price = 10.0, TrainId = train1Db.Id, Type = "economy" },
                    new WagonDTO() { Number = 5, NumberOfSeats = 82, Price = 10.0, TrainId = train1Db.Id, Type = "economy" },
                };

                _wagonRepository.CreateRange(wagons1).GetAwaiter().GetResult();

                //Create default Train 2
                TrainDTO train2 = new TrainDTO()
                {
                    Name = "SNFC 245",
                    Schedule = "Monday, Wednesday, Saturday",
                    Type = "intercity"
                };

                TrainDTO train2Db = _trainRepository.Create(train2).GetAwaiter().GetResult();

                List<StopDTO> stops2 = new List<StopDTO>()
                {
                    new StopDTO() { ArrivalTime = new DateTime(), DepatureTime = new DateTime(0001, 1, 1, 9, 20, 0), Name = "Paris", TrainId = train2Db.Id },
                    new StopDTO() { ArrivalTime = new DateTime(1, 1, 1, 9, 45, 0), DepatureTime = new DateTime(1, 1, 1, 9, 50, 0), Name = "Chartres", TrainId = train2Db.Id },
                    new StopDTO() { ArrivalTime = new DateTime(1, 1, 1, 10, 20, 0), DepatureTime = new DateTime(1, 1, 1, 1, 25, 0), Name = "Nogem-le-Rotrou", TrainId = train2Db.Id },
                    new StopDTO() { ArrivalTime = new DateTime(1, 1, 1, 10, 55, 0), DepatureTime = new DateTime(1, 1, 1, 11, 0, 0), Name = "Le Mans", TrainId = train2Db.Id },
                    new StopDTO() { ArrivalTime = new DateTime(1, 1, 1, 11, 30, 0), DepatureTime = new DateTime(), Name = "Saumur", TrainId = train2Db.Id },
                };

                _stopRepository.CreateRange(stops2).GetAwaiter().GetResult();

                List<WagonDTO> wagons2 = new List<WagonDTO>()
                {
                    new WagonDTO() { Number = 1, NumberOfSeats = 62, Price = 10.0, TrainId = train2Db.Id, Type = "business" },
                    new WagonDTO() { Number = 2, NumberOfSeats = 62, Price = 10.0, TrainId = train2Db.Id, Type = "business" },
                    new WagonDTO() { Number = 3, NumberOfSeats = 77, Price = 5.0, TrainId = train2Db.Id, Type = "economy" },
                    new WagonDTO() { Number = 4, NumberOfSeats = 77, Price = 5.0, TrainId = train2Db.Id, Type = "economy" },
                    new WagonDTO() { Number = 5, NumberOfSeats = 77, Price = 5.0, TrainId = train2Db.Id, Type = "economy" },
                };

                _wagonRepository.CreateRange(wagons2).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
