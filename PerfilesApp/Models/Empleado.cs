using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfilesApp.Models
{
	public class Empleado
	{
    [Key]
    public int EmpleadoId { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public string Nombre { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public string Apellido { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public string Dpi { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public DateTime FechaNacimiento { get; set; }
    public int Edad { get; set; } // Se calcula en SQL
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public string Sexo { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public DateTime FechaIngreso { get; set; }
    public int TiempoServicio { get; set; } // Se calcula en SQL
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public string Direccion { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public string NIT { get; set; }
    [Required(ErrorMessage = "El {0} es obligatorio")]
    public int DepartamentoId { get; set; }
    public string Departamento { get; set; }
    public int EstadoDepartamento { get; set; }

    public string EstadoNombreDepartamento { get; set; }
  }
}