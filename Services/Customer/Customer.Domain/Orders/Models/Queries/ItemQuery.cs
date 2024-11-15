namespace Customer.Domain.Orders.Models.Queries;

public record ItemQuery<T>(T Value, bool ShouldApply);