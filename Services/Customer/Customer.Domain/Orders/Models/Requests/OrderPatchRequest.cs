namespace Customer.Domain.Orders.Models.Requests;

public record OrderPatchRequest
{
    public Guid Id { get; init; }
    
    public ItemRequest<string> Description { get; init; } = null!;

    public ItemRequest<Guid[]> ProductIds { get; init; } = null!;
}