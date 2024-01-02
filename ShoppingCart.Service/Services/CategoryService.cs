using ShoppingCart.DataAccess.Model;
using ShoppingCart.Repository.Infrastructure;
using ShoppingCart.Service.Infrastructure;

namespace ShoppingCart.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateCategory(Category category)
        {
            _unitOfWork.CategoryRepository.InsertCategory(category);
            _unitOfWork.Save();
        }

        public void DeleteCategory(Category category)
        {
            _unitOfWork.CategoryRepository.DeleteCategory(category);
            _unitOfWork.Save();
        }

        public void EditCategory(Category category)
        {
            _unitOfWork.CategoryRepository.UpdateCategory(category);
            _unitOfWork.Save();
        }

        public List<Category> GetAllCategories()
        {
            return _unitOfWork.CategoryRepository.GetAllCategories();
        }

        public Category GetCategoryById(int id)
        {
            return _unitOfWork.CategoryRepository.GetCategoryById(id);
        }
    }
}
