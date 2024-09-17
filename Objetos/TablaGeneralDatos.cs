using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Objetos
{
    public static class TablaGeneralDatos
    {
        public static List<TablaGeneral> ListarTablaGeneral(string nombretabla, string valor)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@nombretabla", nombretabla },
                {"@valor", valor }
            };
            string cadenaConexion = "";
            string procedimientoAlmacenado = "Spu_svb_Trae_TablaGeneral";
            List<TablaGeneral> lista = MetodoGenerico.ListarDatos<TablaGeneral>( procedimientoAlmacenado, parametros);

            /*realizar accione con la lista o el metod que se envia 
             * al metodo de procedimiento almacenado*/
            return lista;
            
       }

    }
}
