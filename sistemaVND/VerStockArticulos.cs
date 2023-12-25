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
    public partial class VerStockArticulos : Form
    {
        SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public VerStockArticulos()
        {
            InitializeComponent();
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;

            cargarDatos();
        }

        private void numeroRegistros()
        {
            int numeroRegistros = dataGridView1.RowCount;
            lblRes.Text = numeroRegistros.ToString();
        }


        public void cargarDatos()
        {
            conexion.Open();
            string sql = "select codigoArticulo as Codigo, nombre as Nombre, talle as Talle, cantidadEnStock as 'Cantidad en Stock', reservado as 'Cantidad reservada' from articulo ";
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
            //TAER EL ID PERO NO LO VA A MOSTRAR ASI..
            string consulta = "select codigoArticulo as Codigo, nombre as Nombre, talle as Talle, cantidadEnStock as 'Cantidad en Stock', reservado as 'Cantidad reservada' from articulo WHERE codigoArticulo LIKE @busqueda OR nombre LIKE @busqueda ";
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

            // Ejecutar la búsqueda y actualizar el DataGridView, o cargar todos los proveedores si el TextBox está vacío
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                cargarDatos();
            }
            else
            {
                buscador(busqueda);
            }
        }




        //BOTON VER
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int codigo = 0;
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    codigo = Convert.ToInt32(row.Cells[0].Value.ToString());
                }

                stockArticulos f = new stockArticulos(this, codigo);
                f.Show(this);
            }
            else
            {
                MessageBox.Show("Seleccionar un artículo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void VerStockArticulos_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
    }
}
