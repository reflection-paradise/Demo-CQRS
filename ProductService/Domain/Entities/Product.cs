using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public int CategoryId { get; set; }

    public decimal Price { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? BusinessId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();
}
