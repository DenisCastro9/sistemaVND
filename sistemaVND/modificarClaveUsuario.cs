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
    public partial class modificarClaveUsuario : Form
    {
        public modificarClaveUsuario()
        {
            InitializeComponent();
            textBox1.UseSystemPasswordChar = true;
            textBox2.UseSystemPasswordChar = true;
            textBox1.TextChanged += textBox1_TextChanged;

        }






    









        private string pregun1, pregun2, pregun3;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        private SqlDataAdapter adaptador;
        private DataSet datos;

        //Comparacion de respuestas
        private void button3_Click(object sender, EventArgs e)
        {
            if(pregun1 == pregunta1.Text)
            {
                if(pregun2 == pregunta2.Text)
                {
                    if(pregun3 == pregunta3.Text)
                    {
                        textBox1.Enabled = true;
                        textBox2.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Una de las 3 Respuestas no coinciden", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Una de las 3 Respuestas no coinciden", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Una de las 3 Respuestas no coinciden", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Metodo para modificar la contraseña
        private void modificarClave()
        {
            conexion.Open();
            string sql = "UPDATE usuarios SET clave = @clave where nombre=@nombre";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.Add("@clave", SqlDbType.VarChar).Value = textBox1.Text;
            comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox3.Text;
            comando.ExecuteNonQuery();
            conexion.Close();

        }





        private void button1_Click(object sender, EventArgs e)
        {
            if (label11.Text== "La contraseña debe tener al menos 7 caracteres.")
            {
                MessageBox.Show("No se puede modificar la contraseña", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                        
            if (textBox1.Text == textBox2.Text)
            {
                modificarClave();
                MessageBox.Show("Constraseña modificada", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dispose();
            }
            else {
                MessageBox.Show("Las contraseñas no coinciden", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
        }











        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (textBox1.Text.Length < 7)
            {
                label11.Text = "La contraseña debe tener al menos 7 caracteres.";
            }
            else
            {
                label11.Text = "";
            }
        }









        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //textBox2.UseSystemPasswordChar = true;
        }


















        private void button7_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        //Sentencia SQL para seleccionar las respuestas de cada usuario
        private void modificarClaveUsuario_Load(object sender, EventArgs e)
        {
            adaptador = new SqlDataAdapter();
            datos = new DataSet();
            SqlCommand comando = new SqlCommand("SELECT preg1, preg2, preg3 FROM usuarios WHERE nombre = @nombre", conexion);
            adaptador.SelectCommand = comando;
            adaptador.SelectCommand.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
        }

        //Boton para traer las respuestas de las preguntas
        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Ingresar nombre de usuario", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    conexion.Open();
                    adaptador.SelectCommand.Parameters["@nombre"].Value = textBox3.Text;
                    adaptador.Fill(datos, "usuarios");
                    int registro = int.Parse(datos.Tables["usuarios"].Rows.Count.ToString());
                    if(registro == 1)
                    {
                        foreach(DataRow fila in datos.Tables["usuarios"].Rows)
                        {
                            pregun1 = fila["preg1"].ToString();
                            pregun2 = fila["preg2"].ToString();
                            pregun3 = fila["preg3"].ToString();
                            respuesta.Text = "Responder las siguientes preguntas:";
                        }
                    }
                    else
                    {
                        MessageBox.Show("El nombre usuario ingresado no existe", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch(SqlException excepcion)
                {
                    MessageBox.Show(excepcion.ToString());
                }
                finally
                {
                    conexion.Close();
                }
            }
        }
    }
}
