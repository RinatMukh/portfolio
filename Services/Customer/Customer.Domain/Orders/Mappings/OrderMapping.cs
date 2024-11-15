using Customer.Domain.Orders.Models.Commands;
using Customer.Domain.Orders.Models.Entities;
using Customer.Domain.Orders.Models.Queries;
using Customer.Domain.Orders.Models.Requests;
using Customer.Domain.Orders.Models.Responses;
using Customer.Domain.Orders.Models.Results;

namespace Customer.Domain.Orders.Mappings
{
    public static class OrderMapping
    {
        public static OrderCreateCommand ToCommand(this OrderPostRequest order, Guid userId) =>
            new(userId)
            {
                Number = order.Number,
                Title = order.Title,
                Description = order.Description,
                ProductIds = order.ProductIds,
            };

        public static OrderUpdateCommand ToCommand(this OrderPatchRequest order, Guid userId) =>
            new (order.Id, order.Description.ToCommand(), order.ProductIds.ToCommand(), userId);

        public static OrderQuery ToQuery(this OrderRequest request, Guid userId) =>
            new(userId)
            {
                Title = request.Title.ToQuery(),
                Number = request.Number.ToQuery(),
                Description = request.Description.ToQuery()
            };
        
        public static Order ToEntity(this OrderCreateCommand command) =>
            new ()
            {
                Title = command.Title,
                Number = command.Number,
                Description = command.Description,
                CreatedBy = command.UserId,
                Products = command.ProductIds.Select(id => new Product
                {
                    ProductId = id
                }).ToList()
            };

        public static OrderResult ToResult(this Order order) =>
            new ()
            {
                Id = order.Id,
                Description = order.Description,
                Title = order.Title,
                ProductIds = order.Products.Select(p => p.ProductId).ToList()
            };

        public static List<OrderResult> ToResults(this List<Order> orders) =>
            orders.Select(order => order.ToResult()).ToList();
        
        public static OrderResponse ToResponse(this OrderResult order) =>
            new (order.Id, order.Number, order.Title, order.Description, order.ProductIds.Select(p => p).ToList());

        public static OrderResponse[] ToResponse(this List<OrderResult> results) =>
            results.Select(r => r.ToResponse()).ToArray();
    }
}
