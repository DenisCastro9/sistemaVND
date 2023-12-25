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
    public partial class FichaPedido : Form
    {
        int numeroPedido = 0;
        DateTime fecha;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public FichaPedido(int numero)
        {
            InitializeComponent();
            this.numeroPedido = numero;
            label4.Text = Convert.ToString(numeroPedido);


            cargarDataGrid();

            cargarDatos();
        }

        private void cargarDatos()
        {
            conexion.Open();
            string c = "select p.fechaDePedido as fecha, c.razonSocial as cliente, e.descripcion as estado, u.nombre as usuario, importeTotal, totalPares from pedido as p join cliente as c on p.idCliente = c.id JOIN estadoPedido as e on p.idEstadoPedido=e.id join usuarios as u on p.idUsuario=u.idUsuario where p.numero=@pedido";
            SqlCommand comando = new SqlCommand(c, conexion);
            comando.Parameters.AddWithValue("@pedido", numeroPedido);
            SqlDataReader registros = comando.ExecuteReader();
            if (registros.Read())
            {
                fecha = Convert.ToDateTime(registros["fecha"].ToString());
                label10.Text = fecha.ToShortDateString().ToString();
                label11.Text = registros["cliente"].ToString();
                label13.Text = registros["estado"].ToString();
                label12.Text = registros["usuario"].ToString();
                label14.Text = "$ " + registros["importeTotal"].ToString();
                label5.Text = registros["totalPares"].ToString();
            }
            conexion.Close();
        }

        private void cargarDataGrid()
        {
            conexion.Open();
            string c = "select a.nombre, d.cantidad, a.talle, a.precioUnitario from articulo as a join detalleDePedido as d on d.idArticulo = a.idArticulo where numero = @idDetallePedido";
            SqlCommand comando = new SqlCommand(c, conexion);
            comando.Parameters.AddWithValue("@idDetallePedido", numeroPedido);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {
                dataGridView1.Rows.Add(registros["nombre"].ToString(),
                    registros["cantidad"].ToString(),
                    registros["talle"].ToString(),
                    registros["precioUnitario"].ToString());
            }

            conexion.Close();
        }


        //BOTON SALIR
        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void FichaPedido_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["precioUnitario"].Index && e.RowIndex >= 0)
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
