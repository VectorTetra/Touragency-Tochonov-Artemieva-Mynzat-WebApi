using Microsoft.EntityFrameworkCore;

// Creating a Country model with the following properties : Id, Name, FlagUrl
namespace TouragencyWebApi.DAL.Entities
{
	public class Country
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string FlagUrl { get; set; }
		// One-to-many relationship with the Region model
		public virtual ICollection<Region> Regions { get; set; }
	}
}
