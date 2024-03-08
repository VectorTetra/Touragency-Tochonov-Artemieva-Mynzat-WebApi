using Microsoft.EntityFrameworkCore;

namespace TouragencyWebApi.DAL.Entities
{
    public class Booking
    {
        public long Id { get; set; }
        public int ClientId { get; set; }
        public int HotelId { get; set; }
        public Client Client { get; set; }
        public Hotel Hotel { get; set; }
        public BookingChildren? BookingChildren { get; set; }
        public BookingData BookingData{ get; set; }
    }
}
