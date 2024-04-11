using Microsoft.EntityFrameworkCore;

namespace TouragencyWebApi.DAL.Entities
{
    public class Booking
    {
        public long Id { get; set; }
        public virtual Client Client { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual ICollection<BookingData>? BookingData { get; set; }
    }
}
