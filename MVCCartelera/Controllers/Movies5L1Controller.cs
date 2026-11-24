using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCartelera.Models;

namespace MVCCartelera.Controllers;

public class Movies5L1Controller : Controller
{
    private readonly Cartelera5L1Context _context;

    public Movies5L1Controller(Cartelera5L1Context context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movies5L1.Include(m => m.Sala).AsNoTracking().ToListAsync();
        return View(movies);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var movie = await _context.Movies5L1.Include(m => m.Sala)
            .AsNoTracking().FirstOrDefaultAsync(m => m.IdMovie == id);
        return movie == null ? NotFound() : View(movie);
    }

    public async Task<IActionResult> Create()
    {
        await LoadRooms();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Movies5L1 movie)
    {
        if (!await _context.Salas5L1.AnyAsync(s => s.IdSala == movie.IdSala))
            ModelState.AddModelError(nameof(movie.IdSala), "Selecciona una sala existente.");

        if (!ModelState.IsValid)
        {
            await LoadRooms(movie.IdSala);
            return View(movie);
        }

        _context.Add(movie);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var movie = await _context.Movies5L1.FindAsync(id);
        if (movie == null) return NotFound();
        await LoadRooms(movie.IdSala);
        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Movies5L1 movie)
    {
        if (id != movie.IdMovie) return NotFound();
        if (!await _context.Salas5L1.AnyAsync(s => s.IdSala == movie.IdSala))
            ModelState.AddModelError(nameof(movie.IdSala), "Selecciona una sala existente.");

        if (!ModelState.IsValid)
        {
            await LoadRooms(movie.IdSala);
            return View(movie);
        }

        _context.Update(movie);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var movie = await _context.Movies5L1.Include(m => m.Sala)
            .AsNoTracking().FirstOrDefaultAsync(m => m.IdMovie == id);
        return movie == null ? NotFound() : View(movie);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var movie = await _context.Movies5L1.FindAsync(id);
        if (movie != null)
        {
            _context.Movies5L1.Remove(movie);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadRooms(int? selected = null)
    {
        var rooms = await _context.Salas5L1.AsNoTracking().OrderBy(s => s.NombreSala).ToListAsync();
        ViewData["IdSala"] = new SelectList(rooms, "IdSala", "NombreSala", selected);
    }
}