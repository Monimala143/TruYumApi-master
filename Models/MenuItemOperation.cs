using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruYumAPI.Models
{
    public class MenuItemOperation : IMenuItemOperation
    {

        private readonly TruyumDBContext _context;

        public MenuItemOperation(TruyumDBContext context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> GetItemsAdminAsync()
        {
            var data = from f in _context.FoodItems
                       select f;
            var foodItems = await data.ToListAsync();
            return foodItems;
        }
        public async Task<List<MenuItem>> GetItemsCustomerAsync()
        {
            var data = from f in _context.FoodItems
                       where f.IsActive && f.LaunchDate <= DateTime.Now
                       select f;
            var foodItems = await data.ToListAsync();
            return foodItems;

        }
        public async Task<List<MenuItem>> GetItemsAnonymousAsync()
        {
            return await GetItemsCustomerAsync();
        }

        public async Task<List<MenuItem>> SearchItemsAdminAsync(string name)
        {
            var items = from f in _context.FoodItems
                        select f;
            var itemList = new List<MenuItem>();
            name = name.ToUpper().Trim();
            await items.ForEachAsync(f =>
            {
                var tempName = f.FoodName.ToUpper();
                if (tempName.StartsWith(name))
                {
                    itemList.Add(f);
                }
            });

            return itemList;
        }

        public async Task<MenuItem> GetMenuItemById(int id)
        {
            return await _context.FoodItems.FindAsync(id);
        }
        public async Task<List<MenuItem>> SearchItemsNonAdminAsync(string name)
        {
            var items = from f in _context.FoodItems
                        where f.IsActive && f.LaunchDate <= DateTime.Now
                        select f;
            var itemList = new List<MenuItem>();
            name = name.ToUpper().Trim();
            await items.ForEachAsync(f =>
            {
                var tempName = f.FoodName.ToUpper();
                if (tempName.StartsWith(name))
                {
                    itemList.Add(f);
                }
            });

            return itemList;
        }

        public async Task<MenuItem> UpdateMenuItem(MenuItem food)
        {
            var item = _context.FoodItems.Find(food.ItemId);
            if (item != null)
            {
                item.FoodName = food.FoodName;
                item.Price = food.Price;
                item.IsActive = food.IsActive;
                item.LaunchDate = food.LaunchDate;
                item.IsFreeDelivery = food.IsFreeDelivery;
                item.ImageURL = food.ImageURL;
            }
            await _context.SaveChangesAsync();

            return item;
        }
    }
}
