using ShoppingCart.DataAccess.Model;

namespace ShoppingCart.Service.Infrastructure
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
        void CreateCategory(Category category);
        void EditCategory(Category category);
        void DeleteCategory(Category category);
    }
}
