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
    public partial class RegistrarPedido : Form
    {
        decimal  total= 0;
        decimal  montoTotal= 0;
        int totalPares = 0;
        int idArt = 0;
        string[] art;
        int ID = 0;

        decimal creditoCliente = 0;

        int cliente = 0;
        int sumaCantidadPares = 0;
        decimal sumaImporteTotal = 0;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public RegistrarPedido(string usuario)
        {
            InitializeComponent();
            dataGridView1.ClearSelection();
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            dateTimePicker1.Value = DateTime.Now;
            articulosDataGridView();
            cargarComboCliente();
            autoComplCliente();
            cargarComboFPago();
            label7.Text = usuario;
            SelectIDuser();

            cargarNroPedido();
        }

       

        private void cargarNroPedido()
        {
            int nro = 0;
            conexion.Open();
            string query2 = "SELECT TOP 1 numero FROM pedido ORDER BY numero DESC";
            SqlCommand command = new SqlCommand(query2, conexion);
            SqlDataReader registro = command.ExecuteReader();
            if (registro.Read())
            {
                nro = Convert.ToInt32(registro["numero"].ToString());
                nro = nro + 1;

            }
            label13.Text = Convert.ToString(nro);
            conexion.Close();
        }



        private void SelectIDuser()
        {
            conexion.Open();
            string sql = "select idUsuario from usuarios where nombre=@nombre";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = label7.Text;
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                ID= Convert.ToInt32(registro["idUsuario"].ToString());
            }
            else
            {
                MessageBox.Show("No existe el usuario", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            conexion.Close();


        }
        private void cargarComboFPago()
        {
            conexion.Open();
            string sql = "select id, descripcion from formaDePago";
            SqlCommand comando = new SqlCommand(sql, conexion);
            DataTable tabla1 = new DataTable();
            SqlDataAdapter adaptador1 = new SqlDataAdapter();
            adaptador1.SelectCommand = comando;
            adaptador1.Fill(tabla1);
            comboBox2.DataSource = tabla1;
            comboBox2.DisplayMember = "descripcion";
            comboBox2.ValueMember = "id";
            conexion.Close();
        }


        private void cargarComboCliente()
        {
            SqlCommand cm = new SqlCommand("select * from cliente", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr.GetString(1)); //ese numero representa la posicion del elemento
                //en la tabla de la BD
            }
            conexion.Close();
        }
        private void autoComplCliente()
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            SqlCommand cmd = new SqlCommand("select razonSocial from cliente", conexion);
            conexion.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                source.Add(reader["razonSocial"].ToString());
            }
            reader.Close();
            conexion.Close();
            comboBox1.AutoCompleteCustomSource = source;
        }










        //CARGAR DATA GRID DE ARTICULO funciona bien
        private void articulosDataGridView()
        {
            conexion.Open();
            string sql = "select idArticulo, codigoArticulo as Código , nombre as Nombre , talle as Talle, precioUnitario as Precio from articulo";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            tabla.Columns.Add("Cantidad Pares");
            dataGridView1.DataSource = tabla;
            dataGridView1.Columns["idArticulo"].Visible = false;
            dataGridView1.Columns["Precio"].Visible = false;
            conexion.Close();
            dataGridView1.ClearSelection();
        }
        private void buscador(string busqueda)
        {
            label3.Text = "0";
            label11.Text = "0,00";
            sumaImporteTotal = 0;
            sumaCantidadPares = 0;
            string consulta = "select idArticulo, codigoArticulo as Código , nombre as Nombre , talle as Talle, precioUnitario as Precio from articulo WHERE nombre LIKE @busqueda OR codigoArticulo LIKE @busqueda";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            tabla.Columns.Add("Cantidad Pares");
            dataGridView1.DataSource = tabla;
            dataGridView1.Columns["idArticulo"].Visible = false;
            dataGridView1.Columns["Precio"].Visible = false;
            dataGridView1.ClearSelection();
        }
        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string busqueda = txtBusqueda.Text.Trim();
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                articulosDataGridView();
            }
            else
            {
                label3.Text = "0";
                label11.Text = "0,00";
                buscador(busqueda);
            }
        }






        //BOTON REGISTRAR
        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex.Equals(-1) || dataGridView1.SelectedRows.Count < 0)
            {
                MessageBox.Show("Seleccionar artículos y elegir un cliente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBoxButtons botones = MessageBoxButtons.YesNo;
            DialogResult dr = MessageBox.Show("¿Desea registrar el pedido número "+label13.Text +"?", "Registrar pedido",
                botones, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {

                //validar que todas las filas con una cantidad mayor a cero estén seleccionadas
                int cantidadColumnIndex = 5; // Índice de la columna de cantidades
                bool todasLasFilasSeleccionadas = true;

                // Verificar si todas las filas con cantidad mayor a cero están seleccionadas
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    int cantidadEnFila;
                    if (int.TryParse(row.Cells[cantidadColumnIndex].Value.ToString(), out cantidadEnFila) && cantidadEnFila > 0)
                    {
                        if (!row.Selected)
                        {
                            // No todas las filas con cantidad mayor a cero están seleccionadas
                            todasLasFilasSeleccionadas = false;
                            break;
                        }
                    }
                }

                if (todasLasFilasSeleccionadas)
                {
                    // Realizar el registro
                    //antes validando que todas las filas seleccionadas tengan cantidades válidas osea MAYORES A CERO Y DISTINTOS DE NULL
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        int cantidad;
                        var cellValue = row.Cells[5].Value; // Índice de la columna "cantidad"

                        if (cellValue == null || !int.TryParse(cellValue.ToString(), out cantidad) || cantidad <= 0)
                        {
                            MessageBox.Show("La cantidad debe ser mayor a cero para todas las filas seleccionadas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }



                    //primero confirmar que el monto del pedido sea menor al credito asignado al cliente
                    conexion.Open();
                    string sqlComparar = "select credito.limite as Credito from cliente join credito on cliente.idCredito=credito.idcreditO where cliente.id=@id";
                    SqlCommand comed = new SqlCommand(sqlComparar, conexion);
                    comed.Parameters.AddWithValue("@id", cliente);
                    SqlDataReader registro = comed.ExecuteReader();
                    if (registro.Read())
                    { creditoCliente = Convert.ToDecimal(registro["Credito"].ToString()); }
                    else
                    {
                        MessageBox.Show("El cliente no tiene credito asignado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        conexion.Close();
                        return;
                    }
                    conexion.Close();

                    if (Convert.ToDecimal(label11.Text) < creditoCliente) //SI IMPORTE TOTAL ES MENOR AL CREDITO ASIGNADO
                    {
                        //ENTONCES SE PUEDE REGISTRAR EL PEDIDO
                        if (dataGridView1.SelectedRows.Count > 0)
                        {
                            String query = "INSERT INTO pedido (fechaDePedido, idCliente, idUsuario, idEstadoPedido, idFormaDePago) VALUES ( @fechaDePedido, @idcliente, @idUsuario, 1, @idFormaDePago)";
                            SqlCommand command = new SqlCommand(query, conexion);
                            command.Parameters.AddWithValue("@fechaDePedido", dateTimePicker1.Value); //lo saco del text time
                            command.Parameters.AddWithValue("@idcliente", cliente);//id del cliente
                            command.Parameters.AddWithValue("@idUsuario", ID);
                            command.Parameters.AddWithValue("@idFormaDePago", comboBox2.SelectedValue);
                            conexion.Open();
                            int result = command.ExecuteNonQuery();
                            if (result < 0)
                            { Console.WriteLine("No se pudo crear el pedido"); }
                            else
                            {
                                string query2 = "select @@Identity from pedido"; // @@Identity devuelve último valor: osea ID va a ser el ultimo valor que tenga el numero de pedido
                                command.CommandText = query2;
                                int id = Convert.ToInt32(command.ExecuteScalar()); //esto sirve para agregar el numero de detalle de pedido, el ID

                                art = new string[dataGridView1.SelectedRows.Count];
                                string aux = "";
                                int y = 0;
                                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                                {
                                    aux = row.Cells[0].Value.ToString() + "-";  //id art
                                    aux += row.Cells[1].Value.ToString() + "-"; //codigo art
                                    aux += row.Cells[2].Value.ToString() + "-";// nombre art
                                    aux += row.Cells[3].Value.ToString() + "-";//talle art
                                    aux += row.Cells[4].Value.ToString() + "-"; //precio art
                                    aux += row.Cells[5].Value.ToString() + "-"; //cantidad art
                                    art[y] = aux;
                                    string cadena = art[y];
                                    string[] valores = cadena.Split('-');


                                    total = (Convert.ToDecimal(valores[4]) * Convert.ToDecimal(valores[5]));
                                    montoTotal = montoTotal + total;
                                    totalPares = totalPares + Convert.ToInt32(valores[5]);
                                    idArt = Convert.ToInt32(valores[0]);
                                    query = "INSERT INTO detalleDePedido (numero, idArticulo, precio , cantidad) VALUES (@idDetallePedido,  @idArticulo ,@precio, @cantidad)";
                                    command = new SqlCommand(query, conexion);
                                    command.Parameters.AddWithValue("@idDetallePedido", id);
                                    command.Parameters.AddWithValue("@idArticulo", Convert.ToInt32(valores[0]));
                                    command.Parameters.AddWithValue("@precio", Convert.ToDecimal(valores[4]));
                                    command.Parameters.AddWithValue("@cantidad", Convert.ToInt32(valores[5]));
                                    int result2 = command.ExecuteNonQuery();
                                    if (result2 < 0)
                                    {
                                        Console.WriteLine("No se pudo crear el Detalle de Pedido");
                                    }
                                    aux = "";
                                }

                                String query3 = "update pedido set importeTotal = @importeTotal, totalPares=@totalPares, idArt=@idArt where numero = @id";
                                SqlCommand commandx = new SqlCommand(query3, conexion);
                                commandx.Parameters.Add("@id", SqlDbType.Char).Value = id;
                                commandx.Parameters.AddWithValue("@importeTotal", Convert.ToDecimal(montoTotal));
                                commandx.Parameters.AddWithValue("@totalPares", Convert.ToDecimal(totalPares));
                                commandx.Parameters.AddWithValue("@idArt", Convert.ToInt32(idArt));
                                commandx.ExecuteNonQuery();
                                MessageBox.Show("Pedido registrado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                label3.Text = "0";
                                label11.Text = "0,00";
                                sumaImporteTotal = 0;
                                sumaCantidadPares = 0;
                            }
                            conexion.Close();
                            total = 0;
                            montoTotal = 0;
                            totalPares = 0;
                        }
                        else
                        {
                            MessageBox.Show("Confirmar los artículos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        articulosDataGridView();//recarga el datagrid de articulos cuando se registra el pedido
                        cargarNroPedido();
                        //cargarComboCliente();

                    }
                    else
                    {
                        MessageBox.Show("El monto total supera el límite de crédito del cliente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                else
                {
                    // Mostrar un mensaje de error
                    MessageBox.Show("No todas las filas con cantidad mayor a cero están seleccionadas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }



            }
            else if (dr == DialogResult.No)
            {
                return;
            }  
            
        }








        //BOTON VER REGISTRADOS
        private void button2_Click(object sender, EventArgs e)
        {
            ConsultarPedido p = new ConsultarPedido();
            p.ShowDialog(this);
        }







        //AGREGO UN NUMERO A CANTIDAD DE PARES:
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dataGridView1.Columns["Cantidad Pares"].Index && e.RowIndex >= 0)
            {
                object cantidadParesValue = dataGridView1.Rows[e.RowIndex].Cells["Cantidad Pares"].Value;
                int cantidadPares;

                if (cantidadParesValue != DBNull.Value && int.TryParse(cantidadParesValue.ToString(), out cantidadPares))
                {
                    decimal precio = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["Precio"].Value);

                    if (dataGridView1.Rows[e.RowIndex].Cells["Cantidad Pares"].Tag != null)
                    {
                        int valorAnterior = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Cantidad Pares"].Tag);
                        sumaCantidadPares -= valorAnterior;
                    }

                    sumaCantidadPares += cantidadPares;
                    decimal importeTotal = cantidadPares * precio;
                    dataGridView1.Rows[e.RowIndex].Cells["Cantidad Pares"].Tag = cantidadPares;
                    label3.Text = sumaCantidadPares.ToString();

                    if (dataGridView1.Rows[e.RowIndex].Cells["Precio"].Tag != null) ///----------------------------------
                    {
                        decimal importeAnterior = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["Precio"].Tag);
                        sumaImporteTotal -= importeAnterior;
                    }

                    sumaImporteTotal += importeTotal;
                    dataGridView1.Rows[e.RowIndex].Cells["Precio"].Tag = importeTotal;
                    label11.Text = sumaImporteTotal.ToString();
                }
                else
                {
                    return;
                }
            }

        }




        //SACAR EL ID DEL CLIENTE
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand("select id from cliente where razonSocial= '" + comboBox1.Text + "'", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            if (dr.Read() == true)
            {
                cliente = Convert.ToInt32(dr["id"].ToString());//CON ESTO SACO EL ID DEL CLIENTE QUE ESTOY SELECCIONANDO
            }
            conexion.Close();
        }

        private void RegistrarPedido_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}

