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

            ViewBag.TestSets = new SelectListItem[] {
                            new SelectListItem(){Text="Set1",Value="0"},
                            new SelectListItem(){Text="Set2",Value="1"}
            };

            ViewBag.Processes = new SelectListItem[] {
                            new SelectListItem(){Text="RCA",Value="0"},
                            new SelectListItem(){Text="Case Complexity",Value="1"}
            };


            return View();
        }

        public ActionResult Case()
        {
            return View();
        }
    }
}