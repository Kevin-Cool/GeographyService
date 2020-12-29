using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using API.DTOmodels;
using DomainLayer.IRepositorys;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
        #region continents
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
                ContinentDTO tempContinent = new ContinentDTO(newContinent);
                return CreatedAtAction(nameof(GetContinentsById), new { id = tempContinent.ID }, tempContinent);
            }
            catch (Exception e)
            {
                return NotFound("Somthing whent wrong :" + e.Message);
            }
            
        }
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Continent continen)
        {
            try
            {
                if (continen == null)
                {
                    return BadRequest("Deze continen bestaat niet.");
                }
                if (!_Continentrepo.Exists(continen))
                {
                    Continent newContinent = _Continentrepo.Add(continen);
                    ContinentDTO tempContinent = new ContinentDTO(newContinent);
                    return CreatedAtAction(nameof(GetContinentsById), new { id = tempContinent.ID }, tempContinent);
                }
                _Continentrepo.Update(continen);
                return new OkObjectResult("continen was updated.");
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
                if (_Continentrepo.Exists(id))
                {
                    _Continentrepo.Remove(id);
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
        #endregion
        #region Countries
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
                Country country = _Countryrepo.GetById(continentid,countryid);
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
        [HttpPost("/api/continent/{continentid}/country/")]
        public ActionResult<CountryDTO> PostCountry([FromBody] Country country)
        {
            try
            {
                Country newcountry = _Countryrepo.Add(country);
                CountryDTO tempcountryt = new CountryDTO(newcountry);
                return CreatedAtAction(nameof(GetContinentsById), new { id = tempcountryt.ID }, tempcountryt);
            }
            catch (Exception e)
            {
                return NotFound("Somthing whent wrong :" + e.Message);
            }

        }
        [HttpPut("/api/continent/{continentid}/country/")]
        public IActionResult PutCountry([FromBody] Country country)
        {
            try
            {
                if (country == null)
                {
                    return BadRequest("Deze country bestaat niet.");
                }
                if (!_Countryrepo.Exists(country))
                {
                    Country newcountry = _Countryrepo.Add(country);
                    CountryDTO tempcountryt = new CountryDTO(newcountry);
                    return CreatedAtAction(nameof(GetContinentsById), new { id = tempcountryt.ID }, tempcountryt);
                }
                _Countryrepo.Update(country);
                return new OkObjectResult("country was updated.");
            }
            catch (Exception e)
            {
                return NotFound("Somthing whent wrong :" + e.Message);
            }

        }
        [HttpDelete("/api/continent/{id}/country/{countryid}")]
        public ActionResult DeleteCountry(int continentid, int countryid)
        {
            try
            {
                if (_Countryrepo.Exists(countryid))
                {
                    _Countryrepo.Remove(continentid,countryid);
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
        #endregion
        // ------------------------------------------------- Cities
        #region Cities
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
                City city = _cityrepo.GetById(continentid,countryid,cityid);
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
        [HttpPost("/api/continent/{continentid}/country/{countryid}/city/{cityid}")]
        public ActionResult<CityDTO> PostCity([FromBody] City city)
        {
            try
            {
                City newCity = _cityrepo.AddCity(city);
                CityDTO tempCity = new CityDTO(newCity);
                return CreatedAtAction(nameof(GetContinentsById), new { id = tempCity.ID }, tempCity);
            }
            catch (Exception e)
            {
                return NotFound("Somthing whent wrong :" + e.Message);
            }

        }
        [HttpPut("/api/continent/{continentid}/country/{countryid}/city/")]
        public IActionResult PutCity([FromBody] City city)
        {
            try
            {
                if (city == null)
                {
                    return BadRequest("Deze city bestaat niet.");
                }
                if (!_cityrepo.Exists(city))
                {
                    City newCity = _cityrepo.AddCity(city);
                    CityDTO tempCity = new CityDTO(newCity);
                    return CreatedAtAction(nameof(GetContinentsById), new { id = tempCity.ID }, tempCity);
                }
                _cityrepo.Update(city);
                return new OkObjectResult("city was updated.");
            }
            catch (Exception e)
            {
                return NotFound("Somthing whent wrong :" + e.Message);
            }
        }
        [HttpPut("/api/continent/{continentid}/country/{countryid}/Capital/")]
        public IActionResult PutCapital([FromBody] City city)
        {
            try
            {
                if (city == null)
                {
                    return BadRequest("Deze Capital bestaat niet.");
                }
                if (!_cityrepo.Exists(city))
                {
                    City newCity = _cityrepo.AddCapital(city);
                    CityDTO tempCity = new CityDTO(newCity);
                    return CreatedAtAction(nameof(GetContinentsById), new { id = tempCity.ID }, tempCity);
                }
                _cityrepo.Update(city);
                return new OkObjectResult("Capital was updated.");
            }
            catch (Exception e)
            {
                return NotFound("Somthing whent wrong :" + e.Message);
            }
        }
        [HttpDelete("/api/continent/{continentid}/country/{countryid}/capital/{cityid}")]
        public ActionResult DeleteCity(int continentid, int countryid, int cityid)
        {
            try
            {
                if (_cityrepo.Exists(cityid))
                {
                    _cityrepo.Remove(continentid, countryid, cityid);
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
        #endregion

    }
}
