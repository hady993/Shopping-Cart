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
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ICategoryService categoryService, IProductService productService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _categoryService = categoryService;
            _productService = productService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var productList = _productService.GetAllProducts();
            var mappedProducts = _mapper.Map<List<ProductViewModel>>(productList);
            return View(mappedProducts);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var singleProduct = _productService.GetProductById(id);
            var mappedProduct = _mapper.Map<ProductDetailViewModel>(singleProduct);
            return View(mappedProduct);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateProductViewModel vm = new CreateProductViewModel();
            vm.Categories = _categoryService.GetAllCategories().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(CreateProductViewModel vm)
        {
            var selectedCategories = vm.Categories.Where(x => x.Selected).Select(x => x.Value).Select(int.Parse);
            string ImageFile = _webHostEnvironment.UploadFile(vm.ProductImage);
            var product = new ProductPostViewModel
            {
                Name = vm.Name,
                Price = vm.Price,
                Description = vm.Description,
                ProductImage = ImageFile
            };
            var mappedProduct = _mapper.Map<Product>(product);
            _productService.CreateProduct(mappedProduct, selectedCategories);
            return RedirectToAction("Index", "Products");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            EditProductViewModel vm = new EditProductViewModel
            {
                Id = id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
            vm.Categories = _categoryService.GetAllCategories().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = product.Categories.Any(c => c.CategoryId == x.Id)
            }).ToList();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(EditProductViewModel vm)
        {
            // If the edit failed!
            if (!ModelState.IsValid)
            {
                // If model validation fails, return to the edit view with validation errors
                vm.Categories = _categoryService.GetAllCategories().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = vm.Categories.Any(c => c.Value == x.Id.ToString() && c.Selected)
                }).ToList();

                return View(vm);
            }

            // If the edit succeeded!
            string oldImgPath = _productService.GetProductById(vm.Id).ProductImage;
            string newImageFile = _webHostEnvironment.UpdateFile(oldImgPath, vm.ProductImage);

            var selectedCategories = vm.Categories.Where(x => x.Selected).Select(x => x.Value).Select(int.Parse);
            var product = new EditProductPostViewModel
            {
                Id = vm.Id,
                Name = vm.Name,
                Price = vm.Price,
                Description = vm.Description,
                ProductImage = newImageFile
            };
            var updatedProduct = _mapper.Map<Product>(product);

            updatedProduct.ProductImage = newImageFile;
            _productService.EditProduct(updatedProduct, selectedCategories);

            return RedirectToAction("Index", "Products");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);
            var mappedProduct = _mapper.Map<DeleteProductViewModel>(product);
            return View(mappedProduct);
        }

        [HttpPost]
        public IActionResult Delete(DeleteProductViewModel vm)
        {
            var product = _productService.GetProductById(vm.Id);

            // To delete product's image!
            _webHostEnvironment.DeleteFile(product.ProductImage);

            // To delete the product from db!
            _productService.DeleteProduct(product);

            return RedirectToAction("Index", "Products");
        }
    }
}
