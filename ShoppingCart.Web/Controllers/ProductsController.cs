using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingCart.DataAccess.Model;
using ShoppingCart.Repository.Infrastructure;
using ShoppingCart.Web.Helper;
using ShoppingCart.Web.ViewModels.ProductViewModels;

namespace ShoppingCart.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var productList = _unitOfWork.ProductRepository.GetAllProducts();
            var mappedProducts = _mapper.Map<List<ProductViewModel>>(productList);
            return View(mappedProducts);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var singleProduct = _unitOfWork.ProductRepository.GetProductById(id);
            var mappedProduct = _mapper.Map<ProductDetailViewModel>(singleProduct);
            return View(mappedProduct);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateProductViewModel vm = new CreateProductViewModel();
            vm.Categories = _unitOfWork.CategoryRepository.GetAllCategories().Select(x => new SelectListItem()
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
            _unitOfWork.ProductRepository.InsertProduct(mappedProduct, selectedCategories);
            _unitOfWork.Save();
            return RedirectToAction("Index", "Products");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _unitOfWork.ProductRepository.GetProductById(id);
            FileUpload fileUpload = new FileUpload(_webHostEnvironment);
            EditProductViewModel vm = new EditProductViewModel
            {
                Id = id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
            vm.Categories = _unitOfWork.CategoryRepository.GetAllCategories().Select(x => new SelectListItem
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
                vm.Categories = _unitOfWork.CategoryRepository.GetAllCategories().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = vm.Categories.Any(c => c.Value == x.Id.ToString() && c.Selected)
                }).ToList();

                return View(vm);
            }

            // If the edit succeeded!
            FileUpload fileUpload = new FileUpload(_webHostEnvironment);
            string oldImgPath = _unitOfWork.ProductRepository.GetProductById(vm.Id).ProductImage;
            string newImageFile = fileUpload.UpdateFile(oldImgPath, vm.ProductImage);

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
            _unitOfWork.ProductRepository.UpdateProduct(updatedProduct, selectedCategories);
            _unitOfWork.Save();

            return RedirectToAction("Index", "Products");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _unitOfWork.ProductRepository.GetProductById(id);
            var mappedProduct = _mapper.Map<DeleteProductViewModel>(product);
            return View(mappedProduct);
        }

        [HttpPost]
        public IActionResult Delete(DeleteProductViewModel vm)
        {
            var product = _unitOfWork.ProductRepository.GetProductById(vm.Id);

            // To delete product's image!
            FileUpload file = new FileUpload(_webHostEnvironment);
            file.DeleteFile(product.ProductImage);

            // To delete the product from db!
            _unitOfWork.ProductRepository.DeleteProduct(product);
            _unitOfWork.Save();

            return RedirectToAction("Index", "Products");
        }
    }
}
