using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Examen.Models
{
    public class PartNumbers
    {
        [Key]
        public int PKPartNumber { get; set; }
        public string PartNumber { get; set; }
        public int FKCustomer { get; set; }
        public bool Available { get; set; }

        [ForeignKey("FKCustomer")]
        public Customers Customers { get; set; }
    }
}