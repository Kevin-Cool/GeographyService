using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOmodels;
using DomainLayer.IRepositorys;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/river")]
    [ApiController]
    public class RiverController : ControllerBase
    {
        private readonly IRiverRepository _riverrepo;
        private readonly ICountryRepository _countryRepo;
        public RiverController(IRiverRepository riverrepo, ICountryRepository countryRepo)
        {
            _riverrepo = riverrepo;
            _countryRepo = countryRepo;
        }


        [HttpGet("/api/river")]
        [HttpHead("/api/river")]
        public IEnumerable<RiverDTO> GetAll()
        {
            try
            {
                List<River> rivers = _riverrepo.GetAll().ToList();
                if (rivers is null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                else
                {
                    return rivers.Select(c => new RiverDTO(c));
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpGet("/api/river/{id}")]
        [HttpHead("/api/river/{id}")]
        public ActionResult<RiverDTO> GetById(int id)
        {
            try
            {
                River river = _riverrepo.GetById(id);
                if (river is null)
                {
                    return NotFound("river met id:" + id + " bestaat niet");
                }
                else
                {
                    return new RiverDTO(river);
                }
            }
            catch (Exception e)
            {
                return NotFound("Somthing whent wrong :" + e.Message);
            }
        }
        [HttpPost("/api/river")]
        public ActionResult<RiverDTO> Post([FromBody] River river)
        {
            try
            {
                foreach (int countryID in river.BelongsToIDs)
                {
                    river.BelongsTo.Add(_countryRepo.GetById(countryID));
                }


                River newriver = _riverrepo.Add(river);
                RiverDTO tempRiver = new RiverDTO(newriver);
                return CreatedAtAction(nameof(GetById), new { id = tempRiver.ID }, tempRiver);
            }
            catch (Exception e)
            {
                return NotFound("Somthing whent wrong :" + e.Message);
            }

        }
        [HttpPut("/api/river")]
        public IActionResult PutCountry([FromBody] River river)
        {
            try
            {
                if (river == null)
                {
                    return BadRequest("Deze river bestaat niet.");
                }
                if (!_riverrepo.Exists(river))
                {
                    River newriver = _riverrepo.Add(river);
                    RiverDTO tempRiver = new RiverDTO(newriver);
                    return CreatedAtAction(nameof(GetById), new { id = tempRiver.ID }, tempRiver);
                }
                _riverrepo.Update(river);
                return new OkObjectResult("river was updated.");
            }
            catch (Exception e)
            {
                return NotFound("Somthing whent wrong :" + e.Message);
            }

        }
        [HttpDelete("/api/river/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (_riverrepo.Exists(id))
                {
                    _riverrepo.Remove(id);
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
    }
}
