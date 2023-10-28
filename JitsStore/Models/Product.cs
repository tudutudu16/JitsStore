using System;
using System.Collections.Generic;

namespace JitsStore.Models;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public string? ProductName { get; set; }

    public decimal? Price { get; set; }

    public int? Unit { get; set; }

    public int? QuantityOfStock { get; set; }

    public string? QuantityOfOrder { get; set; }

    public byte[]? Picture { get; set; }

    public byte? Status { get; set; }

    public string? CategoryId { get; set; }

    public string? SupplierId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Supplier? Supplier { get; set; }
}
