using Microsoft.EntityFrameworkCore;

// Create a ReviewImage model with properties Id, ReviewId, ImageUrl, and Review
namespace TouragencyWebApi.DAL.Entities
{
	public class ReviewImage
	{
		public long Id { get; set; }
		public long ReviewId { get; set; }
		public string ImagePath { get; set; }
		public virtual Review Review { get; set; }
	}
}
