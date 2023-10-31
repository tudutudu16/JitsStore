using JitsStore.ViewModel.Suppliers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JitsStore.Controllers
{
    public class SupplierController : Controller
    {
        private readonly JITS_STORE jITSSTORE;

        public SupplierController(JITS_STORE jITSSTORE)
        {
            this.jITSSTORE = jITSSTORE;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var suppliers = await jITSSTORE.Suppliers.ToListAsync();
            return View(suppliers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierAddViewModel addSupplier)
        {
            var suppliers = new Supplier()
            {
                SupplierId = Guid.NewGuid(),
                CompanyName = addSupplier.CompanyName,
                Address = addSupplier.Address,
                City = addSupplier.City,
                Country = addSupplier.Country,
                Phone = addSupplier.Phone,
                Products = addSupplier.Products
            };

            await jITSSTORE.Suppliers.AddAsync(suppliers);
            await jITSSTORE.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var suppliers = await jITSSTORE.Suppliers.FirstOrDefaultAsync(cate => string.Equals(cate.SupplierId, id));

            if (suppliers != null)
            {
                var viewModel = new SupplierUpdateViewModel()
                {
                    SupplierId = suppliers.SupplierId,
                    CompanyName = suppliers.CompanyName,
                    Address = suppliers.Address,
                    City = suppliers.City,
                    Country = suppliers.Country,
                    Phone = suppliers.Phone,
                    Products = suppliers.Products,
                };

                return await Task.Run(() => View("Update", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(SupplierUpdateViewModel model)
        {
            var suppliers = await jITSSTORE.Suppliers.FindAsync(model.SupplierId);

            if (suppliers != null)
            {
                suppliers.CompanyName = model.CompanyName;
                suppliers.Address = model.Address;
                suppliers.Phone = model.Phone;
                suppliers.City = model.City;
                suppliers.CompanyName = model.CompanyName;
                suppliers.Country = model.Country;
                suppliers.Products = model.Products;

                await jITSSTORE.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SupplierUpdateViewModel model)
        {
            var cate = await jITSSTORE.Suppliers.FindAsync(model.SupplierId);

            if (cate != null)
            {
                jITSSTORE.Suppliers.Remove(cate);
                await jITSSTORE.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
