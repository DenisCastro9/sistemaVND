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
    public partial class ConsultarRemito : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public ConsultarRemito()
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

        //CARGAR EL DATA GRID
        private void cargarDataGrid()
        {
            conexion.Open();
            string sql = " select r.numero, r.fecha, r.idPedido as Pedido, p.totalPares, c.razonSocial as cliente, t.nombre as transporte from remito as r join pedido as p on r.idPedido = p.numero join cliente as c on p.idCliente = c.id join transportista as t on c.idTransportista = t.id order by fecha asc";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {
                string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows.Add(registros["numero"].ToString(), 
                                       fechaFormateada, 
                                       registros["Pedido"].ToString(),//numero de pedido
                                       registros["totalPares"].ToString(),
                                       registros["cliente"].ToString(),
                                       registros["transporte"].ToString());
            }
            conexion.Close();
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
        private void buscador(string busqueda)
        {
            conexion.Open();
            string consulta = "select r.numero, r.fecha, r.idPedido as Pedido, p.totalPares, c.razonSocial as cliente, t.nombre as transporte from remito as r join pedido as p on r.idPedido = p.numero join cliente as c on p.idCliente = c.id join transportista as t on c.idTransportista = t.id where r.numero LIKE @busqueda OR  c.razonSocial LIKE @busqueda order by fecha asc";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {
                string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows.Add(registros["numero"].ToString(),
                                        fechaFormateada,
                                        registros["Pedido"].ToString(),//numero de pedido
                                        registros["totalPares"].ToString(),
                                        registros["cliente"].ToString(),
                                        registros["transporte"].ToString());
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
                int nroR = 0, nroP = 0;

                foreach (DataGridViewRow row in dataGridView1.SelectedRows) //devuelve de acuerdo a lo que se selecciono
                {
                    nroR = Convert.ToInt32(row.Cells[0].Value.ToString());//pasa el nro de remito
                    nroP = Convert.ToInt32(row.Cells[2].Value.ToString());//pasa el nro de pedido

                }

                RemitoVerEnPantalla r = new RemitoVerEnPantalla(nroR, nroP);
                r.Show(this);
            }
            else
            {
                MessageBox.Show("Seleccionar un remito", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


     

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void ConsultarRemito_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
    }
}
