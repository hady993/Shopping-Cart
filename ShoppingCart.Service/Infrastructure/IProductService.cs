using ShoppingCart.DataAccess.Model;

namespace ShoppingCart.Service.Infrastructure
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void CreateProduct(Product product);
        void EditProduct(Product product);
        void DeleteProduct(Product product);
    }
}
