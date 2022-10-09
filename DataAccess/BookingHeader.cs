using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BookingHeader
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TrainId { get; set; }
        public string BookingCode { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string TrainName { get; set; }
        public string Route { get; set; }
        public DateTime Date { get; set; }
        public double TotalCost { get; set; }
        public DateTime BookedDate { get; set; }

        public string Status { get; set; }

        //stripe payment
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
    }
}
