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
/*private List<Proveedor> proveedores = new List<Proveedor>()
    
     Aurami
new Proveedor("00000130", @"^([1-9]\d{5}|0[1-9]\d{4}|[1-9]0{5})$", "123456"),
     Bimbo   new Proveedor("00000003", @"^(?!0{4})\d{4}$", "0012"),
    CBC    new Proveedor("", @"^RVNC(0[0-9]|[1-9][0-9])-[0-9]{8}$", "RVNC12-12345678"),
    CCN    new Proveedor("P4", @"^(?!0{7})\d{7}$", "1234567"),
      Dos robles  new Proveedor("00000067", @"^A-\d{7}$", "A-1234567"),
    ICONSA    new Proveedor("P6", @"^(?!000000)[0-9]{6}$", "123456"),
   COMVARSA     new Proveedor("00000010", @"^(?!0000000000)[0-9]{10}$", "1234567890"),
   CONGELADOS.SA     new Proveedor("00000051", @"^(?!0000000000)[0-9]{10}$", "1234567890"),

    LALA    new Proveedor("00000019", @"^[A-Z]{3}\d{4}$", "ABC1234"),
        new Proveedor("P10", @"^(?!0000000)[0-9]{7}$", "1234567"),
        new Proveedor("00000256", @"^AA[A-Za-z0-9]F-\d{6}$", "AA3F-042893"),
        new Proveedor("P12", @"^(?!000000)[0-9]{7}$", "1234567"),
        new Proveedor("P13", @"^(?!000000)[0-9]{7}$", "1234567"),
        new Proveedor("00000304", @"^(?!000000)[0-9]{6}$", "123456"),
        new Proveedor("P16", @"^(?!0000)[0-9]{4}$", "1234"),
         new Proveedor("P17", @"^(?!0000)[0-9]{4}$", "1235"),
         new Proveedor("P18", @"^(?!0000)[0-9]{4}$", "3233"),
        new Proveedor("P19", @"^(?!000000)[0-9]{6}$", "232323"),
         new Proveedor("P20", @"^(?!000000)[0-9]{6}$", "222122"),
             new Proveedor("P21", @"^(?!0000)[0-9]{4}$", "0909"),
             new Proveedor("P22", @"^[A-Z]{1}[0-9]{2}-[0-9]{9}$", "A01-123456789"),
             
*/