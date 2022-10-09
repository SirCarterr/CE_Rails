using System.ComponentModel.DataAnnotations;

namespace ClientSide.ViewModels
{
    public class SearchVM
    {
        [Required(ErrorMessage = "This field cannot be empty")]
        public string DepatureStation { get; set; }
        [Required(ErrorMessage = "This field cannot be empty")]
        public string ArrivalStation { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "In booking must be at least one adult")]
        public int Adults { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "The number must be higher than 0")]
        public int Children { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "The number must be higher than 0")]
        public int Babies { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
