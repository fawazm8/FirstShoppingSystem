using FirstShopping.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstShopping.Models.ViewModels.Account
{
    public class UserProfileVM
    {
        public UserProfileVM()
        {
        }

        public UserProfileVM(UserDTO row)
        {
            Id = row.Id;
            FirstName = row.FirstName;
            LastName = row.LastName;
            EmailAddress = row.EmailAddress;
            Username = row.Username;
            Password = row.Password;
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "الاسم الاول")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "الاسم الاخير ")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "البريد الالكتروني")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [Display(Name = "اسم المستخدم")]
        public string Username { get; set; }
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }
        [Display(Name = "تاكيد كلمة المرور")]
        public string ConfirmPassword { get; set; }
    }
}