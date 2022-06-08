namespace TripPlanner.Domain.Models
{
    public class House : DomainObject
    {
        public string? Address { get; set; }
        public Locality Locality { get; set; } = null!;
        public Owner Owner { get; set; } = null!;
        public int Beds { get; set; }
        public double Price { get; set; }
        public int Area { get; set; }
    }
}
