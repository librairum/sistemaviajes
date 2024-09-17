using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objetos
{
    public class Cargo
    {
        public int ID { get; set; }

        public string nombrecargo { get; set; }

        public string descripcion { get; set; }

        public ICollection<Empleado> empleados { get; set; }
    }
}
