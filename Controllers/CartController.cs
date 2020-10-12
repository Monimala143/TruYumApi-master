using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruYumAPI.Models;

namespace TruYumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "customer")]
    public class CartController : ControllerBase
    {
        private readonly TruyumDBContext _context;

        public CartController(TruyumDBContext foodDb)
        {
            this._context = foodDb;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var items = from i in _context.Carts
                        select i;

            var menues = (from m in _context.FoodItems
                          select m).ToList();

            await items.ForEachAsync(i =>
            {
                menues.ForEach(m =>
                {
                    if (m.ItemId == i.ItemId)
                        i.FoodItem = m;
                });
            });


            var cartList = await items.ToListAsync();

            if (cartList.Count == 0)
                return BadRequest("There is no fooditems");

            return Ok(cartList);

        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAsync(int userId)
        {
            var items = from i in _context.Carts
                        where i.UserId == userId
                        select i;

            var menues = (from m in _context.FoodItems
                          select m).ToList();

            await items.ForEachAsync(i =>
            {
                menues.ForEach(m =>
                {
                    if (m.ItemId == i.ItemId)
                        i.FoodItem = m;
                });
            });


            var cartList = await items.ToListAsync();

            if (cartList.Count == 0)
                return BadRequest("There is no fooditems");

            return Ok(cartList);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> PostAsync([FromBody] int itemId, [FromRoute] int userId)
        {
            var cartItem = new Cart();
            cartItem.ItemId = itemId;
            cartItem.UserId = userId;
            await _context.Carts.AddAsync(cartItem);
            int rows = await _context.SaveChangesAsync();
            if (rows == 0)
                return BadRequest("item not added");
            return StatusCode(201);
        }

        [HttpDelete("{cartId}")]
        public async Task<IActionResult> DeleteAsync(int cartId)
        {

            Cart cartItem = await _context.Carts.FindAsync(cartId);
            if (cartItem == null)
                return BadRequest("item not deleted");
            _context.Carts.Remove(cartItem);
            int rows = await _context.SaveChangesAsync();
            if (rows == 0)
                return BadRequest("item not deleted");
            return NoContent();
        }
    }
}

