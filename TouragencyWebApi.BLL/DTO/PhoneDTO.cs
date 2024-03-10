using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouragencyWebApi.BLL.DTO
{
    public class PhoneDTO
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public int ContactTypeId { get; set; }
    }
}
