using JitsStore.Services;
using JitsStore.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace JitsStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly JITS_STORE _context;
        private readonly OrderServices _orderServices;
        public OrderController(JITS_STORE jITSSTORE, OrderServices orderServices)
        {
            this._context = jITSSTORE;
            this._orderServices = orderServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orderVM = await _orderServices.GetAll();
            return View(orderVM);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            OrderVM orderVM = await _orderServices.GetDLL();
            return await Task.Run(() => View("Create", orderVM));
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderVM orderVM)
        {
            await _orderServices.AddAsync(orderVM.Order);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(string id)
        {
            var orderVM = await _orderServices.GetData(id);

            if (orderVM.Order != null)
            {
                return await Task.Run(() => View("Update", orderVM));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(OrderVM orderVM)
        {
            if (orderVM.Order != null)
            {
                _orderServices.Update(orderVM.Order, orderVM.OrderDetail);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(OrderVM orderVM)
        {
            if (orderVM.Order != null)
            {
                _orderServices.Delete(orderVM.Order.OrderId);
            }

            return RedirectToAction("Index");
        }
    }
}
