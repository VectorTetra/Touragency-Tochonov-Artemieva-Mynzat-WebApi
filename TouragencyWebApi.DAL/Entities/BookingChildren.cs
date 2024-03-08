using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// Creating a model for the BookingChildren table with the following fields
// bigint BookingId, smallint ChildrenCount, smallint ChildrenAge
namespace TouragencyWebApi.DAL.Entities
{
	public class BookingChildren
	{
		[Key]
    	[Column(Order = 1)]
		public long BookingId { get; set; }

		[Key]
    	[Column(Order = 2)]
		public short ChildrenCount { get; set; }

		[Key]
		[Column(Order = 3)]
		public short ChildrenAge { get; set; }

		public virtual Booking Booking { get; set; }
	}
} 