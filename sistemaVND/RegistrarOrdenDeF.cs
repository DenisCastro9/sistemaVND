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
    public partial class RegistrarOrdenDeF : Form
    {


        private SqlConnection conexion = new SqlConnection("data source=  DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        string articulo = "";
        int pedido = 0;
        int totalPares = 0;
        int nro = 0;
        string[] art;
        string numeroDePedido = "";
        DateTime fechaCreacion;
        string totalParesPedido = "";
        public RegistrarOrdenDeF(List<object[]> data, string articulo, int numeroPedido, int totalPares)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
            foreach (object[] rowData in data)
            {
                dataGridView1.Rows.Add(rowData);
            }
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.ClearSelection();

            this.articulo = articulo;
            label1.Text = this.articulo;
            this.pedido = numeroPedido;
            this.totalPares = totalPares;
            label16.Text = Convert.ToString(this.totalPares);            
            cargarNumeroOrdenF();           
           cargarCliente();            
           cargarMPxArt();        
            
           cagarCantidadMPxArtCortado();            
           cagarCantidadMPxArtOjalillado();
           cagarCantidadMPxArtEmpaque();


            DateTime fechaActual = DateTime.Now;
            DateTime fechaPorDefecto = fechaActual.AddDays(5);
            dateTimePicker1.Value = fechaActual;
            dateTimePicker2.Value = fechaPorDefecto;

            datosParaPDF();
        }

       
        private void cagarCantidadMPxArtEmpaque()
        {
            conexion.Open();
            string sql = "select cxp.idMateriaPrima as 'Codigo MP', mp.descripcion as 'Descripcion MP', cxp.cantidadXpares as 'A despachar' from cantidadMPxPedido as cxp join pedido as p on cxp.numeroPedido=p.numero join materiaPrima as mp on cxp.idMateriaPrima=mp.codigo where p.numero=@numeroPedido and cxp.area= @area";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@numeroPedido", pedido);
            comando.Parameters.AddWithValue("@area", "Empaque");
            SqlDataReader registro = comando.ExecuteReader();
            dataGridView2.Rows.Clear();
            while (registro.Read())
            {
                dataGridView2.Rows.Add(registro["Codigo MP"].ToString(),
                                       registro["Descripcion MP"].ToString(),
                                       registro["A despachar"].ToString());
            }
            conexion.Close();
        }

        private void cagarCantidadMPxArtOjalillado()
        {
            conexion.Open();
            string sql = "select cxp.idMateriaPrima as 'Codigo MP', mp.descripcion as 'Descripcion MP', cxp.cantidadXpares as 'A despachar' from cantidadMPxPedido as cxp join pedido as p on cxp.numeroPedido=p.numero join materiaPrima as mp on cxp.idMateriaPrima=mp.codigo where p.numero=@numeroPedido and cxp.area= @area";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@numeroPedido", pedido);
            comando.Parameters.AddWithValue("@area", "Ojalillado");
            SqlDataReader registro = comando.ExecuteReader();
            dataGridView4.Rows.Clear();
            while (registro.Read())
            {
                dataGridView4.Rows.Add(registro["Codigo MP"].ToString(),
                                       registro["Descripcion MP"].ToString(),
                                       registro["A despachar"].ToString());
            }
            conexion.Close();
        }

        private void cagarCantidadMPxArtCortado()
        {
            conexion.Open();
            string sql = "select cxp.idMateriaPrima as 'Codigo MP', mp.descripcion as 'Descripcion MP', cxp.cantidadXpares as 'A despachar' from cantidadMPxPedido as cxp join pedido as p on cxp.numeroPedido=p.numero join materiaPrima as mp on cxp.idMateriaPrima=mp.codigo where p.numero=@numeroPedido and cxp.area= @area";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@numeroPedido", pedido);
            comando.Parameters.AddWithValue("@area", "Cortado");
            SqlDataReader registro = comando.ExecuteReader();
            dataGridView3.Rows.Clear();
            while (registro.Read())
            {
                dataGridView3.Rows.Add(registro["Codigo MP"].ToString(),
                                       registro["Descripcion MP"].ToString(),
                                       registro["A despachar"].ToString());
            }
            conexion.Close();
        }


        public void cargarMPxArt()
        {
            conexion.Open();
            string sql = "select hilo1, costuron, apliqueI, apliqueII, apliqueIII, horma from materiaPrimaPorArticulo where codigoArt=@articulo ";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@articulo", label1.Text);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                label19.Text = registro["hilo1"].ToString();
                label20.Text = registro["costuron"].ToString();
                label21.Text = registro["apliqueI"].ToString();
                label22.Text = registro["apliqueII"].ToString();
                label23.Text = registro["apliqueIII"].ToString();
                label24.Text = registro["horma"].ToString(); //PROBAR

            }
            else
            {
                //MessageBox.Show("Este articulo no tiene material asignado");
            }
            conexion.Close();
        }

        private void cargarCliente()
        {
            conexion.Open();
            string sql = "select c.razonSocial from pedido as p join cliente as c on p.idCliente=c.id where p.numero=@numero ";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@numero", pedido);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                label3.Text = registro["razonSocial"].ToString();

            }
            else
            {
               // MessageBox.Show("Este pedido no tiene datos asignados");
            }
            conexion.Close();
        }

        private void cargarNumeroOrdenF()
        {
            conexion.Open();
            string query2 = " SELECT TOP 1 numero FROM ordenDeFabricacion ORDER BY numero DESC";
            SqlCommand command = new SqlCommand(query2, conexion);
            SqlDataReader registro = command.ExecuteReader();
            if (registro.Read())
            {
                nro = Convert.ToInt32(registro["numero"].ToString());
                nro = nro + 1;
            }
            label7.Text = Convert.ToString(nro);
            conexion.Close();
        }


        //BOTON VER MATERIAL EMPLEADO
        private void button3_Click(object sender, EventArgs e)
        {
            string nombre = label1.Text;
            MateriaPrimaPorArticulo f = new MateriaPrimaPorArticulo(nombre);
            f.FormClosed += f_FormClosed;
            f.ShowDialog();
        }


        private void f_FormClosed(object sender, FormClosedEventArgs e)
        {
            cargarMPxArt();
        }









        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }



        private void datosParaPDF()
        {
            //select p.numero, p.fechaDePedido, d.idArticulo as Codigo, a.talle, d.cantidad from pedido as p join cliente as c on p.idCliente=c.id 
           // join detalleDePedido as d on p.numero = d.numero join articulo as a on d.idArticulo = a.idarticulo
            
            conexion.Open();
            string c = "select p.numero, p.totalPares, p.fechaDePedido, d.idArticulo as Codigo, a.talle, d.cantidad from pedido as p join cliente as c on p.idCliente=c.id  join detalleDePedido as d on p.numero = d.numero join articulo as a on d.idArticulo = a.idarticulo where p.numero = @pedido";
            SqlCommand comando = new SqlCommand(c, conexion);
            comando.Parameters.AddWithValue("@pedido", pedido);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView6.Rows.Clear();
            while (registros.Read())
            {
                
                numeroDePedido = registros["numero"].ToString();
                totalParesPedido = registros["totalPares"].ToString();
                fechaCreacion = Convert.ToDateTime(registros["fechaDePedido"].ToString());
                label26.Text = fechaCreacion.ToShortDateString().ToString();
                dataGridView6.Rows.Add(registros["Codigo"].ToString(),
                                       registros["talle"].ToString(),
                                       registros["cantidad"].ToString());

            }
            conexion.Close();
        }



        //BOTON REGISTRAR
        private void button1_Click(object sender, EventArgs e)
        {
            if (!existeOrdenF())
            {
                MessageBoxButtons botones = MessageBoxButtons.YesNo;
                DialogResult dr = MessageBox.Show("¿Desea registrar la orden de fabricación?", "Registrar orden de fabricación",
                    botones, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    //ACA SE HACE EL REGISTRO
                    string sql = "insert into ordenDeFabricacion (numero, fechaEmision, fechaCreacion, fechaPrevistaFin, totalPares, idEstadoOrdenF, idPuntoDeControl, idPedido, cliente) values (@numero, @fechaEmision, @fechaCreacion, @fechaPrevistaFin, @totalPares, 1, 1, @idPedido, @cliente)";
                    SqlCommand command = new SqlCommand(sql, conexion);
                    command.Parameters.AddWithValue("@numero", Convert.ToInt32(label7.Text));
                    command.Parameters.AddWithValue("@fechaEmision", DateTime.Now.Date);//FECHA DE HOY
                    command.Parameters.AddWithValue("@fechaCreacion", dateTimePicker1.Value);//prevista inicio
                    command.Parameters.AddWithValue("@fechaPrevistaFin", dateTimePicker2.Value);//prevista fin
                    command.Parameters.AddWithValue("@totalPares", label16.Text);
                    command.Parameters.AddWithValue("@idPedido", pedido);
                    command.Parameters.AddWithValue("@cliente", label3.Text);
                    conexion.Open();

                    int result = command.ExecuteNonQuery();
                    if (result < 0)
                    { Console.WriteLine("No se pudo crear la orden de fabricacion"); }
                    else
                    {
                        art = new string[dataGridView1.Rows.Count];
                        string aux = "";
                        int y = 0;
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            aux = row.Cells[0].Value.ToString() + "-";  //codigo ART
                            aux += row.Cells[1].Value.ToString() + "-"; //descripcion ART
                            aux += row.Cells[2].Value.ToString() + "-";// talle
                            aux += row.Cells[3].Value.ToString() + "-";//cantidad
                            art[y] = aux;
                            string cadena = art[y];
                            string[] valores = cadena.Split('-');

                            sql = "insert into detalleOrdenF (numero, cantidad, idArticulo) values (@numeroParaDetalle, @cantidad, @idArticulo)";
                            command = new SqlCommand(sql, conexion);
                            command.Parameters.AddWithValue("@numeroParaDetalle", Convert.ToInt32(label7.Text));
                            command.Parameters.AddWithValue("@cantidad", Convert.ToInt32(valores[3]));
                            command.Parameters.AddWithValue("@idArticulo", Convert.ToDecimal(valores[0]));

                            int result2 = command.ExecuteNonQuery();
                            aux = "";
                        }
                        //update para idOrdenDeFabricacion del pedido
                        String query3 = "update pedido set idOrdenDeFabricacion = @idOrdenDeFabricacion, idEstadoPedido=2 where numero=@numeroPedido";
                        SqlCommand commandx = new SqlCommand(query3, conexion);
                        commandx.Parameters.AddWithValue("@idOrdenDeFabricacion", Convert.ToInt32(label7.Text));
                        commandx.Parameters.AddWithValue("@numeroPedido", pedido);
                        commandx.ExecuteNonQuery();






                        //ACA SE HACE LA PLANILLA PARA IMPRIMIR

                        SaveFileDialog guardar = new SaveFileDialog();
                        guardar.FileName = DateTime.Now.ToString("ddMMyyyy-OrdenDeFabricacion") + ".pdf";
                        string html_text = Properties.Resources.PlantillaOrdenDeFabricacionEmision.ToString();
                        html_text = html_text.Replace("@FECHA", DateTime.UtcNow.ToShortDateString());
                        html_text = html_text.Replace("@NUMERO", label7.Text);
                        html_text = html_text.Replace("@NOMBREARTICULO", label1.Text);
                        html_text = html_text.Replace("@HILO", label19.Text);
                        html_text = html_text.Replace("@COSTURON", label20.Text);
                        html_text = html_text.Replace("@APLIQUESnrouno", label21.Text);
                        html_text = html_text.Replace("@APLIQUESnrodos", label22.Text);
                        html_text = html_text.Replace("@APLIQUESnrotres", label23.Text);
                        html_text = html_text.Replace("@HORMA", label24.Text);                        
                        html_text = html_text.Replace("@TOTALPARES", label16.Text);
                        html_text = html_text.Replace("@CLIENTE", label3.Text);
                        html_text = html_text.Replace("@PEDIDO", numeroDePedido);//NECESITO EL NUMERO DE PEDIDO
                        html_text = html_text.Replace("@CREACION", label26.Text);//NECESITO LA FECHA DE CREACION DEL PEDIDO
                        html_text = html_text.Replace("@TTPARESPEDIDO", totalParesPedido);

                        string filas = string.Empty;
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            filas += "<tr>";
                            filas += "<td>" + row.Cells["talle"].Value.ToString() + "</td>";
                            filas += "<td>" + row.Cells["cantidad"].Value.ToString() + "</td>";
                            filas += "</tr>";
                        }
                        html_text = html_text.Replace("@GENERALFILAS", filas);

                        string filasCortado = string.Empty;
                        foreach (DataGridViewRow row in dataGridView3.Rows)
                        {
                            filasCortado += "<tr>";
                            filasCortado += "<td>" + row.Cells["codigoMP"].Value.ToString() + "</td>";
                            filasCortado += "<td>" + row.Cells["descripcionCortado"].Value.ToString() + "</td>";
                            filasCortado += "<td>" + row.Cells["aDespachar"].Value.ToString() + "</td>";
                            filasCortado += "<td> " + "-" + "</td>";
                            filasCortado += "<td> " + "-" + "</td>";
                            filasCortado += "</tr>";
                        }
                        html_text = html_text.Replace("@CORTADOFILAS", filasCortado);

                        string filasEmpaque = string.Empty;
                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {
                            filasEmpaque += "<tr>";
                            filasEmpaque += "<td>" + row.Cells["codigoEmpaque"].Value.ToString() + "</td>";
                            filasEmpaque += "<td>" + row.Cells["descripcionEmpaque"].Value.ToString() + "</td>";
                            filasEmpaque += "<td>" + row.Cells["aDespacharEmpaque"].Value.ToString() + "</td>";
                            filasEmpaque += "<td> " + "-" + "</td>";
                            filasEmpaque += "<td> " + "-" + "</td>";
                            filasEmpaque += "</tr>";
                        }
                        html_text = html_text.Replace("@EMPAQUEFILAS", filasEmpaque);

                        string filasAparado = string.Empty;
                        foreach (DataGridViewRow row in dataGridView4.Rows)
                        {
                            filasAparado += "<tr>";
                            filasAparado += "<td>" + row.Cells["codigoOjalillado"].Value.ToString() + "</td>";
                            filasAparado += "<td>" + row.Cells["descripcionOjalillado"].Value.ToString() + "</td>";
                            filasAparado += "<td>" + row.Cells["aDespacharOjalillado"].Value.ToString() + "</td>";
                            filasAparado += "<td> " +"-"+ "</td>";
                            filasAparado += "<td> " + "-" + "</td>";
                            filasAparado += "</tr>";
                        }
                        html_text = html_text.Replace("@APARADOFILAS", filasAparado);

                        string filasPedido = string.Empty;
                        foreach (DataGridViewRow row in dataGridView6.Rows)
                        {
                            filasPedido += "<tr>";
                            filasPedido += "<td>" + row.Cells["codigoP"].Value.ToString() + "</td>";
                            filasPedido += "<td>" + row.Cells["talleP"].Value.ToString() + "</td>";
                            filasPedido += "<td>" + row.Cells["cantidadP"].Value.ToString() + "</td>";                            
                            filasPedido += "</tr>";
                        }
                        html_text = html_text.Replace("@DATADELPEDIDO", filasPedido);




                        if (guardar.ShowDialog() == DialogResult.OK)
                        {
                            // Inicializar PdfSaveOptions 
                            var options = new PdfSaveOptions();
                            // Invoque el método ConvertHTML para convertir el código HTML a PDF
                            Converter.ConvertHTML(html_text, ".", options, guardar.FileName);
                            MessageBox.Show("PDF generado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        MessageBox.Show("Orden de fabricación registrada", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    conexion.Close();
                    this.Close();

                }
                else if (dr == DialogResult.No)
                {
                    return;
                }


            }
            else {
                MessageBox.Show("Ya existe una orden de fabricación con ese número asignado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool existeOrdenF()
        {
            bool existe = false;
                conexion.Open();
                string sql = "select * from ordenDeFabricacion where numero=@numero";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@numero", SqlDbType.Int).Value = label7.Text;
                SqlDataReader registro = comando.ExecuteReader();
                if (registro.Read())
                    existe = true;
                conexion.Close();            
            return existe;
        }

        private void RegistrarOrdenDeF_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
            dataGridView4.ClearSelection();
        }
    }
}
