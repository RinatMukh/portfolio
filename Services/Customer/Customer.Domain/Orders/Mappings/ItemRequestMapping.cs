using Customer.Domain.Orders.Models.Commands;
using Customer.Domain.Orders.Models.Queries;
using Customer.Domain.Orders.Models.Requests;

namespace Customer.Domain.Orders.Mappings;

public static class ItemRequestMapping
{
    public static ItemQuery<T> ToQuery<T>(this ItemRequest<T> request) =>
        new (request.Value, request.ShouldBeIncluded);
    
    public static UpdateItemCommand<T> ToCommand<T>(this ItemRequest<T> request) =>
        new (request.Value, request.ShouldBeIncluded);
}