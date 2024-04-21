using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouragencyWebApi.BLL.DTO
{
    public class TouragencyAccountRegisterDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Middlename { get; set; }
        public string Login {  get; set; }
        public string PositionName { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int TouragencyAccountRoleId { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
