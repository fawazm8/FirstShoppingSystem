using FirstShopping.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirstShopping.Models.ViewModels.Shop
{
    public class CategoryVM
    {
        public CategoryVM()
        {

        }
        public CategoryVM(CategoryDTO row)
        {
            Id = row.Id;
            CategoryArName = row.CategoryArName;
            CategoryEnName = row.CategoryEnName;
            CategorySorting = row.CategorySorting;
            ImageName = row.ImageName;
        }
        public int Id { get; set; }
        [Display(Name = "اسم المجموعة بالعربي")]
        public string CategoryArName { get; set; }
        [Display(Name = "اسم المجموعة بالانجليزي")]
        public string CategoryEnName { get; set; }
        public int CategorySorting { get; set; }

        public string ImageName { get; set; }
    }
}