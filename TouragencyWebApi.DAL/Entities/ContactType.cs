using Microsoft.EntityFrameworkCore;

// Creating a new model for the ContactType table with the following properties
// key int Id, nvarchar(max) Description
namespace TouragencyWebApi.DAL.Entities
{
	public class ContactType
	{
		public int Id { get; set; }
		public string Description { get; set; }
	}
}
