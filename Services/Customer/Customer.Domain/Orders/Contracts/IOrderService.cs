using Customer.Domain.Orders.Models.Commands;
using Customer.Domain.Orders.Models.Queries;
using Customer.Domain.Orders.Models.Results;

namespace Customer.Domain.Orders.Contracts
{
    public interface IOrderService
    {
        Task<OrderResult> CreateAsync(OrderCreateCommand command, CancellationToken token = default);

        Task<OrderResult> UpdateAsync(OrderUpdateCommand command, CancellationToken token = default);

        Task DeleteAsync(OrderDeleteCommand command, CancellationToken token = default);

        Task<List<OrderResult>> GetListAsync(OrderQuery query, CancellationToken token = default);
    }
}
