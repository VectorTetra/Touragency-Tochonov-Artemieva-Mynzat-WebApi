using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouragencyWebApi.BLL.DTO
{
    public class TourStateDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string? Description { get; set; }
        public ICollection<int>? TourIds { get; set; }
    }
}
