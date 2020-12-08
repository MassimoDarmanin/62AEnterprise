using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class CategoriesService : ICategoriesService
    {
        private ICategoriesRepository _categoryRepo;
        public CategoriesService(IProductsRepository productRepo)
        {
            _categoryRepo = _categoryRepo;
        }

        public IQueryable<CategoryViewModel> GetCategories()
        {
            var list = from p in _categoryRepo.GetCategories()
                       select new CategoryViewModel()
                       {
                           Id = p.Id,
                           Name = p.Name
                       };

            return list;
        }
    }
}
