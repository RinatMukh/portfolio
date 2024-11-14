namespace Customer.Domain.Orders.Models.Requests;

public record ItemRequest<T>(T Value, bool ShouldBeIncluded);