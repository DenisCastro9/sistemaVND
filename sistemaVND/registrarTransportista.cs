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
    public partial class registrarTransportista : Form
    {

        public registrarTransportista()
        {
            InitializeComponent();
        }

        //LOCALIDAD
        private void button6_Click(object sender, EventArgs e)
        {
            registrarLocalidad localidadForm = new registrarLocalidad();
            localidadForm.FormClosed += LocalidadForm_FormClosed; // Suscribirse al evento FormClosed
            localidadForm.ShowDialog();
        }
        private void LocalidadForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            cargarLocalidadT(); // Llamar al método cargarDatos() cuando se cierra registrarLocalidad
        }


        //PROVINCIA
        private void button7_Click(object sender, EventArgs e)
        {
            registrarProvincia provinciaForm = new registrarProvincia();
            provinciaForm.FormClosed += ProvinciaForm_FormClosed; // Suscribirse al evento FormClosed
            provinciaForm.ShowDialog();
        }

        private void ProvinciaForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            cargarProvinciaT();
        }
















        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        private SqlDataAdapter adaptador, adaptadorDomi, adaptadorM, adaptadorMD, adaptadorR, adaptadorE, adaptadorED;
        private DataSet datos;

        public void cargarLocalidadT() {
            this.localidadTableAdapter.Fill(this.sistemaVNDDataSetLocalidad.localidad);
        }
        public void cargarProvinciaT()
        {
            this.provinciaTableAdapter.Fill(this.sistemaVNDDataSetProvincias.provincia);
        }




        private void registrarTransportista_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'sistemaVNDDataSetProvincias.provincia' Puede moverla o quitarla según sea necesario.
            this.provinciaTableAdapter.Fill(this.sistemaVNDDataSetProvincias.provincia);
            // TODO: esta línea de código carga datos en la tabla 'sistemaVNDDataSetLocalidad.localidad' Puede moverla o quitarla según sea necesario.
            this.localidadTableAdapter.Fill(this.sistemaVNDDataSetLocalidad.localidad);
            //registrar
            registrarTra();
            registrardomicilio();

            //modificar
            recuperar();
            modificartra();
            modificarDomi();

            //eliminar
            eliminarTra();
            eliminarDomi();
        }


        //Registrar Transportista/Domicilio
        private void button1_Click(object sender, EventArgs e)
        {
            if(txbNombreTra.Text == "")
            {
                MessageBox.Show("Ingresar nombre", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if(maskedTextBox1.Text == "")
                {
                    MessageBox.Show("Ingresar cuit", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if(txbTelTra.Text == "")
                    {
                        MessageBox.Show("Ingresar teléfono", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if(txbMailTra.Text == "" && cmbExtension.SelectedItem == null)
                        {
                            MessageBox.Show("Ingresar mail", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if(txbCalle.Text == "")
                            {
                                MessageBox.Show("Ingresar calle", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                if(txbNumero.Text == "")
                                {
                                    MessageBox.Show("Ingresar número de altura", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    if(txbBarrio.Text == "")
                                    {
                                        MessageBox.Show("Ingresar barrio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    else
                                    {
                                        if(cmbLocalidad.SelectedItem == null)
                                        {
                                            MessageBox.Show("Seleccionar localidad", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                        else
                                        {
                                            if(cmbProvincia.SelectedItem == null)
                                            {
                                                MessageBox.Show("Seleccionar provincia", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            }
                                            else
                                            {
                                                adaptador.InsertCommand.Parameters["@cuit"].Value = maskedTextBox1.Text;
                                                adaptador.InsertCommand.Parameters["@nombre"].Value = txbNombreTra.Text;
                                                adaptador.InsertCommand.Parameters["@mail"].Value = txbMailTra.Text + cmbExtension.SelectedItem.ToString();
                                                adaptador.InsertCommand.Parameters["@telefono"].Value = txbTelTra.Text;
                                                adaptador.InsertCommand.Parameters["@idDomicilio"].Value = txbNumero.Text;
                                                adaptadorDomi.InsertCommand.Parameters["@numero"].Value = txbNumero.Text;
                                                adaptadorDomi.InsertCommand.Parameters["@calle"].Value = txbCalle.Text;
                                                adaptadorDomi.InsertCommand.Parameters["@nombreBarrio"].Value = txbBarrio.Text;
                                                adaptadorDomi.InsertCommand.Parameters["@idLocalidad"].Value = cmbLocalidad.SelectedValue;
                                                adaptadorDomi.InsertCommand.Parameters["@idProvincia"].Value = cmbProvincia.SelectedValue;
                                                try
                                                {
                                                    conexion.Open();
                                                    adaptador.InsertCommand.ExecuteNonQuery();
                                                    adaptadorDomi.InsertCommand.ExecuteNonQuery();
                                                    MessageBox.Show("Transportista registrado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            }
                        }
                    }
                }
            }
        }


        private void registrarTra()
        {
            SqlCommand registrar = new SqlCommand("INSERT INTO transportista VALUES (@cuit, @nombre, @mail, @telefono, @idDomicilio)", conexion);
            adaptador = new SqlDataAdapter();
            adaptador.InsertCommand = registrar;
            adaptador.InsertCommand.Parameters.Add(new SqlParameter("@cuit", SqlDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new SqlParameter("@mail", SqlDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new SqlParameter("@telefono", SqlDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new SqlParameter("@idDomicilio", SqlDbType.Int));
        }


        private void registrardomicilio()
        {
            SqlCommand registrarDomi = new SqlCommand("INSERT INTO domicilio VALUES (@numero, @calle, @nombreBarrio, @idLocalidad, @idProvincia)", conexion);
            adaptadorDomi = new SqlDataAdapter();
            adaptadorDomi.InsertCommand = registrarDomi;
            adaptadorDomi.InsertCommand.Parameters.Add(new SqlParameter("@numero", SqlDbType.Int));
            adaptadorDomi.InsertCommand.Parameters.Add(new SqlParameter("@calle", SqlDbType.VarChar));
            adaptadorDomi.InsertCommand.Parameters.Add(new SqlParameter("@nombreBarrio", SqlDbType.VarChar));
            adaptadorDomi.InsertCommand.Parameters.Add(new SqlParameter("@idLocalidad", SqlDbType.Int));
            adaptadorDomi.InsertCommand.Parameters.Add(new SqlParameter("@idProvincia", SqlDbType.Int));
        }


        //Modificar transportista/domicilio
        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbBuscar.Text))
            {
                MessageBox.Show("Ingresar nombre de un transportista registrado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    conexion.Open();
                    datos = new DataSet();
                    adaptadorR.SelectCommand.Parameters["@nombre"].Value = txbBuscar.Text;
                    adaptadorR.Fill(datos, "transportistas");
                    int registro = int.Parse(datos.Tables["transportistas"].Rows.Count.ToString());
                    if (registro == 1)
                    {
                        foreach (DataRow fila in datos.Tables["transportistas"].Rows)
                        {
                            txbNombreTra.Text = fila["nombre"].ToString();
                            maskedTextBox1.Text = fila["cuit"].ToString();
                            txbTelTra.Text = fila["telefono"].ToString();
                            txbMailTra.Text = fila["mail"].ToString();
                            txbNumero.Text = fila["numero"].ToString();
                            txbCalle.Text = fila["calle"].ToString();
                            txbBarrio.Text = fila["nombreBarrio"].ToString();
                            cmbLocalidad.Text = fila["nombreLocalidad"].ToString();
                            cmbProvincia.Text = fila["nombreProvincia"].ToString();

                            // Extraer el dominio del correo electrónico y mostrar solo lo que está antes del '@'
                            string correo = fila["mail"].ToString();
                            if (correo.Contains("@"))
                            {
                                string nombreAntesDeArroba = correo.Substring(0, correo.IndexOf("@"));
                                txbMailTra.Text = nombreAntesDeArroba;
                            }

                            // Buscar en el ComboBox el dominio coincidente y seleccionarlo
                            if (correo.Contains("@"))
                            {
                                string dominio = correo.Substring(correo.IndexOf("@") + 1);
                                foreach (var item in cmbExtension.Items)
                                {
                                    if (item.ToString().Contains(dominio))
                                    {
                                        cmbExtension.SelectedItem = item;
                                        break;
                                    }
                                }
                            }
                        }
                        txbNombreTra.Enabled = false;
                        txbNumero.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("No existe el transportista", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        limpiarForm();
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
            }
        }


        private void recuperar()
        {
            SqlCommand buscar = new SqlCommand("SELECT transportista.cuit, transportista.nombre, transportista.mail, transportista.telefono, domicilio.numero, domicilio.calle, domicilio.nombreBarrio, localidad.nombreLocalidad, provincia.nombreProvincia FROM transportista INNER JOIN domicilio ON transportista.idDomicilio = domicilio.numero" +
                "                                                                                                                                                                                                                                                                  INNER JOIN localidad ON domicilio.idLocalidad = localidad.idLocalidad" +
                "                                                                                                                                                                                                                                                                  INNER JOIN provincia ON domicilio.idProvincia = provincia.idprovincia WHERE transportista.nombre = @nombre", conexion);
            adaptadorR = new SqlDataAdapter();
            adaptadorR.SelectCommand = buscar;
            adaptadorR.SelectCommand.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (txbNombreTra.Text == "")
            {
                MessageBox.Show("Ingresar nombre", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (maskedTextBox1.Text == "")
                {
                    MessageBox.Show("Ingresar cuit", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (txbTelTra.Text == "")
                    {
                        MessageBox.Show("Ingresar teléfono", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (txbMailTra.Text == "" && cmbExtension.SelectedItem == null)
                        {
                            MessageBox.Show("Ingresar mail", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (txbCalle.Text == "")
                            {
                                MessageBox.Show("Ingresar calle", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                if (txbNumero.Text == "")
                                {
                                    MessageBox.Show("Ingresar número de altura", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    if (txbBarrio.Text == "")
                                    {
                                        MessageBox.Show("Ingresar barrio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    else
                                    {
                                        if (cmbLocalidad.SelectedItem == null)
                                        {
                                            MessageBox.Show("Seleccionar localidad", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                        else
                                        {
                                            if (cmbProvincia.SelectedItem == null)
                                            {
                                                MessageBox.Show("Seleccionar provincia", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            }
                                            else
                                            {
                                                adaptadorM.UpdateCommand.Parameters["@nombre"].Value = txbNombreTra.Text;
                                                adaptadorM.UpdateCommand.Parameters["@cuit"].Value = maskedTextBox1.Text;
                                                adaptadorM.UpdateCommand.Parameters["@telefono"].Value = txbTelTra.Text;
                                                adaptadorM.UpdateCommand.Parameters["@mail"].Value = txbMailTra.Text + cmbExtension.SelectedItem.ToString();
                                                adaptadorMD.UpdateCommand.Parameters["@nombre2"].Value = txbNombreTra.Text;
                                                adaptadorMD.UpdateCommand.Parameters["@calle"].Value = txbCalle.Text;
                                                adaptadorMD.UpdateCommand.Parameters["@nombreBarrio"].Value = txbBarrio.Text;
                                                adaptadorMD.UpdateCommand.Parameters["@idLocalidad"].Value = cmbLocalidad.SelectedValue;
                                                adaptadorMD.UpdateCommand.Parameters["@idProvincia"].Value = cmbProvincia.SelectedValue;
                                                try
                                                {
                                                    conexion.Open();
                                                    adaptadorM.UpdateCommand.ExecuteNonQuery();
                                                    adaptadorMD.UpdateCommand.ExecuteNonQuery();
                                                    MessageBox.Show("Datos modificados", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                }
                            }
                        }
                    }
                }
            }
        }


        private void modificartra()
        {
            SqlCommand modificar = new SqlCommand("UPDATE transportista SET transportista.cuit = @cuit, transportista.mail = @mail, transportista.telefono = @telefono FROM transportista, domicilio WHERE transportista.nombre = @nombre AND transportista.idDomicilio = domicilio.numero", conexion);
            adaptadorM = new SqlDataAdapter();
            adaptadorM.UpdateCommand = modificar;
            adaptadorM.UpdateCommand.Parameters.Add(new SqlParameter("@cuit", SqlDbType.VarChar));
            adaptadorM.UpdateCommand.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
            adaptadorM.UpdateCommand.Parameters.Add(new SqlParameter("@mail", SqlDbType.VarChar));
            adaptadorM.UpdateCommand.Parameters.Add(new SqlParameter("@telefono", SqlDbType.VarChar));
        }


        private void modificarDomi()
        {
            SqlCommand modificarDomi = new SqlCommand("UPDATE domicilio SET domicilio.calle = @calle, domicilio.nombreBarrio = @nombreBarrio, domicilio.idLocalidad = @idLocalidad, domicilio.idProvincia = @idProvincia FROM domicilio INNER JOIN transportista ON domicilio.numero = transportista.idDomicilio WHERE transportista.nombre = @nombre2", conexion);
            adaptadorMD = new SqlDataAdapter();
            adaptadorMD.UpdateCommand = modificarDomi;
            adaptadorMD.UpdateCommand.Parameters.Add(new SqlParameter("@calle", SqlDbType.VarChar));
            adaptadorMD.UpdateCommand.Parameters.Add(new SqlParameter("@nombreBarrio", SqlDbType.VarChar));
            adaptadorMD.UpdateCommand.Parameters.Add(new SqlParameter("@idLocalidad", SqlDbType.Int));
            adaptadorMD.UpdateCommand.Parameters.Add(new SqlParameter("@idProvincia", SqlDbType.Int));
            adaptadorMD.UpdateCommand.Parameters.Add(new SqlParameter("@nombre2", SqlDbType.VarChar));
        }


        //Eliminar Transportista/Domicilio
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbNombreTra.Text))
            {
                MessageBox.Show("Buscar transportista primero", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBoxButtons botones = MessageBoxButtons.YesNo;
                DialogResult dr = MessageBox.Show("¿Desea eliminar el transportista'", "Eliminar transportista",
                    botones, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    adaptadorED.DeleteCommand.Parameters["@nombre2"].Value = txbNombreTra.Text;
                    try
                    {
                        conexion.Open();
                        adaptadorED.DeleteCommand.ExecuteNonQuery();
                        int proc = Convert.ToInt32(adaptadorED.DeleteCommand.ExecuteNonQuery());
                        if(proc < 1)
                        {
                            adaptadorE.DeleteCommand.Parameters["@nombre"].Value = txbNombreTra.Text;
                            adaptadorE.DeleteCommand.ExecuteNonQuery();
                            MessageBox.Show("Transportista eliminado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiarForm();
                        }
                        else
                        {
                            MessageBox.Show("Error de eliminación", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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


        private void eliminarTra()
        {
            SqlCommand eliminar = new SqlCommand("DELETE FROM transportista WHERE transportista.nombre = @nombre", conexion);
            adaptadorE = new SqlDataAdapter();
            adaptadorE.DeleteCommand = eliminar;
            adaptadorE.DeleteCommand.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
        }


        private void eliminarDomi()
        {
            SqlCommand eliminarDomi = new SqlCommand("DELETE FROM domicilio FROM domicilio INNER JOIN transportista ON domicilio.numero = transportista.idDomicilio AND transportista.nombre = @nombre2", conexion);
            adaptadorED = new SqlDataAdapter();
            adaptadorED.DeleteCommand = eliminarDomi;
            adaptadorED.DeleteCommand.Parameters.Add(new SqlParameter("@nombre2", SqlDbType.VarChar));
        }

        private void maskedTextBox1_Click(object sender, EventArgs e)
        {
            maskedTextBox1.SelectionStart = 0;
        }

        private void cmbLocalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbLocalidad.SelectedItem != null)
            {
                DataRowView localidadSeleccionada = cmbLocalidad.SelectedItem as DataRowView;
                int idProvincia = Convert.ToInt32(localidadSeleccionada["idProvincia"]);

                // Buscar la provincia correspondiente en el ComboBox de provincias y seleccionarla
                DataRow[] provinciaRows = sistemaVNDDataSetProvincias.Tables["provincia"].Select($"idprovincia = {idProvincia}");
                if (provinciaRows.Length > 0)
                {
                    int indexProvincia = cmbProvincia.FindStringExact(provinciaRows[0]["nombreProvincia"].ToString());
                    if (indexProvincia >= 0)
                    {
                        cmbProvincia.SelectedIndex = indexProvincia;
                    }
                }
            }
        }

        private void limpiarForm()
        {
            txbNombreTra.Text = "";
            txbNombreTra.Enabled = true;
            maskedTextBox1.Text = "";
            txbTelTra.Text = "";
            txbMailTra.Text = "";
            txbCalle.Text = "";
            txbNumero.Text = "";
            txbNumero.Enabled = true;
            txbBarrio.Text = "";
            cmbLocalidad.SelectedItem = null;
            cmbProvincia.SelectedItem = null;
            txbBuscar.Text = "";
        }



        private void button9_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            consultarTransportista ct = new consultarTransportista();
            ct.ShowDialog();
        }


        private void txbCuitTra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 32 && e.KeyChar <= 47) || (e.KeyChar > 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Ingresar solo números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }


        private void txbTelTra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 33 && e.KeyChar <= 39) || (e.KeyChar > 42 && e.KeyChar <= 44) || (e.KeyChar >= 46 && e.KeyChar <= 47) || (e.KeyChar > 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Formato incorrecto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }


        private void txbNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 32 && e.KeyChar <= 47) || (e.KeyChar > 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Ingresar solo números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}
