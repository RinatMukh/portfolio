namespace Customer.Domain.Orders.Models.Commands
{
    public record OrderUpdateCommand(
        Guid Id,
        UpdateItemCommand<string> Description,
        UpdateItemCommand<Guid[]> ProductIds,
        Guid UserId);
}
