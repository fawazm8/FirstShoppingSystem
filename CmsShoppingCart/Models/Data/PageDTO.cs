using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FirstShopping.Models.Data
{
    [Table("PagesTbl")]
    public class PageDTO
    {
        [Key]
        public int PageId { get; set; }
        public string PageArName { get; set; }
        public string PageEnName { get; set; }
        public string PageDescription { get; set; }
        public int PageSorting { get; set; }
        public bool PageHasSideBar { get; set; }
        public bool Flag { get; set; }
    }
}