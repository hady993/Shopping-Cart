using ShoppingCart.DataAccess.Model;

namespace ShoppingCart.Service.Infrastructure
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void InsertProduct(Product product, IEnumerable<int> categories);
        void UpdateProduct(Product product, IEnumerable<int> categories);
        void DeleteProduct(Product product);
    }
}
