﻿namespace ShoppingCart.Web.ViewModels.ProductViewModels
{
    public class EditProductPostViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
    }
}
