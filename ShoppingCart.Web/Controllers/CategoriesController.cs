using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DataAccess.Model;
using ShoppingCart.Service.Infrastructure;
using ShoppingCart.Web.ViewModels.CategoryViewModels;

namespace ShoppingCart.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var allCategories = _unitOfWork.CategoryRepository.GetAllCategories();
            var mappedCategories = _mapper.Map<List<CategoryViewModel>>(allCategories);
            return View(mappedCategories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetCategoryById(id);
            var mappedCategory = _mapper.Map<EditCategoryViewModel>(category);
            return View(mappedCategory);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetCategoryById(id);
            var mappedCategory = _mapper.Map<DetailCategoryViewModel>(category);
            return View(mappedCategory);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetCategoryById(id);
            var mappedCategory = _mapper.Map<DeleteCategoryViewModel>(category);
            return View(mappedCategory);
        }

        [HttpPost]
        public IActionResult Delete(DeleteCategoryViewModel vm)
        {
            var mappedCategoryInModel = _mapper.Map<Category>(vm);
            _unitOfWork.CategoryRepository.DeleteCategory(mappedCategoryInModel);
            _unitOfWork.Save();
            return RedirectToAction("Index", "Categories");
        }

        [HttpPost]
        public IActionResult Edit(EditCategoryViewModel vm)
        {
            var mappedCategoryInModel = _mapper.Map<Category>(vm);
            _unitOfWork.CategoryRepository.UpdateCategory(mappedCategoryInModel);
            _unitOfWork.Save();
            return RedirectToAction("Index", "Categories");
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryViewModel vm)
        {
            var mappedCategoryInModel = _mapper.Map<Category>(vm);
            _unitOfWork.CategoryRepository.InsertCategory(mappedCategoryInModel);
            _unitOfWork.Save();
            return RedirectToAction("Index", "Categories");
        }
    }
}
