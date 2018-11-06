using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oder.Services.Orders;
using Oder.Services.Orders.OrderExceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Oder.Api.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public ActionResult<OrderDTO> MakeNewOrder([FromBody] OrderDTO newOrderDTO)
        {
            try
            {
                var orderCreated = _orderService.CreateNewOrder(newOrderDTO);
                return Created("api/orders/" + orderCreated.Id, orderCreated);
            }
            catch (OrderException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
