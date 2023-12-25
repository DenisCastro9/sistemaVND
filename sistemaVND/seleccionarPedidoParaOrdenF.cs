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
    public partial class seleccionarPedidoParaOrdenF : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public seleccionarPedidoParaOrdenF()
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

        private void cargarDatos()
        {
            conexion.Open();
            string sql = "SELECT p.numero as Numero, p.fechaDePedido as Fecha ,p.importeTotal as Importe , a.razonSocial as Cliente, p.totalPares as TotalPares FROM pedido as p inner join cliente as a on a.id = p.idCliente where p.idEstadoPedido=1";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla;
            dataGridView1.Columns["Importe"].Visible = false;
            conexion.Close();
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
        private void buscador(string busqueda)
        {
            string consulta = "SELECT p.numero as Numero, p.fechaDePedido as Fecha ,p.importeTotal as Importe , a.razonSocial as Cliente, p.totalPares as TotalPares FROM pedido as p inner join cliente as a on a.id = p.idCliente where p.idEstadoPedido=1 and p.numero LIKE @busqueda OR  a.razonSocial LIKE @busqueda and p.idEstadoPedido=1";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla;
            dataGridView1.Columns["Importe"].Visible = false;
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





        //BOTON VER DETALLE
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)//si estan seleccionada mas de 0 filas del datagrid2...
            {
                int pedido = 0;
                foreach (DataGridViewRow row in dataGridView1.SelectedRows) 
                {
                    pedido = Convert.ToInt32(row.Cells[0].Value.ToString());                    
                }
                FichaPedido p = new FichaPedido(pedido);
                p.Show(this);
            }
            else
            {
                MessageBox.Show("Seleccionar un pedido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        //BOTON SELECCIONAR necesito el numero del pedido
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int numeroPedido = 0;
                foreach (DataGridViewRow row in dataGridView1.SelectedRows) 
                {
                    numeroPedido = Convert.ToInt32(row.Cells[0].Value.ToString());
                }
                RegistroOrdenFCompararStock rg = new RegistroOrdenFCompararStock(numeroPedido);
                rg.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Seleccionar un pedido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }           
        }

        private void seleccionarPedidoParaOrdenF_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            numeroRegistros();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
