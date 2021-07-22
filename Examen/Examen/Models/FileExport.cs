using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen.Models
{
    public class FileExport
    {
        public string FileName { get; set; }
        public byte[] File { get; set; }
    }
}