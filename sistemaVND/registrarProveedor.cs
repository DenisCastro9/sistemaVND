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
    public partial class registrarProveedor : Form
    {
        
        public registrarProveedor()
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
            cargarLocalidadP(); // Llamar al método cargarDatos() cuando se cierra registrarLocalidad
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
            cargarProvinciaP();
        }











        public void cargarProvinciaP()
        {
            this.provinciaTableAdapter.Fill(this.sistemaVNDDataSetProvincias.provincia);
        }
        public void cargarLocalidadP()
        {
            this.localidadTableAdapter.Fill(this.sistemaVNDDataSetLocalidad.localidad);
        }








        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        private SqlDataAdapter adaptador, adaptadorD, adaptadorR, adaptadorM, adaptadorM2, adaptadorE, adaptadorE2;
        private DataSet datos;


 
        private void registrarProveedor_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'sistemaVNDDataSetLocalidad.localidad' Puede moverla o quitarla según sea necesario.
            this.localidadTableAdapter.Fill(this.sistemaVNDDataSetLocalidad.localidad);
            // TODO: esta línea de código carga datos en la tabla 'sistemaVNDDataSetProvincias.provincia' Puede moverla o quitarla según sea necesario.
            this.provinciaTableAdapter.Fill(this.sistemaVNDDataSetProvincias.provincia);


            registrarProv();
            registrarDomicilio();
            recuperar();
            modificarProv();
            modificarDomi();
            eliminarProv();
            eliminarDomi();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(txbNombreProv.Text == "")
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
                    if(txbMailProv.Text == "" && cmbExtension.SelectedItem == null)
                    {
                        MessageBox.Show("Ingresar Email", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if(txbTelProv.Text == "")
                        {
                            MessageBox.Show("Ingresar teléfono", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if(cmbIvaProv.SelectedItem == null)
                            {
                                MessageBox.Show("Seleccionar condición frente al IVA", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                if(txbCalle.Text == "")
                                {
                                    MessageBox.Show("Ingresar nombre de la calle", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
                                                    //string mail = txbMailProv.Text;
                                                    //string extension = cmbExtension.SelectedItem.ToString();
                                                    //string mailCompleto = mail + extension;
                                                    adaptador.InsertCommand.Parameters["@nombre"].Value = txbNombreProv.Text;
                                                    adaptador.InsertCommand.Parameters["@cuit"].Value = maskedTextBox1.Text;
                                                    adaptador.InsertCommand.Parameters["@mail"].Value = txbMailProv.Text + cmbExtension.SelectedItem.ToString();
                                                    adaptador.InsertCommand.Parameters["@telefono"].Value = txbTelProv.Text;
                                                    adaptador.InsertCommand.Parameters["@condicionIva"].Value = cmbIvaProv.SelectedItem;
                                                    adaptador.InsertCommand.Parameters["@idDomicilio"].Value = txbNumero.Text;
                                                    adaptadorD.InsertCommand.Parameters["@numero"].Value = txbNumero.Text;
                                                    adaptadorD.InsertCommand.Parameters["@calle"].Value = txbCalle.Text;
                                                    adaptadorD.InsertCommand.Parameters["@nombreBarrio"].Value = txbBarrio.Text;
                                                    adaptadorD.InsertCommand.Parameters["@idLocalidad"].Value = cmbLocalidad.SelectedValue;
                                                    adaptadorD.InsertCommand.Parameters["@idProvincia"].Value = cmbProvincia.SelectedValue;
                                                    try
                                                    {
                                                        conexion.Open();
                                                        adaptador.InsertCommand.ExecuteNonQuery();
                                                        adaptadorD.InsertCommand.ExecuteNonQuery();
                                                        MessageBox.Show("Proveedor registrado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }


        //Registro del Proveedor
        private void registrarProv()
        {
            SqlCommand registrar = new SqlCommand("INSERT INTO proveedor VALUES (@nombre, @cuit, @mail, @telefono, @condicionIva, @idDomicilio)", conexion);
            adaptador = new SqlDataAdapter();
            adaptador.InsertCommand = registrar;
            adaptador.InsertCommand.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new SqlParameter("@cuit", SqlDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new SqlParameter("@mail", SqlDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new SqlParameter("@telefono", SqlDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new SqlParameter("@condicionIva", SqlDbType.VarChar));
            adaptador.InsertCommand.Parameters.Add(new SqlParameter("@idDomicilio", SqlDbType.Int));
        }


        private void registrarDomicilio()
        {
            SqlCommand registrarD = new SqlCommand("INSERT INTO domicilio VALUES (@numero, @calle, @nombreBarrio, @idLocalidad, @idProvincia)", conexion);
            adaptadorD = new SqlDataAdapter();
            adaptadorD.InsertCommand = registrarD;
            adaptadorD.InsertCommand.Parameters.Add(new SqlParameter("@numero", SqlDbType.Int));
            adaptadorD.InsertCommand.Parameters.Add(new SqlParameter("@calle", SqlDbType.VarChar));
            adaptadorD.InsertCommand.Parameters.Add(new SqlParameter("@nombreBarrio", SqlDbType.VarChar));
            adaptadorD.InsertCommand.Parameters.Add(new SqlParameter("@idLocalidad", SqlDbType.Int));
            adaptadorD.InsertCommand.Parameters.Add(new SqlParameter("@idProvincia", SqlDbType.Int));
        }


        private void button4_Click(object sender, EventArgs e)
        {
            consultarProveedor cp = new consultarProveedor();
            cp.Show();
        }


  


        //Modificar Proveedor
        private void button5_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txbBuscar.Text))
            {
                MessageBox.Show("Ingresar nombre de un proveedor registrado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    conexion.Open();
                    datos = new DataSet();
                    adaptadorR.SelectCommand.Parameters["@nombre"].Value = txbBuscar.Text;
                    adaptadorR.Fill(datos, "proveedores");
                    int registro = int.Parse(datos.Tables["proveedores"].Rows.Count.ToString());
                    if(registro == 1)
                    {
                        foreach(DataRow fila in datos.Tables["proveedores"].Rows)
                        {
                            txbNombreProv.Text = fila["nombre"].ToString();
                            maskedTextBox1.Text = fila["cuit"].ToString();
                            txbMailProv.Text = fila["mail"].ToString();
                            txbTelProv.Text = fila["telefono"].ToString();
                            cmbIvaProv.SelectedItem = fila["condicionIva"].ToString();
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
                                txbMailProv.Text = nombreAntesDeArroba;
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
                        txbNombreProv.Enabled = false;
                        txbNumero.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("No existe el proveedor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        }


        private void recuperar()
        {
            SqlCommand buscar = new SqlCommand("SELECT proveedor.nombre, proveedor.cuit, proveedor.mail, proveedor.telefono, proveedor.condicionIva, domicilio.numero, domicilio.calle, domicilio.nombreBarrio, localidad.nombreLocalidad, provincia.nombreProvincia FROM proveedor INNER JOIN domicilio ON proveedor.idDomicilio = domicilio.numero" +
                "                                                                                                                                                                                                                                                                   INNER JOIN localidad ON domicilio.idLocalidad = localidad.idLocalidad" +
                "                                                                                                                                                                                                                                                                   INNER JOIN provincia ON domicilio.idProvincia = provincia.idprovincia WHERE proveedor.nombre = @nombre", conexion);
            adaptadorR = new SqlDataAdapter();
            adaptadorR.SelectCommand = buscar;
            adaptadorR.SelectCommand.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));

        }





        //MODIFICAR BOTON
        private void button2_Click(object sender, EventArgs e)
        {
            if(txbNombreProv.Text == "")
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
                    if(txbMailProv.Text == "" && cmbExtension.SelectedItem == null)
                    {
                        MessageBox.Show("Ingresar mail", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if(txbTelProv.Text == "")
                        {
                            MessageBox.Show("Ingresar teléfono", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if(cmbIvaProv.SelectedItem == null)
                            {
                                MessageBox.Show("Seleccionar condición frente al IVA", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                                if (cmbProvincia.SelectedItem == null)
                                                {
                                                    MessageBox.Show("Seleccionar provincia", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                }
                                                else
                                                {
                                                    adaptadorM.UpdateCommand.Parameters["@nombre"].Value = txbNombreProv.Text;
                                                    adaptadorM.UpdateCommand.Parameters["@cuit"].Value = maskedTextBox1.Text;
                                                    adaptadorM.UpdateCommand.Parameters["@mail"].Value = txbMailProv.Text + cmbExtension.SelectedItem.ToString();
                                                    adaptadorM.UpdateCommand.Parameters["@telefono"].Value = txbTelProv.Text;
                                                    adaptadorM.UpdateCommand.Parameters["@condicionIva"].Value = cmbIvaProv.SelectedItem;
                                                    adaptadorM2.UpdateCommand.Parameters["@nombre2"].Value = txbNombreProv.Text;
                                                    adaptadorM2.UpdateCommand.Parameters["@calle"].Value = txbCalle.Text;
                                                    adaptadorM2.UpdateCommand.Parameters["@nombreBarrio"].Value = txbBarrio.Text;
                                                    adaptadorM2.UpdateCommand.Parameters["@idLocalidad"].Value = cmbLocalidad.SelectedValue;
                                                    adaptadorM2.UpdateCommand.Parameters["@idProvincia"].Value = cmbProvincia.SelectedValue;
                                                    try
                                                    {
                                                        conexion.Open();
                                                        adaptadorM.UpdateCommand.ExecuteNonQuery();
                                                        adaptadorM2.UpdateCommand.ExecuteNonQuery();
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
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        private void modificarProv()
        {
            SqlCommand modificar = new SqlCommand("UPDATE proveedor SET proveedor.cuit = @cuit, proveedor.mail = @mail, proveedor.telefono = @telefono, proveedor.condicionIva = @condicionIva FROM proveedor, domicilio WHERE proveedor.nombre = @nombre AND proveedor.idDomicilio = domicilio.numero", conexion);
            adaptadorM = new SqlDataAdapter();
            adaptadorM.UpdateCommand = modificar;
            adaptadorM.UpdateCommand.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
            adaptadorM.UpdateCommand.Parameters.Add(new SqlParameter("@cuit", SqlDbType.VarChar));
            adaptadorM.UpdateCommand.Parameters.Add(new SqlParameter("@mail", SqlDbType.VarChar));
            adaptadorM.UpdateCommand.Parameters.Add(new SqlParameter("@telefono", SqlDbType.VarChar));
            adaptadorM.UpdateCommand.Parameters.Add(new SqlParameter("@condicionIva", SqlDbType.VarChar));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        //VALIDAR NUMEROS EN CUIT
        private void txbCuitProv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 32 && e.KeyChar <= 47) || (e.KeyChar > 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Ingresar solo numeros", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void maskedTextBox1_Click(object sender, EventArgs e)
        {
            maskedTextBox1.SelectionStart = 0;
        }

        private void cmbLocalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLocalidad.SelectedItem != null)
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

        private void txbNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 32 && e.KeyChar <= 47) || (e.KeyChar > 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Ingresar solo números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        //VALIDAR NUMEROS EN TELEFONO
        private void txbTelProv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 33 && e.KeyChar <= 39) || (e.KeyChar > 42 && e.KeyChar <= 44) || (e.KeyChar >= 46 && e.KeyChar <= 47) || (e.KeyChar > 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Formato incorrecto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void modificarDomi()
        {
            SqlCommand modificar2 = new SqlCommand("UPDATE domicilio SET domicilio.calle = @calle, domicilio.nombreBarrio = @nombreBarrio, domicilio.idLocalidad = @idLocalidad, domicilio.idProvincia = @idProvincia FROM domicilio INNER JOIN proveedor ON domicilio.numero = proveedor.idDomicilio WHERE proveedor.nombre = @nombre2", conexion);
            adaptadorM2 = new SqlDataAdapter();
            adaptadorM2.UpdateCommand = modificar2;
            adaptadorM2.UpdateCommand.Parameters.Add(new SqlParameter("@calle", SqlDbType.VarChar));
            adaptadorM2.UpdateCommand.Parameters.Add(new SqlParameter("@nombreBarrio", SqlDbType.VarChar));
            adaptadorM2.UpdateCommand.Parameters.Add(new SqlParameter("@idLocalidad", SqlDbType.Int));
            adaptadorM2.UpdateCommand.Parameters.Add(new SqlParameter("@idProvincia", SqlDbType.Int));
            adaptadorM2.UpdateCommand.Parameters.Add(new SqlParameter("@nombre2", SqlDbType.VarChar));
        }


        //Eliminar Proveedor (AGREGUE LA CONFIRMACION DE LA ELIMINACION)
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbNombreProv.Text))
            {
                MessageBox.Show("Ingresar el nombre en el lugar indicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                MessageBoxButtons botones = MessageBoxButtons.YesNo;
                DialogResult dr = MessageBox.Show("¿Desea eliminar el proveedor?", "Eliminar proveedor",
                    botones, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {

                    adaptadorE2.DeleteCommand.Parameters["@nombre2"].Value = txbNombreProv.Text;
                    try
                    {
                        conexion.Open();
                        adaptadorE2.DeleteCommand.ExecuteNonQuery();
                        int proc = Convert.ToInt32(adaptadorE2.DeleteCommand.ExecuteNonQuery());
                        if (proc < 1)
                        {
                            adaptadorE.DeleteCommand.Parameters["@nombre"].Value = txbNombreProv.Text;
                            adaptadorE.DeleteCommand.ExecuteNonQuery();
                            MessageBox.Show("Proveedor eliminado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiarForm();
                        }
                        else
                        {
                            MessageBox.Show("Error de eliminación", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                else if (dr == DialogResult.No)
                {
                    return;
                }
            }
        }

        private void eliminarProv()
        {
            SqlCommand eliminar = new SqlCommand("DELETE FROM proveedor WHERE proveedor.nombre = @nombre", conexion);
            adaptadorE = new SqlDataAdapter();
            adaptadorE.DeleteCommand = eliminar;
            adaptadorE.DeleteCommand.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
        }


        private void eliminarDomi()
        {
            SqlCommand eliminar2 = new SqlCommand("DELETE FROM domicilio FROM domicilio INNER JOIN proveedor ON domicilio.numero = proveedor.idDomicilio AND proveedor.nombre = @nombre2", conexion);
            adaptadorE2 = new SqlDataAdapter();
            adaptadorE2.DeleteCommand = eliminar2;
            adaptadorE2.DeleteCommand.Parameters.Add(new SqlParameter("@nombre2", SqlDbType.VarChar));
        }

      
        private void limpiarForm()
        {
            txbNombreProv.Text = "";
            txbNombreProv.Enabled = true;
            maskedTextBox1.Text = "";
            txbMailProv.Text = "";
            txbTelProv.Text = "";
            cmbIvaProv.SelectedItem = null;
            txbCalle.Text = "";
            txbNumero.Text = "";
            txbNumero.Enabled = true;
            txbBarrio.Text = "";
            cmbLocalidad.SelectedItem = null;
            cmbProvincia.SelectedItem = null;
            txbBuscar.Text = "";
        }

  
    }
}
