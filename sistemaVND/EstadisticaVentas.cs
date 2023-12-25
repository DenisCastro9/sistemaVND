using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;
namespace sistemaVND
{
    public partial class EstadisticaVentas : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public EstadisticaVentas()
        {
            InitializeComponent();
            DateTime fechaActual = DateTime.Now;
            dateTimePicker2.Value = fechaActual.AddDays(1);
            DateTime fechaPorDefecto = fechaActual.AddDays(-7);
            dateTimePicker1.Value = fechaPorDefecto;
        }


        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }








        //CARGA DEL CHART 2: ESTADISTICA MENSUAL
        private void EstadisticaVentas_Load(object sender, EventArgs e)
        {
            button3_Click(sender, e);

            chart2.Titles.Clear();
            chart2.Titles.Add("Ventas por Mes");
            chart2.ChartAreas[0].AxisX.Title = "Mes";
            chart2.ChartAreas[0].AxisY.Title = "Cantidad de Ventas";

            conexion.Open();
            string setLanguageQuery = "SET LANGUAGE 'Spanish'";
            SqlCommand setLanguageCommand = new SqlCommand(setLanguageQuery, conexion);
            setLanguageCommand.ExecuteNonQuery();

            // Obtener el total de ventas antes de entrar al bucle de lectura de datos
            string totalVentasQuery = "SELECT SUM(importeTotal) AS sumaImportes FROM pedido WHERE idEstadoPedido=6";
            SqlCommand totalVentasCommand = new SqlCommand(totalVentasQuery, conexion);
            object totalVentasObj = totalVentasCommand.ExecuteScalar();
            decimal totalVentas = (totalVentasObj != DBNull.Value) ? Convert.ToDecimal(totalVentasObj) : 0;

            string query = "SELECT DATENAME(MONTH, fechaSalidaDePedido) AS Mes, COUNT(*) AS CantidadVentas,  SUM(importeTotal) AS sumaImportes FROM pedido WHERE idEstadoPedido=6 GROUP BY DATENAME(MONTH, fechaSalidaDePedido) ORDER BY DATENAME(MONTH, fechaSalidaDePedido) DESC";
            SqlCommand command = new SqlCommand(query, conexion);
            SqlDataReader reader = command.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Mes", typeof(string));
            dataTable.Columns.Add("Monto Total", typeof(decimal));
            dataTable.Columns.Add("Porcentaje", typeof(double));

            while (reader.Read())
            {
                string mes = reader.GetString(0);
                int cantidadVentas = reader.GetInt32(1);
                decimal montoTotal = reader.GetDecimal(2);

                // Calcular el porcentaje utilizando el total de ventas previamente obtenido
                double porcentaje = (double)(montoTotal / totalVentas) * 100;

                chart2.Series[0].Points.AddXY(mes, cantidadVentas);
                dataTable.Rows.Add(mes, montoTotal, porcentaje);
            }
            chart2.Series[0].LegendText = "Ventas";
            reader.Close();
            conexion.Close();

            dataGridView1.DataSource = dataTable;
            dataGridView1.ClearSelection();

            dataGridView1.CellFormatting += dataGridView1_CellFormatting;

            // Formatear el porcentaje para mostrarlo con dos decimales
            dataGridView1.Columns["Porcentaje"].DefaultCellStyle.Format = "F2";


            /*
            button3_Click(sender, e);


            chart2.Titles.Add("Ventas por Mes");
            chart2.ChartAreas[0].AxisX.Title = "Mes";
            chart2.ChartAreas[0].AxisY.Title = "Cantidad de Ventas";

            conexion.Open();
            string setLanguageQuery = "SET LANGUAGE 'Spanish'";
            SqlCommand setLanguageCommand = new SqlCommand(setLanguageQuery, conexion);
            setLanguageCommand.ExecuteNonQuery();

            string query = "SELECT DATENAME(MONTH, fechaSalidaDePedido) AS Mes, COUNT(*) AS CantidadVentas,  SUM(importeTotal) AS sumaImportes FROM pedido WHERE idEstadoPedido=6 GROUP BY DATENAME(MONTH, fechaSalidaDePedido) ORDER BY DATENAME(MONTH, fechaSalidaDePedido) DESC";
            SqlCommand command = new SqlCommand(query, conexion);
            SqlDataReader reader = command.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Mes", typeof(string));
            dataTable.Columns.Add("Monto Total", typeof(decimal));

            while (reader.Read())
            {
                string mes = reader.GetString(0);
                int cantidadVentas = reader.GetInt32(1);
                decimal montoTotal = reader.GetDecimal(2);

                chart2.Series[0].Points.AddXY(mes, cantidadVentas);
                dataTable.Rows.Add(mes, montoTotal);
            }
            chart2.Series[0].LegendText = "Ventas";
            reader.Close();
            conexion.Close();

            dataGridView1.DataSource = dataTable;
            dataGridView1.ClearSelection();

            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            */
        }




      



        //PRESIONO UNA BARRA Y VEO LOS DATOS A LA DERECHA
        private void chart2_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult hitTestResult = chart2.HitTest(e.X, e.Y);
            if (hitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                DataPoint dataPoint = chart2.Series[0].Points[hitTestResult.PointIndex];
                string mesSeleccionado = dataPoint.AxisLabel;
                label17.Text = mesSeleccionado;

                Dictionary<string, string> mesesTraducidos = new Dictionary<string, string>()
                    {
                         {"Enero", "January"},
                         {"Febrero", "February"},
                         {"Marzo", "March"},
                         {"Abril", "April"},
                         {"Mayo", "May"},
                         {"Junio", "June"},
                         {"Julio", "July"},
                         {"Agosto", "August"},
                         {"Septiembre", "September"},
                         {"Octubre", "October"},
                         {"Noviembre", "November"},
                         {"Diciembre", "December"}
                    };
                string mesTraducido = mesesTraducidos[label17.Text];


                //ACA ME DEBE TRAER LOS DATOS DE ESE MES.
                conexion.Open();
                string promedioFacturadoQuery = "SELECT AVG(importeTotal) as importeTotal FROM pedido WHERE idEstadoPedido=6 AND DATENAME(MONTH, fechaSalidaDePedido) = @mes";
                SqlCommand promedioFacturadoCommand = new SqlCommand(promedioFacturadoQuery, conexion);
                promedioFacturadoCommand.Parameters.AddWithValue("@mes", mesTraducido);
                decimal promedioFacturado = (decimal)promedioFacturadoCommand.ExecuteScalar();


                string totalVentasQuery = "SELECT COUNT(*) as total FROM pedido WHERE idEstadoPedido=6 AND DATENAME(MONTH, fechaSalidaDePedido) = @mes";
                SqlCommand totalVentasCommand = new SqlCommand(totalVentasQuery, conexion);
                totalVentasCommand.Parameters.AddWithValue("@mes", mesTraducido);
                int totalVentas = (int)totalVentasCommand.ExecuteScalar();

                label7.Text = promedioFacturado.ToString("C");
                label2.Text = totalVentas.ToString();
                conexion.Close();

                //TOP 3 CLIENTES MAS FRECUENTES
                chart4.Series[0].LegendText = "Ventas";
                chart4.ChartAreas[0].AxisY.Title = "Cantidad de Ventas";
                conexion.Open();

                string topClientesQuery = "SELECT TOP 3 c.razonSocial, COUNT(*) as Cantidad FROM pedido as p join cliente as c on p.idCliente=c.id WHERE DATENAME(MONTH, fechaSalidaDePedido) = @mes GROUP BY c.razonSocial ORDER BY COUNT(*) DESC";
                SqlCommand topClientesCommand = new SqlCommand(topClientesQuery, conexion);
                topClientesCommand.Parameters.AddWithValue("@mes", mesTraducido);
                SqlDataReader topClientesReader = topClientesCommand.ExecuteReader();

                chart4.Series[0].Points.Clear();

                while (topClientesReader.Read())
                {
                    string cliente = topClientesReader.GetString(0);
                    int cantidad = topClientesReader.GetInt32(1);

                    chart4.Series[0].Points.AddXY(cliente, cantidad);
                }

                topClientesReader.Close();
                conexion.Close();

                //TOP 3 ARTICULOS MAS VENDIDOS
                chart3.Series[0].LegendText = "Artículo";
                chart3.ChartAreas[0].AxisY.Title = "Cantidad facturada";
                conexion.Open();

                string topArticulosQuery = "SELECT TOP 2 a.nombre, SUM(p.importeTotal) as TotalVentas FROM pedido as p join articulo as a on p.idArt=a.codigoArticulo WHERE DATENAME(MONTH, fechaSalidaDePedido) = @mes GROUP BY a.nombre ORDER BY SUM(p.importeTotal) DESC";
                SqlCommand topArticulosCommand = new SqlCommand(topArticulosQuery, conexion);
                topArticulosCommand.Parameters.AddWithValue("@mes", mesTraducido);
                SqlDataReader topArticulosReader = topArticulosCommand.ExecuteReader();

                chart3.Series[0].Points.Clear();

                while (topArticulosReader.Read())
                {
                    string articulo = topArticulosReader.GetString(0);
                    decimal totalVentasRealizadas = topArticulosReader.GetDecimal(1);

                    chart3.Series[0].Points.AddXY(articulo, totalVentasRealizadas);
                }

                topArticulosReader.Close();
                conexion.Close();

            }
        }







        //BOTON BUSCAR: CHART 1: INFORME DE VENTAS ENTRE DOS FECHAS
        private void button3_Click(object sender, EventArgs e)
        {

            chart1.Titles.Clear();
            chart1.Titles.Add("Artículo más vendido");
            DateTime fechaInicio = dateTimePicker1.Value.Date;
            DateTime fechaFin = dateTimePicker2.Value.Date;
            conexion.Open();
            string query = "SELECT a.nombre as articulo, SUM(p.importeTotal) as TotalMonto FROM pedido as p join articulo as a on p.idArt=a.codigoArticulo WHERE p.idEstadoPedido = 6 AND p.fechaSalidaDePedido >= @fechaInicio AND p.fechaSalidaDePedido < @fechaFin GROUP BY a.nombre ORDER BY TotalMonto DESC";
            SqlCommand command = new SqlCommand(query, conexion);
            command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            command.Parameters.AddWithValue("@fechaFin", fechaFin);
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                chart1.Series.Clear();
                Series series = new Series("Ventas");
                series.ChartType = SeriesChartType.Pie;
                foreach (DataRow row in dataTable.Rows)
                {
                    string articulo = row["articulo"].ToString();
                    decimal totalVenta = Convert.ToDecimal(row["TotalMonto"]);
                    series.Points.AddXY(articulo, totalVenta);

                    // Calcular el porcentaje de ventas para el artículo y agregarlo al tooltip
                    double porcentaje = (double)(totalVenta / dataTable.AsEnumerable().Sum(r => r.Field<decimal>("TotalMonto"))) * 100;
                    series.Points.Last().Label = $"{articulo} ({porcentaje:F2}%)";
                }
                chart1.Series.Add(series);
                chart1.Legends[0].Enabled = true;
                chart1.Legends[0].LegendStyle = LegendStyle.Table;
                chart1.Legends[0].TableStyle = LegendTableStyle.Wide;
                chart1.Series[0]["PieLabelStyle"] = "Disabled"; // Desactiva las etiquetas en el gráfico
                chart1.Series[0].IsVisibleInLegend = true; // Muestra los elementos en la leyenda
                chart1.Series[0].Font = new Font("Arial", 8f); // Cambia el tamaño de fuente en la leyenda
                conexion.Close();

                cargarDataGrid();
                traerDatos();
                dataGridView2.ClearSelection();
            }
            else
            {
                MessageBox.Show("No se encontraron datos para el período seleccionado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                conexion.Close();
                dataGridView2.ClearSelection();
            }
        }
        




        private void traerDatos()
        {
            DateTime fechaInicio = dateTimePicker1.Value.Date;
            DateTime fechaFin = dateTimePicker2.Value.Date.AddDays(1);
            conexion.Open();
            string sql = "SELECT COUNT(*) AS totalPedidos, SUM(p.importeTotal) AS sumaImportes FROM pedido as p where p.idEstadoPedido = 6 AND p.fechaSalidaDePedido >= @fechaInicio AND p.fechaSalidaDePedido < @fechaFin ";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            comando.Parameters.AddWithValue("@fechaFin", fechaFin);
            SqlDataReader registros = comando.ExecuteReader();
            if (registros.Read())
            {
                int totalPedidos = registros.GetInt32(0);
                decimal sumaImportes = registros.GetDecimal(1);
                decimal promedioFacturado = sumaImportes / totalPedidos;
                string fechaInicioCorta = fechaInicio.ToShortDateString();
                label23.Text = fechaInicioCorta;
                string fechaFinCorta = fechaFin.ToShortDateString();
                label24.Text = fechaFinCorta;
                label11.Text = "$ " + Math.Round(promedioFacturado, 2).ToString(); // Mostrar el promedio redondeado a 2 decimales
                label9.Text = Convert.ToString(totalPedidos);
                label22.Text = "$ " + Convert.ToString(sumaImportes);
            }
            conexion.Close();
            dataGridView2.ClearSelection();
        }

        private void cargarDataGrid()
        {
            DateTime fechaInicio = dateTimePicker1.Value.Date;
            DateTime fechaFin = dateTimePicker2.Value.Date.AddDays(1);
            conexion.Open();
            string sql = "select p.numero, c.razonSocial, p.importeTotal, p.idArt from pedido as p join cliente as c on p.idCliente=c.id WHERE p.idEstadoPedido = 6 AND p.fechaSalidaDePedido >= @fechaInicio AND p.fechaSalidaDePedido < @fechaFin ";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            comando.Parameters.AddWithValue("@fechaFin", fechaFin);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView2.Rows.Clear();
            while (registros.Read())
            {
                dataGridView2.Rows.Add(registros["numero"].ToString(),
                                       registros["razonSocial"].ToString(),
                                       registros["importeTotal"].ToString());
            }
            conexion.Close();
            dataGridView2.ClearSelection();
        }


        
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Verificar que e.RowIndex y e.ColumnIndex sean válidos.
            {
                int numero = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["numero"].Value.ToString());
                FichaPedido form2 = new FichaPedido(numero);
                form2.ShowDialog(this);
            }
        }



        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Monto Total"].Index && e.RowIndex >= 0)
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal importe))
                {
                    // Formatear el valor agregando el signo "$" y el formato de moneda
                    e.Value = string.Format("{0:C}", importe);
                    e.FormattingApplied = true;
                }
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Porcentaje" && e.Value != null)
            {
                // Verificar que el valor se pueda convertir a un número
                if (double.TryParse(e.Value.ToString(), out double porcentaje))
                {
                    // Formatear el valor como porcentaje con dos decimales y agregar el símbolo "%"
                    e.Value = $"{porcentaje:F2}%";
                    e.FormattingApplied = true; // Indicar que hemos formateado la celda
                }
            }
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView2.Columns["montoTotal"].Index && e.RowIndex >= 0)
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal importeTotal))
                {
                    e.Value = importeTotal.ToString("C2"); // Formatear el valor como moneda
                    e.FormattingApplied = true;
                }
            }
        }
    }
}
