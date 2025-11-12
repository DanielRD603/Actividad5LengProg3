using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Actividad5LengProg3.Data;
using Actividad5LengProg3.Models.Entities;

namespace Actividad5LengProg3.Controllers
{
    public class RecintosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecintosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var recintos = await _context.Recintos.ToListAsync();
            return View(recintos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Recinto model)
        {
            if (ModelState.IsValid)
            {
                _context.Recintos.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        
        public async Task<IActionResult> Edit(int? codigo)
        {
            if (codigo == null)
                return NotFound();

            var recinto = await _context.Recintos.FindAsync(codigo);
            if (recinto == null)
                return NotFound();

            return View(recinto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int codigo, Recinto model)
        {
            if (codigo != model.Codigo)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await RecintoExists(model.Codigo))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int? codigo)
        {
            if (codigo == null)
                return NotFound();

            var recinto = await _context.Recintos
                .Include(r => r.Estudiantes)
                .FirstOrDefaultAsync(r => r.Codigo == codigo);

            if (recinto == null)
                return NotFound();

            return View(recinto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int codigo)
        {
            var recinto = await _context.Recintos
                .Include(r => r.Estudiantes)
                .FirstOrDefaultAsync(r => r.Codigo == codigo);

            if (recinto != null)
            {
                if (recinto.Estudiantes.Any())
                {
                    TempData["Error"] = "No se puede eliminar el recinto porque tiene estudiantes asociados";
                    return RedirectToAction(nameof(Index));
                }

                _context.Recintos.Remove(recinto);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> RecintoExists(int codigo)
        {
            return await _context.Recintos.AnyAsync(e => e.Codigo == codigo);
        }
    }
}