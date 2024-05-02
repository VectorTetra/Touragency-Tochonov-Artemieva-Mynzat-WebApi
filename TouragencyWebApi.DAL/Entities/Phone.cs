using Microsoft.EntityFrameworkCore;

// Create Phone model with Id, Phone, ContactType,and ContactTypeId properties
namespace TouragencyWebApi.DAL.Entities
{
	public class Phone
	{
		public long Id { get; set; }
		public string PhoneNumber { get; set; }
		public virtual ContactType ContactType { get; set; }
		public virtual ICollection<Person> Persons { get; set; }
		public int ContactTypeId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otheBC = (Phone)obj;
            return Id == otheBC.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}