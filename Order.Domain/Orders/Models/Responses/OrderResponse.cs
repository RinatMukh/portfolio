namespace Customer.Domain.Orders.Models.Responses;

public record OrderResponse(
    Guid Id,
    string Number,
    string Title,
    string? Description,
    List<Guid> ProductIds);