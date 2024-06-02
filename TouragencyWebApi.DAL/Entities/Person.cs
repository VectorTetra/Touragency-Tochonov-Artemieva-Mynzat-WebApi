using Microsoft.EntityFrameworkCore;

// Creating a new model Person with the following properties
// Id, Firstname, Lastname, Middlename null, DateOfBirth null
namespace TouragencyWebApi.DAL.Entities
{
	public class Person
	{
		public int Id { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string? Middlename { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
		
		// One-to-one relationship with the TouragencyEmployee model
		public virtual TouragencyEmployee? TouragencyEmployee { get; set; }
		// One-to-one relationship with the Client model
		public virtual Client? Client { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otheBC = (Person)obj;
            return Id == otheBC.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
