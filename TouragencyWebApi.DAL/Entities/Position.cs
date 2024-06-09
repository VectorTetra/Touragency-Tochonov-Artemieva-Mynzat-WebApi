using Microsoft.EntityFrameworkCore;

// Create Position model with properties Id, Name, Description
namespace TouragencyWebApi.DAL.Entities
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<TouragencyEmployee> TouragencyEmployees { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otheBC = (Position)obj;
            return Id == otheBC.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}