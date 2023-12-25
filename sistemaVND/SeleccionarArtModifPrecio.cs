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
    public partial class SeleccionarArtModifPrecio : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public SeleccionarArtModifPrecio()
        {
            InitializeComponent();
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            cargarData();
        }

        private void numeroRegistros()
        {
            int numeroRegistros = dataGridView1.RowCount;
            lblRes.Text = numeroRegistros.ToString();
        }

        private void cargarData()
        {
            conexion.Open();
            string sql = " select nombre as Nombre from ArticulosGeneral";
            SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla;
            conexion.Close();
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
        private void buscador(string busqueda)
        {
            string consulta = "select nombre from ArticulosGeneral WHERE nombre LIKE @busqueda";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);

            // Actualizar el DataGridView con los resultados de la búsqueda
            dataGridView1.DataSource = tabla;
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string busqueda = txtBusqueda.Text.Trim();
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                cargarData();
            }
            else
            {
                buscador(busqueda);
            }
        }


        //BOTON SELECCIONAR 
        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string nombre = "";
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    nombre = row.Cells[0].Value.ToString();
                }
                ModificarPrecioArt mf = new ModificarPrecioArt(nombre);
                mf.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("Seleccionar un artículo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void SeleccionarArtModifPrecio_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
    }
}
