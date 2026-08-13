using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Controllers;

public class AutoresController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index() => View(await context.Autores.AsNoTracking().ToListAsync());

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var autor = await context.Autores.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return autor is null ? NotFound() : View(autor);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido")] Autor autor)
    {
        if (!ModelState.IsValid) return View(autor);
        context.Add(autor);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var autor = await context.Autores.FindAsync(id);
        return autor is null ? NotFound() : View(autor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido")] Autor autor)
    {
        if (id != autor.Id) return NotFound();
        if (!ModelState.IsValid) return View(autor);
        context.Update(autor);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var autor = await context.Autores.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return autor is null ? NotFound() : View(autor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var autor = await context.Autores.FindAsync(id);
        if (autor is not null)
        {
            context.Autores.Remove(autor);
            await context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
