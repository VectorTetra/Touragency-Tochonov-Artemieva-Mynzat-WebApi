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
        public string? ClientTouristNickname { get; set; }
        public string? TourName { get; set; }
        public int? TourNameId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public ICollection<long>? ReviewImageIds { get; set; }
        public ICollection<string>? ReviewImageUrls { get; set; }
        public int Likes { get; set; }
        public ICollection<ReviewImageDTO>? ReviewImages { get; set; }
    }
}
