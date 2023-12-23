using ShoppingCart.Web.ViewModels.CategoryViewModels;

namespace ShoppingCart.Web.ViewModels.ProductViewModels
{
    public class ProductDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public IList<CategoryViewModel> CategoryNames { get; set; }
    }
}
