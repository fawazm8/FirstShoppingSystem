using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstShopping.Models.ViewModels.Account
{
    public class OrdersForUserVM
    {
        [Display(Name = "رقم الطلب")]
        public int OrderNumber { get; set; }
        [Display(Name = "المجموع")]
        public decimal Total { get; set; }
        [Display(Name = "الكمية")]
        public Dictionary<string, int> ProductsAndQty { get; set; }
        [Display(Name = "انشات من قبل")]
        public DateTime CreatedAt { get; set; }
    }
}