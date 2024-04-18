using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.DTO
{
    public class TourNameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PageJSONStructureUrl { get; set; }
        public ICollection<long>? TourIds { get; set; }
        public ICollection<long>? TourImageIds { get; set; }
    }
}
