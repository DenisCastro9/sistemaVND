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
    public partial class SeleccionarClienteModificarCredito : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public SeleccionarClienteModificarCredito()
        {
            InitializeComponent();
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            cargarDataGrid();
        }

        private void numeroRegistros()
        {
            int numeroRegistros = dataGridView1.RowCount;
            lblRes.Text = numeroRegistros.ToString();
        }

        private void cargarDataGrid()
        {
            conexion.Open();
            string sql = "select razonSocial as Nombre, cuit as Cuit, condicionIva as 'Condicion I.V.A.', ingresosBrutos as 'Ingresos brutos', telefono as Teléfono from cliente";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla;
            conexion.Close();
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
        private void buscador(string busqueda)
        {
            string consulta = " select razonSocial as Nombre, cuit as Cuit, condicionIva as 'Condicion I.V.A.', ingresosBrutos as 'Ingresos brutos', telefono as Teléfono from cliente WHERE razonSocial LIKE @busqueda OR cuit LIKE @busqueda";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla;
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string busqueda = txtBusqueda.Text.Trim();
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                cargarDataGrid();
            }
            else
            {
                buscador(busqueda);
            }
        }







        //BOTON SELECCIONAR
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string cliente = "";

                foreach (DataGridViewRow row in dataGridView1.SelectedRows) //devuelve de acuerdo a lo que se selecciono
                {
                    cliente = row.Cells[0].Value.ToString();//pasa el nombre del cliente
                }
                ModificarClienteCredito mc = new ModificarClienteCredito(cliente);
                mc.FormClosed += mc_FormClosed;
                mc.Show(this);
            }
            else
            {
                MessageBox.Show("Seleccionar un cliente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void mc_FormClosed(object sender, FormClosedEventArgs e)
        {
            cargarDataGrid();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void SeleccionarClienteModificarCredito_Load(object sender, EventArgs e)
        {
            numeroRegistros();
            dataGridView1.ClearSelection();
        }
    }
}
