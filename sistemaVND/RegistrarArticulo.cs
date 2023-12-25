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
    public partial class RegistrarArticulo : Form
    {
        int nro = 0;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        ArticulosRegistrados formPrincipal;
        public RegistrarArticulo(ArticulosRegistrados form)
        {
            InitializeComponent();
            formPrincipal = form;
            cargarNroArticulo();
            cargarComboTipo();
        }

        private void cargarComboTipo()
        {
            comboBox1.Items.Add("-");//0
            comboBox1.Items.Add("Zapato de vestir");//1
            comboBox1.Items.Add("Zapatilla");//2
            comboBox1.Items.Add("Mocasin");//3
            comboBox1.Items.Add("Mocasin colegial");//4
        }
        private void cargarNroArticulo()
        {
            conexion.Open();
            string query2 = " SELECT TOP 1 codigoArticulo FROM articulo ORDER BY codigoArticulo DESC";
            SqlCommand command = new SqlCommand(query2, conexion);
            SqlDataReader registro = command.ExecuteReader();
            if (registro.Read())
            {
                nro = Convert.ToInt32(registro["codigoArticulo"].ToString());
                nro = nro + 1;
            }
            textBox1.Text = Convert.ToString(nro);
            conexion.Close();
        }



        private void blanquearForm()
        {
            textBox1.Text = "";
            richTextBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedIndex = -1;
            textBox4.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            textBox6.Text = "";
        }



        private bool ExisteArticulo()
        {
            bool existe = false;
            if (textBox1.Text == "")
            {
                //MessageBox.Show("El codigo no puede estar vacio");
            }
            else
            {
                conexion.Open();
                
                string sql = "select * from articulo where codigoArticulo=@codigoArticulo";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@codigoArticulo", SqlDbType.Int).Value = textBox1.Text;
                SqlDataReader registro = comando.ExecuteReader();
                if (registro.Read())
                    existe = true;
                conexion.Close();
                
            }
            return existe;
        }
        private bool existeNombreArticulo()
        {
            bool existe = false;
            if (textBox2.Text == "")
            {
                //MessageBox.Show("El codigo no puede estar vacio");
            }
            else
            {
                conexion.Open();

                string sql = "select * from articulo where nombre=@codigoArticulo";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@codigoArticulo", SqlDbType.VarChar).Value = textBox2.Text;
                SqlDataReader registro = comando.ExecuteReader();
                if (registro.Read())
                    existe = true;
                conexion.Close();

            }
            return existe;
        }




        





        //BOTON REGISTRAR
        private void button5_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox6.Text == "" || checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false)
            {
                MessageBox.Show("Completar campos obligatorios *", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!ExisteArticulo())
            {
                //1: SI NO EXISTE EL ARTICULO CON EL CODIGO....


                if (!existeNombreArticulo())
                {
                    //2: SI NO EXISTE EL NOMBRE DEL ARTICULO: registra en articulosGeneral y articulo....
                   
                    //hacer el insert en la tabla articuloGENERAL primero
                    int c = int.Parse(textBox1.Text);                    
                    string sql = "insert into ArticulosGeneral (nombre) values (@nombre)";
                    SqlCommand comando = new SqlCommand(sql, conexion);
                    comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox2.Text;
                    conexion.Open();

                    int result = comando.ExecuteNonQuery();

                    if (result < 0)
                    {
                        Console.WriteLine("No se pudo registrar el articulo");
                    }
                    else
                    {
                        //YA REGISTRO EN ATICULOSGENERAL ENTONCES AGREGA A ARTICULO...
                        conexion.Close();
                        if (checkBox1.Checked) //CHECK NIÑO
                        {
                            for (int i = 27; i <= 33; i++)
                            {
                                conexion.Open();
                                string sql1 = "insert into articulo (codigoArticulo, nombre, tipoArticulo, descripcion, marca, fabricacion, precioUnitario, talle, talleNombre, cantidadEnStock, reservado) values(@codigoArticulo, @nombre, @tipoArticulo, @descripcion, @marca, @fabricacion, @precioUnitario, @talle, @talleNombre, @cantidadEnStock, @reservado)";
                                SqlCommand comando1 = new SqlCommand(sql1, conexion);
                                comando1.Parameters.Add("@codigoArticulo", SqlDbType.Int).Value = c;
                                comando1.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox2.Text;
                                comando1.Parameters.Add("@tipoArticulo", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
                                comando1.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = richTextBox1.Text;
                                comando1.Parameters.Add("@marca", SqlDbType.VarChar).Value = textBox4.Text;
                                if (radioButton1.Checked)
                                    comando1.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = "S";
                                if (radioButton2.Checked)
                                    comando1.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = "N";
                                if (radioButton1.Checked == false && radioButton2.Checked == false)
                                { comando1.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = ""; }
                                comando1.Parameters.Add("@precioUnitario", SqlDbType.Decimal).Value = textBox6.Text;
                                comando1.Parameters.Add("@talle", SqlDbType.VarChar).Value = i;
                                comando1.Parameters.Add("@talleNombre", SqlDbType.VarChar).Value = "nino";
                                comando1.Parameters.Add("@cantidadEnStock", SqlDbType.Int).Value = "0";
                                comando1.Parameters.Add("@reservado", SqlDbType.Int).Value = "0";
                                comando1.ExecuteNonQuery();
                                conexion.Close();
                                c++;
                            }
                        }

                        if (checkBox2.Checked)//CHECK DAMA
                        {
                            for (int i = 34; i <= 38; i++)
                            {
                                conexion.Open();
                                string sql2 = "insert into articulo (codigoArticulo, nombre, tipoArticulo, descripcion, marca, fabricacion, precioUnitario, talle, talleNombre, cantidadEnStock, reservado) values(@codigoArticulo, @nombre, @tipoArticulo, @descripcion, @marca, @fabricacion, @precioUnitario, @talle, @talleNombre , @cantidadEnStock, @reservado)";

                                SqlCommand comando2 = new SqlCommand(sql2, conexion);
                                comando2.Parameters.Add("@codigoArticulo", SqlDbType.Int).Value = c;
                                comando2.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox2.Text;
                                comando2.Parameters.Add("@tipoArticulo", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
                                comando2.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = richTextBox1.Text;
                                comando2.Parameters.Add("@marca", SqlDbType.VarChar).Value = textBox4.Text;
                                if (radioButton1.Checked)
                                    comando2.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = "S";
                                if (radioButton2.Checked)
                                    comando2.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = "N";
                                if (radioButton1.Checked == false && radioButton2.Checked == false)
                                { comando2.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = ""; }
                                comando2.Parameters.Add("@precioUnitario", SqlDbType.Decimal).Value = textBox6.Text;
                                comando2.Parameters.Add("@talle", SqlDbType.VarChar).Value = i;
                                comando2.Parameters.Add("@talleNombre", SqlDbType.VarChar).Value = "Dama";
                                comando2.Parameters.Add("@cantidadEnStock", SqlDbType.Int).Value = "0";
                                comando2.Parameters.Add("@reservado", SqlDbType.Int).Value = "0";
                                comando2.ExecuteNonQuery();
                                conexion.Close();
                                c++;
                            }

                        }

                        if (checkBox3.Checked)
                        {
                            for (int i = 39; i <= 46; i++)
                            {
                                conexion.Open();
                                string sql3 = "insert into articulo (codigoArticulo, nombre, tipoArticulo, descripcion, marca, fabricacion, precioUnitario, talle, talleNombre, cantidadEnStock, reservado) values(@codigoArticulo, @nombre, @tipoArticulo, @descripcion, @marca, @fabricacion, @precioUnitario, @talle, @talleNombre , @cantidadEnStock, @reservado)";
                                SqlCommand comando3 = new SqlCommand(sql3, conexion);
                                comando3.Parameters.Add("@codigoArticulo", SqlDbType.Int).Value = c;
                                comando3.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox2.Text;
                                comando3.Parameters.Add("@tipoArticulo", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
                                comando3.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = richTextBox1.Text;
                                comando3.Parameters.Add("@marca", SqlDbType.VarChar).Value = textBox4.Text;
                                if (radioButton1.Checked)
                                    comando3.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = "S";
                                if (radioButton2.Checked)
                                    comando3.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = "N";
                                if (radioButton1.Checked == false && radioButton2.Checked == false)
                                { comando3.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = ""; }
                                comando3.Parameters.Add("@precioUnitario", SqlDbType.Decimal).Value = textBox6.Text;
                                comando3.Parameters.Add("@talle", SqlDbType.VarChar).Value = i;
                                comando3.Parameters.Add("@talleNombre", SqlDbType.VarChar).Value = "Hombre";
                                comando3.Parameters.Add("@cantidadEnStock", SqlDbType.Int).Value = "0";
                                comando3.Parameters.Add("@reservado", SqlDbType.Int).Value = "0";
                                comando3.ExecuteNonQuery();
                                conexion.Close();
                                c++;
                            }

                        }

                        MessageBox.Show("Artículos registrados", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //ACA DEBERIA VENIR UN METODO PARA CARGAR EL PRECIO AL ARTICULOSGENERAL

                        AgregarPrecioArticulosGeneral();
                    }
                    blanquearForm();
                    
                }
                else {

                    //2: SI EXISTE EL NOMBRE DEL ARTICULO: PREGUNTA SI LO QUIERE REGISTRAR IGUAL POR MAS QUE YA EXISTA....
                    MessageBoxButtons botones = MessageBoxButtons.YesNo;
                    DialogResult dr = MessageBox.Show("Ya existe un artículo con ese nombre ¿Desea registrarlo igualmente?", "Registrar articulo",
                        botones, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        //3: SE REGISTRA IGUAL ENTONCES SOLO EN ARTICULO (porque puede tener cambios por eje que se registre un talle niño y solo estaban en dama)                                               
                        //QUE NO HAGA EL INSERT EN ARTICULOS GENERAL, SI YA EXISTE!
                        int c = int.Parse(textBox1.Text);
                            if (checkBox1.Checked) //CHECK NIÑO
                            {
                                for (int i = 27; i <= 33; i++)
                                {
                                    conexion.Open();
                                    string sql1 = "insert into articulo (codigoArticulo, nombre, tipoArticulo, descripcion, marca, fabricacion, precioUnitario, talle, talleNombre, cantidadEnStock, reservado) values(@codigoArticulo, @nombre, @tipoArticulo, @descripcion, @marca, @fabricacion, @precioUnitario, @talle, @talleNombre , @cantidadEnStock, @reservado)";
                                    SqlCommand comando1 = new SqlCommand(sql1, conexion);
                                    comando1.Parameters.Add("@codigoArticulo", SqlDbType.Int).Value = c;
                                    comando1.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox2.Text;
                                    comando1.Parameters.Add("@tipoArticulo", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
                                    comando1.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = richTextBox1.Text;
                                    comando1.Parameters.Add("@marca", SqlDbType.VarChar).Value = textBox4.Text;
                                    if (radioButton1.Checked)
                                        comando1.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = "S";
                                    if (radioButton2.Checked)
                                        comando1.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = "N";
                                    if (radioButton1.Checked == false && radioButton2.Checked == false)
                                    { comando1.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = ""; }
                                    comando1.Parameters.Add("@precioUnitario", SqlDbType.Decimal).Value = textBox6.Text;
                                    comando1.Parameters.Add("@talle", SqlDbType.VarChar).Value = i;
                                    comando1.Parameters.Add("@talleNombre", SqlDbType.VarChar).Value = "nino";
                                comando1.Parameters.Add("@cantidadEnStock", SqlDbType.Int).Value = "0";
                                comando1.Parameters.Add("@reservado", SqlDbType.Int).Value = "0";
                                comando1.ExecuteNonQuery();
                                    conexion.Close();
                                    c++;
                                }
                            }

                            if (checkBox2.Checked)//CHECK DAMA
                            {
                                for (int i = 34; i <= 38; i++)
                                {
                                    conexion.Open();
                                    string sql2 = "insert into articulo (codigoArticulo, nombre, tipoArticulo, descripcion, marca, fabricacion, precioUnitario, talle, talleNombre, cantidadEnStock, reservado) values(@codigoArticulo, @nombre, @tipoArticulo, @descripcion, @marca, @fabricacion, @precioUnitario, @talle, @talleNombre , @cantidadEnStock, @reservado)";

                                    SqlCommand comando2 = new SqlCommand(sql2, conexion);
                                    comando2.Parameters.Add("@codigoArticulo", SqlDbType.Int).Value = c;
                                    comando2.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox2.Text;
                                    comando2.Parameters.Add("@tipoArticulo", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
                                    comando2.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = richTextBox1.Text;
                                    comando2.Parameters.Add("@marca", SqlDbType.VarChar).Value = textBox4.Text;
                                    if (radioButton1.Checked)
                                        comando2.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = "S";
                                    if (radioButton2.Checked)
                                        comando2.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = "N";
                                    if (radioButton1.Checked == false && radioButton2.Checked == false)
                                    { comando2.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = ""; }
                                    comando2.Parameters.Add("@precioUnitario", SqlDbType.Decimal).Value = textBox6.Text;
                                    comando2.Parameters.Add("@talle", SqlDbType.VarChar).Value = i;
                                    comando2.Parameters.Add("@talleNombre", SqlDbType.VarChar).Value = "Dama";
                                comando2.Parameters.Add("@cantidadEnStock", SqlDbType.Int).Value = "0";
                                comando2.Parameters.Add("@reservado", SqlDbType.Int).Value = "0";
                                comando2.ExecuteNonQuery();
                                    conexion.Close();
                                    c++;
                                }

                            }

                            if (checkBox3.Checked)//CHECK HOMBRE
                            {
                                for (int i = 39; i <= 46; i++)
                                {
                                    conexion.Open();
                                    string sql3 = "insert into articulo (codigoArticulo, nombre, tipoArticulo, descripcion, marca, fabricacion, precioUnitario, talle, talleNombre, cantidadEnStock, reservado) values(@codigoArticulo, @nombre, @tipoArticulo, @descripcion, @marca, @fabricacion, @precioUnitario, @talle, @talleNombre , @cantidadEnStock, @reservado)";
                                    SqlCommand comando3 = new SqlCommand(sql3, conexion);
                                    comando3.Parameters.Add("@codigoArticulo", SqlDbType.Int).Value = c;
                                    comando3.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox2.Text;
                                    comando3.Parameters.Add("@tipoArticulo", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
                                    comando3.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = richTextBox1.Text;
                                    comando3.Parameters.Add("@marca", SqlDbType.VarChar).Value = textBox4.Text;
                                    if (radioButton1.Checked)
                                        comando3.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = "S";
                                    if (radioButton2.Checked)
                                        comando3.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = "N";
                                    if (radioButton1.Checked == false && radioButton2.Checked == false)
                                    { comando3.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = ""; }
                                    comando3.Parameters.Add("@precioUnitario", SqlDbType.Decimal).Value = textBox6.Text;
                                    comando3.Parameters.Add("@talle", SqlDbType.VarChar).Value = i;
                                    comando3.Parameters.Add("@talleNombre", SqlDbType.VarChar).Value = "Hombre";
                                comando3.Parameters.Add("@cantidadEnStock", SqlDbType.Int).Value = "0";
                                comando3.Parameters.Add("@reservado", SqlDbType.Int).Value = "0";
                                comando3.ExecuteNonQuery();
                                    conexion.Close();
                                    c++;
                                }

                            }

                        MessageBox.Show("Artículos registrados", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        blanquearForm();
                    }
                    else if (dr == DialogResult.No)
                    {
                        //3: SI DECIDE QUE NO SOLO SALE DEL IF
                        return;
                    }
                }
            }
            else
            {
                //1: SI EXISTE EL ARTICULO CON EL CODIGO
                MessageBox.Show("Ya existe un artículo con ese código", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cargarNroArticulo();
            }
        }



        private void AgregarPrecioArticulosGeneral()
        {
            decimal precio = decimal.Parse(textBox6.Text);
            bool seleccionadoNiño = checkBox1.Checked;
            bool seleccionadoDama = checkBox2.Checked;
            bool seleccionadoHombre = checkBox3.Checked;

            string query = "UPDATE ArticulosGeneral SET ";
            List<string> columnasActualizar = new List<string>();
            if (seleccionadoNiño)
                columnasActualizar.Add("precioNino = @precioNiño");
            if (seleccionadoDama)
                columnasActualizar.Add("precioDama = @precioDama");
            if (seleccionadoHombre)
                columnasActualizar.Add("precioHombre = @precioHombre");

            if (columnasActualizar.Count > 0)
            {
                query += string.Join(", ", columnasActualizar);
                query += " WHERE nombre = @nombre";
                using (SqlCommand command = new SqlCommand(query, conexion))
                {
                    conexion.Open();
                    if (seleccionadoNiño)
                        command.Parameters.AddWithValue("@precioNiño", precio);
                    if (seleccionadoDama)
                        command.Parameters.AddWithValue("@precioDama", precio);
                    if (seleccionadoHombre)
                        command.Parameters.AddWithValue("@precioHombre", precio);
                    command.Parameters.AddWithValue("@nombre", textBox2.Text);
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
            }
        }










        //boton salir
        private void button8_Click(object sender, EventArgs e)
        {
           
        }

 



        //FORMATO DECIMAL PARA EL PRECIO
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 32 && e.KeyChar <= 43) || (e.KeyChar > 45 && e.KeyChar <= 47) || (e.KeyChar > 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Ingresar formato de precio: 0,00", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            formPrincipal.cargarGrilla();
            Dispose();
        }
    }
}
