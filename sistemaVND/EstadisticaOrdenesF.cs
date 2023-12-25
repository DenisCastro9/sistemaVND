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
    public partial class EstadisticaOrdenesF : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public EstadisticaOrdenesF()
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



        //BOTON BUSCAR
        private void button3_Click(object sender, EventArgs e)
        {
            //MOSTRAR ARTICULO MAS FABRICADO:

            chart1.Titles.Clear();
            chart1.Titles.Add("Artículo más fabricado");
            DateTime fechaInicio = dateTimePicker1.Value.Date;
            DateTime fechaFin = dateTimePicker2.Value.Date;
            conexion.Open();
            string query = "SELECT top 10 a.nombre AS articulo, SUM(o.totalPares) AS totalPares FROM ordenDeFabricacion AS o JOIN pedido AS p ON o.idPedido = p.numero JOIN articulo AS a ON p.idArt = a.codigoArticulo WHERE o.idEstadoOrdenF = 5 AND o.fechaRealFin >= @fechaInicial AND o.fechaRealFin <= @fechaFinal GROUP BY a.nombre ORDER BY totalPares DESC";
            SqlCommand command = new SqlCommand(query, conexion);
            command.Parameters.AddWithValue("@fechaInicial", fechaInicio);
            command.Parameters.AddWithValue("@fechaFinal", fechaFin);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                chart1.Series.Clear();
                Series series = new Series("Fabricación");
                series.ChartType = SeriesChartType.Pie;

                decimal totalSum = dataTable.AsEnumerable().Sum(row => Convert.ToDecimal(row["totalPares"]));
                dataTable.Columns.Add("Porcentaje", typeof(string)); // Agregar columna para almacenar los porcentajes

                foreach (DataRow row in dataTable.Rows)
                {
                    string articulo = row["articulo"].ToString();
                    decimal totalPares = Convert.ToDecimal(row["totalPares"]);
                    double porcentaje = (double)(totalPares / totalSum) * 100;
                    row["Porcentaje"] = $"{porcentaje:F2}%"; // Almacenar el porcentaje en la nueva columna
                    series.Points.AddXY(articulo, totalPares);
                }

                chart1.Series.Add(series);
                chart1.Legends[0].Enabled = true;
                chart1.Legends[0].LegendStyle = LegendStyle.Table;
                chart1.Legends[0].TableStyle = LegendTableStyle.Wide;
                chart1.Series[0].IsVisibleInLegend = true; // Mostrar elementos en la leyenda
                chart1.Series[0].Font = new Font("Arial", 8f); // Cambiar el tamaño de fuente en la leyenda
                chart1.Series[0].Label = "#VALX\n#PERCENT{P2}"; // Mostrar nombre del artículo y porcentaje en líneas separadas en la leyenda

                series["PieLabelStyle"] = "Disabled";
                conexion.Close();
                cargarDatos();
                cargarDataGrid();
            }
            else
            {
                MessageBox.Show("No se encontraron datos para el período seleccionado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                conexion.Close();
            }


            //MOSTRAR CLIENTE CON MAS ORDENES ASIGNADAS:
            chart2.Titles.Clear();
            chart2.Titles.Add("Cliente con más órdenes fabricadas");
            chart2.ChartAreas[0].AxisY.Title = "Cantidad de órdenes";
            string queryCliente = "SELECT TOP 10 cliente, COUNT(*) AS cantidad FROM ordenDeFabricacion WHERE idEstadoOrdenF=5 AND fechaRealFin >= @fechaInicial AND fechaRealFin <= @fechaFinal  GROUP BY cliente ORDER BY cantidad DESC";
            conexion.Open();
            SqlCommand commandCliente = new SqlCommand(queryCliente, conexion);
            commandCliente.Parameters.AddWithValue("@fechaInicial", fechaInicio);
            commandCliente.Parameters.AddWithValue("@fechaFinal", fechaFin);
            SqlDataReader reader = commandCliente.ExecuteReader();
            List<string> nombresClientes = new List<string>();
            List<int> cantidadOrdenes = new List<int>();
            while (reader.Read())
            {
                string nombreCliente = reader["cliente"].ToString();
                int cantidad = Convert.ToInt32(reader["cantidad"]);
                nombresClientes.Add(nombreCliente);
                cantidadOrdenes.Add(cantidad);
            }
            reader.Close();
            conexion.Close();

            chart2.Series.Clear();
            chart2.Series.Add("Órdenes de fabricación");
            chart2.Series["Órdenes de fabricación"].ChartType = SeriesChartType.Column;


            // Obtener el máximo valor para ajustar el eje Y y limitar la longitud de las barras
            int maxCantidadOrdenes = cantidadOrdenes.Max();
            chart2.ChartAreas[0].AxisY.Maximum = maxCantidadOrdenes * 1.2; // Ajusta el factor de multiplicación según tus necesidades

            // Calcular el total de órdenes para calcular los porcentajes
            decimal totalOrdenes = cantidadOrdenes.Sum();

            for (int i = 0; i < nombresClientes.Count; i++)
            {
                chart2.Series["Órdenes de fabricación"].Points.AddXY(nombresClientes[i], cantidadOrdenes[i]);

                // Calcular el porcentaje y mostrarlo en cada barra
                double porcentaje = (double)(cantidadOrdenes[i] / totalOrdenes) * 100;
                chart2.Series["Órdenes de fabricación"].Points[i].SetCustomProperty("BarLabelStyle", "Center"); // Mostrar la etiqueta en el centro de la barra
                chart2.Series["Órdenes de fabricación"].Points[i].Label = $"{porcentaje:F2}%";
            }

            // Ajustar el gráfico para que las etiquetas del eje X sean legibles
            chart2.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8f);
            chart2.ChartAreas[0].AxisX.LabelStyle.Interval = 1; // Mostrar todas las etiquetas del eje X
            //chart2.ChartAreas[0].AxisX.LabelStyle.Angle = -45; // Rotar las etiquetas

            // Ajustar el ancho de las columnas
            chart2.Series["Órdenes de fabricación"]["PointWidth"] = "0.6"; // Puedes ajustar el valor (entre 0 y 1) para cambiar el ancho de las columnas.


            /*
            decimal totalOrdenes = cantidadOrdenes.Sum(); // Obtener el total de órdenes para calcular los porcentajes
            for (int i = 0; i < nombresClientes.Count; i++)
            {
                chart2.Series["Órdenes de fabricación"].Points.AddXY(nombresClientes[i], cantidadOrdenes[i]);

                // Calcular el porcentaje y mostrarlo en cada barra
                double porcentaje = (double)(cantidadOrdenes[i] / totalOrdenes) * 100;
                chart2.Series["Órdenes de fabricación"].Points[i].SetCustomProperty("BarLabelStyle", "Outside");
                chart2.Series["Órdenes de fabricación"].Points[i].Label = $"{porcentaje:F2}%";
            }
            // Ajustar el gráfico para que las etiquetas del eje X sean legibles
            chart2.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chart2.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8f);
            //chart2.ChartAreas[0].AxisX.IsUserSelectionEnabled = false;

            // Ajustar el ancho de las columnas
            chart2.Series["Órdenes de fabricación"]["PointWidth"] = "0.6"; // Puedes ajustar el valor (entre 0 y 1) para cambiar el ancho de las columnas.
                        
           chart2.Series["Órdenes de fabricación"].ChartType = SeriesChartType.Column;
           chart2.ChartAreas[0].AxisX.Interval = 1;
           chart2.ChartAreas[0].AxisX.IntervalOffset = 0; 
           chart2.Series["Órdenes de fabricación"]["PointWidth"] = "0.6"; // Puedes ajustar el valor (entre 0 y 1) para cambiar el ancho de las columnas.
            */

        }







        private void cargarDataGrid()
        {
            DateTime fechaInicio = dateTimePicker1.Value.Date;
            DateTime fechaFin = dateTimePicker2.Value.Date.AddDays(1);
            conexion.Open();
            string sql = "SELECT o.cliente, COUNT(*) AS cantidadOrdenes, SUM(o.totalPares) AS totalParesFabricados,  SUM(p.importeTotal) AS montoFacturado FROM ordenDeFabricacion as o join pedido as p on o.idPedido=p.numero where o.idEstadoOrdenF=5 AND  o.fechaRealFin >= @fechaInicio AND o.fechaRealFin <= @fechaFin GROUP BY cliente";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            comando.Parameters.AddWithValue("@fechaFin", fechaFin);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView2.Rows.Clear();
            while (registros.Read())
            {
                dataGridView2.Rows.Add(registros["cliente"].ToString(),
                                        registros["cantidadOrdenes"].ToString(),
                                       registros["totalParesFabricados"].ToString(),
                                       registros["montoFacturado"].ToString());
            }
            conexion.Close();
            dataGridView2.ClearSelection();
        }








        private void cargarDatos()
        {
            DateTime fechaInicio = dateTimePicker1.Value.Date;
            DateTime fechaFin = dateTimePicker2.Value.Date.AddDays(1);
            conexion.Open();
            string sql = "SELECT (SELECT SUM(o.totalPares) FROM ordenDeFabricacion AS o JOIN pedido AS p ON o.idPedido = p.numero JOIN articulo AS a ON p.idArt = a.codigoArticulo WHERE o.idEstadoOrdenF = 5 AND o.fechaRealFin >= @fechaInicio AND o.fechaRealFin <= @fechaFin) AS totalParesFabricados, (SELECT COUNT(*) FROM ordenDeFabricacion AS o JOIN pedido AS p ON o.idPedido = p.numero JOIN articulo AS a ON p.idArt = a.codigoArticulo WHERE o.idEstadoOrdenF = 5 AND o.fechaRealFin >= @fechaInicio AND o.fechaRealFin <= @fechaFin) AS totalOrdenesFinalizadas, (SELECT COUNT(*) FROM ordenDeFabricacion AS o WHERE o.idEstadoOrdenF = 4 AND o.fechaRealFin >= @fechaInicio AND o.fechaRealFin <= @fechaFin) AS totalOrdenesAnuladas";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            comando.Parameters.AddWithValue("@fechaFin", fechaFin);
            SqlDataReader registros = comando.ExecuteReader();
            if (registros.Read())
            {
                int totalParesFabricados = registros.GetInt32(0);
                int totalOrdenesFinalizadas = registros.GetInt32(1);
                int totalOrdenesAnuladas = registros.GetInt32(2);

                string fechaInicioCorta = fechaInicio.ToShortDateString();
                label23.Text = fechaInicioCorta;
                string fechaFinCorta = fechaFin.ToShortDateString();
                label24.Text = fechaFinCorta;

                label11.Text = Convert.ToString(totalParesFabricados);
                label9.Text = Convert.ToString(totalOrdenesFinalizadas);
                label22.Text = Convert.ToString(totalOrdenesAnuladas);
            }
            conexion.Close();
        }


        private void EstadisticaOrdenesF_Load(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }







        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView2.Columns["importeFacturado"].Index && e.RowIndex >= 0)
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
