namespace Customer.Domain.Orders.Models.Requests;

public record OrderPostRequest
{
    public string Number { get; init; } = null!;
    
    public string Title { get; init; } = null!;
    
    public string? Description { get; init; }

    public Guid[] ProductIds { get; init; } = [];
}