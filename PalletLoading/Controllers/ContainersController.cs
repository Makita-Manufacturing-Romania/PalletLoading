using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PalletLoading.Data;
using PalletLoading.Models;

namespace PalletLoading.Controllers
{
    public class ContainersController : MainController
    {

        public ContainersController(PalletLoadingContext context):base(null,context,null)
        {
        }

        // GET: Containers
        public async Task<IActionResult> Index()
        {
            var palletLoadingContext = _context.Containers.Include(c => c.Pallet);
            return View(await palletLoadingContext.ToListAsync());
        }

        // GET: Containers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var container = await _context.Containers
                .Include(c => c.Pallet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (container == null)
            {
                return NotFound();
            }

            return View(container);
        }

        public IActionResult AddContainer()
        {
            ViewData["TypeId"] = new SelectList(_context.ContainerTypes, "Id", "Name");
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            return View("AddContainer");
        }

        // GET: Containers/Create
        public IActionResult Create(int type, string containerName,int country)
        {
            ViewData["PalletId"] = new SelectList(_context.Pallets, "Id", "Name");
            int containerTypeId = Convert.ToInt32(type);
            ContainerType containerType = _context.ContainerTypes.First(x => x.Id == containerTypeId);

            ViewData["length"] = containerType.Length;
            ViewData["width"] = containerType.Width;
            TempData["name"] = containerName;
            TempData["country"] = country;
            return View();
        }

        // POST: Containers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PalletId")] Container container)
        {
            string name = "", country = "";

            if(TempData.ContainsKey("name") && TempData.ContainsKey("country"))
            {
                name = TempData["name"].ToString();
                country = TempData["country"].ToString();
            }

            if (ModelState.IsValid)
            {
                container.Name = name;
                container.CountryId = Convert.ToInt32(country);
                _context.Containers.Add(container);
                await _context.SaveChangesAsync();
            }
            ViewData["PalletId"] = new SelectList(_context.Pallets, "Name", "Name", container.Pallet);
            return View(container);
        }

        // GET: Containers/Edit/5
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
            ViewData["PalletId"] = new SelectList(_context.Pallets, "Id", "Id", container.PalletId);
            return View(container);
        }

        // POST: Containers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PalletId")] Container container)
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
            ViewData["PalletId"] = new SelectList(_context.Pallets, "Id", "Id", container.PalletId);
            return View(container);
        }

        // GET: Containers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var container = await _context.Containers
                .Include(c => c.Pallet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (container == null)
            {
                return NotFound();
            }

            return View(container);
        }

        // POST: Containers/Delete/5
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
