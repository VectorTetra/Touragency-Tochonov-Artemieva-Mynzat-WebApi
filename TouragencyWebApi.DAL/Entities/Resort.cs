using Microsoft.EntityFrameworkCore;

// Creating a Resort model with the following properties: Id, Name, SettlementId, and Settlement.
namespace TouragencyWebApi.DAL.Entities
{
	public class Resort
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int SettlementId { get; set; }
		public virtual Settlement Settlement { get; set; }

		//One-to-many relationship between Resort and Hotel
		public virtual ICollection<Hotel> Hotels { get; set; }
	}
}