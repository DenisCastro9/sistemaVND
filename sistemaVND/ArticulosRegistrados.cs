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
    public partial class ArticulosRegistrados : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");

        public ArticulosRegistrados()
        {
            InitializeComponent();
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            cargarGrilla();
        }


        private void numeroRegistros()
        {
            int numeroRegistros = dataGridView2.RowCount;
            label17.Text = numeroRegistros.ToString();
        }


        public void cargarGrilla()
        {
            conexion.Open();
            string sql = "select codigoArticulo as Código, nombre as Nombre, tipoArticulo as 'Tipo de artículo', marca as Marca, talle as Talle, precioUnitario as Precio from articulo";
            SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView2.DataSource = tabla;
            conexion.Close();
            dataGridView2.ClearSelection();
            numeroRegistros();

        }

 
        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }



        //buscador METODO
        private void buscarArticulos(string busqueda) {

            // Realizar la búsqueda en la base de datos
            string consulta = "select codigoArticulo as Código, nombre as Nombre, tipoArticulo as 'Tipo de artículo', marca as Marca, talle as Talle, precioUnitario as Precio from articulo WHERE codigoArticulo LIKE @busqueda OR nombre LIKE @busqueda";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);

            // Actualizar el DataGridView con los resultados de la búsqueda
            dataGridView2.DataSource = tabla;
            dataGridView2.ClearSelection();
            numeroRegistros();
        }
        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string busqueda = txtBusqueda.Text.Trim();
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                cargarGrilla();
            }
            else
            {
                buscarArticulos(busqueda);
            }
            numeroRegistros();
        }


        //BOTON REGISTRAR NUEVO
        private void button5_Click(object sender, EventArgs e)
        {
            RegistrarArticulo ra = new RegistrarArticulo(this);
            ra.ShowDialog();
        }

    


        //BOTON VER DETALLE
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int codigo=0;
                string nombre = "";

                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    codigo = Convert.ToInt32(row.Cells[0].Value.ToString());
                    nombre = row.Cells[1].Value.ToString();
                }
                FichaArticulo doc = new FichaArticulo(this, codigo, nombre);
                doc.Show(this);
            }
            else
            {
                MessageBox.Show("Seleccionar un artículo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);    
            }
        }




        //BOTON VER LISTADO DE PRECIOS
        private void button3_Click(object sender, EventArgs e)
        {
            PreciosAriculos p = new PreciosAriculos();
            p.ShowDialog(this);
        }


        private void ArticulosRegistrados_Load(object sender, EventArgs e)
        {
          dataGridView2.ClearSelection();
            numeroRegistros();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView2.Columns["Precio"].Index && e.RowIndex >= 0)
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal importe))
                {
                    // Formatear el valor agregando el signo "$" y el formato de moneda
                    e.Value = string.Format("{0:C}", importe);
                    e.FormattingApplied = true;
                }
            }
        }
    }
}
