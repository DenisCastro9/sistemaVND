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
    public partial class modificarUsuario : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public modificarUsuario(string usuario)
        {
            InitializeComponent();
            label7.Text = usuario;
        }


        //BOTON MODIFICAR
        private void button1_Click(object sender, EventArgs e)
        {
            if (pregunta1.Text == "" || pregunta2.Text == "")
            {
                MessageBox.Show("Completar campos obligatorios *", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            conexion.Open();
            string sql = "update usuarios set preg1 = @preg1, preg2 =@preg2, preg3=@preg3, dni=@dni where nombre = @nombre";
            SqlCommand comando = new SqlCommand(sql, conexion);

            comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = label7.Text;
            comando.Parameters.Add("@preg1", SqlDbType.VarChar).Value = pregunta1.Text;
            comando.Parameters.Add("@preg2", SqlDbType.VarChar).Value = pregunta2.Text;
            comando.Parameters.Add("@preg3", SqlDbType.VarChar).Value = pregunta3.Text;
            comando.Parameters.Add("@dni", SqlDbType.BigInt).Value = nomApe.Text;
            int cant = comando.ExecuteNonQuery();
            if (cant == 1)
            {
                MessageBox.Show("Datos modificados", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No existe ese usuario", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            conexion.Close();
        }

        //TRAE LOS DATOS CUANDO SE CARGA EL FORM
        private void modificarUsuario_Load(object sender, EventArgs e)
        {
            conexion.Open();
            string sql = "select preg1, preg2, preg3, dni from usuarios where nombre=@nombre";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = label7.Text;
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                pregunta1.Text = registro["preg1"].ToString();
                pregunta2.Text = registro["preg2"].ToString();
                pregunta3.Text = registro["preg3"].ToString();
                nomApe.Text = registro["dni"].ToString();
            }
            else
            {
                MessageBox.Show("No existe ese usuario", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            conexion.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
