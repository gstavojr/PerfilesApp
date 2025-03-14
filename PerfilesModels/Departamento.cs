using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfilesModels
{
  public class Departamento
  {
    [Key]
    public int DepartamentoId { get; set; }
    public required string Nombre { get; set; }
    public required int Estado { get; set; }
  }
}
