using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace Entidad
{
    public class MiContexto : DbContext
    {

        public MiContexto() : base("name=Data Source=.;Initial Catalog=sistemaviajes;Integrated Security=True")
        {

        }

    }
}
