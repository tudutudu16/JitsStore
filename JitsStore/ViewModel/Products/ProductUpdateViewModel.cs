namespace JitsStore.ViewModel
{
    public class ProductAddViewModel
    {
        public Guid ProductId { get; set; }

        public string? ProductName { get; set; }

        public decimal? Price { get; set; }

        public int? Unit { get; set; }

        public int? QuantityOfStock { get; set; }

        public string? QuantityOfOrder { get; set; }

        public byte[]? Picture { get; set; }

        public byte? Status { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SupplierId { get; set; }

        public virtual Category? Category { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public virtual Supplier? Supplier { get; set; }
    }
}
