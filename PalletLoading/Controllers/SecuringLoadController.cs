using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PalletLoading.Data;
using PalletLoading.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Controllers
{
    public class SecuringLoadController : MainController
    {

        public SecuringLoadController(PalletLoadingContext context) : base(null, context, null)
        {
        }

        // GET: PalletTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SecuringLoads.ToListAsync());
        }

        // GET: PalletTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securingLoads = await _context.SecuringLoads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securingLoads == null)
            {
                return NotFound();
            }

            return View(securingLoads);
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
        public async Task<IActionResult> Create([Bind("Id,Chingi,Coltare,BareFixare,Absorgel,SaciProtectie")] SecuringLoad securingLoad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(securingLoad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(securingLoad);
        }

        // GET: PalletTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securingLoad = await _context.SecuringLoads.FindAsync(id);
            if (securingLoad == null)
            {
                return NotFound();
            }
            return View(securingLoad);
        }

        // POST: PalletTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Chingi,Coltare,BareFixare,Absorgel,SaciProtectie")] SecuringLoad securingLoad)
        {
            if (id != securingLoad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(securingLoad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecuringLoadExists(securingLoad.Id))
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
            return View(securingLoad);
        }

        // GET: PalletTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securingLoad = await _context.SecuringLoads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securingLoad == null)
            {
                return NotFound();
            }

            return View(securingLoad);
        }

        // POST: PalletTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var securingLoad = await _context.SecuringLoads.FindAsync(id);
            _context.SecuringLoads.Remove(securingLoad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecuringLoadExists(int id)
        {
            return _context.SecuringLoads.Any(e => e.Id == id);
        }
    }
}
