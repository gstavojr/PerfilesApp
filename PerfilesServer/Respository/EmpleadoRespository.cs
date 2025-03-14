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
    public bool DeleteEmpleado(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<Empleado> GetEmpleado(int id)
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
        return empleado;
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public async Task<IEnumerable<Empleado>> GetEmpleados()
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
        return empleados;
      }
      catch (Exception ex)
      {
        return [];
      }

    }

    public async Task<bool> SaveEmpleado(Empleado empleado)
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
          new SqlParameter("@Direccion", empleado.Direccion),
          new SqlParameter("@NIT", empleado.NIT),
          new SqlParameter("@DepartamentoId", empleado.DepartamentoId),
        };
        string query = "EXEC sp_SaveEmpleado @EmpleadoId, @Nombre, @Apellido, @Dpi, @FechaNacimiento, @SEXO, @FechaIngreso, @Direccion, @NIT, @DepartamentoId";

        await this._conection.ExecuteNonQueryAsync(query, parameters);

        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }
  }
}
