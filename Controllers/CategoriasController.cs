using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Controllers;

public class CategoriasController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index() => View(await context.Categorias.AsNoTracking().ToListAsync());

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var categoria = await context.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return categoria is null ? NotFound() : View(categoria);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre")] Categoria categoria)
    {
        if (!ModelState.IsValid) return View(categoria);
        context.Add(categoria);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var categoria = await context.Categorias.FindAsync(id);
        return categoria is null ? NotFound() : View(categoria);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Categoria categoria)
    {
        if (id != categoria.Id) return NotFound();
        if (!ModelState.IsValid) return View(categoria);
        context.Update(categoria);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var categoria = await context.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return categoria is null ? NotFound() : View(categoria);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var categoria = await context.Categorias.FindAsync(id);
        if (categoria is not null)
        {
            context.Categorias.Remove(categoria);
            await context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
