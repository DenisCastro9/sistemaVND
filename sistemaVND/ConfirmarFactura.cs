using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistemaVND
{
    public partial class ConfirmarFactura : Form
    {
        private int tipoFactura;
        private int nroFactura;
        private string fecha;
        private int conDeVenta;
        private int alicuotaIva;
        private int idP;
        private int idR;
        double importeTotalPedido;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public ConfirmarFactura(int tipoFactura,int nroFactura, string fecha,int conDeVenta,int alicuotaIva,int idP,int idR)
        {
            this.tipoFactura = 0;
            this.nroFactura = 0;
            this.conDeVenta = 0;
            this.alicuotaIva = 0;
            this.idP = 0;
            this.idR = 0;
            InitializeComponent();
            this.tipoFactura = tipoFactura;
            this.nroFactura = nroFactura;
            this.fecha = fecha;
            this.conDeVenta = conDeVenta;
            this.alicuotaIva = alicuotaIva;
            this.idP = idP;
            this.idR = idR;


            label19.Text = "0,00";
            label17.Text = "0,00";

            cargarComboBox();
            cargarImporteNetoGravado();
            calcularIva();
            calcularMontoFinal();

        }

        

        private void cargarComboBox()
        {
            comboBox2.Items.Add("0"); //0
            comboBox2.Items.Add("10,5"); //1
            comboBox2.Items.Add("21"); //2
            comboBox2.Items.Add("27"); //3
            comboBox2.SelectedIndex = alicuotaIva;
        }




        private void cargarImporteNetoGravado()
        {
            conexion.Open();
            string sql = " select importeTotal from pedido where pedido.numero=@idPedido";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@idPedido", idP);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                importeTotalPedido = Convert.ToDouble(registro["importeTotal"].ToString());

            }

            
            else
            {
                MessageBox.Show("Este pedido no tiene datos asignados", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            label15.Text = importeTotalPedido.ToString("N2");
            txtBase.Text = importeTotalPedido.ToString("N2"); //ACA PUSE EL IMPORTE POR LAS DUDAS------------------------------------------------------
            conexion.Close();

        }




        private void calcularIva()
        {
            //txtLRec = txtLRec.Text = (Convert.ToDecimal(txtLuxRec.Text) * Convert.ToDecimal("1.2")).ToString();
            double importeTotal = Double.Parse(label15.Text);
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Seleccionar Alicuota", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else {


                if (comboBox2.SelectedIndex == 2) {
                    double ImporteDeIva = 0.0;
                    ImporteDeIva = importeTotal * 21 / 100;
                    label13.Text = ImporteDeIva.ToString("N2");
                  }
                if (comboBox2.SelectedIndex == 0)
                {
                    double ImporteDeIva = 0.0;
                    label13.Text = ImporteDeIva.ToString("N2");
                }
                if (comboBox2.SelectedIndex == 1)
                {
                    double ImporteDeIva = 0.0;
                    ImporteDeIva = importeTotal * Double.Parse("10,5") / 100;
                    label13.Text = ImporteDeIva.ToString("N2");
                }
                if (comboBox2.SelectedIndex == 3)
                {
                    double ImporteDeIva = 0.0;
                    ImporteDeIva = importeTotal * 27 / 100;
                    label13.Text = ImporteDeIva.ToString("N2");
                }
            }
        }



        private void calcularMontoFinal()
        {
            double montoFinal = 0.00;
            montoFinal = Double.Parse(label15.Text) + Double.Parse(label13.Text) + Double.Parse(label19.Text) + Double.Parse(label17.Text);
            label16.Text = montoFinal.ToString("N2");

        }


    

  
        //BOTON CALCULAR
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtBase.Text == "")
            {
                MessageBox.Show("Ingresar el importe base", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtAlicuota.Text == "")
            {
                MessageBox.Show("Ingresar Alicuota", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double perIB = 0.00;
            perIB = Double.Parse(txtBase.Text) * Double.Parse(txtAlicuota.Text) / 100;
            txtTotal.Text = perIB.ToString("N2");
            label17.Text = perIB.ToString("N2");
            calcularMontoFinal();
        }

        //boton confirmar
        private void button2_Click(object sender, EventArgs e)
        {

            int tipoFactura = this.tipoFactura;
            int nroFactura = this.nroFactura;
            string fecha = this.fecha;
            int condDeVenta = this.conDeVenta;
            int alicuotaIva = this.alicuotaIva;
            int idP = this.idP;
            int idR = this.idR;
            double importeNeto = Convert.ToDouble(label15.Text);
            double importeIva = Convert.ToDouble(label13.Text);
            double importeTributos = Convert.ToDouble(label17.Text);
            double importeFinal = Convert.ToDouble(label16.Text);


            FacturaVerEnPantalla f = new FacturaVerEnPantalla(tipoFactura, nroFactura, fecha, condDeVenta, alicuotaIva, idP, idR, importeNeto, importeIva, importeTributos, importeFinal);
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        //boton cancelar
        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }




        //formato con coma y punto PARA LA ABSE IMPONIBLE
        private void txtBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 32 && e.KeyChar <= 43) || (e.KeyChar == 45) || (e.KeyChar == 47 ) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Ingresar formato de precio: 0.00,00", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
        //FORMATO DECIMAL PARA EL PORCENTAJE
        private void txtAlicuota_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 43) || (e.KeyChar >= 45 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Ingresar formato de porcentaje: 0,0", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}
