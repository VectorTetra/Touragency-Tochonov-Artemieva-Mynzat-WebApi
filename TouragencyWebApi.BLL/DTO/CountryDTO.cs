using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;


namespace TouragencyWebApi.BLL.DTO
{
    public class CountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FlagUrl { get; set; }
        public ICollection<int>? SettlementIds { get; set; }
    }
}
