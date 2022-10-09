using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BookingHeaderDTO
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int TrainId { get; set; }
        public string? BookingCode { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-z0-9_.-]+@[a-zA-z0-9-]+.[a-zA-z0-9-.]+$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\+?[1-9][0-9]{7,14}$", ErrorMessage = "Invalid number input")]
        public string PhoneNumber { get; set; }

        public string? TrainName { get; set; }
        public string? Route { get; set; }
        public DateTime Date { get; set; }
        public double TotalCost { get; set; }
        public DateTime BookedDate { get; set; }
        
        public string? Status { get; set; }

        //stripe payment
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
    }
}
