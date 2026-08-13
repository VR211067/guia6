using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Libro
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El título es obligatorio")]
    [StringLength(200)]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de publicación es obligatoria")]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de publicación")]
    public DateTime FechaPublicacion { get; set; }

    [Display(Name = "Autor")]
    public int AutorId { get; set; }

    [Display(Name = "Categoría")]
    public int CategoriaId { get; set; }

    public Autor? Autor { get; set; }
    public Categoria? Categoria { get; set; }
}
