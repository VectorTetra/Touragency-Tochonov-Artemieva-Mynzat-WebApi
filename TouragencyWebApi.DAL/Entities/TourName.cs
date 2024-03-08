// using EFCore 
using Microsoft.EntityFrameworkCore;

namespace TouragencyWebApi.DAL.Entities
{
	public class TourName
	{
		public int Id { get; set; }
		public string Name { get; set; }

		// One-to-Many relationship with Tour
		public virtual ICollection<Tour> Tours { get; set; }
	}

}
