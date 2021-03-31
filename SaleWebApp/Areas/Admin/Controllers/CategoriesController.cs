using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleWebApp.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Admin/Categories
        public ActionResult Index()
        {
            AdminContext context = new AdminContext();
            List<Category> categories = context.Categories.ToList();
            return View(categories);
        }
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category categories)
        {
            categories.Id = Guid.NewGuid();
            categories.IsActive = true;

            AdminContext context = new AdminContext();
            context.Categories.Add(categories);
            context.SaveChanges();

            return RedirectToAction("Index", "Categories");
        }
        public ActionResult View(Guid? id)
        {
            if (id.HasValue)
            {
                AdminContext context = new AdminContext();
                Category categories = context.Categories.Where(a => a.Id == id.Value).FirstOrDefault();
                if (categories != null)
                {
                    return View(categories);
                }
                else
                {
                    return RedirectToAction("Index", "Categories");
                }
            }
            else
            {
                return RedirectToAction("Index", "Categories");
            }
        }
        public ActionResult Edit(Guid? id)
        {
            if (id.HasValue)
            {
                AdminContext context = new AdminContext();
                Category categories = context.Categories.Where(a => a.Id == id.Value).FirstOrDefault();
                if (categories != null)
                {
                    return View(categories);
                }
                else
                {
                    return RedirectToAction("Index", "Categories");
                }
            }
            else
            {
                return RedirectToAction("Index", "Categories");
            }
        }
        public ActionResult Update(Category categories)
        {
            AdminContext context = new AdminContext();
            Category result = context.Categories.Where(a => a.Id == categories.Id).FirstOrDefault();
            result.Name = categories.Name;
            result.Color = categories.Color;
            result.IsActive = categories.IsActive;
            context.SaveChanges();
            return RedirectToAction("Index", "Categories");
        }
        public ActionResult Archive(Guid? id)
        {
            if (id.HasValue)
            {
                AdminContext context = new AdminContext();
                Category result = context.Categories.Where(a => a.Id == id.Value).FirstOrDefault();
                result.IsActive = false;
                context.SaveChanges();
                return RedirectToAction("Index", "Categories");
            }
            else
            {
                return RedirectToAction("Index", "Categories");
            }
        }
        public ActionResult Activate(Guid? id)
        {
            if (id.HasValue)
            {
                AdminContext context = new AdminContext();
                Category result = context.Categories.Where(a => a.Id == id.Value).FirstOrDefault();
                result.IsActive = true;
                context.SaveChanges();
                return RedirectToAction("Index", "Categories");
            }
            else
            {
                return RedirectToAction("Index", "Categories");
            }
        }

        [HttpPost]
        public ActionResult Delete(Guid? id)
        {
            if (id.HasValue)
            {
                AdminContext context = new AdminContext();
                Category category= context.Categories.Where(a => a.Id == id.Value).FirstOrDefault();
                context.Categories.Remove(category);
                context.SaveChanges();
                return RedirectToAction("Index", "Categories");
            }
            else
            {
                return RedirectToAction("Index", "Categories");
            }
        }


    }
}