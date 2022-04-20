namespace TripPlanner.Domain.Models
{
    public class Owner : DomainObject
    {
        public string Name { get; set; } = null!;
        public long Phone { get; set; }
        public IEnumerable<House>? Houses { get; set; }
    }
}
