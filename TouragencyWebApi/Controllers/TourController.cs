using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/Tour")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly ITourService _serv;
        public TourController(ITourService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourDTO>>> GetTours([FromQuery] TourQuery tourQuery)
        {
            try
            {
                IEnumerable<TourDTO> collection = null;
                switch (tourQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "Get200Last":
                        {
                            collection = await _serv.Get200Last();
                        }
                        break;
                    case "GetById":
                        {
                            if (tourQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано TourId для пошуку!", nameof(tourQuery.Id));
                            }
                            var acc = await _serv.GetById((long)tourQuery.Id);
                            if (acc != null)
                            {
                                collection = new List<TourDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByTourName":
                        {
                            if (tourQuery.TourNameId is null)
                            {
                                throw new ValidationException("Не вказано TourNameId для пошуку!", nameof(tourQuery.TourNameId));
                            }
                            collection = await _serv.GetByTourNameId((int)tourQuery.TourNameId);
                        }
                        break;
                    case "GetByDateRange":
                        {
                            if (tourQuery.ArrivalDate is null)
                            {
                                throw new ValidationException("Не вказано ArrivalDate для пошуку!", nameof(tourQuery.ArrivalDate));
                            }
                            if (tourQuery.DepartureDate is null)
                            {
                                throw new ValidationException("Не вказано DepartureDate для пошуку!", nameof(tourQuery.DepartureDate));
                            }
                            collection = await _serv.GetByDateRange(tourQuery.DepartureDate.Value, tourQuery.ArrivalDate.Value);
                        }
                        break;
                    case "GetByTourState":
                        {
                            if (tourQuery.TourStateId is null)
                            {
                                throw new ValidationException("Не вказано TourStateId для пошуку!", nameof(tourQuery.TourStateId));
                            }
                            collection = await _serv.GetByTourStateId(tourQuery.TourStateId.Value);
                        }
                        break;
                    case "GetByCountryId":
                        {
                            if (tourQuery.countryId is null)
                            {
                                throw new ValidationException("Не вказано countryId для пошуку!", nameof(tourQuery.countryId));
                            }
                            collection = await _serv.GetByCountryId(tourQuery.countryId.Value);
                        }
                        break;
                    case "GetBySettlementId":
                        {
                            if (tourQuery.settlementId is null)
                            {
                                throw new ValidationException("Не вказано settlementId для пошуку!", nameof(tourQuery.settlementId));
                            }
                            collection = await _serv.GetBySettlementId(tourQuery.settlementId.Value);
                        }
                        break;
                    case "GetByHotelId":
                        {
                            if (tourQuery.hotelId is null)
                            {
                                throw new ValidationException("Не вказано hotelId для пошуку!", nameof(tourQuery.hotelId));
                            }
                            collection = await _serv.GetByHotelId(tourQuery.hotelId.Value);
                        }
                        break;
                    case "GetByDuration":
                        {
                            if (tourQuery.durationDays is null)
                            {
                                throw new ValidationException("Не вказано durationDays для пошуку!", nameof(tourQuery.durationDays));
                            }
                            collection = await _serv.GetByTourDuration(tourQuery.durationDays);
                        }
                        break;
                    case "GetByHotelServicesIds":
                        {
                            if (tourQuery.hotelServicesIds is null)
                            {
                                throw new ValidationException("Не вказано hotelServicesIds для пошуку!", nameof(tourQuery.hotelServicesIds));
                            }
                            collection = await _serv.GetByHotelServicesIds(tourQuery.hotelServicesIds);
                        }
                        break;
                    case "GetByTransportTypeId":
                        {
                            if (tourQuery.transportTypeId is null)
                            {
                                throw new ValidationException("Не вказано transportTypeId для пошуку!", nameof(tourQuery.transportTypeId));
                            }
                            collection = await _serv.GetByTransportTypeId(tourQuery.transportTypeId.Value);
                        }
                        break;
                    case "GetByTouristNickname":
                        {
                            if (tourQuery.TouristNickname is null)
                            {
                                throw new ValidationException("Не вказано TouristNickname для пошуку!", nameof(tourQuery.TouristNickname));
                            }
                            collection = await _serv.GetByTouristNickname(tourQuery.TouristNickname);
                        }
                        break;
                    case "GetByClientFirstname":
                        {
                            if (tourQuery.ClientFirstname is null)
                            {
                                throw new ValidationException("Не вказано ClientFirstname для пошуку!", nameof(tourQuery.ClientFirstname));
                            }
                            collection = await _serv.GetByClientFirstname(tourQuery.ClientFirstname);
                        }
                        break;
                    case "GetByClientLastname":
                        {
                            if (tourQuery.ClientLastname is null)
                            {
                                throw new ValidationException("Не вказано ClientLastname для пошуку!", nameof(tourQuery.ClientLastname));
                            }
                            collection = await _serv.GetByClientLastname(tourQuery.ClientLastname);
                        }
                        break;
                    case "GetByClientMiddlename":
                        {
                            if (tourQuery.ClientMiddlename is null)
                            {
                                throw new ValidationException("Не вказано ClientMiddlename для пошуку!", nameof(tourQuery.ClientMiddlename));
                            }
                            collection = await _serv.GetByClientMiddlename(tourQuery.ClientMiddlename);
                        }
                        break;
                    case "GetByCountryName":
                        {
                            if (tourQuery.CountryName is null)
                            {
                                throw new ValidationException("Не вказано CountryName для пошуку!", nameof(tourQuery.CountryName));
                            }
                            collection = await _serv.GetByCountryName(tourQuery.CountryName);
                        }
                        break;
                    case "GetBySettlementName":
                        {
                            if (tourQuery.SettlementName is null)
                            {
                                throw new ValidationException("Не вказано SettlementName для пошуку!", nameof(tourQuery.SettlementName));
                            }
                            collection = await _serv.GetBySettlementName(tourQuery.SettlementName);
                        }
                        break;
                    case "GetByHotelName":
                        {
                            if (tourQuery.HotelName is null)
                            {
                                throw new ValidationException("Не вказано HotelName для пошуку!", nameof(tourQuery.HotelName));
                            }
                            collection = await _serv.GetByHotelName(tourQuery.HotelName);
                        }
                        break;
                    case "GetByCompositeSearch":
                        {

                            collection = await _serv.GetByCompositeSearch(tourQuery.TourNameId, tourQuery.countryId, tourQuery.settlementId, tourQuery.hotelId,
                                                               tourQuery.ArrivalDate, tourQuery.DepartureDate, tourQuery.durationDays, tourQuery.hotelServicesIds, tourQuery.transportTypeId, tourQuery.TourStateId,
                                                                                              tourQuery.TouristNickname, tourQuery.ClientFirstname, tourQuery.ClientLastname, tourQuery.ClientMiddlename, tourQuery.CountryName, tourQuery.SettlementName, tourQuery.HotelName);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Невідомий параметр пошуку!", nameof(tourQuery.SearchParameter));
                        }

                }
                if (collection.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return collection?.ToList();
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TourDTO>> CreateTour(TourDTO tour)
        {
            try
            {
                var dto = await _serv.Create(tour);
                return Ok(dto);
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<TourDTO>> UpdateTour(TourDTO tour)
        {
            try
            {
                var dto = await _serv.Update(tour);
                return Ok(dto);
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<TourDTO>> DeleteTour(long id)
        {
            try
            {
                var dto = await _serv.Delete(id);
                return Ok(dto);
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

    public class TourQuery
    {
        public string SearchParameter { get; set; } = "";
        public string? TouristNickname { get; set; }
        public string? ClientFirstname { get; set; }
        public string? ClientLastname { get; set; }
        public string? ClientMiddlename { get; set; }
        public string? CountryName { get; set; }
        public string? SettlementName { get; set; }
        public string? HotelName { get; set; }
        public long? Id { get; set; }
        public int? TourNameId { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public int? TourStateId { get; set; }
        public int? countryId { get; set; }
        public int? settlementId { get; set; }
        public int? hotelId { get; set; }
        public int[]? durationDays { get; set; }
        public int[]? hotelServicesIds { get; set; }
        public int? transportTypeId { get; set; }
    }
}
