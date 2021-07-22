using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Examen.Data
{
    public class ExamenContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ExamenContext() : base("name=ExamenContext")
        {
        }

        public System.Data.Entity.DbSet<Examen.Models.Buildings> Buildings { get; set; }

        public System.Data.Entity.DbSet<Examen.Models.Customers> Customers { get; set; }

        public System.Data.Entity.DbSet<Examen.Models.PartNumbers> PartNumbers { get; set; }
    }
}
