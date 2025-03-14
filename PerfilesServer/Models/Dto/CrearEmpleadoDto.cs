using System.ComponentModel.DataAnnotations;

namespace PerfilesServer.Models.Dto
{
  public class CrearEmpleadoDto
  {

   
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public required string Nombre { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public required string Apellido { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public required string Dpi { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public required DateTime FechaNacimiento { get; set; }
    
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public required string Sexo { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public required DateTime FechaIngreso { get; set; }
  
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public required string Direccion { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public required string NIT { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public required int DepartamentoId { get; set; }
    
  }
}
