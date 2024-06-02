// using EFCore;
using Microsoft.EntityFrameworkCore;

namespace TouragencyWebApi.DAL.Entities
{
	public class TourState
	{
		public int Id { get; set; }
		public string Status { get; set; }
		public string? Description { get; set; }
		public virtual ICollection<Tour> Tours { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otheBC = (TourState)obj;
            return Id == otheBC.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}