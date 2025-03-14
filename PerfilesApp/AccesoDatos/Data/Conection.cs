using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Data.SqlClient;


namespace Perfiles.AccesoDatos.Data
{
  public class Conection
  {
    public static string ConectionString = ConfigurationManager.ConnectionStrings["PerfilesDB"].ToString();

    public T ExecuteQuery<T>(string query, Func<SqlDataReader, T> mapper)
    {
      using (SqlConnection connection = new SqlConnection(ConectionString))
      {
        connection.Open();
        using (SqlCommand command = new SqlCommand(query, connection))
        {
          using (SqlDataReader reader = command.ExecuteReader())
          {
            return mapper(reader);
          }
        }
      }
    }

    public T ExecuteQuery<T>(
      string query, Func<SqlDataReader,
      T> mapper,
      params SqlParameter[] parameters
    )
    {
      using (SqlConnection connection = new SqlConnection(ConectionString))
      {
        connection.Open();
        using (SqlCommand command = new SqlCommand(query, connection))
        {
          if (parameters != null && parameters.Length > 0)
          {
            command.Parameters.AddRange(parameters);
          }
          using (SqlDataReader reader = command.ExecuteReader())
          {
            return mapper(reader);
          }
        }
      }
    }

    public void ExecuteNonQuery(string query, params SqlParameter[] parameters)
    {
      using (SqlConnection connection = new SqlConnection(ConectionString))
      {
        connection.Open();
        using (SqlCommand cmd = new SqlCommand(query, connection))
        {
          // Agregar parámetros si los hay
          if (parameters != null && parameters.Length > 0)
          {
            cmd.Parameters.AddRange(parameters);
          }

          // Ejecutar la consulta
          cmd.ExecuteNonQuery();
        }
      }
    }
  }
}
