using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Actividad5LengProg3.Data;
using Actividad5LengProg3.Models.Entities;
using Actividad5LengProg3.Models.ViewModels;

namespace Actividad5LengProg3.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstudiantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task CargarListasAsync()
        {
            ViewBag.Carreras = new SelectList(
                await _context.Carreras.OrderBy(c => c.Nombre).ToListAsync(),
                "Codigo",
                "Nombre"
            );
            ViewBag.Recintos = new SelectList(
                await _context.Recintos.OrderBy(r => r.Nombre).ToListAsync(),
                "Codigo",
                "Nombre"
            );
        }

        public async Task<IActionResult> Index()
        {
            await CargarListasAsync();
            return View(new EstudianteViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(EstudianteViewModel model)
        {
            await CargarListasAsync();

            if (await _context.Estudiantes.AnyAsync(e => e.Matricula == model.Matricula))
            {
                ModelState.AddModelError(nameof(model.Matricula), "Ya existe un estudiante con esa matrícula.");
            }

            if (await _context.Estudiantes.AnyAsync(e => e.CorreoInstitucional == model.CorreoInstitucional))
            {
                ModelState.AddModelError(nameof(model.CorreoInstitucional), "Ya existe un estudiante con ese correo institucional.");
            }

            if (!ModelState.IsValid)
                return View("Index", model);

            var estudiante = new Estudiante
            {
                Matricula = model.Matricula.Trim(),
                NombreCompleto = model.NombreCompleto.Trim(),
                CarreraId = model.CarreraId,
                RecintoId = model.RecintoId,
                CorreoInstitucional = model.CorreoInstitucional.Trim(),
                Celular = model.Celular.Trim(),
                Telefono = model.Telefono.Trim(),
                Direccion = model.Direccion.Trim(),
                FechaNacimiento = model.FechaNacimiento,
                Genero = model.Genero,
                TurnoEstudiante = model.Turno,
                Becado = model.Becado,
                PorcentajeBeca = model.PorcentajeBeca
            };

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Lista));
        }

        public async Task<IActionResult> Lista()
        {
            var estudiantes = await _context.Estudiantes
                .Include(e => e.Carrera)
                .Include(e => e.Recinto)
                .OrderBy(e => e.Matricula)
                .ToListAsync();

            return View(estudiantes);
        }

        public async Task<IActionResult> Editar(string matricula)
        {
            if (string.IsNullOrWhiteSpace(matricula))
                return NotFound();

            var estudiante = await _context.Estudiantes
                .Include(e => e.Carrera)
                .Include(e => e.Recinto)
                .FirstOrDefaultAsync(e => e.Matricula == matricula);

            if (estudiante == null)
                return NotFound();

            await CargarListasAsync();

            var viewModel = new EstudianteViewModel
            {
                Matricula = estudiante.Matricula,
                NombreCompleto = estudiante.NombreCompleto,
                CarreraId = estudiante.CarreraId,
                RecintoId = estudiante.RecintoId,
                CorreoInstitucional = estudiante.CorreoInstitucional,
                Celular = estudiante.Celular,
                Telefono = estudiante.Telefono,
                Direccion = estudiante.Direccion,
                FechaNacimiento = estudiante.FechaNacimiento,
                Genero = estudiante.Genero,
                Turno = estudiante.TurnoEstudiante,
                Becado = estudiante.Becado,
                PorcentajeBeca = estudiante.PorcentajeBeca
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(EstudianteViewModel model)
        {
            await CargarListasAsync();

            if (!ModelState.IsValid)
                return View(model);

            var estudiante = await _context.Estudiantes.FindAsync(model.Matricula);
            if (estudiante == null)
                return NotFound();

            // Verificar correo unico
            if (await _context.Estudiantes.AnyAsync(e =>
                e.CorreoInstitucional == model.CorreoInstitucional &&
                e.Matricula != model.Matricula))
            {
                ModelState.AddModelError(nameof(model.CorreoInstitucional), "Ya existe otro estudiante con ese correo institucional.");
                return View(model);
            }

            estudiante.NombreCompleto = model.NombreCompleto.Trim();
            estudiante.CarreraId = model.CarreraId;
            estudiante.RecintoId = model.RecintoId;
            estudiante.CorreoInstitucional = model.CorreoInstitucional.Trim();
            estudiante.Celular = model.Celular.Trim();
            estudiante.Telefono = model.Telefono.Trim();
            estudiante.Direccion = model.Direccion.Trim();
            estudiante.FechaNacimiento = model.FechaNacimiento;
            estudiante.Genero = model.Genero;
            estudiante.TurnoEstudiante = model.Turno;
            estudiante.Becado = model.Becado;
            estudiante.PorcentajeBeca = model.PorcentajeBeca;

            _context.Update(estudiante);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Lista));
        }

        public async Task<IActionResult> Eliminar(string matricula)
        {
            if (string.IsNullOrWhiteSpace(matricula))
                return NotFound();

            var estudiante = await _context.Estudiantes.FindAsync(matricula);
            if (estudiante != null)
            {
                _context.Estudiantes.Remove(estudiante);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Lista));
        }
    }
}