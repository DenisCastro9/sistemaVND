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
    public partial class eliminarUsuario : Form
    {
        public eliminarUsuario()
        {
            InitializeComponent();
        }


        private SqlConnection conexion = new SqlConnection("data source=  DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        private SqlDataAdapter adaptador, adaptadorE;
        private DataSet datos;


        private void eliminarUsuario_Load(object sender, EventArgs e)
        {
            recuperar();
            eliminar();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txbBuscar.Text))
            {
                MessageBox.Show("Ingresar DNI de un usuario existente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    conexion.Open();
                    datos = new DataSet();
                    adaptador.SelectCommand.Parameters["@dni"].Value = txbBuscar.Text;
                    adaptador.Fill(datos, "usuarioss");
                    int registro = int.Parse(datos.Tables["usuarioss"].Rows.Count.ToString());
                    if(registro == 1)
                    {
                        foreach(DataRow fila in datos.Tables["usuarioss"].Rows)
                        {
                            txbNombre.Text = fila["nombre"].ToString();
                            txbClave.Text = fila["clave"].ToString();
                            txbFechaIngre.Text = fila["fechaIngreso"].ToString();
                            txbFechaEgre.Text = fila["fechaEgreso"].ToString();
                            txbPreg1.Text = fila["preg1"].ToString();
                            txbPreg2.Text = fila["preg2"].ToString();
                            txbPreg3.Text = fila["preg3"].ToString();
                            txbNivel.Text = fila["nivel"].ToString();
                            txbArea.Text = fila["area"].ToString();
                            txbRegion.Text = fila["region"].ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No existe un usuario con el DNI ingresado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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


        private void recuperar()
        {
            SqlCommand recuperar = new SqlCommand("SELECT * FROM usuarios WHERE usuarios.dni = @dni", conexion);
            adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = recuperar;
            adaptador.SelectCommand.Parameters.Add(new SqlParameter("@dni", SqlDbType.BigInt));
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txbBuscar.Text))
            {
                MessageBox.Show("Buscar por DNI, un usuario existente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBoxButtons botones = MessageBoxButtons.YesNo;
                DialogResult dr = MessageBox.Show("¿Desea eliminar el usuario?", "Eliminar usuario",
                    botones, MessageBoxIcon.Question);
                if(dr == DialogResult.Yes)
                {
                    adaptadorE.DeleteCommand.Parameters["@dni2"].Value = txbBuscar.Text;
                    try
                    {
                        conexion.Open();
                        int cant = adaptadorE.DeleteCommand.ExecuteNonQuery();
                        if(cant == 0)
                        {
                            MessageBox.Show("Error de eliminación", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Usuario eliminado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiarForm();
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
                else
                {
                    if(dr == DialogResult.No)
                    {
                        return;
                    }
                }
            }
        }


        private void eliminar()
        {
            SqlCommand eliminar = new SqlCommand("DELETE FROM usuarios WHERE usuarios.dni = @dni2", conexion);
            adaptadorE = new SqlDataAdapter();
            adaptadorE.DeleteCommand = eliminar;
            adaptadorE.DeleteCommand.Parameters.Add(new SqlParameter("@dni2", SqlDbType.BigInt));
        }


        private void txbBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 32 && e.KeyChar <= 47) || (e.KeyChar > 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Ingresar solo números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }


      

        private void button2_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }

        private void limpiarForm()
        {
            txbNombre.Text = "";
            txbClave.Text = "";
            txbFechaIngre.Text = "";
            txbFechaEgre.Text = "";
            txbPreg1.Text = "";
            txbPreg2.Text = "";
            txbPreg3.Text = "";
            txbNivel.Text = "";
            txbArea.Text = "";
            txbRegion.Text = "";
            txbBuscar.Text = "";
        }
    }
}
