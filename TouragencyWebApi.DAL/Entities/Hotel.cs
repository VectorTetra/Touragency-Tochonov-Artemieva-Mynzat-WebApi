using Microsoft.EntityFrameworkCore;

// Creating a model Hotel with the following properties
// Id, Name, Stars int null, Resort Resort, ResortId int, HotelConfiguration HotelConfiguration, HotelConfigurationId int
namespace TouragencyWebApi.DAL.Entities
{
	public class Hotel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int? Stars { get; set; }
		public virtual Resort Resort { get; set; }
		public int ResortId { get; set; }
		public virtual HotelConfiguration HotelConfiguration { get; set; }
		public int HotelConfigurationId { get; set; }

		// Many-to-many relationship between Hotel and Tour
		public virtual ICollection<Tour> Tours { get; set; }
		//One-to-many relationship between Hotel and Booking
		public virtual ICollection<Booking> Bookings { get; set; }
		public virtual ICollection<HotelService> HotelServices { get; set; }
	}
}
