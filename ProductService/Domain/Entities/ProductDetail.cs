using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ProductDetail
{
    public int ProductDetailId { get; set; }

    public string ProductId { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Specifications { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public virtual Product Product { get; set; } = null!;
}
