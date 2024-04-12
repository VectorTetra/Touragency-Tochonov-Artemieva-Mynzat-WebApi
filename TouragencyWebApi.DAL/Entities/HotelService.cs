using Microsoft.EntityFrameworkCore;
// Додай віртуальну навігаційну властивість HotelServiceType зв'язок один до багатьох з класом HotelServiceType

// Create a new model HotelService with the following properties:
// Id, Name, Description nullable
namespace TouragencyWebApi.DAL.Entities
{
	public class HotelService
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		// Додай віртуальну навігаційну властивість HotelServiceType зв'язок один до багатьох з класом HotelServiceType
		public virtual HotelServiceType HotelServiceType { get; set; }
		// Додай віртуальну навігаційну властивість Hotel зв'язок багато до багатьох з класом Hotel
		public virtual ICollection<Hotel> Hotels { get; set; }		
	}
}
