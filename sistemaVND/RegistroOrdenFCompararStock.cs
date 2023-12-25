
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


//PARA HACXER EL PDF:
using Aspose.Html.Saving;
using Aspose.Html.Converters;


namespace sistemaVND
{
    public partial class RegistroOrdenFCompararStock : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        int numeroP = 0;
        int banderaStock = 0;
        int banderaStockArt = 0;
        int cantidadFilas = 0;
        string cliente = "";
        DateTime fechaCreacion;
        string totalPares = "";
        public RegistroOrdenFCompararStock(int numeroPedido)
        {
            InitializeComponent();
            this.numeroP = numeroPedido;
            label7.Text = Convert.ToString(numeroP);
            button4.Enabled = false;
            cargarDataPedido();
            cargarDataStock();
            cargarDataAfabricar();
            cantidadFilas = dataGridView2.RowCount;
            hayStock();


            datosParaElPDF();
        }

        private void datosParaElPDF()
        {
            conexion.Open();
            string c = "select p.fechaDePedido, p.totalPares, c.razonSocial from pedido as p join cliente as c on p.idCliente=c.id where p.numero=@pedido";
            SqlCommand comando = new SqlCommand(c, conexion);
            comando.Parameters.AddWithValue("@pedido", label7.Text);
            SqlDataReader registros = comando.ExecuteReader();
            if (registros.Read())
            {
                fechaCreacion = Convert.ToDateTime(registros["fechaDePedido"].ToString());
                label12.Text = fechaCreacion.ToShortDateString().ToString();
                totalPares= registros["totalPares"].ToString();
                cliente = registros["razonSocial"].ToString();
            }
            conexion.Close();
        }


        private void hayStock()
        {
            int hayStock = 0;
            int noHayStock = 0;
            // Verificar cada fila del DataGridView1 de pedidos
            foreach (DataGridViewRow filaPedido in dataGridView1.Rows)
            {
                string articulo = filaPedido.Cells["codigo"].Value.ToString();
                int cantidadFabricar = Convert.ToInt32(filaPedido.Cells["cantidad"].Value);
                // Buscar el artículo correspondiente en el DataGridView2 de stock
                DataGridViewRow filaStock = null;
                foreach (DataGridViewRow fila in dataGridView2.Rows)
                {
                    if (fila.Cells["Código"].Value.ToString() == articulo)
                    {
                        filaStock = fila;
                        break;
                    }
                }
                // Verificar la cantidad disponibles
                if (filaStock != null)
                {
                    int cantidadDisponible = Convert.ToInt32(filaStock.Cells["Cantidad en stock"].Value);
                    if (cantidadFabricar > cantidadDisponible)
                    {
                        // No hay suficiente cantidad disponible en stock
                        noHayStock++;
                    }
                    else {                        
                        hayStock++;
                    }                    
                }
                else
                {
                    label11.Text = "El artículo  no se encuentra en el stock";
                    label11.ForeColor = Color.Yellow;
                }
            }
            //Hacer las comparaciones de acuerdo a las filas recorridas
            //tomar la cantidad de filas que existen en el data,
            //si haystock es igual a la cantidad de filas entonces no hay que fabricar.
            //si haystock es menor a la cantidad de filas entonces hay que fabricar
            //si haystock es igual a cero entonces hay que fabricar
            int filas = dataGridView2.RowCount;
            if (hayStock < filas)
            {
                label11.Text = "Stock disponible para algunos articulos";
                label11.ForeColor = Color.DarkOrange;
                button4.Enabled = false;//generar PDF
                button1.Enabled = true;//continuar
                button3.Enabled = true;//consultarMP
                button5.Enabled = true;//reservar articulos
            }
            if (hayStock == filas)
            {
                label11.Text = "Hay stock disponible";
                label11.ForeColor = Color.DarkGreen;
                button4.Enabled = true;//generar PDF
                button1.Enabled = false;//continuar
                button3.Enabled = false;//consultarMP
                button5.Enabled = true;//reservar articulos
            }
            if (hayStock == 0)
            {
                label11.Text = "Sin suficiente stock";
                label11.ForeColor = Color.DarkRed;
                button4.Enabled = false;//generar PDF
                button1.Enabled = true;//continuar
                button3.Enabled = true;//consultarMP
                button5.Enabled = true;//reservar articulos
            }


        }

        private void cargarDataAfabricar()
        {
            conexion.Open();
            string sql = "select  a.codigoArticulo as Código, a.nombre, a.talle as Talle from detalleDePedido as p join articulo as a on p.idArticulo=a.idarticulo where p.numero=@numero";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@numero", numeroP);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            tabla.Columns.Add("Cantidad");
            dataGridView3.DataSource = tabla;
            dataGridView3.Columns["Código"].Visible = false;
            dataGridView3.Columns["nombre"].Visible = false;
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                label2.Text = registro["nombre"].ToString();

            }
            conexion.Close();
        }

        private void cargarDataStock()
        {
            conexion.Open();
            string sql = "select  a.nombre, a.codigoArticulo as Código, a.talle as Talle, a.cantidadEnStock as 'Cantidad en stock' from detalleDePedido as p join articulo as a on p.idArticulo=a.idarticulo where p.numero=@numero";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@numero", numeroP);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;

            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            tabla.Columns.Add("Reservar", typeof(string));
            foreach (DataRow row in tabla.Rows)
            {
                row["Reservar"] = "0";
            }
            dataGridView2.DataSource = tabla;

            dataGridView2.Columns["nombre"].Visible = false;

            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                label3.Text = registro["nombre"].ToString();

            }
            conexion.Close();
        }

        private void cargarDataPedido()
        {
            conexion.Open();
            string sql = "select a.codigoArticulo as Código, a.nombre, a.talle, p.cantidad from detalleDePedido as p join articulo as a on p.idArticulo=a.idarticulo where p.numero=@numero";                       
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@numero", numeroP);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {
                label1.Text = registros["nombre"].ToString();
                label2.Text=registros["nombre"].ToString();
                dataGridView1.Rows.Add(registros["Código"].ToString(),
                                       registros["talle"].ToString(),
                                       registros["cantidad"].ToString());
            }
            conexion.Close();
        }


        //BOTON RESERVAR ARTICULOS
        private void button5_Click(object sender, EventArgs e)
        {
            banderaStockArt = 1;//asegurarse que se modifico el stock de articulos Y SI NO NECESITE RESERVAR STOCK DE ARTICULOSS????????????????
            

            MessageBoxButtons botones = MessageBoxButtons.YesNo;
            DialogResult dr = MessageBox.Show("Desea reservar los artíulos indicados", "Reservar artículos",
                botones, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                
                //primero comparar que lo ingresado en Reservar sea menor al stock disponible
              for (int i = 0; i < cantidadFilas; i++)
              {
                DataTable dataTable = (DataTable)dataGridView2.DataSource;

                int aReservar = Convert.ToInt32(dataTable.Rows[i]["Reservar"].ToString());//saco del data lo que voy a reservar
                int stockDisponible = Convert.ToInt32(dataTable.Rows[i]["Cantidad en stock"].ToString());//saco del data lo que hay en stock

                if (stockDisponible < aReservar)//si el aReservar es mayor al stock disponible
                {
                    //si no alcanza el stock hay que avisar con un mensaje
                    MessageBox.Show("Los articulos a reservar no pueden ser menores a lo disponible en stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                        return;
                }
                else
                {
                    int cantidadReservadoBD = 0;
                    int cantidadStockBD = 0;
                 
                        //RESERVADO:
                        //TRAER EL VALOR DE RESERVADO EN LA BD del codigo de ese articulo
                        conexion.Open();
                        string sql = "select a.codigoArticulo as 'Código ART', a.reservado as 'RESERVADO' from articulo as a where a.codigoArticulo=@codigo";
                        SqlCommand comando = new SqlCommand(sql, conexion);
                        comando.Parameters.AddWithValue("@codigo", Convert.ToInt32(dataTable.Rows[i]["Código"].ToString()));//CONSEGUIR EL CODIGO DE LA MP QUE ESTE PASANDO AHORA POR EL FOR
                        SqlDataReader registro = comando.ExecuteReader();
                        if (registro.Read())
                        {
                            cantidadReservadoBD = Convert.ToInt32(registro["RESERVADO"].ToString());

                        }
                        registro.Close();
                        comando.ExecuteNonQuery();
                        conexion.Close();

                        //SUMARLE LO INGRESADO EN LA VARIABLE "reservar" a la columna "reservado" 
                        conexion.Open();
                        string sql1 = "UPDATE articulo set reservado= @reservar where codigoArticulo=@codigo";
                        SqlCommand comando1 = new SqlCommand(sql1, conexion);
                        comando1.Parameters.AddWithValue("@codigo", Convert.ToInt32(dataTable.Rows[i]["Código"].ToString()));//CONSEGUIR EL CODIGO DE LA MP QUE ESTE PASANDO AHORA POR EL FOR
                        comando1.Parameters.AddWithValue("@reservar", cantidadReservadoBD + aReservar);
                        comando1.ExecuteNonQuery();
                        conexion.Close();

                        //STOCK DISPONIBLE:
                        //TRAER EL VALOR DE STOCK DISPONIBLE DE LA BD del codigo de esa MP
                        conexion.Open();
                        string sql2 = "select a.codigoArticulo as 'Código ART', a.cantidadEnStock as 'En stock' from articulo as a where a.codigoArticulo =@codigo";
                        SqlCommand comando2 = new SqlCommand(sql2, conexion);
                        comando2.Parameters.AddWithValue("@codigo", Convert.ToInt32(dataTable.Rows[i]["Código"].ToString()));//CONSEGUIR EL CODIGO DE LA MP QUE ESTE PASANDO AHORA POR EL FOR
                        SqlDataReader registro2 = comando2.ExecuteReader();
                        if (registro2.Read())
                        {
                            cantidadStockBD = Convert.ToInt32(registro2["En stock"].ToString());

                        }
                        registro2.Close();
                        comando2.ExecuteNonQuery();
                        conexion.Close();

                        //RESTARLE LO INGRESADO EN LA VARIABLE "stockDisponible" a la columna "cantidad"
                        conexion.Open();
                        string sql3 = "UPDATE articulo set cantidadEnStock= @enStock where codigoArticulo=@codigo";
                        SqlCommand comando3 = new SqlCommand(sql3, conexion);
                        comando3.Parameters.AddWithValue("@codigo", Convert.ToInt32(dataTable.Rows[i]["Código"].ToString()));//CONSEGUIR EL CODIGO DE LA MP QUE ESTE PASANDO AHORA POR EL FOR
                        comando3.Parameters.AddWithValue("@enStock", cantidadStockBD - aReservar);
                        comando3.ExecuteNonQuery();
                        conexion.Close();

                        cantidadReservadoBD = 0;
                        cantidadStockBD = 0;
                        
                        button5.Enabled = false;                   
                }
              }
                MessageBox.Show("Artículos reservados", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dr == DialogResult.No)
            {
                return;
            }
            cargarDataStock();


        }


        //BOTON CONSULTAR STOCK DE MP
        private void button3_Click(object sender, EventArgs e)
        {
            string articulo=label2.Text;
            int cantidadPares = Convert.ToInt32(label10.Text);
            int numeroPedido = Convert.ToInt32(label7.Text);
            if (label10.Text == "0")
            {
                MessageBox.Show("Cargar cantidades a fabricar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else {
                ConsultarStockParaOrdenF cs = new ConsultarStockParaOrdenF(articulo, cantidadPares, numeroPedido);
                banderaStock = 1;//para verificar que al menos se entro a ver el stock
                cs.ShowDialog();
            }
            
        }





        //BOTON CONTINUAR
        private void button1_Click(object sender, EventArgs e)
        {
            if (label10.Text == "0" && banderaStock == 0 && banderaStockArt==0)
            {
                MessageBox.Show("Consultar el stock de material disponible y reservar artículos necesarios", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else {

                string articulo = label2.Text;
                int numeroPedido = Convert.ToInt32(label7.Text);
                int totalPares = Convert.ToInt32(label10.Text);

                // Obtener los datos de las filas del DataGridView
                List<object[]> data = new List<object[]>();
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    // Obtener los valores de las celdas de la fila
                    object[] rowData = new object[row.Cells.Count];
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        rowData[i] = row.Cells[i].Value;
                    }
                    data.Add(rowData);
                }
                // Abrir el FormB y pasar los datos
                RegistrarOrdenDeF formB = new RegistrarOrdenDeF(data, articulo, numeroPedido, totalPares);
                formB.ShowDialog();
                Dispose();


            }
            
        }




        //CADA VEZ QUE AGREGO CANTIDADES 
        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Calcula la suma de las cantidades en la columna "cantidad"
            int sumaCantidades = 0;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Cells["Cantidad"].Value != null)
                {
                    int cantidad = 0;
                    if (int.TryParse(row.Cells["Cantidad"].Value.ToString(), out cantidad))
                    {
                        sumaCantidades += cantidad;
                    }
                }
            }
            label10.Text = sumaCantidades.ToString();

        }



        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        //BOTON REMOVER ITEM
        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dataGridView3.SelectedRows[0];
                dataGridView3.Rows.Remove(filaSeleccionada);
            }
            else
            {
                MessageBox.Show("Seleccionar un Ítem", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }









        //BOTON GENERAR PEDIDO EN PDF
        private void button4_Click(object sender, EventArgs e)
        {
            //VALIDAR QUE SE HAYAN RESERVADO ARTICULOS
            if (banderaStockArt == 0)
            {
                MessageBox.Show("Reservar artículos necesarios", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else {


                MessageBoxButtons botones = MessageBoxButtons.YesNo;
                DialogResult dr = MessageBox.Show("¿Desea generar el archivo PDF?", "Pedido reservado del stock",
                    botones, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    SaveFileDialog guardar = new SaveFileDialog();
                    guardar.FileName = DateTime.Now.ToString("ddMMyyyy-pedidoOriginal") + (".pdf");

                    string html_text = Properties.Resources.PlantillaPedido.ToString();
                    html_text = html_text.Replace("@PEDIDO", label7.Text);
                    html_text = html_text.Replace("@FECHA", DateTime.UtcNow.ToShortDateString());
                    html_text = html_text.Replace("@CLIENTE", cliente);

                    html_text = html_text.Replace("@CREACION", label12.Text);
                    html_text = html_text.Replace("@TOTALPARES", totalPares);
                    html_text = html_text.Replace("@ARTICULONOMBRE", label1.Text);
                    string filas = string.Empty;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        filas += "<tr>";
                        filas += "<td>" + row.Cells["codigo"].Value.ToString() + "</td>";
                        filas += "<td>" + row.Cells["talle"].Value.ToString() + "</td>";
                        filas += "<td>" + row.Cells["cantidad"].Value.ToString() + "</td>";
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

                    //AHORA CAMBIA EL ESTADO DEL PEDIDO A FACTURAR
                    conexion.Open();
                    string query5 = "update pedido set idEstadoPedido=3 where numero=@numeroPedido";
                    SqlCommand commandy2 = new SqlCommand(query5, conexion);
                    commandy2.Parameters.AddWithValue("@numeroPedido", label7.Text);
                    commandy2.ExecuteNonQuery();
                    conexion.Close();
                    this.Close();


                }
                else if (dr == DialogResult.No)
                {
                    return;
                }
            }
        }



        private void RegistroOrdenFCompararStock_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
        }
    }
}
