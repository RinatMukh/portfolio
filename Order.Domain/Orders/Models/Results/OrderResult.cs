namespace Customer.Domain.Orders.Models.Results
{
    public class OrderResult
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public List<Guid> ProductIds { get; set; } = [];
    }
}
