using DeveloperTest.Business;
using DeveloperTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeveloperTest.Controllers
{
    [Produces("application/json")]
    [Route("customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private static readonly string[] CustomerTypes = { "Large", "Small" };

        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet("customerTypes")]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<List<string>> GetCustomerTypes()
        {
            try
            {
                return Ok(CustomerTypes);
            }
            catch (Exception e)
            {
                _logger.LogError($"Unable to get customer types, error: {e.Message}");
                return BadRequest();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<CustomerModel>>> GetCustomersAsync()
        {
            try
            {
                var customers = await _customerService.GetAllCustomers();

                return Ok(customers);
            }
            catch (Exception e)
            {
                _logger.LogError($"Unable to get customers, error: {e.Message}");
                return BadRequest();
            }
        }

        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(CustomerModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<CustomerModel>>> GetCustomerAsync(Guid customerId)
        {
            try
            {
                var customer = await _customerService.GetCustomer(customerId);
                if(customer is null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
            catch (Exception e)
            {
                _logger.LogError($"Unable to get customer, error: {e.Message}");
                return BadRequest();
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(BaseCustomerModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult>AddCustomer(BaseCustomerModel request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var customer = await _customerService.CreateCustomer(request);

            return Created($"customes/{customer.Id}", customer);
        }
    }
}
