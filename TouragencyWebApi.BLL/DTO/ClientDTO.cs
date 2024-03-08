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
        public int PersonId { get; set; }
        public string TouristNickname { get; set; }
        public string? AvatarImagePath { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public ICollection<long>? BookingIds { get; set; }
    }
}
