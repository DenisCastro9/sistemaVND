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
    public partial class VerTodosLosPedidos : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public VerTodosLosPedidos()
        {
            InitializeComponent();
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            dataGridView1.ClearSelection();

        }

        private void numeroRegistros()
        {
            int numeroRegistros = dataGridView1.RowCount;
            lblRes.Text = numeroRegistros.ToString();
        }

        //--------------BUSCADOR--------------
        private void cargarDatos()
        {            
            conexion.Open();
            string sql = "select p.numero as Numero, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Region' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id order by numero desc";
            SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla;
            dataGridView1.Columns["Importe total"].Visible = false;
            conexion.Close();
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
        private void buscador(string busqueda)
        {
            string consulta = "select p.numero as Numero, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Region' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id WHERE p.numero LIKE @busqueda OR c.razonSocial LIKE @busqueda OR est.descripcion LIKE @busqueda order by numero desc";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla;
            dataGridView1.Columns["Importe total"].Visible = false;
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
        //--------------BUSCADOR--------------






        //APENAS CARGA LA PAGINA POR DEFECTO ESTA SELECCIONADO EN TODOS
        private void VerTodosLosPedidos_Load(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                conexion.Open();
                string sql = "select p.numero as Número, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Región' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id order by numero desc";
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Importe total"].Visible = false;
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            dataGridView1.ClearSelection();
            numeroRegistros();
        }

        //SELECCIONADO EN TODOS
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                conexion.Open();
                string sql = "select p.numero as Número, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Región' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id order by numero desc";
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Importe total"].Visible = false;
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            numeroRegistros();
        }
        //REGISTRADOS
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                conexion.Open();
                string sql = "select p.numero as Número, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Región' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id where p.idEstadoPedido=1 order by numero desc";
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Importe total"].Visible = false;
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            numeroRegistros();
        }
        //EN FABRICACION
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                conexion.Open();
                string sql = "select p.numero as Número, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Región' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id where p.idEstadoPedido=2 order by numero desc";
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Importe total"].Visible = false;
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            numeroRegistros();
        }
        //A FACTURAR
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                conexion.Open();
                string sql = "select p.numero as Número, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Región' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id where p.idEstadoPedido=3 order by numero desc";
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Importe total"].Visible = false;
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            numeroRegistros();
        }
        //REMITADO
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true)
            {
                conexion.Open();
                string sql = "select p.numero as Número, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Región' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id where p.idEstadoPedido=4 order by numero desc";
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Importe total"].Visible = false;
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            numeroRegistros();
        }
        //FACTURADOS
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked == true)
            {
                conexion.Open();
                string sql = "select p.numero as Número, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Región' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id where p.idEstadoPedido=5 order by numero desc";
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Importe total"].Visible = false;
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            numeroRegistros();
        }
        //DESPACHADOS
        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked == true)
            {
                conexion.Open();
                string sql = "select p.numero as Número, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Región' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id where p.idEstadoPedido=6 order by numero desc";
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Importe total"].Visible = false;
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            numeroRegistros();
        }
        //ANULADOS
        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked == true)
            {
                conexion.Open();
                string sql = "select p.numero as Número, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Región' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id where p.idEstadoPedido=7 order by numero desc";
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Importe total"].Visible = false;
                conexion.Close();
                dataGridView1.ClearSelection();
                numeroRegistros();
            }
        }








        //BOTON VER DETALLE DE PEDIDO
        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)//si estan seleccionada mas de 0 filas del datagrid2...
            {
                int pedido = 0;//necesito dos variables para pasarle a la otra clase "buscarDetallePEdido.cs"
                decimal monto = 0;
                foreach (DataGridViewRow row in dataGridView1.SelectedRows) //devuelve de acuerdo a lo que se selecciono
                {
                    pedido = Convert.ToInt32(row.Cells[0].Value.ToString());//saco el numero de pedido de la columna cero "numero de pedido"                    
                    monto = Convert.ToDecimal(row.Cells[6].Value.ToString());// saco el monto total de la columna "importe total"                    

                }
                BuscarDetallePEdido p = new BuscarDetallePEdido(pedido, monto);//le envio estos datos al form 
                p.FormClosed += p_FormClosed;
                p.Show(this);
            }
            else
            {
                MessageBox.Show("Seleccionar un pedido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void p_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                conexion.Open();
                string sql = "select p.numero as Número, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Región' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id order by numero desc";
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Importe total"].Visible = false;
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            if (radioButton2.Checked == true)
            {
                conexion.Open();
                string sql = "select p.numero as Número, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Región' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id where p.idEstadoPedido=1 order by numero desc";
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Importe total"].Visible = false;
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            if (radioButton3.Checked == true)
            {
                conexion.Open();
                string sql = "select p.numero as Número, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Región' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id where p.idEstadoPedido=2 order by numero desc";
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Importe total"].Visible = false;
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            if (radioButton4.Checked == true)
            {
                conexion.Open();
                string sql = "select p.numero as Número, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Región' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id where p.idEstadoPedido=3 order by numero desc";
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Importe total"].Visible = false;
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            if (radioButton5.Checked == true)
            {
                conexion.Open();
                string sql = "select p.numero as Número, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Región' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id where p.idEstadoPedido=4 order by numero desc";
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Importe total"].Visible = false;
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            if (radioButton6.Checked == true)
            {
                conexion.Open();
                string sql = "select p.numero as Número, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Región' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id where p.idEstadoPedido=5 order by numero desc";
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Importe total"].Visible = false;
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            if (radioButton7.Checked == true)
            {
                conexion.Open();
                string sql = "select p.numero as Número, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Región' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id where p.idEstadoPedido=6 order by numero desc";
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Importe total"].Visible = false;
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            if (radioButton8.Checked == true)
            {
                conexion.Open();
                string sql = "select p.numero as Número, p.fechaDePedido as Fecha, c.razonSocial as Cliente, p.totalPares as 'Cantidad de pares', u.nombre as 'Pedido tomado por', est.descripcion as Estado, p.importeTotal as 'Importe total', u.region as 'Región' from pedido as p join cliente as c on p.idCliente = c.id join usuarios as u on p.idUsuario = u.idUsuario join estadoPedido as est on p.idEstadoPedido = est.id where p.idEstadoPedido=7 order by numero desc";
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Importe total"].Visible = false;
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            dataGridView1.ClearSelection();
            numeroRegistros();
        }










        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
