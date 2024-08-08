using FirstShopping.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstShopping.Models.ViewModels.Pages
{

    public class SideBarVM
    {
        public SideBarVM()
        {

        }
        public SideBarVM(SidebarDTO row)
        {
            id = row.id;
            body = row.body;
        }
        public int id { get; set; }
        [Display(Name = "التفاصيل")]
        [AllowHtml]
        public string body { get; set; }
    }
}