using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoppingCart.Web.ViewModels.ProductViewModels
{
    public class EditProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public IFormFile ProductImage { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
