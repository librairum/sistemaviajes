using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
namespace Datos
{
    public class CargoDatos
    {

        private void Insertar()
        {
            using (var db = new sistemaviajesContext())
            {
                var cargo = new Cargo();
                
                //db.Cargo.Add()
            }
        }
        
    }
}
