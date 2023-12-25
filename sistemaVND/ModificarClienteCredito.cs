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
    public partial class ModificarClienteCredito : Form
    {
        string cliente;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public ModificarClienteCredito(string cliente)
        {
            InitializeComponent();
            this.cliente = cliente;
            textBox1.Text = cliente; 
            cargarTransportista();
            cargarjurisdiccion();
            comboIva();
            cargarCredito();
            cargarDescuento();
            cargarDatos();
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





        private void cargarDatos()
        {
            conexion.Open();
            string sql = "select c.cuit as cuit, c.mail as mail, c.telefono as telefono, c.ingresosBrutos as ingresosBrutos, c.condicionIva as condicionIva, c.domicilioDeEntrega as domicilioDeEntrega, c.calle as calle, c.altura as altura, c.barrio as barrio, l.nombreLocalidad as localidad, p.nombreProvincia as provincia, cred.limite as credito, d.descripcion as descuento,  j.jurisdiccion as jurisdiccion, t.nombre as transporte from cliente as c join localidad as l on c.idLocalidad = l.idLocalidad join provincia as p on c.idProvincia = p.idprovincia join credito as cred on c.idCredito = cred.idcreditO join descuento as d on c.idDescuento = d.iddescuento join jurisdiccion as j on c.idJurisdiccion = j.idjurisdiccion join transportista as t on c.idTransportista = t.id where razonSocial = @razonSocial";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.Add("@razonSocial", SqlDbType.VarChar).Value = cliente;
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                maskedTextBox1.Text = registro["cuit"].ToString();
                string correo = registro["mail"].ToString();

                // Extraer la parte antes del '@' del correo electrónico
                if (correo.Contains("@"))
                {
                    string parteAntesDelArroba = correo.Substring(0, correo.IndexOf("@"));
                    textBox3.Text = parteAntesDelArroba;
                }
                else
                {
                    textBox3.Text = correo; // Si no se encuentra '@', deja el correo sin cambios.
                }

                // Extraer el dominio del correo electrónico y mostrarlo en cmbExtension
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
                else
                {
                    // Si no se encuentra '@' en el correo, limpia la selección en cmbExtension
                    cmbExtension.SelectedItem = null;
                }

                textBox4.Text = registro["telefono"].ToString();
                textBox5.Text = registro["ingresosBrutos"].ToString();
                comboBox1.Text = registro["condicionIva"].ToString();
                textBox6.Text = registro["domicilioDeEntrega"].ToString();                
                textBox10.Text = registro["calle"].ToString();
                textBox9.Text = registro["altura"].ToString();
                textBox8.Text = registro["barrio"].ToString();
                comboBox4.Text = registro["localidad"].ToString();
                comboBox2.Text = registro["provincia"].ToString();
                comboBox5.Text = registro["credito"].ToString();
                comboBox6.Text = registro["descuento"].ToString();
                comboBox7.Text = registro["jurisdiccion"].ToString();
                comboBox3.Text = registro["transporte"].ToString();
            }
            else
            {
                MessageBox.Show("No existe el cliente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            conexion.Close();
        }




        //MODIFICAR
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || maskedTextBox1.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "")
            {
                MessageBox.Show("Completar campos obligatorios *", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            conexion.Open();
            string sql = "update cliente set cuit = @cuit, mail =@mail, telefono=@telefono, ingresosBrutos=@ingresosBrutos, condicionIva=@condicionIva, domicilioDeEntrega = @domicilioDeEntrega,  calle = @calle, altura = @altura, barrio = @barrio, idLocalidad = @localidad, idProvincia = @provincia, idCredito = @credito, idDescuento = @descuento, idJurisdiccion = @jurisdiccion, idTransportista = @transportista where razonSocial = @razonSocial";
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
            comando.Parameters.Add("@localidad", SqlDbType.Int).Value = comboBox4.SelectedValue;
            comando.Parameters.Add("@provincia", SqlDbType.Int).Value = comboBox2.SelectedValue;
            comando.Parameters.Add("@credito", SqlDbType.Int).Value = comboBox5.SelectedValue;
            comando.Parameters.Add("@descuento", SqlDbType.Int).Value = comboBox6.SelectedValue;
            comando.Parameters.Add("@jurisdiccion", SqlDbType.Int).Value = comboBox7.SelectedValue;
            comando.Parameters.Add("@transportista", SqlDbType.Int).Value = comboBox3.SelectedValue;
            int cant = comando.ExecuteNonQuery();
            if (cant == 1)
            {
                MessageBox.Show("Datos modificados", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No existe ese cliente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            conexion.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void ModificarClienteCredito_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'sistemaVNDDataSetProvincias.provincia' Puede moverla o quitarla según sea necesario.
            this.provinciaTableAdapter.Fill(this.sistemaVNDDataSetProvincias.provincia);
            // TODO: esta línea de código carga datos en la tabla 'sistemaVNDDataSetLocalidad.localidad' Puede moverla o quitarla según sea necesario.
            this.localidadTableAdapter.Fill(this.sistemaVNDDataSetLocalidad.localidad);

        }
    }
}
