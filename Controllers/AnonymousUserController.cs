using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TruYumAPI.Models;

namespace TruYumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AnonymousUserController : ControllerBase
    {
        IMenuItemOperation operation;
        public AnonymousUserController(IMenuItemOperation operation)
        {
            this.operation = operation;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await operation.GetItemsAnonymousAsync();

            if (items.Count == 0)
                return BadRequest("No items");
            return Ok(items);
        }

        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetAsync(string name)
        {
            var itemList = await operation.SearchItemsNonAdminAsync(name);

            if (itemList.Count == 0)
                return NotFound("Object cannot be found");

            return Ok(itemList);
        }

    }
}
