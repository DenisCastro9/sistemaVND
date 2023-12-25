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
    public partial class RegistrarRemito : Form
    {
        private int idcliente;
        private int nroPEDIDO;
        int cantidad = 0;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");


        public RegistrarRemito(int nro, int total, int idCliente)
        {
            //DEL FORM ANTERIOR VIENE EL NRO DE PEDIDO, EL TOTAL DE PARES, LA DESCRP Y EL NRO DE CLIENTE
            this.idcliente = 0;
            this.nroPEDIDO = 0;
            InitializeComponent();
            lblCodPedido.Text = Convert.ToString(nro); //CODIGO PEDIDO A LABEL
            label12.Text = Convert.ToString(total);//TOTAL DE PARES DEL PEDIDO A LABEL
            txtIdCliente.Text = Convert.ToString(idCliente);//NUMERO DEL CLIENTE A TEXTBOX
            this.nroPEDIDO = nro; // GUARDO EN VARIABLE EL NRO DE PEDIDO
            this.idcliente = idCliente;//GUARDO EN VARIABLE EL NRO DE CLIENTE

            cargarDatosUsandoElIdCliente();
            cargarNroRemito();
            cargarDescripcionDelPedido();
            textBox2.Text = "1";

        }

        private void cargarDescripcionDelPedido()
        {
            contarRegistros();
            conexion.Open();
            string sql = "  select a.nombre as articulo, a.talle, d.cantidad  from detalleDePedido as d join articulo as a on d.idArticulo = a.idarticulo join pedido as p on p.numero = d.numero where p.numero = @nroPedido"; 
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nroPedido", nroPEDIDO);
            SqlDataReader registro = comando.ExecuteReader();
            for (int i = 0; i < cantidad; i++)
            {
                if (registro.Read())
                { 
                lblDescPedido.Text += registro["articulo"].ToString() + " " + registro["talle"].ToString() + "\n";
                lblCantPedido.Text += registro["cantidad"].ToString() + "\n";
                }
            
            else
            {
                    MessageBox.Show("Este pedido no tiene datos asignados", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            conexion.Close();
        }


        //CUENTA LOS REGISTROS QUE VIENEN DE LA BD
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






        private void cargarNroRemito()
        {
            int nro = 0;
            conexion.Open();
            string query2 = " SELECT TOP 1 numero FROM remito ORDER BY numero DESC";
            SqlCommand command = new SqlCommand(query2, conexion);
            SqlDataReader registro = command.ExecuteReader();
            if (registro.Read())
            {
                nro = Convert.ToInt32(registro["numero"].ToString());
                nro = nro + 1;
                
            }
            textBox1.Text = Convert.ToString(nro);
            conexion.Close();
        }







        //CARGAR LOS DATOS DEL FORM ANTERIOR (BUSCARPEDIDO.CS) ACA (son CLIENTE, DOMICILIO DE ENTREGA Y TRANSPORTE)
        private void cargarDatosUsandoElIdCliente()
        {
            conexion.Open();
            string sql = " select c.razonSocial, c.domicilioDeEntrega, t.nombre as transp from cliente as c join transportista as t on c.idTransportista=t.id where c.id=@idCliente";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@idCliente",idcliente);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                txtNombreCliente.Text = registro["razonSocial"].ToString();
                txtDomicilioCliente.Text = registro["domicilioDeEntrega"].ToString();
                txtTransporteCliente.Text = registro["transp"].ToString();

            }
            else {
                MessageBox.Show("Este pedido no tiene datos asignados", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
                conexion.Close();

        }




        //PASAR PEDIDO A ESTADO REMITADO
        private void cambiarEstadoDePedido()
        {
            //EL REMITO PARA A REMITADO ASI SE PUEDE FACTUAR DESPUES....
            conexion.Open();
            string sql = " update pedido set idEstadoPedido=4 where numero=@idPedido";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@idPedido", Convert.ToInt32(lblCodPedido.Text)); //id del pedido
            comando.ExecuteNonQuery();
            conexion.Close();
        }



        //BOTON ACEPTAR ACA SE REGISTRA EL REMITO
        private void button1_Click(object sender, EventArgs e)
        {


            if (txtNombreCliente.Text == "")
            {
                MessageBox.Show("Seleccionar cliente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtTransporteCliente.Text == "")
            {
                MessageBox.Show("Seleccionar transportista", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtDomicilioCliente.Text == "")
            {
                MessageBox.Show("Indicar domicilio de entrega", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (textBox2.Text == "")
            {
                MessageBox.Show("Agregar bultos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            conexion.Open();
            string sql = "insert into remito (numero, fecha, bultos, idPedido) values (@numero, @fecha, @bultos, @idPedido)";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.Add("@numero", SqlDbType.Int).Value = Convert.ToInt32(textBox1.Text);
            comando.Parameters.AddWithValue("@fecha", DateTime.Now.Date);
            comando.Parameters.Add("@bultos", SqlDbType.Int).Value = Convert.ToInt32(textBox2.Text);
            comando.Parameters.Add("@idPedido", SqlDbType.Int).Value = Convert.ToInt32(lblCodPedido.Text);
            int result = comando.ExecuteNonQuery();
            if (result < 0)
            {
                Console.WriteLine("No se pudo crear el remito");
            }
            else
            {
                conexion.Close();
                MessageBox.Show("Remito registrado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                cambiarEstadoDePedido();

                int nro = Convert.ToInt32(textBox1.Text);
                int nroP = Convert.ToInt32(nroPEDIDO);

                RemitoVerEnPantalla acep = new RemitoVerEnPantalla(nro, nroP);
                this.Hide();
                acep.ShowDialog();
                this.Close();
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {

            Dispose();
        }
    }
}
