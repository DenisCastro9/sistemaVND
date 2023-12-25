using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Aspose.Html.Converters;
using Aspose.Html.Saving;

namespace sistemaVND
{
    public partial class consultarTransportista : Form
    {
        public consultarTransportista()
        {
            InitializeComponent();
        }

        private void numeroRegistros()
        {
            int numeroRegistros = dataGridView1.RowCount;
            lblRes.Text = numeroRegistros.ToString();
        }

        SqlConnection conexion = new SqlConnection("data source=  DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        SqlDataAdapter adaptador;

        private void consultarTransportista_Load(object sender, EventArgs e)
        {
            cargarDataGrid();
            dataGridView1.ClearSelection();
            numeroRegistros();
        }


        private void cargarDataGrid()
        {
            adaptador = new SqlDataAdapter();
            SqlCommand datos = new SqlCommand("SELECT transportista.nombre, transportista.cuit, transportista.mail, transportista.telefono, domicilio.calle, domicilio.nombreBarrio, localidad.nombreLocalidad, localidad.codigoPostal, provincia.nombreProvincia FROM transportista INNER JOIN domicilio ON transportista.idDomicilio = domicilio.numero" +
                "                                                                                                                                                                                                                                                                    INNER JOIN localidad ON domicilio.idLocalidad = localidad.idLocalidad" +
                "                                                                                                                                                                                                                                                                    INNER JOIN provincia ON domicilio.idProvincia = provincia.idprovincia", conexion);
            adaptador.SelectCommand = datos;
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Nombre";
            dataGridView1.Columns[1].HeaderText = "Cuit";
            dataGridView1.Columns[2].HeaderText = "Mail";
            dataGridView1.Columns[3].HeaderText = "Teléfono";
            dataGridView1.Columns[4].HeaderText = "Calle";
            dataGridView1.Columns[5].HeaderText = "Barrio";
            dataGridView1.Columns[6].HeaderText = "Localidad";
            dataGridView1.Columns[7].HeaderText = "Código Postal";
            dataGridView1.Columns[8].HeaderText = "Provincia";
            dataGridView1.ClearSelection();
            numeroRegistros();
        }



        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            conexion.Open();
            SqlCommand buscar = conexion.CreateCommand();
            buscar.CommandType = CommandType.Text;
            buscar.CommandText = "SELECT transportista.nombre, transportista.cuit, transportista.mail, transportista.telefono, domicilio.calle, domicilio.nombreBarrio, localidad.nombreLocalidad, localidad.codigoPostal, provincia.nombreProvincia FROM transportista INNER JOIN domicilio ON transportista.idDomicilio = domicilio.numero" +
                "                                                                                                                                                                                                                                                                    INNER JOIN localidad ON domicilio.idLocalidad = localidad.idLocalidad" +
                "                                                                                                                                                                                                                                                                    INNER JOIN provincia ON domicilio.idProvincia = provincia.idprovincia WHERE transportista.nombre LIKE ('" + textBox1.Text + "%')";
            buscar.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(buscar);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conexion.Close();
            dataGridView1.ClearSelection();
            numeroRegistros();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = "Generado el " + DateTime.Now.ToString("dd-MM-yyyy") + ".pdf";

            string html_text = Properties.Resources.plantillaListadoTransportistas.ToString();
            html_text = html_text.Replace("@FECHA", DateTime.UtcNow.ToShortDateString());

            string filas = string.Empty;
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["nombre"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["cuit"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["mail"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["telefono"].Value.ToString() + "</td>";
                filas += "</tr>";
            }
            html_text = html_text.Replace("@FILAS", filas);

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                var options = new PdfSaveOptions();
                Converter.ConvertHTML(html_text, ".", options, guardar.FileName);
                MessageBox.Show("PDF Generado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
