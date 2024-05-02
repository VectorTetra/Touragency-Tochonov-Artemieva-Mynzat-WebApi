namespace TouragencyWebApi.BLL.DTO
{
    public class TourDTO
    {
        // Ідентифікатор туру
        public long Id { get; set; }

        // Дата прибуття
        public int TourNameId { get; set; }
        public string TourName { get; set; }

        // Дата прибуття
        public DateTime ArrivalDate { get; set; }

        // Дата виїзду в тур
        public DateTime DepartureDate { get; set; }

        // Кількість вільних місць в турі
        public int FreeSeats { get; set; }

        // Статус туру (активний, скасований, завершений і т.д.)
        public int TourStateId { get; set; }

        // зв'язок з таблицею Settlements
        public ICollection<int>? CountryIds { get; set; }
        public ICollection<int>? SettlementIds { get; set; }

        // Many-to-many зв'язок з таблицею Hotels
        public ICollection<int>? HotelIds { get; set; }
        // One-to-many зв'язок з таблицею Reviews
        public ICollection<long> ReviewIds { get; set; }
        public ICollection<int>? TransportTypeIds { get; set; }
        public ICollection<long>? BookingIds { get; set; }
        public ICollection<int>? ClientIds { get; set; }
        //-----------------------------------------------------------------------
        // Нові поля для уточнення пошуку

        // Сервіси готелів у турі - харчування, розваги тощо
        public ICollection<int>? HotelServiceIds { get; set; }
        // Тривалість туру - (3-4 днів, 6-7 днів тощо)
        public ICollection<int>? TourDuration { get; set; }
    }
}
