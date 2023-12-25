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
    public partial class DetalleOrdenF : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        int numeroOrdenF = 0;
        private Fabricacion formPrincipal;
        public DetalleOrdenF(Fabricacion form, string codigo)
        {
            InitializeComponent();
            formPrincipal = form;
            this.numeroOrdenF = Convert.ToInt32(codigo);
            label5.Text = Convert.ToString(numeroOrdenF);


            cargarPuntoDeControl();

            cargarDatosOrdenF();
            cargarDatosDelArticulo();
            cargarFechas();
            cargarTalleCantidad();

            
        }

        private void cargarTalleCantidad()
        {
            conexion.Open();
            string sql = "select  a.talle as Talle, dof.cantidad  from ordenDeFabricacion as ordf join detalleOrdenF as dof on ordf.numero=dof.numero join articulo as a on dof.idArticulo=a.codigoArticulo join estadoOrdenF as estof on ordf.idEstadoOrdenF=estof.id join puntoDeControl as pc on ordf.idPuntoDeControl=pc.id where ordf.numero=@numeroOrdenF";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@numeroOrdenF", numeroOrdenF);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {
                dataGridView1.Rows.Add(registros["Talle"].ToString(),
                                       registros["cantidad"].ToString());

            }
            conexion.Close();
        }





        private void cargarFechas()
        {
            conexion.Open();
            string sql = "select ordf.fechaCreacion, ordf.fechaPrevistaFin, ordf.fechaRealInicio as 'Inicio', ordf.fechaRealFin as 'Fin' from ordenDeFabricacion as ordf  where ordf.numero=@numeroOrdenF";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@numeroOrdenF", numeroOrdenF);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {

                if (registros["fechaCreacion"] != DBNull.Value)
                {
                    DateTime fechaCreacion = Convert.ToDateTime(registros["fechaCreacion"]);
                    dataGridView3.Rows.Add(fechaCreacion.ToString("dd-MM-yyyy"), "");
                }

                if (registros["fechaPrevistaFin"] != DBNull.Value)
                {
                    DateTime fechaPrevistaFin = Convert.ToDateTime(registros["fechaPrevistaFin"]);
                    dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[1].Value = fechaPrevistaFin.ToString("dd-MM-yyyy");
                }

                if (registros["Inicio"] != DBNull.Value)
                {
                    DateTime inicio = Convert.ToDateTime(registros["Inicio"]);
                    dataGridView2.Rows.Add(inicio.ToString("dd-MM-yyyy"), "");
                }

                if (registros["Fin"] != DBNull.Value)
                {
                    DateTime fin = Convert.ToDateTime(registros["Fin"]);
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[1].Value = fin.ToString("dd-MM-yyyy");
                }

            }
            conexion.Close();
        }

        private void cargarPuntoDeControl()
        {
            conexion.Open();
            string sql = "select id, descripcion from puntoDeControl";
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
            string sql = "select ordf.cliente, a.nombre, a.talle as Talle, dof.cantidad, estof.descripcion as 'Estado orden', pc.descripcion as 'Punto de control', ordf.idPedido as 'Pedido numero'  from ordenDeFabricacion as ordf join detalleOrdenF as dof on ordf.numero=dof.numero join articulo as a on dof.idArticulo=a.codigoArticulo join estadoOrdenF as estof on ordf.idEstadoOrdenF=estof.id join puntoDeControl as pc on ordf.idPuntoDeControl=pc.id where ordf.numero=@numeroOrdenF";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@numeroOrdenF", numeroOrdenF);
            SqlDataReader registros = comando.ExecuteReader();
            if (registros.Read())
            {
                label3.Text = registros["cliente"].ToString();
                label6.Text = registros["nombre"].ToString();
                label1.Text = registros["Estado orden"].ToString();
                comboBox1.Text = registros["Punto de control"].ToString();
                label25.Text = registros["Pedido numero"].ToString();
            }
            else {
                MessageBox.Show("ERROR", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            conexion.Close();
        }










        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //BOTON ANULAR
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBoxButtons botones = MessageBoxButtons.YesNo;
            DialogResult dr = MessageBox.Show("Desea anular la orden de fabricación", "Anular orden de fabricacion",
                botones, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                conexion.Open();
                String query3 = "update ordenDeFabricacion set idEstadoOrdenF = 4, idPuntoDeControl=7, fechaRealFin=@fechaRealFin where numero=@numeroOrden";
                SqlCommand commandx = new SqlCommand(query3, conexion);
                commandx.Parameters.AddWithValue("@numeroOrden", numeroOrdenF);
                commandx.Parameters.AddWithValue("@fechaRealFin", DateTime.Now.Date);//fecha que finalizo la orden al ser anulada
                commandx.ExecuteNonQuery();
                MessageBox.Show("Orden de fabricación Anulada", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        //BOTON REGISTRAR PUNTO DE CONTROL 
        private void button1_Click(object sender, EventArgs e)
        {
            //ACORDATE DE PEDIR CONFIRMACION PARA REGISTRAR EL PUNTO Y SI EL PUNTO ES DEL ESTADO "FINALIZADA"
            //HAY QUE HACER UPDATE EN ESTADOoRDENf A FINALIZADA

            MessageBoxButtons botones = MessageBoxButtons.YesNo;
            DialogResult dr = MessageBox.Show("Desea Modificar el punto de control", "Modificar punto de control",
                botones, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                conexion.Open();
                String query3 = "update ordenDeFabricacion set idPuntoDeControl=@puntoDeControl where numero=@numeroOrden";
                SqlCommand commandx = new SqlCommand(query3, conexion);
                commandx.Parameters.AddWithValue("@numeroOrden", numeroOrdenF);
                commandx.Parameters.Add("@puntoDeControl", SqlDbType.Int).Value = comboBox1.SelectedValue;


                if (comboBox1.SelectedValue.Equals(7))
                {
                    // UPDATE PARA QUE LA ORDEN PASE A ESTADO FINALIZADO. NO PONER CONEXION PARA QUE GUARDE BIEN LOS DATOS
                    //conexion.Open();
                    string query4 = "update ordenDeFabricacion set idEstadoOrdenF=5, fechaRealFin=@fechaRealFin where numero=@numeroOrden";
                    SqlCommand commandy = new SqlCommand(query4, conexion);
                    commandy.Parameters.AddWithValue("@numeroOrden", numeroOrdenF);
                    commandy.Parameters.AddWithValue("@fechaRealFin", DateTime.Now.Date);//fecha que finalizo la orden 
                    commandy.ExecuteNonQuery();
                    //conexion.Close();

                    //ACA EL PEDIDO PASA A ESTADO A FACTURAR= idEstadoPedido=3
                    //conexion.Open();
                    string query5 = "update pedido set idEstadoPedido=3 where numero=@numeroOrden";
                    SqlCommand commandy2 = new SqlCommand(query5, conexion);
                    commandy2.Parameters.AddWithValue("@numeroOrden", label25.Text);
                    commandy2.ExecuteNonQuery();
                    //conexion.Close();
                }


                commandx.ExecuteNonQuery();
                MessageBox.Show("Punto de control actualizado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
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



        
        //HABILITACION DE LOS BOTONES DE ACUERDO AL ESTADO DE LA ORDENF
        private void DetalleOrdenF_Load(object sender, EventArgs e)
        {
            if (label1.Text == "Anulada" || label1.Text == "Finalizada")
            {
                button3.Enabled = false;
                button1.Enabled = false;
            }
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
        }





        //BOTON VER DETALLE DEL PEDIDO
        private void button4_Click(object sender, EventArgs e)
        {
            int numero = Convert.ToInt32(label25.Text);
            FichaPedido fp = new FichaPedido(numero);
                fp.ShowDialog();
        }

        //BOTON VER MP EMPLEADA
        private void button5_Click(object sender, EventArgs e)
        {
            int numeroPedido = Convert.ToInt32(label25.Text);
            int numeroOrdenF = Convert.ToInt32(label5.Text);
            VerMPempleada fp = new VerMPempleada(numeroPedido, numeroOrdenF);
            fp.ShowDialog();
        }
    }
}
