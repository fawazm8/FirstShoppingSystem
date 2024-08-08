using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FirstShopping.Models.Data
{
    [Table("CategortyTbl")]
    public class CategoryDTO
    {
        [Key]
        public int Id { get; set; }
        public string CategoryArName { get; set; }
        public string CategoryEnName { get; set; }
        public int CategorySorting { get; set; }
    }
}