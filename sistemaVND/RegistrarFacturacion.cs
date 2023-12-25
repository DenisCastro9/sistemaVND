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
    public partial class RegistrarFacturacion : Form
    {
        private int idcliente;
        private int nroPEDIDO;
        int cantidad = 0;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public RegistrarFacturacion(int nro, int total, int idCliente)
        {
            this.idcliente = 0;
            this.nroPEDIDO = 0;
            InitializeComponent();
            label16.Text = Convert.ToString(nro);//nro de PEDIDO
            label24.Text = "Total de pares: "+Convert.ToString(total);//total de pares
            textBox4.Text = Convert.ToString(idCliente);//idcliente
            this.nroPEDIDO = nro; // GUARDO EN VARIABLE EL NRO DE PEDIDO
            this.idcliente = idCliente;//GUARDO EN VARIABLE EL NRO DE CLIENTE

            cargarLosComboBox();
            cargarComboFormaDePago();
            cargarDatosDelCliente();
            cargarNroFactura();
            cargarDescripcionDelPedido();
            cargarImporteTotalDePedido();
            cargarNroDeRemitoAsignado();
        }

        private void cargarComboFormaDePago()
        {
            conexion.Open();
            string sql = "select id, descripcion from formaDePago";
            SqlCommand comando = new SqlCommand(sql, conexion);
            DataTable tabla1 = new DataTable();
            SqlDataAdapter adaptador1 = new SqlDataAdapter();
            adaptador1.SelectCommand = comando;
            adaptador1.Fill(tabla1);
            comboBox1.DataSource = tabla1;
            comboBox1.DisplayMember = "descripcion";
            comboBox1.ValueMember = "id";
            conexion.Close();
        }

        private void cargarLosComboBox()
        {
            comboBox2.Items.Add("Exento"); //0
            comboBox2.Items.Add("10,5"); //1
            comboBox2.Items.Add("21"); //2
            comboBox2.Items.Add("27"); //3
            comboBox2.SelectedIndex = 2;

            comboBox3.Items.Add("A");//0
            comboBox3.Items.Add("B");//1
            comboBox3.SelectedIndex = 0;

        }

        private void cargarDatosDelCliente()
        {
            conexion.Open();
            string sql = "  select c.razonSocial, c.ingresosBrutos, c.condicionIva from cliente as c where c.id = @idCliente";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@idCliente", idcliente);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                textBox3.Text = registro["razonSocial"].ToString();
                textBox6.Text = registro["ingresosBrutos"].ToString();
                textBox2.Text = registro["condicionIva"].ToString();

            }
            else
            {
                MessageBox.Show("Este pedido no tiene datos asignados", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            conexion.Close();
        }




        //REVISAR PORQUE CREO TENIA QUE TENER 12 DIGITOS
        private void cargarNroFactura()
        {
            int nro = 0;
            conexion.Open();
            string query2 = " SELECT TOP 1 numero FROM factura ORDER BY numero DESC";
            SqlCommand command = new SqlCommand(query2, conexion);
            SqlDataReader registro = command.ExecuteReader();
            if (registro.Read())
            {
                nro = Convert.ToInt32(registro["numero"].ToString());
                nro = nro + 1;
            }
            
            txtNumeroFactura.Text = Convert.ToString(nro);
            conexion.Close();
        }



        private void cargarDescripcionDelPedido()
        {
            contarRegistros();
            conexion.Open();
            string sql = "   select a.nombre as articulo, a.talle, d.cantidad, a.precioUnitario from detalleDePedido as d join articulo as a on d.idArticulo = a.idarticulo join pedido as p on p.numero = d.numero where p.numero = @nroPedido";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nroPedido", nroPEDIDO);
            SqlDataReader registro = comando.ExecuteReader();
            for (int i = 0; i < cantidad; i++)
            {
                if (registro.Read())

                {
                    string nombreArticulo = registro["articulo"].ToString();
                    string talleArticulo = registro["talle"].ToString();
                    int cantidadArticulo = Convert.ToInt32(registro["cantidad"]);
                    decimal precioUnitario = Convert.ToDecimal(registro["precioUnitario"]);

                    decimal importe = cantidadArticulo * precioUnitario; // Calcula el importe para este artículo

                    label17.Text += $"{nombreArticulo}  {talleArticulo} \n";
                    label18.Text += $"{cantidadArticulo} \n";
                    label19.Text += $"{precioUnitario}\n";
                    label20.Text += $"{importe}\n";
                }

                else
                {
                    MessageBox.Show("Este pedido no tiene datos asignados", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            conexion.Close();
        }




        private void contarRegistros()
        {
            conexion.Open();
            string sql = " select count(*) as cantidad from detalleDePedido join pedido on detalleDePedido.numero=pedido.numero where pedido.numero = @nroPed";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nroPed", nroPEDIDO);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                cantidad = Convert.ToInt32(registro["cantidad"].ToString());
            }
            conexion.Close();

        }


        

        private void cargarImporteTotalDePedido()
        {
            conexion.Open();
            string sql = "  select importeTotal, f.descripcion from pedido as p join formaDePago as f on p.idFormaDePago=f.id where numero= @nroP";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nroP", nroPEDIDO);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                label15.Text = registro["importeTotal"].ToString();
                comboBox1.Text = registro["descripcion"].ToString();

            }
            else
            {
                MessageBox.Show("Este pedido no tiene datos asignados", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            conexion.Close();
        }



        private void cargarNroDeRemitoAsignado()
        {
            conexion.Open();
            string sql = " select r.numero from remito as r join pedido as p on r.idPedido=p.numero where p.numero=@nroP";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nroP", nroPEDIDO);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                textBox1.Text = registro["numero"].ToString();

            }


            else
            {
                MessageBox.Show("Este pedido no tiene datos asignados", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            conexion.Close();
        }






        //BOTON CONTINUAR...
         private void button1_Click(object sender, EventArgs e)
        {
            if (txtNumeroFactura.Text == "")
            {
                MessageBox.Show("El nuúero de factura no puede estar vacío", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Seleccionar tipo de factura", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Seleccionar forma de pago", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Seleccionar Alicuota IVA", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //pasar al proximo form: tipoFactura, nroFactura, fecha, conDeVenta, alicuotaIva, idPedido, idRemito


            //selected item: valor que tiene escrito - selected index: posicion
            int tipoFactura = Convert.ToInt32(comboBox3.SelectedIndex);
            int nroFactura = Convert.ToInt32(txtNumeroFactura.Text);
            string fecha = DateTime.Now.ToString("dd-mm-yyyy");
            int conDeVenta = Convert.ToInt32(comboBox1.SelectedIndex);
            int alicuotaIva = Convert.ToInt32(comboBox2.SelectedIndex);
            int idP = Convert.ToInt32(nroPEDIDO);
            int idR = Convert.ToInt32(textBox1.Text);

            ConfirmarFactura c = new ConfirmarFactura(tipoFactura, nroFactura, fecha, conDeVenta, alicuotaIva, idP, idR);
            this.Hide();
            c.ShowDialog();
            this.Close();
        }





        //BOTON SALIR
        private void button2_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
