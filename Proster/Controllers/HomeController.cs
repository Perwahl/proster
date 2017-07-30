using Proster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proster.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.UserNames = new SelectListItem[] { 
                            new SelectListItem(){Text="Elias",Value="e"},
                            new SelectListItem(){Text="Jonte",Value="j"}
            };            
            
            return View();
        }
    }
}