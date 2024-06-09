using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.DTO
{
    public class TouragencyEmployeeDTO
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int PositionId { get; set; }
        public int? AccountId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Middlename { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? PositionName { get; set; }
        public string? PositionDescription { get; set; }
        public string? AccountLogin { get; set; }
    }
}
