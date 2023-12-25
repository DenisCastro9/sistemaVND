using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Threading;
using Dominio;



namespace sistemaVND
{
    public partial class login : Form
    {
        Thread hilo;

        public login()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void releaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);


        private void txBUsuario_Enter(object sender, EventArgs e)
        {
            if(txBUsuario.Text == "Nombre de Usuario")
            {
                txBUsuario.Text = "";
                txBUsuario.ForeColor = Color.LightGray;
            }
        }

        private void txBUsuario_Leave(object sender, EventArgs e)
        {
            if(txBUsuario.Text == "")
            {
                txBUsuario.Text = "Nombre de Usuario";
                txBUsuario.ForeColor = Color.DimGray;
            }
        }

        private void txBContr_Enter(object sender, EventArgs e)
        {
            if(txBContr.Text == "Contraseña")
            {
                txBContr.Text = "";
                txBContr.ForeColor = Color.LightGray;
                txBContr.UseSystemPasswordChar = true;
            }
        }

        private void txBContr_Leave(object sender, EventArgs e)
        {
            if(txBContr.Text == "")
            {
                txBContr.Text = "Contraseña";
                txBContr.ForeColor = Color.DimGray;
                txBContr.UseSystemPasswordChar = false;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            releaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            releaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //metodo login

        private void loginP()
        {
            if(txBUsuario.Text != "Nombre de Usuario")
            {
                if(txBContr.Text != "Contraseña")
                {
                    userModel user = new userModel();
                    var validLogin = user.LoginUser(txBUsuario.Text, txBContr.Text);
                    if(validLogin == true)
                    {
                        this.Close();
                        hilo = new Thread(AbrirForm);
                        hilo.SetApartmentState(ApartmentState.STA);
                        hilo.Start();
                    }
                    else
                    {
                        MessageBox.Show("Datos incorrectos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese nombre de usuario y contraseña");
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese nombre de usuario y contraseña");
            }
        }

        SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        SqlDataAdapter adaptador, adaptador2;
        DataSet area;
        DateTime ingreso = DateTime.Now;

        //Guardar fecha y hora de ingreso

        private void ingresoLogin()
        {
            adaptador2 = new SqlDataAdapter();

            SqlCommand fechaIngreso = new SqlCommand("UPDATE usuarios SET fechaIngreso = @fechaIngreso WHERE nombre = @nombre AND clave = @clave", conexion);
            fechaIngreso.Parameters.AddWithValue("@nombre", txBUsuario.Text);
            fechaIngreso.Parameters.AddWithValue("@clave", txBContr.Text);
            adaptador2.UpdateCommand = fechaIngreso;
            //adaptador2.UpdateCommand.Parameters.Add(new SqlParameter("@fechaIngreso", SqlDbType.DateTime));//  ------------------------------------------------------------
            adaptador2.UpdateCommand.Parameters.Add(new SqlParameter("@fechaIngreso", SqlDbType.DateTime));
            adaptador2.UpdateCommand.Parameters["@fechaIngreso"].Value = DateTime.Now;
        }

        private void AbrirForm(object obj)
        {
            Application.Run(new menuPrincipal(areaL.Text, txBUsuario.Text, txBContr.Text));
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            adaptador = new SqlDataAdapter();
            area = new DataSet();

            SqlCommand consulta = new SqlCommand("SELECT area FROM usuarios WHERE nombre = @nombre AND clave = @clave", conexion);
            consulta.Parameters.AddWithValue("@nombre", txBUsuario.Text);
            consulta.Parameters.AddWithValue("@clave", txBContr.Text);
            adaptador.SelectCommand = consulta;

            ingresoLogin();
            adaptador2.UpdateCommand.Parameters["@fechaIngreso"].Value = ingreso;

            try
            {
                conexion.Open();
                adaptador2.UpdateCommand.ExecuteNonQuery();
                //MessageBox.Show("se cargo la hora de ingreso", "sistema");
                adaptador.Fill(area, "usuarios");
                foreach (DataRow fila in area.Tables["usuarios"].Rows)
                {
                    areaL.Text = fila["area"].ToString();
                    areaL.Visible = false;
                }
                if (txBUsuario.Text != "Nombre de Usuario")
                {
                    if (txBContr.Text != "Contraseña")
                    {
                        userModel user = new userModel();
                        var validLogin = user.LoginUser(txBUsuario.Text, txBContr.Text);
                        if (validLogin == true)
                        {
                            using (menuPrincipal menu = new menuPrincipal(areaL.Text, txBUsuario.Text, txBContr.Text))
                            {
                                menu.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Datos incorrectos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese nombre de usuario y contraseña");
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese nombre de usuario y contraseña");
                }
            }
            catch (SqlException excepcion)
            {
                MessageBox.Show(excepcion.ToString());
            }
            finally
            {
                conexion.Close();
            }
            loginP();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            registrarUsuario ru = new registrarUsuario();
            ru.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            modificarClaveUsuario mcu = new modificarClaveUsuario();
            mcu.Show();
        }
    }
}
