using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace sistemaVND
{
    public partial class registrarUsuario : Form
    {
        public registrarUsuario()
        {
            InitializeComponent();
            button2.Click += button2_Click;
        }

        private SqlConnection conexion;

        private void registrarUsuario_Load(object sender, EventArgs e)
        {
        }



              




        private bool ExisteUsuario()
        {
            bool existe = false;
            if (nomApe.Text == "")
            {
                MessageBox.Show("El nombre no puede estar vacío", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                conexion.Open();

                string sql = "select * from usuarios where nombre=@nombre";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nomApe.Text;
                SqlDataReader registro = comando.ExecuteReader();
                if (registro.Read())
                    existe = true;
                conexion.Close();

            }
            return existe;
        }

        public void limpiar()
        {

            nivel.Text = ("");
            area.Text = ("");
            dni.Text = ("");
            nomApe.Text = ("");
            clave.Text = ("");
            confClave.Text = ("");
            textBox1.Text = ("");
            pregunta1.Text = ("");
            pregunta2.Text = ("");
            pregunta3.Text = ("");
        }


        private void button1_Click(object sender, EventArgs e)
        {

            conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");

            

            if (!ExisteUsuario())
            {

                if (dni.Text == "")
                {
                    MessageBox.Show("Ingresar DNI", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (nomApe.Text == "")
                {
                    MessageBox.Show("Ingresar nombre de usuario", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (label17.Text == "La contraseña debe tener al menos 7 caracteres.")
                {
                    MessageBox.Show("La contraseña debe tener al menos 7 caracteres.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (nivel.SelectedItem == null)
                {
                    MessageBox.Show("Seleccionar un nivel", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (area.SelectedItem == null)
                {
                    MessageBox.Show("Seleccioanr un área", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (pregunta1.Text == "")
                {
                    MessageBox.Show("Completar campos obligatorios *", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (this.clave.Text.Equals(this.confClave.Text))
                {
                    conexion.Open();
                    string sql = "insert into usuarios (dni, nombre, clave," +
                        "preg1, preg2, preg3, nivel, area, region) values (@dni, @nombre, @clave," +
                        "@preg1, @preg2, @preg3, @nivel, @area, @region)";

                    SqlCommand comando = new SqlCommand(sql, conexion);

                    comando.Parameters.Add("@dni", SqlDbType.Int).Value = dni.Text;
                    comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nomApe.Text;
                    comando.Parameters.Add("@clave", SqlDbType.VarChar).Value = clave.Text;
                    comando.Parameters.Add("@preg1", SqlDbType.VarChar).Value = pregunta1.Text;
                    comando.Parameters.Add("@preg2", SqlDbType.VarChar).Value = pregunta2.Text;
                    comando.Parameters.Add("@preg3", SqlDbType.VarChar).Value = pregunta3.Text;
                    comando.Parameters.Add("@nivel", SqlDbType.VarChar).Value = nivel.SelectedItem;
                    comando.Parameters.Add("@area", SqlDbType.VarChar).Value = area.SelectedItem;
                    comando.Parameters.Add("@region", SqlDbType.VarChar).Value = textBox1.Text;

                    comando.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("Usuario registrado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                }
                else
                {
                    MessageBox.Show("Las claves no coinciden", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            else
            {
                MessageBox.Show("Ya existe un usuario con ese nombre", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }



        private void button7_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        private void dni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 32 && e.KeyChar <= 47) || (e.KeyChar > 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Ingresar numeros enteros", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            // Cambiar la propiedad UseSystemPasswordChar del TextBox a false para mostrar la contraseña
            clave.UseSystemPasswordChar = false;
            confClave.UseSystemPasswordChar = false;

            // Colocar el foco en el TextBox para que el usuario pueda ver la contraseña
            clave.Focus();
            confClave.Focus();
        }



        private void clave_TextChanged(object sender, EventArgs e)
        {
            if (clave.Text.Length < 7)
            {
                label17.Text = "La contraseña debe tener al menos 7 caracteres.";
            }
            else
            {
                label17.Text = "";
            }
        }
    }
}
