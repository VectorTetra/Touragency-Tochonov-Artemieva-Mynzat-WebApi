using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.DTO
{
    public class HotelImageDTO
    {
        public long Id { get; set; }
        public string ImageUrl { get; set; }
        public int? HotelId { get; set; }
    }
}
