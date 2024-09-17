using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objetos
{
    public class TablaGeneral
    {

        public int codigotabla { get; set; }
        public string nombretabla { get; set; }
        public string valor { get; set; }
        public string descripcion { get; set; }

        public List<TablaGeneral> ListarGenero()
        {
            List<TablaGeneral> lista = new List<TablaGeneral>();
            lista.Add(new TablaGeneral
            {
                codigotabla = 1,
                nombretabla = "GeneroPersona",
                descripcion = "Masculino",
                valor = "M"
            });
            lista.Add(new TablaGeneral
            {
                codigotabla = 2,
                nombretabla = "GeneroPersona",
                descripcion = "Femenino",
                valor = "F"
            });

            

            return lista;
        }

        


    }
}
