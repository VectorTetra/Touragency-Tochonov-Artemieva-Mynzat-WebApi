using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouragencyWebApi.DAL.Entities
{
    public class TourImage
    {
        public long Id { get; set; }
        public string ImageUrl { get; set; }
        public virtual TourName? TourName { get; set; }
    }
}
