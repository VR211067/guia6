using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    public ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
