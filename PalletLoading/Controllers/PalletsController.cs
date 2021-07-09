using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PalletLoading.Data;
using PalletLoading.Models;
using PalletLoading.ViewModels;

namespace PalletLoading.Controllers
{
    public class PalletsController : Controller
    {
        private readonly PalletLoadingContext _context;

        public PalletsController(PalletLoadingContext context)
        {
            _context = context;
        }

        // GET: Pallets
        public async Task<IActionResult> Index()
        {
            List<Pallet> pallets = _context.Pallets.ToList();
            List<PalletType> palletTypes = _context.PalletTypes.ToList();

            var viewModel = new PalletViewModel
            {
                Pallet = pallets,
                PalletType = palletTypes
            };
            return View(viewModel);
        }

        // GET: Pallets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pallet = await _context.Pallets
                .FirstOrDefaultAsync(m => m.Id == id);

            PalletType palletType = _context.PalletTypes.First(x => x.Id == pallet.TypeId);

            var viewModel = new PalletDetailsViewModel()
            {
                Pallet = pallet,
                PalletType = palletType
            };
            if (pallet == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Pallets/Create
        public IActionResult Create(int? id)
        {
            ViewData["PalletId"] = new SelectList(_context.PalletTypes, "Id", "Name");
            ViewData["CountryId"] = new SelectList(_context.Countries.OrderBy(x => x.Name), "Id", "Name");
            TempData["containerId"] = id.ToString();

            var container = _context.Containers.Include(c => c.Country).First(x => x.Id == id);
            List<Pallet> pallets = _context.Pallets.Where(x => x.Container2Id == container.Id).ToList();
            ContainerType type = _context.ContainerTypes.First(x => x.Id == container.TypeId);
            List<SwitchedPallet> switchedPallets = _context.SwitchedPallets.Where(x => x.IdContainer == container.Id).ToList();
            var pickedPallets = _context.ImportData.Where(x => x.container_no == container.Name).Count();
            ViewData["pickedPallets"] = pickedPallets;
            List<ContainerAT> containerATs = _context.ContainerATs.Where(x => x.ContainerId == container.Id).ToList();
            List<Countries> countries = new();
            foreach(var containerAT in containerATs)
            {
                var country = _context.Countries.First(x => x.Id == containerAT.CountryId);
                countries.Add(country);
            }

            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "sp_execPalletPLJob";


                cmd.CommandType = CommandType.StoredProcedure;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                cmd.CommandTimeout = 120;
                cmd.ExecuteReader();


                cmd.Connection.Close();
            }
            ImportDataPalletsLP idplp = _context.ImportDataPalletsLP.Where(c => c.CustomerCode180P.Equals(container.Country.Abbreviation) || c.CustomerCode250P.Equals(container.Country.Abbreviation)).FirstOrDefault();

            var viewModel = new AddContainerViewModel
            {
                Container = container,
                Pallets = pallets,
                Type = type,
                SwitchedPallets = switchedPallets,
                Countries = countries,
                idplp = idplp
            };

            return View(viewModel);
        }
        public IActionResult GetLP(string country)
        {

            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "sp_execPalletPLJob";


                cmd.CommandType = CommandType.StoredProcedure;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                cmd.CommandTimeout = 120;
                cmd.ExecuteReader();


                cmd.Connection.Close();
            }

            var importLP = _context.ImportDataPalletsLP.Where(c => c.CustomerCode180P.Equals(country) || c.CustomerCode250P.Equals(country)).FirstOrDefault();

            if(importLP == null)
            {
                importLP = new ImportDataPalletsLP { CustomerCode180P = country, CustomerCode250P = country, LOADED180 = 0, LOADED250 = 0, PICKED180 = 0, PICKED250 = 0 };
            }

            return Json(new { importLP = importLP });

        }
        public IActionResult GetPallets(string country, string container, int containerId)
        {

            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "sp_execPalletJob";


                cmd.CommandType = CommandType.StoredProcedure;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                cmd.CommandTimeout = 120;
                cmd.ExecuteReader();


                cmd.Connection.Close();
            }


            var dateContainer = _context.Containers.Any(c => c.CreatedDate == DateTime.Today);
            var listPalletsMap = _context.ImportData.Where(c => c.consignee_code.Equals(country) && c.container_no.Equals(container)).OrderBy(c => c.loading_time).ToList();



            if (dateContainer && listPalletsMap.Count != 0)
            {

                 listPalletsMap = _context.ImportData.Where(c => c.consignee_code.Equals(country) && c.container_no.Equals(container)).OrderBy(c => c.loading_time).ToList();
                var listPalletsApp = _context.Pallets.Where(c => c.Container2Id == containerId).OrderBy(c => c.OrderNo).ToList();
                var mvcp = new List<ModelViewCreatePallet>();
                int i = 0;
                for (; i < listPalletsApp.Count; i++)
                {
                    if (i < listPalletsMap.Count)
                    {
                        listPalletsApp[i].PalletImportDataId = listPalletsMap[i].id;
                        //listPalletsMap[i].Pallet = null;
                        var tempMVID = new ModelViewCreatePallet { OrderNoApp = listPalletsApp[i].OrderNo, PalletMap = listPalletsMap[i] };
                        mvcp.Add(tempMVID);
                    }

                }
                for (; i < listPalletsMap.Count; i++)
                {
                    var tempMVID = new ModelViewCreatePallet { OrderNoApp = -1, PalletMap = listPalletsMap[i] };
                    //listPalletsMap[i].Pallet = null;

                    mvcp.Add(tempMVID);
                }
                _context.UpdateRange(listPalletsApp);
                _context.SaveChanges();

                return Json(new { listPallets = mvcp });
            }
            else
            {
                var listPalletsMap2 = _context.ImportDataHistory.Where(c => c.consignee_code.Equals(country) && c.container_no.Equals(container)).OrderBy(c => c.loading_time).ToList();
                var listPalletsApp = _context.Pallets.Where(c => c.Container2Id == containerId).OrderBy(c => c.OrderNo).ToList();
                var mvcp = new List<ModelViewCreatePallet>();
                if (listPalletsMap2.Count == 0)
                {
                    return Json(new { listPallets = mvcp });

                }

                int i = 0;
                for (; i < listPalletsApp.Count; i++)
                {
                    listPalletsApp[i].PalletImportDataHistoryId = listPalletsMap2[i].id;
                    //listPalletsMap[i].Pallet = null;
                    var tempMVID = new ModelViewCreatePallet { OrderNoApp = listPalletsApp[i].OrderNo, PalletMapHistory = listPalletsMap2[i] };
                    mvcp.Add(tempMVID);
                }
                for (; i < listPalletsMap2.Count; i++)
                {
                    var tempMVID = new ModelViewCreatePallet { OrderNoApp = -1, PalletMapHistory = listPalletsMap2[i] };
                    //listPalletsMap[i].Pallet = null;

                    mvcp.Add(tempMVID);
                }
                _context.UpdateRange(listPalletsApp);
                _context.SaveChanges();

                return Json(new { listPallets = mvcp });

            }
        }


        // POST: Pallets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,TypeId")] Pallet pallet)
        {
            if (ModelState.IsValid)
            {
                string containerId = "", position = "";
                if (TempData.ContainsKey("containerId"))
                    containerId = TempData["containerId"].ToString();
                int idContainer = Convert.ToInt32(containerId);

                if (TempData.ContainsKey("position"))
                    position = TempData["position"].ToString();
                string rowAux = position.Split(",").First();
                string colAux = position.Split(",").Last();
                int row = Convert.ToInt32(rowAux);
                int col = Convert.ToInt32(colAux);

                Container container = _context.Containers.First(x => x.Id == idContainer);
                //container.PalletId = pallet.Id;

                /*                ICollection<Container> ContainerList = new Collection<Container>();
                                if (pallet.Containers != null)
                                {
                                    ContainerList = pallet.Containers;
                                }

                                ContainerList.Add(container);

                                pallet.Containers = ContainerList;*/
                if (_context.Pallets.Any(x => x.OrderNo == 1) && _context.Pallets.Any(x => x.Container2Id == container.Id))
                {

                    var lastPallet = _context.Pallets.OrderBy(x => x.OrderNo).Last(x => x.Container2Id == container.Id);
                    pallet.OrderNo = lastPallet.OrderNo + 1;
                }
                else
                    pallet.OrderNo = 1;

                pallet.Row = row;
                pallet.Column = col;

                pallet.Container2Id = idContainer;
                _context.Pallets.Add(pallet);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", new { id = idContainer });
            }
            return View(pallet);
        }

        public ActionResult AddCountry(string container, string country)
        {
            int containerId = Convert.ToInt32(container);
            int countryId = Convert.ToInt32(country);
            ContainerAT containerAT = new();
            containerAT.ContainerId = containerId;
            containerAT.CountryId = countryId;
            _context.ContainerATs.Add(containerAT);
            _context.SaveChanges();

            return RedirectToAction("Create", new { id = containerId});
        }

        public ActionResult SwitchPallets(string idContainer, string draggedPallet, string droppedPallet)
        {
            int containerId = Convert.ToInt32(idContainer);

            int idDragged = Convert.ToInt32(draggedPallet);
            int idDropped = Convert.ToInt32(droppedPallet);

            Pallet dragged = _context.Pallets.First(x => x.Id == idDragged);
            Pallet dropped = new();
            SwitchedPallet switchedPallets = new();
            if (_context.Pallets.Any(x => x.Id == idDropped))
                dropped = _context.Pallets.First(x => x.Id == idDropped);

            int? rowAux = 0, colAux = 0;
            int orderNoAux = 0;

            List<Pallet> pallets = _context.Pallets.Where(x => x.Container2Id == containerId).ToList();
            if (!pallets.Any(x => x.Row == rowAux && x.Column == colAux))
            {
                dropped.Row = dragged.Row;
                dropped.Column = dragged.Column;
                dropped.OrderNo = dragged.OrderNo;
                _context.Remove(dragged);
                _context.Add(dropped);
            }
            else
            {

                rowAux = dropped.Row;
                colAux = dropped.Column;
                orderNoAux = dropped.OrderNo;

                dropped.Row = dragged.Row;
                dropped.Column = dragged.Column;
                dropped.OrderNo = dragged.OrderNo;

                dragged.Row = rowAux;
                dragged.Column = colAux;
                dragged.OrderNo = orderNoAux;

                switchedPallets.FirstPallet = dragged.Name;
                switchedPallets.SecondPallet = dropped.Name;
                switchedPallets.IdContainer = containerId;

                _context.SwitchedPallets.Add(switchedPallets);
            }
            _context.SaveChanges();
            return RedirectToAction("Create", new { id = containerId });
        }

        public ActionResult AddPallet(string idContainer, string position, string palletName)
        {
            int containerId = Convert.ToInt32(idContainer);
            Container container = _context.Containers.First(x => x.Id == containerId);
            string rowAux = position.Split(",").First();
            string colAux = position.Split(",").Last();
            int row = Convert.ToInt32(rowAux);
            int col = Convert.ToInt32(colAux);

            Pallet pallet = new();
            pallet.Row = row;
            pallet.Column = col;
            pallet.Container2Id = containerId;
            pallet.Name = palletName;

            if (_context.Pallets.Any(x => x.OrderNo == 1) && _context.Pallets.Any(x => x.Container2Id == container.Id))
            {

                var lastPallet = _context.Pallets.OrderBy(x => x.OrderNo).Last(x => x.Container2Id == container.Id);
                pallet.OrderNo = lastPallet.OrderNo + 1;
            }
            else
                pallet.OrderNo = 1;

            _context.Pallets.Add(pallet);
            _context.SaveChanges();

            return RedirectToAction("Create", new { id = containerId });
        }

        public ActionResult RemovePallet(string idContainer, string palletId)
        {
            int containerId = Convert.ToInt32(idContainer);
            Container container = _context.Containers.First(x => x.Id == containerId);

            int idPallet = Convert.ToInt32(palletId);
            Pallet pallet = _context.Pallets.First(x => x.Id == idPallet);

            _context.Pallets.Remove(pallet);
            _context.SaveChanges();

            return RedirectToAction("Create", new { id = containerId });
        }

        public ActionResult AddRow(string idContainer)
        {
            int containerId = Convert.ToInt32(idContainer);
            Container container = _context.Containers.First(x => x.Id == containerId);

            if (container.NoOfRows < 3)
                container.NoOfRows++;
            _context.SaveChanges();
            return RedirectToAction("Create", new { id = containerId });
        }

        public ActionResult RemoveRow(string idContainer)
        {
            int containerId = Convert.ToInt32(idContainer);
            Container container = _context.Containers.First(x => x.Id == containerId);
            List<Pallet> pallets = new();
            var rowToRemove = _context.Pallets.Max(x => x.Row);
            if (_context.Pallets.Any(x => x.Row == rowToRemove))
            {
                pallets = _context.Pallets.Where(x => x.Row == rowToRemove).ToList();
                foreach (var palet in pallets)
                    _context.Remove(palet);
            }

            if (container.NoOfRows > 1)
                container.NoOfRows--;
            _context.SaveChanges();
            return RedirectToAction("Create", new { id = containerId });
        }

        public ActionResult AddColumn(string idContainer)
        {
            int containerId = Convert.ToInt32(idContainer);
            Container container = _context.Containers.First(x => x.Id == containerId);

            container.NoOfColumns++;
            _context.SaveChanges();
            return RedirectToAction("Create", new { id = containerId });
        }

        public ActionResult RemoveColumn(string idContainer)
        {
            int containerId = Convert.ToInt32(idContainer);
            Container container = _context.Containers.First(x => x.Id == containerId);
            List<Pallet> pallets = new();
            var colToRemove = _context.Pallets.Max(x => x.Column);
            if(_context.Pallets.Any(x => x.Column == colToRemove))
            {
                pallets = _context.Pallets.Where(x => x.Column == colToRemove).ToList();
                foreach (var pallet in pallets)
                    _context.Remove(pallet);
            }



            if (container.NoOfColumns > 1)
                container.NoOfColumns--;
            _context.SaveChanges();
            return RedirectToAction("Create", new { id = containerId });
        }

        // GET: Pallets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pallet = await _context.Pallets.FindAsync(id);
            if (pallet == null)
            {
                return NotFound();
            }
            return View(pallet);
        }

        // POST: Pallets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TypeId")] Pallet pallet)
        {
            if (id != pallet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pallet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PalletExists(pallet.Id))
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
            return View(pallet);
        }

        // GET: Pallets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pallet = await _context.Pallets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pallet == null)
            {
                return NotFound();
            }

            return View(pallet);
        }

        // POST: Pallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pallet = await _context.Pallets.FindAsync(id);
            _context.Pallets.Remove(pallet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PalletExists(int id)
        {
            return _context.Pallets.Any(e => e.Id == id);
        }
    }
}
