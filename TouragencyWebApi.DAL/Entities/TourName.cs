// using EFCore 
using Microsoft.EntityFrameworkCore;

namespace TouragencyWebApi.DAL.Entities
{
	public class TourName
	{
		public int Id { get; set; }
		public string Name { get; set; }
		
		public string PageJSONStructureUrl { get; set; }
		// One-to-Many relationship with Tour
		public virtual ICollection<Tour> Tours { get; set; }
		public virtual ICollection<TourImage> TourImages { get; set; }
	}

}
