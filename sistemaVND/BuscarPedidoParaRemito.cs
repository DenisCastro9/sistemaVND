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
    public partial class BuscarPedidoParaRemito : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public BuscarPedidoParaRemito()
        {
            InitializeComponent();
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            cargarDataPedido();
        }

        private void numeroRegistros()
        {
            int numeroRegistros = dataGridView1.RowCount;
            label18.Text = numeroRegistros.ToString();
        }

        //CARGAR LOS PEDIDOS
        private void cargarDataPedido()
        {
            conexion.Open();
            string sql = "select p.numero as numero, p.fechaDePedido as fechaDePedido, p.importeTotal as importeTotal, p.totalPares as totalPares, a.nombre as articulo, c.id as idCliente, c.razonSocial as cliente from cliente as c join pedido as p on p.idCliente = c.id join articulo as a on p.idArt = a.idarticulo  where p.idEstadoPedido=3 order by numero asc";
            SqlCommand comando = new SqlCommand(sql, conexion);

            SqlDataReader registros = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {
                string fechaFormateada = DateTime.Parse(registros["fechaDePedido"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows.Add(registros["numero"].ToString(), //numero pedido 0
                                       fechaFormateada, //fecha de toma de pedido 1
                                       registros["importeTotal"].ToString(),//importe total pedido 2
                                       registros["totalPares"].ToString(),//total pares del pedido 3
                                       registros["articulo"].ToString(),//nombre articulo 4
                                       registros["idCliente"].ToString(),
                                       registros["cliente"].ToString());//nro cliente 5
            }
            int idClienteColumnIndex = 5; // Índice de la columna "idCliente" (recuerda que los índices empiezan en 0)
            dataGridView1.Columns[idClienteColumnIndex].Visible = false;
            conexion.Close();
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
        private void buscador(string busqueda)
        {
            conexion.Open();
            string consulta = " select p.numero as numero, p.fechaDePedido as fechaDePedido, p.importeTotal as importeTotal, p.totalPares as totalPares, a.nombre as articulo, c.id as idCliente, c.razonSocial as cliente from cliente as c join pedido as p on p.idCliente = c.id join articulo as a on p.idArt = a.idarticulo  where p.idEstadoPedido=3 and p.numero LIKE @busqueda OR  c.razonSocial LIKE @busqueda AND p.idEstadoPedido=3 order by numero asc";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {
                string fechaFormateada = DateTime.Parse(registros["fechaDePedido"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows.Add(registros["numero"].ToString(), //numero pedido 0
                                       fechaFormateada, //fecha de toma de pedido 1
                                       registros["importeTotal"].ToString(),//importe total pedido 2
                                       registros["totalPares"].ToString(),//total pares del pedido 3
                                       registros["articulo"].ToString(),//nombre articulo 4
                                       registros["idCliente"].ToString(),
                                       registros["cliente"].ToString());//nro cliente 5
            }
            int idClienteColumnIndex = 5; // Índice de la columna "idCliente" (recuerda que los índices empiezan en 0)
            dataGridView1.Columns[idClienteColumnIndex].Visible = false;
            conexion.Close();
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string busqueda = txtBusqueda.Text.Trim();
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                cargarDataPedido();
            }
            else
            {
                buscador(busqueda);
            }
            numeroRegistros();
        }














        //BOTON ACEPTAR pasa datos al registrar remito;
        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                int nro = 0, total = 0, idCliente = 0;

                foreach (DataGridViewRow row in dataGridView1.SelectedRows) //devuelve de acuerdo a lo que se selecciono
                {
                    nro = Convert.ToInt32(row.Cells[0].Value.ToString());//pasa el nro de pedido            
                    total = Convert.ToInt32(row.Cells[3].Value.ToString()); //pasa el total de pares
                    idCliente = Convert.ToInt32(row.Cells[5].Value.ToString());// pasa el id del cliente
                }

                RegistrarRemito r = new RegistrarRemito(nro, total, idCliente);
                this.Hide();
                r.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Seleccionar un pedido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
 


        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void BuscarPedidoParaRemito_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            numeroRegistros();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["importeTotal"].Index && e.RowIndex >= 0)
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
