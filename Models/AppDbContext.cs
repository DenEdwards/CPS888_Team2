using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cafeteria.Models
{
    public class AppDbContext : DbContext { 
        public AppDbContext() : base("CafeteriaConnection")
        {

        }
        //These are then called from a controller: HomeController
        public DbSet<User> Users { get; set; }
        public DbSet<Caterer> Caterers { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
 
}