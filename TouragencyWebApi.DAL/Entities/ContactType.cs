using Microsoft.EntityFrameworkCore;

// Creating a new model for the ContactType table with the following properties
// key int Id, nvarchar(max) Description
namespace TouragencyWebApi.DAL.Entities
{
	public class ContactType
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public virtual ICollection<Email>? Emails { get; set; }
		public virtual ICollection<Phone>? Phones { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otheBC = (ContactType)obj;
            return Id == otheBC.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
