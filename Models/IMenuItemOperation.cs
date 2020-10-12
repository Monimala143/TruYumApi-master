using System.Collections.Generic;
using System.Threading.Tasks;

namespace TruYumAPI.Models
{
    public interface IMenuItemOperation
    {
        Task<List<MenuItem>> GetItemsAdminAsync();
        Task<List<MenuItem>> GetItemsAnonymousAsync();
        Task<List<MenuItem>> GetItemsCustomerAsync();
        Task<List<MenuItem>> SearchItemsAdminAsync(string name);
        Task<MenuItem> GetMenuItemById(int id);
        Task<List<MenuItem>> SearchItemsNonAdminAsync(string name);
        Task<MenuItem> UpdateMenuItem(MenuItem menuItem);
    }
}