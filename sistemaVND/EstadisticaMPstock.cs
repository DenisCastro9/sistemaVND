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
    public partial class EstadisticaMPstock : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public EstadisticaMPstock()
        {
            InitializeComponent();
            cargarComboTipoMP();
            chart2.Series[0].LegendText = "";
        }


        private void cargarComboTipoMP()
        {
            SqlCommand cm = new SqlCommand("select * from tipoMP", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr.GetString(1)); //veo la descripcion del tipo MP
            }
            conexion.Close();
        }



        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }




        //BOTON BUSCAR CHART 2: ESTADISTICA DE UN TIPO DE MP: SIMPLE
        private void button1_Click(object sender, EventArgs e)
        {

            chart2.ChartAreas[0].AxisY.Title = "Cantidad";
            chart2.Series["Stock"].Points.Clear();
            if (comboBox3.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Seleccionar un tipo de material en la lista desplegable", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string query = "SELECT  mp.descripcion as 'Materia prima', mp.cantidad as Stock, u.descripcion as UnidadM FROM materiaPrima as mp join tipoMP as t on mp.idtipoMP=t.id join unidadDeMedida as u on mp.IdUnidadDeMedida = u.id where t.descripcion = @nombre ORDER BY cantidad DESC";
                List<string> nombresArticulos = new List<string>();
                List<int> cantidadesEnStock = new List<int>();
                conexion.Open();
                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@nombre", comboBox3.Text);
                SqlDataReader reader = command.ExecuteReader();
                int cantidadExistenciasTotal = 0;
                string unidadDeMedida = "";
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string nombreArticulo = reader.GetString(0);
                        int cantidadEnStock = reader.GetInt32(1);
                        unidadDeMedida = reader.GetString(2);
                        cantidadExistenciasTotal += cantidadEnStock;
                        nombresArticulos.Add(nombreArticulo);
                        cantidadesEnStock.Add(cantidadEnStock);
                    }
                }
                label4.Text = cantidadExistenciasTotal.ToString() + " " + unidadDeMedida + "s";
                reader.Close();
                conexion.Close();

                chart2.Series["Stock"].ChartType = SeriesChartType.Column;

                decimal totalStock = cantidadesEnStock.Sum(); // Obtener el total de stock para calcular los porcentajes

                for (int i = 0; i < nombresArticulos.Count; i++)
                {
                    chart2.Series["Stock"].Points.AddXY(nombresArticulos[i], cantidadesEnStock[i]);

                    // Calcular el porcentaje y mostrarlo sobre la barra
                    double porcentaje = (double)(cantidadesEnStock[i] / totalStock) * 100;
                    chart2.Series["Stock"].Points[i].SetCustomProperty("BarLabelStyle", "Center");
                    chart2.Series["Stock"].Points[i].Label = $"{porcentaje:F2}%";
                }

                chart2.ChartAreas[0].AxisX.Interval = 1;
                chart2.ChartAreas[0].AxisX.IntervalOffset = 0;
            }

            /*
            chart2.ChartAreas[0].AxisY.Title = "Cantidad";
            chart2.Series["Stock"].Points.Clear();
            if (comboBox3.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Seleccionar un tipo de material en la lista desplegable", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else {
                string query = "SELECT  mp.descripcion as 'Materia prima', mp.cantidad as Stock, u.descripcion as UnidadM FROM materiaPrima as mp join tipoMP as t on mp.idtipoMP=t.id join unidadDeMedida as u on mp.IdUnidadDeMedida = u.id where t.descripcion = @nombre ORDER BY cantidad DESC";
                List<string> nombresArticulos = new List<string>();
                List<int> cantidadesEnStock = new List<int>();
                conexion.Open();
                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@nombre", comboBox3.Text);
                SqlDataReader reader = command.ExecuteReader();
                int cantidadExistenciasTotal = 0;
                string unidadDeMedida = "";
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string nombreArticulo = reader.GetString(0);
                        int cantidadEnStock = reader.GetInt32(1);
                        unidadDeMedida = reader.GetString(2);
                        cantidadExistenciasTotal += cantidadEnStock;
                        nombresArticulos.Add(nombreArticulo);
                        cantidadesEnStock.Add(cantidadEnStock);
                    }
                }
                label4.Text = cantidadExistenciasTotal.ToString() + " " + unidadDeMedida+"s";
                reader.Close();
                conexion.Close();
                for (int i = 0; i < nombresArticulos.Count; i++)
                {
                    chart2.Series["Stock"].Points.AddXY(nombresArticulos[i], cantidadesEnStock[i]);
                }
                chart2.Series["Stock"].ChartType = SeriesChartType.Column;
                chart2.ChartAreas[0].AxisX.Interval = 1;
                chart2.ChartAreas[0].AxisX.IntervalOffset = 0;
            }

            */
        }













        //AL CARGAR LA PANTALLA CARGA EL CHART 1: RANKING ARTICULOS: DOBLE
        private void EstadisticaMPstock_Load(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {           
                
                chart1.ChartAreas[0].AxisY.Title = "Cantidad";
                string query = "SELECT TOP 10 descripcion, cantidad, reservado FROM materiaPrima ORDER BY cantidad DESC";
                SqlCommand command = new SqlCommand(query, conexion);
                conexion.Open();
                SqlDataReader reader = command.ExecuteReader();
                chart1.Series.Clear();
                Series seriesStock = chart1.Series.Add("Stock");
                Series seriesReservado = chart1.Series.Add("Reservado");
                while (reader.Read())
                {
                    string nombreArticulo = reader.GetString(0);
                    int cantidadEnStock = reader.GetInt32(1);
                    int cantidadReservada = reader.GetInt32(2);

                    seriesStock.Points.AddXY(nombreArticulo, cantidadEnStock);
                    seriesReservado.Points.AddXY(nombreArticulo, cantidadReservada);
                }
                reader.Close();
                conexion.Close();
                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.IntervalOffset = 0;


                /*chart1.ChartAreas[0].AxisY.Title = "Cantidad";
                string query = "SELECT TOP 10 descripcion, cantidad, reservado FROM materiaPrima ORDER BY cantidad DESC";
                SqlCommand command = new SqlCommand(query, conexion);
                conexion.Open();
                SqlDataReader reader = command.ExecuteReader();
                chart1.Series.Clear();
                Series seriesStock = chart1.Series.Add("Stock");
                Series seriesReservado = chart1.Series.Add("Reservado");
                while (reader.Read())
                {
                    string nombreArticulo = reader.GetString(0);
                    int cantidadEnStock = reader.GetInt32(1);
                    int cantidadReservada = reader.GetInt32(2);

                    seriesStock.Points.AddXY(nombreArticulo, cantidadEnStock);
                    seriesReservado.Points.AddXY(nombreArticulo, cantidadReservada);
                }
                reader.Close();
                conexion.Close();

                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.IntervalOffset = 0;

                // Calcular el porcentaje y mostrarlo sobre cada barra del Stock
                double totalStock = seriesStock.Points.Sum(p => p.YValues[0]);
                foreach (DataPoint dataPoint in seriesStock.Points)
                {
                    double porcentaje = (dataPoint.YValues[0] / totalStock) * 100;
                    dataPoint.SetCustomProperty("BarLabelStyle", "Center");
                    dataPoint.Label = $"{porcentaje:F2}%";
                }

                // Calcular el porcentaje y mostrarlo sobre cada barra del Reservado
                double totalReservado = seriesReservado.Points.Sum(p => p.YValues[0]);
                foreach (DataPoint dataPoint in seriesReservado.Points)
                {
                    double porcentaje = (dataPoint.YValues[0] / totalReservado) * 100;
                    dataPoint.SetCustomProperty("BarLabelStyle", "Center");
                    dataPoint.Label = $"{porcentaje:F2}%";
                }*/
            }
        }

        //CHECKEADO EN MAS RESERVADO
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
            if (radioButton2.Checked == true)
            {
               

                
                chart1.ChartAreas[0].AxisY.Title = "Cantidad";
                string query = "SELECT TOP 10 descripcion, cantidad, reservado FROM materiaPrima ORDER BY reservado DESC";
                SqlCommand command = new SqlCommand(query, conexion);
                conexion.Open();
                SqlDataReader reader = command.ExecuteReader();
                chart1.Series.Clear();
                Series seriesStock = chart1.Series.Add("Stock");
                Series seriesReservado = chart1.Series.Add("Reservado");
                while (reader.Read())
                {
                    string nombreArticulo = reader.GetString(0);
                    int cantidadEnStock = reader.GetInt32(1);
                    int cantidadReservada = reader.GetInt32(2);

                    seriesStock.Points.AddXY(nombreArticulo, cantidadEnStock);
                    seriesReservado.Points.AddXY(nombreArticulo, cantidadReservada);
                }
                reader.Close();
                conexion.Close();
                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.IntervalOffset = 0;


                /* chart1.ChartAreas[0].AxisY.Title = "Cantidad";
                string query = "SELECT TOP 10 descripcion, cantidad, reservado FROM materiaPrima ORDER BY reservado DESC";
                SqlCommand command = new SqlCommand(query, conexion);
                conexion.Open();
                SqlDataReader reader = command.ExecuteReader();
                chart1.Series.Clear();
                Series seriesStock = chart1.Series.Add("Stock");
                Series seriesReservado = chart1.Series.Add("Reservado");
                while (reader.Read())
                {
                    string nombreArticulo = reader.GetString(0);
                    int cantidadEnStock = reader.GetInt32(1);
                    int cantidadReservada = reader.GetInt32(2);

                    seriesStock.Points.AddXY(nombreArticulo, cantidadEnStock);
                    seriesReservado.Points.AddXY(nombreArticulo, cantidadReservada);
                }
                reader.Close();
                conexion.Close();

                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.IntervalOffset = 0;

                // Calcular el porcentaje y mostrarlo sobre cada barra del Stock
                double totalStock = seriesStock.Points.Sum(p => p.YValues[0]);
                foreach (DataPoint dataPoint in seriesStock.Points)
                {
                    double porcentaje = (dataPoint.YValues[0] / totalStock) * 100;
                    dataPoint.SetCustomProperty("BarLabelStyle", "Center");
                    dataPoint.Label = $"{porcentaje:F2}%";
                }

                // Calcular el porcentaje y mostrarlo sobre cada barra del Reservado
                double totalReservado = seriesReservado.Points.Sum(p => p.YValues[0]);
                foreach (DataPoint dataPoint in seriesReservado.Points)
                {
                    double porcentaje = (dataPoint.YValues[0] / totalReservado) * 100;
                    dataPoint.SetCustomProperty("BarLabelStyle", "Center");
                    dataPoint.Label = $"{porcentaje:F2}%";
                }*/
            }

        }

        //CHEKEADO EN MAS STOCK
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           
            if (radioButton1.Checked == true)
            {
                

                
                chart1.ChartAreas[0].AxisY.Title = "Cantidad";
                string query = "SELECT TOP 10 descripcion, cantidad, reservado FROM materiaPrima ORDER BY cantidad DESC";
                SqlCommand command = new SqlCommand(query, conexion);
                conexion.Open();
                SqlDataReader reader = command.ExecuteReader();

                chart1.Series.Clear();
                Series seriesStock = chart1.Series.Add("Stock");
                Series seriesReservado = chart1.Series.Add("Reservado");
                while (reader.Read())
                {
                    string nombreArticulo = reader.GetString(0);
                    int cantidadEnStock = reader.GetInt32(1);
                    int cantidadReservada = reader.GetInt32(2);

                    seriesStock.Points.AddXY(nombreArticulo, cantidadEnStock);
                    seriesReservado.Points.AddXY(nombreArticulo, cantidadReservada);
                }
                reader.Close();
                conexion.Close();
                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.IntervalOffset = 0;


                /*chart1.ChartAreas[0].AxisY.Title = "Cantidad";
                string query = "SELECT TOP 10 descripcion, cantidad, reservado FROM materiaPrima ORDER BY cantidad DESC";
                SqlCommand command = new SqlCommand(query, conexion);
                conexion.Open();
                SqlDataReader reader = command.ExecuteReader();

                chart1.Series.Clear();
                Series seriesStock = chart1.Series.Add("Stock");
                Series seriesReservado = chart1.Series.Add("Reservado");
                while (reader.Read())
                {
                    string nombreArticulo = reader.GetString(0);
                    int cantidadEnStock = reader.GetInt32(1);
                    int cantidadReservada = reader.GetInt32(2);

                    seriesStock.Points.AddXY(nombreArticulo, cantidadEnStock);
                    seriesReservado.Points.AddXY(nombreArticulo, cantidadReservada);
                }
                reader.Close();
                conexion.Close();

                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.IntervalOffset = 0;

                // Calcular el porcentaje y mostrarlo sobre cada barra del Stock
                double totalStock = seriesStock.Points.Sum(p => p.YValues[0]);
                foreach (DataPoint dataPoint in seriesStock.Points)
                {
                    double porcentaje = (dataPoint.YValues[0] / totalStock) * 100;
                    dataPoint.SetCustomProperty("BarLabelStyle", "Center");
                    dataPoint.Label = $"{porcentaje:F2}%";
                }

                // Calcular el porcentaje y mostrarlo sobre cada barra del Reservado
                double totalReservado = seriesReservado.Points.Sum(p => p.YValues[0]);
                foreach (DataPoint dataPoint in seriesReservado.Points)
                {
                    double porcentaje = (dataPoint.YValues[0] / totalReservado) * 100;
                    dataPoint.SetCustomProperty("BarLabelStyle", "Center");
                    dataPoint.Label = $"{porcentaje:F2}%";
                }*/
            }
        }

        //CHECKEADO EN DEBAJO DEL STOCK MINIMO
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            
            if (radioButton3.Checked == true)
            {
               

                
                chart1.ChartAreas[0].AxisY.Title = "Cantidad";
                string query = "SELECT descripcion, cantidad, stockMinimo FROM materiaPrima WHERE cantidad < stockMinimo ORDER BY stockMinimo ASC";
                SqlCommand command = new SqlCommand(query, conexion);
                conexion.Open();
                SqlDataReader reader = command.ExecuteReader();

                chart1.Series.Clear();
                Series seriesStock = chart1.Series.Add("Stock");
                Series seriesReservado = chart1.Series.Add("Stock minimo");
                while (reader.Read())
                {
                    string nombreArticulo = reader.GetString(0);
                    int cantidadEnStock = reader.GetInt32(1);
                    int cantidadReservada = reader.GetInt32(2);

                    seriesStock.Points.AddXY(nombreArticulo, cantidadEnStock);
                    seriesReservado.Points.AddXY(nombreArticulo, cantidadReservada);
                }
                reader.Close();
                conexion.Close();
                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.IntervalOffset = 0;


                /* chart1.Series.Clear();
                chart1.ChartAreas[0].AxisY.Title = "Cantidad";

                // Consulta para obtener el stock actual
                string queryStock = "SELECT TOP 10 descripcion, cantidad FROM materiaPrima WHERE cantidad >= stockMinimo ORDER BY cantidad DESC";

                // Consulta para obtener el stock mínimo
                string queryStockMinimo = "SELECT TOP 10 descripcion, stockMinimo FROM materiaPrima WHERE cantidad < stockMinimo ORDER BY stockMinimo ASC";

                conexion.Open();

                // Obtener datos de stock actual
                SqlCommand commandStock = new SqlCommand(queryStock, conexion);
                SqlDataReader readerStock = commandStock.ExecuteReader();
                Series seriesStock = chart1.Series.Add("Stock disponible");
                seriesStock.ChartType = SeriesChartType.Column;

                while (readerStock.Read())
                {
                    string nombreArticulo = readerStock.GetString(0);
                    int cantidadEnStock = readerStock.GetInt32(1);

                    seriesStock.Points.AddXY(nombreArticulo, cantidadEnStock);
                }

                readerStock.Close();

                // Obtener datos de stock mínimo
                SqlCommand commandStockMinimo = new SqlCommand(queryStockMinimo, conexion);
                SqlDataReader readerStockMinimo = commandStockMinimo.ExecuteReader();
                Series seriesStockMinimo = chart1.Series.Add("Stock mínimo");
                seriesStockMinimo.ChartType = SeriesChartType.Column;

                while (readerStockMinimo.Read())
                {
                    string nombreArticulo = readerStockMinimo.GetString(0);
                    int stockMinimo = readerStockMinimo.GetInt32(1);

                    seriesStockMinimo.Points.AddXY(nombreArticulo, stockMinimo);
                }

                readerStockMinimo.Close();

                conexion.Close();

                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.IntervalOffset = 0;*/
            }
        }
    }
}
