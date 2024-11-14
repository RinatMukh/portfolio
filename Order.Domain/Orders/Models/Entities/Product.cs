namespace Customer.Domain.Orders.Models.Entities
{
    public class Product
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }
        public DateTime CreatedAtUtc { get; set; }

        public DateTime UpdatedAtUtc { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid UpdatedBy { get; set; }

        public Guid? DeletedBy { get; set; }

        public Order Order { get; set; } = null!;
    }
}
