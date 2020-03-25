using Microsoft.EntityFrameworkCore;
using orders.Models.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orders.Models
{
    public class OrdersDbContext: DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options):
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new OrderConfiguration());
        }

        public DbSet<Order> Orders { get; set; }
    }
}
