using ShoppingCart.DataAccess.Model;

namespace ShoppingCart.Service.Infrastructure
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
        void InsertCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
