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
    public partial class registrarProvincia : Form
    {
        public registrarProvincia()
        {
            InitializeComponent();
        }


        SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        SqlDataAdapter adaptador, adaptadorE;


        private void registrarProvincia_Load(object sender, EventArgs e)
        {
            registrarProv();
            eliminarProv();
        }


        private void registrarProv()
        {
            SqlCommand registrar = new SqlCommand("INSERT INTO provincia VALUES (@nombreProvincia)", conexion);
            adaptador = new SqlDataAdapter();
            adaptador.InsertCommand = registrar;
            adaptador.InsertCommand.Parameters.Add(new SqlParameter("@nombreProvincia", SqlDbType.VarChar));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Ingresar nombre de la provincia", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                adaptador.InsertCommand.Parameters["@nombreProvincia"].Value = textBox1.Text;
                try
                {
                    conexion.Open();
                    adaptador.InsertCommand.ExecuteNonQuery();
                    MessageBox.Show("Datos registrados", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiarForm();
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

        private void button4_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Ingresar nombre de la provincia", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (!existeProv())
                {
                    MessageBox.Show("No existe una provincia con ese nombre", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    adaptadorE.DeleteCommand.Parameters["@nombreProvincia"].Value = textBox1.Text;
                    try
                    {
                        conexion.Open();
                        adaptadorE.DeleteCommand.ExecuteNonQuery();
                        MessageBox.Show("Se eliminó correctamente", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiarForm();
                    }
                    catch (SqlException excepcion)
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

        private void eliminarProv()
        {
            SqlCommand eliminar = new SqlCommand("DELETE FROM provincia WHERE nombreProvincia = @nombreProvincia", conexion);
            adaptadorE = new SqlDataAdapter();
            adaptadorE.DeleteCommand = eliminar;
            adaptadorE.DeleteCommand.Parameters.Add(new SqlParameter("@nombreProvincia", SqlDbType.VarChar));
        }


        private bool existeProv()
        {
            bool existe = false;
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Ingresar nombre de la provincia", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                conexion.Open();
                SqlCommand probar = new SqlCommand("SELECT * FROM provincia WHERE nombreProvincia = @nombreProvincia", conexion);
                probar.Parameters.Add("@nombreProvincia", SqlDbType.VarChar).Value = textBox1.Text;
                SqlDataReader registro = probar.ExecuteReader();
                if(registro.Read())
                {
                    existe = true;
                }
                conexion.Close();
            }
            return existe;
        }









        //BOTON SALIR
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }















        private void limpiarForm()
        {
            textBox1.Text = "";
        }
    }
}
