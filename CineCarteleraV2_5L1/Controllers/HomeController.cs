using CineCarteleraV2_5L1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineCarteleraV2_5L1.Controllers;

public class HomeController : Controller
{
    private readonly CineCarteleraContext _context;

    public HomeController(CineCarteleraContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.TotalPeliculas = await _context.Peliculas.CountAsync();
        ViewBag.TotalSalas = await _context.Salas.CountAsync();
        ViewBag.TotalFunciones = await _context.Funciones.CountAsync();

        var peliculas = await _context.Peliculas
            .Where(p => p.Disponible)
            .OrderBy(p => p.Titulo)
            .Take(4)
            .ToListAsync();

        return View(peliculas);
    }
}