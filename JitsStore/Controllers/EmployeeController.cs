using JitsStore.Models;
using JitsStore.ViewModel.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace JitsStore.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly JITS_STORE jITSSTORE;

        public EmployeeController(JITS_STORE jITSSTORE)
        {
            this.jITSSTORE = jITSSTORE;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categorires = await jITSSTORE.Employee.ToListAsync();
            return View(categorires);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeAddViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Address = addEmployeeRequest.Address,
                BirthDate = addEmployeeRequest.BirthDate,
                ContactType = addEmployeeRequest.ContactType,
                Email = addEmployeeRequest.Email,
                Orders = addEmployeeRequest.Orders,
                Phone = addEmployeeRequest.Phone,
                Photo = addEmployeeRequest.Photo,
                Salary = addEmployeeRequest.Salary,
                Status = addEmployeeRequest.Status,
            };

            await jITSSTORE.Employee.AddAsync(employee);
            await jITSSTORE.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var employee = await jITSSTORE.Employee.FirstOrDefaultAsync(cate => string.Equals(cate.EmployeeId, id));

            if (employee != null)
            {
                var viewModel = new EmployeeUpdateViewModel()
                {
                    EmployeeId = employee.EmployeeId,
                    Name = employee.Name,
                    Address = employee.Address,
                    BirthDate = employee.BirthDate,
                    ContactType = employee.ContactType,
                    Email = employee.Email,
                    Orders = employee.Orders,
                    Phone = employee.Phone,
                    Photo = employee.Photo,
                    Salary = employee.Salary,
                    Status = employee.Status,
                };

                return await Task.Run(() => View("Update", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeUpdateViewModel model)
        {
            var employee = await jITSSTORE.Employee.FindAsync(model.EmployeeId);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Address = model.Address;
                employee.BirthDate = model.BirthDate;
                employee.ContactType = model.ContactType;
                employee.Email = model.Email;
                employee.Orders = model.Orders;
                employee.Phone = model.Phone;
                employee.Photo = model.Photo;
                employee.Salary = model.Salary;
                employee.Status = model.Status;

                await jITSSTORE.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeUpdateViewModel model)
        {
            var cate = await jITSSTORE.Employee.FindAsync(model.EmployeeId);

            if (cate != null)
            {
                jITSSTORE.Employee.Remove(cate);
                await jITSSTORE.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
