using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen.Models
{
    public class SearchPartNumber
    {
        public int FKCustomer { get; set; }
        public string PartNumber { get; set; }
    }
}