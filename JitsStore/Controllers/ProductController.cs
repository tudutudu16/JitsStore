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
        public async Task<IActionResult> Add()
        {
            ProductVM productVM = await productServices.GetDDL();

            return await Task.Run(() => View("Create", productVM));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductVM productVM)
        {
            await productServices.AddAsync(productVM.Product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var productVM = await productServices.GetById(id);

            if (productVM.Product != null)
            {
                return await Task.Run(() => View("Update", productVM));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(ProductVM productVM)
        {
            productServices.Update(productVM.Product);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductVM productVM)
        {
            if (productVM.Product != null)
            {
                productServices.Delete(productVM.Product.ProductId);
            }

            return RedirectToAction("Index");
        }
    }
}
