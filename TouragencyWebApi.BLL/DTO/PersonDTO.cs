using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.DTO
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Middlename { get; set; }
        public ICollection<long>? EmailIds { get; set; }
        public ICollection<string>? Emails { get; set; }
        public ICollection<long>? PhoneIds { get; set; }
        public ICollection<string>? Phones { get; set; }
        public int? TouragencyEmployeeId { get; set; }
        public int? ClientId { get; set; }
    }
}
