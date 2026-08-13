using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Controllers;

public class LibrosController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index() => View(await context.Libros.Include(x => x.Autor).Include(x => x.Categoria).AsNoTracking().ToListAsync());

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var libro = await context.Libros.Include(x => x.Autor).Include(x => x.Categoria).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return libro is null ? NotFound() : View(libro);
    }

    public IActionResult Create()
    {
        CargarListas();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Titulo,FechaPublicacion,AutorId,CategoriaId")] Libro libro)
    {
        if (ModelState.IsValid)
        {
            context.Add(libro);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        CargarListas(libro.AutorId, libro.CategoriaId);
        return View(libro);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var libro = await context.Libros.FindAsync(id);
        if (libro is null) return NotFound();
        CargarListas(libro.AutorId, libro.CategoriaId);
        return View(libro);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,FechaPublicacion,AutorId,CategoriaId")] Libro libro)
    {
        if (id != libro.Id) return NotFound();
        if (ModelState.IsValid)
        {
            context.Update(libro);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        CargarListas(libro.AutorId, libro.CategoriaId);
        return View(libro);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var libro = await context.Libros.Include(x => x.Autor).Include(x => x.Categoria).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return libro is null ? NotFound() : View(libro);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var libro = await context.Libros.FindAsync(id);
        if (libro is not null)
        {
            context.Libros.Remove(libro);
            await context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private void CargarListas(int? autorId = null, int? categoriaId = null)
    {
        ViewData["AutorId"] = new SelectList(context.Autores.OrderBy(x => x.Apellido).ThenBy(x => x.Nombre).Select(x => new { x.Id, NombreCompleto = x.Nombre + " " + x.Apellido }), "Id", "NombreCompleto", autorId);
        ViewData["CategoriaId"] = new SelectList(context.Categorias.OrderBy(x => x.Nombre), "Id", "Nombre", categoriaId);
    }
}
