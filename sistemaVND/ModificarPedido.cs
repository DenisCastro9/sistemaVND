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
    public partial class ModificarPedido : Form
    {
        DateTime fecha;
        int pedido = 0;
        int sumaCantidadPares = 0;
        decimal sumaImporteTotal = 0;
        string[] art;

        decimal total = 0;
        decimal montoTotal = 0;
        int totalPares = 0;
        int idArt = 0;


        decimal creditoCliente = 0;


        BuscarDetallePEdido formPrincipal;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public ModificarPedido(BuscarDetallePEdido form,int numeroP)
        {
            InitializeComponent();
            formPrincipal = form;
            this.pedido = numeroP;
            label14.Text = Convert.ToString(pedido);
            cargarDatos();
            cargarDataG();
            cargarDataFormaDePago();
        }



        private void cargarDataG()
        {
            conexion.Open();
            string sql = "select a.idarticulo, a.codigoArticulo as Código, a.nombre as Nombre, a.talle as Talle, a.precioUnitario as Precio, d.cantidad as 'Cantidad Pares' from detalleDePedido as d join articulo as a on d.idArticulo = a.idArticulo where d.numero=@numero";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@numero", pedido);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();            
            adaptador.Fill(tabla);
            tabla.Columns.Add("A modificar");
            dataGridView1.DataSource = tabla;
            dataGridView1.Columns["idArticulo"].Visible = false;
            dataGridView1.Columns["Precio"].Visible = false;
            conexion.Close();
            dataGridView1.ClearSelection();
        }


        private void cargarDatos()
        {
            conexion.Open();
            string sql = "select c.razonSocial as Cliente, p.fechaDePedido as Fecha, p.totalPares as Total, p.importeTotal as Importe, f.descripcion as 'Forma de pago' from pedido as p join cliente as c on p.idCliente = c.id join formaDePago as f on p.idFormaDePago = f.id where numero = @numero";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@numero", pedido);
            SqlDataReader registros = comando.ExecuteReader();
            if (registros.Read())
            {
                label11.Text = registros["Cliente"].ToString();
                fecha = Convert.ToDateTime(registros["Fecha"].ToString());
                label10.Text = fecha.ToShortDateString().ToString();                
                label6.Text = registros["Total"].ToString();
                label3.Text = registros["Importe"].ToString();
                comboBox2.Text = registros["Forma de pago"].ToString();
            }
            conexion.Close();
            
        }
        private void cargarDataFormaDePago()
        {
            conexion.Open();
            string sql = "select id, descripcion from formaDePago";
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


        //BOTON MODIFICAR
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBoxButtons botones = MessageBoxButtons.YesNo;
            DialogResult dr = MessageBox.Show("¿Desea modificar el pedido número " + label14.Text + "?", "Modificar pedido",
                botones, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (dataGridView1.SelectedRows.Count != dataGridView1.Rows.Count)
                {
                    MessageBox.Show("Debe seleccionar todas las filas antes de modificar.", "Error de selección", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }


                //primero confirmar que el monto del pedido sea menor al credito asignado al cliente
                conexion.Open();
                string sqlComparar = "select credito.limite as Credito from cliente join credito on cliente.idCredito=credito.idcreditO where cliente.razonSocial=@cliente";
                SqlCommand comed = new SqlCommand(sqlComparar, conexion);
                comed.Parameters.AddWithValue("@cliente", label11.Text);
                SqlDataReader registro = comed.ExecuteReader();
                if (registro.Read())
                {
                    creditoCliente = Convert.ToDecimal(registro["Credito"].ToString());
                }
                else
                {
                    MessageBox.Show("El cliente no tiene crédito asignado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    conexion.Close();
                    return;
                }
                conexion.Close();



                if (Convert.ToDecimal(label3.Text) < creditoCliente) //SI IMPORTE TOTAL ES MENOR AL CREDITO ASIGNADO
                {

                    //ENTONCES SE PUEDE REGISTRAR EL PEDIDO
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        String query = "update pedido set idFormaDePago=@idFormaDePago";
                        SqlCommand command = new SqlCommand(query, conexion);
                        command.Parameters.AddWithValue("@idFormaDePago", comboBox2.SelectedValue);
                        conexion.Open();
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                        {
                            Console.WriteLine("No se pudo modificar la forma de pago");
                        }
                        else {

                            //primero deberia borrar el detalle que contenga ese numero
                            
                            string sqlDelete = "delete from detalleDePedido WHERE numero=@numero";
                            SqlCommand comandoDele = new SqlCommand(sqlDelete, conexion);
                            comandoDele.Parameters.AddWithValue("@numero", pedido);
                            int resultadoDelete = comandoDele.ExecuteNonQuery();
                            



                            //e insertarlo de nuevo para que no se guarde todo igual
                            art = new string[dataGridView1.SelectedRows.Count];
                            string aux = "";
                            int y = 0;
                            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                            {
                                aux = row.Cells[0].Value.ToString() + "-";  //id art
                                aux += row.Cells[1].Value.ToString() + "-"; //codigo art
                                aux += row.Cells[2].Value.ToString() + "-";// nombre art
                                aux += row.Cells[3].Value.ToString() + "-";//talle art
                                aux += row.Cells[4].Value.ToString() + "-"; //precio art
                                aux += row.Cells[5].Value.ToString() + "-"; //cantidad art
                                aux += row.Cells[6].Value.ToString() + "-"; //cantidad a modificar
                                art[y] = aux;
                                string cadena = art[y];
                                string[] valores = cadena.Split('-');

                                total = (Convert.ToDecimal(valores[4]) * Convert.ToDecimal(valores[6]));
                                montoTotal = montoTotal + total;
                                totalPares = totalPares + Convert.ToInt32(valores[6]);
                                idArt = Convert.ToInt32(valores[0]);



                                query = "INSERT INTO detalleDePedido (numero, idArticulo, precio , cantidad) VALUES (@numero,  @idArticulo ,@precio, @cantidad)"; 
                                command = new SqlCommand(query, conexion);
                                command.Parameters.AddWithValue("@numero", pedido); 
                                command.Parameters.AddWithValue("@idArticulo", Convert.ToInt32(valores[0])); 
                                command.Parameters.AddWithValue("@precio", Convert.ToDecimal(valores[4]));
                                command.Parameters.AddWithValue("@cantidad", Convert.ToInt32(valores[6]));
                                int result2 = command.ExecuteNonQuery();
                                if (result2 < 0)
                                {
                                    Console.WriteLine("No se pudo modificar el detalle del pedido");
                                }
                                aux = "";
                            }


                            String query3 = "update pedido set importeTotal = @importeTotal, totalPares=@totalPares, idArt=@idArt where numero = @id";
                            SqlCommand commandx = new SqlCommand(query3, conexion);
                            commandx.Parameters.Add("@id", SqlDbType.Int).Value = pedido;
                            commandx.Parameters.AddWithValue("@importeTotal", Convert.ToDecimal(montoTotal));
                            commandx.Parameters.AddWithValue("@totalPares", Convert.ToDecimal(totalPares));
                            commandx.Parameters.AddWithValue("@idArt", Convert.ToInt32(idArt));
                            commandx.ExecuteNonQuery();

                            MessageBox.Show("Datos modificados", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            formPrincipal.cargarDatosNuevos();
                            formPrincipal.DetallePedidosDataGridview();
                            Dispose();

                        }
                        conexion.Close();
                        total = 0;
                        montoTotal = 0;
                        totalPares = 0;
                    }
                    else
                    {
                        MessageBox.Show("Confirme los artículos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("El monto total supera el límite de crédito del cliente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (dr == DialogResult.No)
            {
                return;
            }
        }




        //HAGO CAMBIOS EN EL DATA
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["A modificar"].Index && e.RowIndex >= 0)
            {
                int cantidadPares = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["A modificar"].Value);
                decimal precio = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["Precio"].Value);

                if (dataGridView1.Rows[e.RowIndex].Cells["A modificar"].Tag != null)
                {
                    int valorAnterior = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["A modificar"].Tag);
                    sumaCantidadPares -= valorAnterior;
                }

                sumaCantidadPares += cantidadPares;
                decimal importeTotal = cantidadPares * precio;
                dataGridView1.Rows[e.RowIndex].Cells["A modificar"].Tag = cantidadPares;
                label6.Text = sumaCantidadPares.ToString();

                if (dataGridView1.Rows[e.RowIndex].Cells["Precio"].Tag != null)
                {
                    decimal importeAnterior = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["Precio"].Tag);
                    sumaImporteTotal -= importeAnterior;
                }

                sumaImporteTotal += importeTotal;
                dataGridView1.Rows[e.RowIndex].Cells["Precio"].Tag = importeTotal;
                label3.Text = sumaImporteTotal.ToString();
            }
        }



        //BOTON CANCELAR
        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void ModificarPedido_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
