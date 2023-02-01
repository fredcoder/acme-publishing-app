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
    public class DeliveryOrderController : ControllerBase
    {
        public readonly IDeliveryOrderService _deliveryOrderService;
        public readonly IDeliveryCompanyService _deliveryCompanyService;

        public DeliveryOrderController(IDeliveryOrderService deliveryOrderService, IDeliveryCompanyService deliveryCompanyService)
        {
            _deliveryOrderService = deliveryOrderService;
            _deliveryCompanyService = deliveryCompanyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryOrder>>> Get()
        {
            var deliveryOrders = await _deliveryOrderService.GetDeliveryOrders();
            return Ok(deliveryOrders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryOrder>> Get(string id)
        {
            var deliveryOrder = await _deliveryOrderService.GetDeliveryOrder(id);
            if (deliveryOrder == null)
            {
                return NotFound();
            }
            return Ok(deliveryOrder);
        }

        [HttpPost]
        public async Task<ActionResult<DeliveryOrder>> Post([FromBody] DeliveryOrder deliveryOrder)
        {
            var newDeliveryOrder = new DeliveryOrder();
            newDeliveryOrder.PrintDistCompanyId = await _deliveryCompanyService.GetDeliveryCompanyId(deliveryOrder.SubscriptionId, deliveryOrder.DeliveryAddress.CountryId);
            try
            {
                newDeliveryOrder = await _deliveryOrderService.PostDeliveryOrder(deliveryOrder);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PostDeliveryOrder Error", e.Message);
                return BadRequest(ModelState);
            }
            return Created(new Uri($"{Request.Path}/{newDeliveryOrder.Id}", UriKind.Relative), newDeliveryOrder);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DeliveryOrder>> Put(string id, [FromBody] DeliveryOrder deliveryOrder)
        {
            if (id != deliveryOrder.Id)
            {
                ModelState.AddModelError("PutDeliveryOrder Error", "Mismatched Ids");
                return BadRequest(ModelState);
            }

            try
            {
                var obj = await _deliveryOrderService.GetDeliveryOrder(id);
                if (obj == null)
                {
                    ModelState.AddModelError("PutDeliveryOrder Error", "DeliveryOrder does not exist");
                    return BadRequest(ModelState);
                }
                deliveryOrder.PrintDistCompanyId = await _deliveryCompanyService.GetDeliveryCompanyId(deliveryOrder.SubscriptionId, deliveryOrder.DeliveryAddress.CountryId);
                await _deliveryOrderService.PutDeliveryOrder(id, deliveryOrder);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PutDeliveryOrder Error", e.Message);
                return BadRequest(ModelState);
            }
            return Accepted(deliveryOrder);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var obj = await _deliveryOrderService.GetDeliveryOrder(id);
                if (obj == null)
                {
                    ModelState.AddModelError("DeleteDeliveryOrder Error", "DeliveryOrder does not exist");
                    return BadRequest(ModelState);
                }

                await _deliveryOrderService.DeleteDeliveryOrder(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("DeleteDeliveryOrder Error", e.Message);
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}