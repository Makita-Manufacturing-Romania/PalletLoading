using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PalletLoading.Data;
using PalletLoading.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Controllers
{
    public class LoadingTypeController : MainController
    {

        public LoadingTypeController(PalletLoadingContext context) : base(null, context, null)
        {
        }

        // GET: PalletTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoadingTypes.ToListAsync());
        }

        // GET: PalletTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loadingTypes = await _context.LoadingTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loadingTypes == null)
            {
                return NotFound();
            }

            return View(loadingTypes);
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
        public async Task<IActionResult> Create([Bind("Id,Abbreviation,Name")] LoadingType loadingType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loadingType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loadingType);
        }

        // GET: PalletTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loadingType = await _context.LoadingTypes.FindAsync(id);
            if (loadingType == null)
            {
                return NotFound();
            }
            return View(loadingType);
        }

        // POST: PalletTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Abbreviation,Name")] LoadingType loadingType)
        {
            if (id != loadingType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loadingType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoadingTypeExists(loadingType.Id))
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
            return View(loadingType);
        }

        // GET: PalletTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loadingType = await _context.LoadingTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loadingType == null)
            {
                return NotFound();
            }

            return View(loadingType);
        }

        // POST: PalletTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loadingType = await _context.LoadingTypes.FindAsync(id);
            _context.LoadingTypes.Remove(loadingType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoadingTypeExists(int id)
        {
            return _context.LoadingTypes.Any(e => e.Id == id);
        }
    }
}
