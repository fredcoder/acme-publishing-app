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
    public class PrintDistCompanyController : ControllerBase
    {
        public readonly IPrintDistCompanyService _printDistCompanyService;

        public PrintDistCompanyController(IPrintDistCompanyService printDistCompanyService)
        {
            _printDistCompanyService = printDistCompanyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrintDistCompany>>> Get()
        {
            var printDistCompanies = await _printDistCompanyService.GetPrintDistCompanies();
            return Ok(printDistCompanies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrintDistCompany>> Get(string id)
        {
            var printDistCompany = await _printDistCompanyService.GetPrintDistCompany(id);
            if (printDistCompany == null)
            {
                return NotFound();
            }
            return Ok(printDistCompany);
        }

        [HttpPost]
        public async Task<ActionResult<PrintDistCompany>> Post([FromBody] PrintDistCompany printDistCompany)
        {
            var newPrintDistCompany = new PrintDistCompany();
            try
            {
                newPrintDistCompany = await _printDistCompanyService.PostPrintDistCompany(printDistCompany);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PostPrintDistCompany Error", e.Message);
                return BadRequest(ModelState);
            }
            return Created(new Uri($"{Request.Path}/{newPrintDistCompany.Id}", UriKind.Relative), newPrintDistCompany);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PrintDistCompany>> Put(string id, [FromBody] PrintDistCompany printDistCompany)
        {
            if (id != printDistCompany.Id)
            {
                ModelState.AddModelError("PutPrintDistCompany Error", "Mismatched Ids");
                return BadRequest(ModelState);
            }

            try
            {
                var obj = await _printDistCompanyService.GetPrintDistCompany(id);
                if (obj == null)
                {
                    ModelState.AddModelError("PutPrintDistCompany Error", "PrintDistCompany does not exist");
                    return BadRequest(ModelState);
                }
                await _printDistCompanyService.PutPrintDistCompany(id, printDistCompany);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PutPrintDistCompany Error", e.Message);
                return BadRequest(ModelState);
            }
            return Accepted(printDistCompany);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var obj = await _printDistCompanyService.GetPrintDistCompany(id);
                if (obj == null)
                {
                    ModelState.AddModelError("DeletePrintDistCompany Error", "PrintDistCompany does not exist");
                    return BadRequest(ModelState);
                }

                await _printDistCompanyService.DeletePrintDistCompany(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("DeletePrintDistCompany Error", e.Message);
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}