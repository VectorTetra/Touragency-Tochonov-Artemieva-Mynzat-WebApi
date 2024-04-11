using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// Creating a model for the BookingChildren table with the following fields
// bigint BookingId, smallint ChildrenCount, smallint ChildrenAge
namespace TouragencyWebApi.DAL.Entities
{
    #region BookingChildren_v1.0
    /*
    [PrimaryKey(nameof(BookingId), nameof(ChildrenCount), nameof(ChildrenAge))]
    public class BookingChildren
	{
    	[Column(Order = 1)]
		public long BookingId { get; set; }

    	[Column(Order = 2)]
		public short ChildrenCount { get; set; }

		[Column(Order = 3)]
		public short ChildrenAge { get; set; }

		public virtual Booking Booking { get; set; }
	}
	*/
    #endregion BookingChildren_v1.0
    public class BookingChildren
    {
        public long Id { get; set; }
        public long BookingId { get; set; }
        public short ChildrenCount { get; set; }
        public short ChildrenAge { get; set; }
        public virtual BookingData BookingData { get; set; }
    }
} 