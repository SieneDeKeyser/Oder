using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oder.Services.Customers;
using Oder.Domain.Customers.Exceptions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Oder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _customerLogger;
        public CustomerController(ICustomerService customerService, ILogger<CustomerController> customerLogger)
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
                return Ok(_customerService.CreateNewCustomer(customerDTO));
            }
            catch (CustomerInputException ex)
            {
               return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Get() { return Ok(); }
    }
}
