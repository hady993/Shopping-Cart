using ShoppingCart.DataAccess.Model;
using ShoppingCart.Repository.Infrastructure;
using ShoppingCart.Service.Infrastructure;

namespace ShoppingCart.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateProduct(Product product, IEnumerable<int> categories)
        {
            _unitOfWork.ProductRepository.InsertProduct(product, categories);
            _unitOfWork.Save();
        }

        public void DeleteProduct(Product product)
        {
            _unitOfWork.ProductRepository.DeleteProduct(product);
            _unitOfWork.Save();
        }

        public void EditProduct(Product product, IEnumerable<int> categories)
        {
            _unitOfWork.ProductRepository.UpdateProduct(product, categories);
            _unitOfWork.Save();
        }

        public List<Product> GetAllProducts()
        {
            return _unitOfWork.ProductRepository.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            return _unitOfWork.ProductRepository.GetProductById(id);
        }
    }
}
