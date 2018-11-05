using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oder.Domain.Items.Exceptions;
using Oder.Services.Items;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Oder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly ILogger<ItemController> _itemLogger;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [Authorize]
        [HttpPost]
        public ActionResult<ItemDTO> CreateNewItem([FromBody] ItemDTO itemDTO)
        {
            try
            {
                return Ok(_itemService.CreateNewItem(itemDTO));
            }
            catch (ItemInputException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
