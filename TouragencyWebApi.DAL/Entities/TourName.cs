// using EFCore 
using Microsoft.EntityFrameworkCore;

namespace TouragencyWebApi.DAL.Entities
{
	public class TourName
	{
		public int Id { get; set; }
		public string Name { get; set; }
        // Покажчик, чи є в турі нічні переїзди
        public bool IsHaveNightRides { get; set; }
        // Кількість нічних переїздів
        public short NightRidesCount { get; set; }
        // Повний маршрут туру
        public string? Route { get; set; }
		// Покажчик, скільки днів триває тур
		public int Duration { get; set; }
        public string PageJSONStructureUrl { get; set; }
		// One-to-Many relationship with Tour
		public virtual ICollection<Tour> Tours { get; set; }
		public virtual ICollection<TourImage> TourImages { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
        // Many-to-many зв'язок з таблицею Settlements
        public virtual ICollection<Settlement> Settlements { get; set; }

        // Many-to-many зв'язок з таблицею Hotels
        public virtual ICollection<Hotel> Hotels { get; set; }
        public virtual ICollection<TransportType> TransportTypes { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otheBC = (TourName)obj;
            return Id == otheBC.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

}
