using Microsoft.EntityFrameworkCore;

// Create Review model with properties Id bigint, ClientId int, Client Client, ReviewText string, CreationDate Date, TourId int, Tour Tour, Likes int, Dislikes int
namespace TouragencyWebApi.DAL.Entities
{
	public class Review
	{
		public long Id { get; set; }
		public short Rating { get; set; }
		public int ClientId { get; set; }
		public virtual Client Client { get; set; }
		public string ReviewCaption { get; set; }
		public string ReviewText { get; set; }
		public string CreationDate { get; set; }
		public int TourId { get; set; }
		public virtual Tour Tour { get; set; }
		public int Likes { get; set; }
	}
} 