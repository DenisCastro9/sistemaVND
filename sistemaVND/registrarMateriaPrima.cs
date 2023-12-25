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
    public partial class registrarMateriaPrima : Form
    {
        private consultarMateriaPrima formPrincipal;
        int nro = 0;
        int uDM = 0;
        int subUDM = 0;
        int tipo = 0;
        int marca = 0;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public registrarMateriaPrima(consultarMateriaPrima form)
        {
            InitializeComponent();
            formPrincipal = form;
            cargarNumeroMP();


            cargarComboUDM1();
            cargarComboSUBUDM();
            cargarComboTipoMP();
            cargarComboMarcaMP();

            autocomUDM();
            autocomSubUDM();
            autocomMarca();
            autocomTipo();
        }

        //--------------------AUTOCOMPLETAR (NO OLVIDARSE CAMBIAR LAS PROPIEDADES DEL COMBO)-----------------
        private void autocomTipo()
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            SqlCommand cmd = new SqlCommand("SELECT descripcion FROM tipoMP", conexion);
            conexion.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                source.Add(reader["descripcion"].ToString());
            }
            reader.Close();
            conexion.Close();
            comboBoxTipo.AutoCompleteCustomSource = source;
        }
        private void autocomMarca()
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            SqlCommand cmd = new SqlCommand("SELECT descripcion FROM marcaMP", conexion);
            conexion.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                source.Add(reader["descripcion"].ToString());
            }
            reader.Close();
            conexion.Close();
            comboBox1.AutoCompleteCustomSource = source;
        }
        private void autocomSubUDM()
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            SqlCommand cmd = new SqlCommand("SELECT descripcion FROM SubUnidadDeMedida", conexion);
            conexion.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                source.Add(reader["descripcion"].ToString());
            }
            reader.Close();
            conexion.Close();
            comboBox3.AutoCompleteCustomSource = source;
        }
        private void autocomUDM()
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            SqlCommand cmd = new SqlCommand("SELECT descripcion FROM unidadDeMedida", conexion);
            conexion.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                source.Add(reader["descripcion"].ToString());
            }
            reader.Close();
            conexion.Close();
            comboBox2.AutoCompleteCustomSource = source;
        }






        private void cargarNumeroMP()
        {
            conexion.Open();
            string query2 = " SELECT TOP 1 codigo FROM materiaPrima ORDER BY codigo DESC";
            SqlCommand command = new SqlCommand(query2, conexion);
            SqlDataReader registro = command.ExecuteReader();
            if (registro.Read())
            {
                nro = Convert.ToInt32(registro["codigo"].ToString());
                nro = nro + 1;
            }
            textBoxCod.Text = Convert.ToString(nro);
            conexion.Close();
        }





        private void cargarComboUDM1()
        {
            SqlCommand cm = new SqlCommand("select * from unidadDeMedida", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr.GetString(1)); //ese numero representa la posicion del elemento
                //en la tabla de la BD
            }
            conexion.Close();
        }

        private void cargarComboSUBUDM()
        {
            SqlCommand cm = new SqlCommand("select * from subUnidadDeMedida", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr.GetString(1)); //ese numero representa la posicion del elemento
                //en la tabla de la BD
            }
            conexion.Close();
        }

        private void cargarComboMarcaMP()
        {
            SqlCommand cm = new SqlCommand("select * from marcaMP", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr.GetString(1)); //ese numero representa la posicion del elemento
                //en la tabla de la BD
            }
            conexion.Close();
        }

        private void cargarComboTipoMP()
        {
            SqlCommand cm = new SqlCommand("select * from tipoMP", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboBoxTipo.Items.Add(dr.GetString(1)); //ese numero representa la posicion del elemento
                //en la tabla de la BD
            }
            conexion.Close();
        }



        private bool existeMP()
        {
            bool existe = false;
            if (textBoxCod.Text == "")
            {
                MessageBox.Show("El código no puede estar vacío", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                conexion.Open();
                string sql = "select * from materiaPrima where codigo=@codigo";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@codigo", SqlDbType.VarChar).Value = textBoxCod.Text;
                SqlDataReader registro = comando.ExecuteReader();
                if (registro.Read())
                    existe = true;
                conexion.Close();
            }
            return existe;
        }




        //BOTON REGISTRAR
        private void button1_Click(object sender, EventArgs e)
        {
            if (!existeMP())
            {
                if (textBoxCod.Text == "" || textBoxNom.Text == "" || textBoxStock.Text == "")
                {
                    MessageBox.Show("Completar campos obligatorios *", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                conexion.Open();

                string sql = "insert into materiaPrima (codigo, descripcion, stockMinimo, cantidad , IdUnidadDeMedida, cantidadQueContiene,IdSubUnidadDeMedidda, idMarcaMP, idtipoMP, reservado) values (@codigo, @descripcion, @stockMinimo, @cantidad, @IdUnidadDeMedida, @cantidadQueContiene, @IdSubUnidadDeMedidda, @idMarcaMP, @idtipoMP, @reservado)";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@codigo", SqlDbType.VarChar).Value = textBoxCod.Text;
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = textBoxNom.Text;
                comando.Parameters.Add("@stockMinimo", SqlDbType.Int).Value = textBoxStock.Text;
                comando.Parameters.Add("@cantidad", SqlDbType.Int).Value = "0";
                comando.Parameters.Add("@IdUnidadDeMedida", SqlDbType.Int).Value =uDM;
                comando.Parameters.Add("@cantidadQueContiene", SqlDbType.Decimal).Value = Convert.ToDecimal(textBox1.Text);
                comando.Parameters.Add("@IdSubUnidadDeMedidda", SqlDbType.Int).Value = subUDM;
                comando.Parameters.Add("@idMarcaMP", SqlDbType.Int).Value = marca;
                comando.Parameters.Add("@idtipoMP", SqlDbType.Int).Value = tipo;
                comando.Parameters.Add("@reservado", SqlDbType.Int).Value = "0";
                comando.ExecuteNonQuery();

                conexion.Close();
                MessageBox.Show("Se cargó la materia prima", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                blanquearForm();
                cargarNumeroMP();
            }
            else
            {
                MessageBox.Show("Ya existe una materia prima con ese código", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cargarNumeroMP();
            }

        }





        private void blanquearForm()
        {
            cargarNumeroMP();
            comboBox2.SelectedIndex = -1;
            textBoxCod.Text = "";
            textBoxNom.Text = "";
            textBox1.Text = "";
            comboBox3.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
            comboBoxTipo.SelectedIndex = -1;
            textBoxStock.Text = "";
        }


        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            
        }


        //---------------------sacar los ID-----------------------------//

        //sacar el ID DE UNIDAD DE MEDIDA
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand("select id from unidadDeMedida where descripcion= '" + comboBox2.Text + "'", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            if (dr.Read() == true)
            {
                uDM = Convert.ToInt32(dr["id"].ToString());//CON ESTO SACO EL ID DE la unidad de medida QUE ESTOY SELECCIONANDO
            }
            conexion.Close();
        }

        //sacar el ID de subunidadDeMedida
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand("select id from SubUnidadDeMedida where descripcion= '" + comboBox3.Text + "'", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            if (dr.Read() == true)
            {
                subUDM = Convert.ToInt32(dr["id"].ToString());//CON ESTO SACO EL ID DE la unidad de medida QUE ESTOY SELECCIONANDO
            }
            conexion.Close();
        }

        //SACAR EL ID DE MARCA
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand("select id from marcaMP where descripcion= '" + comboBox1.Text + "'", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            if (dr.Read() == true)
            {
                marca = Convert.ToInt32(dr["id"].ToString());//CON ESTO SACO EL ID DE la unidad de medida QUE ESTOY SELECCIONANDO
            }
            conexion.Close();
        }

        //SACAR EL ID DE TIPO MP
        private void comboBoxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand("select id from tipoMP where descripcion= '" + comboBoxTipo.Text + "'", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            if (dr.Read() == true)
            {
                tipo = Convert.ToInt32(dr["id"].ToString());//CON ESTO SACO EL ID DE la unidad de medida QUE ESTOY SELECCIONANDO
            }
            conexion.Close();
        }




        //VALIDAR SOLO ENTEROS
        private void textBoxStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 32 && e.KeyChar <= 47) || (e.KeyChar > 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Ingresar números enteros", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 32 && e.KeyChar <= 43) || (e.KeyChar > 45 && e.KeyChar <= 47) || (e.KeyChar > 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Ingresar formato: 0,00", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            formPrincipal.actualizarPantalla();
            this.Close();
        }
    }
}
