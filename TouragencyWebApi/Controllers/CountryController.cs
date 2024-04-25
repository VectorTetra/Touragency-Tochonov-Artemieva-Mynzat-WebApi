﻿using Microsoft.AspNetCore.Mvc;
using TouragencyWebApi.BLL.Interfaces;

using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace TouragencyWebApi.Controllers
{
    [Route("api/Country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _serv;
        public CountryController(ICountryService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountry([FromQuery] CountryQuery countryQuery)
        {
            try
            {
                IEnumerable<CountryDTO?> collection = null;
                switch (countryQuery.SearchParameter)
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
                            if (countryQuery.Id == null)
                            {
                                throw new ValidationException("Не вказано CountryId для пошуку!", nameof(countryQuery.Id));
                            }
                            var cntr = await _serv.GetById((int)countryQuery.Id);
                            if (cntr != null)
                            {
                                collection = new List<CountryDTO?> { cntr };
                            }
                        }
                        break;
                    case "GetByName":
                        {
                            if (countryQuery.CountryName == null)
                            {
                                throw new ValidationException("Не вказано CountryQuery для пошуку!", nameof(countryQuery.CountryName));
                            }
                            collection = await _serv.GetByName(countryQuery.CountryName);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр countryQuery.SearchParameter!", nameof(countryQuery.SearchParameter));
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
        public async Task<ActionResult<CountryDTO>> AddCountry(CountryDTO countryDTO)
        {
            try
            {
                var createdCountry = await _serv.Add(countryDTO);
                return Ok(createdCountry);
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
        public async Task<ActionResult> UpdateCountry(CountryDTO countryDTO)
        {
            try
            {
                var updatedCountry = await _serv.Update(countryDTO);
                return Ok(updatedCountry);
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
        public async Task<ActionResult> DeleteCountry(int id)
        {
            try
            {
                var deletedCountry = await _serv.Delete(id);
                return Ok(deletedCountry);
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

    public class CountryQuery
    {
        public string SearchParameter { get; set; } = "GetAll";
        public int? Id { get; set; }
        public string? CountryName { get; set; }

    }
}
