using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.DTO
{
    public class TouragencyEmployeeAccountDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int TouragencyAccountRoleId { get; set; }
        public int TouragencyEmployeeId { get; set; }
    }
}
