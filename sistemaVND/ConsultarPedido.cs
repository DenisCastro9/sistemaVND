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
    public partial class ConsultarPedido : Form
    {

        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public ConsultarPedido()
        {
            InitializeComponent();
            textBox1.TextChanged += textBox1_TextChanged;
            cargarDataPedidos();
        }
        private void numeroRegistros()
        {
            int numeroRegistros = dataGridView1.RowCount;
            lblRes.Text = numeroRegistros.ToString();
        }


        private void cargarDataPedidos()
        {
            conexion.Open();
            string sql = "SELECT p.numero as Número, p.fechaDePedido as Fecha ,p.importeTotal as Importe , a.razonSocial as Cliente, p.totalPares as TotalPares FROM pedido as p inner join cliente as a on a.id = p.idCliente where p.idEstadoPedido=1 order by numero asc";
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
            string sql = "SELECT p.numero as Número, p.fechaDePedido as Fecha ,p.importeTotal as Importe , a.razonSocial as Cliente, p.totalPares as TotalPares " +
                "FROM pedido as p inner join cliente as a on a.id = p.idCliente where p.idEstadoPedido=1 and p.numero LIKE @busqueda OR a.razonSocial LIKE @busqueda AND p.idEstadoPedido=1 order by numero asc";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla;
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string busqueda = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                cargarDataPedidos();
            }
            else
            {
                buscador(busqueda);
            }
        }






        //BOTON VER DETALLE
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)//si estan seleccionada mas de 0 filas del datagrid2...
            {
                int pedido = 0;
                decimal monto = 0;
                foreach (DataGridViewRow row in dataGridView1.SelectedRows) //devuelve de acuerdo a lo que se selecciono
                {
                    pedido = Convert.ToInt32(row.Cells[0].Value.ToString());//saco el numero de pedido de la columna cero "numero de pedido"
                    monto = Convert.ToDecimal(row.Cells[2].Value.ToString());// saco el monto total de la columna "importe total"                   

                }
                BuscarDetallePEdido p = new BuscarDetallePEdido(pedido, monto);//le envio estos datos al form 
                p.FormClosed += p_FormClosed;
                p.Show(this);
            }
            else
            {
                MessageBox.Show("Selecciona un pedido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }







        private void p_FormClosed(object sender, FormClosedEventArgs e)
        {
            cargarDataPedidos();
        }



        private void ConsultarPedido_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            numeroRegistros();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["importe"].Index && e.RowIndex >= 0)
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
