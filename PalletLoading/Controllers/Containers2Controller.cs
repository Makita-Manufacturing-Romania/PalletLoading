using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PalletLoading.Data;
using PalletLoading.Models;
using PalletLoading.ViewModels;
using System.IO;
using Rotativa.AspNetCore;
using OfficeOpenXml;
using System.Drawing;
using System.Globalization;

namespace PalletLoading.Controllers
{
    public class Containers2Controller : MainController
    {

        public Containers2Controller(PalletLoadingContext context) : base(null, context, null)
        {
        }

        public ActionResult GeneratePDF(int id)
        {
            int containerId = Convert.ToInt32(id);
            Container container = _context.Containers.First(x => x.Id == containerId);
            List<Pallet> pallets = _context.Pallets.Include(c => c.PalletImportData).Include(c => c.PalletImportDataHistory).Where(x => x.Container2Id == containerId).ToList();

            ViewData["PalletId"] = new SelectList(_context.PalletTypes, "Id", "Name");
            TempData["containerId"] = id.ToString();

            ContainerType type = _context.ContainerTypes.First(x => x.Id == container.TypeId);
            Countries country = _context.Countries.First(x => x.Id == container.CountryId);
            Pallet pallet = new Pallet();
            if (pallets != null || pallets.Count() == 0)
            {
                pallet = pallets.First();
            }
            else
            {
                pallet = new Pallet();
            }

            var viewModel = new ContainerDetailsViewModel
            {
                Container = container,
                Pallets = pallets,
                Type = type,
                Country = country,
                Pallet = pallet
            };

            return new ViewAsPdf("Report", viewModel)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
        }

        public IActionResult GetExcel(string sdate, string edate, string searchString)
        {
            var countOrders = 0;
            var cultureGB = new CultureInfo("en-GB");
            var startDateTemp = DateTime.Parse(sdate, cultureGB).Date;
            var endDateTemp = DateTime.Parse(edate, cultureGB).AddDays(1).Date;

            List<Container> containers = _context.Containers
            .Include(c => c.Type)
            .Include(c => c.ContainerAT)
            .Include(c => c.Pallets)
            .Include("ContainerAT.Country")
            .Include("Pallets.PalletImportData")
            .Include("Pallets.PalletImportDataHistory").Where(c => c.CreatedDate >= startDateTemp && c.CreatedDate <= endDateTemp)
            .OrderByDescending(x => x.Id)
            //.Take(500)
            .ToList();


            if (!String.IsNullOrEmpty(searchString))
            {
                containers = containers.Where(x => x.ContainerAT.Any(c => c.ContainerName.Equals(searchString))).OrderByDescending(x => x.Id).ToList();
            }

            var stream = new System.IO.MemoryStream();
            var noOrders = containers.Count();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report");

                worksheet.Cells[1, 1].Value = "Total. containere:";
                worksheet.Cells[1, 2].Value = noOrders;

                worksheet.Cells[6, 1].Value = "Nr.Crt";
                worksheet.Cells[6, 2].Value = "Container";
                worksheet.Cells[6, 3].Value = "Tip container";
                worksheet.Cells[6, 4].Value = "Country";
                worksheet.Cells[6, 5].Value = "Volum";
                worksheet.Cells[6, 6].Value = "Number of pallets";

                int c = 7;
                for (c = 7; c < containers.Count + 7; c++)
                {
                    worksheet.Cells[c, 1].Value = c - 6;
                    var templistContainer = "";
                    var templistCountry = "";
                    if (containers[c - 7].ContainerAT != null)
                    {
                        var listNames = new List<String>();
                        var listNamesCountry = new List<String>();
                        foreach (var containerName in containers[c - 7].ContainerAT)
                        {
                            listNames.Add(containerName.ContainerName);
                            listNamesCountry.Add(containerName.Country.Name);
                        }
                        templistContainer = String.Join(", ", listNames);
                        templistCountry = String.Join(", ", listNamesCountry);
                    }
                    worksheet.Cells[c, 2].Value = templistContainer;
                    worksheet.Cells[c, 3].Value = containers[c - 7].Type.Name;
                    worksheet.Cells[c, 4].Value = templistCountry;

                    decimal volumTotal = 0;
                    var tempPID = containers[c - 7].Pallets.Where(c => c.PalletImportData != null && c.PalletImportData.serial_from != 0).Sum(c => c.PalletImportData.volume * c.PalletImportData.picking_qty);
                    var tempPIDH = containers[c - 7].Pallets.Where(c => c.PalletImportDataHistory != null && c.PalletImportDataHistory.serial_from != 0).Sum(c => c.PalletImportDataHistory.volume * c.PalletImportDataHistory.picking_qty);
                    var tempPIDHPC = containers[c - 7].Pallets.Where(c => c.PalletImportDataHistory != null && c.PalletImportDataHistory.serial_from == 0)
                    .Join(_context.PartCenterPallets, c => c.PalletImportDataHistory.id, c => c.ImportDataHistoryId, (c, d) => new { ImportData = c, PartCenterPallets = d })
                    .Sum(c => c.PartCenterPallets.Volume);
                    var tempPIDPC = containers[c - 7].Pallets.Where(c => c.PalletImportData != null && c.PalletImportData.serial_from == 0)
                    .Join(_context.PartCenterPallets, c => c.PalletImportData.id, c => c.ImportDataId, (c, d) => new { ImportData = c, PartCenterPallets = d })
                    .Sum(c => c.PartCenterPallets.Volume);
                    volumTotal = tempPID + tempPIDH + tempPIDHPC + tempPIDPC;
                    worksheet.Cells[c, 5].Value = volumTotal.ToString("N2") +"/" + containers[c - 7].Type.volume.ToString("N2")+" ("+ (volumTotal*100 / containers[c - 7].Type.volume).ToString("N2") + "%)";
                    worksheet.Cells[c, 6].Value = containers[c - 7].Pallets.Count();

                }
                package.Save();
            }

            string fileName = "ReportData.xlsx";
            string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            stream.Position = 0;
            return File(stream, fileType, fileName);
        }
        // GET: Containers2
        public async Task<IActionResult> Index(string searchString, int? pageNumber, string currentdatetimepickerStartDate, string currentdatetimepickerEndDate, string datetimepickerStartDate, string datetimepickerEndDate)
        {
            //var tempTest = _context.vEmployees_AD.ToList();
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
            ViewBag.currentSearch = "";

            if (pageNumber == null)
            {
                pageNumber = 1;
            }

            List<Container> containers = _context.Containers
                .Include(c => c.Type)
                .Include(c => c.ContainerAT)
                .Include(c => c.Pallets)
                .Include("ContainerAT.Country")
                .Include("Pallets.PalletImportData")
                .Include("Pallets.PalletImportDataHistory").Where(c => c.CreatedDate >= startDateTemp && c.CreatedDate <= endDateTemp)
                .OrderByDescending(x => x.Id)
                //.Take(500)
                .ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.currentSearch = searchString;
                containers = containers.Where(x => x.ContainerAT.Any(c => c.ContainerName.Equals(searchString))).OrderByDescending(x => x.Id).ToList();
            }

            //List<ContainerVM> containerList = new();
            //foreach(var container in containers)
            //{
            //    ContainerVM newContainer = new();
            //    newContainer.Container = container;

            //    var type = _context.ContainerTypes.First(x => x.Id == container.TypeId);
            //    newContainer.ContainerType = type;

            //    var country = _context.Countries.First(x => x.Id == container.CountryId);
            //    newContainer.Countries = country;

            //    containerList.Add(newContainer);
            //}

            int pageSize = 15;
            if (pageNumber < 1)
                pageNumber = 1;
            else if (pageNumber > ((containers.Count() - 1) / pageSize) + 1)
                pageNumber = ((containers.Count() - 1) / pageSize) + 1;

            var paginedList = await PaginatedList<Container>.CreateAsync(containers, pageNumber ?? 1, pageSize);

            return View(paginedList);
        }

        // GET: Containers2/Details/5
        public async Task<IActionResult> Details(int? id, string pallet)
        {
            if (id == null)
            {
                return NotFound();
            }

            Container container = _context.Containers.Include(c => c.ContainerAT).Include("ContainerAT.Country").First(x => x.Id == id);
            int idPallet = -1;
            if (pallet != null)
                idPallet = Convert.ToInt32(pallet);

            Pallet palletDetailed = null;
            if (idPallet != -1)
                palletDetailed = _context.Pallets.First(x => x.Id == idPallet);

            if (container == null)
            {
                return NotFound();
            }
            Countries country = _context.Countries.First(x => x.Id == container.CountryId);
            ContainerType type = _context.ContainerTypes.First(x => x.Id == container.TypeId);
            List<Pallet> pallets = _context.Pallets.Include(c => c.PalletImportData).Include(c => c.PalletImportDataHistory).Where(x => x.Container2Id == container.Id).ToList();

            List<ModelImportDataPccPallets> PccPallets = _context.Pallets.Include(c => c.PalletImportData).Include(c => c.PalletImportDataHistory).Where(x => x.Container2Id == container.Id)
                .GroupJoin(_context.PartCenterPallets, c => c.PalletImportDataId, c => c.ImportDataId, (c, d) => new { pallet = c, PartCenterPallets = d })
                .SelectMany(c => c.PartCenterPallets.DefaultIfEmpty(), (c, d) => new ModelImportDataPccPallets { pallet = c.pallet, PartCenterPallets = d })
                .GroupJoin(_context.PartCenterPallets, c => c.pallet.PalletImportDataHistoryId, c => c.ImportDataHistoryId, (c, d) => new { pallet = c.pallet, PartCenterPalletsHistory = d, PartCenterPallets = c.PartCenterPallets })
                .SelectMany(c => c.PartCenterPalletsHistory.DefaultIfEmpty(), (c, d) => new ModelImportDataPccPallets { pallet = c.pallet, PartCenterPalletsHistory = d, PartCenterPallets = c.PartCenterPallets })
                .ToList();

            var palletLength = _context.PalletTypes.Max(x => x.Length);
            var palletWidth = _context.PalletTypes.Max(x => x.Width);
            ViewBag.palletLength = palletLength;
            ViewBag.palletWidth = palletWidth;

            var viewModel = new ContainerDetailsViewModel
            {
                Container = container,
                Country = country,
                Type = type,
                Pallets = pallets,
                ModelImportDataPccPallets = PccPallets,
                Pallet = palletDetailed
            };

            return View(viewModel);
        }

        // GET: Containers2/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries.OrderBy(c=>c.Abbreviation).Select(c => new { Id = c.Id, Name = (c.Name + " - " + c.Abbreviation) }), "Id", "Name");

            ViewData["TypeId"] = new SelectList(_context.ContainerTypes, "Id", "Name");
            return View();
        }

        // POST: Containers2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TypeId,CountryId")] Container container)
        {
            if (ModelState.IsValid)
            {
                ContainerType type = _context.ContainerTypes.First(x => x.Id == container.TypeId);
                container.NoOfRows = type.Width / 100;
                container.NoOfColumns = type.Length / 100;
                container.CreatedDate = DateTime.Today;
                _context.Add(container);
                await _context.SaveChangesAsync();
                ContainerAT containerAT = new();
                containerAT.ContainerId = container.Id;
                containerAT.CountryId = (int)container.CountryId;
                containerAT.ContainerName = container.Name;
                _context.ContainerATs.Add(containerAT);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Pallets", new { id = container.Id });
            }

            ViewData["CountryId"] = new SelectList(_context.Countries.OrderBy(c => c.Abbreviation), "Id", "Id", container.CountryId);
            ViewData["TypeId"] = new SelectList(_context.ContainerTypes, "Id", "Name");
            return View(container);
        }

        public IActionResult Create2()
        {
            ViewBag.data = new string[] { "American Football", "Badminton", "Basketball", "Cricket", "Football", "Golf", "Hockey", "Rugby", "Snooker", "Tennis" };
            return View();
        }

        // GET: Containers2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var container = await _context.Containers.FindAsync(id);
            if (container == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", container.CountryId);
            return View(container);
        }

        // POST: Containers2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PalletId,TypeId,CountryId")] Container container)
        {
            if (id != container.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(container);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContainerExists(container.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", container.CountryId);
            return View(container);
        }

        // GET: Containers2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var container = await _context.Containers
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (container == null)
            {
                return NotFound();
            }

            return View(container);
        }

        // POST: Containers2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var container = await _context.Containers.FindAsync(id);
            _context.Containers.Remove(container);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContainerExists(int id)
        {
            return _context.Containers.Any(e => e.Id == id);
        }
    }
}
