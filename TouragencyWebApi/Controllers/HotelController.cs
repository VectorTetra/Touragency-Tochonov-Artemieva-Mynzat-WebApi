﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/Hotel")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _serv;
        public HotelController(IHotelService serv)
        {
            _serv = serv;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels([FromQuery] HotelQuery hotelQuery)
        {
            try
            {
                IEnumerable<HotelDTO> collection = null;
                switch (hotelQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "GetById":
                        {
                            if (hotelQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано HotelId для пошуку!", nameof(hotelQuery.Id));
                            }
                            var acc = await _serv.GetById((int)hotelQuery.Id);
                            if (acc != null)
                            {
                                collection = new List<HotelDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByName":
                        {
                            if (hotelQuery.Name is null)
                            {
                                throw new ValidationException("Не вказано Name для пошуку!", nameof(hotelQuery.Name));
                            }
                            collection = await _serv.GetByNameSubstring(hotelQuery.Name);
                        }
                        break;
                    case "GetByStars":
                        {
                            if (hotelQuery.Stars is null)
                            {
                                throw new ValidationException("Не вказано Stars для пошуку!", nameof(hotelQuery.Stars));
                            }
                            collection = await _serv.GetByStars((int[])hotelQuery.Stars);
                        }
                        break;
                    case "GetByHotelConfigurationId":
                        {
                            if (hotelQuery.HotelConfigurationId is null)
                            {
                                throw new ValidationException("Не вказано HotelConfigurationId для пошуку!", nameof(hotelQuery.HotelConfigurationId));
                            }
                            collection = await _serv.GetByHotelConfigurationId((int)hotelQuery.HotelConfigurationId);
                        }
                        break;
                    case "GetByBedConfigurationId":
                        {
                            if (hotelQuery.BedConfigurationId is null)
                            {
                                throw new ValidationException("Не вказано BedConfigurationId для пошуку!", nameof(hotelQuery.BedConfigurationId));
                            }
                            collection = await _serv.GetByBedConfigurationId((int)hotelQuery.BedConfigurationId);
                        }
                        break;
                    case "GetBySettlementId":
                        {
                            if (hotelQuery.SettlementId is null)
                            {
                                throw new ValidationException("Не вказано SettlementId для пошуку!", nameof(hotelQuery.SettlementId));
                            }
                            collection = await _serv.GetBySettlementId((int)hotelQuery.SettlementId);
                        }
                        break;

                    case "GetByTourId":
                        {
                            if (hotelQuery.TourIds is null)
                            {
                                throw new ValidationException("Не вказано TourId для пошуку!", nameof(hotelQuery.TourIds));
                            }
                            collection = await _serv.GetByTourId((long)hotelQuery.TourIds);
                        }
                        break;
                    case "GetByBookingId":
                        {
                            if (hotelQuery.BookingIds is null)
                            {
                                throw new ValidationException("Не вказано BookingId для пошуку!", nameof(hotelQuery.BookingIds));
                            }
                            collection = await _serv.GetByBookingId((long)hotelQuery.BookingIds);
                        }
                        break;
                    case "GetByHotelServiceId":
                        {
                            if (hotelQuery.HotelServiceIds is null)
                            {
                                throw new ValidationException("Не вказано HotelServiceId для пошуку!", nameof(hotelQuery.HotelServiceIds));
                            }
                            collection = await _serv.GetByHotelServiceId((int)hotelQuery.HotelServiceIds);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Невірно вказаний параметр пошуку!", nameof(hotelQuery.SearchParameter));
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
                return new ObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<HotelDTO>> CreateHotel(HotelDTO hotelDTO)
        {
            try
            {
                await _serv.Create(hotelDTO);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return new ObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<HotelDTO>> UpdateHotel(HotelDTO hotelDTO)
        {
            try
            {
                await _serv.Update(hotelDTO);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return new ObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<HotelDTO>> DeleteHotel(int id)
        {
            try
            {
                await _serv.Delete(id);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return new ObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }
        }
    }

    public class HotelQuery
    {
        public string SearchParameter { get; set; }
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int[]? Stars { get; set; }
        //public virtual Resort Resort { get; set; }
        public int? HotelConfigurationId { get; set; }
        public int? BedConfigurationId { get; set; }
        public int? SettlementId { get; set; }
        // Many-to-many relationship between Hotel and Tour
        public long? TourIds { get; set; }
        // One-to-many relationship between Hotel and Booking
        public long? BookingIds { get; set; }
        // В цьому полі можуть зберігатися дані про послуги готелю (наприклад, Wi-Fi, сніданок, басейн, парковка, трансфер)
        // А також дані про модель харчування (наприклад, BB, HB, FB, AI)
        public int? HotelServiceIds { get; set; }
    }
}
