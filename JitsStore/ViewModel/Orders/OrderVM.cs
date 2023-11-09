using Microsoft.AspNetCore.Mvc.Rendering;

namespace JitsStore.ViewModel
{
    public class OrderVM
    {
        public OrderViewVM OrderViewVM { get; set; }
        public Order Order { get; set; }
        public OrderDetail OrderDetail { get; set; }
        public List<SelectListItem> CustomerDDL { get; set; }
        public List<SelectListItem> EmployeeDDL { get; set; }
        public List<SelectListItem> ShipperDDL { get; set; }
        public List<SelectListItem> ProductDDL { get; set; }
    }
}
