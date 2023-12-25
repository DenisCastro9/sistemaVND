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
    public partial class consultarMateriaPrima : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public consultarMateriaPrima(string area)
        {
            InitializeComponent();
            txtBusqueda.TextChanged += TxtBusqueda_TextChanged;
            actualizarPantalla();

            if (area.Equals("Fabricacion"))
            {
                button1.Enabled = false;
            }
        }

        private void numeroRegistros()
        {
            int numeroRegistros = dataGridView1.RowCount;
            lblRes.Text = numeroRegistros.ToString();
        }

        //PANTALLA CARGADA
        public void actualizarPantalla()
        {
            conexion.Open();
            string sql = " select mp.codigo as Código, mp.cantidad as Cantidad, mp.descripcion as Descripción, m.descripcion as Marca, t.descripcion as 'Tipo de material', mp.stockMinimo from materiaPrima as mp join marcaMP as m on m.id = mp.idMarcaMP join tipoMP as t on t.id = mp.idtipoMP";
            SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla;
            dataGridView1.Columns["stockMinimo"].Visible = false;
            conexion.Close();
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
        //---------------------------------ESTE BSUCADOR SI FUNCIONA POR DESCRIPCION Y CODIGO---------------------------------
        private void BuscarProveedores(string busqueda)
        {
            string consulta = "select mp.codigo as Código, mp.cantidad as Cantidad, mp.descripcion as Descripción, m.descripcion as Marca, t.descripcion as 'Tipo de material', mp.stockMinimo from materiaPrima as mp join marcaMP as m on m.id = mp.idMarcaMP join tipoMP as t on t.id = mp.idtipoMP WHERE mp.descripcion LIKE @busqueda OR mp.codigo LIKE @busqueda";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla;
            dataGridView1.Columns["stockMinimo"].Visible = false;
            dataGridView1.ClearSelection();
            numeroRegistros();
        }
        private void TxtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string busqueda = txtBusqueda.Text.Trim();
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                actualizarPantalla();
            }
            else
            {
                BuscarProveedores(busqueda);
            }
        }


        //FORMATO DE LA CELDA PARA QUE NOS MUESTRE MP DEBAJO DEL STOCK MINIMO
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Cantidad"].Index)
            {
                int cantidad = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Cantidad"].Value);
                int stockMinimo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["stockMinimo"].Value);

                if (cantidad < stockMinimo)
                {
                    e.CellStyle.BackColor = Color.Red;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "Cantidad disponible menor al stock minimo registrado";
                }
            }
        }





        //BOTON CERRAR
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }





        //BOTON VER DETALLE 
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string codigo ="" ;
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    codigo = row.Cells[0].Value.ToString(); 
                }
                FichaMateriaPrima f = new FichaMateriaPrima(this, codigo);
                f.Show(this);
                actualizarPantalla();
            }
            else
            {
                MessageBox.Show("Seleccionar una materia prima", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        //BOTON IR A REGISTRO
        private void button1_Click(object sender, EventArgs e)
        {
            registrarMateriaPrima rmp = new registrarMateriaPrima(this);
            rmp.Show(this);
            actualizarPantalla();
        }

        private void consultarMateriaPrima_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            numeroRegistros();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
