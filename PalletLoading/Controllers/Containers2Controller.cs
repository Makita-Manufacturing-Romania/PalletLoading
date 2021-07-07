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

namespace PalletLoading.Controllers
{
    public class Containers2Controller : Controller
    {
        private readonly PalletLoadingContext _context;

        public Containers2Controller(PalletLoadingContext context)
        {
            _context = context;
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
            Pallet pallet = pallets.First();

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

        // GET: Containers2
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
            if(pageNumber == null)
            {
                pageNumber = 1;
            }
            List<Container> containers = _context.Containers.OrderByDescending(x => x.Id).ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                containers = _context.Containers.Where(x => x.Name.Contains(searchString)).OrderByDescending(x => x.Id).ToList();
            }

            List<ContainerVM> containerList = new();
            foreach(var container in containers)
            {
                ContainerVM newContainer = new();
                newContainer.Container = container;

                var type = _context.ContainerTypes.First(x => x.Id == container.TypeId);
                newContainer.ContainerType = type;

                var country = _context.Countries.First(x => x.Id == container.CountryId);
                newContainer.Countries = country;

                containerList.Add(newContainer);
            }

            int pageSize = 15;
            if (pageNumber < 1)
                pageNumber = 1;
            else if (pageNumber > ((containerList.Count() - 1) / pageSize) + 1)
                pageNumber = ((containerList.Count() - 1) / pageSize) + 1;

            var paginedList = await PaginatedList<ContainerVM>.CreateAsync(containerList, pageNumber ?? 1, pageSize);
            
            return View(paginedList);
        }

        // GET: Containers2/Details/5
        public async Task<IActionResult> Details(int? id,string pallet)
        {
            if (id == null)
            {
                return NotFound();
            }

            Container container = _context.Containers.First(x => x.Id == id);
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
            List<Pallet> pallets = _context.Pallets.Include(c => c.PalletImportData).Include(c=>c.PalletImportDataHistory).Where(x => x.Container2Id == container.Id).ToList();
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
                Pallet = palletDetailed
            };
            
            return View(viewModel);
        }

        // GET: Containers2/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
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
                container.NoOfRows = type.Width/100;
                container.NoOfColumns = type.Length / 100;
                container.CreatedDate = DateTime.Today;
                _context.Add(container);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create","Pallets", new { id = container.Id});
            }

            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", container.CountryId);
            ViewData["TypeId"] = new SelectList(_context.ContainerTypes, "Id", "Name");
            return View(container);
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
            ViewData["PalletId"] = new SelectList(_context.Pallets, "Id", "Id", container.PalletId);
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
            ViewData["PalletId"] = new SelectList(_context.Pallets, "Id", "Id", container.PalletId);
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
                .Include(c => c.Pallet)
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
