using System.ComponentModel.DataAnnotations;

namespace CineCarteleraV2_5L1.Models;

public class Pelicula
{
    [Key]
    public int IdPelicula { get; set; }

    [Required(ErrorMessage = "Escribe el título de la película.")]
    [StringLength(120)]
    [Display(Name = "Título")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Escribe el género.")]
    [StringLength(50)]
    public string Genero { get; set; } = string.Empty;

    [Range(1, 32767, ErrorMessage = "La duración debe ser mayor que cero.")]
    [Display(Name = "Duración (minutos)")]
    public short DuracionMinutos { get; set; }

    [Required]
    [StringLength(10)]
    public string Clasificacion { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Display(Name = "Fecha de estreno")]
    public DateTime? FechaEstreno { get; set; }

    [StringLength(600)]
    public string? Sinopsis { get; set; }

    [Display(Name = "Disponible")]
    public bool Disponible { get; set; } = true;

    public ICollection<Funcion> Funciones { get; set; } = new List<Funcion>();
}