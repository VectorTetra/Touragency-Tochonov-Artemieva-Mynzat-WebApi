using Microsoft.EntityFrameworkCore;

// Creating a model Client with properties
// Id, PersonId, Person
namespace TouragencyWebApi.DAL.Entities
{
    [Index(nameof(TouristNickname), IsUnique = true)]
    public class Client
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public string TouristNickname { get; set; }
        public string? AvatarImagePath { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public virtual ICollection<Booking>? Bookings { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<Tour>? Tours { get; set; }
        public int TouragencyAccountRoleId { get; set; } = 3;
        public virtual TouragencyAccountRole TouragencyAccountRole { get; set; }

    }
}
