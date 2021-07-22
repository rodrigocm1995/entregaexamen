using ClosedXML.Excel;
using Examen.Data;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace Examen.Controllers
{
    public class ReportController : Controller
    {
        private ExamenContext db = new ExamenContext();

        /// <summary>
        /// Inicializa la pantalla de reporte obtiendo los customer activos y la lista completa de partnumber activos
        /// </summary>
        /// <returns></returns>
        // GET: Report
        public ActionResult Index()
        {
            var customers = db.Customers.Where(p => p.Available == true).ToList();
            customers.Insert(0, new Models.Customers { Available = true, Customer = "Seleccione", Prefix = "", PKCustomer = 0 });
            ViewBag.FKCustomer = new SelectList(customers, "PKCustomer", "Customer");
            var partNumbers = db.PartNumbers.Where(p => p.Available == true).Include(p => p.Customers).Include(p => p.Customers.Buildings).ToList();
            return View(partNumbers.ToList());
        }

        /// <summary>
        /// Nos permite buscar por cliente o por partnumber 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(int FKCustomer, string PartNumber)
        {
            var customers = db.Customers.Where(p => p.Available == true).ToList();
            customers.Insert(0, new Models.Customers { Available = true, Customer = "Seleccione", Prefix = "", PKCustomer = 0 });
            ViewBag.FKCustomer = new SelectList(customers, "PKCustomer", "Customer");

            return View(GetData(FKCustomer, PartNumber));
        }

        List<Models.PartNumbers> GetData(int FKCustomer, string PartNumber)
        {
            List<Models.PartNumbers> partNumbers = new List<Models.PartNumbers>();

            if (FKCustomer > 0 && PartNumber.Length == 0)
                partNumbers = db.PartNumbers.Where(p => p.Available == true && p.FKCustomer == FKCustomer).Include(p => p.Customers).Include(p => p.Customers.Buildings).ToList();

            if (FKCustomer > 0 && PartNumber.Length > 0)
                partNumbers = db.PartNumbers.Where(p => p.Available == true && p.FKCustomer == FKCustomer && p.PartNumber.Trim().ToUpper().Contains(
                    PartNumber.Trim().ToUpper())).Include(p => p.Customers).ToList();

            if (FKCustomer == 0 && PartNumber.Length == 0)
                partNumbers = db.PartNumbers.Where(p => p.Available == true).Include(p => p.Customers).ToList();

            if (FKCustomer == 0 && PartNumber.Length > 0)
                partNumbers = db.PartNumbers.Where(p => p.Available == true && p.PartNumber.Trim().ToUpper().Contains(
                    PartNumber.Trim().ToUpper())).Include(p => p.Customers).ToList();

            return partNumbers;
        }

        /// <summary>
        /// Permite exportar la lista a excel con la ayuda de una librería llamada ClosedXML
        /// https://github.com/ClosedXML/ClosedXML
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Export(List<Models.PartNumbers> List)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                var workBook = new XLWorkbook();
                var workSheet = workBook.Worksheets.Add("Examen");

                workSheet.Cell(1, 1).Value = "PartNumber";
                workSheet.Cell(1, 1).Style.Font.Bold = true;
                workSheet.Cell(1, 2).Value = "Customer";
                workSheet.Cell(1, 2).Style.Font.Bold = true;
                workSheet.Cell(1, 3).Value = "Building";
                workSheet.Cell(1, 3).Style.Font.Bold = true;

                int row = 2;
                List.ForEach(p =>
                {
                    workSheet.Cell(row, 1).Value = p.PartNumber;
                    workSheet.Cell(row, 2).Value = p.Customers.Customer;
                    workSheet.Cell(row, 3).Value = p.Customers.Buildings.Building;
                    row = row + 1;
                });

                workBook.SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                return Json(new Models.FileExport
                {
                    File = memoryStream.ToArray(),
                    FileName = "Examen.xlsx"
                });
            }
        }
    }
}