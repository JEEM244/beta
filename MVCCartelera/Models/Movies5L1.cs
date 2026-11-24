using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCartelera.Models;

[Table("Movies5L1")]
public class Movies5L1
{
    [Key]
    public int IdMovie { get; set; }

    [Required, StringLength(100)]
    public string Titulo { get; set; } = string.Empty;

    [Required, StringLength(40)]
    public string Genero { get; set; } = string.Empty;

    [Range(1, 500)]
    [Display(Name = "Duración (minutos)")]
    public int DuracionMinutos { get; set; }

    [Required, StringLength(10)]
    public string Clasificacion { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Display(Name = "Fecha de estreno")]
    public DateTime? FechaEstreno { get; set; }

    [StringLength(500)]
    public string? Sinopsis { get; set; }

    [Display(Name = "Sala")]
    public int IdSala { get; set; }

    [ForeignKey(nameof(IdSala))]
    public virtual Salas5L1? Sala { get; set; }
}