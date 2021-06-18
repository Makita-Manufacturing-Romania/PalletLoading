using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            TempData["containerId"] = id.ToString();

            var container = _context.Containers.Include(c=>c.Country).First(x => x.Id == id);
            List<Pallet> pallets = _context.Pallets.Where(x => x.Container2Id == container.Id).ToList();
            ContainerType type = _context.ContainerTypes.First(x => x.Id == container.TypeId);

            var viewModel = new AddContainerViewModel
            {
                Container = container,
                Pallets = pallets,
                Type = type
            };

            return View(viewModel);
        }

        public IActionResult GetPallets(string country, string container,int containerId)
        {
            var listPalletsMap = _context.ImportData.Where(c => c.consignee_code.Equals(country) && c.container_no.Equals(container)).OrderBy(c=>c.loading_time).ToList();
            var listPalletsApp = _context.Pallets.Where(c => c.Container2Id == containerId).OrderBy(c => c.OrderNo).ToList();
            var mvcp = new List<ModelViewCreatePallet>();
            int i = 0;
            for( ; i < listPalletsApp.Count; i++)
            {
                listPalletsApp[i].PalletImportDataId = listPalletsMap[i].id;
                var tempMVID = new ModelViewCreatePallet { OrderNoApp = listPalletsApp[i].OrderNo, PalletMap = listPalletsMap[i] };
                mvcp.Add(tempMVID);
            }
            for(; i < listPalletsMap.Count; i++)
            {
                var tempMVID = new ModelViewCreatePallet { OrderNoApp = -1, PalletMap = listPalletsMap[i] };
                mvcp.Add(tempMVID);
            }
            _context.UpdateRange(listPalletsApp);


            return Json(new { listPallets = mvcp });
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
                string containerId = "",position = ""; 
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
                return RedirectToAction("Create", new { id = idContainer});
            }
            return View(pallet);
        }
        
        public ActionResult AddPallet(string idContainer, string position, string palletName)
        {
            int containerId = Convert.ToInt32(idContainer);
            Container container = _context.Containers.First(x => x.Id == containerId);
            string rowAux = position.Split(",").First();
            string colAux = position.Split(",").Last();
            int row = Convert.ToInt32(rowAux);
            int col = Convert.ToInt32(colAux);

            Pallet pallet = new Pallet();
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

            return RedirectToAction("Create", new { id = containerId});
        }

        public ActionResult RemovePallet(string idContainer, string palletId)
        {
            int containerId = Convert.ToInt32(idContainer);
            Container container = _context.Containers.First(x => x.Id == containerId);

            int idPallet = Convert.ToInt32(palletId);
            Pallet pallet = _context.Pallets.First(x => x.Id == idPallet);

            _context.Pallets.Remove(pallet);
            _context.SaveChanges();

            return RedirectToAction("Create", new { id = containerId});
        }

        public ActionResult AddRow(string idContainer)
        {
            int containerId = Convert.ToInt32(idContainer);
            Container container = _context.Containers.First(x => x.Id == containerId);

            if(container.NoOfRows < 3)
                container.NoOfRows++;
            _context.SaveChanges();
            return RedirectToAction("Create", new { id = containerId });
        }

        public ActionResult RemoveRow(string idContainer)
        {
            int containerId = Convert.ToInt32(idContainer);
            Container container = _context.Containers.First(x => x.Id == containerId);

            if(container.NoOfRows > 1)
                container.NoOfRows--;
            _context.SaveChanges();
            return RedirectToAction("Create", new { id = containerId });
        }

        public ActionResult AddColumn(string idContainer)
        {
            int containerId = Convert.ToInt32(idContainer);
            Container container = _context.Containers.First(x => x.Id == containerId);

            if(container.NoOfColumns < 18)
                container.NoOfColumns++;
            _context.SaveChanges();
            return RedirectToAction("Create", new { id = containerId });
        }

        public ActionResult RemoveColumn(string idContainer)
        {
            int containerId = Convert.ToInt32(idContainer);
            Container container = _context.Containers.First(x => x.Id == containerId);

            if(container.NoOfColumns > 1)
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
