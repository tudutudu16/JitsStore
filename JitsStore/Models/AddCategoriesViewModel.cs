﻿namespace JitsStore.Models
{
    public class AddCategoriesViewModel
    {
        public string CategoryName { get; set; } = null!;

        public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
