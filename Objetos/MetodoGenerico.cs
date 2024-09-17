using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace Objetos
{
    public static class MetodoGenerico
    {
        static string _cadenaconeixon = "Data Source=.;Initial Catalog=sistemaviajes;Integrated Security=True";
        public static List<T> ListarDatos<T>(string nombreProcedimiento, Dictionary<string,object> parametros)
            where T : new()
        {
            List<T> entidades = new List<T>();
            using(SqlConnection cn = new SqlConnection(_cadenaconeixon))
            {
                SqlCommand cmd = new SqlCommand(nombreProcedimiento, cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                foreach(var param in parametros)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }
                cn.Open();

                using(SqlDataReader lector = cmd.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        T entidad = new T();
                        var propiedades = typeof(T).GetProperties();

                        foreach(var propiedad in propiedades)
                        {
                            if(lector.HasColumn(propiedad.Name) 
                                && !lector.IsDBNull(lector.GetOrdinal(propiedad.Name))
                                )

                            {
                                //asignar valores a los registros de la entidad, datos obtener a traves de un banco 
                                propiedad.SetValue(entidad, lector[propiedad.Name]);
                                
                            }
                        }
                        entidades.Add(entidad);
                    }
                }
            }
            return entidades;

        }

        

        
        public static int Insertar<T>(T entity, string connectionString, string storedProcedureName)
        {
            using (SqlConnection connection = new SqlConnection(_cadenaconeixon))
            {
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Obtener las propiedades de la entidad
                    PropertyInfo[] properties = typeof(T).GetProperties();

                    // Agregar parámetros al comando
                    foreach (PropertyInfo property in properties)
                    {
                        SqlParameter parameter = new SqlParameter
                        {
                            ParameterName = "@" + property.Name,
                            Value = property.GetValue(entity)
                        };
                        command.Parameters.Add(parameter);
                    }

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }
            
        
        public static int Insertar(Dictionary<string, object> parametros, string nombreProcedimiento )
        {
            using (SqlConnection connection = new SqlConnection(_cadenaconeixon))
            {
                using (SqlCommand cmd = new SqlCommand(nombreProcedimiento, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach(var parametro in parametros)
                    {
                        cmd.Parameters.AddWithValue(parametro.Key, parametro.Value);
                    }
                    connection.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas;
                }
            }
        }
        public static int Actualizar(Dictionary<string, object> parametros, string nombreProcedimiento)
        {
            using (SqlConnection connection = new SqlConnection(_cadenaconeixon))
            {
                using (SqlCommand cmd = new SqlCommand(nombreProcedimiento, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var parametro in parametros)
                    {
                        cmd.Parameters.AddWithValue(parametro.Key, parametro.Value);
                    }
                    connection.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas;
                }
            }
        }
        public static int Eliminar(Dictionary<string, object> parametros, string nombreProcedimiento)
        {
            using (SqlConnection connection = new SqlConnection(_cadenaconeixon))
            {
                using (SqlCommand cmd = new SqlCommand(nombreProcedimiento, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var parametro in parametros)
                    {
                        cmd.Parameters.AddWithValue(parametro.Key, parametro.Value);
                    }
                    connection.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas;
                }
            }
        }

    }
}
public static class SqlDataReaderExtensions
{
    public static bool HasColumn(this SqlDataReader reader, string columnName)
    {
        for (int i = 0; i < reader.FieldCount; i++)
        {
            if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
}