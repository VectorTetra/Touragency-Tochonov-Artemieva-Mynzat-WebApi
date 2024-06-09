using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.DTO
{
    public class ContinentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<int> CountryIds { get; set; }
    }
}
