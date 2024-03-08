using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// Creating a model BookingData with the following properties
// Composite primary key - bigiint BookingId, bigint RoomId, smalldatetime DateBeginPeriod, smalldatetime DateEndPeriod
// money TotalPrice, smallint AdultsCount

namespace TouragencyWebApi.DAL.Entities
{
	public class BookingData
	{
		[Key]
		[Column(Order = 1)]
		public long BookingId { get; set; }
		[Key]
		[Column(Order = 2)]
		public int RoomNumber { get; set; }
		[Key]
		[Column(Order = 3)]
		public System.DateTime DateBeginPeriod { get; set; }
		[Key]
		[Column(Order = 4)]
		public System.DateTime DateEndPeriod { get; set; }
		public int TotalPrice { get; set; }
		public short AdultsCount { get; set; }
		public virtual Booking Booking { get; set; }
	}
}