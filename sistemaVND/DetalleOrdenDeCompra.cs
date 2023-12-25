using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.Mail;
using System.IO;

using System.Data.SqlClient;
using System.Web;
using sistemaVND.Properties;
//LAS DOS LIBRERIAS QUE SIGUEN GENERAN EL PDF
using Aspose.Html.Converters;
using Aspose.Html.Saving;






namespace sistemaVND
{
    public partial class DetalleOrdenDeCompra : Form
    {
        
        int numeroOrden;
        DateTime fecha;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public DetalleOrdenDeCompra(int numeroOrden, string estado)
        {
            InitializeComponent();
            dataGridView1.Columns.Add("codigoItem", "Código");
            dataGridView1.Columns.Add("descripcion", "Descripción");
            dataGridView1.Columns.Add("cantidad", "Cantidad");
            dataGridView1.Columns.Add("precioUnitario", "Precio Unitario");
            dataGridView1.Columns.Add("importeItem", "Total");
            this.numeroOrden = numeroOrden;
            label7.Text = Convert.ToString(numeroOrden);
            label20.Text = estado;
            cargarDatos();
            cargarData();


        }




        private void cargarData()
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
            string sql = "select cast(ord.fecha as DATE) as fecha, ord.enviarEmpresa, ord.enviarDomicilio, ord.enviarTelefono, ord.subtotal, ord.ivaPorcentaje, ord.importeIva, ord.importeDescuento, ord.importeEnvio, ord.importeTotal, p.nombre as proveedor, p.telefono, p.mail, d.calle, d.numero, l.nombreLocalidad as localidad, pro.nombreProvincia as provincia from ordenDeCompra as ord join proveedor as p on ord.idProveedor = p.id join domicilio as d on p.idDomicilio = d.numero join localidad as l on d.idLocalidad = l.idLocalidad join provincia as pro on l.idProvincia = pro.idprovincia where ord.numero = @numero";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.Add("@numero", SqlDbType.VarChar).Value = numeroOrden;
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                fecha = Convert.ToDateTime(registro["fecha"].ToString());
                label5.Text =fecha.ToShortDateString().ToString();
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




        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        //BOTON REGISTRAR FECHA DE INGRESO
        private void button4_Click(object sender, EventArgs e)
        {
            MessageBoxButtons botones = MessageBoxButtons.YesNo;
            DialogResult dr = MessageBox.Show("Desea registrar la fecha de ingreso", "Registrar orden de compra",
                botones, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                conexion.Open();
            string sql = "update ordenDeCompra set fechaDeIngreso=@fechaDeIngreso, idEstadoOrdenC=4 where numero=@numero";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@fechaDeIngreso", dateTimePicker1.Value);
            comando.Parameters.AddWithValue("@numero", numeroOrden);
            comando.ExecuteNonQuery();
            conexion.Close();
                MessageBox.Show("Fecha de ingreso registada", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else if (dr == DialogResult.No)
            {
                return;
            }
        }


        //BOTON ANULAR
        private void button3_Click(object sender, EventArgs e)
        {

            MessageBoxButtons botones = MessageBoxButtons.YesNo;
            DialogResult dr = MessageBox.Show("Desea anular la orden de compra", "Anular orden de compra",
                botones, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                conexion.Open();
                string sql = "update ordenDeCompra set idEstadoOrdenC=3 where numero=@numero";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@numero", numeroOrden);
                comando.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Orden de compra anulada", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            else if (dr == DialogResult.No)
            {
                return;
            }

        }



        //HABILITACION DE BOTONES DEPENDIENDO DEL ESTADO DE LA ORDEN
        private void DetalleOrdenDeCompra_Load(object sender, EventArgs e)
        {          
            if (label20.Text.Equals("Registrada"))
            {
                button4.Enabled = false;
            }
            else if (label20.Text.Equals("Anulada"))
            {
                button4.Enabled = false;
                button1.Enabled = false;
                button3.Enabled = false;
            }
            else if (label20.Text.Equals("Enviada"))
            {
                button1.Enabled = false;
                button3.Enabled = false;
            }
            dataGridView1.ClearSelection();
        }



        //BOTON ENVIAR 
        private void button1_Click(object sender, EventArgs e)
        {
            int numeroOrden = Convert.ToInt32(label7.Text);
            string mailProveedor = label37.Text;
            enviarMailOrdenDeCompra eoc = new enviarMailOrdenDeCompra(numeroOrden, mailProveedor);
            eoc.ShowDialog();
            this.Close();
        }






        //BOTON GENERAR PDF
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


            if (guardar.ShowDialog() == DialogResult.OK)
            {
                var options = new PdfSaveOptions();
                Converter.ConvertHTML(html_text, ".", options, guardar.FileName);
                MessageBox.Show("PDF generado");
            }

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
