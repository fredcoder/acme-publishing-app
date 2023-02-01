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
    public class DeliveryAddressController : ControllerBase
    {
        public readonly IDeliveryAddressService _deliveryAddressService;

        public DeliveryAddressController(IDeliveryAddressService deliveryAddressService)
        {
            _deliveryAddressService = deliveryAddressService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryAddress>>> Get()
        {
            var deliveryAddresses = await _deliveryAddressService.GetDeliveryAddresses();
            return Ok(deliveryAddresses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryAddress>> Get(string id)
        {
            var deliveryAddress = await _deliveryAddressService.GetDeliveryAddress(id);
            if (deliveryAddress == null)
            {
                return NotFound();
            }
            return Ok(deliveryAddress);
        }

        [HttpPost]
        public async Task<ActionResult<DeliveryAddress>> Post([FromBody] DeliveryAddress deliveryAddress)
        {
            var newDeliveryAddress = new DeliveryAddress();
            try
            {
                newDeliveryAddress = await _deliveryAddressService.PostDeliveryAddress(deliveryAddress);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PostDeliveryAddress Error", e.Message);
                return BadRequest(ModelState);
            }
            return Created(new Uri($"{Request.Path}/{newDeliveryAddress.Id}", UriKind.Relative), newDeliveryAddress);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DeliveryAddress>> Put(string id, [FromBody] DeliveryAddress deliveryAddress)
        {
            if (id != deliveryAddress.Id)
            {
                ModelState.AddModelError("PutDeliveryAddress Error", "Mismatched Ids");
                return BadRequest(ModelState);
            }

            try
            {
                var obj = await _deliveryAddressService.GetDeliveryAddress(id);
                if (obj == null)
                {
                    ModelState.AddModelError("PutDeliveryAddress Error", "DeliveryAddress does not exist");
                    return BadRequest(ModelState);
                }
                await _deliveryAddressService.PutDeliveryAddress(id, deliveryAddress);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PutDeliveryAddress Error", e.Message);
                return BadRequest(ModelState);
            }
            return Accepted(deliveryAddress);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var obj = await _deliveryAddressService.GetDeliveryAddress(id);
                if (obj == null)
                {
                    ModelState.AddModelError("DeleteDeliveryAddress Error", "DeliveryAddress does not exist");
                    return BadRequest(ModelState);
                }

                await _deliveryAddressService.DeleteDeliveryAddress(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("DeleteDeliveryAddress Error", e.Message);
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}