using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Examen.Models
{
    public class Customers
    {
        [Key]
        public int PKCustomer { get; set; }
        public string Customer { get; set; }
        public string Prefix { get; set; }
        public int FKBuilding { get; set; }
        public bool Available { get; set; }

        [ForeignKey("FKBuilding")]
        public Buildings Buildings { get; set; }
    }
}