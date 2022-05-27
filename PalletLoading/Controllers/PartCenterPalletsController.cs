using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PalletLoading.Data;
using PalletLoading.Models;

namespace PalletLoading.Controllers
{
    public class PartCenterPalletsController : MainController
    {

        public PartCenterPalletsController(PalletLoadingContext context) : base(null, context, null)
        {
        }

        // GET: PartCenterPallets
        public async Task<IActionResult> Index(string searchString, int? pageNumber, string currentdatetimepickerStartDate, string currentdatetimepickerEndDate, string datetimepickerStartDate, string datetimepickerEndDate)
        {
            //var tempTest = _context.ImportData.Where(c => c.container_no.Equals("MUW09052022") && c.consignee_code.Equals("MUW") && c.pallet_no == 1546)
            //    .Join(_context.PartCenterPallets,c=>c.id,c=>c.ImportDataId,(c,d)=>new ModelImportDataPccPallets { ImportData = c, PartCenterPallets = d})
            //    .ToList();

            DateTime dater = DateTime.Now;
            var cultureGB = new CultureInfo("en-GB");
            if (datetimepickerStartDate == null)
            {
                datetimepickerStartDate = currentdatetimepickerStartDate;
            }
            if (datetimepickerEndDate == null)
            {
                datetimepickerEndDate = currentdatetimepickerEndDate;
            }
            if (datetimepickerStartDate == null)
            {
                DateTime date = DateTime.Now;
                datetimepickerStartDate = new DateTime(date.Year, date.Month, 1).ToString("dd-MM-yyyy");
            }
            if (datetimepickerEndDate == null)
            {
                datetimepickerEndDate = DateTime.Now.ToString("dd-MM-yyyy");
            }
            var startDateTemp = DateTime.ParseExact(datetimepickerStartDate, "dd-MM-yyyy", cultureGB);
            var endDateTemp = DateTime.ParseExact(datetimepickerEndDate, "dd-MM-yyyy", cultureGB).AddDays(1);
            ViewBag.currentdatetimepickerStartDate = datetimepickerStartDate;
            ViewBag.currentdatetimepickerEndDate = datetimepickerEndDate;


            if (pageNumber == null)
            {
                pageNumber = 1;
            }

            List<PartCenterPallets> palletLoadingContext = _context.PartCenterPallets
                .Where(c => c.InputTimestamp >= startDateTemp && c.InputTimestamp <= endDateTemp)
                .OrderBy(c=>c.Status)
                .ThenByDescending(c=>c.InputTimestamp).ToList();
            ViewBag.currentSearch = "";
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.currentSearch = searchString;
                searchString = searchString.ToUpper();
                palletLoadingContext = palletLoadingContext.Where(x => x.Destination.Contains(searchString) || x.Pallet_number.ToString().Contains(searchString)).ToList();
            }

            int pageSize = 15;
            if (pageNumber < 1)
                pageNumber = 1;
            else if (pageNumber > ((palletLoadingContext.Count() - 1) / pageSize) + 1)
                pageNumber = ((palletLoadingContext.Count() - 1) / pageSize) + 1;

            var paginedList = await PaginatedList<PartCenterPallets>.CreateAsync(palletLoadingContext, pageNumber ?? 1, pageSize);

            return View(paginedList);
        }

        public IActionResult GetExcel(string sdate, string edate, string searchString)
        {
            var countOrders = 0;
            var cultureGB = new CultureInfo("en-GB");
            var startDateTemp = DateTime.Parse(sdate, cultureGB).Date;
            var endDateTemp = DateTime.Parse(edate, cultureGB).AddDays(1).Date;

            List<PartCenterPallets> palletLoadingContext = _context.PartCenterPallets
                            .Where(c => c.InputTimestamp >= startDateTemp && c.InputTimestamp <= endDateTemp)
                            .OrderBy(c => c.Status)
                            .ThenByDescending(c => c.InputTimestamp).ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();
                palletLoadingContext = palletLoadingContext.Where(x => x.Destination.Contains(searchString) || x.Pallet_number.ToString().Contains(searchString)).ToList();
            }

            var stream = new System.IO.MemoryStream();
            var noOrders = palletLoadingContext.Count();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report");

                worksheet.Cells[1, 1].Value = "Total. containere:";
                worksheet.Cells[1, 2].Value = noOrders;

                worksheet.Cells[6, 1].Value = "Nr.Crt";
                worksheet.Cells[6, 2].Value = "Destination";
                worksheet.Cells[6, 3].Value = "Pallet number";
                worksheet.Cells[6, 4].Value = "Mass (kg)";
                worksheet.Cells[6, 5].Value = "Shift";
                worksheet.Cells[6, 6].Value = "Volume";
                worksheet.Cells[6, 7].Value = "Input date";

                int c = 7;
                for (c = 7; c < palletLoadingContext.Count + 7; c++)
                {
                    worksheet.Cells[c, 1].Value = c - 6;
                    var templistContainer = "";
                    var templistCountry = "";
                    
                    worksheet.Cells[c, 2].Value = palletLoadingContext[c - 7].Destination;
                    worksheet.Cells[c, 3].Value = palletLoadingContext[c - 7].Pallet_number;
                    worksheet.Cells[c, 4].Value = palletLoadingContext[c - 7].Mass;
                    worksheet.Cells[c, 5].Value = palletLoadingContext[c - 7].Shift;
                    worksheet.Cells[c, 6].Value = palletLoadingContext[c - 7].Volume;
                    worksheet.Cells[c, 7].Value = palletLoadingContext[c - 7].InputTimestamp;

                }
                package.Save();
            }

            string fileName = "ReportData.xlsx";
            string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            stream.Position = 0;
            return File(stream, fileType, fileName);
        }

        // GET: PartCenterPallets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partCenterPallets = await _context.PartCenterPallets
                .Include(p => p.ImportData)
                .Include(p => p.ImportDataHistory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partCenterPallets == null)
            {
                return NotFound();
            }

            return View(partCenterPallets);
        }

        // GET: PartCenterPallets/Create
        public IActionResult Create()
        {
            ViewData["ImportDataId"] = new SelectList(_context.ImportData, "id", "id");
            ViewData["ImportDataHistoryId"] = new SelectList(_context.ImportData, "id", "id");
            return View();
        }

        // POST: PartCenterPallets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Container_no,Destination,Pallet_number,Length,Width,Height,Mass,Shift,Status,Volume,InputTimestamp,ImportDataId,ImportDataHistoryId")] PartCenterPallets partCenterPallets)
        {
            if (ModelState.IsValid)
            {
                //if (_context.ImportData.Any(c => c.container_no == partCenterPallets.Container_no && c.consignee_code == partCenterPallets.Destination && c.pallet_no == partCenterPallets.Pallet_number)) {
                //    var tempImportData = _context.ImportData.FirstOrDefault(c => c.container_no == partCenterPallets.Container_no && c.consignee_code == partCenterPallets.Destination && c.pallet_no == partCenterPallets.Pallet_number);
                //    partCenterPallets.ImportDataId = tempImportData.id;
                //    partCenterPallets.ImportDataHistoryId = null;
                //}
                //else if(_context.ImportDataHistory.Any(c => c.container_no == partCenterPallets.Container_no && c.consignee_code == partCenterPallets.Destination && c.pallet_no == partCenterPallets.Pallet_number))
                //{
                //    var tempImportData = _context.ImportDataHistory.FirstOrDefault(c => c.container_no == partCenterPallets.Container_no && c.consignee_code == partCenterPallets.Destination && c.pallet_no == partCenterPallets.Pallet_number);
                //    partCenterPallets.ImportDataHistoryId = tempImportData.id;
                //    partCenterPallets.ImportDataId = null;
                //}
                partCenterPallets.Destination = partCenterPallets.Destination.ToUpper();
                partCenterPallets.InputTimestamp = DateTime.Now;
                partCenterPallets.Status = false;
                partCenterPallets.Volume = partCenterPallets.Length * partCenterPallets.Height * partCenterPallets.Width;
                _context.Add(partCenterPallets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ImportDataId"] = new SelectList(_context.ImportData, "id", "id", partCenterPallets.ImportDataId);
            ViewData["ImportDataHistoryId"] = new SelectList(_context.ImportData, "id", "id", partCenterPallets.ImportDataHistoryId);
            return View(partCenterPallets);
        }

        // GET: PartCenterPallets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partCenterPallets = await _context.PartCenterPallets.FindAsync(id);
            if (partCenterPallets == null)
            {
                return NotFound();
            }
            ViewData["ImportDataId"] = new SelectList(_context.ImportData, "id", "id", partCenterPallets.ImportDataId);
            ViewData["ImportDataHistoryId"] = new SelectList(_context.ImportData, "id", "id", partCenterPallets.ImportDataHistoryId);
            return View(partCenterPallets);
        }

        // POST: PartCenterPallets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Container_no,Destination,Pallet_number,Length,Width,Height,Mass,Shift,Status,Volume,InputTimestamp")] PartCenterPallets partCenterPallets)
        {
            if (id != partCenterPallets.Id)
            {
                return NotFound();
            }

            //if (_context.ImportData.Any(c => c.container_no == partCenterPallets.Container_no && c.consignee_code == partCenterPallets.Destination && c.pallet_no == partCenterPallets.Pallet_number))
            //{
            //    var tempImportData = _context.ImportData.FirstOrDefault(c => c.container_no == partCenterPallets.Container_no && c.consignee_code == partCenterPallets.Destination && c.pallet_no == partCenterPallets.Pallet_number);
            //    partCenterPallets.ImportDataId = tempImportData.id;
            //    partCenterPallets.ImportDataHistoryId = null;

            //}
            //else if (_context.ImportDataHistory.Any(c => c.container_no == partCenterPallets.Container_no && c.consignee_code == partCenterPallets.Destination && c.pallet_no == partCenterPallets.Pallet_number))
            //{
            //    var tempImportData = _context.ImportDataHistory.FirstOrDefault(c => c.container_no == partCenterPallets.Container_no && c.consignee_code == partCenterPallets.Destination && c.pallet_no == partCenterPallets.Pallet_number);
            //    partCenterPallets.ImportDataHistoryId = tempImportData.id;
            //    partCenterPallets.ImportData = null;
            //}
            partCenterPallets.Volume = partCenterPallets.Length * partCenterPallets.Height * partCenterPallets.Width;

            _context.Update(partCenterPallets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public ActionResult CheckPCPallet(string destination, decimal palletNo)
        {

            var checkPallet = _context.PartCenterPallets.Any(c => c.Destination.ToLower().Equals(destination.ToLower()) && c.Pallet_number == palletNo && c.Status == false);
            //var checkPallet2 = _context.ImportData.Any(c => c.container_no.ToLower().Equals(container.ToLower()) && c.consignee_code.ToLower().Equals(destination.ToLower()) && c.pallet_no == palletNo);
            //var checkPallet3 = _context.ImportDataHistory.Any(c => c.container_no.ToLower().Equals(container.ToLower()) && c.consignee_code.ToLower().Equals(destination.ToLower()) && c.pallet_no == palletNo);
            if (checkPallet)
            {
                return Json(new { res = "false" });

            }
            return Json(new { res = "true" });

        }

        // GET: PartCenterPallets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partCenterPallets = await _context.PartCenterPallets
                .Include(p => p.ImportData)
                .Include(p => p.ImportDataHistory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partCenterPallets == null)
            {
                return NotFound();
            }

            return View(partCenterPallets);
        }

        // POST: PartCenterPallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partCenterPallets = await _context.PartCenterPallets.FindAsync(id);
            _context.PartCenterPallets.Remove(partCenterPallets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartCenterPalletsExists(int id)
        {
            return _context.PartCenterPallets.Any(e => e.Id == id);
        }
    }
}
