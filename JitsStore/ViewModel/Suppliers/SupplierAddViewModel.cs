using JitsStore.Models;
namespace JitsStore.ViewModel.Suppliers
{
    public class SupplierAddViewModel
    {
        public string? SupplierId { get; set; }

        public string? CompanyName { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? Phone { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
