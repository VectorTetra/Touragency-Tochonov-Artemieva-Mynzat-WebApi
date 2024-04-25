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
        public DateTime CreationDate { get; set; }
        public long TourId { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual ICollection<ReviewImage> ReviewImages { get; set; }
        public int Likes { get; set; }
    }
}