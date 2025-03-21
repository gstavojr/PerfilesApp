using PerfilesServer.Models;

namespace PerfilesServer.Respository.IRepository
{
  public interface IEmpleadoRespository
  {
    Task<ResponseApi<IEnumerable<Empleado>>> GetEmpleados();
    Task<ResponseApi<Empleado>> GetEmpleado(int id);
    Task<ResponseApi<bool>> SaveEmpleado(Empleado empleado);
    Task<ResponseApi<bool>> DeleteEmpleado(int id);

  }
}
