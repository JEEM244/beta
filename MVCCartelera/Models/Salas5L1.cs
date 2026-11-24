using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCartelera.Models;

[Table("Salas5L1")]
public class Salas5L1
{
    [Key]
    public int IdSala { get; set; }

    [Required, StringLength(40)]
    [Display(Name = "Nombre de la sala")]
    public string NombreSala { get; set; } = string.Empty;

    [Range(1, 1000)]
    public int Capacidad { get; set; }

    [StringLength(30)]
    [Display(Name = "Tipo de sala")]
    public string? TipoSala { get; set; }

    public bool Activa { get; set; } = true;

    public virtual ICollection<Movies5L1> Movies5L1 { get; set; } = new List<Movies5L1>();
}