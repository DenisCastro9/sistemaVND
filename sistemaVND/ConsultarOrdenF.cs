using Aspose.Html.Converters;
using Aspose.Html.Saving;
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
    public partial class ConsultarOrdenF : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public ConsultarOrdenF()
        {
            InitializeComponent();

            cargarComboPuntoDeControl();
            autocomPuntoDeControl();
        }

        private void numeroRegistros()
        {
            int numeroRegistros = dataGridView1.RowCount;
            lblRes.Text = numeroRegistros.ToString();
        }



        private void cargarComboPuntoDeControl()
        {
            SqlCommand cm = new SqlCommand("select * from puntoDeControl", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr.GetString(1)); //ese numero representa la posicion del elemento
                //en la tabla de la BD
            }
            conexion.Close();
        }
        private void autocomPuntoDeControl()
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            SqlCommand cmd = new SqlCommand("SELECT descripcion FROM puntoDeControl", conexion);
            conexion.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                source.Add(reader["descripcion"].ToString());
            }

            reader.Close();
            conexion.Close();

            comboBox2.AutoCompleteCustomSource = source;
        }





        //BOTON BUSCAR
        private void button3_Click(object sender, EventArgs e)
        {
            //TODAS LAS ORDENES
            if (radioButton4.Checked == true)
            {
                conexion.Open();
                string sql = "select o.numero, o.fechaEmision, e.descripcion as Estado, p.descripcion as Punto from ordenDeFabricacion as o join estadoOrdenF as e on o.idEstadoOrdenF = e.id join puntoDeControl as p on o.idPuntoDeControl = p.id order by numero asc";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateadaCreacion = DateTime.Parse(registros["fechaEmision"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["numero"].ToString(),
                                           fechaFormateadaCreacion,
                                           registros["Estado"].ToString(),
                                           registros["Punto"].ToString());
                }
                dataGridView1.ClearSelection();
                conexion.Close();
            }
            //SOLO REGISTRADAS
            if (radioButton1.Checked == true)
            {
                conexion.Open();
                string sql = "select o.numero, o.fechaEmision, e.descripcion as Estado, p.descripcion as Punto from ordenDeFabricacion as o join estadoOrdenF as e on o.idEstadoOrdenF=e.id join puntoDeControl as p on o.idPuntoDeControl=p.id where o.idEstadoOrdenF = 1 order by numero asc"; 
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateadaCreacion = DateTime.Parse(registros["fechaEmision"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["numero"].ToString(),
                                           fechaFormateadaCreacion,
                                           registros["Estado"].ToString(),
                                           registros["Punto"].ToString());
                }
                dataGridView1.ClearSelection();
                conexion.Close();
            }
            //EN PROCESO
            if (radioButton3.Checked == true)
            {
                conexion.Open();
                string sql = "select o.numero, o.fechaEmision, e.descripcion as Estado, p.descripcion as Punto from ordenDeFabricacion as o join estadoOrdenF as e on o.idEstadoOrdenF=e.id join puntoDeControl as p on o.idPuntoDeControl=p.id where idEstadoOrdenF = 2 order by numero asc ";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateadaCreacion = DateTime.Parse(registros["fechaEmision"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["numero"].ToString(),
                                           fechaFormateadaCreacion,
                                           registros["Estado"].ToString(),
                                           registros["Punto"].ToString());
                }

                conexion.Close();
            }
            dataGridView1.ClearSelection();
            //ANULADAS
            if (radioButton2.Checked == true)
            {
                conexion.Open();
                string sql = "select o.numero, o.fechaEmision,  e.descripcion as Estado, p.descripcion as Punto from ordenDeFabricacion as o join estadoOrdenF as e on o.idEstadoOrdenF=e.id join puntoDeControl as p on o.idPuntoDeControl=p.id where idEstadoOrdenF = 4 order by numero asc";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateadaCreacion = DateTime.Parse(registros["fechaEmision"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["numero"].ToString(),
                                           fechaFormateadaCreacion,
                                           registros["Estado"].ToString(),
                                           registros["Punto"].ToString());
                }
                dataGridView1.ClearSelection();
                conexion.Close();
            }
            //POR PUNTO DE CONTROL
            if (radioButton6.Checked == true)
            {
                conexion.Open();
                string sql = "select o.numero, o.fechaEmision, e.descripcion as Estado, p.descripcion as Punto from ordenDeFabricacion as o join estadoOrdenF as e on o.idEstadoOrdenF=e.id join puntoDeControl as p on o.idPuntoDeControl=p.id WHERE p.descripcion = @nombre order by numero asc";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = comboBox2.Text;
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateadaCreacion = DateTime.Parse(registros["fechaEmision"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["numero"].ToString(),
                                           fechaFormateadaCreacion,
                                           registros["Estado"].ToString(),
                                           registros["Punto"].ToString());
                }
                dataGridView1.ClearSelection();
                conexion.Close();
            }
            //POR FECHA
            if (radioButton7.Checked == true)
            {
                conexion.Open();
                string sql = "select o.numero, o.fechaEmision, e.descripcion as Estado, p.descripcion as Punto from ordenDeFabricacion as o join estadoOrdenF as e on o.idEstadoOrdenF=e.id join puntoDeControl as p on o.idPuntoDeControl=p.id WHERE o.fechaCreacion between @fechaInicio and @fechafin order by numero asc";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@fechaInicio", SqlDbType.Date).Value = dateTimePicker1.Value;
                comando.Parameters.Add("@fechafin", SqlDbType.Date).Value = dateTimePicker2.Value;
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateadaCreacion = DateTime.Parse(registros["fechaEmision"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["numero"].ToString(),
                                           fechaFormateadaCreacion,
                                           registros["Estado"].ToString(),
                                           registros["Punto"].ToString());
                }
                dataGridView1.ClearSelection();
                conexion.Close();
            }
            //FINALIZADAS
            if (radioButton8.Checked == true)
            {
                conexion.Open();
                string sql = "select o.numero, o.fechaEmision, e.descripcion as Estado, p.descripcion as Punto from ordenDeFabricacion as o join estadoOrdenF as e on o.idEstadoOrdenF=e.id join puntoDeControl as p on o.idPuntoDeControl=p.id where o.idEstadoOrdenF = 5 order by numero asc";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateadaCreacion = DateTime.Parse(registros["fechaEmision"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["numero"].ToString(),
                                           fechaFormateadaCreacion,
                                           registros["Estado"].ToString(),
                                           registros["Punto"].ToString());
                }
                dataGridView1.ClearSelection();
                conexion.Close();
            }
            dataGridView1.ClearSelection();
            numeroRegistros();
        }


  

        private void ConsultarOrdenF_Load(object sender, EventArgs e)
        {
            //TODAS LAS ORDENES
            if (radioButton4.Checked == true)
            {
                conexion.Open();
                string sql = "select o.numero, o.fechaEmision, e.descripcion as Estado, p.descripcion as Punto from ordenDeFabricacion as o join estadoOrdenF as e on o.idEstadoOrdenF = e.id join puntoDeControl as p on o.idPuntoDeControl = p.id order by numero asc";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateadaCreacion = DateTime.Parse(registros["fechaEmision"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["numero"].ToString(),
                                           fechaFormateadaCreacion,
                                           registros["Estado"].ToString(),
                                           registros["Punto"].ToString());
                }

                conexion.Close();
            }
            dataGridView1.ClearSelection();
            numeroRegistros();
        }





        //BOTON GENERAR INFORME
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = DateTime.Now.ToString("ddMMyyyy-OrdenDeFabricacion.pdf") + ".pdf";


            string html_text = Properties.Resources.PlantillaOrdenDeFabricacion.ToString();
            html_text = html_text.Replace("@FECHA", DateTime.UtcNow.ToShortDateString());
            string filas = string.Empty;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["numero"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["fechaEmision"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["estadoOrdenF"].Value.ToString() + "</td>"; //USAR SIEMPRE LOS NOMBRES QUE PUSISTE EN EL DATAGRID
                filas += "<td>" + row.Cells["puntoDeControl"].Value.ToString() + " </td>";
                filas += "</tr>";
            }
            html_text = html_text.Replace("@FILAS", filas);

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                // Inicializar PdfSaveOptions 
                var options = new PdfSaveOptions();
                // Invoque el método ConvertHTML para convertir el código HTML a PDF
                Converter.ConvertHTML(html_text, ".", options, guardar.FileName); //USA SIEMPRE LA SEGUNDA OPCION
                MessageBox.Show("PDF generado");
            }
        }




        //AL HACER DOBLE CLICK EN LA ORDEN
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Verificar que e.RowIndex y e.ColumnIndex sean válidos.
            {
                string codigo = dataGridView1.Rows[e.RowIndex].Cells["Numero"].Value.ToString();
                FichaOrdenFParaConsulta form = new FichaOrdenFParaConsulta(codigo);
                form.ShowDialog(this);
            }
        }


        //BOTON SALIR
        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
