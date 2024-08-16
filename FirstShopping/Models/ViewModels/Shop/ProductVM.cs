using FirstShopping.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstShopping.Models.ViewModels.Shop
{
    public class ProductVM
    {
        public ProductVM()
        {

        }
        public ProductVM(ProductDTO row)
        {
            id = row.id;
            ProductArName = row.ProductArName;
            ProductEnName = row.ProductEnName;
            Price = row.Price;
            Description = row.Description;
            ImageName = row.ImageName;
            CategoryID = row.CategoryID;
        }
        public int id { get; set; }
        [Required(ErrorMessage = "يجب ادخال اسم المنتج بالعربي")]
        [Display(Name = "اسم المنتج بالعربي ")]
        public string ProductArName { get; set; }
        [Display(Name = "اسم المنتج بالانجليزي ")]
        [Required(ErrorMessage = "يجب ادخال اسم المنتج بالانجليزي")]
        public string ProductEnName { get; set; }
        [Required(ErrorMessage = "يجب ادخال سعر المنتج")]
        [Display(Name = "سعر المنتج ")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "يجب ادخال وصف المنتج")]
        [Display(Name = " وصف المنتج  ")]
        [AllowHtml]
        public string Description { get; set; }
        [Display(Name = "اسم المجموعة ")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "يجب اختيار المجموعة")]
        [Display(Name = "المجموعة ")]
        public int CategoryID { get; set; }

        public string ImageName { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public IEnumerable<string> GalleryImages { get; set; }

        public int? ProductSort { get; set; }


    }
}