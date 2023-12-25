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
    public partial class ConfigDeposito : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public ConfigDeposito()
        {
            InitializeComponent();
            cargarComboProveedorMarca();
            cargarDataMarcas();
            cargarComboProveedorTipo();
            cargarDataTipo();
            cargarComboUDM();

            autocomplUDM();
        }

        private void autocomplUDM()
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

        private void cargarComboUDM()
        {
            conexion.Open();
            string sql = "select id, descripcion from unidadDeMedida";
            SqlCommand comando = new SqlCommand(sql, conexion);
            DataTable tabla1 = new DataTable();
            SqlDataAdapter adaptador1 = new SqlDataAdapter();
            adaptador1.SelectCommand = comando;
            adaptador1.Fill(tabla1);
            comboBox2.DataSource = tabla1;
            comboBox2.DisplayMember = "descripcion";
            comboBox2.ValueMember = "id";
            conexion.Close();
        }

        private void cargarDataTipo()
        {
            conexion.Open();
            string sql = " select m.descripcion, p.nombre from tipoMP as m join proveedor as p on m.idProveedor=p.id";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView2.Rows.Clear();
            while (registros.Read())
            {
                dataGridView2.Rows.Add(registros["descripcion"].ToString(), //numero pedido 0
                                       registros["nombre"].ToString()); //fecha tomado pedido 1
            }

            conexion.Close();
            dataGridView2.ClearSelection();
        }

        private void cargarComboProveedorTipo()
        {
            conexion.Open();
            string sql = "select id, nombre from proveedor";
            SqlCommand comando = new SqlCommand(sql, conexion);
            DataTable tabla1 = new DataTable();
            SqlDataAdapter adaptador1 = new SqlDataAdapter();
            adaptador1.SelectCommand = comando;
            adaptador1.Fill(tabla1);
            comboBox1.DataSource = tabla1;
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "id";
            conexion.Close();
        }

        private void cargarDataMarcas()
        {
            conexion.Open();
            string sql = " select m.descripcion, p.nombre from marcaMP as m join proveedor as p on m.idProveedor=p.id";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (registros.Read())
            {
                dataGridView1.Rows.Add(registros["descripcion"].ToString(), //numero pedido 0
                                       registros["nombre"].ToString()); //fecha tomado pedido 1
            }

            conexion.Close();
            dataGridView1.ClearSelection();
        }

        private void cargarComboProveedorMarca()
        {
            conexion.Open();
            string sql = "select id, nombre from proveedor";
            SqlCommand comando = new SqlCommand(sql, conexion);
            DataTable tabla1 = new DataTable();
            SqlDataAdapter adaptador1 = new SqlDataAdapter();
            adaptador1.SelectCommand = comando;
            adaptador1.Fill(tabla1);
            comboBox7.DataSource = tabla1;
            comboBox7.DisplayMember = "nombre";
            comboBox7.ValueMember = "id";
            conexion.Close();
        }




        private bool existeMarca() {

            bool existe = false;
            if (textBox1.Text == "")
            {
                MessageBox.Show("El nombre de la marca no puede estar vacío", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                conexion.Open();
                string sql = "select * from marcaMP where descripcion=@descripcion";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = textBox1.Text;
                SqlDataReader registro = comando.ExecuteReader();
                if (registro.Read())
                    existe = true;
                conexion.Close();
            }
            return existe;
        }

        private bool existeTipo() {
            bool existe = false;
            if (textBox2.Text == "")
            {
                MessageBox.Show("El nombre del tipo no puede estar vacío", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                conexion.Open();
                string sql = "select * from tipoMP where descripcion=@descripcion";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = textBox2.Text;
                SqlDataReader registro = comando.ExecuteReader();
                if (registro.Read())
                    existe = true;
                conexion.Close();
            }
            return existe;
        }

        private bool existeUnidadDeMedida() {
            bool existe = false;
            if (textBox3.Text == "")
            {
                MessageBox.Show("La descripción de la unidad de medida no puede estar vacía", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                conexion.Open();
                string sql = "select * from unidadDeMedida where descripcion=@descripcion";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = textBox3.Text;
                SqlDataReader registro = comando.ExecuteReader();
                if (registro.Read())
                    existe = true;
                conexion.Close();
            }
            return existe;
        }

        //registrar marca
        private void button1_Click(object sender, EventArgs e)
        {
            if (!existeMarca())
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Completar el nombre de marca", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                conexion.Open();

                string sql = "insert into marcaMP (descripcion, idProveedor) values (@descripcion, @idProveedor)";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = textBox1.Text;
                comando.Parameters.Add("@idProveedor", SqlDbType.Int).Value = comboBox7.SelectedValue;
                comando.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Se cargó la marca", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                blanquearFormMarca();
                cargarDataMarcas();
            }
            else
            {
                MessageBox.Show("Ya existe esa marca", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                blanquearFormMarca();
            }

        }



        private void blanquearFormMarca()
        {
            textBox1.Text = "";
            comboBox7.SelectedIndex = 1;
        }

        private void blanquearFormTipo() {
            textBox2.Text = "";
            comboBox1.SelectedIndex = 1;
        }



        //registrar tipo
        private void button3_Click(object sender, EventArgs e)
        {
            if (!existeTipo())
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Completar el nombre de tipo de material", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                conexion.Open();

                string sql = "insert into tipoMP (descripcion, idProveedor) values (@descripcion, @idProveedor)";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = textBox2.Text;
                comando.Parameters.Add("@idProveedor", SqlDbType.Int).Value = comboBox1.SelectedValue;
                comando.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Se cargó el tipo de materia prima", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                blanquearFormTipo();
                cargarDataTipo();
            }
            else
            {
                MessageBox.Show("Ya existe ese tipo de materia prima", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                blanquearFormTipo();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //REGISTRAR UNIDADES DE MEDIDA
        private void button4_Click(object sender, EventArgs e)
        {
            if (!existeUnidadDeMedida())
            {
                if (textBox3.Text == "")
                {
                    MessageBox.Show("Completar la descripción de la unidad de medida", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                conexion.Open();
                string sql = "insert into unidadDeMedida (descripcion) values (@descripcion)";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = textBox3.Text;
                comando.ExecuteNonQuery();
                conexion.Close();

                conexion.Open();
                string sql2 = "insert into SubUnidadDeMedida (descripcion) values (@descripcion)";
                SqlCommand comandox = new SqlCommand(sql2, conexion);
                comandox.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = textBox3.Text;
                comandox.ExecuteNonQuery();
                conexion.Close();

                MessageBox.Show("Se cargó la Unidad De Medida", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox3.Text = "";
                cargarComboUDM();
            }
            else
            {
                MessageBox.Show("Ya existe esa Unidad de Medida", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Text = "";
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void ConfigDeposito_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
        }
    }
}

