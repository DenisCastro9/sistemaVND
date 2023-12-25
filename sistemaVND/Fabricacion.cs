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
    public partial class Fabricacion : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        string area = "";
        public Fabricacion(string area)
        {
            InitializeComponent();
            this.area = area;

            dataGridView2.CellFormatting += dataGridView2_CellFormatting;
            dataGridView3.CellFormatting += dataGridView3_CellFormatting;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;


            cargarOrdenesFinalizadas();
            cargarOrdenesPorHacer();
            cargarOrdenesEnProceso();


            siCheckBox();

            //POR HACER
            dateTimePicker5.ValueChanged +=dateTimePicker5_ValueChanged;
            dateTimePicker6.ValueChanged += dateTimePicker6_ValueChanged;
            // Poner el DATETIME: DESDE con fecha 7 dias despues al dia de hoy:
            DateTime fechaActual5 = DateTime.Now;
            DateTime fechaPorDefecto5 = fechaActual5.AddDays(7);
            dateTimePicker5.Value = fechaPorDefecto5;
            // Poner el DATETIME: HASTA con fecha 7 dias despues a la fecha que tiene asigando el dateTime 5
            DateTime fechaPorDefecto6 = fechaPorDefecto5.AddDays(7);
            dateTimePicker6.Value = fechaPorDefecto6;

            //EN PROCESO
            dateTimePicker3.ValueChanged += dateTimePicker3_ValueChanged;
            dateTimePicker4.ValueChanged += dateTimePicker4_ValueChanged;
            //poner el DATETIME: DESDE con la fecha de hoy 
            DateTime fechaActual3 = DateTime.Now;
            dateTimePicker3.Value = fechaActual3;
            //poner el DATETIME: HASTA con fecha 7 dias despues a la fecha que tiene asignado el dateTime 3
            DateTime fechaPorDefecto4 = fechaActual3.AddDays(7);
            dateTimePicker4.Value = fechaPorDefecto4;


            //FINALIZADAS
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            dateTimePicker2.ValueChanged += dateTimePicker2_ValueChanged;
            // Poner el DATETIME: DESDE con fecha 7 dias anteriores al dia de hoy:
            DateTime fechaActual1 = DateTime.Now;
            DateTime fechaPorDefecto1 = fechaActual1.AddDays(-7);
            dateTimePicker1.Value = fechaPorDefecto1;
            // Poner el DATETIME: HASTA con fecha del dia de hoy
            DateTime fechaPorDefecto2 = DateTime.Now;
            dateTimePicker2.Value = fechaPorDefecto2;


        }

        public void siCheckBox()
        {
            if (checkBox3.Checked)
            {
                conexion.Open();
                string sql = "SELECT numero as 'Número orden', cliente as Cliente, fechaCreacion as 'Fecha prevista inicio' FROM ordenDeFabricacion WHERE idEstadoOrdenF=1";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView3.DataSource = tabla;
                dataGridView3.ClearSelection();
                conexion.Close();
            }
            if (checkBox1.Checked)
            {
                conexion.Open();
                string sql = "SELECT numero as 'Número orden', cliente as Cliente, fechaRealFin as 'Fecha finalizacion', idEstadoOrdenF FROM ordenDeFabricacion WHERE idEstadoOrdenF=5 or idEstadoOrdenF=4";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["idEstadoOrdenF"].Visible = false;
                dataGridView1.ClearSelection();
                conexion.Close();
            }
            if (checkBox2.Checked)
            {
                conexion.Open();
                string sql = "SELECT orf.numero as 'Número orden', orf.cliente as Cliente,  p.descripcion as 'Punto de control', orf.fechaPrevistaFin FROM ordenDeFabricacion as orf join puntoDeControl as p on orf.idPuntoDeControl=p.id WHERE idEstadoOrdenF=2";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView2.DataSource = tabla;
                dataGridView2.Columns["fechaPrevistaFin"].Visible = false;
                dataGridView2.ClearSelection();
                conexion.Close();
            }
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
        }






        //ORDENES FINALIZADAS/ANULADAS
        public void cargarOrdenesFinalizadas()
        {
            //aca se cargar las ordenes que estan finalizadas, la fecha deberia ser menor/PASADA a la de hoy!

            DateTime fechaInicio = dateTimePicker1.Value;
            DateTime fechaFin = dateTimePicker2.Value;
            conexion.Open();
            string sql = "SELECT numero as 'Número orden', cliente as Cliente, fechaRealFin as 'Fecha finalización', idEstadoOrdenF FROM ordenDeFabricacion WHERE fechaRealFin >=  @FechaInicio AND fechaRealFin <= @FechaFin  AND idEstadoOrdenF=5 or idEstadoOrdenF=4";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@FechaInicio", fechaInicio);
            comando.Parameters.AddWithValue("@FechaFin", fechaFin);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla;
            dataGridView1.Columns["idEstadoOrdenF"].Visible = false;
            dataGridView1.ClearSelection();
            conexion.Close();
            
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) 
        {
            //NECESITO QUE EN LAS ORDENES: idEstadoOrdenF=4, LA CELDA APAREZCA PINTADA EN ROJO Y CUANDO PASO EL CURSOR POR LA CELDA APAREZCA UN CARTEL FLOTANTE DICIENDO "Anulada"
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Número orden"].Index)
            {
                int estadoOrden = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idEstadoOrdenF"].Value);

                if (estadoOrden == 4)
                {
                    // Establecer el color de fondo de la celda en rojo
                    e.CellStyle.BackColor = Color.Red;

                    // Establecer el tooltip de la celda
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "Anulada";
                }
            }
        }
        private void buscadorFinalizado(string busqueda)
        {            
            string consulta = "SELECT numero as 'Número orden', cliente as Cliente, fechaRealFin as 'Fecha finalización', idEstadoOrdenF FROM ordenDeFabricacion WHERE numero LIKE @busqueda AND idEstadoOrdenF=5 or numero LIKE @busqueda AND idEstadoOrdenF=4 ";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla;
            dataGridView1.Columns["idEstadoOrdenF"].Visible = false;
            dataGridView1.ClearSelection();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string busqueda = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                if (checkBox1.Checked)
                {
                    conexion.Open();
                    string sql = "SELECT numero as 'Número orden', cliente as Cliente, fechaRealFin as 'Fecha finalización', idEstadoOrdenF FROM ordenDeFabricacion WHERE idEstadoOrdenF=5 or idEstadoOrdenF=4";
                    SqlCommand comando = new SqlCommand(sql, conexion);
                    SqlDataAdapter adaptador = new SqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView1.DataSource = tabla;
                    dataGridView1.Columns["idEstadoOrdenF"].Visible = false;
                    dataGridView1.ClearSelection();
                    conexion.Close();
                }
                else
                {
                    cargarOrdenesFinalizadas();
                }
            }
            else
            {
                buscadorFinalizado(busqueda);
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            cargarOrdenesFinalizadas();
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            cargarOrdenesFinalizadas();
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Verificar que e.RowIndex y e.ColumnIndex sean válidos.
            {
                string codigo = dataGridView1.Rows[e.RowIndex].Cells["Número orden"].Value.ToString();
                DetalleOrdenF form2 = new DetalleOrdenF(this, codigo);
                form2.ShowDialog(this);
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                conexion.Open();
                string sql = "SELECT numero as 'Número orden', cliente as Cliente, fechaRealFin as 'Fecha finalización', idEstadoOrdenF FROM ordenDeFabricacion WHERE idEstadoOrdenF=5 or idEstadoOrdenF=4";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["idEstadoOrdenF"].Visible = false;
                dataGridView1.ClearSelection();
                conexion.Close();
            }
        }





        //ORDENES EN PROCESO:
        public void cargarOrdenesEnProceso()
        {
            //aca se cargar las ordenes que estan en proceso, la fecha deberia ser igual a la de hoy!
            DateTime fechaInicio = dateTimePicker3.Value;
            DateTime fechaFin = dateTimePicker4.Value;
            conexion.Open();
            string sql = "SELECT orf.numero as 'Número orden', orf.cliente as Cliente,  p.descripcion as 'Punto de control', orf.fechaPrevistaFin FROM ordenDeFabricacion as orf join puntoDeControl as p on orf.idPuntoDeControl=p.id WHERE idEstadoOrdenF=2 AND fechaRealInicio >= @FechaInicio AND fechaPrevistaFin <= @FechaFin AND idEstadoOrdenF=2";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@FechaInicio", fechaInicio);
            comando.Parameters.AddWithValue("@FechaFin", fechaFin);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView2.DataSource = tabla;
            dataGridView2.Columns["fechaPrevistaFin"].Visible = false;
            dataGridView2.ClearSelection();
            conexion.Close();
            
        }        
        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //NECESITO QUE SI LA fechaPrevistaFin ES MENOR A LA FECHA DE HOY LA CELDA APAREZCA PINTADA EN ROJO Y CUANDO PASO EL CURSOR POR LA CELDA APAREZCA UN CARTEL FLOTANTE DICIENDO "La orden esta atrasada"
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView2.Columns["Número orden"].Index)//esta es la celda que se va a pintar
            {
                DateTime fechaPrevistaFin = (DateTime)dataGridView2.Rows[e.RowIndex].Cells["fechaPrevistaFin"].Value;

                if (fechaPrevistaFin < DateTime.Today)
                {
                    e.CellStyle.BackColor = Color.Red;
                    dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "La orden está atrasada";
                }
            }      
        } 
        private void buscadorEnProceso(string busqueda)
        {
            string consulta = "SELECT orf.numero as 'Número orden', orf.cliente as Cliente,  p.descripcion as 'Punto de control', orf.fechaPrevistaFin FROM ordenDeFabricacion as orf join puntoDeControl as p on orf.idPuntoDeControl=p.id WHERE idEstadoOrdenF=2 AND numero LIKE @busqueda AND idEstadoOrdenF=2";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView2.DataSource = tabla;
            dataGridView2.Columns["fechaPrevistaFin"].Visible = false;
            dataGridView2.ClearSelection();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string busqueda = textBox2.Text.Trim();
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                if (checkBox2.Checked)
                {
                    conexion.Open();
                    string sql = "SELECT orf.numero as 'Número orden', orf.cliente as Cliente,  p.descripcion as 'Punto de control', orf.fechaPrevistaFin FROM ordenDeFabricacion as orf join puntoDeControl as p on orf.idPuntoDeControl=p.id WHERE idEstadoOrdenF=2";
                    SqlCommand comando = new SqlCommand(sql, conexion);
                    SqlDataAdapter adaptador = new SqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView2.DataSource = tabla;
                    dataGridView2.Columns["fechaPrevistaFin"].Visible = false;
                    dataGridView2.ClearSelection();
                    conexion.Close();
                }
                else
                {
                    cargarOrdenesEnProceso();
                }
            }
            else
            {
                buscadorEnProceso(busqueda);
            }
        }
        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            cargarOrdenesEnProceso();
        }
        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            cargarOrdenesEnProceso();
        }
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Verificar que e.RowIndex y e.ColumnIndex sean válidos.
            {
                string codigo = dataGridView2.Rows[e.RowIndex].Cells["Número orden"].Value.ToString();
                DetalleOrdenF form2 = new DetalleOrdenF(this, codigo);
                form2.ShowDialog(this);
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                conexion.Open();
                string sql = "SELECT orf.numero as 'Número orden', orf.cliente as Cliente,  p.descripcion as 'Punto de control', orf.fechaPrevistaFin FROM ordenDeFabricacion as orf join puntoDeControl as p on orf.idPuntoDeControl=p.id WHERE idEstadoOrdenF=2";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView2.DataSource = tabla;
                dataGridView2.Columns["fechaPrevistaFin"].Visible = false;
                dataGridView2.ClearSelection();
                conexion.Close();
            }
        }







        //ORDENES POR HACER:
        public void cargarOrdenesPorHacer()
        {
            //aca se cargar las ordenes que estan registradas, la fecha deberia ser mayor/FUTURA a la de hoy!
            //FORMTATO DE LAS FECHAS: fechaCreacion >='2023-06-06' AND fechaPrevistaFin <= '2023-06-09'
            DateTime fechaInicio = dateTimePicker5.Value;
            DateTime fechaFin = dateTimePicker6.Value;
            conexion.Open();
            string sql = "SELECT numero as 'Número orden', cliente as Cliente, fechaCreacion as 'Fecha prevista inicio' FROM ordenDeFabricacion WHERE idEstadoOrdenF=1 AND fechaCreacion >= @FechaInicio AND fechaPrevistaFin <= @FechaFin AND idEstadoOrdenF=1";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@FechaInicio", fechaInicio);
            comando.Parameters.AddWithValue("@FechaFin", fechaFin);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView3.DataSource = tabla;
            dataGridView3.ClearSelection();
            conexion.Close();
            
        }
        private void dataGridView3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView3.Columns["Fecha prevista inicio"].Index) 
            {
                DateTime fechaCreacion = (DateTime)dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                // Verificar si la fecha de creación es menor a la fecha actual
                if (fechaCreacion < DateTime.Today)
                {
                    e.CellStyle.BackColor = Color.Red;
                    dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "La fecha de inicio ya comenzó";//tooltip
                }
                // Verificar si la fecha de creación es igual a la fecha actual
                if (fechaCreacion == DateTime.Today)
                {
                    e.CellStyle.BackColor = Color.Green;
                    dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "La orden debe iniciar hoy";//tooltip
                }
            }
        }
        private void buscadorPorHacer(string busqueda)
        {
            string consulta = " SELECT numero as 'Número orden', cliente as Cliente, fechaCreacion  as 'Fecha prevista inicio' FROM ordenDeFabricacion WHERE idEstadoOrdenF=1 AND numero LIKE @busqueda AND idEstadoOrdenF=1 ";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView3.DataSource = tabla;
            dataGridView3.ClearSelection();
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string busqueda = textBox3.Text.Trim();
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                if (checkBox3.Checked)
                {
                    conexion.Open();
                    string sql = "SELECT numero as 'Número orden', cliente as Cliente, fechaCreacion as 'Fecha prevista inicio' FROM ordenDeFabricacion WHERE idEstadoOrdenF=1";
                    SqlCommand comando = new SqlCommand(sql, conexion);
                    SqlDataAdapter adaptador = new SqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView3.DataSource = tabla;
                    dataGridView3.ClearSelection();
                    conexion.Close();
                }
                else
                {
                    cargarOrdenesPorHacer();
                }
            }
            else
            {
                buscadorPorHacer(busqueda);
            }
        }
        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            cargarOrdenesPorHacer();
        }
        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
            cargarOrdenesPorHacer();
        }
        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e) 
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Verificar que e.RowIndex y e.ColumnIndex sean válidos.
            {
                string codigo = dataGridView3.Rows[e.RowIndex].Cells["Número orden"].Value.ToString();
                DetalleOrdenFporHacer form2 = new DetalleOrdenFporHacer(this, codigo);
                form2.ShowDialog(this);
            }
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                conexion.Open();
                string sql = "SELECT numero as 'Número orden', cliente as Cliente, fechaCreacion as 'Fecha prevista inicio' FROM ordenDeFabricacion WHERE idEstadoOrdenF=1";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView3.DataSource = tabla;
                dataGridView3.ClearSelection();
                conexion.Close();
            }
        }







        //BOTON NUEVA ORDEN
        private void button3_Click(object sender, EventArgs e)
        {
            seleccionarPedidoParaOrdenF sp = new seleccionarPedidoParaOrdenF();
            sp.ShowDialog();
            
            List<object[]> data = new List<object[]>();
            string articulo = "";
            int numero1 = 0;
            int numero2 = 0;
            RegistrarOrdenDeF registrarForm = new RegistrarOrdenDeF(data, articulo, numero1, numero2);
            registrarForm.FormClosed += registrarForm_FormClosed;
            
        }
        private void registrarForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (checkBox3.Checked)
            {
                conexion.Open();
                string sql = "SELECT numero as 'Número orden', cliente as Cliente, fechaCreacion as 'Fecha prevista inicio' FROM ordenDeFabricacion WHERE idEstadoOrdenF=1";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView3.DataSource = tabla;
                dataGridView3.ClearSelection();
                conexion.Close();
            }
            else
            {
                cargarOrdenesPorHacer();
            }
        }




        //BOTON CONSULTAR ORDENES DE FABRICACION
        private void button2_Click(object sender, EventArgs e)
        {
            ConsultarOrdenF cof = new ConsultarOrdenF();
            cof.ShowDialog();
        }


















        //CLICK  CONSULTAR MP EN DEPOSITO
        private void materiaPrimaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            consultarMateriaPrima cm = new consultarMateriaPrima(area);
            cm.Show(this);
        }
        //CLICK VER MP POR ARTICULO EN DEPOSITO
        private void articulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeleccionarArticuloParaMateriaPrima mpa = new SeleccionarArticuloParaMateriaPrima();
            mpa.Show(this);
        }
        //CLICK VER CANTIDAD MP POR ARTICULO EN DEPOSITO
        private void cantidadDeMateriaPorArticuloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeleccionarArtParaCantidadMP spc = new SeleccionarArtParaCantidadMP();
            spc.ShowDialog();
        }
        //CLICK VER STOCK ARTICULOS EN DEPOSITO
        private void stockDeArticulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerStockArticulos st = new VerStockArticulos();
            st.Show();
        }






        public void Fabricacion_Load(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                conexion.Open();
                string sql = "SELECT numero as 'Número orden', cliente as Cliente, fechaCreacion as 'Fecha prevista inicio' FROM ordenDeFabricacion WHERE idEstadoOrdenF=1";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView3.DataSource = tabla;
                dataGridView3.ClearSelection();
                conexion.Close();
                
            }
            if (checkBox1.Checked)
            {
                conexion.Open();
                string sql = "SELECT numero as 'Número orden', cliente as Cliente, fechaRealFin as 'Fecha finalización', idEstadoOrdenF FROM ordenDeFabricacion WHERE idEstadoOrdenF=5 or idEstadoOrdenF=4";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["idEstadoOrdenF"].Visible = false;
                dataGridView1.ClearSelection();
                conexion.Close();
            }
            if (checkBox2.Checked)
            {
                
                conexion.Open();
                string sql = "SELECT orf.numero as 'Número orden', orf.cliente as Cliente,  p.descripcion as 'Punto de control', orf.fechaPrevistaFin FROM ordenDeFabricacion as orf join puntoDeControl as p on orf.idPuntoDeControl=p.id WHERE idEstadoOrdenF=2";
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView2.DataSource = tabla;
                dataGridView2.Columns["fechaPrevistaFin"].Visible = false;
                dataGridView2.ClearSelection();
                conexion.Close();
                
            }
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
        }

        //BOTON SALIR
        private void button7_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void reporteOrdenesDeFabricacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EstadisticaOrdenesF esof = new EstadisticaOrdenesF();
            esof.ShowDialog();
        }
    }
}
