using System.Runtime.Intrinsics.Arm;
using Microsoft.Data.SqlClient;
using Perfiles.AccesoDatos.Data;
using PerfilesServer.Models;
using PerfilesServer.Respository.IRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PerfilesServer.Respository
{
  public class EmpleadoRespository : IEmpleadoRespository
  {
    private readonly Conection _conection;
    public EmpleadoRespository(Conection conection)
    {
      _conection = conection;
    }
    public async Task<ResponseApi<bool>> DeleteEmpleado(int id)
    {
      try
      {
        var searchempleado = await this.GetEmpleado(id);
        if (!searchempleado.IsSucces)
        {
          return new ResponseApi<bool>(false, "Empleado no encontrado", false);
        }


        SqlParameter[] parameters = { new SqlParameter("@EmpleadoId", id) };
        await this._conection.ExecuteNonQueryAsync("DELETE FROM Empleado WHERE EmpleadoId = @EmpleadoId", parameters);
        return new ResponseApi<bool>(true, "Empleado eliminado correctamente", true);
      } catch( Exception ex )
      {
        return new ResponseApi<bool>(false, $"Error al eliminar empleado ${ex.Message}", false);
      }
    }

    public async Task<ResponseApi<Empleado>> GetEmpleado(int id)
    {
      try
      {
        Empleado? empleado = await this._conection.ExecuteQueryAsync("EXEC sp_GetEmpleadoById @EmpleadoId", reader =>
        {
          if (reader.Read())
          {
            return new Empleado
            {
              EmpleadoId = (int)reader["EmpleadoId"],
              Nombre = reader["Nombre"]?.ToString() ?? string.Empty,
              Apellido = reader["Apellido"]?.ToString() ?? string.Empty,
              Dpi = reader["Dpi"]?.ToString() ?? string.Empty,
              FechaNacimiento = (DateTime)reader["FechaNacimiento"],
              Edad = (int)reader["Edad"],
              Sexo = reader["SEXO"]?.ToString() ?? string.Empty,
              FechaIngreso = (DateTime)reader["FechaIngreso"],
              TiempoServicio = (int)reader["TiempoServicio"],
              Direccion = reader["Direccion"]?.ToString() ?? string.Empty,
              NIT = reader["NIT"]?.ToString() ?? string.Empty,
              DepartamentoId = (int)reader["DepartamentoId"],
              Departamento = reader["Departamento"]?.ToString() ?? string.Empty,
              EstadoDepartamento = (int)reader["EstadoDepartamento"]
            };
          }
          return null;
        }, new SqlParameter("@EmpleadoId", id));
        if (empleado == null)
        {

          return new  ResponseApi<Empleado>(false, "Empleado no encontrado", null);
        }
        return new ResponseApi<Empleado>(true, "Empleado encontrado", empleado);
      }
      catch (Exception ex)
      {
        return new ResponseApi<Empleado>(false, $"Error al obtener empleado ${ex.Message}", null);
      }
    }

    public async Task<ResponseApi<IEnumerable<Empleado>>> GetEmpleados()
    {
      IEnumerable<Empleado> empleados = [];

      try
      {
        empleados = await this._conection.ExecuteQueryAsync("EXEC sp_GetEmpleados", reader =>
        {
          ICollection<Empleado> empleadosTemp = [];
          while (reader.Read())
          {
            empleadosTemp.Add(new Empleado
            {
              EmpleadoId = (int)reader["EmpleadoId"],
              Nombre = reader["Nombre"]?.ToString() ?? string.Empty,
              Apellido = reader["Apellido"]?.ToString() ?? string.Empty,
              Dpi = reader["Dpi"]?.ToString() ?? string.Empty,
              FechaNacimiento = (DateTime)reader["FechaNacimiento"],
              Edad = (int)reader["Edad"],
              Sexo = reader["SEXO"]?.ToString() ?? string.Empty,
              FechaIngreso = (DateTime)reader["FechaIngreso"],
              TiempoServicio = (int)reader["TiempoServicio"],
              Direccion = reader["Direccion"]?.ToString() ?? string.Empty,
              NIT = reader["NIT"]?.ToString() ?? string.Empty,
              DepartamentoId = (int)reader["DepartamentoId"],
              Departamento = reader["Departamento"]?.ToString() ?? string.Empty,
              EstadoDepartamento = (int)reader["EstadoDepartamento"]
            });
          }
          return empleadosTemp;
        });
        return new ResponseApi<IEnumerable<Empleado>>(true, "Empleados encontrados", empleados);
      }
      catch (Exception ex)
      {
        return new ResponseApi<IEnumerable<Empleado>>(false, $"Error al obtener empleados ${ex.Message}", empleados);
      }

    }

    public async Task<ResponseApi<bool>> SaveEmpleado(Empleado empleado)
    {

      try
      {
        SqlParameter[] parameters =
        {
          new SqlParameter("@EmpleadoId", empleado.EmpleadoId),
          new SqlParameter("@Nombre", empleado.Nombre),
          new SqlParameter("@Apellido", empleado.Apellido),
          new SqlParameter("@Dpi", empleado.Dpi),
          new SqlParameter("@FechaNacimiento", empleado.FechaNacimiento),
          new SqlParameter("@SEXO", empleado.Sexo),
          new SqlParameter("@FechaIngreso", empleado.FechaIngreso),
          new SqlParameter("@Direccion", empleado.Direccion != null && empleado.Direccion != "" ? empleado.Direccion :  DBNull.Value),
          new SqlParameter("@NIT", empleado.NIT != null && empleado.NIT != "" ? empleado.NIT:  DBNull.Value),
          new SqlParameter("@DepartamentoId", empleado.DepartamentoId),
        };
        string query = "EXEC sp_SaveEmpleado @EmpleadoId, @Nombre, @Apellido, @Dpi, @FechaNacimiento, @SEXO, @FechaIngreso, @Direccion, @NIT, @DepartamentoId";

        await this._conection.ExecuteNonQueryAsync(query, parameters);

        return new ResponseApi<bool>(true, "Empleado guardado correctamente");
      }
      catch (Exception ex)
      {
        return new ResponseApi<bool>(false, $"Error al guardar empleado ${ex.Message}", true);
      }

    }
  }
}
