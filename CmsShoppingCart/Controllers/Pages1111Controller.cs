using FirstShopping.Models.Data;
using FirstShopping.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstShopping.Controllers
{
    public class PagesController : Controller
    {
        // GET: Index/{page}
        public ActionResult Index(string page = "")
        {
            // Get/set page SimilarName
            if (page == "")
                page = "home";

            // Declare model and DTO
            PageVM model;
            PageDTO dto;

            // Check if page exists
            using (Db db = new Db())
            {
                if (! db.Pages.Any(x => x.PageArName.Equals(page)))
                {
                    return RedirectToAction("Index", new { page = "" });
                }
            }

            // Get page DTO
            using (Db db = new Db())
            {
                dto = db.Pages.Where(x => x.PageArName == page).FirstOrDefault();
            }

            // Set page title
            ViewBag.PageTitle = dto.PageArName;

            // Check for sidebar
            if (dto.PageHasSideBar == true)
            {
                ViewBag.Sidebar = "Yes";
            }
            else
            {
                ViewBag.Sidebar = "No";
            }

            // Init model
            model = new PageVM(dto);

            // Return view with model
            return View(model);
        }
        
        public ActionResult PagesMenuPartial()
        {
            // Declare a list of PageVM
            List<PageVM> pageVMList;

            // Get all pages except home
            using (Db db = new Db())
            {
                pageVMList = db.Pages.ToArray().OrderBy(x => x.PageSorting).Where(x => x.PageEnName != "home").Select(x => new PageVM(x)).ToList();
            }
            // Return partial view with list
            return PartialView(pageVMList);
        }

        public ActionResult SidebarPartial()
        {
            // Declare model
            SidebarVM model;

            // Init model
            using (Db db = new Db())
            {
                SidebarDTO dto = db.Sidebar.Find(1);

                model = new SidebarVM(dto);
            }

            // Return partial view with model
            return PartialView(model);
        }
    }
}