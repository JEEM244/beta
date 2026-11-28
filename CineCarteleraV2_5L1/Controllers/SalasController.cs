using CineCarteleraV2_5L1.Data;
using CineCarteleraV2_5L1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineCarteleraV2_5L1.Controllers;

public class SalasController : Controller
{
    private readonly CineCarteleraContext _context;

    public SalasController(CineCarteleraContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var salas = await _context.Salas
            .OrderBy(s => s.NombreSala)
            .ToListAsync();

        return View(salas);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("NombreSala,Capacidad,FormatoSala,Activa")] Sala sala)
    {
        if (ModelState.IsValid)
        {
            _context.Salas.Add(sala);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(sala);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var sala = await _context.Salas
            .FirstOrDefaultAsync(s => s.IdSala == id);

        if (sala == null)
            return NotFound();

        return View(sala);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var sala = await _context.Salas.FindAsync(id);

        if (sala == null)
            return NotFound();

        return View(sala);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("IdSala,NombreSala,Capacidad,FormatoSala,Activa")] Sala sala)
    {
        if (id != sala.IdSala)
            return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(sala);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(sala);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var sala = await _context.Salas
            .FirstOrDefaultAsync(s => s.IdSala == id);

        if (sala == null)
            return NotFound();

        return View(sala);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var sala = await _context.Salas.FindAsync(id);

        if (sala == null)
            return RedirectToAction(nameof(Index));

        bool tieneFunciones = await _context.Funciones
            .AnyAsync(f => f.IdSala == id);

        if (tieneFunciones)
        {
            TempData["Error"] = "No se puede eliminar esta sala porque tiene funciones registradas.";
            return RedirectToAction(nameof(Index));
        }

        _context.Salas.Remove(sala);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}