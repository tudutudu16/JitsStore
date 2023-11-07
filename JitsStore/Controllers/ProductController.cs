using JitsStore.Services;
using JitsStore.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace JitsStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly JITS_STORE context;
        private readonly ProductServices productServices;
        public ProductController(JITS_STORE jITSSTORE, ProductServices _productServices)
        {
            this.context = jITSSTORE;
            this.productServices = _productServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await productServices.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddViewModel productVM)
        {
            var product = new Product()
            {
                ProductId = Guid.NewGuid(),
                ProductName = productVM.ProductName,
                Price = productVM.Price,
                Unit = productVM.Unit,
                QuantityOfStock = productVM.QuantityOfStock,
                QuantityOfOrder = productVM.QuantityOfOrder,
                Picture = productVM.Picture,
                Status = productVM.Status,
                CategoryId = productVM.CategoryId,
                SupplierId = productVM.SupplierId
            };

            await productServices.AddAsync(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var productVM = await productServices.GetById(id);

            if (productVM.product != null)
            {
                return await Task.Run(() => View("Update", productVM));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(ProductVM productVM)
        {
            productServices.Update(productVM.product);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductVM productVM)
        {
            if (productVM.product != null)
            {
                productServices.Delete(productVM.product.ProductId);
            }

            return RedirectToAction("Index");
        }
    }
}
