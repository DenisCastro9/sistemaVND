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
    public partial class CantidadMPxArticulo : Form
    {
        string nombreART = "";
        string[] art;
        private SqlConnection conexion = new SqlConnection("data source=  DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public CantidadMPxArticulo(string nombre)
        {
            InitializeComponent();
            label1.Text = Convert.ToString(nombre);
            this.nombreART = nombre;

            cargarComboArea();

            cargarComboMP();
            autocomComboMP();

            cargarDatos();

            
        }


        private void cargarDatos()
        {
            conexion.Open();
            string sql = "select c.articulo, c.area, mp.codigo as CodigoMP, mp.descripcion as descripcionMP, c.cantidad, SUB.descripcion as subunidadDeMedida from cantidadMPxArticulo as c join materiaPrima as mp on c.idMateriaPrima = mp.codigo join SubUnidadDeMedida as sub on mp.IdSubUnidadDeMedidda = sub.id where c.articulo =@articulo ";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@articulo", nombreART);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView2.Rows.Clear();
            while (registros.Read())
            {
                
                dataGridView2.Rows.Add(registros["articulo"].ToString(),
                                       registros["area"].ToString(),
                                       registros["CodigoMP"].ToString(),                                       
                                       registros["descripcionMP"].ToString(),
                                       registros["cantidad"].ToString(),
                                       registros["subunidadDeMedida"].ToString()
                                       );
            }            
            conexion.Close();
            int totalItems = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                totalItems++;
            }
            label17.Text = Convert.ToString(totalItems);
        }







        private void cargarComboArea()
        {
            comboBox2.Items.Add("Cortado");
            comboBox2.Items.Add("Empaque");
            comboBox2.Items.Add("Ojalillado");
        }

        private void autocomComboMP()
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            SqlCommand cmd = new SqlCommand("SELECT descripcion FROM materiaPrima", conexion);
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
        private void cargarComboMP()
        {
            SqlCommand cm = new SqlCommand("select * from materiaPrima", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr.GetString(2)); //veo la descripcion de la MP
            }
            conexion.Close();
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand("select mp.codigo, sub.descripcion from materiaPrima as mp join SubUnidadDeMedida as sub on mp.IdSubUnidadDeMedidda=sub.id where mp.descripcion= '" + comboBox1.Text + "'", conexion);
            conexion.Open();
            SqlDataReader dr = cm.ExecuteReader();
            if (dr.Read() == true)
            {
                textBox5.Text = dr["codigo"].ToString();//codigo mp
                textBox7.Text = dr["descripcion"].ToString();//subunidad de medida
            }
            conexion.Close();
        }



        //EN ESTE CASO NOS GUIAMOS CON LOS NOMBRES DE LOS ARTICULOS, NO LOS CODIGOS        
        private bool existeRegistroMP()
        {
            bool existe = false;
            if (label1.Text == "")
            { }
            else
            {
                conexion.Open();
                string sql = "select * from cantidadMPxArticulo where articulo=@nombreART";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@nombreART", SqlDbType.VarChar).Value = nombreART;
                SqlDataReader registro = comando.ExecuteReader();
                if (registro.Read())
                    existe = true;
                conexion.Close();

            }
            return existe;
        }




        //BOTON GUARDAR CAMBIOS:
        private void button1_Click(object sender, EventArgs e)
        {
            if (!existeRegistroMP())
            {
            //NO TIENE DATOS ASIGNADOS SE REGISTRA

                if (dataGridView2.RowCount <= 0 || label17.Text == "0")                    
                {
                    MessageBox.Show("Agregar al menos un ítem", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else 
                {
                    art = new string[dataGridView2.Rows.Count];
                    string aux = "";
                    int y = 0;
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        aux = row.Cells[0].Value.ToString() + "-";  //articulo nombre
                        aux += row.Cells[1].Value.ToString() + "-"; //area
                        aux += row.Cells[2].Value.ToString() + "-";// codigoMP
                        aux += row.Cells[3].Value.ToString() + "-";//descripcionMP
                        aux += row.Cells[4].Value.ToString() + "-"; //cantidad
                        aux += row.Cells[5].Value.ToString() + "-"; //subUnidadM
                        art[y] = aux;
                        string cadena = art[y];
                        string[] valores = cadena.Split('-');

                        conexion.Open();
                        string sql = "insert into cantidadMPxArticulo (articulo, cantidad, area, idMateriaPrima) values (@articulo, @cantidad, @area, @idMateriaPrima)";
                        SqlCommand comando = new SqlCommand(sql, conexion);

                        comando.Parameters.AddWithValue("@articulo", nombreART);
                        comando.Parameters.AddWithValue("@cantidad", Convert.ToString(valores[4]));
                        comando.Parameters.AddWithValue("@area", Convert.ToString(valores[1]));
                        comando.Parameters.AddWithValue("@idMateriaPrima", Convert.ToInt32(valores[2]));
                        
                        int result2 = comando.ExecuteNonQuery();
                        conexion.Close();
                        aux = "";
                    }
                    MessageBox.Show("Datos registrados", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //dataGridView2.Rows.Clear();
                    //label17.Text = "0";
                }

            }
            else
            {
                //SI EL ARTICULO TIENE DATOS ASIGNADOS PREVIAMENTE SE MODIFICA
                if (dataGridView2.RowCount <= 0 || label17.Text == "0")
                {
                    MessageBox.Show("Agregar al menos un ítem", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    
                    //PRIMERO BORRAR LOS DATOS
                    conexion.Open();
                    string sqlDelete = "delete from cantidadMPxArticulo WHERE articulo=@articulo";
                    SqlCommand comandoDele = new SqlCommand(sqlDelete, conexion);
                    comandoDele.Parameters.AddWithValue("@articulo", nombreART);
                    int result = comandoDele.ExecuteNonQuery();
                    conexion.Close();
                    


                    //DESPUES LOS REGISTRO DE NUEVO:
                    art = new string[dataGridView2.Rows.Count];
                    string aux = "";
                    int y = 0;
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        aux = row.Cells[0].Value.ToString() + "-";  //articulo nombre
                        aux += row.Cells[1].Value.ToString() + "-"; //area
                        aux += row.Cells[2].Value.ToString() + "-";// codigoMP
                        aux += row.Cells[3].Value.ToString() + "-";//descripcionMP
                        aux += row.Cells[4].Value.ToString() + "-"; //cantidad
                        aux += row.Cells[5].Value.ToString() + "-"; //subUnidadM
                        art[y] = aux;
                        string cadena = art[y];
                        string[] valores = cadena.Split('-');

                        conexion.Open();
                        string sql = "insert into cantidadMPxArticulo (articulo, cantidad, area, idMateriaPrima) values (@articulo, @cantidad, @area, @idMateriaPrima)";
                        SqlCommand comando = new SqlCommand(sql, conexion);
                        comando.Parameters.AddWithValue("@articulo", nombreART);
                        comando.Parameters.AddWithValue("@cantidad", Convert.ToString(valores[4]));
                        comando.Parameters.AddWithValue("@area", Convert.ToString(valores[1]));
                        comando.Parameters.AddWithValue("@idMateriaPrima", Convert.ToInt32(valores[2]));
                        int result2 = comando.ExecuteNonQuery();
                        conexion.Close();
                        aux = "";
                    }
                    MessageBox.Show("Datos guardados", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //dataGridView2.Rows.Clear();
                    //label17.Text = "0";
                }

            }
        }









        //BOTON AGREGAR ITEM
        private void button3_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex.Equals(-1) || textBox6.Text == "" || comboBox2.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Ingresar datos para agregar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //VERIFICAR QUE NO SE HAYA CARGADO ESA MP A LA TABLA
                string codigo = textBox5.Text;
                bool codigoExistente = false;

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {                    
                    string ObtenerElCodigo = row.Cells[2].Value.ToString();

                    if (ObtenerElCodigo == codigo)
                    {
                        MessageBox.Show("NO se puede cargar dos veces un material", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        codigoExistente = true;
                        break;
                    }
                }

                if (!codigoExistente)
                {
                    dataGridView2.Rows.Add(label1.Text, comboBox2.Text, codigo, comboBox1.Text, textBox6.Text, textBox7.Text);
                }

                
                int totalItems = 0;
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    totalItems++;
                }
                label17.Text = Convert.ToString(totalItems);

                textBox6.Text = "";
                textBox7.Text = "";
                textBox5.Text = "";
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;

            }
        }


        //BOTON ELIMINAR ITEM
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {                
                DataGridViewRow filaSeleccionada = dataGridView2.SelectedRows[0];                
                dataGridView2.Rows.Remove(filaSeleccionada);
            }
            else
            {
                MessageBox.Show("Seleccionar un ítem", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            int totalItems = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                totalItems++;
            }
            label17.Text = Convert.ToString(totalItems);
        }


        //BOTON VER MATERIA PRIMA EMPLEADA
        private void button5_Click(object sender, EventArgs e)
        {
            string nombre = label1.Text;
            MateriaPrimaPorArticulo f = new MateriaPrimaPorArticulo(nombre);
            f.ShowDialog();
        }



        //BOTON SALIR
        private void button6_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        private void CantidadMPxArticulo_Load(object sender, EventArgs e)
        {
            dataGridView2.ClearSelection();
        }
    }
}
