using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.DTO
{
    public class HotelConfigurationDTO
    {
        public int Id { get; set; }
        public string? CompassSide { get; set; }
        public string? WindowView { get; set; }
        public bool IsAllowChildren { get; set; }
        public bool IsAllowPets { get; set; }

        // Many-to-many relationship between HotelConfiguration and Hotel
        public ICollection<int> HotelIds { get; set; }
    }
}
