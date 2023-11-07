using JitsStore.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JitsStore.Services
{
    public class ProductServices
    {
        private readonly JITS_STORE _context;

        public ProductServices(JITS_STORE context)
        {
            _context = context;
        }
        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var result = _context.Products.ToList();

            return result;
        }

        public async Task<ProductVM> GetById(Guid id)
        {
            var result = _context.Products.FirstOrDefault(x => x.ProductId == id);
            ProductVM productVM = new ProductVM();
            productVM.product = result;
            productVM.SuppliersSelectList = _context.Suppliers.Select(x => new SelectListItem
            {
                Text = x.CompanyName,
                Value = x.SupplierId.ToString()
            }).ToList();
            productVM.CategoriesSelectList = _context.Categories.Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.CategoryId.ToString()
            }).ToList();

            return productVM;
        }

        public int Update(Product product)
        {
            _context.Products.Update(product);

            return _context.SaveChanges();
        }
        public int Delete(Guid productId)
        {
            Product product = _context.Products.FirstOrDefault(x => x.ProductId.Equals(productId));
            if(product != null)
            {
                _context.Products.Remove(product);
                return _context.SaveChanges();
            }

            return 0;
        }
    }
}
