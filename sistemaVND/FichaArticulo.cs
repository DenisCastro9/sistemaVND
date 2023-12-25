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
    public partial class FichaArticulo : Form
    {
        int codigo;
        string nombre;
        private string nombreOriginal;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        ArticulosRegistrados formPrincipal;
        public FichaArticulo(ArticulosRegistrados form, int codigo, string nombre)
        {
            InitializeComponent();
            formPrincipal = form;
            nombreOriginal = textBox2.Text;
            this.codigo = codigo;
            textBox1.Text = Convert.ToString(codigo);
            this.nombre = nombre;
            textBox2.Text = nombre;
            textBox1.Enabled = false;
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            textBox6.Enabled = false;
            cargarComboTipo();

            cargarDatos();
            
        }



        private void cargarComboTipo()
        {
            comboBox1.Items.Add("-");//0
            comboBox1.Items.Add("Zapato de vestir");//1
            comboBox1.Items.Add("Zapatilla");//2
            comboBox1.Items.Add("Mocasin");//3
            comboBox1.Items.Add("Mocasin colegial");//4
        }

        private void cargarDatos()
        {
            conexion.Open();
            string sql = "select nombre, tipoArticulo, descripcion, marca, talle, talleNombre, fabricacion, precioUnitario from articulo where codigoArticulo = @codigoArticulo";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.Add("@codigoArticulo", SqlDbType.Int).Value = codigo;
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                nombre = registro["nombre"].ToString();
                textBox2.Text = nombre;
                richTextBox1.Text = registro["descripcion"].ToString();

                //comboBox1.Text = registro["tipoArticulo"].ToString();
                if (registro["tipoArticulo"].ToString().Equals("-"))
                {
                    comboBox1.SelectedIndex = 0;
                }
                if (registro["tipoArticulo"].ToString().Equals("Zapato de vestir"))
                {
                    comboBox1.SelectedIndex = 1;
                }
                if (registro["tipoArticulo"].ToString().Equals("Zapatilla"))
                {
                    comboBox1.SelectedIndex = 2;
                }
                if (registro["tipoArticulo"].ToString().Equals("Mocasin"))
                {
                    comboBox1.SelectedIndex = 3;
                }
                if (registro["tipoArticulo"].ToString().Equals("Mocasin colegial"))
                {
                    comboBox1.SelectedIndex = 4;
                }
                textBox4.Text = registro["marca"].ToString();
                if (registro["talleNombre"].ToString().Equals("nino"))
                {
                    checkBox1.Checked = true;
                }
                if (registro["talleNombre"].ToString().Equals("Dama"))
                {
                    checkBox2.Checked = true;
                }
                if (registro["talleNombre"].ToString().Equals("Hombre"))
                {
                    checkBox3.Checked = true;
                }
                textBox6.Text = registro["precioUnitario"].ToString();
                if (registro["fabricacion"].ToString() == "S")
                    radioButton1.Checked = true;
                if (registro["fabricacion"].ToString() == "N")
                    radioButton2.Checked = true;
            }
            else
            {
                MessageBox.Show("No existe el artículo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            conexion.Close();
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


        //BOTON GUARDAR CAMBIOS 
        private void button5_Click(object sender, EventArgs e)
        {
            //verificar que el textBox nombre de articulo no haya cambiado, si no cambio modifica el resto de datos normalmente,
            //si cambio fijarse que sea un nombre que no existe...

            if (textBox2.Text == "")
            {
                MessageBox.Show("Completar campos obligatorios *", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombreCambiado = textBox2.Text;

            if (nombreCambiado != nombre)
            {
                //SI CAMBIO EL CONTENIDO CONTROLAR QUE NO EXISTA....
                if (!existeNombreArticulo())
                {

                    //1: SI NO EXISTE EL NOMBRE DEL ARTICULO....


                    MessageBoxButtons botones = MessageBoxButtons.YesNo;
                    DialogResult dr = MessageBox.Show("Se modificarán los datos de todos los artículos con el mismo nombre ¿Desea continuar?", "Modificar artículos",
                        botones, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)//CAMBIA LOS DATOS DE TODOS LOS ARTICULOS CON EL MISMO NOMBRE
                    {
                        conexion.Open();
                        string sql = "update articulo set nombre=@nombre, descripcion=@descripcion, tipoArticulo=@tipoArticulo, marca=@marca, fabricacion=@fabricacion where nombre=@nombreTraido";
                        SqlCommand comando = new SqlCommand(sql, conexion);
                        comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox2.Text;
                        comando.Parameters.Add("@nombreTraido", SqlDbType.VarChar).Value = nombre;
                        comando.Parameters.Add("@tipoArticulo", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
                        comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = richTextBox1.Text;
                        comando.Parameters.Add("@marca", SqlDbType.VarChar).Value = textBox4.Text;
                        if (radioButton1.Checked)
                        { comando.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = "S"; }
                        if (radioButton2.Checked)
                        { comando.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = "N"; }                        
                        if (radioButton1.Checked == false && radioButton2.Checked == false)
                        { comando.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = ""; }
                        comando.ExecuteNonQuery();
                        conexion.Close();


                        //ACA DEBERIA HACER UPDATE EN LA TABLA ARTICULOSGENERAL PARA QUE QUEDEN CON EL MISMO DATO
                        conexion.Open();
                        string sql1 = "update ArticulosGeneral set nombre=@nombre where nombre=@nombreTraido";
                        SqlCommand com = new SqlCommand(sql1, conexion);
                        com.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox2.Text;
                        com.Parameters.Add("@nombreTraido", SqlDbType.VarChar).Value = nombre;
                        com.ExecuteNonQuery();
                        conexion.Close();

                        //DEBERIA MODIFICAR EL NOMBRE TAMBUEN PARA LA TABLA ARTICULOS GENERAL
                        conexion.Open();
                        string sql3 = "update materiaPrimaPorArticulo set codigoArt=@nombre where codigoArt=@nombreTraido";
                        SqlCommand com3 = new SqlCommand(sql3, conexion);
                        com3.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox2.Text;
                        com3.Parameters.Add("@nombreTraido", SqlDbType.VarChar).Value = nombre;
                        com3.ExecuteNonQuery();
                        conexion.Close();

                        //HACER UPDATE EN LA TABLA CANTIDADMPXARTICULO---------------------------------------------------
                        conexion.Open();
                          string sql4 = "update cantidadMPxArticulo set articulo=@nombre where articulo=@nombreTraido";
                          SqlCommand com4= new SqlCommand(sql4, conexion);
                          com4.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox2.Text;
                          com4.Parameters.Add("@nombreTraido", SqlDbType.VarChar).Value = nombre;
                          com4.ExecuteNonQuery();
                          conexion.Close();






                        MessageBox.Show("Se modificaron los datos de todos los artículos con el mismo nombre", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        formPrincipal.cargarGrilla();
                        Dispose();

                    }
                    else if (dr == DialogResult.No)//NO CAMBIA DATOS
                    {
                        return;
                    }


                }
                else
                {
                    //1: SI EXISTE MUESTRA EL MENSAJE
                    MessageBox.Show("Ese nombre ya esta asignado a un artículo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            else
            {
                //SI NO CAMBIO EL CONTENIDO modificaria los datos igualmente menos el nombre y no tocar la tabla ARTICULOSgENERAL
                //NO TE OLVIDES DE PREGUNTAR SI QUIERE CAMBIAR POR MAS QUE SE MODIFIQUEN TODOS LOS DATOS...!!


                MessageBoxButtons botones = MessageBoxButtons.YesNo;
                DialogResult dr = MessageBox.Show("Se modificarán los datos de todos los artículos con el mismo nombre ¿Desea continuar?", "Modificar artículos",
                    botones, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)//CAMBIA LOS DATOS DE TODOS LOS ARTICULOS CON EL MISMO NOMBRE
                {
                    conexion.Open();
                    string sql = "update articulo set descripcion=@descripcion, tipoArticulo=@tipoArticulo, marca=@marca, fabricacion=@fabricacion where nombre=@nombreTraido";
                    SqlCommand comando = new SqlCommand(sql, conexion);                
                    comando.Parameters.Add("@nombreTraido", SqlDbType.VarChar).Value = nombre;
                    comando.Parameters.Add("@tipoArticulo", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
                    comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = richTextBox1.Text;
                    comando.Parameters.Add("@marca", SqlDbType.VarChar).Value = textBox4.Text;
                    if (radioButton1.Checked)
                    { comando.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = "S"; }
                    if (radioButton2.Checked)
                    { comando.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = "N"; }
                    if (radioButton1.Checked == false && radioButton2.Checked == false)
                    { comando.Parameters.Add("@fabricacion", SqlDbType.VarChar).Value = ""; }
                    comando.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("Se modificaron los datos de todos los artículos con el mismo nombre", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formPrincipal.cargarGrilla();
                    Dispose();


                }
                else if (dr == DialogResult.No)//NO CAMBIA DATOS
                {
                    return;
                }




            }


        }



        

        private void button4_Click(object sender, EventArgs e)
        {
            formPrincipal.cargarGrilla();
            Dispose();
        }
    }
}
