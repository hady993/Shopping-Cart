using AutoMapper;
using ShoppingCart.DataAccess.Model;
using ShoppingCart.Web.ViewModels;

namespace ShoppingCart.Web.Helper
{
    public class AutomapperProfile : Profile
    {
        // To map the source to the destination automatically!
        public AutomapperProfile()
        {
            CreateMap<Category, CategoryViewModel>();
        }
    }
}
