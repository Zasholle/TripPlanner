namespace TripPlanner.Domain.Models
{
    public class User : DomainObject
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FullName { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; } = null!;
    }
}
