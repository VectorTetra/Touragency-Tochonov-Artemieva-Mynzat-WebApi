using Microsoft.EntityFrameworkCore;

// Creating a Settlement model with the following properties: Id, Name, and Region
namespace TouragencyWebApi.DAL.Entities
{
	public class Settlement
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual Region Region { get; set; }
		public int RegionId { get; set; }

		//Many-to-many relationship between Settlement and Tour
		public virtual ICollection<Tour> Tours { get; set; }
		//One-to-many relationship between Settlement and Resort
		public virtual ICollection<Resort> Resorts { get; set; }
	}
}