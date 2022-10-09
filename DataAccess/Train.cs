using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Train
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Schedule { get; set; }
        public string Type { get; set; } //high-speed, intercity
    }
}
