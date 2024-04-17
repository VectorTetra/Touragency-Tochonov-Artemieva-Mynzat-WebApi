using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouragencyWebApi.BLL.DTO
{
    public class BookingDataDTO
    {
        public long Id { get; set; }
        public long BookingId { get; set; }
        public int RoomNumber { get; set; }
        public DateTime DateBeginPeriod { get; set; }
        public DateTime DateEndPeriod { get; set; }
        public int TotalPrice { get; set; }
        public short AdultsCount { get; set; }
        public ICollection<long>? BookingChildrenIds { get; set; }
        public int BedConfigurationId { get; set; }
    }
}
