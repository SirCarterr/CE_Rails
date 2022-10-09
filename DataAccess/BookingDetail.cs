using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BookingDetail
    {
        public int Id { get; set; }
        public int BookingHeaderId { get; set; }

        public int WagonNumber { get; set; }
        public int SeatNumber { get; set; }
        public string TicketType { get; set; } //adult, child or baby
        public double Cost { get; set; }
    }
}
