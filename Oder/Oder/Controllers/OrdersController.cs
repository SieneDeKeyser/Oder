using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oder.Domain.Orders.OrderExceptions;
using Oder.Services.Orders;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Oder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _orderLogger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> orderLogger)
        {
            _orderService = orderService;
            _orderLogger = orderLogger;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<OrderDTO>> GetAllOrders()
        {
           return Ok(_orderService.GetAllOrders());
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
                _orderLogger.LogInformation(ex.Message);
                return BadRequest(ex.Message);
            }
        }


    }
}
