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
    public partial class FacturaVerDetalle : Form
    {
        int numeroFactura;
        int numeroPedido;
        int cantidad = 0;
        double netoGravado = 0;
        DateTime fecha;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public FacturaVerDetalle(int nroF, int nroP)
        {
            this.numeroFactura = 0;
            this.numeroPedido = 0;
            InitializeComponent();
            this.numeroFactura = nroF;
            this.numeroPedido = nroP;
            label7.Text = Convert.ToString(nroF);
            label49.Text = Convert.ToString(nroP);


            cargarDescripcionDelPedido();
            cargarDatos();
        }

        private void cargarDatos()
        {
            
            conexion.Open();
            string sql = "select f.fecha, f.montoTotal, f.tipoFactura, f.PorcentajeAlicuotaIva, f.importeIva, f.importeTributos, f.idRemito, c.razonSocial, c.calle, c.altura, c.barrio, l.nombreLocalidad, pr.nombreProvincia, c.condicionIva, c.ingresosBrutos, c.cuit, fp.descripcion, p.totalPares, p.importeTotal from factura as f join cliente as c on f.idCliente = c.id join localidad as l on c.idLocalidad = l.idLocalidad join provincia as pr on c.idProvincia = pr.idprovincia join formaDePago as fp on f.idFormaDePago = fp.id join detalleFactura as detf on f.idDetalleFactura = detf.numero join pedido as p on detf.idPedido = p.numero where f.numero = @nroFactura";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nroFactura", numeroFactura);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                
                fecha= Convert.ToDateTime(registro["fecha"].ToString());
                label11.Text = fecha.ToShortDateString().ToString();
                netoGravado = Convert.ToDouble(registro["montoTotal"].ToString());
                label56.Text = registro["tipoFactura"].ToString();
                label55.Text = registro["PorcentajeAlicuotaIva"].ToString();
                label34.Text = registro["importeIva"].ToString();
                label45.Text = registro["importeTributos"].ToString();
                label33.Text = registro["idRemito"].ToString();
                label25.Text = registro["razonSocial"].ToString();
                label26.Text = registro["calle"].ToString() + " " + registro["altura"].ToString() + " " + registro["barrio"].ToString() + " " + registro["nombreLocalidad"].ToString() + " " + registro["nombreProvincia"].ToString();
                label27.Text = registro["condicionIva"].ToString();
                label23.Text = registro["ingresosBrutos"].ToString();
                label24.Text = registro["cuit"].ToString();
                label30.Text = registro["descripcion"].ToString();
                label50.Text = registro["totalPares"].ToString();
                label35.Text = registro["importeTotal"].ToString();

            }
            else
            {
                MessageBox.Show("No existe la factura", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            label47.Text = Convert.ToString(netoGravado);
            conexion.Close();
        }


        private void cargarDescripcionDelPedido()
        {
            contarRegistros();
            conexion.Open();
            string sql = "   select a.nombre as articulo, a.talle, d.cantidad, a.precioUnitario from detalleDePedido as d join articulo as a on d.idArticulo = a.idarticulo join pedido as p on p.numero = d.numero where p.numero = @nroPedido";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nroPedido", numeroPedido);
            SqlDataReader registro = comando.ExecuteReader();
            for (int i = 0; i < cantidad; i++)
            {
                if (registro.Read())
                {
                    string nombreArticulo = registro["articulo"].ToString();
                    string talleArticulo = registro["talle"].ToString();
                    int cantidadArticulo = Convert.ToInt32(registro["cantidad"]);
                    decimal precioUnitario = Convert.ToDecimal(registro["precioUnitario"]);

                    decimal importe = cantidadArticulo * precioUnitario; // Calcula el importe para este artículo

                    label51.Text += $"{nombreArticulo} {talleArticulo} / {cantidadArticulo}\n";
                    label52.Text += $"$ {precioUnitario}\n";
                    label53.Text += $"$ {importe}\n";
                }

                else
                {
                    MessageBox.Show("Éste pedido no tiene datos asignados", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            conexion.Close();
        }


        private void contarRegistros()
        {
            conexion.Open();
            string sql = " select count(*) as cantidad from detalleDePedido join pedido on detalleDePedido.numero=pedido.numero where pedido.numero = @nroPed";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nroPed", numeroPedido);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                cantidad = Convert.ToInt32(registro["cantidad"].ToString());
            }
            conexion.Close();
        }


        //BOTON IMPRIMIR FACTURA
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = DateTime.Now.ToString("ddMMyyyy") + ".pdf";
            string html_text = Properties.Resources.PlantillaFactura.ToString();
            html_text = html_text.Replace("@LETRA", label56.Text);
            html_text = html_text.Replace("@NUMERO", label7.Text);
            html_text = html_text.Replace("@FECHA", label11.Text);

            html_text = html_text.Replace("@CLIENTE", label25.Text);
            html_text = html_text.Replace("@DOMCLI", label26.Text);
            html_text = html_text.Replace("@ICLIEN", label27.Text);
            html_text = html_text.Replace("@INBCLIEN", label23.Text);
            html_text = html_text.Replace("@CUIT", label24.Text);

            html_text = html_text.Replace("@CONDICIONES", label30.Text);


            html_text = html_text.Replace("@REMITO", label33.Text);
            html_text = html_text.Replace("@PORCIVA", label55.Text);


            //FILAS
            html_text = html_text.Replace("@PED", label49.Text);
            html_text = html_text.Replace("@CANT", label50.Text);
            html_text = html_text.Replace("@DESCR", label51.Text);
            html_text = html_text.Replace("@PRECIO", label52.Text);



            html_text = html_text.Replace("@NETO", label35.Text);
            html_text = html_text.Replace("@IVA", label34.Text);
            html_text = html_text.Replace("@EXENTO", label43.Text);
            html_text = html_text.Replace("@IBB", label45.Text);
            html_text = html_text.Replace("@TOTAL", label47.Text);

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                var options = new PdfSaveOptions();
                Converter.ConvertHTML(html_text, ".", options, guardar.FileName);
                MessageBox.Show("PDF generado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
