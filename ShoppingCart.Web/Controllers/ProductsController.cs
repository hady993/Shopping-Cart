using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingCart.DataAccess.Model;
using ShoppingCart.Service.Infrastructure;
using ShoppingCart.Web.Helper;
using ShoppingCart.Web.ViewModels.ProductViewModels;

namespace ShoppingCart.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProduct _product;
        private IMapper _mapper;
        private readonly ICategory _category;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IProduct product, IMapper mapper, ICategory category, IWebHostEnvironment webHostEnvironment)
        {
            _product = product;
            _mapper = mapper;
            _category = category;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var productList = _product.GetAllProducts();
            var mappedProducts = _mapper.Map<List<ProductViewModel>>(productList);
            return View(mappedProducts);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var singleProduct = _product.GetProductById(id);
            var mappedProduct = _mapper.Map<ProductDetailViewModel>(singleProduct);
            return View(mappedProduct);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateProductViewModel vm = new CreateProductViewModel();
            vm.Categories = _category.GetAllCategories().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(CreateProductViewModel vm)
        {
            FileUpload fileUpload = new FileUpload(_webHostEnvironment);
            var selectedCategories = vm.Categories.Where(x => x.Selected).Select(x => x.Value).Select(int.Parse);
            string ImageFile = fileUpload.UploadFile(vm.ProductImage);
            var product = new ProductPostViewModel
            {
                Name = vm.Name,
                Price = vm.Price,
                Description = vm.Description,
                ProductImage = ImageFile
            };
            var mappedProduct = _mapper.Map<Product>(product);
            _product.InsertProduct(mappedProduct, selectedCategories);
            _product.Save();
            return RedirectToAction("Index");
        }
    }
}
