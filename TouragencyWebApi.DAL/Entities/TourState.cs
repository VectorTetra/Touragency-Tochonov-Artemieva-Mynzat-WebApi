// using EFCore;
using Microsoft.EntityFrameworkCore;

namespace TouragencyWebApi.DAL.Entities
{
	public class TourState
	{
		public int Id { get; set; }
		public string Status { get; set; }
		public string? Description { get; set; }
		public virtual ICollection<Tour> Tours { get; set; }
	}
}