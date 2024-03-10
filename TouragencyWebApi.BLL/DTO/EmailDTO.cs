using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouragencyWebApi.BLL.DTO
{
    public class EmailDTO
    {
        public long Id { get; set; }
        public string EmailAddress { get; set; }
        public int ContactTypeId { get; set; }
    }
}
