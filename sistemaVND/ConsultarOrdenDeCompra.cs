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
    public partial class ConsultarOrdenDeCompra : Form
    {
        
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public ConsultarOrdenDeCompra()
        {
            InitializeComponent();
            cargarComboOrdenesRecibidas();

        }

        private void numeroRegistros()
        {
            int numeroRegistros = dataGridView1.RowCount;
            lblRes1.Text = numeroRegistros.ToString();
            int numeroRegistros2 = dataGridView2.RowCount;
            lblRes2.Text = numeroRegistros2.ToString();
        }

        //DATAGRID DE LAS ORDENES RECIBIDAS:
        private void cargarComboOrdenesRecibidas()
        {
            conexion.Open();
            string sql = " select o.numero , o.fecha, p.nombre as proveedor, o.importeTotal, o.fechaDeIngreso from ordenDeCompra as o join proveedor as p on o.idProveedor = p.id where o.idEstadoOrdenC = 4 order by numero asc";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            dataGridView2.Rows.Clear();
            while (registros.Read())
            {
                string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                string fechaReciboFormateada = DateTime.Parse(registros["fechaDeIngreso"].ToString()).ToString("dd/MM/yyyy");
               
                dataGridView2.Rows.Add(registros["numero"].ToString(),
                                       fechaFormateada,
                                       registros["proveedor"].ToString(),
                                       registros["importeTotal"].ToString(),
                                       fechaReciboFormateada);
            }
            conexion.Close();
            dataGridView2.ClearSelection();
            numeroRegistros();
        }



        //BOTON REGISTRAR ORDEN DE COMPRA
        private void button1_Click(object sender, EventArgs e)
        {
            RegistrarOrdenDeCompra registroForm = new RegistrarOrdenDeCompra();
            registroForm.FormClosed += registroForm_FormClosed;
            registroForm.Show();
        }


        private void registroForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                conexion.Open();
                string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC=1";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["importeTotal"].ToString());
                }

                conexion.Close();
            }
            if (radioButton4.Checked == true)
            {
                conexion.Open();
                string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC!=4";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["importeTotal"].ToString());
                }

                conexion.Close();
            }
            if (radioButton2.Checked == true)
            {
                conexion.Open();
                string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC=2";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["importeTotal"].ToString());
                }

                conexion.Close();
            }
            if (radioButton3.Checked == true)
            {
                conexion.Open();
                string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC=3";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["importeTotal"].ToString());
                }

                conexion.Close();
            }
            dataGridView1.ClearSelection();
            numeroRegistros();
        }



        //BOTON VER DETALLE
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int numeroOrden = 0;
                string estado = "";

                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    estado= row.Cells[0].Value.ToString();
                    numeroOrden = Convert.ToInt32(row.Cells[1].Value.ToString());
                }
                DetalleOrdenDeCompra detalleForm = new DetalleOrdenDeCompra(numeroOrden, estado);
                detalleForm.FormClosed += detalleForm_FormClosed;
                detalleForm.Show();

                string mail = "virgivega46@gmail.com";              
                enviarMailOrdenDeCompra envioForm = new enviarMailOrdenDeCompra(numeroOrden, mail);
                envioForm.FormClosed += envioForm_FormClosed;
            }
            else
            {
                MessageBox.Show("Seleccionar una órden de compra", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void envioForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                conexion.Open();
                string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC=1";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["importeTotal"].ToString());
                }

                conexion.Close();
            }
            if (radioButton4.Checked == true)
            {
                conexion.Open();
                string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC!=4";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["importeTotal"].ToString());
                }

                conexion.Close();
            }
            if (radioButton2.Checked == true)
            {
                conexion.Open();
                string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC=2";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["importeTotal"].ToString());
                }

                conexion.Close();
            }
            if (radioButton3.Checked == true)
            {
                conexion.Open();
                string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC=3";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["importeTotal"].ToString());
                }

                conexion.Close();
            }
            dataGridView1.ClearSelection();
            cargarComboOrdenesRecibidas();
            numeroRegistros();
        }

        private void detalleForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                conexion.Open();
                string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC=1";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["importeTotal"].ToString());
                }

                conexion.Close();
            }
            if (radioButton4.Checked == true)
            {
                conexion.Open();
                string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC!=4";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["importeTotal"].ToString());
                }

                conexion.Close();
            }
            if (radioButton2.Checked == true)
            {
                conexion.Open();
                string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC=2";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["importeTotal"].ToString());
                }

                conexion.Close();
            }
            if (radioButton3.Checked == true)
            {
                conexion.Open();
                string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC=3";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["importeTotal"].ToString());
                }

                conexion.Close();
            }
            dataGridView1.ClearSelection();
            cargarComboOrdenesRecibidas();
            numeroRegistros();
        }




        //BUSCADOR 1
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (textBox1.Text != "")
            {
                dataGridView1.CurrentCell = null;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    r.Visible = false;
                }
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if ((c.Value.ToString().ToUpper()).IndexOf(textBox1.Text.ToUpper()) == 0)
                        {
                            r.Visible = true;
                            break;
                        }
                    }
                }
            }
            else {
                if (radioButton1.Checked == true)
                {
                    conexion.Open();
                    string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC=1";
                    SqlCommand comando = new SqlCommand(sql, conexion);
                    SqlDataReader registros = comando.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    while (registros.Read())
                    {
                        string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                        dataGridView1.Rows.Add(registros["estado"].ToString(),
                                               registros["numero"].ToString(),
                                               fechaFormateada,
                                               registros["proveedor"].ToString(),
                                               registros["importeTotal"].ToString());
                    }

                    conexion.Close();
                }
                if (radioButton4.Checked == true)
                {
                    conexion.Open();
                    string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC!=4";
                    SqlCommand comando = new SqlCommand(sql, conexion);
                    SqlDataReader registros = comando.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    while (registros.Read())
                    {
                        string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                        dataGridView1.Rows.Add(registros["estado"].ToString(),
                                               registros["numero"].ToString(),
                                               fechaFormateada,
                                               registros["proveedor"].ToString(),
                                               registros["importeTotal"].ToString());
                    }

                    conexion.Close();
                }
                if (radioButton2.Checked == true)
                {
                    conexion.Open();
                    string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC=2";
                    SqlCommand comando = new SqlCommand(sql, conexion);
                    SqlDataReader registros = comando.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    while (registros.Read())
                    {
                        string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                        dataGridView1.Rows.Add(registros["estado"].ToString(),
                                               registros["numero"].ToString(),
                                               fechaFormateada,
                                               registros["proveedor"].ToString(),
                                               registros["importeTotal"].ToString());
                    }

                    conexion.Close();
                }
                if (radioButton3.Checked == true)
                {
                    conexion.Open();
                    string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC=3";
                    SqlCommand comando = new SqlCommand(sql, conexion);
                    SqlDataReader registros = comando.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    while (registros.Read())
                    {
                        string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                        dataGridView1.Rows.Add(registros["estado"].ToString(),
                                               registros["numero"].ToString(),
                                               fechaFormateada,
                                               registros["proveedor"].ToString(),
                                               registros["importeTotal"].ToString());
                    }

                    conexion.Close();
                }
                dataGridView1.ClearSelection();
            }
            dataGridView1.ClearSelection();
            numeroRegistros();
        }



        //BUSCADOR 2
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                dataGridView2.CurrentCell = null;
                foreach (DataGridViewRow r in dataGridView2.Rows)
                {
                    r.Visible = false;
                }
                foreach (DataGridViewRow r in dataGridView2.Rows)
                {
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if ((c.Value.ToString().ToUpper()).IndexOf(textBox2.Text.ToUpper()) == 0)
                        {
                            r.Visible = true;
                            break;
                        }
                    }
                }
            }
            else{
                cargarComboOrdenesRecibidas();

            }
            dataGridView2.ClearSelection();
            numeroRegistros();
        }





        //CHECKEADO EN REGISTRADAS
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {            
            if (radioButton1.Checked == true)
            {
                conexion.Open();
                string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC=1";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();                
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["importeTotal"].ToString());
                }
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            numeroRegistros();
        }
        //CHECKEADO EN TODAS (MENOS LAS RECIBIDAS)
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {           
            
            if (radioButton4.Checked == true)
            {
                conexion.Open();
                string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC!=4";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["importeTotal"].ToString());
                }                
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            numeroRegistros();
        }
        //CHECKEADO EN ENVIADAS
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {                
            if (radioButton2.Checked == true)
            {
                conexion.Open();
                string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC=2";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["importeTotal"].ToString());
                }
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            numeroRegistros();
        }
        //CHECKEADO EN ANULADAS
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {         
            if (radioButton3.Checked == true)
            {
                conexion.Open();
                string sql = " select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC=3";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
               dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["importeTotal"].ToString());
                }
                conexion.Close();
                dataGridView1.ClearSelection();
            }
            numeroRegistros();
        }
        //CUANDO CARGA LA PAGINA APARECE POR DEFECTO SELECCIONADO EL CHECKBOX TODAS
        private void ConsultarOrdenDeCompra_Load(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                conexion.Open();
                string sql = "select e.descripcion as estado, o.numero, o.fecha, p.nombre as proveedor, o.importeTotal from ordenDeCompra as o join estadoOrdenC as e on o.idEstadoOrdenC=e.id join proveedor as p on o.idProveedor=p.id where o.idEstadoOrdenC!=4";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registros = comando.ExecuteReader();
               dataGridView1.Rows.Clear();
                while (registros.Read())
                {
                    string fechaFormateada = DateTime.Parse(registros["fecha"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows.Add(registros["estado"].ToString(),
                                           registros["numero"].ToString(),
                                           fechaFormateada,
                                           registros["proveedor"].ToString(),
                                           registros["importeTotal"].ToString());
                }

                conexion.Close();
                dataGridView1.ClearSelection();
                dataGridView2.ClearSelection();
            }
            numeroRegistros();
        }

       









        //BOTON VER DETALLE DE ORDEN RECIBIDA
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int numeroOrden = 0;

                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    numeroOrden = Convert.ToInt32(row.Cells[0].Value.ToString());

                }
                DetalleOrdenCRecibida dc = new DetalleOrdenCRecibida(numeroOrden);
                dc.Show(this);
            }
            else
            {
                MessageBox.Show("Seleccionar una órden de compra", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

  

        //BOTON IR A INFORMES
        private void button6_Click(object sender, EventArgs e)
        {
            InformesOrdenDeCompra io = new InformesOrdenDeCompra();
            io.Show(this);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["importeTotal"].Index && e.RowIndex >= 0)
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal importe))
                {
                    // Formatear el valor agregando el signo "$" y el formato de moneda
                    e.Value = string.Format("{0:C}", importe);
                    e.FormattingApplied = true;
                }
            }
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView2.Columns["importeTotalOR"].Index && e.RowIndex >= 0)
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal importe))
                {
                    // Formatear el valor agregando el signo "$" y el formato de moneda
                    e.Value = string.Format("{0:C}", importe);
                    e.FormattingApplied = true;
                }
            }
        }
    }
}
