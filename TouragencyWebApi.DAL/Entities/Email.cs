using Microsoft.EntityFrameworkCore;

// Create a new model Email with the following properties
// Id: long, Email: string, ContactType ContactType, ContactTypeId: int
namespace TouragencyWebApi.DAL.Entities
{
    public class Email
    {
        public long Id { get; set; }
        public string EmailAddress { get; set; }
        public virtual ContactType ContactType { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
        public int ContactTypeId { get; set; }
    }

}