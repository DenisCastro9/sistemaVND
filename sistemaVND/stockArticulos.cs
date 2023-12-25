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
    public partial class stockArticulos : Form
    {
        int codigo = 0;
        int cantidadArt = 0;
        int reservadoArt = 0;
        private VerStockArticulos formPrincipal;
        SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        
        public stockArticulos(VerStockArticulos form, int codigo)
        {
            this.codigo = codigo;
            InitializeComponent();
            formPrincipal = form;
            cargarFicha();
        }


        private void cargarFicha()
        {
            conexion.Open();
            string sql = "select nombre, talle, cantidadEnStock, reservado from articulo where codigoArticulo=@codigoArticulo ";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@codigoArticulo", codigo);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {                
                label10.Text = registro["nombre"].ToString() +" - Talle "+ registro["talle"].ToString();

                cantidadArt= Convert.ToInt32(registro["cantidadEnStock"].ToString());
                textBox1.Text = Convert.ToString(cantidadArt);

                reservadoArt = Convert.ToInt32(registro["reservado"].ToString());
                textBox3.Text = Convert.ToString(reservadoArt);
            }
            else
            {
                MessageBox.Show("Este artículo no tiene stock permitido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            conexion.Close();
        }





        //BOTON SALIR
        private void button6_Click(object sender, EventArgs e)
        {
        }



        //BOTON GUARDAR
        private void button1_Click(object sender, EventArgs e)
        {
            int reservadoParaDarDeBaja = Convert.ToInt32(textBox4.Text);
            //NO SE DEBE DAR DE BAJA MAS DE LO QUE HAY EN RESERVA
            if (reservadoArt < reservadoParaDarDeBaja)
            {
                MessageBox.Show("No se pueden dar de baja más artículos de los que existen en reserva", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                conexion.Open();
                string sql = "update articulo set cantidadEnStock=@cantidadEnStock, reservado=@reservado where codigoArticulo=@codigoArticulo";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@codigoArticulo", SqlDbType.VarChar).Value = codigo;

                int totalCantidad = cantidadArt + Convert.ToInt32(textBox2.Text);
                comando.Parameters.Add("@cantidadEnStock", SqlDbType.Int).Value = totalCantidad;

                int totalReservado = reservadoArt - Convert.ToInt32(textBox4.Text);
                comando.Parameters.Add("@reservado", SqlDbType.Int).Value = totalReservado;

                int cant = comando.ExecuteNonQuery();
                if (cant == 1)
                {
                    MessageBox.Show("Se modificaron los datos", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.Text = "0";
                    textBox4.Text = "0";

                }
                else
                {
                    MessageBox.Show("No existe ese artículo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                conexion.Close();
                cargarFicha();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            formPrincipal.cargarDatos();
            Dispose();
        }
    }
}
