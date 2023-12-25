using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace sistemaVND
{
    public partial class consultarUsuarios : Form
    {
        public consultarUsuarios()
        {
            InitializeComponent();
        }

        private void numeroRegistros()
        {
            int numeroRegistros = dataGridView1.RowCount;
            lblRes.Text = numeroRegistros.ToString();
        }

        private SqlConnection conexion = new SqlConnection("data source=  DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");


        private void consultarUsuarios_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'sistemaVNDDataSetUsuarios.usuarios' Puede moverla o quitarla según sea necesario.
            this.usuariosTableAdapter.Fill(this.sistemaVNDDataSetUsuarios.usuarios);
            dataGridView1.ClearSelection();
            numeroRegistros();
        }


        private void txtBusqueda_KeyUp(object sender, KeyEventArgs e)
        {
            conexion.Open();
            SqlCommand buscar = conexion.CreateCommand();
            buscar.CommandType = CommandType.Text;
            // buscar.CommandText = "SELECT dni, nombre, clave, preg1, preg2, preg3, nivel, area, region FROM usuarios WHERE usuarios.nombre LIKE ('" + txtBusqueda.Text + "%')";
            buscar.CommandText = "SELECT dni, nombre, nivel, area, region FROM usuarios WHERE usuarios.nombre LIKE ('" + txtBusqueda.Text + "%')";
            buscar.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(buscar);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conexion.Close();
            dataGridView1.ClearSelection();
            numeroRegistros();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
