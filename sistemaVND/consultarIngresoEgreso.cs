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
    public partial class consultarIngresoEgreso : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public consultarIngresoEgreso()
        {
            InitializeComponent();
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            cargarDatos();
        }


        private void numeroRegistros()
        {
            int numeroRegistros = dataGridView1.RowCount;
            label18.Text = numeroRegistros.ToString();
        }

        private void cargarDatos()
        {
            conexion.Open();
            string sql = "select dni, nombre, fechaIngreso, fechaEgreso, area, region from usuarios";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {
                string fechaIngreso = registros["fechaIngreso"] != DBNull.Value
                ? DateTime.Parse(registros["fechaIngreso"].ToString()).ToString("dd/MM/yyyy HH:mm:ss")
        :        string.Empty;

                string fechaEgreso = registros["fechaEgreso"] != DBNull.Value
                    ? DateTime.Parse(registros["fechaEgreso"].ToString()).ToString("dd/MM/yyyy HH:mm:ss")
                    : string.Empty;

                dataGridView1.Rows.Add(registros["dni"].ToString(),
                                       registros["nombre"].ToString(),
                                       fechaIngreso,
                                       fechaEgreso,
                                       registros["area"].ToString(),
                                       registros["region"].ToString());

            }
            conexion.Close();
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
        private void buscador(string busqueda)
        {
            conexion.Open();
            string sql = "select dni, nombre, fechaIngreso, fechaEgreso, area, region from usuarios where nombre LIKE @busqueda OR dni LIKE @busqueda";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {

                dataGridView1.Rows.Add(registros["dni"].ToString(),
                                       registros["nombre"].ToString(),
                                       registros["fechaIngreso"].ToString(),
                                       registros["fechaEgreso"].ToString(),
                                       registros["area"].ToString(),
                                       registros["region"].ToString());
            }
            conexion.Close();
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string busqueda = txtBusqueda.Text.Trim();
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                cargarDatos();
            }
            else
            {
                buscador(busqueda);
            }
        }












        private void consultarIngresoEgreso_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'sistemaVNDDataSet.usuarios' Puede moverla o quitarla según sea necesario.
            //this.usuariosTableAdapter.Fill(this.sistemaVNDDataSet.usuarios);
            dataGridView1.ClearSelection();
            numeroRegistros();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
