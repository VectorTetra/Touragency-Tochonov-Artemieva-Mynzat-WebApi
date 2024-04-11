using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.DTO
{
    public class BookingDTO
    {
        public long Id { get; set; }
        public int ClientId { get; set; }
        public int HotelId { get; set; }
        public long TourId { get; set; }
        public ICollection<long>? BookingChildrenIds { get; set; }
        public ICollection<long>? BookingDataIds { get; set; }
        public int? BedConfigurationId { get; set; }
    }
}
