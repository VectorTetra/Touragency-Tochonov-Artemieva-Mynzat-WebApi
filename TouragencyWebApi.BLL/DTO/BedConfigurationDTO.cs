using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.DTO
{
    public class BedConfigurationDTO
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public short Capacity { get; set; }
        public ICollection<int>? HotelIds { get; set; }
        public ICollection<long>? BookingDataIds { get; set; }
    }
}
