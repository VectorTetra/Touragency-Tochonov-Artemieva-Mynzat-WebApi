﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouragencyWebApi.BLL.DTO
{
    public class PhoneDTO
    {
        public long Id { get; set; }
        public string PhoneNumber { get; set; }
        public int ContactTypeId { get; set; }
        public ICollection<int>? PersonIds { get; set; }
    }
}
