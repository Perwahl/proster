using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proster.Models;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.Script.Serialization;

namespace Proster.Controllers
{
    public class HomeController : Controller
    {
       // public List<Product> lstProduct new List<Compo>();;
        public List<Component> lstComponent = new List<Component>();
        public List<Area> lstArea = new List<Area>();

        public static String ProductJsonPath = AppDomain.CurrentDomain.BaseDirectory + @"\Data\Products.json" ;
        public static String ComponentJsonPath = AppDomain.CurrentDomain.BaseDirectory + @"\Data\Components.json";
        public static String AreaJsonPath = AppDomain.CurrentDomain.BaseDirectory + @"\Data\Areas.json";

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
            List<Product> lstProduct = GetProducts();

            ViewBag.Product = lstProduct;
        }

        private static List<Product> GetProducts()
        {
            var lstProduct = new List<Product>();
            var text = System.IO.File.ReadAllText(ProductJsonPath);
            var serializer = new JavaScriptSerializer();
            lstProduct = serializer.Deserialize<List<Product>>(text);
            return lstProduct;
        }

        private static List<Component> GetComponents()
        {
            var lstComponent = new List<Component>();
            var text = System.IO.File.ReadAllText(ComponentJsonPath);
            var serializer = new JavaScriptSerializer();
            lstComponent = serializer.Deserialize<List<Component>>(text);
            return lstComponent;
        }

        private static List<Area> GetAreas()
        {
            var lstArea = new List<Area>();
            var text = System.IO.File.ReadAllText(AreaJsonPath);
            var serializer = new JavaScriptSerializer();
            lstArea = serializer.Deserialize<List<Area>>(text);
            return lstArea;
        }

        //for server side
        void BindComponent(int? mProduct)
        {
            try
            {
                if (mProduct != 0)
                {
                    var lstComponent = GetComponents();
                    var component = from c in lstComponent
                                    where c.ParentProduct == mProduct || c.ParentProduct == 0
                                    select c;
                    ViewBag.Component = component;
                }
                else
                {
                    List<Component> Component = new List<Component>
                    {
                       new Component {Name = "Select" }
                    };
                    ViewBag.Component = Component;
                }
            }
            catch (Exception ex)
            {
            }
        }

        void BindArea(int? mComponent)
        {
            try
            {
                if (mComponent != 0)
                {
                    var lstCause = GetAreas();
                    var cause = from c in lstCause
                                where c.ParentComponent == mComponent || c.ParentComponent == 0
                                select c;
                    ViewBag.Cause = cause;
                }
                else
                {
                    List<Area> Cause = new List<Area> {
                        new Area { Name = "Select" } };
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
                    List<Component> lstComponent = GetComponents();
                
                    var component = from c in lstComponent
                                    where c.ParentProduct == mProduct || c.ParentProduct == 0
                                    select c;
                    return Json(new SelectList(component.ToArray(), "ID", "Name"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
            }
            return Json(null);
        }

        public JsonResult AreaList(int mComponent)
        {
            try
            {
                if (mComponent != 0)
                {         
                    List<Area> lstCause = GetAreas();
                    var cause = from c in lstCause
                                    where c.ParentComponent == mComponent || c.ParentComponent == 0
                                    select c;
                    return Json(new SelectList(cause.ToArray(), "ID", "Name"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
            }
            return Json(null);
        }

        public JsonResult AddValue(string type, string value, int parentID)
        {            
            if (type == "product")
            {
                var lstProduct = GetProducts();
                lstProduct.Add(new Product { Name = value, ID =lstProduct[lstProduct.Count()-1].ID + 1, });
                SaveList(lstProduct);
                return Json(new SelectList(lstProduct.ToArray(), "ID", "Name"), JsonRequestBehavior.AllowGet);
            }
            else if(type == "component")
            {
                var lstComponent = GetComponents();
                lstComponent.Add(new Component { Name = value, ID = lstComponent[lstComponent.Count()-1].ID+1, ParentProduct = parentID });
                SaveList(lstComponent);
                //  return Json(new SelectList(lstComponent.ToArray(), "ID", "Name"), JsonRequestBehavior.AllowGet);
                return ComponentList(parentID);
            }
            else if (type == "area")
            {
                var lstArea = GetAreas();
                lstArea.Add(new Area { Name = value, ID = lstArea[lstArea.Count() - 1].ID + 1, ParentComponent = parentID });
                SaveList(lstArea);
                // return Json(new SelectList(lstArea.ToArray(), "ID", "Name"), JsonRequestBehavior.AllowGet);
                return AreaList(parentID);
            }
            else
            {
                return Json(null);
            }            
        }

        internal void SaveList(List<Product> products)
        {
            var a = ProductJsonPath;
            if (!System.IO.File.Exists(ProductJsonPath))
            {               
                return;
            }
                      
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(products);
            
            System.IO.File.WriteAllText(ProductJsonPath, serializedResult);
        }

        internal void SaveList(List<Component> components)
        {
            var a = ComponentJsonPath;
            if (!System.IO.File.Exists(ComponentJsonPath))
            {
                return;
            }

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(components);

            System.IO.File.WriteAllText(ComponentJsonPath, serializedResult);
        }

        internal void SaveList(List<Area> areas)
        {
            var a = AreaJsonPath;
            if (!System.IO.File.Exists(AreaJsonPath))
            {
                return;
            }

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(areas);

            System.IO.File.WriteAllText(AreaJsonPath, serializedResult);
        }

        public ActionResult CreateMatrix()
        {
            BindProduct();
            BindComponent(0);
            BindArea(0);
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

