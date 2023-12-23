using AutoMapper;
using ShoppingCart.DataAccess.Model;
using ShoppingCart.Web.ViewModels.CategoryViewModels;
using ShoppingCart.Web.ViewModels.ProductViewModels;

namespace ShoppingCart.Web.Helper
{
    public class AutomapperProfile : Profile
    {
        // To map the source to the destination automatically!
        public AutomapperProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Category, EditCategoryViewModel>().ReverseMap();
            CreateMap<Category, DetailCategoryViewModel>();
            CreateMap<Category, DeleteCategoryViewModel>().ReverseMap();
            CreateMap<CreateCategoryViewModel, Category>();

            CreateMap<Product, ProductViewModel>();
            CreateMap<Product, ProductDetailViewModel>().ForMember
            (
                dest => dest.CategoryNames,
                opt => opt.MapFrom(src => src.Categories.Select(y => y.Category).ToList())
            );
            CreateMap<ProductPostViewModel, Product>();
        }
    }
}
