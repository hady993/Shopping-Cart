﻿namespace ShoppingCart.Service.Infrastructure
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        void Save();
    }
}
