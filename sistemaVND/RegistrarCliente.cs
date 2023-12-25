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
    public partial class RegistrarCliente : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");

        public RegistrarCliente()
        {
            InitializeComponent();
            //cargarProvincia();


            //cargarLocalidad();            
            cargarjurisdiccion();


            cargarTransportista();            
            comboIva();
            cargarCredito();
            cargarDescuento();



            comboBox1.SelectedIndex = -1; //condicion IVA
            comboBox3.SelectedIndex = -1;//transportista
            comboBox4.SelectedIndex = -1;//localidad
            comboBox2.SelectedIndex = -1;//provincia
        }












        private void cargarDescuento()
        {
            conexion.Open();
            string sql = "select iddescuento, descripcion from descuento";
            SqlCommand comando = new SqlCommand(sql, conexion);
            DataTable tabla1 = new DataTable();
            SqlDataAdapter adaptador1 = new SqlDataAdapter();
            adaptador1.SelectCommand = comando;
            adaptador1.Fill(tabla1);
            comboBox6.DataSource = tabla1;
            comboBox6.DisplayMember = "descripcion";
            comboBox6.ValueMember = "iddescuento";
            conexion.Close();
        }

        private void cargarCredito()
        {
            conexion.Open();
            string sql = "select idcreditO, limite from credito";
            SqlCommand comando = new SqlCommand(sql, conexion);
            DataTable tabla1 = new DataTable();
            SqlDataAdapter adaptador1 = new SqlDataAdapter();
            adaptador1.SelectCommand = comando;
            adaptador1.Fill(tabla1);
            comboBox5.DataSource = tabla1;
            comboBox5.DisplayMember = "limite";
            comboBox5.ValueMember = "idcreditO";
            conexion.Close();
        }

        private void comboIva()
        {
            comboBox1.Items.Add("Responsable inscripto"); //0
            comboBox1.Items.Add("Monotributo");//1
            comboBox1.Items.Add("Exento");//2
            comboBox1.Items.Add("No responsable");//3
            comboBox1.Items.Add("Consumidor final");//4
        }

        private void cargarjurisdiccion()
        {
            conexion.Open();
            string sql = "select idjurisdiccion, jurisdiccion from jurisdiccion";
            SqlCommand comando = new SqlCommand(sql, conexion);
            DataTable tabla1 = new DataTable();
            SqlDataAdapter adaptador1 = new SqlDataAdapter();
            adaptador1.SelectCommand = comando;
            adaptador1.Fill(tabla1);
            comboBox7.DataSource = tabla1;
            comboBox7.DisplayMember = "jurisdiccion";
            comboBox7.ValueMember = "idjurisdiccion";
            conexion.Close();
        }

        private void cargarTransportista()
        {
            conexion.Open();
            string sql = "select id, nombre from transportista";
            SqlCommand comando = new SqlCommand(sql, conexion);
            DataTable tabla1 = new DataTable();
            SqlDataAdapter adaptador1 = new SqlDataAdapter();
            adaptador1.SelectCommand = comando;
            adaptador1.Fill(tabla1);
            comboBox3.DataSource = tabla1;
            comboBox3.DisplayMember = "nombre";
            comboBox3.ValueMember = "id";
            conexion.Close();
        }












        //BOTON REGISTRAR..
        private void button1_Click(object sender, EventArgs e)
        {
            if (!ExisteArticulo())
            {
                if (textBox1.Text == "" || maskedTextBox1.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox10.Text == "" || textBox9.Text == "" || textBox8.Text == "" || textBox6.Text == "" || comboBox1.Items.Count <= 0 || comboBox3.Items.Count <= 0 || comboBox2.Items.Count <= 0)
                {
                    MessageBox.Show("Completar campos obligatorios *", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                conexion.Open();
                string sql = "insert into cliente (razonSocial, cuit, mail, telefono, ingresosBrutos, condicionIva, domicilioDeEntrega, calle, altura, barrio, idLocalidad, idProvincia, idCredito, idDescuento, idJurisdiccion, idTransportista)" +
                    " values (@razonSocial, @cuit, @mail, @telefono, @ingresosBrutos, @condicionIva, @domicilioDeEntrega, @calle, @altura, @barrio, @idLocalidad, @idProvincia, @idCredito, @idDescuento, @idJurisdiccion, @idTransportista)";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@razonSocial", SqlDbType.VarChar).Value = textBox1.Text;
                comando.Parameters.Add("@cuit", SqlDbType.VarChar).Value = maskedTextBox1.Text;
                comando.Parameters.Add("@mail", SqlDbType.VarChar).Value = textBox3.Text + cmbExtension.SelectedItem.ToString();
                comando.Parameters.Add("@telefono", SqlDbType.VarChar).Value = textBox4.Text;
                comando.Parameters.Add("@ingresosBrutos", SqlDbType.VarChar).Value = textBox5.Text;
                comando.Parameters.Add("@condicionIva", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
                comando.Parameters.Add("@domicilioDeEntrega", SqlDbType.VarChar).Value = textBox6.Text;                
                comando.Parameters.Add("@calle", SqlDbType.VarChar).Value = textBox10.Text;
                comando.Parameters.Add("@altura", SqlDbType.Int).Value = textBox9.Text;
                comando.Parameters.Add("@barrio", SqlDbType.VarChar).Value = textBox8.Text;
                comando.Parameters.Add("@idLocalidad", SqlDbType.Int).Value = comboBox4.SelectedValue;
                comando.Parameters.Add("@idProvincia", SqlDbType.Int).Value = comboBox2.SelectedValue;
                comando.Parameters.Add("@idCredito", SqlDbType.Int).Value = comboBox5.SelectedValue;
                comando.Parameters.Add("@idDescuento", SqlDbType.Int).Value = comboBox6.SelectedValue;
                comando.Parameters.Add("@idJurisdiccion", SqlDbType.Int).Value = comboBox7.SelectedValue;
                comando.Parameters.Add("@idTransportista", SqlDbType.Int).Value = comboBox3.SelectedValue;
                comando.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Cliente registrado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                blanquearForm();
            }
            else
            {
                MessageBox.Show("Ya existe un cliente con ese nombre", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                blanquearForm();
            }
        }   

















        //BOTON VER REGISTRADOS
        private void button5_Click(object sender, EventArgs e)
        {
            consultarCliente cc = new consultarCliente();
            cc.ShowDialog();
        }
        private void blanquearForm()
        {
            textBox1.Text = "";
            maskedTextBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            comboBox1.SelectedIndex = -1; //condicion IVA
            comboBox3.SelectedIndex = -1;//transportista
            comboBox4.SelectedIndex = -1;//localidad
            comboBox2.SelectedIndex = -1;//provincia
            comboBox7.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
        }
        private bool ExisteArticulo()
        {
            bool existe = false;
            if (textBox1.Text == "")
            {
                MessageBox.Show("La razón social del cliente no puede estar vacía", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                conexion.Open();

                string sql = "select * from cliente where razonSocial=@razonSocial";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@razonSocial", SqlDbType.VarChar).Value = textBox1.Text;
                SqlDataReader registro = comando.ExecuteReader();
                if (registro.Read())
                    existe = true;
                conexion.Close();

            }
            return existe;
        }








        //SOLO NUMEROS, PARENTESIS, ESPACIOS Y GUIONES EN TELEFONO
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 33 && e.KeyChar <= 39) || (e.KeyChar > 42 && e.KeyChar <= 44) || (e.KeyChar >= 46 && e.KeyChar <= 47) || (e.KeyChar > 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Formato incorrecto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
        //SOLO NUMEROS ESPACIOS Y GUIONES EN INRGESOS BRUTOS      
        private void textBox5_KeyUp(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 33 && e.KeyChar <= 44) || (e.KeyChar >= 46 && e.KeyChar <= 47) || (e.KeyChar > 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Formato incorrecto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }






        private void button6_Click(object sender, EventArgs e)
        {
            registrarLocalidad localidadForm = new registrarLocalidad();
            localidadForm.FormClosed += LocalidadForm_FormClosed; // Suscribirse al evento FormClosed
            localidadForm.ShowDialog();
        }

        private void LocalidadForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //cargarLocalidad(); 
            localidadBindingSource.ResetBindings(true);// Llamar al método cargarDatos() cuando se cierra registrarLocalidad
        }

        private void button2_Click(object sender, EventArgs e)
        {
            registrarProvincia provinciaForm = new registrarProvincia();
            provinciaForm.FormClosed += ProvinciaForm_FormClosed; // Suscribirse al evento FormClosed
            provinciaForm.ShowDialog();
        }

        private void ProvinciaForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //cargarProvincia();
            provinciaBindingSource.ResetBindings(true);
        }

        private void maskedTextBox1_Click(object sender, EventArgs e)
        {
            maskedTextBox1.SelectionStart = 0;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem != null)
            {
                DataRowView localidadSeleccionada = comboBox4.SelectedItem as DataRowView;
                int idProvincia = Convert.ToInt32(localidadSeleccionada["idProvincia"]);

                // Buscar la provincia correspondiente en el ComboBox de provincias y seleccionarla
                DataRow[] provinciaRows = sistemaVNDDataSetProvincias.Tables["provincia"].Select($"idprovincia = {idProvincia}");
                if (provinciaRows.Length > 0)
                {
                    int indexProvincia = comboBox2.FindStringExact(provinciaRows[0]["nombreProvincia"].ToString());
                    if (indexProvincia >= 0)
                    {
                        comboBox2.SelectedIndex = indexProvincia;
                    }
                }
            }
        }

        private void RegistrarCliente_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'sistemaVNDDataSetProvincias.provincia' Puede moverla o quitarla según sea necesario.
            this.provinciaTableAdapter.Fill(this.sistemaVNDDataSetProvincias.provincia);
            // TODO: esta línea de código carga datos en la tabla 'sistemaVNDDataSetLocalidad.localidad' Puede moverla o quitarla según sea necesario.
            this.localidadTableAdapter.Fill(this.sistemaVNDDataSetLocalidad.localidad);

        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
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
