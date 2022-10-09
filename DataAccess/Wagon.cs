using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Wagon
    {
        [Key]
        public int Id { get; set; }
        public int TrainId { get; set; }
        [ForeignKey("TrainId")]
        public Train Train { get; set; }
        public int Number { get; set; }
        [Required]
        public string Type { get; set; }
        public int NumberOfSeats { get; set; }
        public double Price { get; set; }
    }
}
