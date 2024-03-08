using Microsoft.EntityFrameworkCore;

// Creating a Region model with the following properties: Id, Name, and CountryId
namespace TouragencyWebApi.DAL.Entities
{
	public class Region
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int CountryId { get; set; }
		public virtual Country Country { get; set; }
		public virtual ICollection<Settlement> Settlements { get; set; }
	}
}