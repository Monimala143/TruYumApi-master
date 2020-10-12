using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TruYumAPI.Models
{
    public class TruyumDBContext: DbContext
    {
        public DbSet<MenuItem> FoodItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public TruyumDBContext(DbContextOptions options):base(options)
        {
        }
    }
}
