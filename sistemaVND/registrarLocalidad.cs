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
    public partial class registrarLocalidad : Form
    {
        
        public registrarLocalidad()
        {
            InitializeComponent();
        }


        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        private SqlDataAdapter adaptador, adaptadorB, adaptadorM, AdaptadorE;
        private DataSet datos;

        private void registrarLocalidad_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'sistemaVNDDataSetProvincias.provincia' Puede moverla o quitarla según sea necesario.
            this.provinciaTableAdapter.Fill(this.sistemaVNDDataSetProvincias.provincia);
            

            registrarLocali();
            buscarLocalidad();
            modificarLocalidad();
            eliminarLocalidad();
        }


        private void registrarLocali()
        {
            SqlCommand registrar = new SqlCommand("INSERT INTO localidad VALUES (@nombreLocalidad, @codigoPostal, @idProvincia)", conexion);
            adaptador = new SqlDataAdapter();
            adaptador.InsertCommand = registrar;
            adaptador.InsertCommand.Parameters.Add(new SqlParameter("@nombreLocalidad", SqlDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new SqlParameter("@codigoPostal", SqlDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new SqlParameter("@idProvincia", SqlDbType.Int));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Ingresar nombre de localidad", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if(string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Ingresar código postal", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if(comboBox1.SelectedItem == null)
                    {
                        MessageBox.Show("Seleccionar la provincia de la localidad", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        adaptador.InsertCommand.Parameters["@nombreLocalidad"].Value = textBox1.Text;
                        adaptador.InsertCommand.Parameters["@codigoPostal"].Value = textBox2.Text;
                        adaptador.InsertCommand.Parameters["@idProvincia"].Value = comboBox1.SelectedValue;
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
            }
        }


        private void buscarLocalidad()
        {
            SqlCommand buscar = new SqlCommand("SELECT * FROM localidad WHERE nombreLocalidad = @nombreLocalidad", conexion);
            adaptadorB = new SqlDataAdapter();
            adaptadorB.SelectCommand = buscar;
            adaptadorB.SelectCommand.Parameters.Add(new SqlParameter("@nombreLocalidad", SqlDbType.VarChar));
        }


        //BOTON BUSCAR 
        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Escribir el nombre de la localidad para buscar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    conexion.Open();
                    datos = new DataSet();
                    adaptadorB.SelectCommand.Parameters["@nombreLocalidad"].Value = textBox1.Text;
                    adaptadorB.Fill(datos, "localidades");
                    int registro = int.Parse(datos.Tables["localidades"].Rows.Count.ToString());
                    if(registro == 1)
                    {
                        foreach(DataRow fila in datos.Tables["localidades"].Rows)
                        {
                            textBox1.Text = fila["nombreLocalidad"].ToString();
                            textBox2.Text = fila["codigoPostal"].ToString();
                            comboBox1.SelectedItem = fila["idProvincia"].ToString();
                        }
                        textBox1.Enabled = false;
                        return;
                    }
                    else
                    {
                        MessageBox.Show("No existe una localidad con ese nombre", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
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


        private void modificarLocalidad()
        {
            SqlCommand modificar = new SqlCommand("UPDATE localidad SET codigoPostal = @codigoPostal, idProvincia = @idProvincia WHERE nombreLocalidad = @nombreLocalidad", conexion);
            adaptadorM = new SqlDataAdapter();
            adaptadorM.UpdateCommand = modificar;
            adaptadorM.UpdateCommand.Parameters.Add(new SqlParameter("@codigoPostal", SqlDbType.VarChar));
            adaptadorM.UpdateCommand.Parameters.Add(new SqlParameter("@idProvincia", SqlDbType.Int));
            adaptadorM.UpdateCommand.Parameters.Add(new SqlParameter("@nombreLocalidad", SqlDbType.VarChar));
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Ingresar código postal de la localidad", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                adaptadorM.UpdateCommand.Parameters["@codigoPostal"].Value = textBox2.Text;
                adaptadorM.UpdateCommand.Parameters["@idProvincia"].Value = comboBox1.SelectedValue;
                adaptadorM.UpdateCommand.Parameters["@nombreLocalidad"].Value = textBox1.Text;
                try
                {
                    conexion.Open();
                    adaptadorM.UpdateCommand.ExecuteNonQuery();
                    MessageBox.Show("Datos modificados", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
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






        //BOTON SALIR
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }











        private void eliminarLocalidad()
        {
            SqlCommand eliminar = new SqlCommand("DELETE FROM localidad WHERE nombreLocalidad = @nombreLocalidad", conexion);
            AdaptadorE = new SqlDataAdapter();
            AdaptadorE.DeleteCommand = eliminar;
            AdaptadorE.DeleteCommand.Parameters.Add(new SqlParameter("@nombreLocalidad", SqlDbType.VarChar));
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("No existe una localidad con ese nombre", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                AdaptadorE.DeleteCommand.Parameters["@nombreLocalidad"].Value = textBox1.Text;
                try
                {
                    conexion.Open();
                    AdaptadorE.DeleteCommand.ExecuteNonQuery();
                    MessageBox.Show("Localidad eliminada", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void limpiarForm()
        {
            textBox1.Text = "";
            textBox1.Enabled = true;
            textBox2.Text = "";
            comboBox1.SelectedItem = null;
        }
    }
}
