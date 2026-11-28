using CineCarteleraV2_5L1.Data;
using CineCarteleraV2_5L1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CineCarteleraV2_5L1.Controllers;

public class FuncionesController : Controller
{
    private readonly CineCarteleraContext _context;

    public FuncionesController(CineCarteleraContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var funciones = await _context.Funciones
            .Include(f => f.Pelicula)
            .Include(f => f.Sala)
            .OrderBy(f => f.FechaFuncion)
            .ThenBy(f => f.HoraInicio)
            .ToListAsync();

        return View(funciones);
    }

    public async Task<IActionResult> Create()
    {
        await CargarListasAsync();
        return View(new Funcion());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("IdPelicula,IdSala,FechaFuncion,HoraInicio,Precio,Idioma")]
        Funcion funcion)
    {
        if (ModelState.IsValid)
        {
            bool horarioOcupado = await _context.Funciones.AnyAsync(f =>
                f.IdSala == funcion.IdSala &&
                f.FechaFuncion == funcion.FechaFuncion &&
                f.HoraInicio == funcion.HoraInicio);

            if (horarioOcupado)
            {
                ModelState.AddModelError(string.Empty,
                    "Ya existe una función en esa sala, fecha y hora.");
            }
            else
            {
                _context.Funciones.Add(funcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        await CargarListasAsync(funcion.IdPelicula, funcion.IdSala);
        return View(funcion);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var funcion = await ObtenerFuncionAsync(id.Value);

        if (funcion == null)
            return NotFound();

        return View(funcion);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var funcion = await _context.Funciones.FindAsync(id);

        if (funcion == null)
            return NotFound();

        await CargarListasAsync(funcion.IdPelicula, funcion.IdSala);
        return View(funcion);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("IdFuncion,IdPelicula,IdSala,FechaFuncion,HoraInicio,Precio,Idioma")]
        Funcion funcion)
    {
        if (id != funcion.IdFuncion)
            return NotFound();

        if (ModelState.IsValid)
        {
            bool horarioOcupado = await _context.Funciones.AnyAsync(f =>
                f.IdFuncion != funcion.IdFuncion &&
                f.IdSala == funcion.IdSala &&
                f.FechaFuncion == funcion.FechaFuncion &&
                f.HoraInicio == funcion.HoraInicio);

            if (horarioOcupado)
            {
                ModelState.AddModelError(string.Empty,
                    "Ya existe otra función en esa sala, fecha y hora.");
            }
            else
            {
                _context.Update(funcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        await CargarListasAsync(funcion.IdPelicula, funcion.IdSala);
        return View(funcion);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var funcion = await ObtenerFuncionAsync(id.Value);

        if (funcion == null)
            return NotFound();

        return View(funcion);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var funcion = await _context.Funciones.FindAsync(id);

        if (funcion != null)
        {
            _context.Funciones.Remove(funcion);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<Funcion?> ObtenerFuncionAsync(int id)
    {
        return await _context.Funciones
            .Include(f => f.Pelicula)
            .Include(f => f.Sala)
            .FirstOrDefaultAsync(f => f.IdFuncion == id);
    }

    private async Task CargarListasAsync(int? peliculaSeleccionada = null, int? salaSeleccionada = null)
    {
        var peliculas = await _context.Peliculas
            .OrderBy(p => p.Titulo)
            .ToListAsync();

        var salas = await _context.Salas
            .OrderBy(s => s.NombreSala)
            .ToListAsync();

        ViewData["IdPelicula"] = new SelectList(
            peliculas, "IdPelicula", "Titulo", peliculaSeleccionada);

        ViewData["IdSala"] = new SelectList(
            salas, "IdSala", "NombreSala", salaSeleccionada);
    }
}