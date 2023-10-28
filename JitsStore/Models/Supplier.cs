﻿using System;
using System.Collections.Generic;

namespace JitsStore.Models;

public partial class Supplier
{
    public string SupplierId { get; set; } = null!;

    public string? CompanyName { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
