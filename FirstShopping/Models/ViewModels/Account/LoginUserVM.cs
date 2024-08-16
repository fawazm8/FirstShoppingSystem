using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstShopping.Models.ViewModels.Account
{
    public class LoginUserVM
    {
        [Required]
        [Display(Name = "اسم المستخدم")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }
        [Display(Name = "تذكرني")]
        public bool RememberMe { get; set; }
    }
}