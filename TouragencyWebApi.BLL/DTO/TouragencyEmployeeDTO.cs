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
    }
}
