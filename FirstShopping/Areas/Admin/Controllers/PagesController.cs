using FirstShopping.Models.Data;
using FirstShopping.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstShopping.Areas.Admin.Controllers
{

    public class PagesController : Controller
    {
        // GET: Admin/Pages

        public ActionResult Index()
        {
            // Declare list of PageVM
            List<PageVM> pagesList;

            using (Db db = new Db())
            {
                // Init the list
                pagesList = db.Pages.ToArray().OrderBy(x => x.PageSorting).Select(x => new PageVM(x)).ToList();
            }

            // Return view with list
            return View(pagesList);
        }
        [HttpGet]
        public ActionResult AddPages()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddPages(PageVM model)
        {
            using (Db db = new Db())
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                PageDTO dto = new PageDTO();
                if (db.Pages.Any(p => p.PageArName == model.PageArName) || db.Pages.Any(p => p.PageEnName == model.PageEnName))
                {
                    ModelState.AddModelError("", "اسم الصفحة موجود سابقا ");
                    return View(model);
                }
                dto.PageArName = model.PageArName;
                dto.PageEnName = model.PageEnName;
                dto.PageDescription = model.PageDescription;
                dto.PageHasSideBar = model.PageHasSideBar;
                dto.PageSorting = 100;

                db.Pages.Add(dto);
                db.SaveChanges();

                TempData["sm"] = "تمت عملية الحفظ بنجاح";

            }
            return View();
        }
        [HttpGet]

        public ActionResult EditPage(int id)
        {
            PageVM model;
            using (Db db = new Db())
            {
                var dto = db.Pages.Find(id);
                if (dto == null)
                {
                    return Content("الصفحة لاتوجد ");
                }
                model = new PageVM(dto);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditPage(PageVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            int id = model.PageId;
            string arName = model.PageArName;
            string enName = model.PageEnName;
            using (Db db = new Db())
            {
                var dTO = db.Pages.Find(model.PageId);
                if (db.Pages.Where(x => x.PageId != id).Any(x => x.PageArName == arName) ||
                    db.Pages.Where(x => x.PageId != id).Any(x => x.PageEnName == enName))
                {
                    ModelState.AddModelError("", "اسم الصفحة موجود سابقا ");
                    return View(model);
                }
                dTO.PageId = id;
                dTO.PageArName = arName;
                dTO.PageEnName = enName;
                dTO.PageDescription = model.PageDescription;
                dTO.PageHasSideBar = model.PageHasSideBar;

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeletePage(int id)
        {
            using (Db db = new Db())
            {
                PageDTO pageDTO = db.Pages.Find(id);
                db.Pages.Remove(pageDTO);
                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public void ReOrderPages(int[] id)
        {
            using (Db db = new Db())
            {
                int count = 1;
                PageDTO dto;
                foreach (var pageId in id)
                {
                    dto = db.Pages.Find(pageId);
                    dto.PageSorting = count;
                    db.SaveChanges();
                    count++;
                }

            }

        }

        [HttpGet]
        public ActionResult PageDetails(int id)
        {
            PageVM model;
            using (Db db = new Db())
            {
                PageDTO dTO = db.Pages.Find(id);
                if (dTO == null)
                {
                    return Content("الصفحة غير موجودة");
                }
                model = new PageVM(dTO);

            }


            return View(model);
        }
        [HttpGet]
        public ActionResult EditSidebar()
        {
            SideBarVM model;
            using (Db db = new Db())
            {
                SidebarDTO dto = db.Sidebar.Find(1);
                model = new SideBarVM(dto);

            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditSidebar(SideBarVM model)
        {
            using (Db db = new Db())
            {
                SidebarDTO dto = db.Sidebar.Find(1);
                dto.body = model.body;
                db.SaveChanges();
            }
            TempData["SM"] = "تم عملية التعديل بنجاح";

            return RedirectToAction("EditSidebar");
        }


    }
}