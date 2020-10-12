using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TruYumAPI.Models;

namespace TruYumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class AdminController : ControllerBase
    {
        IMenuItemOperation operation;
        public AdminController(IMenuItemOperation operation)
        {
            this.operation = operation;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await operation.GetItemsAdminAsync();

            if (items.Count == 0)
                return BadRequest("No items");
            return Ok(items);
        }

        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetAsync(string name)
        {
            var itemList = await operation.SearchItemsAdminAsync(name);

            if (itemList.Count == 0)
                return NotFound("Object cannot be found");

            return Ok(itemList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var item = await operation.GetMenuItemById(id);

            if (item == null)
                return NotFound("No item with given id");

            return Ok(item);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(MenuItem food)
        {
            var item = await operation.UpdateMenuItem(food);

            if (item == null)
                return NotFound("No item found with that id");
           
            return Ok();
        }
    }
}
