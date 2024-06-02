namespace TouragencyWebApi.DAL.Entities
{
	public class Tour
	{
		// Ідентифікатор туру
		public long Id { get; set; }

		// Дата прибуття
		public virtual TourName Name { get; set; }

		// Дата прибуття
		public DateTime ArrivalDate { get; set; }

		// Дата виїзду в тур
		public DateTime DepartureDate { get; set; }
		// Кількість вільних місць в турі
		public int FreeSeats { get; set; }

		// Статус туру (активний, скасований, завершений і т.д.)
		public virtual TourState TourState { get; set; }

		// One-to-many зв'язок з таблицею Reviews
		public virtual ICollection<Review> Reviews { get; set; }
		public virtual ICollection<Booking> Bookings { get; set; }
		public virtual ICollection<Client> Clients { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherTour = (Tour)obj;
            return Id == otherTour.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }
}