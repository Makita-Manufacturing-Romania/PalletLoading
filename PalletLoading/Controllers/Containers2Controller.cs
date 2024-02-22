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
using System.ComponentModel;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

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
            PalletLoading.Models.Container container = _context.Containers.First(x => x.Id == containerId);
            List<Pallet> pallets = _context.Pallets.Include(c => c.PalletImportData).Include(c => c.PalletImportDataHistory).Where(x => x.Container2Id == containerId).ToList();

            ViewData["PalletId"] = new SelectList(_context.PalletTypes, "Id", "Name");
            TempData["containerId"] = id.ToString();

            ContainerType type = _context.ContainerTypes.First(x => x.Id == container.TypeId);
            Countries country = _context.Countries.First(x => x.Id == container.CountryId);
            Pallet pallet = new Pallet();
            if (pallets != null && pallets.Count() != 0)
            {
                pallet = pallets.First();
            }
            else
            {
                pallet = new Pallet();
            }


            container = _context.Containers.Include(c => c.ContainerAT).Include("ContainerAT.Country").First(x => x.Id == id);
            string countryText = "";
            foreach (var item in container.ContainerAT)
            {
                countryText += ", " + item.Country.Name;
            }
            countryText = countryText.TrimStart(',');

            var viewModel = new ContainerDetailsViewModel
            {
                Container = container,
                Pallets = pallets,
                Type = type,
                //Country = country,
                Pallet = pallet,
                countryText = countryText
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

            List<PalletLoading.Models.Container> containers = _context.Containers
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

            List<PalletLoading.Models.Container> containers = _context.Containers
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

            var paginedList = await PaginatedList<PalletLoading.Models.Container>.CreateAsync(containers, pageNumber ?? 1, pageSize);


            return View(paginedList);
        }

        // GET: Containers2/Details/5
        public async Task<IActionResult> Details(int? id, string pallet)
        {
            if (id == null)
            {
                return NotFound();
            }

            PalletLoading.Models.Container container = _context.Containers.Include(c => c.ContainerAT).Include("ContainerAT.Country").First(x => x.Id == id);
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


            ViewBag.containerId = id;

            var loadingTypesList = _context.LoadingTypes.ToList();

            List<string> loadingTypesToSelect = new List<string> { "Select Loading Type" };
            int LoadingTypeId = 0;
            if (container.LoadingTypeId != null)
            {
                LoadingTypeId = (int)container.LoadingTypeId;
            }
            else 
            {
                LoadingTypeId = 0;
            }

            int selectedType = 0;

            int i = 1;
            foreach (var item in loadingTypesList)
            {
                loadingTypesToSelect.Add(item.Id + ".   " + item.Abbreviation + "-" + item.Name);

                if (item.Id == LoadingTypeId)
                {
                    selectedType = i;
                }

                i++;
            }


            ViewBag.LoadingTypes = loadingTypesToSelect;
            ViewBag.selectedLoadType = selectedType;

            //send confirmations
            ViewBag.SecuringLoadConfirmation = container.securingLoadConfirm;
            ViewBag.LoadingTypeConfirmation = container.loadingTypeConfirm;
            ViewBag.FormDefinitionConfirmation = container.formDefinitionConfirm;

            bool pdfConf = false;
            if (container.securingLoadConfirm == true && container.loadingTypeConfirm == true && container.formDefinitionConfirm == true)
            {
                pdfConf = true;
            }
            ViewBag.ExportPDFConfirmation = pdfConf;

            //signatures martor
            if (container.operatorName == null)
            {
                ViewBag.OperatorSignatureValidation = false;
            }
            else
            {
                if (container.operatorName.Trim() == "")
                {
                    ViewBag.OperatorSignatureValidation = false;
                }
                else
                {
                    ViewBag.OperatorSignatureValidation = true;
                }
            }
            if (container.forklifterName == null)
            {
                ViewBag.ForklifterSignatureValidation = false;
            }
            else
            {
                if (container.forklifterName.Trim() == "")
                {
                    ViewBag.ForklifterSignatureValidation = false;
                }
                else
                {
                    ViewBag.ForklifterSignatureValidation = true;
                }
            }
            if (container.tlName == null)
            {
                ViewBag.TeamLeaderSignatureValidation = false;
            }
            else
            {
                if (container.tlName.Trim() == "")
                {
                    ViewBag.TeamLeaderSignatureValidation = false;
                }
                else
                {
                    ViewBag.TeamLeaderSignatureValidation = true;
                }
            }
            if (container.svName == null)
            {
                ViewBag.SupervisorSignatureValidation = false;
            }
            else
            {
                if (container.svName.Trim() == "")
                {
                    ViewBag.SupervisorSignatureValidation = false;
                }
                else
                {
                    ViewBag.SupervisorSignatureValidation = true;
                }
            }


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
        public async Task<IActionResult> Create([Bind("Id,Name,TypeId,CountryId")] PalletLoading.Models.Container container)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PalletId,TypeId,CountryId")] PalletLoading.Models.Container container)
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


        public IActionResult ShowSelectSecuringLoadModal(int id1)
        {
            var securingLoadElement = _context.SecuringLoads
                .Where(l => l.ContainerId == id1)
                .FirstOrDefault();

            if (securingLoadElement == null)
            {
                securingLoadElement = new SecuringLoad();
                securingLoadElement.Coltare = false;
                securingLoadElement.BareFixare = false;
                securingLoadElement.SaciProtectie = false;
                securingLoadElement.Chingi = false;
                securingLoadElement.Absorgel = false;
                securingLoadElement.ContainerId = id1;
                _context.Add(securingLoadElement);
                _context.SaveChanges();
            }

            return View("SelectSecuringLoadModal", securingLoadElement);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSecuringLoadData([Bind("Id,ContainerId,Chingi,Coltare,BareFixare,Absorgel,SaciProtectie")] SecuringLoad securingLoad)
        {
            var containerData = _context.Containers
                .Where(l => l.Id == securingLoad.ContainerId)
                .FirstOrDefault();
            containerData.securingLoadConfirm = true;

            _context.Update(securingLoad);
            _context.Update(containerData);
            _context.SaveChanges();

            return RedirectToAction("Details", "Containers2", new { id = securingLoad.ContainerId });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeLoadingType(int containerId, string selectedType)
        {

            string[] splitParts = selectedType.Split('.');
            string loadingTypeId = splitParts[0];

            string loadingData = splitParts[1].TrimStart();
            string[] splitParts2 = selectedType.Split('-');
            string abbreviation = splitParts2[0];
            string name = splitParts2[1];

            var containerData = _context.Containers
                .Where(l => l.Id == containerId)
                .FirstOrDefault();
            containerData.LoadingTypeId = Convert.ToInt32(loadingTypeId);

            containerData.loadingTypeConfirm = true;

            _context.Update(containerData);
            _context.SaveChanges();

            var jsonData = new Dictionary<string, object>();
            jsonData["Status"] = "Ok";
            return Json(jsonData);

        }


        public IActionResult ShowFormDefinitionModal(int id1)
        {
            var formTypeId = _context.Containers
                .Where(l => l.Id == id1)
                .Select(l => l.FormTypeId)
                .FirstOrDefault();

            if (formTypeId == null)
            {
                formTypeId = _context.FormDefinitions
                    .Select(l => l.Id)
                    .FirstOrDefault();
            }

            var formTypeData = _context.FormDefinitions
                .Where(l => l.Id == formTypeId)
                .FirstOrDefault();
            formTypeData.ContainerId = id1;

            return View("SelectFormDefinitionModal", formTypeData);
        }


        [HttpPost]
        public async Task<IActionResult> EditFormDefinitionData([Bind("Id,PDF_Name,DocRefNumber,Department,ContainerId")] FormDefinition formTypeCurrentData)
        {
            var formTypePreviousData = _context.FormDefinitions
                .Where(l => l.Id == formTypeCurrentData.Id)
                .FirstOrDefault();

            var containerData = _context.Containers
                .Where(l => l.Id == formTypeCurrentData.ContainerId)
                .FirstOrDefault();

            if (formTypeCurrentData.PDF_Name != formTypePreviousData.PDF_Name || formTypeCurrentData.DocRefNumber != formTypePreviousData.DocRefNumber ||
                formTypeCurrentData.Department != formTypePreviousData.Department)
            {
                //data was changed so we create a new entity
                FormDefinition newFormDefinition = new FormDefinition();

                newFormDefinition.PDF_Name = formTypeCurrentData.PDF_Name;
                newFormDefinition.DocRefNumber = formTypeCurrentData.DocRefNumber;
                newFormDefinition.Department = formTypeCurrentData.Department;
                _context.Add(newFormDefinition);
                _context.SaveChanges();

                containerData.FormTypeId = newFormDefinition.Id;
            }
            else
            {
                //data stays the same but we set FormDefinition Id to Default
                containerData.FormTypeId = formTypeCurrentData.Id;
            }

            containerData.formDefinitionConfirm = true;

            _context.Update(containerData);
            _context.SaveChanges();

            return RedirectToAction("Details", "Containers2", new { id = formTypeCurrentData.ContainerId });
        }

        [HttpGet]
        public ActionResult OperatorSign(int id)
        {
            string username = this._context.User.Where(c => c.Username.Equals(User.Identity.Name.Replace("MMRMAKITA\\", ""))).Select(c => c.Username).FirstOrDefault();

            var containerData = _context.Containers
                .Where(l => l.Id == id)
                .FirstOrDefault();

            containerData.operatorName = username;
            _context.Update(containerData);
            _context.SaveChanges();

            return RedirectToAction("Details", "Containers2", new { id = id });
        }

        [HttpGet]
        public ActionResult ForklifterSign(int id)
        {
            string username = this._context.User.Where(c => c.Username.Equals(User.Identity.Name.Replace("MMRMAKITA\\", ""))).Select(c => c.Username).FirstOrDefault();

            var containerData = _context.Containers
                .Where(l => l.Id == id)
                .FirstOrDefault();

            containerData.forklifterName = username;
            _context.Update(containerData);
            _context.SaveChanges();

            return RedirectToAction("Details", "Containers2", new { id = id });
        }

        [HttpGet]
        public ActionResult TeamLeaderSign(int id)
        {
            string username = this._context.User.Where(c => c.Username.Equals(User.Identity.Name.Replace("MMRMAKITA\\", ""))).Select(c => c.Username).FirstOrDefault();

            var containerData = _context.Containers
                .Where(l => l.Id == id)
                .FirstOrDefault();

            containerData.tlName = username;
            _context.Update(containerData);
            _context.SaveChanges();

            return RedirectToAction("Details", "Containers2", new { id = id });
        }

        [HttpGet]
        public ActionResult SupervisorSign(int id)
        {
            string username = this._context.User.Where(c => c.Username.Equals(User.Identity.Name.Replace("MMRMAKITA\\", ""))).Select(c => c.Username).FirstOrDefault();

            var containerData = _context.Containers
                .Where(l => l.Id == id)
                .FirstOrDefault();

            containerData.svName = username;
            _context.Update(containerData);
            _context.SaveChanges();

            return RedirectToAction("Details", "Containers2", new { id = id });
        }

        public ActionResult GeneratePDFNew(int id, string actionType)
        {
            int containerId = Convert.ToInt32(id);
            PalletLoading.Models.Container container = _context.Containers.First(x => x.Id == containerId);
            List<Pallet> pallets = _context.Pallets.Include(c => c.PalletImportData).Include(c => c.PalletImportDataHistory).Where(x => x.Container2Id == containerId).ToList();

            ViewData["PalletId"] = new SelectList(_context.PalletTypes, "Id", "Name");
            TempData["containerId"] = id.ToString();

            ContainerType type = _context.ContainerTypes.First(x => x.Id == container.TypeId);
            Countries country = _context.Countries.First(x => x.Id == container.CountryId);
            Pallet pallet = new Pallet();
            if (pallets != null && pallets.Count() != 0)
            {
                pallet = pallets.First();
            }
            else
            {
                pallet = new Pallet();
            }


            container = _context.Containers.Include(c => c.ContainerAT).Include("ContainerAT.Country").First(x => x.Id == id);
            string countryText = "";
            foreach (var item in container.ContainerAT)
            {
                countryText += ", " + item.Country.Name;
            }
            countryText = countryText.TrimStart(',');



            //get new data for PDF
            decimal totalTools = 0;
            decimal totalSpares = 0;
            int totalExportPallets = 0;
            int totalPartCenterPallets = 0;
            List<string> clients = new List<string>();

            foreach (var data in pallets)
            {
                if (data.PalletImportData != null )
                {
                    if (data.PalletImportData.salse_part != "PC Pallet")
                    {
                        //Tools
                        totalTools += data.PalletImportData.picking_qty;
                        totalExportPallets++;
                        if (!clients.Contains(data.PalletImportData.consignee_code))
                        {
                            clients.Add(data.PalletImportData.consignee_code);
                        }
                    }
                    else
                    {
                        //Spares
                        totalSpares += data.PalletImportData.picking_qty;
                        totalPartCenterPallets++;
                        if (!clients.Contains("PC"))
                        {
                            clients.Add("PC");
                        }
                    }
                }
                else if (data.PalletImportDataHistory != null )
                {
                    if (data.PalletImportDataHistory.salse_part != "PC Pallet")
                    {
                        //Tools
                        totalTools += data.PalletImportDataHistory.picking_qty;
                        totalExportPallets++;
                        if (!clients.Contains(data.PalletImportDataHistory.consignee_code))
                        {
                            clients.Add(data.PalletImportDataHistory.consignee_code);
                        }
                    }
                    else
                    {
                        //Spares
                        totalSpares += data.PalletImportDataHistory.picking_qty;
                        totalPartCenterPallets++;
                        if (!clients.Contains("PC"))
                        {
                            clients.Add("PC");
                        }
                    }
                }
            }

            TotalQuantities totalQuantities = new TotalQuantities();
            totalQuantities.TotalTools = Decimal.ToInt32(totalTools);
            totalQuantities.TotalSpares = Decimal.ToInt32(totalSpares);
            totalQuantities.TotalQuantity = Decimal.ToInt32(totalTools) + Decimal.ToInt32(totalSpares);
            totalQuantities.TotalToolsPallets = totalExportPallets;
            totalQuantities.TotalSparesPallets = totalPartCenterPallets;
            totalQuantities.TotalPallets = totalExportPallets + totalPartCenterPallets;

            //check clients totals
            List<ClientsData> clientsData = new List<ClientsData>();
            foreach (var item in clients)
            {
                ClientsData clnData = new ClientsData();
                clnData.name = item;
                clnData.quantity = 0;
                clnData.pallets = 0;

                clientsData.Add(clnData);
            }


            foreach (var data in pallets)
            {
                foreach (var client in clientsData)
                {

                    if (data.PalletImportData != null)
                    {
                        if ((client.name == "PC" && data.PalletImportData.salse_part == "PC Pallet") ||
                            (data.PalletImportData.consignee_code == client.name && data.PalletImportData.salse_part != "PC Pallet"))
                        {
                            client.quantity += Decimal.ToInt32(data.PalletImportData.picking_qty);
                            client.pallets += 1;
                        }
                    }
                    else if (data.PalletImportDataHistory != null)
                    {
                        if ((client.name == "PC" && data.PalletImportDataHistory.salse_part == "PC Pallet") ||
                            (data.PalletImportDataHistory.consignee_code == client.name && data.PalletImportDataHistory.salse_part != "PC Pallet"))
                        {
                            client.quantity += Decimal.ToInt32(data.PalletImportDataHistory.picking_qty);
                            client.pallets += 1;
                        }
                    }

                }
            }

            List<PalletForPDFViewModel> sortedPalletsWithTotals = new List<PalletForPDFViewModel>();

            int i = 1;
            int limit = pallets.Count;
            foreach (var client in clientsData) 
            {
                i = 1;
                foreach (var data in pallets) 
                {
                    if (client.name == "PC")
                    {
                        //Spares
                        if (data.PalletImportData != null)
                        {
                            if (data.PalletImportData.salse_part == "PC Pallet")
                            {
                                PalletForPDFViewModel rowToAdd = new PalletForPDFViewModel();
                                rowToAdd.orderNo = data.OrderNo.ToString();
                                rowToAdd.consigneAndContainer = data.PalletImportData.consignee_code + " - " + data.PalletImportData.container_no;
                                rowToAdd.salesPart = data.PalletImportData.salse_part;
                                rowToAdd.pallet_no = Convert.ToInt32(data.PalletImportData.pallet_no).ToString();
                                rowToAdd.serial_from = Convert.ToInt32(data.PalletImportData.serial_from).ToString();
                                rowToAdd.serial_to = Convert.ToInt32(data.PalletImportData.serial_to).ToString();
                                rowToAdd.picking_qty = Convert.ToInt32(data.PalletImportData.picking_qty).ToString();
                                rowToAdd.weight = data.PalletImportData.weight.ToString();
                                sortedPalletsWithTotals.Add(rowToAdd);
                            }
                        }
                        if (data.PalletImportDataHistory != null)
                        {
                            if (data.PalletImportDataHistory.salse_part == "PC Pallet")
                            {
                                PalletForPDFViewModel rowToAdd = new PalletForPDFViewModel();
                                rowToAdd.orderNo = data.OrderNo.ToString();
                                rowToAdd.consigneAndContainer = data.PalletImportDataHistory.consignee_code + " - " + data.PalletImportDataHistory.container_no;
                                rowToAdd.salesPart = data.PalletImportDataHistory.salse_part;
                                rowToAdd.pallet_no = Convert.ToInt32(data.PalletImportDataHistory.pallet_no).ToString();
                                rowToAdd.serial_from = Convert.ToInt32(data.PalletImportDataHistory.serial_from).ToString();
                                rowToAdd.serial_to = Convert.ToInt32(data.PalletImportDataHistory.serial_to).ToString();
                                rowToAdd.picking_qty = Convert.ToInt32(data.PalletImportDataHistory.picking_qty).ToString();
                                rowToAdd.weight = data.PalletImportDataHistory.weight.ToString();
                                sortedPalletsWithTotals.Add(rowToAdd);
                            }
                        }
                    }
                    else
                    {
                        //Tools
                        if (data.PalletImportData != null)
                        {
                            if (client.name.Trim() == data.PalletImportData.consignee_code && data.PalletImportData.salse_part != "PC Pallet")
                            {
                                PalletForPDFViewModel rowToAdd = new PalletForPDFViewModel();
                                rowToAdd.orderNo = data.OrderNo.ToString();
                                rowToAdd.consigneAndContainer = data.PalletImportData.consignee_code + " - " + data.PalletImportData.container_no;
                                rowToAdd.salesPart = data.PalletImportData.salse_part;
                                rowToAdd.pallet_no = Convert.ToInt32(data.PalletImportData.pallet_no).ToString();
                                rowToAdd.serial_from = Convert.ToInt32(data.PalletImportData.serial_from).ToString();
                                rowToAdd.serial_to = Convert.ToInt32(data.PalletImportData.serial_to).ToString();
                                rowToAdd.picking_qty = Convert.ToInt32(data.PalletImportData.picking_qty).ToString();
                                rowToAdd.weight = data.PalletImportData.weight.ToString();
                                sortedPalletsWithTotals.Add(rowToAdd);
                            }
                        }
                        if (data.PalletImportDataHistory != null)
                        {
                            if (client.name.Trim() == data.PalletImportDataHistory.consignee_code && data.PalletImportDataHistory.salse_part != "PC Pallet")
                            {
                                PalletForPDFViewModel rowToAdd = new PalletForPDFViewModel();
                                rowToAdd.orderNo = data.OrderNo.ToString();
                                rowToAdd.consigneAndContainer = data.PalletImportDataHistory.consignee_code + " - " + data.PalletImportDataHistory.container_no;
                                rowToAdd.salesPart = data.PalletImportDataHistory.salse_part;
                                rowToAdd.pallet_no = Convert.ToInt32(data.PalletImportDataHistory.pallet_no).ToString();
                                rowToAdd.serial_from = Convert.ToInt32(data.PalletImportDataHistory.serial_from).ToString();
                                rowToAdd.serial_to = Convert.ToInt32(data.PalletImportDataHistory.serial_to).ToString();
                                rowToAdd.picking_qty = Convert.ToInt32(data.PalletImportDataHistory.picking_qty).ToString();
                                rowToAdd.weight = data.PalletImportDataHistory.weight.ToString();
                                sortedPalletsWithTotals.Add(rowToAdd);
                            }
                        }
                    }
                    
                    if (i == pallets.Count) 
                    {
                        //Total insert
                        PalletForPDFViewModel lineRow = new PalletForPDFViewModel();
                        lineRow.orderNo = "Line";
                        lineRow.consigneAndContainer = "";
                        lineRow.salesPart = "";
                        lineRow.pallet_no = "";
                        lineRow.serial_from = "";
                        lineRow.serial_to = "";
                        lineRow.picking_qty = "";
                        lineRow.weight = "";
                        sortedPalletsWithTotals.Add(lineRow);
                        PalletForPDFViewModel rowToAdd = new PalletForPDFViewModel();
                        rowToAdd.orderNo = "New";
                        rowToAdd.consigneAndContainer = "Total " + client.name;
                        rowToAdd.salesPart = "";
                        rowToAdd.pallet_no = /*"Total Pallets:" +*/ client.pallets.ToString();
                        rowToAdd.serial_from = "";
                        rowToAdd.serial_to = "";
                        rowToAdd.picking_qty = /*"Total Qty:" +*/ client.quantity.ToString();
                        rowToAdd.weight = "";
                        sortedPalletsWithTotals.Add(rowToAdd);
                        PalletForPDFViewModel emptyRow = new PalletForPDFViewModel();
                        emptyRow.orderNo = "Empty";
                        emptyRow.consigneAndContainer = "";
                        emptyRow.salesPart = "";
                        emptyRow.pallet_no = "";
                        emptyRow.serial_from = "";
                        emptyRow.serial_to = "";
                        emptyRow.picking_qty = "";
                        emptyRow.weight = "";
                        sortedPalletsWithTotals.Add(emptyRow);
                        sortedPalletsWithTotals.Add(emptyRow);
                        sortedPalletsWithTotals.Add(emptyRow);
                    }

                    i++;
                }
            }


            List<string> totalClients2 = new List<string>();
            foreach (var palletData in pallets)
            {
                if (palletData.PalletImportData != null)
                {
                    if (!totalClients2.Contains(palletData.PalletImportData.consignee_code))
                    {
                        totalClients2.Add(palletData.PalletImportData.consignee_code);
                    }
                }
                else if (palletData.PalletImportDataHistory != null)
                {
                    if (!totalClients2.Contains(palletData.PalletImportDataHistory.consignee_code))
                    {
                        totalClients2.Add(palletData.PalletImportDataHistory.consignee_code);
                    }
                }
            }
            string totalClients = "";
            foreach (var client in totalClients2)
            {
                totalClients += client + ", ";
            }

            totalClients = totalClients.TrimEnd(' ');
            totalClients = totalClients.TrimEnd(',');

            //get selected truck type (TIP CAMION)
            string truckType = container.Type.Name;

            //get selected loading type (TIPUL INCARCARII)
            LoadingType loadingTypeData = _context.LoadingTypes
                .Where(l => l.Id == container.LoadingTypeId)
                .FirstOrDefault();
            string loadingType = loadingTypeData.Abbreviation + "-" + loadingTypeData.Name;

            //get form data
            FormDefinition formDefinition = _context.FormDefinitions
                .Where(l => l.Id == container.FormTypeId)
                .FirstOrDefault();

            //securing load data
            SecuringLoad securingLoad = _context.SecuringLoads
                .Where(l => l.ContainerId == container.Id)
                .FirstOrDefault();


            var viewModel = new ContainerDetailsViewModel
            {
                Container = container,
                Pallets = pallets,
                Type = type,
                //Country = country,
                Pallet = pallet,
                countryText = countryText,
                TotalQuantities = totalQuantities,
                palletCount = pallets.Count,
                ClientsData = clientsData,
                PalletsDataForPDF = sortedPalletsWithTotals,
                loadingData = container.CreatedDate.ToShortDateString(),
                totalClients = totalClients,
                truckType = truckType,
                loadingType = loadingType,
                FormDefinition = formDefinition,
                SecuringLoad = securingLoad,
            };


            if (actionType == "Download")
            {
                return new ViewAsPdf("ReportNew", viewModel)
                {
                    PageSize = Rotativa.AspNetCore.Options.Size.A4,
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                    FileName = formDefinition.PDF_Name + ".pdf"
                };
            }

            return new ViewAsPdf("ReportNew", viewModel)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };


        }


    }
}