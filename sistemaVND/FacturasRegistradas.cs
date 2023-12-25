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
    public partial class FacturasRegistradas : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public FacturasRegistradas()
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
            string sql = "  select f.numero, f.fecha, c.razonSocial, d.idPedido, f.montoTotal  from factura as f  join cliente as c on f.idCliente = c.id  join detalleFactura as d on f.idDetalleFactura = d.numero order by fecha asc";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {
                string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows.Add(registros["numero"].ToString(),
                                       fechaFormateada,
                                       registros["razonSocial"].ToString(),
                                       registros["idPedido"].ToString(),
                                       registros["montoTotal"].ToString());
            }

            conexion.Close();
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
        private void buscador(string busqueda)
        {
            conexion.Open();
            string sql = "  select f.numero, f.fecha, c.razonSocial, d.idPedido, f.montoTotal  from factura as f  join cliente as c on f.idCliente = c.id  join detalleFactura as d on f.idDetalleFactura = d.numero " +
                "where f.numero LIKE @busqueda OR  c.razonSocial LIKE @busqueda order by fecha asc";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {
                string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows.Add(registros["numero"].ToString(),
                                       fechaFormateada,
                                       registros["razonSocial"].ToString(),
                                       registros["idPedido"].ToString(),
                                       registros["montoTotal"].ToString());
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
                cargarDataGrid();
            }
            else
            {
                buscador(busqueda);
            }
        }





        //BOTON VER EN PANTALLA
        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                int nroF = 0, nroP = 0;
                foreach (DataGridViewRow row in dataGridView1.SelectedRows) //devuelve de acuerdo a lo que se selecciono
                {
                    nroF = Convert.ToInt32(row.Cells[0].Value.ToString());//pasa el nro de factura
                    nroP = Convert.ToInt32(row.Cells[3].Value.ToString());//pasa el nro de pedido

                }
                FacturaVerDetalle r = new FacturaVerDetalle(nroF, nroP);
                r.Show(this);
            }
            else
            {
                MessageBox.Show("Seleccionar una factura", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void FacturasRegistradas_Load(object sender, EventArgs e)
        {
            numeroRegistros();
            dataGridView1.ClearSelection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["monto"].Index && e.RowIndex >= 0)
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
