using Customer.Domain.Orders.Models.Entities;
using Customer.Domain.Orders.Models.Queries;

namespace Customer.Domain.Orders.Contracts
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order, CancellationToken token = default);

        Task<Order> UpdateAsync(Order order, CancellationToken token = default);

        Task<List<Order>> GetListAsync(OrderQuery query, CancellationToken token = default);
        
        Task<Order?> GetByIdAsync(Guid id, CancellationToken token = default);
    }
}
