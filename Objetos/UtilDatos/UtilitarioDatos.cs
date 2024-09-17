using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace Objetos.UtilDatos
{
    static class UtilitarioDatos
    {

        public  static List<T> ConvertToList<T>(this IDataReader reader) where T : new()
        {
            List<T> list = new List<T>();

            // Obtener las propiedades de la entidad
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            while (reader.Read())
            {
                T obj = new T();

                foreach (PropertyInfo property in properties)
                {
                    // Validar si el DataReader contiene la columna correspondiente
                    if (ColumnExists(reader, property.Name) && !reader.IsDBNull(reader.GetOrdinal(property.Name)))
                    {
                        // Asignar el valor del DataReader a la propiedad de la entidad
                        property.SetValue(obj, Convert.ChangeType(reader[property.Name], property.PropertyType), null);
                    }
                }

                list.Add(obj);
            }

            return list;
        }

        private static bool ColumnExists(IDataReader reader, string columnName)
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
}
