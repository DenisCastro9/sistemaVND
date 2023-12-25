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
    public partial class RegistrarOrdenDeCompra : Form
    {
        string[] art;
        int idMP=0;
        int nro = 0;
        int idProveedor = 0;
        private SqlConnection conexion = new SqlConnection("data source=  DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public RegistrarOrdenDeCompra()
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now;
            cargarNumeroODC();
            cargarCOMBOMP();//
            cargarComboProveedor();
            textBox5.Enabled = false;
            cargarComboIva();

            autoCompletarProveedor();
            autoCompletarMP();//
        }

        private void autoCompletarMP()
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            SqlCommand cmd = new SqlCommand("SELECT descripcion FROM materiaPrima", conexion);
            conexion.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                source.Add(reader["descripcion"].ToString());
            }

            reader.Close();
            conexion.Close();

            comboBox1.AutoCompleteCustomSource = source;
        }





     



        //---------------------------------INICIO CARGA COMBOiVA--------------------------------------------
        private void cargarComboIva()
        {
            SqlCommand cm = new SqlCommand("select * from iva", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr.GetString(1)); //ese numero representa la posicion del elemento
                //en la tabla de la BD
            }
            conexion.Close();
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal alicuota = 0;
            decimal subtotal = Convert.ToDecimal(label17.Text);
            SqlCommand cm = new SqlCommand("select * from iva where descripcion= '" + comboBox3.Text + "'", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            if (dr.Read() == true)
            {
                alicuota = Convert.ToDecimal(dr["descripcion"].ToString());
            }
            decimal ImporteDeIva = 0;
            ImporteDeIva = subtotal * alicuota / 100;
            textBox4.Text = ImporteDeIva.ToString("N2"); //TENEMOS QUE PONER EL IVA QUE SE VA A SUMAR, NO EL SUMADO... 
            conexion.Close();
        }
//---------------------------------fin CARGA COMBOiVA--------------------------------------------






        //TRAIGO EL NUMERO DE ORDEN DE LA BD +1
        private void cargarNumeroODC()
        {
            
            conexion.Open();
            string query2 = " SELECT TOP 1 numero FROM ordenDeCompra ORDER BY numero DESC";
            SqlCommand command = new SqlCommand(query2, conexion);
            SqlDataReader registro = command.ExecuteReader();
            if (registro.Read())
            {
                nro = Convert.ToInt32(registro["numero"].ToString());
                nro = nro + 1;
            }
            textBox1.Text = Convert.ToString(nro);
            conexion.Close();
        }




//---------------------------combo proveedor inicio-------------------------------------
        private void cargarComboProveedor()
        {
            SqlCommand cm = new SqlCommand("select * from proveedor", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr.GetString(1)); //ese numero representa la posicion del elemento
                //en la tabla de la BD
            }
            conexion.Close();
        }

        //cuando presiono un item del combo proveedor (GUARDO EL ID DEL PROVEEDOR EN UNA VARIABLE)
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand("select p.id, p.telefono, p.mail, d.calle, d.numero, l.nombreLocalidad as localidad, pro.nombreProvincia as provincia from proveedor as p join domicilio as d on p.idDomicilio = d.numero join localidad as l on d.idLocalidad = l.idLocalidad join provincia as pro on l.idProvincia = pro.idprovincia where nombre= '" + comboBox2.Text + "'", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            if (dr.Read() == true)
            {
                textBox3.Text = dr["telefono"].ToString();
                textBox9.Text = dr["mail"].ToString();                
                textBox2.Text = dr["calle"].ToString() + " " + dr["numero"].ToString() + " " + dr["localidad"].ToString() + " " + dr["provincia"].ToString();
                idProveedor = Convert.ToInt32(dr["id"].ToString());//CON ESTO SACO EL ID DEL PROVEEDOR QUE ESTOY SELECCIONANDO
            }
            conexion.Close();
        }

        //---------------------------combo proveedor fin----------------------------------------

        private void autoCompletarProveedor()
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            SqlCommand cmd = new SqlCommand("SELECT nombre FROM proveedor", conexion);
            conexion.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                source.Add(reader["nombre"].ToString());
            }

            reader.Close();
            conexion.Close();

            comboBox2.AutoCompleteCustomSource = source;
        }










//------------------------- COMO CARGAR DATOS A TEXTBOX SELECCIONANDO UNO DE UN COMBObOX----------------inicio----------------
        //primero CARGA EL COMBOBOX MP asi:
        private void cargarCOMBOMP()
        {
            SqlCommand cm = new SqlCommand("select * from materiaPrima", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr.GetString(2)); //ese numero representa la posicion del elemento
                //en la tabla de la BD
            }
            conexion.Close();
        }

        //CUANDO PRESIONO UN ELEMENTO DEL COMBOBOX MATERIA PRIMA...
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand("select * from materiaPrima where descripcion= '" + comboBox1.Text + "'", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            if (dr.Read() == true)
            {
                textBox5.Text = dr["codigo"].ToString();
                //aca que me traiga el ultimo precio
                textBox7.Text= dr["ultimoPrecio"].ToString();                
            }
            conexion.Close();
        }
        //------------------------- COMO CARGAR DATOS A TEXTBOX SELECCIONANDO UNO DE UN COMBObOX----------------fin----------------









        //BOTON AGREGAR MP AL DATAGRID (PARA PONERLE EL SIGNO $ AL DATAGRID ENTRAR A "Editar columnas" y "DataGridViewCellStyle { Format=C2 }")
       private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex.Equals(-1)  || textBox6.Text == "" || textBox7.Text == "") 
            {
                MessageBox.Show("Ingresar datos para agregar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else 
            {
                //que pregunte si quiero agregar el precio ingresado de la MP
                MessageBoxButtons botones = MessageBoxButtons.YesNo;
                DialogResult dr = MessageBox.Show("El precio unitario será el ingresado ¿Continuar?", "Actualizar precio unitario",
                    botones, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    decimal num1 = Convert.ToDecimal(textBox6.Text);
                    decimal num2 = Convert.ToDecimal(textBox7.Text);
                    decimal sumaTotal = Convert.ToDecimal(num1) * Convert.ToDecimal(num2);
                    dataGridView1.Rows.Add(textBox5.Text, comboBox1.Text, textBox6.Text, textBox7.Text, sumaTotal);
                    decimal totalMonto = 0;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        totalMonto += Convert.ToDecimal(row.Cells["total"].Value);
                    }
                    label17.Text = Convert.ToString(totalMonto);
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox5.Text = "";
                    comboBox1.SelectedIndex = -1;
                }
                else if (dr == DialogResult.No)
                {
                    return;
                }  
            }
            dataGridView1.ClearSelection();
        }




        




        //BOTON REGISTRAR
        private void button1_Click(object sender, EventArgs e)
        {

                if (!existeOrdenC())
                {
                    if (comboBox2.SelectedIndex.Equals(-1) || textBox10.Text == "" || textBox11.Text == "" || textBox8.Text == "" || dataGridView1.RowCount <= 0 || label31.Text == "0,00")
                    {
                    MessageBox.Show("Completar campos obligatorios, agregar al menos un ítem y calcular importe total", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                    }
                    else
                    {
                        string sql = "INSERT INTO ordenDeCompra (numero, fecha, subtotal, importeDescuento, ivaPorcentaje, importeIva, importeEnvio, importeTotal, enviarEmpresa, enviarDomicilio, enviarTelefono, idProveedor, idEstadoOrdenC) VALUES(@numero, @fecha, @subtotal, @importeDescuento, @ivaPorcentaje, @importeIva, @importeEnvio, @importeTotal, @enviarEmpresa, @enviarDomicilio, @enviarTelefono, @idProveedor, 1)";
                        SqlCommand command = new SqlCommand(sql, conexion);
                        command.Parameters.AddWithValue("@numero", Convert.ToInt32(textBox1.Text));
                        command.Parameters.AddWithValue("@fecha", dateTimePicker1.Value); //NECESITO GUARDAR SOLO LA FECHA........BUSCAR.................................................................................................................................
                        command.Parameters.AddWithValue("@subtotal", Convert.ToDecimal(label17.Text));
                        command.Parameters.AddWithValue("@importeDescuento", Convert.ToDecimal(textBox12.Text));

                    if (comboBox3.SelectedItem != null)
                    {
                        command.Parameters.AddWithValue("@ivaPorcentaje", comboBox3.SelectedItem);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ivaPorcentaje", "0");
                    }


                        //command.Parameters.AddWithValue("@ivaPorcentaje", comboBox3.SelectedItem); //NO TE OLVIDES QUE HICISTE UNA TABLA IVA ACA Y TE VA A TOMAR EL VALOR QUE SE VE EN EL COMBO
                        command.Parameters.AddWithValue("@importeIva", Convert.ToDecimal(textBox4.Text));
                        command.Parameters.AddWithValue("@importeEnvio", Convert.ToDecimal(textBox13.Text));
                        command.Parameters.AddWithValue("@importeTotal", Convert.ToDecimal(label31.Text));
                        command.Parameters.AddWithValue("@enviarEmpresa", Convert.ToString(textBox10.Text));
                        command.Parameters.AddWithValue("@enviarDomicilio", Convert.ToString(textBox11.Text));
                        command.Parameters.AddWithValue("@enviarTelefono", textBox8.Text);
                        command.Parameters.AddWithValue("@idProveedor", idProveedor); //HICISTE ESTO PORQUE EL COMBO NO TE TRAE LOS ID, SOLO TRAJISTE EL  NOMBRE DEL PROVEEDOR
                        
                        conexion.Open();

                        int result = command.ExecuteNonQuery();

                        if (result < 0)
                        { Console.WriteLine("No se pudo crear la orden de compra");}
                        else
                        {

                            art = new string[dataGridView1.Rows.Count];
                            string aux = "";
                            int y = 0;
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                aux = row.Cells[0].Value.ToString() + "-";  //codigo
                                aux += row.Cells[1].Value.ToString() + "-"; //descripcion
                                aux += row.Cells[2].Value.ToString() + "-";// cantidad
                                aux += row.Cells[3].Value.ToString() + "-";//precioUnitario
                                aux += row.Cells[4].Value.ToString() + "-"; //total
                                art[y] = aux;
                                string cadena = art[y];
                                string[] valores = cadena.Split('-');


                                //INICIO SELECT PARA SABER EL ID DE LA MATERIA PRIMA
                                string sqlMP = "select id from materiaPrima where codigo=@codigo";
                                SqlCommand command2 = new SqlCommand(sqlMP, conexion);
                                command2.Parameters.AddWithValue("@codigo", Convert.ToInt32(valores[0]));
                                SqlDataReader registro = command2.ExecuteReader();
                                if (registro.Read())
                                {idMP = Convert.ToInt32(registro["id"].ToString());}
                                else
                                {
                                MessageBox.Show("No existe esa MP", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);           }
                                conexion.Close();
                                //FIN SELECT PARA SABER EL ID DE LA MATERIA PRIMA

                                sql = "INSERT INTO detalleOrdenDeCompra (numero, codigoItem, cantidad, precioUnitario, importeItem, idMateriaPrima) VALUES (@numero, @codigoItem, @cantidad, @precioUnitario, @importeItem, @idMateriaPrima)";
                                command = new SqlCommand(sql, conexion);
                                command.Parameters.AddWithValue("@numero", Convert.ToInt32(textBox1.Text));
                                command.Parameters.AddWithValue("@codigoItem", Convert.ToInt32(valores[0]));
                                command.Parameters.AddWithValue("@cantidad", Convert.ToInt32(valores[2]));
                                command.Parameters.AddWithValue("@precioUnitario", Convert.ToDecimal(valores[3]));
                                command.Parameters.AddWithValue("@importeItem", Convert.ToDecimal(valores[4]));
                                command.Parameters.AddWithValue("@idMateriaPrima", idMP);
                                conexion.Open();
                                int result2 = command.ExecuteNonQuery();
                                aux = "";
                            }                    
                        MessageBox.Show("Orden de compra registrada", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                            conexion.Close();
                            modificarPrecioUnitario();
                            blanquearForm();
                    }
                } 
                else
                { 
                MessageBox.Show("Ya existe una orden de compra con ese numero", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cargarNumeroODC();
                }
        }



        private void modificarPrecioUnitario()
        {
            //HAY QUE HACER UN FOREACH
            art = new string[dataGridView1.Rows.Count];
            string aux = "";
            int y = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                aux = row.Cells[0].Value.ToString() + "-";  //codigo
                aux += row.Cells[1].Value.ToString() + "-"; //descripcion
                aux += row.Cells[2].Value.ToString() + "-";// cantidad
                aux += row.Cells[3].Value.ToString() + "-";//precio
                aux += row.Cells[4].Value.ToString() + "-"; //total
                art[y] = aux;
                string cadena = art[y];
                string[] valores = cadena.Split('-');

                conexion.Open();
                string sql = "update materiaPrima set ultimoPrecio=@precio where codigo=@codigo";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@precio", Convert.ToDecimal(valores[3]));
                comando.Parameters.AddWithValue("@codigo", Convert.ToString(valores[0]));
                int result2 = comando.ExecuteNonQuery();
                conexion.Close();
                aux = "";
            }
        }




        private bool existeOrdenC()
        {
            bool existe = false;
            if (textBox1.Text == "")
            {
                MessageBox.Show("El número de Orden de compra no puede estar vacío", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
             
            }
            else
            {
                conexion.Open();

                string sql = "select * from ordenDeCompra where numero=@numero";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@numero", SqlDbType.Int).Value = textBox1.Text;
                SqlDataReader registro = comando.ExecuteReader();
                if (registro.Read())
                    existe = true;
                conexion.Close();

            }
            return existe;
        }



        private void blanquearForm() //ACA PUEDO VER COMO BORRAR TODAS LAS FILAS AGREGADAS EN UN DATAGRID
        {
            cargarNumeroODC();
            comboBox2.SelectedIndex = -1;
            textBox3.Text = "";
            textBox2.Text = "";
            textBox9.Text = "";
            comboBox1.SelectedIndex = -1;
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            label17.Text = "0,00";
            comboBox3.SelectedIndex = -1;
            textBox4.Text = "0,00";
            textBox12.Text = "0,00"; 
            textBox13.Text = "0,00";
            label31.Text = "0,00";
            dataGridView1.Rows.Clear(); //BORRA TODAS LAS FILAS AGREGADAS AL DATAGRID
            
           
        }






        //BOTON CALCULAR OTROS IMPORTES (EL ROWCOUNT CUENTA LOS ITEMS QUE EXISTEN EN EL DATAGRID )
        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex.Equals(-1) || textBox10.Text == "" || textBox11.Text == "" || textBox8.Text == "" || dataGridView1.RowCount <= 0)
            {
                MessageBox.Show("Completar campos obligatorios y agregar al menos un ítem", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                decimal ImporteFinal = Convert.ToDecimal(label17.Text) + Convert.ToDecimal(textBox4.Text) + Convert.ToDecimal(textBox13.Text) - Convert.ToDecimal(textBox12.Text);
                label31.Text = Convert.ToString(ImporteFinal);
            }
        }





        //VALIDAR SOLO NUMEROS ENTEROS (elcodigoascii.com.ar contiene los numeros para establecer los rangos)
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 32 && e.KeyChar <= 47) || (e.KeyChar > 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Ingresar numeros enteros", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        //VALIDAR DECIMALES: 
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 32 && e.KeyChar <= 43) || (e.KeyChar > 45 && e.KeyChar <= 47) || (e.KeyChar > 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Ingresar formato de precio: 0,00", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 32 && e.KeyChar <= 43) || (e.KeyChar > 45 && e.KeyChar <= 47) || (e.KeyChar > 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Ingresar formato de precio: 0,00", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 32 && e.KeyChar <= 43) || (e.KeyChar > 45 && e.KeyChar <= 47) || (e.KeyChar > 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Ingresar formato de precio: 0,00", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }











        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RegistrarOrdenDeCompra_Load(object sender, EventArgs e)
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
            if (e.ColumnIndex == dataGridView1.Columns["precioUnitario"].Index && e.RowIndex >= 0)
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

