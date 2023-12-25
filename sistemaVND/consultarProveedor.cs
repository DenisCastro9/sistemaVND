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
    public partial class consultarProveedor : Form
    {
        public consultarProveedor()
        {
            InitializeComponent();
        }

        private void numeroRegistros()
        {
            int numeroRegistros = dataGridView1.RowCount;
            lblRes.Text = numeroRegistros.ToString();
        }

        SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        SqlDataAdapter adaptador;


        private void consultarProveedor_Load(object sender, EventArgs e)
        {
            cargarDataGrid();
            numeroRegistros();
        }


        private void cargarDataGrid()
        {
            adaptador = new SqlDataAdapter();
            SqlCommand datos = new SqlCommand("SELECT proveedor.nombre, proveedor.cuit, proveedor.mail, proveedor.telefono, proveedor.condicionIva, domicilio.calle, domicilio.numero, domicilio.nombreBarrio, localidad.nombreLocalidad, localidad.codigoPostal, provincia.nombreProvincia FROM proveedor INNER JOIN domicilio ON proveedor.idDomicilio = domicilio.numero" +
                "                                                                                                                                                                                                                                                                                          INNER JOIN localidad ON domicilio.idLocalidad = localidad.idLocalidad" +
                "                                                                                                                                                                                                                                                                                          INNER JOIN provincia ON domicilio.idProvincia = provincia.idprovincia", conexion);
            adaptador.SelectCommand = datos;
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Nombre";
            dataGridView1.Columns[1].HeaderText = "Cuit";
            dataGridView1.Columns[2].HeaderText = "Email";
            dataGridView1.Columns[3].HeaderText = "Teléfono";
            dataGridView1.Columns[4].HeaderText = "Condición Frente al IVA";
            dataGridView1.Columns[5].HeaderText = "Calle";
            dataGridView1.Columns[6].HeaderText = "Número de Altura";
            dataGridView1.Columns[7].HeaderText = "Barrio";
            dataGridView1.Columns[8].HeaderText = "Localidad";
            dataGridView1.Columns[9].HeaderText = "Código Postal";
            dataGridView1.Columns[10].HeaderText = "Provincia";
            dataGridView1.ClearSelection();
            numeroRegistros();
        }


        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            conexion.Open();
            SqlCommand buscar = conexion.CreateCommand();
            buscar.CommandType = CommandType.Text;
            buscar.CommandText = "SELECT proveedor.nombre, proveedor.cuit, proveedor.mail, proveedor.telefono, proveedor.condicionIva, domicilio.calle, domicilio.numero, domicilio.nombreBarrio, localidad.nombreLocalidad, localidad.codigoPostal, provincia.nombreProvincia FROM proveedor INNER JOIN domicilio ON proveedor.idDomicilio = domicilio.numero" +
                "                                                                                                                                                                                                                                                                             INNER JOIN localidad ON domicilio.idLocalidad = localidad.idLocalidad" +
                "                                                                                                                                                                                                                                                                             INNER JOIN provincia ON localidad.idProvincia = provincia.idprovincia WHERE proveedor.nombre LIKE ('" + textBox1.Text + "%')";
            buscar.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(buscar);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conexion.Close();
            dataGridView1.ClearSelection();
            numeroRegistros();
        }

     

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
