using CineCarteleraV2_5L1.Data;
using CineCarteleraV2_5L1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineCarteleraV2_5L1.Controllers;

public class PeliculasController : Controller
{
    private readonly CineCarteleraContext _context;

    public PeliculasController(CineCarteleraContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var peliculas = await _context.Peliculas
            .OrderBy(p => p.Titulo)
            .ToListAsync();

        return View(peliculas);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Titulo,Genero,DuracionMinutos,Clasificacion,FechaEstreno,Sinopsis,Disponible")]
        Pelicula pelicula)
    {
        if (ModelState.IsValid)
        {
            _context.Peliculas.Add(pelicula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(pelicula);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var pelicula = await _context.Peliculas
            .FirstOrDefaultAsync(p => p.IdPelicula == id);

        if (pelicula == null)
            return NotFound();

        return View(pelicula);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var pelicula = await _context.Peliculas.FindAsync(id);

        if (pelicula == null)
            return NotFound();

        return View(pelicula);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("IdPelicula,Titulo,Genero,DuracionMinutos,Clasificacion,FechaEstreno,Sinopsis,Disponible")]
        Pelicula pelicula)
    {
        if (id != pelicula.IdPelicula)
            return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(pelicula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(pelicula);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var pelicula = await _context.Peliculas
            .FirstOrDefaultAsync(p => p.IdPelicula == id);

        if (pelicula == null)
            return NotFound();

        return View(pelicula);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var pelicula = await _context.Peliculas.FindAsync(id);

        if (pelicula == null)
            return RedirectToAction(nameof(Index));

        bool tieneFunciones = await _context.Funciones
            .AnyAsync(f => f.IdPelicula == id);

        if (tieneFunciones)
        {
            TempData["Error"] = "No se puede eliminar esta película porque tiene funciones registradas.";
            return RedirectToAction(nameof(Index));
        }

        _context.Peliculas.Remove(pelicula);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}