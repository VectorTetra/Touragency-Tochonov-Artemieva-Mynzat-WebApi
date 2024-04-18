using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouragencyWebApi.DAL.Entities
{
    public class HotelImage
    {
        public long Id { get; set; }
        public string ImageUrl { get; set; }
        public virtual Hotel? Hotel { get; set; }
    }
}
