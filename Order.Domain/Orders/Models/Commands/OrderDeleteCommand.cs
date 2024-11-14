namespace Customer.Domain.Orders.Models.Commands
{
    public record OrderDeleteCommand(Guid Id, Guid DeletedBy);
}
