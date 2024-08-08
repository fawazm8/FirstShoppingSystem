using FirstShopping.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstShopping.Models.ViewModels.Shop
{
    public class OrderVM
    {
        public OrderVM()
        {
        }

        public OrderVM(OrderDTO row)
        {
            OrderId = row.OrderId;
            UserId = row.UserId;
            CreatedAt = row.CreatedAt;
        }
        [Display(Name = "الطلب")]
        public int OrderId { get; set; }
        [Display(Name = "المستخدم")]
        public int UserId { get; set; }
        [Display(Name = "التاريخ")]
        public DateTime CreatedAt { get; set; }
    }
}