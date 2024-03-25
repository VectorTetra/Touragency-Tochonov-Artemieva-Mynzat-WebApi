namespace TouragencyWebApi.DAL.Entities
{
	public class Tour
	{
		// Ідентифікатор туру
		public int Id { get; set; }

		// Дата прибуття
		public virtual TourName Name { get; set; }

		// Дата прибуття
		public DateTime ArrivalDate { get; set; }

		// Дата виїзду в тур
		public DateTime DepartureDate { get; set; }

		// Покажчик, чи є в турі нічні переїзди
		public bool IsHaveNightRides { get; set; }

		// Кількість нічних переїздів
		public short NightRidesCount { get; set; }

		// Кількість вільних місць в турі
		public int FreeSeats { get; set; }

		// Статус туру (активний, скасований, завершений і т.д.)
		public virtual TourState TourState { get; set; }

		// Повний маршрут туру
		public string? Route { get; set; }

		// Many-to-many зв'язок з таблицею Settlements
		public virtual ICollection<Settlement> Settlements { get; set; }

		// Many-to-many зв'язок з таблицею Hotels
		public virtual ICollection<Hotel> Hotels { get; set; }

		// One-to-many зв'язок з таблицею Reviews
		public virtual ICollection<Review> Reviews { get; set; }
		public virtual ICollection<TransportType> TransportTypes { get; set; }
		public virtual ICollection<Booking> Bookings { get; set; }
		public virtual ICollection<Client> Clients { get; set; }

	}
}