using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.DTO
{
    public class HotelServiceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        // Додай віртуальну навігаційну властивість HotelServiceType зв'язок один до багатьох з класом HotelServiceType
        public int HotelServiceTypeId { get; set; }
        // Додай віртуальну навігаційну властивість Hotel зв'язок багато до багатьох з класом Hotel
        public  ICollection<int> HotelIds { get; set; }
    }
}
