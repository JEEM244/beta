using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCartelera.Models;

namespace MVCCartelera.Controllers;

public class Salas5L1Controller : Controller
{
    private readonly Cartelera5L1Context _context;

    public Salas5L1Controller(Cartelera5L1Context context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var rooms = await _context.Salas5L1.AsNoTracking()
            .Include(s => s.Movies5L1).OrderBy(s => s.NombreSala).ToListAsync();
        return View(rooms);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var room = await _context.Salas5L1.Include(s => s.Movies5L1)
            .AsNoTracking().FirstOrDefaultAsync(s => s.IdSala == id);
        return room == null ? NotFound() : View(room);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Salas5L1 room)
    {
        if (!ModelState.IsValid) return View(room);
        _context.Add(room);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var room = await _context.Salas5L1.FindAsync(id);
        return room == null ? NotFound() : View(room);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Salas5L1 room)
    {
        if (id != room.IdSala) return NotFound();
        if (!ModelState.IsValid) return View(room);
        _context.Update(room);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var room = await _context.Salas5L1.AsNoTracking()
            .FirstOrDefaultAsync(s => s.IdSala == id);
        return room == null ? NotFound() : View(room);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var room = await _context.Salas5L1.FindAsync(id);
        if (room == null) return RedirectToAction(nameof(Index));
        if (await _context.Movies5L1.AnyAsync(m => m.IdSala == id))
        {
            TempData["Error"] = "No se puede eliminar esta sala porque tiene películas asignadas.";
            return RedirectToAction(nameof(Index));
        }
        _context.Salas5L1.Remove(room);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}