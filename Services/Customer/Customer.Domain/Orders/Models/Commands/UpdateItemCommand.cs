namespace Customer.Domain.Orders.Models.Commands;

public record UpdateItemCommand<T>(T Value, bool ShouldBeChanged);