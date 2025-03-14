using System.ComponentModel.DataAnnotations;

namespace PerfilesServer.Models
{
  public class Empleado
  {
    [Key]
    public int EmpleadoId { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public required string Nombre { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public required string Apellido { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public required string Dpi { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public required DateTime FechaNacimiento { get; set; }
    public int Edad { get; set; } // Se calcula en SQL
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public required string Sexo { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public required DateTime FechaIngreso { get; set; }
    public int TiempoServicio { get; set; } // Se calcula en SQL
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public required string Direccion { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public required string NIT { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public required int DepartamentoId { get; set; }
    public string Departamento { get; set; }
    public required int EstadoDepartamento { get; set; }
  }
}
