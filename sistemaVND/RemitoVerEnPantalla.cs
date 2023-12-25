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
    public partial class RemitoVerEnPantalla : Form
    {
        int numeroRemito;
        int numeroPedido;
        int cantidad = 0;
        DateTime fecha;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public RemitoVerEnPantalla(int nroR, int nroP)
        {
            this.numeroRemito = 0;
            this.numeroPedido = 0;
            InitializeComponent();
            label8.Text = Convert.ToString(nroR);
            this.numeroRemito = nroR;
            this.numeroPedido = nroP;
            cargarDatosRemitoDelPedido();
            cargarDatorRemitoDelCliente();
            cargarDescripcionDelPedido();
        }

        private void cargarDescripcionDelPedido()
        {
            contarRegistros();
            conexion.Open();
            string sql = "  select a.nombre as articulo, a.talle, d.cantidad  from detalleDePedido as d join articulo as a on d.idArticulo = a.idarticulo join pedido as p on p.numero = d.numero where p.numero = @nroPedido";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nroPedido", numeroPedido);
            SqlDataReader registro = comando.ExecuteReader();
            for (int i = 0; i < cantidad; i++)
            {
                if (registro.Read())
                {
                    label36.Text += registro["articulo"].ToString() + " " + registro["talle"].ToString() + "\n";
                    label37.Text+= registro["cantidad"].ToString() + "\n"; 
                }

                else
                {
                    MessageBox.Show("Este pedido no tiene datos asignados", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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


        private void cargarDatorRemitoDelCliente() //CARGAR LOS DATOS DEL CLIENTE EN EL REMITO EN PANTALLA
        {
            conexion.Open();
            string sql = "  select c.razonSocial, c.cuit, c.calle, c.altura, l.nombreLocalidad , pr.nombreProvincia, c.ingresosBrutos, c.condicionIva, c.domicilioDeEntrega, t.nombre as transportista from cliente as c join localidad as l on c.idLocalidad = l.idLocalidad join transportista as t on c.idTransportista = t.id join pedido as p on p.idCliente = c.id join provincia as pr on c.idProvincia = pr.idprovincia join remito as r on r.idPedido = p.numero where r.numero = @nroR ";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nroR", numeroRemito); //ACA QUEDE
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                label25.Text = registro["razonSocial"].ToString();//nombre cliente
                label24.Text = registro["cuit"].ToString();

                label26.Text = registro["calle"].ToString() + " " + registro["altura"].ToString() + " " + registro["nombreLocalidad"].ToString() + " " + registro["nombreProvincia"].ToString();

                label23.Text = registro["ingresosBrutos"].ToString();
                label27.Text = registro["condicionIva"].ToString();
                label31.Text = registro["domicilioDeEntrega"].ToString();
                label30.Text = registro["transportista"].ToString();

            }
            else
            {
                MessageBox.Show("Este pedido no tiene datos asignados", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            conexion.Close();
        }

        private void cargarDatosRemitoDelPedido() //CARGAR LOS DATOS DEL PEDIDO EN EL REMITO EN PANTALLA
        {
            conexion.Open();
            string sql = "  select r.fecha, r.bultos , p.numero as codigoP, p.totalPares from pedido as p join remito as r on r.idPedido = p.numero join articulo as a on p.idArt = a.idarticulo  where r.numero=@nroR";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nroR", numeroRemito ); //ACA QUEDE
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                fecha = Convert.ToDateTime(registro["fecha"].ToString());
                label11.Text = fecha.ToShortDateString().ToString();
                label38.Text = registro["bultos"].ToString();
                label35.Text = registro["codigoP"].ToString();
                label44.Text = registro["totalPares"].ToString();

            }
            else
            {
                MessageBox.Show("Este pedido no tiene datos asignados", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            conexion.Close();
        }


        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }










        //BOTON GENERAR PDF
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = DateTime.Now.ToString("ddMMyyyy") + ".pdf";
            string html_text = Properties.Resources.PlantillaRemito.ToString();
            html_text = html_text.Replace("@NRO", label8.Text);
            html_text = html_text.Replace("@FECHA", label11.Text);

            html_text = html_text.Replace("@CLIENTE", label25.Text);
            html_text = html_text.Replace("@DOMCLI", label26.Text);
            html_text = html_text.Replace("@ICLIEN", label27.Text);
            html_text = html_text.Replace("@INBCLIEN", label23.Text);
            html_text = html_text.Replace("@CUIT", label24.Text);



            html_text = html_text.Replace("@TRANSPORTISTA", label30.Text);
            html_text = html_text.Replace("@ENTREGA", label31.Text);


            //FILAS
            html_text = html_text.Replace("@PED", label35.Text);

            string articuloWithLineBreaks = label36.Text.Replace("\n", "<br>");
            html_text = html_text.Replace("@DESCR", articuloWithLineBreaks);

            string articuloWithLineBreaks2 = label37.Text.Replace("\n", "<br>");
            html_text = html_text.Replace("@CANT", articuloWithLineBreaks2);



            html_text = html_text.Replace("@BULTOS", label38.Text);
            html_text = html_text.Replace("@TOTAL", label44.Text);

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                var options = new PdfSaveOptions();
                Converter.ConvertHTML(html_text, ".", options, guardar.FileName);
                MessageBox.Show("PDF generado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
