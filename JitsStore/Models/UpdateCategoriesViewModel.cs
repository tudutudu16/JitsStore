﻿namespace JitsStore.Models
{
	public class UpdateCategoriesViewModel
	{
		public Guid CategoryId { get; set; }

		public string CategoryName { get; set; } = null!;

		public string? Description { get; set; }

		public virtual ICollection<Product> Products { get; set; } = new List<Product>();
	}
}