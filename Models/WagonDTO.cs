using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WagonDTO
    {
        public int Id { get; set; }
        public int TrainId { get; set; }
        public TrainDTO Train { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cannot be less than 0")]
        public int Number { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cannot be less than 0")]
        public int NumberOfSeats { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cannot be less than 0")]
        public double Price { get; set; }
    }
}
