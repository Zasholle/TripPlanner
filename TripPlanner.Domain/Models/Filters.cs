namespace TripPlanner.Domain.Models
{
    public class Filters
    {
        public double MinArea { get; set; }
        public double MaxArea { get; set; }
        public int MinBeds { get; set; }
        public int MaxBeds { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
    }
}
