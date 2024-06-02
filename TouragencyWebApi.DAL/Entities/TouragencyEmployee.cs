using Microsoft.EntityFrameworkCore;

// Create TouragencyEmployee model with properties
// Id, PersonId, PositionId, Person, Position
namespace TouragencyWebApi.DAL.Entities
{
	public class TouragencyEmployee
	{
		public int Id { get; set; }
		public int PersonId { get; set; }
		public int PositionId { get; set; }
		public virtual Person Person { get; set; }
		public virtual Position Position { get; set; }
		public virtual TouragencyEmployeeAccount? Account { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otheBC = (TouragencyEmployee)obj;
            return Id == otheBC.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}