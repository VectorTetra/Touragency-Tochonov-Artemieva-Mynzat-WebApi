using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.DTO
{
    public class HotelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Stars { get; set; }
        public string Description { get; set; }
        //public virtual Resort Resort { get; set; }
        public ICollection<int> HotelConfigurationIds { get; set; }
        public ICollection<int> BedConfigurationIds { get; set; }
        public int SettlementId { get; set; }
        // Many-to-many relationship between Hotel and Tour
        public ICollection<int> TourNameIds { get; set; }
        // One-to-many relationship between Hotel and Booking
        public ICollection<long> BookingIds { get; set; }
        // В цьому полі можуть зберігатися дані про послуги готелю (наприклад, Wi-Fi, сніданок, басейн, парковка, трансфер)
        // А також дані про модель харчування (наприклад, BB, HB, FB, AI)
        public ICollection<int> HotelServiceIds { get; set; }
        public ICollection<long> HotelImageIds { get; set; }
    }
}
