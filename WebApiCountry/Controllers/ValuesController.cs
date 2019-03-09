using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Entities;
using WebApi.Domain.Interfaces;
using WebApi.Service.Services;
using WebApi.Service.Validators;

namespace WebApiCountry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICountryService iCountryService;

        public ValuesController(ICountryService iCountryService)
        {
            this.iCountryService = iCountryService;
        }


        // GET api/values
        [HttpGet]
        public ActionResult  Get()
        {
            try
            {
                var countries = iCountryService.GetAll();
                return Ok(countries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "CountryCreated")]
         public ActionResult<Country> Get(int id)
        {
            try
            {
                var country = iCountryService.Get(id);
                if (country == null)
                {
                    return NotFound();
                }
                return Ok(country);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Country country)
        {
            try
            {
                var objCountry = iCountryService.Post<CountryValidator>(country);
                return new CreatedAtRouteResult("CountryCreated", new { id = objCountry.Id }, objCountry);
            }
            catch (ArgumentNullException )
            {
                return NotFound("Country does not exist");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Country country)
        {
            if (id != country.Id)
            {
                BadRequest();
            }
            try
            {

                iCountryService.Put<CountryValidator>(country);
                return Ok(country);
            }
            catch (ArgumentNullException)
            {
                return NotFound("country does not exist");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                iCountryService.Delete(id);
                return Ok();
            }
            catch (ArgumentException)
            {
                return NotFound("country does not exist");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
