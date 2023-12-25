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
    public partial class FichaMateriaPrima : Form
    {
        private consultarMateriaPrima formPrincipal;
        private string codigoMP;
        int cantidadMP = 0;
        int reservadoMP = 0;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public FichaMateriaPrima(consultarMateriaPrima form, string codigo)
        {
            InitializeComponent();
            formPrincipal = form;
            textBoxstock.Enabled = false;
            textBoxCod.Enabled = false;
            textBoxCod.Text = codigo;
            this.codigoMP = codigo;
            
            cargarComboUDM();
            cargarComboSubUDM();
            cargarComboTipo();
            cargarComboMarca();
            textBoxCod.Enabled = false;
            cargarFicha();
        }

        
        private void cargarFicha()
        {
            conexion.Open();
            string sql = "select mp.descripcion, un.descripcion as Unidad, mp.cantidadQueContiene, subun.descripcion as subUnidad, m.descripcion as marca, t.descripcion as tipo, mp.stockMinimo, mp.cantidad, mp.reservado from materiaPrima as mp  join marcaMp as m on mp.idMarcaMP = m.id     join tipoMP as t on mp.idtipoMP = t.id  JOIN unidadDeMedida as un on mp.IdUnidadDeMedida = un.id  join SubUnidadDeMedida as subun on mp.IdSubUnidadDeMedidda = subun.id where mp.codigo =@codigo ";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@codigo", codigoMP);


            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                //VER COMO MOSTRAR LOS COMBObOX-----------------------------------------------------------------------------------------------------------------------------------
                textBoxNom.Text = registro["descripcion"].ToString();
                comboBox2.Text = registro["Unidad"].ToString();
                textBox1.Text = registro["cantidadQueContiene"].ToString();
                comboBox3.Text = registro["subUnidad"].ToString();
                
                textBoxstock.Text = registro["stockMinimo"].ToString();
                cantidadMP= Convert.ToInt32(registro["cantidad"].ToString());
                textBoxCant.Text = Convert.ToString(cantidadMP);
                reservadoMP = Convert.ToInt32(registro["reservado"].ToString());
                textBox2.Text =Convert.ToString(reservadoMP);

                comboBox1.Text = registro["marca"].ToString();
                comboBoxTipo.Text = registro["tipo"].ToString();

            }
            else
            {
                MessageBox.Show("Este código no tiene material asignado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            conexion.Close();
        }



        private void cargarComboMarca()
        {
            conexion.Open();
            string sql = "select id, descripcion from marcaMP";
            SqlCommand comando = new SqlCommand(sql, conexion);
            DataTable tabla1 = new DataTable();
            SqlDataAdapter adaptador1 = new SqlDataAdapter();
            adaptador1.SelectCommand = comando;
            adaptador1.Fill(tabla1);
            comboBox1.DataSource = tabla1;
            comboBox1.DisplayMember = "descripcion";
            comboBox1.ValueMember = "id";
            conexion.Close();
        }

        private void cargarComboTipo()
        {
            conexion.Open();
            string sql = "select id, descripcion from tipoMP";
            SqlCommand comando = new SqlCommand(sql, conexion);
            DataTable tabla1 = new DataTable();
            SqlDataAdapter adaptador1 = new SqlDataAdapter();
            adaptador1.SelectCommand = comando;
            adaptador1.Fill(tabla1);
            comboBoxTipo.DataSource = tabla1;
            comboBoxTipo.DisplayMember = "descripcion";
            comboBoxTipo.ValueMember = "id";
            conexion.Close();
        }

        private void cargarComboSubUDM()
        {
            conexion.Open();
            string sql = "select id, descripcion from SubUnidadDeMedida";
            SqlCommand comando = new SqlCommand(sql, conexion);
            DataTable tabla1 = new DataTable();
            SqlDataAdapter adaptador1 = new SqlDataAdapter();
            adaptador1.SelectCommand = comando;
            adaptador1.Fill(tabla1);
            comboBox3.DataSource = tabla1;
            comboBox3.DisplayMember = "descripcion";
            comboBox3.ValueMember = "id";
            conexion.Close();
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


    


        //BOTON MODIFICAR..
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxCod.Text == "" || textBoxNom.Text == "")
            {
                MessageBox.Show("El código y descripción no pueden estar vacíos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int reservadoParaDarDeBaja = Convert.ToInt32(textBox3.Text);
            //NO SE DEBE DAR DE BAJA MAS DE LO QUE HAY EN RESERVA
            if (reservadoMP < reservadoParaDarDeBaja)
            {
                MessageBox.Show("No se puede dar de baja más material del que existe en reserva", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //ACA SE HACE EL GUARDADO
                conexion.Open();
                string sql = "update materiaPrima set descripcion=@descripcion, cantidad=@cantidad, IdUnidadDeMedida=@IdUnidadDeMedida, cantidadQueContiene = @cantidadQueContiene ,IdSubUnidadDeMedidda = @IdSubUnidadDeMedidda , idMarcaMP = @idMarcaMP, idtipoMP = @idtipoMP, reservado=@reservado  where codigo = @codigo";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@codigo", SqlDbType.VarChar).Value = codigoMP;
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = textBoxNom.Text;
                int totalCantidad = cantidadMP + Convert.ToInt32(textBox4.Text);
                comando.Parameters.Add("@cantidad", SqlDbType.Int).Value = totalCantidad;
                comando.Parameters.Add("@IdUnidadDeMedida", SqlDbType.Int).Value = comboBox2.SelectedValue;
                comando.Parameters.Add("@cantidadQueContiene", SqlDbType.Decimal).Value = Convert.ToDecimal(textBox1.Text);
                comando.Parameters.Add("@IdSubUnidadDeMedidda", SqlDbType.Int).Value = comboBox3.SelectedValue;
                comando.Parameters.Add("@idMarcaMP", SqlDbType.Int).Value = comboBox1.SelectedValue;
                comando.Parameters.Add("@idtipoMP", SqlDbType.Int).Value = comboBoxTipo.SelectedValue;
                int totalReservado = reservadoMP - Convert.ToInt32(textBox3.Text);
                comando.Parameters.Add("@reservado", SqlDbType.Int).Value = totalReservado;
                int cant = comando.ExecuteNonQuery();
                if (cant == 1)
                {
                    MessageBox.Show("Se modificaron los datos", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox3.Text = "0";
                    textBox4.Text = "0";

                }
                else
                {
                    MessageBox.Show("No existe esa materia prima", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                conexion.Close();
                cargarFicha();

            }


           
        }

        

        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            
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
            Dispose();
        }
    }
}
