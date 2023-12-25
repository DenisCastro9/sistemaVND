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

using Aspose.Html.Converters;
using Aspose.Html.Saving;

namespace sistemaVND
{
    public partial class InformesOrdenDeCompra : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public InformesOrdenDeCompra()
        {
            InitializeComponent();
            cargarComboProveedor();

            autoCompletarProveedor();
        }

        private void numeroRegistros()
        {
            int numeroRegistros = dataGridView1.RowCount;
            lblRes.Text = numeroRegistros.ToString();
        }

        private void autoCompletarProveedor()
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            SqlCommand cmd = new SqlCommand("SELECT nombre FROM proveedor", conexion);
            conexion.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                source.Add(reader["nombre"].ToString());
            }

            reader.Close();
            conexion.Close();

            comboBox2.AutoCompleteCustomSource = source;
        }




        private void cargarComboProveedor()
        {
            SqlCommand cm = new SqlCommand("select * from proveedor", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr.GetString(1)); //ese numero representa la posicion del elemento
                //en la tabla de la BD
            }
            conexion.Close();
        }




        //BOTON BUSCAR
        private void button3_Click(object sender, EventArgs e)
        {
            //TODAS LAS ORDENES
            if (radioButton4.Checked == true) 
            {
                conexion.Open();
                string sql = "select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, P.telefono from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC = e.id join proveedor as p on o.idProveedor = p.id";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["telefono"].ToString());
                }

                conexion.Close();
                dataGridView1.ClearSelection();
            } 
            //SOLO REGISTRADAS
            if (radioButton1.Checked == true)
            {
                conexion.Open();
                string sql = "select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, P.telefono from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC = e.id join proveedor as p on o.idProveedor = p.id where o.idEstadoOrdenC = 1";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["telefono"].ToString());
                }

                conexion.Close();
                dataGridView1.ClearSelection();
            }
            //ANULADAS
            if (radioButton3.Checked == true)
            {
                conexion.Open();
                string sql = "select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, P.telefono from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC = e.id join proveedor as p on o.idProveedor = p.id where o.idEstadoOrdenC = 3";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["telefono"].ToString());
                }

                conexion.Close();
                dataGridView1.ClearSelection();
            }
            //RECIBIDAS
            if (radioButton5.Checked == true)
            {
                conexion.Open();
                string sql = "select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, P.telefono from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC = e.id join proveedor as p on o.idProveedor = p.id where o.idEstadoOrdenC = 4";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["telefono"].ToString());
                }

                conexion.Close();
                dataGridView1.ClearSelection();
            }
            //ENVIADAS
            if (radioButton2.Checked == true)
            {
                conexion.Open();
                string sql = "select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, P.telefono from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC = e.id join proveedor as p on o.idProveedor = p.id where o.idEstadoOrdenC = 2";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["telefono"].ToString());
                }

                conexion.Close();
                dataGridView1.ClearSelection();
            }
            //POR PROVEEDOR
            if (radioButton6.Checked == true) 
            {
                conexion.Open();
                string sql = "select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, P.telefono from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC = e.id join proveedor as p on o.idProveedor = p.id where p.nombre = @nombre ";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = comboBox2.Text;
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["telefono"].ToString());
                }

                conexion.Close();
                dataGridView1.ClearSelection();
            }
            //POR FECHA
            if (radioButton7.Checked == true)
            {                
                conexion.Open();
                string sql = "select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, P.telefono from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC = e.id join proveedor as p on o.idProveedor = p.id  where o.fecha between @fechaInicio and @fechafin ";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@fechaInicio", SqlDbType.Date).Value = dateTimePicker1.Value;
                comando.Parameters.Add("@fechafin", SqlDbType.Date).Value = dateTimePicker2.Value;
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["telefono"].ToString());
                }

                conexion.Close();
                dataGridView1.ClearSelection();
            }
            numeroRegistros();
        }






        //GENERAR EL INFORME
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = DateTime.Now.ToString("ddMMyyyy") + ".pdf";


            string html_text = Properties.Resources.plantillaEnvio.ToString();
            html_text = html_text.Replace("@FECHA", DateTime.UtcNow.ToShortDateString());
            string filas = string.Empty;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["estado"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["numero"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["fecha"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["proveedor"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["telefonoProveedor"].Value.ToString() + " </td>";
                filas += "</tr>";
            }
            html_text = html_text.Replace("@FILAS", filas);

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                // Inicializar PdfSaveOptions 
                var options = new PdfSaveOptions();
                // Invoque el método ConvertHTML para convertir el código HTML a PDF
                Converter.ConvertHTML(html_text, ".", options, guardar.FileName);
                MessageBox.Show("PDF generado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }





        private void InformesOrdenDeCompra_Load(object sender, EventArgs e)
        {
            //TODAS LAS ORDENES
            if (radioButton4.Checked == true)
            {
                conexion.Open();
                string sql = "select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, P.telefono from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC = e.id join proveedor as p on o.idProveedor = p.id";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(), 
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["telefono"].ToString());
                }

                conexion.Close();
            }
            dataGridView1.ClearSelection();
            numeroRegistros();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
