using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StripePaymentDTO
    {
        public StripePaymentDTO()
        {
            ReturnUrl = "payment";
        }
        public string ReturnUrl { get; set; }
        public BookingDTO Booking { get; set; }
    }
}
