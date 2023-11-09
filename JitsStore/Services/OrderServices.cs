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
            var result = from order in _context.Orders
                         join detail in _context.OrderDetails on order.OrderId equals detail.OrderId into group1
                         from record in group1.DefaultIfEmpty()
                         select new OrderViewVM
                         {
                             OrderId = order.OrderId.ToString(),
                             Productname = record.ProductId != null ? _context.Products.Where(x => x.ProductId.Equals(record.ProductId)).Select(x => x.ProductName).FirstOrDefault() : null,
                             ProductId = record.ProductId != null ? record.ProductId : Guid.Empty,
                             OrderDate = order.OrderDate,
                             ShippedDate = order.ShippedDate,
                             ShipAddress = order.ShipAddress,
                             CustomerName = _context.Customers.Where(x => x.CustomerId.Equals(order.CustomerId)).Select(x => x.CustomerName).FirstOrDefault(),
                             CustomerId = order.CustomerId,
                             EmployeeName = _context.Employee.Where(x => x.EmployeeId.Equals(order.EmployeeId)).Select(x => x.Name).FirstOrDefault(),
                             EmployeeId = order.EmployeeId,
                             ShipperName = _context.Shippers.Where(x => x.ShipperId.Equals(order.ShipperId)).Select(x => x.Name).FirstOrDefault(),
                             ShipperId = order.ShipperId,
                             Note = order.Note,
                         };

            return result.ToList();
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

            orderVM.ProductDDL = _context.Products.Select(x => new SelectListItem
            {
                Text = x.ProductName,
                Value = x.ProductId.ToString()
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

            orderVM.ProductDDL = _context.Products.Select(x => new SelectListItem
            {
                Text = x.ProductName,
                Value = x.ProductId.ToString()
            }).ToList();

            return orderVM;
        }

        public int Update(Order order, OrderDetail detail)
        {
            _context.Orders.Update(order);
            int check = _context.OrderDetails.Where(x => x.ProductId.Equals(detail.ProductId) && x.OrderId.Equals(detail.OrderId)).Count();
            if (check > 0)
            {
                _context.OrderDetails.Update(detail);
            } 
            else
            {
                _context.OrderDetails.Add(detail);
            }    
            return _context.SaveChanges();
        }
        public int Delete(string orderId)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.OrderId.Equals(orderId));
            if (order != null)
            {
                _context.Orders.Remove(order);
                return _context.SaveChanges();
            }

            return 0;
        }
    }
}
