using Microsoft.AspNetCore.Mvc;

namespace CineCarteleraV2_5L1.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}