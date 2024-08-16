using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstShopping.Models.ViewModels.Account
{
    public class UserNavPartialVM
    {
        [Display(Name = "الاسم الاول")]
        public string FirstName { get; set; }
        [Display(Name = "الاسم الاخير")]
        public string LastName { get; set; }
    }
}