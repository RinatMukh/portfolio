namespace Customer.Domain.Orders.Models.Commands;

public record OrderCreateCommand(Guid UserId)
{
    public string Number { get; init; } = null!;
    public string Title { get; init; } = null!;
    public string? Description { get; init; }

    public Guid[] ProductIds { get; init; } = [];
}
