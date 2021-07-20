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
    public class ContainerTypesController : MainController
    {

        public ContainerTypesController(PalletLoadingContext context):base(null,context,null)
        {
        }

        // GET: ContainerTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContainerTypes.ToListAsync());
        }

        // GET: ContainerTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var containerType = await _context.ContainerTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (containerType == null)
            {
                return NotFound();
            }

            return View(containerType);
        }

        // GET: ContainerTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContainerTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Length,Width,Height")] ContainerType containerType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(containerType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(containerType);
        }

        // GET: ContainerTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var containerType = await _context.ContainerTypes.FindAsync(id);
            if (containerType == null)
            {
                return NotFound();
            }
            return View(containerType);
        }

        // POST: ContainerTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Length,Width,Height")] ContainerType containerType)
        {
            if (id != containerType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(containerType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContainerTypeExists(containerType.Id))
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
            return View(containerType);
        }

        // GET: ContainerTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var containerType = await _context.ContainerTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (containerType == null)
            {
                return NotFound();
            }

            return View(containerType);
        }

        // POST: ContainerTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var containerType = await _context.ContainerTypes.FindAsync(id);
            _context.ContainerTypes.Remove(containerType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContainerTypeExists(int id)
        {
            return _context.ContainerTypes.Any(e => e.Id == id);
        }
    }
}
