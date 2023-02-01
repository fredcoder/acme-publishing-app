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
    public class DeliveryCompanyController : ControllerBase
    {
        public readonly IDeliveryCompanyService _deliveryCompanyService;

        public DeliveryCompanyController(IDeliveryCompanyService deliveryCompanyService)
        {
            _deliveryCompanyService = deliveryCompanyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryCompany>>> Get()
        {
            var deliveryCompanies = await _deliveryCompanyService.GetDeliveryCompanies();
            return Ok(deliveryCompanies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryCompany>> Get(string subscriptionId, string countryId)
        {
            var deliveryCompany = await _deliveryCompanyService.GetDeliveryCompany(subscriptionId, countryId);
            if (deliveryCompany == null)
            {
                return NotFound();
            }
            return Ok(deliveryCompany);
        }

        [HttpPost]
        public async Task<ActionResult<DeliveryCompany>> Post([FromBody] DeliveryCompany deliveryCompany)
        {
            var newDeliveryCompany = new DeliveryCompany();
            try
            {
                newDeliveryCompany = await _deliveryCompanyService.PostDeliveryCompany(deliveryCompany);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PostDeliveryCompany Error", e.Message);
                return BadRequest(ModelState);
            }
            return Created(new Uri($"{Request.Path}/{newDeliveryCompany.SubscriptionId + newDeliveryCompany.CountryId}", UriKind.Relative), newDeliveryCompany);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DeliveryCompany>> Put(string subscriptionId, string countryId, [FromBody] DeliveryCompany deliveryCompany)
        {
            if (subscriptionId + countryId != deliveryCompany.SubscriptionId + deliveryCompany.CountryId)
            {
                ModelState.AddModelError("PutDeliveryCompany Error", "Mismatched Ids");
                return BadRequest(ModelState);
            }

            try
            {
                var obj = await _deliveryCompanyService.GetDeliveryCompany(subscriptionId, countryId);
                if (obj == null)
                {
                    ModelState.AddModelError("PutDeliveryCompany Error", "DeliveryCompany does not exist");
                    return BadRequest(ModelState);
                }
                await _deliveryCompanyService.PutDeliveryCompany(subscriptionId, countryId, deliveryCompany);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PutDeliveryCompany Error", e.Message);
                return BadRequest(ModelState);
            }
            return Accepted(deliveryCompany);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string subscriptionId, string countryId)
        {
            try
            {
                var obj = await _deliveryCompanyService.GetDeliveryCompany(subscriptionId, countryId);
                if (obj == null)
                {
                    ModelState.AddModelError("DeleteDeliveryCompany Error", "DeliveryCompany does not exist");
                    return BadRequest(ModelState);
                }

                await _deliveryCompanyService.DeleteDeliveryCompany(subscriptionId, countryId);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("DeleteDeliveryCompany Error", e.Message);
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}