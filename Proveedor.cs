using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formato_Prov
{
    public class Proveedor
    {

        public string codigo { get; set; }
        public string formato { get; set; }

        public string ejemploFormato { get; set; }
        public string nombre { get; set; }
        public string factura { get; set; }

        public Proveedor(string Codigo, string Formato,string ejemploformato)
        {

           codigo = Codigo;
            formato = Formato;
            ejemploFormato = ejemploformato;
        }
    }
}
