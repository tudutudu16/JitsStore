using JitsStore.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JitsStore.Services
{
    public class OrderServices
    {
        private readonly JITS_STORE _context;

        public OrderServices(JITS_STORE context)
        {
            _context = context;
        }
        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<OrderViewVM>> GetAll()
        {
            var result = _context.Orders.Select(x => new OrderViewVM
            {
                OrderId = x.OrderId.ToString(),
                OrderDate = x.OrderDate,
                ShippedDate = x.ShippedDate,
                ShipAddress = x.ShipAddress,
                CustomerName = _context.Customers.Where(x => x.CustomerId.Equals(x.CustomerId)).Select(x => x.CustomerName).FirstOrDefault(),
                CustomerId = x.CustomerId,
                EmployeeName = _context.Employee.Where(x => x.EmployeeId.Equals(x.EmployeeId)).Select(x => x.Name).FirstOrDefault(),
                EmployeeId = x.EmployeeId,
                ShipperName = _context.Shippers.Where(x => x.ShipperId.Equals(x.ShipperId)).Select(x => x.Name).FirstOrDefault(),
                ShipperId = x.ShipperId,
                Note = x.Note,
            });

            return result;
        }

        public async Task<OrderVM> GetData(string id)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.OrderId.Equals(id));
            OrderVM orderVM = new OrderVM();
            orderVM.Order = order;
            orderVM.CustomerDDL = _context.Customers.Select(x => new SelectListItem
            {
                Text = x.CustomerName,
                Value = x.CustomerId.ToString()
            }).ToList();

            orderVM.EmployeeDDL = _context.Employee.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.EmployeeId.ToString()
            }).ToList();

            orderVM.ShipperDDL = _context.Shippers.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ShipperId.ToString()
            }).ToList();

            return orderVM;
        }

        public async Task<OrderVM> GetDLL()
        {
            OrderVM orderVM = new OrderVM();
            orderVM.Order = new Order();
            orderVM.CustomerDDL = _context.Customers.Select(x => new SelectListItem
            {
                Text = x.CustomerName,
                Value = x.CustomerId.ToString()
            }).ToList();

            orderVM.EmployeeDDL = _context.Employee.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.EmployeeId.ToString()
            }).ToList();

            orderVM.ShipperDDL = _context.Shippers.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ShipperId.ToString()
            }).ToList();

            return orderVM;
        }

        public int Update(Order order)
        {
            _context.Orders.Update(order);

            return _context.SaveChanges();
        }
        public int Delete(string orderId)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.OrderId.Equals(orderId));
            if(order != null)
            {
                _context.Orders.Remove(order);
                return _context.SaveChanges();
            }

            return 0;
        }
    }
}
