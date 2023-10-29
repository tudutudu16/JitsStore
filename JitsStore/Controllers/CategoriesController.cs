using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JitsStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly JITS_STORE jITSSTORE;

        public CategoriesController(JITS_STORE jITSSTORE)
        {
            this.jITSSTORE = jITSSTORE;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categorires = await jITSSTORE.Categories.ToListAsync();
            return View(categorires);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCategoriesViewModel addCategoriesRequest)
        {
            var category = new Category()
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = addCategoriesRequest.CategoryName,
                Description = addCategoriesRequest.Description,
                Products = addCategoriesRequest.Products,
            };

            await jITSSTORE.Categories.AddAsync(category);
            await jITSSTORE.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var cate = await jITSSTORE.Categories.FirstOrDefaultAsync(cate => string.Equals(cate.CategoryId, id));

            if (cate != null)
            {
                var viewModel = new UpdateCategoriesViewModel()
                {
                    CategoryId = cate.CategoryId,
                    CategoryName = cate.CategoryName,
                    Description = cate.Description,
                    Products = cate.Products,
                };

                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateCategoriesViewModel model)
        {
            var cate = await jITSSTORE.Categories.FindAsync(model.CategoryId);

            if (cate != null)
            {
                cate.CategoryName = model.CategoryName;
                cate.Description = model.Description;
                cate.Products = model.Products;

                await jITSSTORE.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateCategoriesViewModel model)
        {
            var cate = await jITSSTORE.Categories.FindAsync(model.CategoryId);

            if (cate != null)
            {
                jITSSTORE.Categories.Remove(cate);
                await jITSSTORE.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
