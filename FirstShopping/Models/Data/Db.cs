using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FirstShopping.Models.Data
{
    public class Db : DbContext
    {
        public DbSet<PageDTO> Pages { get; set; }
        public DbSet<SidebarDTO> Sidebar { get; set; }
        public DbSet<CategoryDTO> Category { get; set; }
        public DbSet<ProductDTO> product { get; set; }
        
    }
}