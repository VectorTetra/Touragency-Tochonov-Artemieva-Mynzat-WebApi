using Microsoft.EntityFrameworkCore;

// Creating a model Hotel with the following properties
// Id, Name, Stars int null, Resort Resort, ResortId int, HotelConfiguration HotelConfiguration, HotelConfigurationId int
namespace TouragencyWebApi.DAL.Entities
{
	public class Hotel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int? Stars { get; set; }
        //public virtual Resort Resort { get; set; }
        public string Description { get; set; }
        public virtual ICollection<HotelConfiguration> HotelConfigurations { get; set; }
		public virtual ICollection<BedConfiguration> BedConfigurations { get; set; }
		public virtual Settlement Settlement { get; set; }
		// Many-to-many relationship between Hotel and Tour
		public virtual ICollection<Tour> Tours { get; set; }
		// One-to-many relationship between Hotel and Booking
		public virtual ICollection<Booking> Bookings { get; set; }
		// � ����� ��� ������ ���������� ��� ��� ������� ������ (���������, Wi-Fi, �������, ������, ��������, ��������)
		// � ����� ��� ��� ������ ���������� (���������, BB, HB, FB, AI)
		public virtual ICollection<HotelService> HotelServices { get; set; }
		public virtual ICollection<HotelImage> HotelImages { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherHotel = (Hotel)obj;
            return Id == otherHotel.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }


    }
}
