using FirstShopping.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstShopping.Models.ViewModels.Pages
{
    public class PageVM
    {
        public PageVM()
        {

        }
        public PageVM(PageDTO page)
        {
            PageId = page.PageId;
            PageArName = page.PageArName;
            PageEnName = page.PageEnName;
            PageDescription = page.PageDescription;
            PageSorting = page.PageSorting;
        }
        public int PageId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "اسم الصفحة بالعربي")]
        public string PageArName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "اسم الصفحة بالانجليزي")]
        public string PageEnName { get; set; }
        [Required]
        //[StringLength(50, MinimumLength = 3)]
        [Display(Name = "وصف الصفحة")]
        [AllowHtml]
        public string PageDescription { get; set; }
        [Display(Name = "ترتيب الصفحة")]
        public int PageSorting { get; set; }
        [Display(Name = "هل تحتوي على شريط جانبي؟")]
        public bool PageHasSideBar { get; set; }

    }
}