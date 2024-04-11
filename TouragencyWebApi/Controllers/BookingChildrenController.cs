﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/BookingChildren")]
    [ApiController]
    public class BookingChildrenController : ControllerBase
    {
        private readonly IBookingChildrenService _serv;
        public BookingChildrenController(IBookingChildrenService serv)
        {
            _serv = serv;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingChildrenDTO>>> GetBookingChildren([FromQuery] BookingChildrenQuery bookingChildrenQuery)
        {
            try
            {
                IEnumerable<BookingChildrenDTO> collection = null;
                switch (bookingChildrenQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "GetById":
                        {
                            if (bookingChildrenQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано BookingChildrenId для пошуку!", nameof(bookingChildrenQuery.Id));
                            }
                            var acc = await _serv.GetById((long)bookingChildrenQuery.Id);
                            if (acc != null)
                            {
                                collection = new List<BookingChildrenDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByBookingDataId":
                        {
                            if (bookingChildrenQuery.BookingDataId is null)
                            {
                                throw new ValidationException("Не вказано BookingDataId для пошуку!", nameof(bookingChildrenQuery.BookingDataId));
                            }
                            collection = await _serv.GetByBookingDataId((long)bookingChildrenQuery.BookingDataId);
                        }
                        break;
                    case "GetByChildrenCount":
                        {
                            if (bookingChildrenQuery.ChildrenCount is null)
                            {
                                throw new ValidationException("Не вказано ChildrenCount для пошуку!", nameof(bookingChildrenQuery.ChildrenCount));
                            }
                            collection = await _serv.GetByChildrenCount((short)bookingChildrenQuery.ChildrenCount);
                        }
                        break;
                    case "GetByChildrenAge":
                        {
                            if (bookingChildrenQuery.ChildrenAge is null)
                            {
                                throw new ValidationException("Не вказано ChildrenAge для пошуку!", nameof(bookingChildrenQuery.ChildrenAge));
                            }
                            collection = await _serv.GetByChildrenAge((short)bookingChildrenQuery.ChildrenAge);
                        }
                        break;
                    default:
                        throw new ValidationException("Невідомий параметр пошуку!", nameof(bookingChildrenQuery.SearchParameter));
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
        public async Task<ActionResult<string>> CreateBookingChildren(BookingChildrenDTO bookingChildrenDTO)
        {
            try
            {
                await _serv.Create(bookingChildrenDTO);
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
        public async Task<ActionResult<string>> UpdateBookingChildren(BookingChildrenDTO bookingChildrenDTO)
        {
            try
            {
                await _serv.Update(bookingChildrenDTO);
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
        public async Task<ActionResult<string>> DeleteBookingChildren(long id)
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
    public class BookingChildrenQuery
    {
        public string SearchParameter { get; set; }
        public long? Id { get; set; }
        public long? BookingDataId { get; set; }
        public short? ChildrenCount { get; set; }
        public short? ChildrenAge { get; set; }
    }
}