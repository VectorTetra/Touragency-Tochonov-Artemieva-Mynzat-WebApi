namespace TouragencyWebApi.BLL.DTO
{
    public class ReviewDTO
    {
        public long Id { get; set; }
        public short Rating { get; set; }
        public int ClientId { get; set; }
        public int TourId { get; set; } 
        public string ReviewCaption { get; set; }
        public string ReviewText { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<long> ReviewImageIds { get; set; }
        public int Likes { get; set; }
    }
}
