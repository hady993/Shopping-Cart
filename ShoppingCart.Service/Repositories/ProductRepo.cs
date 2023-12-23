using Microsoft.EntityFrameworkCore;
using ShoppingCart.DataAccess.Model;
using ShoppingCart.Service.Data;
using ShoppingCart.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Service.Repositories
{
    public class ProductRepo : IProduct
    {
        private readonly ApplicationDbContext _context;

        public ProductRepo(ApplicationDbContext context)
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

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            //_context.Products.Update(product);
        }
    }
}
