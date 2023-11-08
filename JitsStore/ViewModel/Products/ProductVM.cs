using Microsoft.AspNetCore.Mvc.Rendering;

namespace JitsStore.ViewModel
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public List<SelectListItem> CategoriesSelectList { get; set; }
        public List<SelectListItem> SuppliersSelectList { get; set; }
    }
}
