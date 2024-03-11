using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PalletLoading.Data;
using PalletLoading.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Controllers
{
    public class ClientTypeController : MainController
    {

        public ClientTypeController(PalletLoadingContext context) : base(null, context, null)
        {
        }

        // GET: PalletTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClientTypes.ToListAsync());
        }

        // GET: PalletTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formDefinition = await _context.ClientTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formDefinition == null)
            {
                return NotFound();
            }

            return View(formDefinition);
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
        public async Task<IActionResult> Create([Bind("Id,Name")] ClientType clientType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientType);
        }

        // GET: PalletTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientType = await _context.ClientTypes.FindAsync(id);
            if (clientType == null)
            {
                return NotFound();
            }
            return View(clientType);
        }

        // POST: PalletTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ClientType clientType)
        {
            if (id != clientType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientTypeExists(clientType.Id))
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
            return View(clientType);
        }

        // GET: PalletTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientType = await _context.ClientTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientType == null)
            {
                return NotFound();
            }

            return View(clientType);
        }

        // POST: PalletTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formDefinition = await _context.ClientTypes.FindAsync(id);
            _context.ClientTypes.Remove(formDefinition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientTypeExists(int id)
        {
            return _context.ClientTypes.Any(e => e.Id == id);
        }
    }
}
