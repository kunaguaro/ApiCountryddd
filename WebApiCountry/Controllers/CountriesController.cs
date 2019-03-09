using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApi.Domain.Dtos;
using WebApi.Domain.Entities;
using WebApi.Domain.Interfaces;
using WebApi.Infrastructure.Data.Context;
using WebApi.Service.Validators;
using static WebApiCountry.Controllers.CountriesController;

namespace WebApiCountry.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CountriesController : ControllerBase
    {
       
        private readonly ICountryService countryService;
        private readonly ILogger<Country> logger;


        public CountriesController( ICountryService countryService, ILogger<Country> logger)
        {         
            this.countryService = countryService;
            this.logger = logger;
        }


        /// <summary>Lista  los valores de todos los campos de la tabla Country</summary>
        /// <remarks>Esta API lista los valores de todos los campos de la tabla Country</remarks>
        /// <returns>Lista Country</returns>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var countrys = countryService.GetAll();
                return Ok(countrys);
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong, message description:" + ex.Message);
                return BadRequest(ex);
            }
        }


        /// <summary>Lista los valores del Dto CountryDensity </summary>
        /// <remarks>Esta API lista los valoresdel Dto CountryDensity </remarks>
        /// <returns>Lista CountryDensityDTO</returns>
        [HttpGet]
        [Route("api/[controller]/Densidad")]
        public ActionResult GetAllCountriesDensity()
        {
            try
            {
                List<CountryDensityDTO> model = new List<CountryDensityDTO>();
                model = countryService.GetAllCountriesDensity().ToList();
                return Ok(model);
            }
            catch (Exception ex)
            {

                logger.LogError("Something went wrong, message description:" + ex.Message);
                return BadRequest(ex);
            }

        }



            /// <summary>muestra un pais por su id</summary>
            /// <remarks>Esta API busca un pais por su numero de Id</remarks>
            /// <param name="id"></param>
            /// <returns>Country</returns>
        [HttpGet("{id}", Name = "CountryCreated")]
        public  IActionResult GetCountry([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
             

            }

            var country = countryService.Get(id);

            if (country == null)
            {

                return NotFound();
            }

            return Ok(country);
        }

        /// <summary>Esta API actualiza un  pais</summary>
        /// <remarks>Esta API Actualiza un pais existente</remarks>
        /// <param name="id"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        /// <response code="201">Country modificada</response>
        /// <response code="400">Country valores perdidos o no valido</response>
        /// <response code="500">No se pudo modificar tu Country</response>
        [HttpPut("{id}")]
        public  IActionResult PutCountry([FromRoute] int id, [FromBody] Country country)
        {
            if (id != country.Id)
            {
                BadRequest();
            }
            try
            {
                countryService.Put<CountryValidator>(country);
                return Ok(country);
            }
            catch (ArgumentNullException)
            {
                return NotFound("country does not exist");
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong, message description:" + ex.Message);
                return BadRequest(ex);
            }
        }

        /// <summary>Esta API agrega un nuevo pais</summary>
        /// <remarks>Esta API agrega un nuevo pais</remarks>
        /// <param name="country"></param>
        /// <response code="201">Country creada</response>
        /// <response code="400">Country valores perdidos o no valido</response>
        /// <response code="500">No se pudo crear Country</response>
        [HttpPost]
        public IActionResult PostCountry([FromBody] Country country)
        {
            try
            {
                var objCountry = countryService.Post<CountryValidator>(country);
                logger.LogInformation("Record added " + country.Id.ToString());
                return new CreatedAtRouteResult("CountryCreated", new { id = objCountry.Id }, objCountry);
            }
            catch (ArgumentNullException )
            {
                return NotFound("Country does not exist");
            }
            catch ( FluentValidation.ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }

        }

        /// <summary>Esta Api Elimina un Pais</summary>
        /// <remarks>Esta API Elimina un Pais</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public  IActionResult DeleteCountry([FromRoute] int id)
        {
            try
            {
                countryService.Delete(id);
                return Ok("Record Deleted");
            }
            catch (ArgumentException)
            {
                return NotFound("country does not exist");
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong,  message description: " + ex.Message);
                return BadRequest(ex);
            }
        }

      
    }
}