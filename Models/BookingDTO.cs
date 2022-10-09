using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BookingDTO
    {
        public BookingHeaderDTO BookingHeaderDTO { get; set; } = new BookingHeaderDTO();
        public IEnumerable<BookingDetailDTO> BookingDetailDTOs { get; set; } = new List<BookingDetailDTO>();
    }
}
