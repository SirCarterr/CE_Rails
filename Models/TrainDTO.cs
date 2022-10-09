using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TrainDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"((Monday|Tuesday|Wednesday|Thursday|Friday|Saturday|Sunday|Weekdays|Even days|Odd days)+[,\s]*)+", ErrorMessage = "Wrong input")]
        public string Schedule { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
