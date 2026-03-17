using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GCook.Models;
using GCook.Data;
using Microsoft.EntityFrameworkCore;

namespace GCook.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _db;

    public HomeController(ILogger<HomeController> logger, AppDbContext appDb)
    {
        _logger = logger;
        _db = appDb;
    }

    public IActionResult Index()
    {
        var receitas = _db.Receitas.ToList();
        ViewData["Categorias"] = _db.Categorias.ToList();
        return View(receitas);
    }

    public IActionResult Receita(int id)
    {
       var receita = _db.Receitas
            .Where(r => r.Id == id)
            .Include(r => r.Categoria)
            .Include(r => r.Preparo)
            .Include(r => r.Ingredientes)
            .ThenInclude(ri => ri.Ingrediente)
            .Include(r => r.Usuario)
            .SingleOrDefault();
        return View(receita);    
             
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
