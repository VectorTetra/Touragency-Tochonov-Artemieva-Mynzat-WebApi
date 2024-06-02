using Microsoft.EntityFrameworkCore;

// Creating a Settlement model with the following properties: Id, Name, and Region
namespace TouragencyWebApi.DAL.Entities
{
	public class Settlement
	{
		public int Id { get; set; }
		public string Name { get; set; }
		//public virtual Region Region { get; set; }

		//Many-to-many relationship between Settlement and Tour
		public virtual ICollection<TourName> TourNames { get; set; }
		public virtual ICollection<Hotel> Hotels { get; set; }
		
		// One-to-many relationship between Settlement and Country
		public virtual Country Country { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otheBC = (Settlement)obj;
            return Id == otheBC.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}