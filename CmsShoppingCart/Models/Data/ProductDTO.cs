using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FirstShopping.Models.Data
{
    [Table("ProductTbl")]
    public class ProductDTO
    {

        [Key]
        public int id { get; set; }
        public string ProductArName { get; set; }
        public string ProductEnName { get; set; }
        public decimal Price { get; set; }

        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public string ImageName { get; set; }

        public CategoryDTO category { get; set; }
        public int? ProductSort { get; set; }
    }
}