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
    public class SubscriptionController : ControllerBase
    {
        public readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscription>>> Get()
        {
            var subscriptions = await _subscriptionService.GetSubscriptions();
            return Ok(subscriptions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subscription>> Get(string id)
        {
            var subscription = await _subscriptionService.GetSubscription(id);
            if (subscription == null)
            {
                return NotFound();
            }
            return Ok(subscription);
        }

        [HttpPost]
        public async Task<ActionResult<Subscription>> Post([FromBody] Subscription subscription)
        {
            var newSubscription = new Subscription();
            try
            {
                newSubscription = await _subscriptionService.PostSubscription(subscription);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PostSubscription Error", e.Message);
                return BadRequest(ModelState);
            }
            return Created(new Uri($"{Request.Path}/{newSubscription.Id}", UriKind.Relative), newSubscription);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Subscription>> Put(string id, [FromBody] Subscription subscription)
        {
            if (id != subscription.Id)
            {
                ModelState.AddModelError("PutSubscription Error", "Mismatched Ids");
                return BadRequest(ModelState);
            }

            try
            {
                var obj = await _subscriptionService.GetSubscription(id);
                if (obj == null)
                {
                    ModelState.AddModelError("PutSubscription Error", "Subscription does not exist");
                    return BadRequest(ModelState);
                }
                await _subscriptionService.PutSubscription(id, subscription);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PutSubscription Error", e.Message);
                return BadRequest(ModelState);
            }
            return Accepted(subscription);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var obj = await _subscriptionService.GetSubscription(id);
                if (obj == null)
                {
                    ModelState.AddModelError("DeleteSubscription Error", "Subscription does not exist");
                    return BadRequest(ModelState);
                }

                await _subscriptionService.DeleteSubscription(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("DeleteSubscription Error", e.Message);
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}