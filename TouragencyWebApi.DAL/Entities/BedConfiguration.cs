using Microsoft.EntityFrameworkCore;
namespace TouragencyWebApi.DAL.Entities
{
    public class BedConfiguration
    {
        public int Id { get; set; }
        public string ConfigDescription { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
