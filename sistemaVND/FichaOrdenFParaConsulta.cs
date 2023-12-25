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
    public partial class FichaOrdenFParaConsulta : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        int numeroOrdenF = 0;
        public FichaOrdenFParaConsulta(string codigo)
        {
            InitializeComponent();
            this.numeroOrdenF = Convert.ToInt32(codigo);
            label5.Text = Convert.ToString(numeroOrdenF);

            cargarPuntoDeControl();

            cargarDatosOrdenF();
            cargarDatosDelArticulo();            
            cargarFechas();
            cargarTalleCantidad();
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
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[1].Value = fin.ToString("dd-MM-yyyy"); //System.ArgumentOutOfRangeException: 'El índice estaba fuera del intervalo. Debe ser un valor no negativo e inferior al tamaño de la colección. Nombre del parámetro: index'
                }

            }
            conexion.Close();
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
                MessageBox.Show("Éste artículo no tiene detalle asignado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            else
            {
                MessageBox.Show("Error de carga", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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


        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }



        //BOTON VER DETALLE DEL PEIDDO
        private void button4_Click(object sender, EventArgs e)
        {
            int numero = Convert.ToInt32(label25.Text);
            FichaPedido fp = new FichaPedido(numero);
            fp.ShowDialog();
        }

        //BOTON VER MP ASIGNADA
        private void button5_Click(object sender, EventArgs e)
        {
            int numeroPedido = Convert.ToInt32(label25.Text);
            int numeroOrdenF = Convert.ToInt32(label5.Text);
            VerMPempleada fp = new VerMPempleada(numeroPedido, numeroOrdenF);
            fp.ShowDialog();
        }




        private void FichaOrdenFParaConsulta_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
        }
    }
}
