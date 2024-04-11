using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.DTO
{
    public class SettlementDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<int> ToursIds { get; set; }
        public ICollection<int> HotelIds { get; set; }
        public int CountryId { get; set; }
    }
}
