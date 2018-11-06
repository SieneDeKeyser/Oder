using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oder.Services.Customers;
using Oder.Domain.Customers.Exceptions;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Oder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomersController> _customerLogger;
        public CustomersController(ICustomerService customerService, ILogger<CustomersController> customerLogger)
        {
            _customerService = customerService;
            _customerLogger = customerLogger;
        }

        [HttpPost]
        public ActionResult<CustomerDTO> CreateNewCustomer([FromBody] CustomerDTO customerDTO)
        {
            _customerLogger.LogInformation("hoi");
            try
            {
                return Created("api/customers", _customerService.CreateNewCustomer(customerDTO));
            }
            catch (CustomerInputException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<CustomerDTO>> GetAllCustomers()
        {
            return Ok(_customerService.GetAllCustomers());
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public ActionResult<CustomerDTO> GetCustomerBasedOnId(int id)
        {
            try
            {
               return Ok(_customerService.GetCustomerById(id));
            }
            catch (CustomerNotFoundException ex)
            {
               return BadRequest(ex.Message);
            }
        }
    }
}
