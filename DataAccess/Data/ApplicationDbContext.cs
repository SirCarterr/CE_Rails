using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Train> Trains { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<Wagon> Wagons { get; set; }
        public DbSet<BookingHeader> BookingHeaders { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }
    }
}
