using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouragencyWebApi.BLL.DTO
{
    public class BookingChildrenDTO
    {
        public long Id { get; set; }
        public long? BookingDataId { get; set; }
        public short ChildrenCount { get; set; }
        public short ChildrenAge { get; set; }
    }
}
