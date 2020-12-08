using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.Services;
using ShoppingCart.Application.ViewModels;

namespace Presentation.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsService _productsSevice;
        public ProductsController(IProductsService productsService)
        {
            _productsSevice = productsService;
        }

        public IActionResult Index()
        {
            var list = _productsSevice.GetProducts().OrderByDescending(x=>x.Price).Where(x=>x.Price > 100);
            var list2 = list.ToList();

            return View(list);
        }

        public IActionResult Details (Guid id)
        {
            var myProduct = _productsSevice.GetProduct(id);

            return View(myProduct);
        }
        //
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel data)
        {
            try
            {
                _productsSevice.AddProduct(data);

                ViewData["feedback"] = "Product was added successfully";
                ModelState.Clear();
            }
            catch(Exception ex)
            {
                //log errors
                ViewData["warning"] = "Product was not added. Check your details";
            }

            return View();
        }
        //
    }
}
