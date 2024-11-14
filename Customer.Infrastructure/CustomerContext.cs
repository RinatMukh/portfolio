using Customer.Domain.Orders.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure
{
    public class CustomerContext(DbContextOptions<CustomerContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders { get; set; }
    }
}
