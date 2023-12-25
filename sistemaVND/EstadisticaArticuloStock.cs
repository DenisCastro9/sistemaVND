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
    public partial class EstadisticaArticuloStock : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public EstadisticaArticuloStock()
        {
            InitializeComponent();
            chart2.Series[0].LegendText = "";
            cargarDataRanking();
            dataGridView2.ClearSelection();

            cargarComboArticulo();
            autoComArticulo();


        }

        private void autoComArticulo()
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            SqlCommand cmd = new SqlCommand("SELECT nombre FROM ArticulosGeneral", conexion);
            conexion.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                source.Add(reader["nombre"].ToString());
            }

            reader.Close();
            conexion.Close();

            comboBox3.AutoCompleteCustomSource = source;
        }
        private void cargarComboArticulo()
        {
            SqlCommand cm = new SqlCommand("select * from ArticulosGeneral", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr.GetString(1)); //veo la descripcion del art
            }
            conexion.Close();
        }



        //CARGAR TABLA DEL RANKING
        private void cargarDataRanking()
        {
            conexion.Open();
            string sql = "SELECT TOP 10 CONCAT(nombre, '  ', talle) AS Articulo, cantidadEnStock as Stock, talleNombre as 'Tipo de talle' FROM articulo ORDER BY cantidadEnStock DESC";
            SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView2.DataSource = tabla;           
            conexion.Close();
        }


        //CARGA 1er CHART: RANKING ARTICULOS: SIMPLE
        private void EstadisticaArticuloStock_Load(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisY.Title = "Cantidad";
            string query = "SELECT TOP 10 CONCAT(nombre, '  ', talle) AS Articulo, cantidadEnStock FROM articulo ORDER BY cantidadEnStock DESC";

            List<string> nombresArticulos = new List<string>();
            List<int> cantidadesEnStock = new List<int>();
            conexion.Open();
            SqlCommand command = new SqlCommand(query, conexion);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string nombreArticulo = reader.GetString(0);
                    int cantidadEnStock = reader.GetInt32(1);

                    nombresArticulos.Add(nombreArticulo);
                    cantidadesEnStock.Add(cantidadEnStock);
                }
            }
            reader.Close();

            // Calcular el total de stock para calcular los porcentajes
            decimal totalStock = cantidadesEnStock.Sum();

            for (int i = 0; i < nombresArticulos.Count; i++)
            {
                chart1.Series["Stock"].Points.AddXY(nombresArticulos[i], cantidadesEnStock[i]);

                // Calcular el porcentaje y mostrarlo en cada barra
                double porcentaje = (double)(cantidadesEnStock[i] / totalStock) * 100;
                chart1.Series["Stock"].Points[i].SetCustomProperty("BarLabelStyle", "Outside");
                chart1.Series["Stock"].Points[i].Label = $"{porcentaje:F2}%";
            }

            chart1.Series["Stock"].ChartType = SeriesChartType.Column;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.IntervalOffset = 0;
            dataGridView2.ClearSelection();


            /*
            chart1.ChartAreas[0].AxisY.Title = "Cantidad";
            string query = "SELECT TOP 10 CONCAT(nombre, '  ', talle) AS Articulo, cantidadEnStock FROM articulo ORDER BY cantidadEnStock DESC";

            List<string> nombresArticulos = new List<string>();
            List<int> cantidadesEnStock = new List<int>();
            conexion.Open();                
            SqlCommand command = new SqlCommand(query, conexion);
            SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string nombreArticulo = reader.GetString(0);
                        int cantidadEnStock = reader.GetInt32(1);

                    nombresArticulos.Add(nombreArticulo);
                    cantidadesEnStock.Add(cantidadEnStock);                    
                    }
                }            
            reader.Close();
            for (int i = 0; i < nombresArticulos.Count; i++)
            {
                chart1.Series["Stock"].Points.AddXY(nombresArticulos[i], cantidadesEnStock[i]);
            }
            chart1.Series["Stock"].ChartType = SeriesChartType.Column;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.IntervalOffset = 0;
            dataGridView2.ClearSelection();
            */
        }



        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }
           



        //BOTON BUSCAR CHART 2: ESTADISTICA DE UN ARTICULO SELECCIONADO: SIMPLE
        private void button1_Click(object sender, EventArgs e)
        {
            chart2.ChartAreas[0].AxisY.Title = "Cantidad";
            chart2.Series["Stock"].Points.Clear();
            label4.Text = string.Empty;

            if (comboBox3.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Seleccionar un artículo de la lista desplegable", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string query = "SELECT CONCAT(nombre, '  ', talle) AS Artículo, cantidadEnStock FROM articulo where nombre=@nombre ORDER BY cantidadEnStock DESC ";

                List<string> nombresArticulos = new List<string>();
                List<int> cantidadesEnStock = new List<int>();
                //conexion.Open();
                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@nombre", comboBox3.Text);
                SqlDataReader reader = command.ExecuteReader();

                int cantidadExistenciasTotal = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string nombreArticulo = reader.GetString(0);
                        int cantidadEnStock = reader.GetInt32(1);
                        cantidadExistenciasTotal += cantidadEnStock;
                        nombresArticulos.Add(nombreArticulo);
                        cantidadesEnStock.Add(cantidadEnStock);
                    }
                }
                reader.Close();

                // Calcular el total de stock para calcular los porcentajes
                decimal totalStock = cantidadesEnStock.Sum();

                for (int i = 0; i < nombresArticulos.Count; i++)
                {
                    chart2.Series["Stock"].Points.AddXY(nombresArticulos[i], cantidadesEnStock[i]);

                    // Calcular el porcentaje y mostrarlo en cada barra
                    double porcentaje = (double)(cantidadesEnStock[i] / totalStock) * 100;
                    chart2.Series["Stock"].Points[i].SetCustomProperty("BarLabelStyle", "Outside");
                    chart2.Series["Stock"].Points[i].Label = $"{porcentaje:F2}%";
                }

                chart2.Series["Stock"].ChartType = SeriesChartType.Column;
                chart2.ChartAreas[0].AxisX.Interval = 1;
                chart2.ChartAreas[0].AxisX.IntervalOffset = 0;
                label4.Text = cantidadExistenciasTotal.ToString();
            }

            /*
            chart2.ChartAreas[0].AxisY.Title = "Cantidad";
            chart2.Series["Stock"].Points.Clear();
            label4.Text = string.Empty;

            if (comboBox3.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Seleccionar un artículo de la lista desplegable", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string query = "SELECT CONCAT(nombre, '  ', talle) AS Artículo, cantidadEnStock FROM articulo where nombre=@nombre ORDER BY cantidadEnStock DESC ";

                List<string> nombresArticulos = new List<string>();
                List<int> cantidadesEnStock = new List<int>();
                //conexion.Open();
                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@nombre", comboBox3.Text);
                SqlDataReader reader = command.ExecuteReader();

                int cantidadExistenciasTotal = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string nombreArticulo = reader.GetString(0);
                        int cantidadEnStock = reader.GetInt32(1);
                        cantidadExistenciasTotal += cantidadEnStock;
                        nombresArticulos.Add(nombreArticulo);
                        cantidadesEnStock.Add(cantidadEnStock);
                    }
                }
                reader.Close();
                for (int i = 0; i < nombresArticulos.Count; i++)
                {
                    chart2.Series["Stock"].Points.AddXY(nombresArticulos[i], cantidadesEnStock[i]);
                }
                chart2.Series["Stock"].ChartType = SeriesChartType.Column;
                chart2.ChartAreas[0].AxisX.Interval = 1;
                chart2.ChartAreas[0].AxisX.IntervalOffset = 0;
                label4.Text= cantidadExistenciasTotal.ToString();
            }
            */
        }



    }
}
