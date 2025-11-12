using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Actividad5LengProg3.Data;
using Actividad5LengProg3.Models.Entities;

namespace Actividad5LengProg3.Controllers
{
    public class CarrerasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarrerasController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            var carreras = await _context.Carreras.ToListAsync();
            return View(carreras);
        }

        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Carrera model)
        {
            if (ModelState.IsValid)
            {
                _context.Carreras.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        
        public async Task<IActionResult> Edit(int? codigo)
        {
            if (codigo == null)
                return NotFound();

            var carrera = await _context.Carreras.FindAsync(codigo);
            if (carrera == null)
                return NotFound();

            return View(carrera);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int codigo, Carrera model)
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
                    if (!await CarreraExists(model.Codigo))
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

            var carrera = await _context.Carreras
                .Include(c => c.Estudiantes)
                .FirstOrDefaultAsync(c => c.Codigo == codigo);

            if (carrera == null)
                return NotFound();

            return View(carrera);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int codigo)
        {
            var carrera = await _context.Carreras
                .Include(c => c.Estudiantes)
                .FirstOrDefaultAsync(c => c.Codigo == codigo);

            if (carrera != null)
            {
                if (carrera.Estudiantes.Any())
                {
                    TempData["Error"] = "No se puede eliminar la carrera porque tiene estudiantes asociados";
                    return RedirectToAction(nameof(Index));
                }

                _context.Carreras.Remove(carrera);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarreraExists(int codigo)
        {
            return await _context.Carreras.AnyAsync(e => e.Codigo == codigo);
        }
    }
}