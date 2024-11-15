namespace Customer.Domain.Orders.Models.Queries;

public record OrderQuery(Guid UserId)
{
    public ItemQuery<string> Title { get; init; } = null!;

    public ItemQuery<string> Description { get; init; } = null!;
    
    public ItemQuery<string> Number { get; init; } = null!;
}
