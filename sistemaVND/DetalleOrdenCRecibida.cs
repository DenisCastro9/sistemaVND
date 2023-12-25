using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



using System.IO;
using System.Data.SqlClient;
using System.Web;
using sistemaVND.Properties;
using Aspose.Html.Saving;
using Aspose.Html.Converters;

namespace sistemaVND
{

    public partial class DetalleOrdenCRecibida : Form
    {
        int numeroOrden;
        DateTime fecha;
        DateTime fechaIngreso;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");

        public DetalleOrdenCRecibida(int numeroOrden)
        {
            InitializeComponent();
            dataGridView1.Columns.Add("codigoItem", "Código");
            dataGridView1.Columns.Add("descripcion", "Descripción");
            dataGridView1.Columns.Add("cantidad", "Cantidad");
            dataGridView1.Columns.Add("precioUnitario", "Precio Unitario");
            dataGridView1.Columns.Add("importeItem", "Total");
            this.numeroOrden = numeroOrden;
            label7.Text = Convert.ToString(numeroOrden);

            cargarDatos();
            cargarDataGrid();
        }

        private void cargarDataGrid()
        {
            conexion.Open();
            string c = "select dor.codigoItem, d.descripcion, dor.cantidad, dor.precioUnitario, dor.importeItem from detalleOrdenDeCompra as dor join materiaPrima as d on dor.idMateriaPrima = d.id where dor.numero = @numero";
            SqlCommand comando = new SqlCommand(c, conexion);
            comando.Parameters.AddWithValue("@numero", numeroOrden);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {
                dataGridView1.Rows.Add(
               registros["codigoItem"].ToString(),
               registros["descripcion"].ToString(),
               registros["cantidad"].ToString(),
               registros["precioUnitario"].ToString(),
              registros["importeItem"].ToString());

            }
            conexion.Close();
            dataGridView1.ClearSelection();
        }
        private void cargarDatos()
        {
            conexion.Open();
            string sql = "select cast(ord.fecha as DATE) as fecha, ord.fechaDeIngreso, ord.enviarEmpresa, ord.enviarDomicilio, ord.enviarTelefono, ord.subtotal, ord.ivaPorcentaje, ord.importeIva, ord.importeDescuento, ord.importeEnvio, ord.importeTotal, p.nombre as proveedor, p.telefono, p.mail, d.calle, d.numero, l.nombreLocalidad as localidad, pro.nombreProvincia as provincia from ordenDeCompra as ord join proveedor as p on ord.idProveedor = p.id join domicilio as d on p.idDomicilio = d.numero join localidad as l on d.idLocalidad = l.idLocalidad join provincia as pro on l.idProvincia = pro.idprovincia where ord.numero = @numero";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.Add("@numero", SqlDbType.VarChar).Value = numeroOrden;
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                fecha = Convert.ToDateTime(registro["fecha"].ToString());
                label5.Text = fecha.ToShortDateString().ToString();
                fechaIngreso = Convert.ToDateTime(registro["fechaDeIngreso"].ToString());
                label19.Text= fechaIngreso.ToShortDateString().ToString();
                label39.Text = registro["enviarEmpresa"].ToString();
                label38.Text = registro["enviarDomicilio"].ToString();
                label21.Text = registro["enviarTelefono"].ToString();
                label17.Text = registro["subtotal"].ToString();
                label11.Text = registro["ivaPorcentaje"].ToString();
                label27.Text = registro["importeIva"].ToString();
                label30.Text = registro["importeDescuento"].ToString();
                label32.Text = registro["importeEnvio"].ToString();
                label41.Text = registro["importeTotal"].ToString();
                label9.Text = registro["proveedor"].ToString();
                label36.Text = registro["telefono"].ToString();
                label37.Text = registro["mail"].ToString();
                label35.Text = registro["calle"].ToString() + " " + registro["numero"].ToString() + " " + registro["localidad"].ToString() + " " + registro["provincia"].ToString();
            }
            else
            {
                MessageBox.Show("No existe la orden", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            conexion.Close();

        }


        //BOTON GENERAR PDF (HUBO QUE DESCARGAR UN NUGET: Aspose.Html Y USAR LAS CLASES DE ARRIBA)
        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = DateTime.Now.ToString("ddMMyyyy-OrdenDeCompra") + ".pdf";

            
            string html_text = Properties.Resources.plantilla_html.ToString();
            html_text = html_text.Replace("@FECHA", label5.Text);
            html_text = html_text.Replace("@NRO", label7.Text);
            html_text = html_text.Replace("@PROVEEDOR", label9.Text);
            html_text = html_text.Replace("@DOMICILIOP", label35.Text);
            html_text = html_text.Replace("@TELEFONOP", label36.Text);
            html_text = html_text.Replace("@MAIL", label37.Text);
            html_text = html_text.Replace("@EMPRESA", label39.Text);
            html_text = html_text.Replace("@DOMICILIO", label38.Text);
            html_text = html_text.Replace("@TELEFONO", label21.Text);
            string filas = string.Empty;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["codigoItem"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["descripcion"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["precioUnitario"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["importeItem"].Value.ToString() + " </td>";
                filas += "</tr>";
            }
            html_text = html_text.Replace("@FILAS", filas);
            html_text = html_text.Replace("@SUBTOTAL", label17.Text);
            html_text = html_text.Replace("@IVA", label27.Text);
            html_text = html_text.Replace("@DESCUENTO", label30.Text);
            html_text = html_text.Replace("@ENVIO", label32.Text);
            html_text = html_text.Replace("@TOTAL", label41.Text);

           

            //si presiono aceptar en guardar
            if (guardar.ShowDialog() == DialogResult.OK)
            {
                // Inicializar PdfSaveOptions 
                var options = new PdfSaveOptions();
                // Invoque el método ConvertHTML para convertir el código HTML a PDF traigo el archivo html,
                // el option y donde se va a guardar junto con el nombre del archivo
                Converter.ConvertHTML(html_text, ".", options, guardar.FileName);
                MessageBox.Show("PDF generado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void DetalleOrdenCRecibida_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
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
            if (e.ColumnIndex == dataGridView1.Columns["importeItem"].Index && e.RowIndex >= 0)
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
