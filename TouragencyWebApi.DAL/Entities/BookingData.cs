using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// Creating a model BookingData with the following properties
// Composite primary key - bigiint BookingId, bigint RoomId, smalldatetime DateBeginPeriod, smalldatetime DateEndPeriod
// money TotalPrice, smallint AdultsCount

namespace TouragencyWebApi.DAL.Entities
{
    #region BookingData_v1.0
    /*
	[PrimaryKey(nameof(BookingId), nameof(RoomNumber), nameof(DateBeginPeriod), nameof(DateEndPeriod))]
    public class BookingData
	{
		
		[Column(Order = 1)]
		public long BookingId { get; set; }
		
		[Column(Order = 2)]
		public int RoomNumber { get; set; }
		
		[Column(Order = 3)]
		public System.DateTime DateBeginPeriod { get; set; }
		
		[Column(Order = 4)]
		public System.DateTime DateEndPeriod { get; set; }
		public int TotalPrice { get; set; }
		public short AdultsCount { get; set; }
		public virtual Booking Booking { get; set; }
		
	}
	 */
    #endregion BookingData_v1.0
    public class BookingData
    {
        public long Id { get; set; }
        public long BookingId { get; set; }
        public int RoomNumber { get; set; }
        public System.DateTime DateBeginPeriod { get; set; }
        public System.DateTime DateEndPeriod { get; set; }
        public int TotalPrice { get; set; }
        public short AdultsCount { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual ICollection<BookingChildren>? BookingChildren { get; set; }
        public virtual BedConfiguration? BedConfiguration { get; set; }
    }
}