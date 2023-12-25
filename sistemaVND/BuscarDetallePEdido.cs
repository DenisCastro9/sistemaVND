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
	public partial class BuscarDetallePEdido : Form
	{
		private int pedido;
		DateTime fecha;
		private SqlConnection conn = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");

		public BuscarDetallePEdido(int pedido, decimal monto)//recupero de la clase registrarPedido.cs
		{
			this.pedido = 0;
			InitializeComponent();
			dataGridView1.ClearSelection();
			label14.Text = Convert.ToString(pedido);
			label15.Text = Convert.ToString(monto);
			this.pedido = pedido;
			DetallePedidosDataGridview();
			cargarDatosNuevos();
			cargarFechaDeSalida();
		
		}

		private void numeroRegistros()
		{
			int numeroRegistros = dataGridView1.RowCount;
			label18.Text = numeroRegistros.ToString();
		}

		private void cargarFechaDeSalida()
        {
			conn.Open();
			string sql = "select numero, fechaSalidaDePedido from pedido where numero=@numero";
			SqlCommand comando = new SqlCommand(sql, conn);
			comando.Parameters.AddWithValue("@numero", pedido);
			SqlDataReader registros = comando.ExecuteReader();
			while (registros.Read())
			{
				if (registros["fechaSalidaDePedido"] != DBNull.Value)
				{
					DateTime fechaCreacion = Convert.ToDateTime(registros["fechaSalidaDePedido"]);
					label17.Text=fechaCreacion.ToString("dd-MM-yyyy");
				}			
			}
			conn.Close();
		}


        public void cargarDatosNuevos()
        {
			conn.Open();
			string c = "select p.fechaDePedido as fecha, c.razonSocial as cliente, e.descripcion as estado, u.nombre as usuario, p.totalPares from pedido as p join cliente as c on p.idCliente = c.id JOIN estadoPedido as e on p.idEstadoPedido=e.id join usuarios as u on p.idUsuario=u.idUsuario where p.numero=@pedido";
			SqlCommand comando = new SqlCommand(c, conn);
			comando.Parameters.AddWithValue("@pedido", pedido);
			SqlDataReader registros = comando.ExecuteReader();
			if (registros.Read())
			{
				fecha = Convert.ToDateTime(registros["fecha"].ToString());
				label10.Text = fecha.ToShortDateString().ToString();
				label11.Text = registros["cliente"].ToString();
				label13.Text = registros["estado"].ToString();
				label12.Text = registros["usuario"].ToString();
				label5.Text = registros["totalPares"].ToString();
			}
			conn.Close();
        }
        public void DetallePedidosDataGridview()
		{
			
			conn.Open();
			string c = "select a.nombre,d.cantidad, a.talle, a.precioUnitario from articulo as a join detalleDePedido as d on d.idArticulo = a.idArticulo where numero = @idDetallePedido ; ";
			SqlCommand comando = new SqlCommand(c, conn);
			comando.Parameters.AddWithValue("@idDetallePedido", pedido);
			SqlDataReader registros = comando.ExecuteReader();
			dataGridView1.Rows.Clear();
			while (registros.Read())
			{
				dataGridView1.Rows.Add(registros["nombre"].ToString(),
					registros["cantidad"].ToString(),
					registros["talle"].ToString(),
					registros["precioUnitario"].ToString());
			}
				
			conn.Close();
			dataGridView1.ClearSelection();
			numeroRegistros();
		}	




		//HABILITACION DE LOS BOTONES DE ACUERDO AL ESTADO
        private void BuscarDetallePEdido_Load(object sender, EventArgs e)
        {
			if (label13.Text.Equals("Registrado"))
			{
				button4.Enabled = false;
			}
			else if (label13.Text.Equals("En fabricacion"))
			{
				button2.Enabled = false;
				button3.Enabled = false;
				button4.Enabled = false;
			}
			else if (label13.Text.Equals("A facturar"))
			{
				button2.Enabled = false;
				button3.Enabled = false;
				button4.Enabled = false;
			}
			else if (label13.Text.Equals("Remitado"))
			{
				button2.Enabled = false;
				button3.Enabled = false;
				button4.Enabled = false;
			}
			else if (label13.Text.Equals("Facturado"))
			{
				button2.Enabled = false;
				button3.Enabled = false;
			}
			else if (label13.Text.Equals("Despachado"))
			{
				button2.Enabled = false;
				button3.Enabled = false;
				button4.Enabled = false;
			}
			else if (label13.Text.Equals("Anulado"))
			{
				button2.Enabled = false;
				button3.Enabled = false;
				button4.Enabled = false;
			}
			dataGridView1.ClearSelection();
			numeroRegistros();
		}






		//BOTON SALIR
        private void button1_Click(object sender, EventArgs e)
        {
			this.Close();
        }








		//BOTON ANULAR
        private void button3_Click(object sender, EventArgs e)
        {
			MessageBoxButtons botones = MessageBoxButtons.YesNo;
			DialogResult dr = MessageBox.Show("Desea anular el pedido", "Anular pedido",
				botones, MessageBoxIcon.Question);

			if (dr == DialogResult.Yes)
			{
				conn.Open();
				string sql = "update pedido set idEstadoPedido=7 where numero=@numero";
				SqlCommand comando = new SqlCommand(sql, conn);
				comando.Parameters.AddWithValue("@numero", pedido);
				comando.ExecuteNonQuery();
				conn.Close();
				MessageBox.Show("Pedido anulado");
				this.Close();
			}
			else if (dr == DialogResult.No)
			{
				return;
			}

		}



		//BOTON REGISTRAR FECHA DE SALIDA
        private void button4_Click(object sender, EventArgs e)
        {
			MessageBoxButtons botones = MessageBoxButtons.YesNo;
			DialogResult dr = MessageBox.Show("Desea registrar la fecha de salida", "Registrar envio del pedido",
				botones, MessageBoxIcon.Question);

			if (dr == DialogResult.Yes)
			{
				conn.Open();
				string sql = "update pedido set fechaSalidaDePedido=@fechaSalidaDePedido, idEstadoPedido=6 where numero=@numero";
				SqlCommand comando = new SqlCommand(sql, conn);
				comando.Parameters.AddWithValue("@fechaSalidaDePedido", dateTimePicker1.Value);
				comando.Parameters.AddWithValue("@numero", pedido);
				comando.ExecuteNonQuery();
				conn.Close();
				MessageBox.Show("Fecha de salida registada");
				this.Close();
			}
			else if (dr == DialogResult.No)
			{
				return;
			}
		}



		//BOTON MODIFICAR 
        private void button2_Click(object sender, EventArgs e)
        {
			//DEBE LLEVAR  A UNA PANTALLA NUEVA DONDE SE HAGA EL UPDATE SOLO A LAS  CANTIDADES y la forma de pago.
			int numeroP = Convert.ToInt32(label14.Text);
			ModificarPedido mp = new ModificarPedido(this, numeroP);
			mp.ShowDialog(this);
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
