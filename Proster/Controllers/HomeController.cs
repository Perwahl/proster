using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proster.Models;
using System.Text.RegularExpressions;

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
            ViewBag.Product = new SelectListItem[] {
                            new SelectListItem(){Text="Qlik Sense Server",Value="0"},
                            new SelectListItem(){Text="Qlik Sense Desktop",Value="1"}
            };

            ViewBag.Component = new SelectListItem[] {
                            new SelectListItem(){Text="Engine",Value="0"},
                            new SelectListItem(){Text="Proxy",Value="1"}
            };

            ViewBag.Cause = new SelectListItem[] {
                            new SelectListItem(){Text="Port issue",Value="0"},
                            new SelectListItem(){Text="Certificate issue",Value="1"}
            };
            return View();
        }

        #region Private Methods
        void BindProduct()
        {
            List<Product> lstProduct = new List<Product>
            {
                new Product { ID = null, Name = "Select" },
                new Product { ID = 1, Name = "Qlik Sense" },
                new Product { ID = 2, Name = "QlikView" }
            };
            ViewBag.Product = lstProduct;
        }
        //for server side
        void BindComponent(int? mProduct)
        {
            try
            {
                if (mProduct != 0)
                {
                    //below code is only for demo, you can pick city from database
                    int index = 1;
                    List<Component> lstComponent = new List<Component>
                    {
                        new Component { Product = 0, ID=null, Name = "Select" },
                        new Component { Product = 1, ID=index++, Name = "Engine" },
                        new Component { Product = 1, ID=index++, Name = "Proxy" },
                        new Component { Product = 1, ID=index++, Name = "Repository" },
                        new Component { Product = 1, ID=index++, Name = "Application" },
                        new Component { Product = 1, ID=index++, Name = "Data Load Script" },
                        new Component { Product = 2, ID=index++, Name = "Application" },
                        new Component { Product = 2, ID=index++, Name = "Data Load Script" },
                        new Component { Product = 2, ID=index++, Name = "QlikView Server Service" },
                        new Component { Product = 2, ID=index++, Name = "QlikView Webserver" },
                    };
                    var component = from c in lstComponent
                                    where c.Product == mProduct || c.Product == 0
                                    select c;
                    ViewBag.Component = component;
                }
                else
                {
                    List<Component> Component = new List<Component>
                    {
                       new Component { ID = null, Name = "Select" }
                    };
                    ViewBag.Component = Component;
                }
            }
            catch (Exception ex)
            {
            }
        }

        void BindCause(int? mComponent)
        {
            try
            {
                if (mComponent != 0)
                {
                    //below code is only for demo, you can pick city from database
                    int index = 1;
                    List<Cause> lstCause = new List<Cause>
                    {
                        new Cause { Component = 0, ID=null, Name = "Select" },
                        new Cause { Component = 1, ID=index++, Name = "Port Issue" },
                        new Cause { Component = 1, ID=index++, Name = "Certificate Issue" },
                        new Cause { Component = 1, ID=index++, Name = "Product Defect" },
                        new Cause { Component = 1, ID=index++, Name = "Product Defect" },
                        new Cause { Component = 1, ID=index++, Name = "Other" },
                        new Cause { Component = 2, ID=index++, Name = "Port Issue" },
                        new Cause { Component = 2, ID=index++, Name = "Expression Issue" },
                        new Cause { Component = 2, ID=index++, Name = "Product Defect" },
                        new Cause { Component = 2, ID=index++, Name = "Other" },
                    };
                    var cause = from c in lstCause
                                where c.Component == mComponent || c.Component == 0
                                select c;
                    ViewBag.Cause = cause;
                }
                else
                {
                    List<Cause> Cause = new List<Cause> {
new Cause { ID = null, Name = "Select" } };
                    ViewBag.Cause = Cause;
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        //for client side using jquery
        public JsonResult ComponentList(int mProduct)
        {
            try
            {
                if (mProduct != 0)
                {
                    //below code is only for demo, you can pick city from database
                    int index = 1;
                    List<Component> lstComponent = new List<Component>
                    {
                        new Component { Product = 0, ID=null, Name = "Select" },
                        new Component { Product = 1, ID=index++, Name = "Engine" },
                        new Component { Product = 1, ID=index++, Name = "Proxy" },
                        new Component { Product = 1, ID=index++, Name = "Repository" },
                        new Component { Product = 1, ID=index++, Name = "Application" },
                        new Component { Product = 1, ID=index++, Name = "Data Load Script" },
                        new Component { Product = 2, ID=index++, Name = "Application" },
                        new Component { Product = 2, ID=index++, Name = "Data Load Script" },
                        new Component { Product = 2, ID=index++, Name = "QlikView Server Service" },
                        new Component { Product = 2, ID=index++, Name = "QlikView Webserver" },
                    };
                    var component = from c in lstComponent
                                    where c.Product == mProduct || c.Product == 0
                                    select c;
                    return Json(new SelectList(component.ToArray(), "ID", "Name"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
            }
            return Json(null);
        }

        public JsonResult CauseList(int mComponent)
        {
            try
            {
                if (mComponent != 0)
                {
                    //below code is only for demo, you can pick city from database
                    int index = 1;
                    List<Cause> lstCause = new List<Cause>
                    {
                        new Cause { Component = 0, ID=null, Name = "Select" },
                        new Cause { Component = 1, ID=index++, Name = "Port Issue" },
                        new Cause { Component = 1, ID=index++, Name = "Certificate Issue" },
                        new Cause { Component = 1, ID=index++, Name = "Product Defect" },
                        new Cause { Component = 1, ID=index++, Name = "Product Defect" },
                        new Cause { Component = 1, ID=index++, Name = "Other" },
                        new Cause { Component = 2, ID=index++, Name = "Port Issue" },
                        new Cause { Component = 2, ID=index++, Name = "Expression Issue" },
                        new Cause { Component = 2, ID=index++, Name = "Product Defect" },
                        new Cause { Component = 2, ID=index++, Name = "Other" },
                    };
                    var cause = from c in lstCause
                                    where c.Component == mComponent || c.Component == 0
                                    select c;
                    return Json(new SelectList(cause.ToArray(), "ID", "Name"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
            }
            return Json(null);
        }

        public ActionResult CreateMatrix()
        {
            BindProduct();
            BindComponent(0);
            BindCause(0);
            return View();
        }
        [HttpPost]
        public ActionResult CreateMatrix(RCA mRegister)
        {
            if (ModelState.IsValid)
            {
                return View("Completed");
            }
            else
            {
                ViewBag.SelProduct = mRegister.product;
                BindProduct();
                ViewBag.SelCity = mRegister.component;
                if (mRegister.product != null)
                    BindComponent(mRegister.product.ID);
                else
                    BindComponent(null);
                return View();
            }
        }
    }
}

