using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using acme_publishing_app.Models;
using acme_publishing_app.Services;

namespace acme_publishing_app.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        public readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> Get()
        {
            var countries = await _countryService.GetCountries();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> Get(string id)
        {
            var country = await _countryService.GetCountry(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        [HttpPost]
        public async Task<ActionResult<Country>> Post([FromBody] Country country)
        {
            var newCountry = new Country();
            try
            {
                newCountry = await _countryService.PostCountry(country);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PostCountry Error", e.Message);
                return BadRequest(ModelState);
            }
            return Created(new Uri($"{Request.Path}/{newCountry.Id}", UriKind.Relative), newCountry);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Country>> Put(string id, [FromBody] Country country)
        {
            if (id != country.Id)
            {
                ModelState.AddModelError("PutCountry Error", "Mismatched Ids");
                return BadRequest(ModelState);
            }

            try
            {
                var obj = await _countryService.GetCountry(id);
                if (obj == null)
                {
                    ModelState.AddModelError("PutCountry Error", "Country does not exist");
                    return BadRequest(ModelState);
                }
                await _countryService.PutCountry(id, country);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PutCountry Error", e.Message);
                return BadRequest(ModelState);
            }
            return Accepted(country);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var obj = await _countryService.GetCountry(id);
                if (obj == null)
                {
                    ModelState.AddModelError("DeleteCountry Error", "Country does not exist");
                    return BadRequest(ModelState);
                }

                await _countryService.DeleteCountry(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("DeleteCountry Error", e.Message);
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}