using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Formato_Prov
{
    public partial class FormProv : Form
    {

        ValidadorFormatoFactura obj = new ValidadorFormatoFactura();
        public Proveedor prv;
        public bool validado = false;
        public FormProv(Proveedor Prv)
        {
            this.prv = Prv;
            InitializeComponent();
        }
        public FormProv()
        {
            InitializeComponent();
        }
        ValidadorFormatoFactura validador = new ValidadorFormatoFactura();
     
        private bool ValidarCampo(Guna2TextBox control, string mensajeError)
        {
            if (string.IsNullOrEmpty(control.Text))
            {
                Error.SetError(control, mensajeError);
                return false;
            }
            else
            {
                Error.SetError(control, "");
                return true;
            }
        }
        private bool ValidarCode()
        {
            return ValidarCampo(txtCodeproveedor, "Debe escribir el código del proveedor");
        }
        private bool ValidarFact()
        {
            return ValidarCampo(txtNumF, "Debe escribir el número de factura");
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (ValidarFact() == false)
            {
                return;
            }
            if (ValidarCode() == false)
            {
                return;
            }
            // Buscar el proveedor por su código
            var validador = new ValidadorFormatoFactura();

             bool resultado = obj.ValidarYGuardarFactura(prv.formato,txtNumF.Text);
            if (resultado)
            {
                validado = true;
                prv.factura = txtNumF.Text;
                Close();
            }
            else
                validado = false;
        }
    

        private void txtCodeproveedor_TextChanged(object sender, EventArgs e)
        {
            string codigoProveedor = txtCodeproveedor.Text;
            if (prv != null)
            {

              
                txtNumF.PlaceholderText = prv.ejemploFormato;
               
                //txtnomProv.Text = proveedorEncontrado.Nombre;
         
            }
            else
            {
                txtnomProv.Text = "";
                txtNumF.PlaceholderText = "";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtCodeproveedor.Text = prv.codigo;
            txtNumF.Text = prv.factura;
            txtnomProv.Text = prv.nombre;

        }

       
    }
}
