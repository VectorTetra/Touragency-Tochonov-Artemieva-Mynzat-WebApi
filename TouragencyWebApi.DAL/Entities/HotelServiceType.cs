namespace TouragencyWebApi.DAL.Entities
{
    public class HotelServiceType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<HotelService> HotelServices { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otheBC = (HotelServiceType)obj;
            return Id == otheBC.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
