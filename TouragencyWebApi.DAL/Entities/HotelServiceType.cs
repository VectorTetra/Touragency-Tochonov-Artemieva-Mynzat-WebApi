public class HotelServiceType
{
	public int Id { get; set; }
	public string Description { get; set; }
	public virtual ICollection<HotelService> HotelServices { get; set; }
}
