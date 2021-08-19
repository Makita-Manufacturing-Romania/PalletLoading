using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PalletLoading.Data;
using PalletLoading.Models;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Controllers
{
    public class CMRController : MainController
    {
        public CMRController(PalletLoadingContext context) : base(null, context, null)
        {
        }

        public ActionResult GeneratePDF(int id)
        {
            //Container container = _context.Containers.FirstOrDefault(x => x.Id == id);
            CmrData cmr = _context.CmrDatas.First(x => x.Id == id);

            return new ViewAsPdf("Create2", cmr)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
            };
        }

        public ActionResult Create(int id)
        {
            Container container = _context.Containers.Include(x => x.Country).First(x => x.Id == id);
            List<Pallet> pallets = _context.Pallets.Include(x => x.PalletImportDataHistory).Include(x => x.PalletImportData).Where(x => x.Container2Id == id).ToList();

            CmrData newCmr = new();
            newCmr.ContainerName = container.Name;
            decimal weight = 0;
            int noOfTools = 0, noOfSpr = 0, noPalletSpr = 0,noPalletTools = 0;
            Pallet pallet = new();

            if (pallets.Any(x => x.PalletImportDataId != null))
            {
                //weight, noOfTools, noOfSpr - today
                weight = pallets.Where(x => x.PalletImportData.salse_part != "").Sum(x => x.PalletImportData.weight);
                if (pallets.Any(x => x.PalletImportData.serial_from != 0))
                {
                    noPalletTools = pallets.Where(x => x.PalletImportData.serial_from != 0).Count();
                    noOfTools = (int)pallets.Where(x => x.PalletImportData.serial_from != 0).Sum(x => x.PalletImportData.picking_qty);
                }
                if (pallets.Any(x => x.PalletImportData.serial_from == 0))
                {
                    noPalletSpr = pallets.Where(x => x.PalletImportData.serial_from == 0).Count();
                    noOfSpr = (int)pallets.Where(x => x.PalletImportData.serial_from == 0).Sum(x => x.PalletImportData.picking_qty);
                }
                if (pallets.Any(x => x.PalletImportData.IPPLNO != "          "))
                    pallet = pallets.First(x => x.PalletImportData.IPPLNO != "          ");
                newCmr.PackingList = pallet.PalletImportData.IPPLNO;
            }

            else
            {
                //weight, noOfTools, noOfSpr - history
                weight = pallets.Where(x => x.PalletImportDataHistory.salse_part != "").Sum(x => x.PalletImportDataHistory.weight);
                if (pallets.Any(x => x.PalletImportDataHistory.serial_from != 0))
                {
                    noPalletTools = pallets.Where(x => x.PalletImportDataHistory.serial_from != 0).Count();
                    noOfTools = (int)pallets.Where(x => x.PalletImportDataHistory.serial_from != 0).Sum(x => x.PalletImportDataHistory.picking_qty);
                }
                if (pallets.Any(x => x.PalletImportDataHistory.serial_from == 0))
                {
                    noPalletSpr = pallets.Where(x => x.PalletImportDataHistory.serial_from == 0).Count();
                    noOfSpr = (int)pallets.Where(x => x.PalletImportDataHistory.serial_from == 0).Sum(x => x.PalletImportDataHistory.picking_qty);
                }
                if (pallets.Any(x => x.PalletImportDataHistory.IPPLNO != "          "))
                    pallet = pallets.First(x => x.PalletImportDataHistory.IPPLNO != "          ");
                newCmr.PackingList = pallet.PalletImportDataHistory.IPPLNO;
            }
            //client address
            var details = _context.CountryDescriptionImportData.First(x => x.CCODE.Equals(container.Country.Abbreviation));
            string address = details.CADR12 + "," + details.CADR22 + "," + details.CADR32;

            newCmr.NoOfTools = noPalletTools.ToString() + " PAL TOOLS - " + noOfTools.ToString() + " PCS";
            newCmr.NoOfSpr = noPalletSpr.ToString() + " PAL SPARSE - " + noOfSpr.ToString() + " PCS";
            newCmr.Weight = weight.ToString();
            newCmr.Adress = address;
            _context.CmrDatas.Add(newCmr);
            _context.SaveChanges();
            container.CmrId = newCmr.Id;
            _context.SaveChanges();
            return View(newCmr);            
        }

        public IActionResult Create2(string cmrId, string sealNo, string licensePlate, string driver, string weight)
        {
            int id = Convert.ToInt32(cmrId);
            CmrData newCmr = _context.CmrDatas.First(x => x.Id == id);
            newCmr.SerialNo = sealNo;
            newCmr.LicensePlate = licensePlate;
            newCmr.Driver = driver;
            if (newCmr.NoOfSpr != null && weight != null)
            {
                decimal weightAux = Convert.ToDecimal(weight);
                weightAux += Convert.ToDecimal(newCmr.Weight);
                newCmr.Weight = weightAux.ToString();
            }
            _context.SaveChanges();
            return View(newCmr);
        }
        public ActionResult CmrReport(int id)
        {
            CmrData cmr = _context.CmrDatas.First(x => x.Id == id);
            return View("Create2",cmr);
        }

        public ActionResult Delete (int id)
        {
            Container container = _context.Containers.First(x => x.Id == id);
            CmrData cmr = _context.CmrDatas.First(x => x.Id == container.CmrId);
            container.CmrId = null;
            _context.CmrDatas.Remove(cmr);
            _context.SaveChanges();
            return RedirectToAction("Create","Pallets", new { id = container.Id});
        }
    }
}
