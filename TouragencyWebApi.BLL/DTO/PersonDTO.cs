using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouragencyWebApi.BLL.DTO
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Middlename { get; set; }
        public ICollection<EmailDTO>? Emails { get; set; }
        public ICollection<PhoneDTO>? Phones { get; set; }
    }
}
