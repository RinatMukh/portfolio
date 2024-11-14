namespace Customer.Domain.Orders.Models.Requests;

public record OrderRequest(
    ItemRequest<string> Title,
    ItemRequest<string> Description,
    ItemRequest<string> Number);