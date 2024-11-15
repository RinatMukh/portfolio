namespace Customer.Domain.Orders.Models.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public string Number { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime UpdatedAtUtc { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid UpdatedBy { get; set; }

        public Guid DeletedBy { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
