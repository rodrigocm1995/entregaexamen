using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Examen.Models
{
    public class Buildings
    {
        [Key]
        public int PKBuilding { get; set; }
        public string Building { get; set; }
        public bool Available { get; set; }
    }
}