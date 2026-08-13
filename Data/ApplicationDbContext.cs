using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Autor> Autores { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Libro> Libros { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Libro>()
            .HasOne(libro => libro.Autor)
            .WithMany(autor => autor.Libros)
            .HasForeignKey(libro => libro.AutorId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Libro>()
            .HasOne(libro => libro.Categoria)
            .WithMany(categoria => categoria.Libros)
            .HasForeignKey(libro => libro.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
