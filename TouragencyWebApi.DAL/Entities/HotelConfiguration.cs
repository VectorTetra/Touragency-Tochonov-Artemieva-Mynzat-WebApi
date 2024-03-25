using Microsoft.EntityFrameworkCore;

// Creating a new model HotelConfiguration with the following properties
// Id int not null, CompassSide string null, WindowView string null, IsAllowChildren bool not null, IsAllowPets bool not null
namespace TouragencyWebApi.DAL.Entities
{
	public class HotelConfiguration
	{
		public int Id { get; set; }
		public string? CompassSide { get; set; }
		public string? WindowView { get; set; }
		public bool IsAllowChildren { get; set; }
		public bool IsAllowPets { get; set; }

		// Many-to-many relationship between HotelConfiguration and Hotel
		public virtual ICollection<Hotel> Hotels { get; set; }
	}
}
