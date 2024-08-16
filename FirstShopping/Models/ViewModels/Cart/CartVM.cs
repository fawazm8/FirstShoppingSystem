using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstShopping.Models.ViewModels.Cart
{
    public class CartVM
    {
       
        [Display(Name = "الرقم")]
        public int ProductId { get; set; }
        [Display(Name = "الاسم")]
        public string ProductName { get; set; }
        [Display(Name = "الكمية")]
        public int Quantity { get; set; }
        [Display(Name = "السعر")]
        public decimal Price { get; set; }
        [Display(Name = "المجموع")]
        public decimal Total { get { return Quantity * Price; } }
        [Display(Name = "الصورة")]
        public string Image { get; set; }
    }
}