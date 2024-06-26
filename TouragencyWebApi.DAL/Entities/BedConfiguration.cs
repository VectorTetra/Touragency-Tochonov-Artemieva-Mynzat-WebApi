﻿using Microsoft.EntityFrameworkCore;
namespace TouragencyWebApi.DAL.Entities
{
    public class BedConfiguration
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public short Capacity { get; set; }
        public virtual ICollection<Hotel>? Hotels { get; set; }
        public virtual ICollection<BookingData>? BookingDatas { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otheBC = (BedConfiguration)obj;
            return Id == otheBC.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
