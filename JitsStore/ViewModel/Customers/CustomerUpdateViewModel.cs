﻿namespace JitsStore.ViewModel.Customers
{
    public class CustomerUpdateViewModel
    {
        public Guid CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public int? Sex { get; set; }

        public int? Age { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
