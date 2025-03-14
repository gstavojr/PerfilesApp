using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfilesApp.Models
{
  public class Departamento
  {
    [Key]
    public int DepartamentoId { get; set; }
    public  string Nombre { get; set; }
    public  int Estado { get; set; }

    public string NombreEstado { get; set; }
  }
}