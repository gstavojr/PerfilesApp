using PerfilesServer.Models;

namespace PerfilesServer.Respository.IRepository
{
  public interface IEmpleadoRespository
  {
    Task<IEnumerable<Empleado>> GetEmpleados();
    Task<Empleado> GetEmpleado(int id);
    Task<bool> SaveEmpleado(Empleado empleado);
    bool DeleteEmpleado(int id);

  }
}
