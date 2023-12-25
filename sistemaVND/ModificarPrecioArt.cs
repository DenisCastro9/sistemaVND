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
    public partial class ModificarPrecioArt : Form
    {
        string articulo = "";
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public ModificarPrecioArt(string nombre)
        {
            InitializeComponent();
            this.articulo = nombre;
            label2.Text = articulo;
            habilitacionDeCheckBox();
            
        }

        private void habilitacionDeCheckBox()
        {
            conexion.Open();
            string sql = "select nombre, talleNombre from  articulo where nombre=@nombre";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = articulo;
            SqlDataReader registro = comando.ExecuteReader();
            while (registro.Read())
            {
                if (registro["talleNombre"].ToString().Equals("nino"))
                {
                    checkBox1.Enabled = true;
                }
                if (registro["talleNombre"].ToString().Equals("Dama"))
                {
                    checkBox2.Enabled = true;
                }
                if (registro["talleNombre"].ToString().Equals("Hombre"))
                {
                    checkBox3.Enabled = true;
                }                
            }
            conexion.Close();
        }



        //BOTON SALIR
        private void button8_Click(object sender, EventArgs e)
        {
            Dispose();
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



        //BOTON GUARDAR
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "" || checkBox1.Checked==false && checkBox2.Checked==false && checkBox3.Checked==false)
            {
                MessageBox.Show("Ingresar precio unitario y seleccionar al menos un tipo de talle");
                return;
            }

            MessageBoxButtons botones = MessageBoxButtons.YesNo;
            DialogResult dr = MessageBox.Show("¿Desea modificar el precio de los talles seleccionados?", "Modificar precio",
                botones, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                if (checkBox1.Checked == true)//SI ESTA CHEKEADO EN NIÑO
                {
                    conexion.Open();
                    string sql = "update articulo set precioUnitario=@precioUnitario where talleNombre=@talle and nombre= @nombre";
                    SqlCommand comando = new SqlCommand(sql, conexion);
                    comando.Parameters.Add("@precioUnitario", SqlDbType.Decimal).Value = textBox6.Text;
                    comando.Parameters.Add("@talle", SqlDbType.VarChar).Value = "nino";
                    comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = articulo;
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
                if (checkBox2.Checked == true)//SI ESTA CHEKEADO EN DAMA
                {
                    conexion.Open();
                    string sql = "update articulo set precioUnitario=@precioUnitario where talleNombre=@talle and nombre= @nombre";
                    SqlCommand comando = new SqlCommand(sql, conexion);
                    comando.Parameters.Add("@precioUnitario", SqlDbType.Decimal).Value = textBox6.Text;
                    comando.Parameters.Add("@talle", SqlDbType.VarChar).Value = "Dama";
                    comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = articulo;
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
                if (checkBox3.Checked == true)//SI ESTA CHEKEADO EN HOMBRE
                {
                    conexion.Open();
                    string sql = "update articulo set precioUnitario=@precioUnitario where talleNombre=@talle and nombre= @nombre";
                    SqlCommand comando = new SqlCommand(sql, conexion);
                    comando.Parameters.Add("@precioUnitario", SqlDbType.Decimal).Value = textBox6.Text;
                    comando.Parameters.Add("@talle", SqlDbType.VarChar).Value = "Hombre";
                    comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = articulo;
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
                modificarPrecioArticulosGeneral();
                MessageBox.Show("Precio modificado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBox6.Text = "";
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked =false;
            }
            else if (dr == DialogResult.No)
            {
                return;
            }            
        }




        private void modificarPrecioArticulosGeneral()
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
                        command.Parameters.AddWithValue("@nombre", label2.Text);
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }  
            }
        }




        //BOTON VER LISTADO DE PRECIOS DE ARTICULOS
        private void button1_Click(object sender, EventArgs e)
        {
            //DEBERIA LLEVAR A UNA PANTALLA CON LISTADOS DIFERENCIADOS POR ARTICULO
            //QUE SE PUEDA IMPRIMIR
            PreciosAriculos p = new PreciosAriculos();
            p.ShowDialog(this);
        }
    }
}
