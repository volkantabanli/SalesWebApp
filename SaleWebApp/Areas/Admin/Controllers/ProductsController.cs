using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleWebApp.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Admin/Products
        public ActionResult Index()
        {
            AdminContext context = new AdminContext();
            List<Product> products = context.Products.ToList();
            return View(products);
        }
        public ActionResult New()

        {


            DropDownCategoriesList();

            return View();
        }

        [HttpPost]
        public ActionResult Create(Product products, HttpPostedFileBase file)
        {
            products.Id = Guid.NewGuid();
            products.IsActive = true;

                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Areas/UploadedFiles"), _FileName);
                    file.SaveAs(_path);

                    products.Image = _FileName;
                }


            AdminContext context = new AdminContext();
            context.Products.Add(products);
            context.SaveChanges();

            return RedirectToAction("Index", "Products");
        }

        public ActionResult View(Guid? id)
        {
            if (id.HasValue)
            {
                AdminContext context = new AdminContext();
                Product products = context.Products.Where(a => a.Id == id.Value).FirstOrDefault();
                if (products != null)
                {
                    return View(products);
                }
                else
                {
                    return RedirectToAction("Index", "Products");
                }
            }
            else
            {
                return RedirectToAction("Index", "Products");
            }
        }

        public ActionResult Edit(Guid? id)
        {
            if (id.HasValue)
            {
                AdminContext context = new AdminContext();
                Product products = context.Products.Where(a => a.Id == id.Value).FirstOrDefault();
                if (products != null)
                {
                    return View(products);
                }
                else
                {
                    return RedirectToAction("Index", "Products");
                }
            }
            else
            {
                return RedirectToAction("Index", "Products");
            }
        }

        public ActionResult Update(Product products)
        {
            AdminContext context = new AdminContext();
            Product result = context.Products.Where(a => a.Id == products.Id).FirstOrDefault();
            result.Name = products.Name;
            result.Description = products.Description;
            result.UnitsInStock = products.UnitsInStock;
            result.UnitPrice = products.UnitPrice;
            result.SalePrice = products.SalePrice;
            result.Image = products.Image;
            result.IsActive = products.IsActive;
            context.SaveChanges();
            return RedirectToAction("Index", "Products");
        }
        public ActionResult Archive(Guid? id)
        {
            if (id.HasValue)
            {
                AdminContext context = new AdminContext();
                Product result = context.Products.Where(a => a.Id == id.Value).FirstOrDefault();
                result.IsActive = false;
                context.SaveChanges();
                return RedirectToAction("Index", "Products");
            }
            else
            {
                return RedirectToAction("Index", "Products");
            }
        }
        public ActionResult Activate(Guid? id)
        {
            if (id.HasValue)
            {
                AdminContext context = new AdminContext();
                Product result = context.Products.Where(a => a.Id == id.Value).FirstOrDefault();
                result.IsActive = true;
                context.SaveChanges();
                return RedirectToAction("Index", "Products");
            }
            else
            {
                return RedirectToAction("Index", "Products");
            }
        }
        [HttpPost]
        public ActionResult Delete(Guid? id)
        {
            if (id.HasValue)
            {
                AdminContext context = new AdminContext();
                Product products = context.Products.Where(a => a.Id == id.Value).FirstOrDefault();
                context.Products.Remove(products);
                context.SaveChanges();
                return RedirectToAction("Index", "Products");
            }
            else
            {
                return RedirectToAction("Index", "Products");
            }
        }


       private void DropDownCategoriesList()
        {
            AdminContext context = new AdminContext();

            List<SelectListItem> degerler = (from i in context.Categories.ToList()
                                             select new SelectListItem
                                             {

                                                 Text = i.Name,
                                                 Value = i.No.ToString()
                                             }).ToList();


            ViewBag.dgr = degerler;
        }
    }
}