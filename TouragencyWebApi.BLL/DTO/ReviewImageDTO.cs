using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.DTO
{
    public class ReviewImageDTO
    {
        public long Id { get; set; }
        public long ReviewId { get; set; }
        public string ImagePath { get; set; }
    }
}
