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
    public class CustomerController : ControllerBase
    {
        public readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            var customers = await _customerService.GetCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(string id)
        {
            var customer = await _customerService.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Post([FromBody] Customer customer)
        {
            var newCustomer = new Customer();
            try
            {
                newCustomer = await _customerService.PostCustomer(customer);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PostCustomer Error", e.Message);
                return BadRequest(ModelState);
            }
            return Created(new Uri($"{Request.Path}/{newCustomer.Id}", UriKind.Relative), newCustomer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> Put(string id, [FromBody] Customer customer)
        {
            if (id != customer.Id)
            {
                ModelState.AddModelError("PutCustomer Error", "Mismatched Ids");
                return BadRequest(ModelState);
            }

            try
            {
                var obj = await _customerService.GetCustomer(id);
                if (obj == null)
                {
                    ModelState.AddModelError("PutCustomer Error", "Customer does not exist");
                    return BadRequest(ModelState);
                }
                await _customerService.PutCustomer(id, customer);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PutCustomer Error", e.Message);
                return BadRequest(ModelState);
            }
            return Accepted(customer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var obj = await _customerService.GetCustomer(id);
                if (obj == null)
                {
                    ModelState.AddModelError("DeleteCustomer Error", "Customer does not exist");
                    return BadRequest(ModelState);
                }

                await _customerService.DeleteCustomer(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("DeleteCustomer Error", e.Message);
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}