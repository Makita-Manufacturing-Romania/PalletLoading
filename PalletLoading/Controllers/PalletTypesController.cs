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
    public class PalletTypesController : Controller
    {
        private readonly PalletLoadingContext _context;

        public PalletTypesController(PalletLoadingContext context)
        {
            _context = context;
        }

        // GET: PalletTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PalletTypes.ToListAsync());
        }

        // GET: PalletTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var palletType = await _context.PalletTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (palletType == null)
            {
                return NotFound();
            }

            return View(palletType);
        }

        // GET: PalletTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PalletTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Length,Width,Height")] PalletType palletType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(palletType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(palletType);
        }

        // GET: PalletTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var palletType = await _context.PalletTypes.FindAsync(id);
            if (palletType == null)
            {
                return NotFound();
            }
            return View(palletType);
        }

        // POST: PalletTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Length,Width,Height")] PalletType palletType)
        {
            if (id != palletType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(palletType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PalletTypeExists(palletType.Id))
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
            return View(palletType);
        }

        // GET: PalletTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var palletType = await _context.PalletTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (palletType == null)
            {
                return NotFound();
            }

            return View(palletType);
        }

        // POST: PalletTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var palletType = await _context.PalletTypes.FindAsync(id);
            _context.PalletTypes.Remove(palletType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PalletTypeExists(int id)
        {
            return _context.PalletTypes.Any(e => e.Id == id);
        }
    }
}
