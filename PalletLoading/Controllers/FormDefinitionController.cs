using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PalletLoading.Data;
using PalletLoading.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Controllers
{
    public class FormDefinitionController : MainController
    {

        public FormDefinitionController(PalletLoadingContext context) : base(null, context, null)
        {
        }

        // GET: PalletTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.FormDefinitions.ToListAsync());
        }

        // GET: PalletTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formDefinition = await _context.FormDefinitions
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
        public async Task<IActionResult> Create([Bind("Id,PDF_Name,DocRefNumber,Department")] FormDefinition formDefinition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formDefinition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formDefinition);
        }

        // GET: PalletTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formDefinition = await _context.FormDefinitions.FindAsync(id);
            if (formDefinition == null)
            {
                return NotFound();
            }
            return View(formDefinition);
        }

        // POST: PalletTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PDF_Name,DocRefNumber,Department")] FormDefinition formDefinition)
        {
            if (id != formDefinition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formDefinition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormDefinitionExists(formDefinition.Id))
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
            return View(formDefinition);
        }

        // GET: PalletTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formDefinition = await _context.FormDefinitions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formDefinition == null)
            {
                return NotFound();
            }

            return View(formDefinition);
        }

        // POST: PalletTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formDefinition = await _context.FormDefinitions.FindAsync(id);
            _context.FormDefinitions.Remove(formDefinition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormDefinitionExists(int id)
        {
            return _context.FormDefinitions.Any(e => e.Id == id);
        }
    }
}
