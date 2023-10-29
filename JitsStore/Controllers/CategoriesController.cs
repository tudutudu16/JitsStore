using Microsoft.AspNetCore.Mvc;

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
            return RedirectToAction("Add");
        }
    }
}
