using Customer.Domain.Orders.Contracts;
using Customer.Domain.Orders.Models.Entities;
using Customer.Domain.Orders.Models.Queries;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure.Orders.Repositories
{
    public class OrderRepository(CustomerContext context) : IOrderRepository
    {
        public async Task<Order> AddAsync(Order order, CancellationToken token = default)
        {
            await context.Orders.AddAsync(order, token);
            await context.SaveChangesAsync(token);
            return order;
        }
        public async Task<Order> UpdateAsync(Order order, CancellationToken token = default)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync(token);
            return order;
        }

        public async Task<List<Order>> GetListAsync(OrderQuery query, CancellationToken token = default)
        {
            var orders = context.Orders.Where(o => o.DeletedAtUtc != null);
            
            if(query.Title.ShouldApply)
                orders = orders.Where(o => o.Title == query.Title.Value);
            if(query.Description.ShouldApply)
                orders = orders.Where(o => o.Description == query.Description.Value);
            if(query.Number.ShouldApply)
                orders = orders.Where(o => o.Number == query.Number.Value);

            orders = orders.Where(o => o.CreatedBy == query.UserId);
            
            return await orders.ToListAsync(token);
        }

        public Task<Order?> GetByIdAsync(Guid id, CancellationToken token = default) =>
            context.Orders.FirstOrDefaultAsync(order => order.Id == id, token);
    }
}
