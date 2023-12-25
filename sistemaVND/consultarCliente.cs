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
    public partial class consultarCliente : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        private SqlDataAdapter adaptador;

        public consultarCliente()
        {
            InitializeComponent();
            textBox1.TextChanged += textBox1_TextChanged;
            cargarData();
        }


        private void numeroRegistros()
        {
            int numeroRegistros = dataGridView1.RowCount;
            label18.Text = numeroRegistros.ToString();
        }



        private void cargarData()
        {            
            conexion.Open();
            string sql = "select razonSocial as Nombre, cuit as Cuit, condicionIva as 'Condicion I.V.A.', ingresosBrutos as 'Ingresos brutos', telefono as 'Teléfono' from cliente";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla;
            conexion.Close();
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
        private void buscador(string busqueda)
        {
            string sql = " select razonSocial as Nombre, cuit as Cuit, condicionIva as 'Condicion I.V.A.', ingresosBrutos as 'Ingresos brutos', telefono as 'Teléfono' from cliente where razonSocial LIKE @busqueda OR cuit LIKE @busqueda";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla;
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string busqueda = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                cargarData();
            }
            else
            {
                buscador(busqueda);
            }
        }













        //CARGA EL FORM EN CUANTO SE ABRE...
        private void consultarCliente_Load(object sender, EventArgs e)
        {
                      
            dataGridView1.ClearSelection();
            numeroRegistros();
            elimiarCliente();
        }



 


        //BOTON VER
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string cliente = "";

                foreach (DataGridViewRow row in dataGridView1.SelectedRows) //devuelve de acuerdo a lo que se selecciono
                {
                    cliente = row.Cells[0].Value.ToString();//pasa el nombre del cliente
                }
                FichaDeCliente fc = new FichaDeCliente(cliente);
                fc.FormClosed += fc_FormClosed;
                fc.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccionar un cliente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void fc_FormClosed(object sender, FormClosedEventArgs e)
        {
            cargarData();
        }







        //BOTON GENERAR LISTADO
        private void button3_Click(object sender, EventArgs e)
        {

            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = "Generado el " + DateTime.Now.ToString("dd-MM-yyyy") + ".pdf";

            string html_text = Properties.Resources.plantillaListadoClientes.ToString();
            html_text = html_text.Replace("@FECHA", DateTime.UtcNow.ToShortDateString());
            string filas = string.Empty;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Nombre"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cuit"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Condicion I.V.A."].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Ingresos brutos"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Telefono"].Value.ToString() + "</td>";
                filas += "</tr>";
            }
            html_text = html_text.Replace("@FILAS", filas);

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                var options = new PdfSaveOptions();
                Converter.ConvertHTML(html_text, ".", options, guardar.FileName);
                MessageBox.Show("PDF generado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
        }



        //BOTON ELIMINAR
        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {


                MessageBoxButtons botones = MessageBoxButtons.YesNo;
                DialogResult dr = MessageBox.Show("¿Desea eliminar el cliente?", "Eliminar proveedor",
                    botones, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {

                    string clienteCuit = "";

                    foreach (DataGridViewRow row in dataGridView1.SelectedRows) //devuelve de acuerdo a lo que se selecciono
                    {
                        clienteCuit = row.Cells[1].Value.ToString();//pasa el cuit del cliente
                    }
                    adaptador.DeleteCommand.Parameters["@cuit"].Value = clienteCuit;
                    try
                    {
                        conexion.Open();
                        adaptador.DeleteCommand.ExecuteNonQuery();
                        MessageBox.Show("Cliente eliminado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conexion.Close();
                        cargarData();

                    }
                    catch (SqlException excepcion)
                    {
                        MessageBox.Show(excepcion.ToString());
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else if (dr == DialogResult.No)
                {
                            return;
                }
            }
            else
            {
                MessageBox.Show("Seleccionar un cliente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void elimiarCliente()
        {
            SqlCommand eliminar = new SqlCommand("DELETE FROM cliente WHERE cliente.cuit = @cuit", conexion);
            adaptador = new SqlDataAdapter();
            adaptador.DeleteCommand = eliminar;
            adaptador.DeleteCommand.Parameters.Add(new SqlParameter("@cuit", SqlDbType.VarChar));
        }

    }
}
