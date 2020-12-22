using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.Services;
using ShoppingCart.Application.ViewModels;

namespace Presentation.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsService _productsSevice;
        private ICategoriesService _categoriesService;
        private IWebHostEnvironment _env;
        public ProductsController(IProductsService productsService, ICategoriesService categoriesService, IWebHostEnvironment env)
        {
            _productsSevice = productsService;
            _categoriesService = categoriesService;
            _env = env;
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
            var catList = _categoriesService.GetCategories();
            ViewBag.Categories = catList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel data, IFormFile file)
        {
            try
            {
                if(file != null)
                {
                    string newFilename = Guid.NewGuid() + System.IO.Path.GetExtension(file.FileName);
                    //C: \Users\Massimo\source\repos\MassimoDarmanin\62AEnterprise\SWD62AEP\Presentation\wwwroot\Images\
                    string absolutePath = _env.WebRootPath + @"\Images\";

                    using (var stream = System.IO.File.Create(absolutePath + newFilename))
                    {
                        file.CopyTo(stream);
                    }
                    data.ImageUrl = @"\Images\" + newFilename;
                }

                _productsSevice.AddProduct(data);

                ViewData["feedback"] = "Product was added successfully";
                ModelState.Clear();
            }
            catch(Exception ex)
            {
                //log errors
                ViewData["warning"] = "Product was not added. Check your details";
            }
            var catList = _categoriesService.GetCategories();
            ViewBag.Categories = catList;

            return View();
        }

        public IActionResult Delete(Guid id)
        {
            _productsSevice.DeleteProduct(id);
            TempData["feedback"] = "Product was deleted successfully";
            return RedirectToAction("Index");
        }
        //
    }
}
