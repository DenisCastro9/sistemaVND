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

using System.Windows.Forms.DataVisualization.Charting;

namespace sistemaVND
{
    public partial class EstadisticaClientesVentas : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public EstadisticaClientesVentas()
        {
            InitializeComponent();
            dataGridView2.ClearSelection();
            cargarDataCliente();

            DateTime fechaActual = DateTime.Now;
            dateTimePicker2.Value = fechaActual.AddDays(1);
            DateTime fechaPorDefecto = fechaActual.AddDays(-7);
            dateTimePicker1.Value = fechaPorDefecto;

        }

        //CARGA TABLA CON CLIENTES MAS FRECUENTES
        private void cargarDataCliente()
        {
            conexion.Open();
            string sql = "SELECT TOP 10 c.razonSocial as Cliente, COUNT(*) AS totalPedidos FROM pedido as p join cliente as c on p.idCliente=c.id where p.idEstadoPedido=6 GROUP BY c.razonSocial ORDER BY totalPedidos DESC";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView2.Rows.Clear();
            while (registros.Read())
            {
                dataGridView2.Rows.Add(registros["Cliente"].ToString(),
                                       registros["totalPedidos"].ToString());
            }
            conexion.Close();
        }

        //CARGAR CHART 1: ESTADISTICA CLIENTES GENERAL: SIMPLE
        private void EstadisticaClientesVentas_Load(object sender, EventArgs e)
        {
            button1_Click(sender, e);
            chart1.ChartAreas[0].AxisY.Title = "Cantidad";
            string query = "SELECT TOP 10 c.razonSocial as Cliente, COUNT(*) AS totalPedidos, SUM(p.importeTotal) AS sumaImportes FROM pedido as p join cliente as c on p.idCliente=c.id where p.idEstadoPedido=6 GROUP BY c.razonSocial ORDER BY totalPedidos DESC";
            conexion.Open();
            SqlCommand command = new SqlCommand(query, conexion);
            SqlDataReader reader = command.ExecuteReader();

            List<string> nombresClientes = new List<string>();
            List<int> cantidadPedidos = new List<int>();
            List<decimal> importeTotal = new List<decimal>();

            while (reader.Read())
            {
                string nombreCliente = reader["Cliente"].ToString();
                int cantidad = Convert.ToInt32(reader["totalPedidos"]);
                decimal sumaImportes = Convert.ToDecimal(reader["sumaImportes"]);
                nombresClientes.Add(nombreCliente);
                cantidadPedidos.Add(cantidad);
                importeTotal.Add(sumaImportes);
            }

            reader.Close();
            conexion.Close();

            label9.Text = cantidadPedidos.Sum().ToString();
            label11.Text = "$ " + importeTotal.Sum().ToString();

            chart1.Series.Clear();
            chart1.Series.Add("Ventas");
            chart1.Series["Ventas"].ChartType = SeriesChartType.Column;

            decimal totalPedidos = cantidadPedidos.Sum(); // Obtener el total de pedidos para calcular los porcentajes

            for (int i = 0; i < nombresClientes.Count; i++)
            {
                chart1.Series["Ventas"].Points.AddXY(nombresClientes[i], cantidadPedidos[i]);

                // Calcular el porcentaje y mostrarlo sobre la barra
                double porcentaje = (double)(cantidadPedidos[i] / totalPedidos) * 100;
                chart1.Series["Ventas"].Points[i].SetCustomProperty("BarLabelStyle", "Center");
                chart1.Series["Ventas"].Points[i].Label = $"{porcentaje:F2}%";
            }

            chart1.Series["Ventas"].ChartType = SeriesChartType.Column;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.IntervalOffset = 0;

            /*
           

            chart1.ChartAreas[0].AxisY.Title = "Cantidad";
            string query = "SELECT TOP 10 c.razonSocial as Cliente, COUNT(*) AS totalPedidos, SUM(p.importeTotal) AS sumaImportes FROM pedido as p join cliente as c on p.idCliente=c.id where p.idEstadoPedido=6 GROUP BY c.razonSocial ORDER BY totalPedidos DESC";
            conexion.Open();
            SqlCommand command = new SqlCommand(query, conexion);
            SqlDataReader reader = command.ExecuteReader();

            List<string> nombresClientes = new List<string>();
            List<int> cantidadPedidos = new List<int>();
            List<decimal> importeTotal = new List<decimal>();

            while (reader.Read())
                {
                string nombreCliente = reader["Cliente"].ToString();
                int cantidad = Convert.ToInt32(reader["totalPedidos"]);
                decimal sumaImportes = Convert.ToDecimal(reader["sumaImportes"]);
                nombresClientes.Add(nombreCliente);
                cantidadPedidos.Add(cantidad);
                importeTotal.Add(sumaImportes);
            }
            
            reader.Close();
            conexion.Close();

            label9.Text = cantidadPedidos.Sum().ToString();
            label11.Text = "$ "+ importeTotal.Sum().ToString();

            chart1.Series.Clear();
            chart1.Series.Add("Ventas");
            chart1.Series["Ventas"].ChartType = SeriesChartType.Column;

            for (int i = 0; i < nombresClientes.Count; i++)
            {
                chart1.Series["Ventas"].Points.AddXY(nombresClientes[i], cantidadPedidos[i]);
            }
            chart1.Series["Ventas"].ChartType = SeriesChartType.Column;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.IntervalOffset = 0;
            */

            dataGridView2.ClearSelection();
        }


        //DOBLE CLICK EN TABLA CLIENTE LLEVA A LA FICHA DEL MISMO.
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Verificar que e.RowIndex y e.ColumnIndex sean válidos.
            {
                string cliente = dataGridView2.Rows[e.RowIndex].Cells["Cliente"].Value.ToString();
                FichaDeCliente form2 = new FichaDeCliente(cliente);
                form2.ShowDialog(this);
            }
        }











        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }





        //BOTON BUSCAR: CHART 2 VENTAS REALIZADAS A CLIENTES ENTRE FECCHAS: SIMPLE
        private void button1_Click(object sender, EventArgs e)
        {
            chart2.ChartAreas[0].AxisY.Title = "Cantidad";
            DateTime fechaInicial = dateTimePicker1.Value.Date;
            DateTime fechaFinal = dateTimePicker2.Value.Date.AddDays(1);

            conexion.Open();
            string sqlQuery = "SELECT c.razonSocial AS Cliente, COUNT(*) AS totalPedidos, SUM(p.importeTotal) AS sumaImportes FROM pedido AS p JOIN cliente AS c ON p.idCliente = c.id WHERE p.idEstadoPedido = 6 AND p.fechaSalidaDePedido >= @fechaInicial AND p.fechaSalidaDePedido < @fechaFinal GROUP BY c.razonSocial ORDER BY totalPedidos DESC";
            SqlCommand command = new SqlCommand(sqlQuery, conexion);
            command.Parameters.AddWithValue("@fechaInicial", fechaInicial);
            command.Parameters.AddWithValue("@fechaFinal", fechaFinal);
            SqlDataReader reader = command.ExecuteReader();

            List<string> nombresClientes = new List<string>();
            List<int> cantidadPedidos = new List<int>();
            List<decimal> importeTotal = new List<decimal>();

            while (reader.Read())
            {
                string nombreCliente = reader["Cliente"].ToString();
                int cantidad = Convert.ToInt32(reader["totalPedidos"]);
                decimal sumaImportes = Convert.ToDecimal(reader["sumaImportes"]);
                nombresClientes.Add(nombreCliente);
                cantidadPedidos.Add(cantidad);
                importeTotal.Add(sumaImportes);
            }

            reader.Close();
            conexion.Close();

            label17.Text = cantidadPedidos.Sum().ToString();
            label15.Text = "$ " + importeTotal.Sum().ToString();

            chart2.Series.Clear();
            chart2.Series.Add("Ventas");
            chart2.Series["Ventas"].ChartType = SeriesChartType.Column;

            decimal totalPedidos = cantidadPedidos.Sum(); // Obtener el total de pedidos para calcular los porcentajes

            for (int i = 0; i < nombresClientes.Count; i++)
            {
                chart2.Series["Ventas"].Points.AddXY(nombresClientes[i], cantidadPedidos[i]);

                // Calcular el porcentaje y mostrarlo sobre la barra
                double porcentaje = (double)(cantidadPedidos[i] / totalPedidos) * 100;
                chart2.Series["Ventas"].Points[i].SetCustomProperty("BarLabelStyle", "Center");
                chart2.Series["Ventas"].Points[i].Label = $"{porcentaje:F2}%";
            }

            chart2.Series["Ventas"].ChartType = SeriesChartType.Column;
            chart2.ChartAreas[0].AxisX.Interval = 1;
            chart2.ChartAreas[0].AxisX.IntervalOffset = 0;

            cargarDataPedidos();
            dataGridView1.ClearSelection();

            conexion.Close();

            /*
            chart2.ChartAreas[0].AxisY.Title = "Cantidad";
            DateTime fechaInicial = dateTimePicker1.Value.Date;
            DateTime fechaFinal = dateTimePicker2.Value.Date.AddDays(1);

            conexion.Open();
            string sqlQuery = "SELECT c.razonSocial AS Cliente, COUNT(*) AS totalPedidos, SUM(p.importeTotal) AS sumaImportes FROM pedido AS p JOIN cliente AS c ON p.idCliente = c.id WHERE p.idEstadoPedido = 6 AND p.fechaSalidaDePedido >= @fechaInicial AND p.fechaSalidaDePedido < @fechaFinal GROUP BY c.razonSocial ORDER BY totalPedidos DESC";
            SqlCommand command = new SqlCommand(sqlQuery, conexion);
            command.Parameters.AddWithValue("@fechaInicial", fechaInicial);
            command.Parameters.AddWithValue("@fechaFinal", fechaFinal);
            SqlDataReader reader = command.ExecuteReader();

            List<string> nombresClientes = new List<string>();
            List<int> cantidadPedidos = new List<int>();
            List<decimal> importeTotal = new List<decimal>();

            while (reader.Read())
            {
                string nombreCliente = reader["Cliente"].ToString();
                int cantidad = Convert.ToInt32(reader["totalPedidos"]);
                decimal sumaImportes = Convert.ToDecimal(reader["sumaImportes"]);
                nombresClientes.Add(nombreCliente);
                cantidadPedidos.Add(cantidad);
                importeTotal.Add(sumaImportes);
            }

            reader.Close();
            conexion.Close();

            label17.Text = cantidadPedidos.Sum().ToString();
            label15.Text = "$ " + importeTotal.Sum().ToString();

            chart2.Series.Clear();
            chart2.Series.Add("Ventas");
            chart2.Series["Ventas"].ChartType = SeriesChartType.Column;

            for (int i = 0; i < nombresClientes.Count; i++)
            {
                chart2.Series["Ventas"].Points.AddXY(nombresClientes[i], cantidadPedidos[i]);
            }
            chart2.Series["Ventas"].ChartType = SeriesChartType.Column;
            chart2.ChartAreas[0].AxisX.Interval = 1;
            chart2.ChartAreas[0].AxisX.IntervalOffset = 0;

            cargarDataPedidos();
            dataGridView1.ClearSelection();

            conexion.Close();
            */
        }


        private void cargarDataPedidos()
        {
            DateTime fechaInicio = dateTimePicker1.Value;
            DateTime fechaFin = dateTimePicker2.Value;
            conexion.Open();
            string sql = "select p.numero as Número, p.fechaSalidaDePedido as 'Fecha de salida', c.razonSocial as Cliente from pedido as p join cliente as c on p.idCliente=c.id where p.idEstadoPedido = 6 AND p.fechaSalidaDePedido >= @fechaInicial AND p.fechaSalidaDePedido < @fechaFinal";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@fechaInicial", fechaInicio);
            comando.Parameters.AddWithValue("@fechaFinal", fechaFin);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla;
            dataGridView1.ClearSelection();
            conexion.Close();
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int numero= Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Número"].Value.ToString());
            FichaPedido form2 = new FichaPedido(numero);
            form2.ShowDialog(this);
        }

    }
}
