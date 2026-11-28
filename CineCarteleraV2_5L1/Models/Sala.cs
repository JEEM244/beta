using System.ComponentModel.DataAnnotations;

namespace CineCarteleraV2_5L1.Models;

public class Sala
{
    [Key]
    public int IdSala { get; set; }

    [Required(ErrorMessage = "Escribe el nombre de la sala.")]
    [StringLength(40)]
    [Display(Name = "Nombre de la sala")]
    public string NombreSala { get; set; } = string.Empty;

    [Range(1, 32767, ErrorMessage = "La capacidad debe ser mayor que cero.")]
    public short Capacidad { get; set; }

    [Required]
    [StringLength(30)]
    [Display(Name = "Formato de sala")]
    public string FormatoSala { get; set; } = string.Empty;

    public bool Activa { get; set; } = true;

    public ICollection<Funcion> Funciones { get; set; } = new List<Funcion>();
}