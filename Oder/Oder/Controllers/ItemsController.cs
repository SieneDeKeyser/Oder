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
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly ILogger<ItemsController> _itemLogger;

        public ItemsController(IItemService itemService, ILogger<ItemsController> itemLogger)
        {
            _itemService = itemService;
            _itemLogger = itemLogger;
        }

        [Authorize]
        [HttpPost]
        public ActionResult<ItemDTO> CreateNewItem([FromBody] ItemDTO itemDTO)
        {
            try
            {
                var newItem = _itemService.CreateNewItem(itemDTO);
                return Created("api/items/" + newItem.Id, newItem );
            }
            catch (ItemInputException ex)
            {
                _itemLogger.LogInformation(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<ItemDTO> GetAllItems()
        {
            return Ok(_itemService.GetAllItems());
        }
    }
}
