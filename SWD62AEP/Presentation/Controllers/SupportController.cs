using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class SupportController : Controller
    {
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }


        [HttpPost]
         public IActionResult Contact(string email, string query)
        {
            //ViewData, ViewBag

            if (string.IsNullOrEmpty(query))
            {
                ViewData["Feedback"] = "Question was left empty";
                //ViewData["divcolor"] = "warning";
            }
            else
            {
                ViewData["FeedBack"] = "Thankyou for your query, we will get back to you asap";
                //ViewBag.feedback = "Thankyou for your query, we will get back to you asap";
                //ViewData["divcolor"] = "normal";
            }

            return View();
        }
    }
}
