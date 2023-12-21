using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Service.Infrastructure;
using ShoppingCart.Web.ViewModels;

namespace ShoppingCart.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategory _category;
        private IMapper _mapper;

        public CategoriesController(ICategory category, IMapper mapper)
        {
            _category = category;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var allCategories = _category.GetAllCategories();
            var mappedCategories = _mapper.Map<List<CategoryViewModel>>(allCategories);
            return View(mappedCategories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
    }
}
