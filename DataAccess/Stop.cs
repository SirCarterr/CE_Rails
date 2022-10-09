using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Stop
    {
        [Key]
        public int Id { get; set; }
        public int TrainId { get; set; }
        [ForeignKey("TrainId")]
        public Train Train { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepatureTime { get; set; }
    }
}
