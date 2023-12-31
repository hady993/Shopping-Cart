using Microsoft.EntityFrameworkCore;
using ShoppingCart.DataAccess.Model;
using ShoppingCart.Repository.Data;
using ShoppingCart.Repository.Infrastructure;

namespace ShoppingCart.Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Include(x => x.Categories).ThenInclude(y => y.Category).FirstOrDefault(p => p.Id == id);
        }

        public void InsertProduct(Product product, IEnumerable<int> categories)
        {
            foreach (var item in categories)
            {
                product.Categories.Add(new ProductCategory()
                {
                    Product = product,
                    CategoryId = item
                });
            }
            _context.Products.Add(product);
        }

        public void UpdateProduct(Product product, IEnumerable<int> categories)
        {
            var existingProduct = _context.Products.Include(x => x.Categories).FirstOrDefault(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                // Update scalar properties
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.ProductImage = product.ProductImage;

                // Delete old categories!
                existingProduct.Categories.Clear();

                // Add the new categories!
                foreach (var item in categories)
                {
                    existingProduct.Categories.Add(new ProductCategory()
                    {
                        Product = existingProduct,
                        CategoryId = item
                    });
                }
            }
        }
    }
}
