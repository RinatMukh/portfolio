using Customer.Domain.Orders.Contracts;
using Customer.Domain.Orders.Mappings;
using Customer.Domain.Orders.Models.Commands;
using Customer.Domain.Orders.Models.Entities;
using Customer.Domain.Orders.Models.Queries;
using Customer.Domain.Orders.Models.Results;

namespace Customer.Application.Orders.Services
{
    public sealed class OrderService(IOrderRepository repository) : IOrderService
    {
        public async Task<OrderResult> CreateAsync(OrderCreateCommand command, CancellationToken token = default)
        {
            var order = command.ToEntity();
            var currentDate = DateTime.UtcNow;
            
            order.Id = Ulid.NewUlid().ToGuid();
            order.CreatedAtUtc = currentDate;
            order.UpdatedAtUtc = currentDate;

            foreach(var relation in order.Products)
            {
                relation.CreatedAtUtc = currentDate;
                relation.UpdatedAtUtc = currentDate;
                relation.CreatedBy = command.UserId;
                relation.UpdatedBy = command.UserId;
            }

            await repository.AddAsync(order, token);

            var result = order.ToResult();

            return result;
        }

        public async Task DeleteAsync(OrderDeleteCommand command, CancellationToken token = default)
        {
            var order = await repository.GetByIdAsync(command.Id, token);
            
            if(order == null)
                throw new ArgumentException("Order not found");

            order.UpdatedAtUtc = DateTime.UtcNow;
            order.UpdatedBy = command.DeletedBy;
            order.DeletedAtUtc = DateTime.UtcNow;
            order.DeletedBy = command.DeletedBy;

            foreach (var relation in order.Products)
            {
                relation.DeletedBy = command.DeletedBy;
                relation.DeletedAtUtc = DateTime.UtcNow;
            }
            
            await repository.UpdateAsync(order, token);
        }

        public async Task<List<OrderResult>> GetListAsync(OrderQuery query, CancellationToken token = default)
        {
            var orders = await repository.GetListAsync(query, token);

            return orders.ToResults();
        }

        public async Task<OrderResult> UpdateAsync(OrderUpdateCommand command, CancellationToken token = default)
        {
            var order = await repository.GetByIdAsync(command.Id, token);
            
            if(order == null)
                throw new ArgumentException("Order not found");
            
            order.UpdatedAtUtc = DateTime.UtcNow;
            order.UpdatedBy = command.UserId;
            order.Description = command.Description.ShouldBeChanged ? command.Description.Value : order.Description;

            if (command.ProductIds.ShouldBeChanged)
                UpdateProduct(command, order);
            
            await repository.UpdateAsync(order, token);
            
            return order.ToResult();
        }

        private void UpdateProduct(OrderUpdateCommand command, Order order)
        {
            foreach (var product in order.Products)
            {
                if(product.DeletedBy is not null)
                    continue;
                
                var inputProduct = command.ProductIds.Value.FirstOrDefault(p => p == product.ProductId);

                if (inputProduct != default)
                    continue;
                        
                product.DeletedBy = command.UserId;
                product.DeletedAtUtc = DateTime.UtcNow;
            }

            foreach (var id in command.ProductIds.Value)
            {
                var existingProduct = order.Products.FirstOrDefault(p => p.ProductId == id);
                    
                if(existingProduct is not null)
                    continue;
                    
                order.Products.Add(new Product
                {
                    ProductId = id,
                    CreatedBy = command.UserId,
                    CreatedAtUtc = DateTime.UtcNow,
                    UpdatedBy = command.UserId,
                    UpdatedAtUtc = DateTime.UtcNow
                });
            }        
        }
    }
}
