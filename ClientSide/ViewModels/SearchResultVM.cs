using Models;

namespace ClientSide.ViewModels
{
    public class SearchResultVM
    {
        public TrainDTO Train { get; set; }
        public StopDTO DepatureStop { get; set; }
        public StopDTO ArrivalStop { get; set; }
        public DateTime TravelTime { get; set; }
        public double[] Cost { get; set; } = new double[3];
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Babies { get; set; }
        public DateTime Date { get; set; }
    }
}
