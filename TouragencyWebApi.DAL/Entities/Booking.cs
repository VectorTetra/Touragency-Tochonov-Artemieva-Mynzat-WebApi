using Microsoft.EntityFrameworkCore;

namespace TouragencyWebApi.DAL.Entities
{
    public class Booking
    {
        public long Id { get; set; }
        public int ClientId { get; set; }
        public int HotelId { get; set; }
        public virtual Client Client { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual BookingChildren? BookingChildren { get; set; }
        public virtual BookingData BookingData { get; set; }
    }
}
