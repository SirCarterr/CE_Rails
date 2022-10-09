using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StopDTO
    {
        [Key]
        public int Id { get; set; }
        public int TrainId { get; set; }
        public TrainDTO Train { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime ArrivalTime { get; set; }
        [Required]
        public DateTime DepatureTime { get; set; }
    }
}
