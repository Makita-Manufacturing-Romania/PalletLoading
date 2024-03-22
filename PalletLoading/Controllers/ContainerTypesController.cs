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
        public async Task<IActionResult> Create(ContainerType containerType)
        {
            if (ModelState.IsValid)
            {
                if (containerType.Length > 0 && containerType.Width > 0 && containerType.Height > 0)
                {
                    containerType.volume = (containerType.Length * containerType.Width * containerType.Height) / 1000000;
                }
                else
                {
                    containerType.volume = 0;
                }

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
        public async Task<IActionResult> Edit(int id, ContainerType containerType)
        {
            if (id != containerType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (containerType.Length > 0 && containerType.Width > 0 && containerType.Height > 0)
                    {
                        containerType.volume = (containerType.Length * containerType.Width * containerType.Height) /1000000;
                    }
                    else
                    {
                        containerType.volume = 0;
                    }

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
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var containerType = await _context.ContainerTypes.FindAsync(id);
            var containersLinked = _context.Containers
                .Where(l => l.TypeId == id)
                .FirstOrDefault();

            var jsonData = new Dictionary<string, string>();
            if (containersLinked == null)
            {
                _context.ContainerTypes.Remove(containerType);
                await _context.SaveChangesAsync();
                jsonData["message"] = "_";
            }
            else
            {
                jsonData["message"] = "Cannot delete Type because it's linked to created Containers";
            }
            return Json(jsonData);
            //return RedirectToAction(nameof(Index));
        }

        private bool ContainerTypeExists(int id)
        {
            return _context.ContainerTypes.Any(e => e.Id == id);
        }

    }
}
