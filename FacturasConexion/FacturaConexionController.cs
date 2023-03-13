using System;
using System.Collections.Generic;
using System.Linq;
using RMH.APP.Core.Shared.Contracts;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using RMH.APP.Core.Shared;
using System.Windows;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Formato_Prov;

namespace FacturasConexion
{
    [Export(typeof(IRMHCustomExtension))]

    public class FacturaConexionController : IRMHCustomExtension
    { 
        
        Proveedor proveedor = new Proveedor("", "", "");
        ValidadorFormatoFactura obj = new ValidadorFormatoFactura();
        public ApplicationSession AppSession { get; set; }

        public FacturaConexionController()
        {
            ServiceProvider service = ServiceProvider.Instance;

            AppSession = (ApplicationSession)service.GetObject(typeof(ApplicationSession));

            //register for getting notificatios when ITEM object is updated

            AppSession.onOrderSavedBefore += AppSession_onOrderSavedBefore;
           
        }



        private void AppSession_onOrderSavedBefore(IPurchaseOrder arg1, System.ComponentModel.CancelEventArgs arg2)
        {
            // Obtener la conexión a la base de datos
            string connectionString = AppSettings.Database.ConnectionString;
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                // Obtener el número de factura
                string factura = arg1.Header.Reference;
                // Obtener el ID del proveedor
                string proveedorId = arg1.Header.SupplierCode;
                string nomprov = arg1.Header.SupplierName;

                // Buscar el proveedor por ID
                ValidadorFormatoFactura validador = new ValidadorFormatoFactura();
                Proveedor proveedor = validador.BuscarProveedorPorCodigo(proveedorId);
                string query = "select Terms FROM Supplier WHERE Code = @proveedorId";
                string query2 = "select CustomText1 FROM Supplier WHERE Code = @proveedorId";
                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@proveedorId", proveedorId);
                string formato = command.ExecuteScalar()?.ToString();

                SqlCommand command2 = new SqlCommand(query2, conexion);
                command2.Parameters.AddWithValue("@proveedorId", proveedorId);
                string customText1 = command2.ExecuteScalar()?.ToString();

                // Validar el formato de la factura
                if(obj.ValidarYGuardarFactura(formato,factura))
                {
                    // El formato es válido, permitir guardar la factura
                    arg2.Cancel = false;
                   
                    MessageBox.Show(formato);
                    MessageBox.Show("CustomText1: " + customText1); // mostrar CustomText1 en un MessageBox
                }
                else
                {
                    // El formato no es válido, mostrar un mensaje de error y cancelar el guardado de la factura
                    arg2.Cancel = true;
                    MessageBox.Show("El formato del número de factura no es válido para este proveedor.");
                    Form1 frm = new Form1(proveedorId, factura, nomprov);
                    frm.ShowDialog();
                }
            }
        }

    }
}

