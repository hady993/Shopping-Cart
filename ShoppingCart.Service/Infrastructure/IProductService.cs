using ShoppingCart.DataAccess.Model;

namespace ShoppingCart.Service.Infrastructure
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void CreateProduct(Product product, IEnumerable<int> categories);
        void EditProduct(Product product, IEnumerable<int> categories);
        void DeleteProduct(Product product);
    }
}
