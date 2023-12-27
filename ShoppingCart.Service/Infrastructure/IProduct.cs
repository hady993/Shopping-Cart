﻿using ShoppingCart.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Service.Infrastructure
{
    public interface IProduct
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void InsertProduct(Product product, IEnumerable<int> categories);
        void UpdateProduct(Product product, IEnumerable<int> categories);
        void DeleteProduct(Product product);
        void Save();
    }
}
