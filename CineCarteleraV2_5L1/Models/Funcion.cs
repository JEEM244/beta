using System.ComponentModel.DataAnnotations;

namespace CineCarteleraV2_5L1.Models;

public class Funcion
{
    [Key]
    public int IdFuncion { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecciona una película.")]
    [Display(Name = "Película")]
    public int IdPelicula { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecciona una sala.")]
    [Display(Name = "Sala")]
    public int IdSala { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha")]
    public DateTime FechaFuncion { get; set; } = DateTime.Today;

    [Required]
    [DataType(DataType.Time)]
    [Display(Name = "Hora de inicio")]
    public TimeSpan HoraInicio { get; set; } = new(12, 0, 0);

    [Range(typeof(decimal), "0", "999999.99", ErrorMessage = "El precio no puede ser negativo.")]
    public decimal Precio { get; set; }

    [Required]
    [StringLength(30)]
    public string Idioma { get; set; } = "Español";

    public Pelicula? Pelicula { get; set; }
    public Sala? Sala { get; set; }
}