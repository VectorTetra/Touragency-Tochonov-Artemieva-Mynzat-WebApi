using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public PersonDTO Person { get; set; }
        public string TouristNickname { get; set; }
        public string? AvatarImagePath { get; set; }
        public ICollection<long>? BookingIds { get; set; }
        public ICollection<long>? TourIds { get; set; }
    }
}
