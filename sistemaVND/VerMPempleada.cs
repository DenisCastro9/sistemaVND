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
    public partial class VerMPempleada : Form
    {
        int numeroOrdenF = 0;
        int numeroP = 0;
        private SqlConnection conexion = new SqlConnection("data source=  DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public VerMPempleada(int numeroPedido, int numeroOrdenF)
        {
            InitializeComponent();
            this.numeroOrdenF=numeroOrdenF;
            label10.Text = Convert.ToString(numeroOrdenF);
            this.numeroP = numeroPedido;

            cargarDatos();

            cargarMPcortado();
            cargarMPempaque();
            cargarMPaparado();
        }

        private void cargarDatos()
        {            
            conexion.Open();
            string sql = "select a.nombre as articulo, a.talle, do.cantidad, orf.totalPares from detalleOrdenF as do join articulo as a on do.idArticulo=a.codigoArticulo join ordenDeFabricacion as orf on do.numero=orf.numero where do.numero= @numeroOrdenF";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@numeroOrdenF", numeroOrdenF);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {
                label5.Text = registros["articulo"].ToString();
                label12.Text = registros["totalPares"].ToString();
                dataGridView1.Rows.Add(registros["talle"].ToString(),
                                       registros["cantidad"].ToString());
            }
            conexion.Close();
            dataGridView1.ClearSelection();

        }

        private void cargarMPaparado()
        {
            conexion.Open();
            string sql = "select cxp.idMateriaPrima as 'Codigo MP', mp.descripcion as 'Descripcion MP', cxp.cantidadXpares as 'A despachar' from cantidadMPxPedido as cxp join pedido as p on cxp.numeroPedido=p.numero join materiaPrima as mp on cxp.idMateriaPrima=mp.codigo where p.numero=@numeroPedido and cxp.area= @area";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@numeroPedido", numeroP);
            comando.Parameters.AddWithValue("@area", "Ojalillado");
            SqlDataReader registro = comando.ExecuteReader();
            dataGridView4.Rows.Clear();
            while (registro.Read())
            {
                dataGridView4.Rows.Add(registro["Codigo MP"].ToString(),
                                       registro["Descripcion MP"].ToString(),
                                       registro["A despachar"].ToString());
            }
            conexion.Close();
            dataGridView4.ClearSelection();
        }

        private void cargarMPempaque()
        {
            conexion.Open();
            string sql = "select cxp.idMateriaPrima as 'Codigo MP', mp.descripcion as 'Descripcion MP', cxp.cantidadXpares as 'A despachar' from cantidadMPxPedido as cxp join pedido as p on cxp.numeroPedido=p.numero join materiaPrima as mp on cxp.idMateriaPrima=mp.codigo where p.numero=@numeroPedido and cxp.area= @area";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@numeroPedido", numeroP);
            comando.Parameters.AddWithValue("@area", "Empaque");
            SqlDataReader registro = comando.ExecuteReader();
            dataGridView2.Rows.Clear();
            while (registro.Read())
            {
                dataGridView2.Rows.Add(registro["Codigo MP"].ToString(),
                                       registro["Descripcion MP"].ToString(),
                                       registro["A despachar"].ToString());
            }
            conexion.Close();
            dataGridView2.ClearSelection();
        }

        private void cargarMPcortado()
        {
            conexion.Open();
            string sql = "select cxp.idMateriaPrima as 'Codigo MP', mp.descripcion as 'Descripcion MP', cxp.cantidadXpares as 'A despachar' from cantidadMPxPedido as cxp join pedido as p on cxp.numeroPedido=p.numero join materiaPrima as mp on cxp.idMateriaPrima=mp.codigo where p.numero=@numeroPedido and cxp.area= @area";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@numeroPedido", numeroP);
            comando.Parameters.AddWithValue("@area", "Cortado");
            SqlDataReader registro = comando.ExecuteReader();
            dataGridView3.Rows.Clear();
            while (registro.Read())
            {
                dataGridView3.Rows.Add(registro["Codigo MP"].ToString(),
                                       registro["Descripcion MP"].ToString(),
                                       registro["A despachar"].ToString());
            }
            conexion.Close();
            dataGridView3.ClearSelection();
        }


        //BOTON SALIR
        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void VerMPempleada_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
            dataGridView4.ClearSelection();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
