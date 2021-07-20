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
    public class UserRightsController : MainController
    {

        public UserRightsController(PalletLoadingContext context):base(null,context,null)
        {
        }

        // GET: UserRights
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserRight.ToListAsync());
        }

        // GET: UserRights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRight = await _context.UserRight
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRight == null)
            {
                return NotFound();
            }

            return View(userRight);
        }

        // GET: UserRights/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserRights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Right,RightLevel")] UserRight userRight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userRight);
        }

        // GET: UserRights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRight = await _context.UserRight.FindAsync(id);
            if (userRight == null)
            {
                return NotFound();
            }
            return View(userRight);
        }

        // POST: UserRights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Right,RightLevel")] UserRight userRight)
        {
            if (id != userRight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRightExists(userRight.Id))
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
            return View(userRight);
        }

        // GET: UserRights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRight = await _context.UserRight
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRight == null)
            {
                return NotFound();
            }

            return View(userRight);
        }

        // POST: UserRights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userRight = await _context.UserRight.FindAsync(id);
            _context.UserRight.Remove(userRight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRightExists(int id)
        {
            return _context.UserRight.Any(e => e.Id == id);
        }
    }
}
