using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IProductsRepository
    {
        IQueryable <Product> GetProducts();
        Product GetProduct(Guid id);

        void AddProduct(Product p);

        void DeleteProduct(Guid id);
    }
}
