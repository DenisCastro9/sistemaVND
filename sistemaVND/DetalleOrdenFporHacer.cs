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
    public partial class DetalleOrdenFporHacer : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        int numeroOrdenF = 0;
        DateTime fechaInicio;
        DateTime fechaPrevistaFin;
        private Fabricacion formPrincipal;

        public DetalleOrdenFporHacer(Fabricacion form, string codigo)
        {
            InitializeComponent();
            formPrincipal = form;
            this.numeroOrdenF = Convert.ToInt32(codigo);
            label5.Text = Convert.ToString(numeroOrdenF);


            cargarDatosOrdenF();
            cargarDatosDelArticulo();
        }

        private void cargarDatosDelArticulo()
        {
            conexion.Open();
            string sql = "select hilo1, costuron, apliqueI, apliqueII, apliqueIII, horma from materiaPrimaPorArticulo where codigoArt=@articulo";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@articulo", label6.Text);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                label19.Text = registro["hilo1"].ToString();
                label20.Text = registro["costuron"].ToString();
                label21.Text = registro["apliqueI"].ToString();
                label22.Text = registro["apliqueII"].ToString();
                label23.Text = registro["apliqueIII"].ToString();
                label24.Text = registro["horma"].ToString();
            }
            else
            {
                MessageBox.Show("Este artículo no tiene detalle asignado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            conexion.Close();
        }

        private void cargarDatosOrdenF()
        {
            conexion.Open();
            string sql = "select ordf.cliente, ordf.fechaCreacion, ordf.fechaPrevistaFin, a.nombre, a.talle as Talle, dof.cantidad, ordf.idPedido as 'Pedido numero' from ordenDeFabricacion as ordf join detalleOrdenF as dof on ordf.numero=dof.numero join articulo as a on dof.idArticulo=a.codigoArticulo where ordf.numero=@numeroOrdenF";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@numeroOrdenF", numeroOrdenF);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {
                label3.Text = registros["cliente"].ToString();
                fechaInicio = Convert.ToDateTime(registros["fechaCreacion"].ToString());
                label9.Text = fechaInicio.ToShortDateString().ToString();
                fechaPrevistaFin = Convert.ToDateTime(registros["fechaPrevistaFin"].ToString());
                label15.Text = fechaPrevistaFin.ToShortDateString().ToString();
                label6.Text = registros["nombre"].ToString();
                label25.Text = registros["Pedido numero"].ToString();
                dataGridView1.Rows.Add(registros["Talle"].ToString(),
                                       registros["cantidad"].ToString());
            }
            conexion.Close();
        }





        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        //BOTON INICIAR 
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBoxButtons botones = MessageBoxButtons.YesNo;
            DialogResult dr = MessageBox.Show("Desea iniciar la fabricación de la orden", "Iniciar orden de fabricacion",
                botones, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                conexion.Open();
                String query3 = "update ordenDeFabricacion set idEstadoOrdenF = 2, idPuntoDeControl=2, fechaRealInicio=@fechaRealInicio where numero=@numeroOrden";
                SqlCommand commandx = new SqlCommand(query3, conexion);
                commandx.Parameters.AddWithValue("@numeroOrden", numeroOrdenF);
                commandx.Parameters.AddWithValue("@fechaRealInicio", DateTime.Now.Date);//fecha real que inicio la orden
                commandx.ExecuteNonQuery();
                MessageBox.Show("Orden de fabricación iniciada", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conexion.Close();

                formPrincipal.cargarOrdenesEnProceso();
                formPrincipal.cargarOrdenesPorHacer();
                formPrincipal.cargarOrdenesFinalizadas();
                formPrincipal.siCheckBox();
                this.Close();
            }
            else if (dr == DialogResult.No)
            {
                return;
            }           
        }

        //BOTON ANULAR SIN PASAR POR EL INICIO DE LA ORDEN
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBoxButtons botones = MessageBoxButtons.YesNo;
            DialogResult dr = MessageBox.Show("Desea anular la orden de fabricación", "Anular orden de fabricacion",
                botones, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                conexion.Open();
                String query3 = "update ordenDeFabricacion set idEstadoOrdenF = 4, idPuntoDeControl=7, fechaRealFin=@fechaRealFin, fechaRealInicio=@fechaRealInicio where numero=@numeroOrden";
                SqlCommand commandx = new SqlCommand(query3, conexion);
                commandx.Parameters.AddWithValue("@numeroOrden", numeroOrdenF);
                commandx.Parameters.AddWithValue("@fechaRealFin", DateTime.Now.Date);//fecha que finalizo la orden al ser anulada
                commandx.Parameters.AddWithValue("@fechaRealInicio", DateTime.Now.Date);//como la orden se anula sin pasar por fabricacion, agregarle esta fecha de inciio, la misma que de fin
                commandx.ExecuteNonQuery();
                MessageBox.Show("Orden de fabricación anulada", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conexion.Close();


                //ACA EL PEDIDO PASA A ESTADO REGISTRADO NUEVAMENTE, ya que se anulo la orden, no el pedido
                conexion.Open();
                string query5 = "update pedido set idEstadoPedido=1 where numero=@numeroOrden";
                SqlCommand commandy2 = new SqlCommand(query5, conexion);
                commandy2.Parameters.AddWithValue("@numeroOrden", label25.Text);
                commandy2.ExecuteNonQuery();
                


                //COMO EL PEDIDO PASA A SER REGISTRADO DE NUEVO, LA CANTIDAD DE MPXPEDIDO TIENE QUE VOLVER A CERO TAMBIEN
                string query6 = "delete from cantidadMPxPedido where numeroPedido=@numero";
                SqlCommand commands = new SqlCommand(query6, conexion);
                commands.Parameters.AddWithValue("numero", label25.Text);
                commands.ExecuteNonQuery();
                conexion.Close();

                formPrincipal.cargarOrdenesEnProceso();
                formPrincipal.cargarOrdenesPorHacer();
                formPrincipal.cargarOrdenesFinalizadas();
                formPrincipal.siCheckBox();
                this.Close();
            }
            else if (dr == DialogResult.No)
            {
                return;
            }
        }

        //BOTON VER DETALLE DEL PEDIDO
        private void button4_Click(object sender, EventArgs e)
        {
            int numero = Convert.ToInt32(label25.Text);
            FichaPedido fp = new FichaPedido(numero);
            fp.ShowDialog();
        }

        //BOTON VER MP empleada
        private void button5_Click(object sender, EventArgs e)
        {
            int numeroPedido = Convert.ToInt32(label25.Text);
            int numeroOrdenF = Convert.ToInt32(label5.Text);
            VerMPempleada fp = new VerMPempleada(numeroPedido, numeroOrdenF);
            fp.ShowDialog();
        }

        private void DetalleOrdenFporHacer_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
