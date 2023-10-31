using JitsStore.ViewModel.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JitsStore.Controllers
{
    public class CustomerController : Controller
    {
        private readonly JITS_STORE jITSSTORE;

        public CustomerController(JITS_STORE jITSSTORE)
        {
            this.jITSSTORE = jITSSTORE;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customers = await jITSSTORE.Customers.ToListAsync();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerAddViewModel addCustomer)
        {
            var customers = new Customer()
            {
                CustomerId = Guid.NewGuid(),
                CustomerName = addCustomer.CustomerName,
                Address = addCustomer.Address,
                Age = addCustomer.Age,
                Email = addCustomer.Email,
                Orders = addCustomer.Orders,
                Phone = addCustomer.Phone,
                Sex = addCustomer.Sex
            };

            await jITSSTORE.Customers.AddAsync(customers);
            await jITSSTORE.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var customers = await jITSSTORE.Customers.FirstOrDefaultAsync(cate => string.Equals(cate.CustomerId, id));

            if (customers != null)
            {
                var viewModel = new CustomerUpdateViewModel()
                {
                    CustomerId = customers.CustomerId,
                    CustomerName = customers.CustomerName,
                    Address = customers.Address,
                    Age = customers.Age,
                    Email = customers.Email,
                    Orders = customers.Orders,
                    Sex= customers.Sex,
                    Phone = customers.Phone,
                };

                return await Task.Run(() => View("Update", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(CustomerUpdateViewModel model)
        {
            var customers = await jITSSTORE.Customers.FindAsync(model.CustomerId);

            if (customers != null)
            {
                customers.CustomerName = model.CustomerName; 
                customers.Address = model.Address;
                customers.Age = model.Age;
                customers.Email = model.Email;
                customers.Phone = model.Phone;
                customers.Sex = model.Sex;
                customers.Orders = model.Orders;

                await jITSSTORE.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CustomerUpdateViewModel model)
        {
            var cate = await jITSSTORE.Customers.FindAsync(model.CustomerId);

            if (cate != null)
            {
                jITSSTORE.Customers.Remove(cate);
                await jITSSTORE.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
