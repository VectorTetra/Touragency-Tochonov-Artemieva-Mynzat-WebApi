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
        public bool IsHaveNightRides { get; set; }
        public short NightRidesCount { get; set; }
        public string? Route { get; set; }
        public int Duration { get; set; }
        public ICollection<long> TourIds { get; set; }
        public ICollection<long>? TourImageIds { get; set; }
        public ICollection<int> CountryIds { get; set; }
        public ICollection<int> SettlementIds { get; set; }

        public ICollection<long>? ReviewIds { get; set; }

        // Many-to-many зв'язок з таблицею Hotels
        public ICollection<int> HotelIds { get; set; }
        public ICollection<int> TransportTypeIds { get; set; }
        public ICollection<CountryDTO>? Countries { get; set; }
        public ICollection<SettlementDTO>? Settlements { get; set; }
        public ICollection<HotelDTO>? Hotels { get; set; }
    }
}
