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
    public partial class ConsultarStockParaOrdenF : Form
    {
        int cantidadFilas = 0;
        int cantidadFilasCantidadMPXART = 0;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        string articulo = "";
        int cantidadPares = 0;
        int numeroPedido = 0;
        string[] art;
        public ConsultarStockParaOrdenF(string articulo, int cantidadPares, int numeroPedido)
        {
            InitializeComponent();
            this.articulo = articulo;
            this.cantidadPares = cantidadPares;
            label3.Text = articulo;
            label4.Text = Convert.ToString(cantidadPares);
            this.numeroPedido = numeroPedido;

            cargarCantidadDeMPXarticulo();
            cargarMPStock();


            cantidadFilas = dataGridView3.RowCount;
            cantidadFilasCantidadMPXART = dataGridView1.Rows.Count;
        }


        private void cargarMPStock()
        {
            //VOY A NECESITAR GUARDAR LA CANTIDAD EN STOCK EN UNA VARIABLE, AL IGUAL QUE LA CANTIDAD A RESERVAR
            //POR CADA MP. VARIABLES GENERALES O HACER UN NUEVO SELECT EN EWL BOTON?
            conexion.Open();
            string sql = "select mp.codigo as 'Código MP', mp.descripcion as Descripción, CONCAT_WS (' ', mp.cantidad, un.descripcion) as 'En stock', CONCAT_WS (' ', mp.cantidadQueContiene, sub.descripcion ) as 'Contiene', mp.cantidad  from cantidadMPxArticulo as can join materiaPrima as mp on can.idMateriaPrima = mp.codigo join unidadDeMedida as un on mp.IdUnidadDeMedida=un.id join SubUnidadDeMedida as sub on mp.IdSubUnidadDeMedidda = sub.id where articulo = @nombre";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nombre", articulo);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            tabla.Columns.Add("Reservar", typeof(string));
            foreach (DataRow row in tabla.Rows)
            {
                row["Reservar"] = "0";
            }
            dataGridView3.DataSource = tabla;
            dataGridView3.Columns["cantidad"].Visible = false;
            conexion.Close();
        }





        private void cargarCantidadDeMPXarticulo()
        {
            decimal porPar=0;
            string Subunidad= "";
            //EL CALCULO DE MP POR CANTIDAD DE PARES SE PUEDE HACER ACA?=????????????
            conexion.Open();
            string sql = "select mp.codigo as 'Código MP' ,mp.descripcion as Descripción, can.cantidad + ' '+ sub.descripcion as 'Necesito por par', can.cantidad as cantidadXpar, sub.descripcion as subunidad, can.area from cantidadMPxArticulo as can join materiaPrima as mp on can.idMateriaPrima = mp.codigo join SubUnidadDeMedida as sub on mp.IdSubUnidadDeMedidda = sub.id where articulo =@nombre";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nombre", articulo);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            tabla.Columns.Add("Cantidad por total de pares", typeof(string));
            foreach (DataRow row in tabla.Rows)
            {
                porPar = Convert.ToDecimal(row["cantidadXpar"]);
                Subunidad = Convert.ToString(row["subunidad"]);
                row["Cantidad por total de pares"] = porPar * cantidadPares +" "+ Subunidad; 
            }            
            dataGridView1.DataSource = tabla;
            dataGridView1.Columns["cantidadXpar"].Visible = false;
            dataGridView1.Columns["subunidad"].Visible = false;
            dataGridView1.Columns["area"].Visible = false;
            conexion.Close();
        }


        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }





        //BOTON CONFIRMAR
        private void button1_Click(object sender, EventArgs e)
        {

            int cantidadReservadoBD = 0;
            int cantidadStockBD = 0;
            MessageBoxButtons botones = MessageBoxButtons.YesNo;
            DialogResult dr = MessageBox.Show("Desea reservar la materia prima indicada", "Reservar materia prima",
                botones, MessageBoxIcon.Question);

           if (dr == DialogResult.Yes)
           {
                //primero comparar que lo ingresado en Reservar sea menor al stock disponible
                //el for hace todo esto por cada fila
             for (int i = 0; i < cantidadFilas; i++)
             {
                DataTable dataTable = (DataTable)dataGridView3.DataSource;

                int reservar = Convert.ToInt32(dataTable.Rows[i]["Reservar"].ToString());
                int stockDisponible = Convert.ToInt32(dataTable.Rows[i]["cantidad"].ToString());
                    if (stockDisponible < reservar)//si el reservar es mayor al stock disponible
                    {
                        //si no alcanza el stock hay que avisar con un mensaje
                        MessageBox.Show("El material a reservar no puede ser menor a lo disponible en stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        
                        return; //ACA NECESITO SALIR DEL IF GENERAL PARA QUE NO SE EJECUTE EL FOREACH CUANDO SALGA DEL FOR ACTUAL
                    }
                    else
                    {
                        //RESERVADO:
                        //TRAER EL VALOR DE RESERVADO EN LA BD del codigo de esa MP
                        conexion.Open();
                        string sql = "select mp.codigo as 'Codigo MP', mp.reservado as 'RESERVADO' from materiaPrima as mp where mp.codigo =@codigo";
                        SqlCommand comando = new SqlCommand(sql, conexion);
                        comando.Parameters.AddWithValue("@codigo", Convert.ToInt32(dataTable.Rows[i]["Código MP"].ToString()));//CONSEGUIR EL CODIGO DE LA MP QUE ESTE PASANDO AHORA POR EL FOR
                        SqlDataReader registro = comando.ExecuteReader();
                        if (registro.Read())
                        {
                            cantidadReservadoBD = Convert.ToInt32(registro["RESERVADO"].ToString());

                        }
                        registro.Close();
                        comando.ExecuteNonQuery();
                        conexion.Close();
                        //SUMARLE LO INGRESADO EN LA VARIABLE "reservar" a la columna "reservado" 
                        conexion.Open();
                        string sql1 = "UPDATE materiaPrima set reservado= @reservar where codigo=@codigo";
                        SqlCommand comando1 = new SqlCommand(sql1, conexion);
                        comando1.Parameters.AddWithValue("@codigo", Convert.ToInt32(dataTable.Rows[i]["Código MP"].ToString()));//CONSEGUIR EL CODIGO DE LA MP QUE ESTE PASANDO AHORA POR EL FOR
                        comando1.Parameters.AddWithValue("@reservar", cantidadReservadoBD + reservar);
                        comando1.ExecuteNonQuery();
                        conexion.Close();


                        //STOCK DISPONIBLE:
                        //TRAER EL VALOR DE STOCK DISPONIBLE DE LA BD del codigo de esa MP
                        conexion.Open();
                        string sql2 = "select mp.codigo as 'Codigo MP', mp.cantidad as 'En stock' from materiaPrima as mp where mp.codigo =@codigo";
                        SqlCommand comando2 = new SqlCommand(sql2, conexion);
                        comando2.Parameters.AddWithValue("@codigo", Convert.ToInt32(dataTable.Rows[i]["Código MP"].ToString()));//CONSEGUIR EL CODIGO DE LA MP QUE ESTE PASANDO AHORA POR EL FOR
                        SqlDataReader registro2 = comando2.ExecuteReader();
                        if (registro2.Read())
                        {
                            cantidadStockBD = Convert.ToInt32(registro2["En stock"].ToString());

                        }
                        registro2.Close();
                        comando2.ExecuteNonQuery();
                        conexion.Close();
                        //RESTARLE LO INGRESADO EN LA VARIABLE "stockDisponible" a la columna "cantidad"
                        conexion.Open();
                        string sql3 = "UPDATE materiaPrima set cantidad= @enStock where codigo=@codigo";
                        SqlCommand comando3 = new SqlCommand(sql3, conexion);
                        comando3.Parameters.AddWithValue("@codigo", Convert.ToInt32(dataTable.Rows[i]["Código MP"].ToString()));//CONSEGUIR EL CODIGO DE LA MP QUE ESTE PASANDO AHORA POR EL FOR
                        comando3.Parameters.AddWithValue("@enStock", cantidadStockBD - reservar);
                        comando3.ExecuteNonQuery();
                        conexion.Close();

                        cantidadReservadoBD = 0;
                        cantidadStockBD = 0;
                    }
             }




                    //ACA HAY QUE HACER UN INSERT EN LA TABLA cantidadMPxPedido, para asi saber cuanto material lleva cada orden
                    art = new string[dataGridView1.Rows.Count];
                    string aux = "";
                    int y = 0;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        aux = row.Cells[0].Value.ToString() + "-";  //codigo Mp
                        aux += row.Cells[1].Value.ToString() + "-"; //MP descripcion
                        aux += row.Cells[2].Value.ToString() + "-";// necesito por par
                        aux += row.Cells[3].Value.ToString() + "-";//cantidad por par
                        aux += row.Cells[4].Value.ToString() + "-";//subunidad de medida
                        aux += row.Cells[5].Value.ToString() + "-";//area
                        aux += row.Cells[6].Value.ToString() + "-";//cantidad por total de pares
                        art[y] = aux;
                        string cadena = art[y];
                        string[] valores = cadena.Split('-');
                        conexion.Open();
                        string sqlPorOrden = "insert into cantidadMPxPedido (numeroPedido, articulo, cantidadXpares, area, idMateriaPrima, totalPares) values (@numeroPedido, @articulo, @cantidadXpares, @area, @idMateriaPrima, @totalPares)";
                        SqlCommand comandoXOrden = new SqlCommand(sqlPorOrden, conexion);
                        comandoXOrden.Parameters.AddWithValue("@numeroPedido", numeroPedido);
                        comandoXOrden.Parameters.AddWithValue("@articulo", label3.Text);
                        comandoXOrden.Parameters.AddWithValue("@cantidadXpares", Convert.ToString(valores[6]));
                        comandoXOrden.Parameters.AddWithValue("@area", Convert.ToString(valores[5]));
                        comandoXOrden.Parameters.AddWithValue("@idMateriaPrima", Convert.ToInt32(valores[0]));
                        comandoXOrden.Parameters.AddWithValue("@totalPares", label4.Text);

                        int resultado = comandoXOrden.ExecuteNonQuery();
                        conexion.Close();
                        aux = "";                        
                    }
                MessageBox.Show("Material Reservado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dispose();
               
            }
           else if (dr == DialogResult.No)
           {
                return;
           }

        }



        private void ConsultarStockParaOrdenF_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView3.ClearSelection();
        }
    }
}
