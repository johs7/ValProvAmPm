using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Formato_Prov
{
   
    public class ValidadorFormatoFactura
    {
        public enum eValidregex { No, Yes, YesButUseCompare }

       

        public bool ValidarYGuardarFactura(string formato, string factura)
        {
            if (!string.IsNullOrEmpty(formato) && Regex.IsMatch(factura, formato))
            {
                // El formato es válido, permitir guardar la factura
          
                return true;
                 
            }
            else
            {
                // El formato no es válido, lanzar una excepción o manejar el error de otra manera
               

                return false;
            }
        }
        // Método para validar el código del proveedor

        /*public Proveedor BuscarProveedorPorCodigo(string codigo)
        {
            return proveedores.FirstOrDefault(proveedor => proveedor.Codigo == codigo);

        }*/
        public static eValidregex IsValidRX(string pattern, out Regex RX)
        {
            RX = null;

            if (pattern.Length == 0)
                return eValidregex.No;

            List<char> c1 = new List<char>
            {
                '\\' , '.' , '(' , ')' , '{' , '}' , '^' , '$' , '+' , '*' , '?' , '[' , ']', '|'
            };

            if (c1.Count(e => pattern.Contains(e)) > 0)
            {
                TimeSpan ts_timeout = new TimeSpan(days: 0, hours: 0, minutes: 0, seconds: 1, milliseconds: 0);

                try
                {
                    RX = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase, ts_timeout);
                    return eValidregex.Yes;
                }
                catch (ArgumentNullException)
                {
                    return eValidregex.No;
                }
                catch (ArgumentOutOfRangeException)
                {
                    return eValidregex.No;
                }
                catch (ArgumentException)
                {
                    return eValidregex.No;
                }
            }
            else
            {
                return eValidregex.YesButUseCompare;
            }

        }
}
}
