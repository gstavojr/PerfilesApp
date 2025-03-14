using AutoMapper;
using PerfilesServer.Models;
using PerfilesServer.Models.Dto;

namespace PerfilesServer.Mapper
{
  public class PerfilesMapper: Profile
  {
    public PerfilesMapper()
    {
      CreateMap<Empleado, CrearEmpleadoDto>().ReverseMap();
      CreateMap<Empleado, ActualizarEmpleadoDto>().ReverseMap();
    }
  }
}
