using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objetos
{
    public class EmpleadoDTO
    {
        //Vista para mostra DatosEmpleado y su cuenta de usuario, esta informacion de usuario se extrae de tabla usuario
        public int idempleado { get; set; }
        public string apellidos { get; set; }
        public string nombres { get; set; }
        public string dni { get; set; }
        public string genero { get; set; }
        public DateTime? fechanacimiento { get; set; }
        public string nombreusuario { get; set; }
        public string contrasena { get; set; }
        public string tipocuenta { get; set; }
        public string nombretipocuenta { get; set; }
        public int idcargo { get; set; }
        public string nombrecargo { get; set; }

        /*

     idempleado int,
apellidos varchar(100),
nombres varchar(100),
dni char(8),
Genero char(1),
FechaNacimiento date,
nombreusuario varchar(50),
contrasena varchar(50),	
tipocuenta varchar(50),
nombretipocuenta varchar(200),
idcargo int,
nombrecargo varchar(100)
     */

    }
}
