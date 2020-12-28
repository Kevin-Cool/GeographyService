using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOmodels;
using DomainLayer.IRepositorys;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContinentCountryCityController : ControllerBase
    {
        private readonly ICityRepository _cityrepo;
        private readonly IContinentRepository _Continentrepo;
        private readonly ICountryRepository _Countryrepo;
        public ContinentCountryCityController(ICityRepository cityrepo, IContinentRepository Continentrepo, ICountryRepository Countryrepo)
        {
            _cityrepo = cityrepo;
            _Continentrepo = Continentrepo;
            _Countryrepo = Countryrepo;
        }
        // ------------------------------------------------- Continents
        [HttpGet("/api/continent/")]
        [HttpHead("/api/continent/")]
        public IEnumerable<ContinentDTO> GetAllContinents()
        {
            try
            {
                List<Continent> continents = _Continentrepo.GetAll().ToList();
                if (continents is null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                else
                {
                    return continents.Select(c => new ContinentDTO(c));
                }

            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpGet("/api/continent/{id}")]
        [HttpHead("/api/continent/{id}")]
        public ActionResult<ContinentDTO> GetContinentsById(int id)
        {
            try
            {

                Continent continents = _Continentrepo.GetById(id);
                if (continents is null)
                {
                    return NotFound("Continent met id:" + id + " bestaat niet");
                }
                else
                {
                    return new ContinentDTO(continents);
                }
            }
            catch (Exception e)
            {
                return NotFound("Somthing whent wrong :" + e.Message);
            }
        }
        [HttpPost("/api/continent/")]
        public ActionResult<ContinentDTO> Post([FromBody] Continent continent)
        {
            try
            {
                Continent newContinent = _Continentrepo.Add(continent);
                return CreatedAtAction(nameof(GetContinentsById), new { id = newContinent.ID }, newContinent);
            }
            catch (Exception e)
            {
                return NotFound("Somthing whent wrong :" + e.Message);
            }
            
        }
        [HttpDelete("/api/continent/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (_Continentrepo.Exists(_Continentrepo.GetById(id)))
                {
                    _Continentrepo.Remove(_Continentrepo.GetById(id));
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return NotFound("Somthing whent wrong :" + e.Message);
            }

        }
        // ------------------------------------------------- Countries
        [HttpGet("/api/continent/{continentid}/country")]
        [HttpHead("/api/continent/{continentid}/country")]
        public IEnumerable<CountryDTO> GetAllCountries(int continentid)
        {
            try
            {
                List<Country> country = _Countryrepo.GetAll(continentid).ToList();
                if (country is null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                else
                {
                    return country.Select(c => new CountryDTO(c));
                }

            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpGet("/api/continent/{continentid}/country/{countryid}")]
        [HttpHead("/api/continent/{continentid}/country/{countryid}")]
        public ActionResult<CountryDTO> GetCountryById(int continentid,int countryid)
        {
            try
            {
                Country country = _Countryrepo.GetById(countryid);
                if (country is null)
                {
                    return NotFound("country met id:" + countryid + " bestaat niet");
                }
                else
                {
                    return new CountryDTO(country);
                }
            }
            catch (Exception e)
            {
                return NotFound("Somthing whent wrong :" + e.Message);
            }
        }
        // ------------------------------------------------- Cities
        [HttpGet("/api/continent/{continentid}/country/{countryid}/city")]
        [HttpHead("/api/continent/{continentid}/country/{countryid}/city")]
        public IEnumerable<CityDTO> GetAllCity(int continentid, int countryid)
        {
            try
            {
                List<City> city = _cityrepo.GetAll(countryid).ToList();
                if (city is null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                else
                {
                    return city.Select(c => new CityDTO(c));
                }

            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpGet("/api/continent/{continentid}/country/{countryid}/city/{cityid}")]
        [HttpHead("/api/continent/{continentid}/country/{countryid}/city/{cityid}")]
        public ActionResult<CityDTO> GetCityById(int continentid, int countryid, int cityid)
        {
            try
            {
                City city = _cityrepo.GetById(countryid);
                if (city is null)
                {
                    return NotFound("city met id:" + countryid + " bestaat niet");
                }
                else
                {
                    return new CityDTO(city);
                }
            }
            catch (Exception e)
            {
                return NotFound("Somthing whent wrong :" + e.Message);
            }
        }

    }
}
