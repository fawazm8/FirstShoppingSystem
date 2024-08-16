using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FirstShopping.Areas.Admin.Models.ViewModels.Shop
{
    public class OrdersForAdminVM
    {
        [Display(Name = "رقم الطلب")]
        public int OrderNumber { get; set; }
        [Display(Name = "الاسم")]
        public string Username { get; set; }
        [Display(Name = "المجموع")]
        public decimal Total { get; set; }
        [Display(Name = "الكمية")]
        public Dictionary<string, int> ProductsAndQty { get; set; }
        [Display(Name = "تاريخ الانشاء")]
        public DateTime CreatedAt { get; set; }
    }
}