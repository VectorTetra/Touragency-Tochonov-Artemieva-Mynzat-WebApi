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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otheBC = (Booking)obj;
            return Id == otheBC.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
