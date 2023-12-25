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

    public partial class MateriaPrimaPorArticulo : Form
    {
        string nombre;             
        private SqlConnection conexion = new SqlConnection("data source=  DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public MateriaPrimaPorArticulo(string nombre)            
        {
            this.nombre = "";
            InitializeComponent();
            textBox10.Text = Convert.ToString(nombre);
            this.nombre = nombre;
            verDatos();


            autocompletarCuero1();
            autocompletarCuero2();
            autoComForro1();
            autocomForro2();
            autocomRefuerzo1();
            autocomRefuerzo2();
            autocomRefuerzo3();
            autoComHilo1();
            autoComHilo2();
            autocomContrafuerte();
            autocomElastico();
            autocomOjalillos();
            autocomPlantillaArmado();
            autoComCaja();
            autoComPlantilla();
            autoComCordon();
            autocomFondo();

        }



        //CARGA LOS DATOS TRAIDOS DE LA BASE SI LOS TUVIERA
        private void verDatos()
        {
            conexion.Open();
            string sql = " select * from materiaPrimaPorArticulo where codigoArt=@codigoArt";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.Add("@codigoArt", SqlDbType.VarChar).Value = nombre;
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                textBox2.Text = registro["cuero1"].ToString();
                textBox3.Text = registro["cuero2"].ToString();
                textBox4.Text = registro["forro1"].ToString();
                textBox1.Text = registro["forro2"].ToString();
                textBox11.Text = registro["refuerzo1"].ToString();
                textBox12.Text = registro["refuerzo2"].ToString();
                textBox18.Text = registro["refuerzo3"].ToString();
                textBox5.Text = registro["hilo1"].ToString();
                textBox19.Text = registro["hilo2"].ToString();
                textBox6.Text = registro["costuron"].ToString();
                textBox7.Text = registro["apliqueI"].ToString();
                textBox8.Text = registro["apliqueII"].ToString();
                textBox9.Text = registro["apliqueIII"].ToString();
                textBox20.Text = registro["contrafuerte"].ToString();
                textBox21.Text = registro["elastico"].ToString();
                textBox22.Text = registro["ojalillos"].ToString();
                textBox23.Text = registro["plantillaArmado"].ToString();
                textBox17.Text = registro["caja"].ToString();
                textBox16.Text = registro["plantilla"].ToString();
                textBox15.Text = registro["cordon"].ToString();
                textBox14.Text = registro["horma"].ToString();
                textBox24.Text = registro["fondo"].ToString();
                textBox29.Text = registro["comentario1"].ToString();
                textBox31.Text = registro["comentario2"].ToString();
                textBox25.Text = registro["comentario3"].ToString();
            }
            else
            {
                MessageBox.Show("No hay datos registrados", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            conexion.Close();
        }


        //AUTOCOMPLETAR------------------------------------------------------------------INICIO
        //para que funcione el autocompletar tienen que estar las propiedades:
        //AutoCompletMode:suggestApenned
        //AutoCompleteSource: CustomSource


        DataTable datoFondo = new DataTable();
        private void autocomFondo()
        {
            conexion.Open();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM materiaPrima", conexion);
            adaptador.Fill(datoFondo);
            for (int i = 0; i < datoFondo.Rows.Count; i++)
            {
                lista.Add(datoFondo.Rows[i]["descripcion"].ToString());
            }
            textBox24.AutoCompleteCustomSource = lista;
            conexion.Close();
        }

        DataTable datoPlantillaArmado = new DataTable();
        private void autocomPlantillaArmado()
        {
            conexion.Open();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM materiaPrima", conexion);
            adaptador.Fill(datoPlantillaArmado);
            for (int i = 0; i < datoPlantillaArmado.Rows.Count; i++)
            {
                lista.Add(datoPlantillaArmado.Rows[i]["descripcion"].ToString());
            }
            textBox23.AutoCompleteCustomSource = lista;
            conexion.Close();
        }

        DataTable datoOjalillo = new DataTable();
        private void autocomOjalillos()
        {
            conexion.Open();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM materiaPrima", conexion);
            adaptador.Fill(datoOjalillo);
            for (int i = 0; i < datoOjalillo.Rows.Count; i++)
            {
                lista.Add(datoOjalillo.Rows[i]["descripcion"].ToString());
            }
            textBox22.AutoCompleteCustomSource = lista;
            conexion.Close();
        }

        DataTable datoElastico = new DataTable();
        private void autocomElastico()
        {
            conexion.Open();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM materiaPrima", conexion);
            adaptador.Fill(datoElastico);
            for (int i = 0; i < datoElastico.Rows.Count; i++)
            {
                lista.Add(datoElastico.Rows[i]["descripcion"].ToString());
            }
            textBox21.AutoCompleteCustomSource = lista;
            conexion.Close();
        }

        DataTable datoContrafuerte = new DataTable();
        private void autocomContrafuerte()
        {
            conexion.Open();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM materiaPrima", conexion);
            adaptador.Fill(datoContrafuerte);
            for (int i = 0; i < datoContrafuerte.Rows.Count; i++)
            {
                lista.Add(datoContrafuerte.Rows[i]["descripcion"].ToString());
            }
            textBox20.AutoCompleteCustomSource = lista;
            conexion.Close();
        }

        DataTable datoHilo2 = new DataTable();
        private void autoComHilo2()
        {
            conexion.Open();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM materiaPrima", conexion);
            adaptador.Fill(datoHilo2);
            for (int i = 0; i < datoHilo2.Rows.Count; i++)
            {
                lista.Add(datoHilo2.Rows[i]["descripcion"].ToString());
            }
            textBox19.AutoCompleteCustomSource = lista;
            conexion.Close();
        }

        DataTable datoRefuerzo3 = new DataTable();
        private void autocomRefuerzo3()
        {
            conexion.Open();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM materiaPrima", conexion);
            adaptador.Fill(datoRefuerzo3);
            for (int i = 0; i < datoRefuerzo3.Rows.Count; i++)
            {
                lista.Add(datoRefuerzo3.Rows[i]["descripcion"].ToString());
            }
            textBox18.AutoCompleteCustomSource = lista;
            conexion.Close();
        }

        DataTable datoRefuerzo2 = new DataTable();
        private void autocomRefuerzo2()
        {
            conexion.Open();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM materiaPrima", conexion);
            adaptador.Fill(datoRefuerzo2);
            for (int i = 0; i < datoRefuerzo2.Rows.Count; i++)
            {
                lista.Add(datoRefuerzo2.Rows[i]["descripcion"].ToString());
            }
            textBox12.AutoCompleteCustomSource = lista;
            conexion.Close();
        }

        DataTable datoRefuerzo1 = new DataTable();
        private void autocomRefuerzo1()
        {
            conexion.Open();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM materiaPrima", conexion);
            adaptador.Fill(datoRefuerzo1);
            for (int i = 0; i < datoRefuerzo1.Rows.Count; i++)
            {
                lista.Add(datoRefuerzo1.Rows[i]["descripcion"].ToString());
            }
            textBox11.AutoCompleteCustomSource = lista;
            conexion.Close();
        }

        DataTable datoForro2 = new DataTable();
        private void autocomForro2()
        {
            conexion.Open();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM materiaPrima", conexion);
            adaptador.Fill(datoForro2);
            for (int i = 0; i < datoForro2.Rows.Count; i++)
            {
                lista.Add(datoForro2.Rows[i]["descripcion"].ToString());
            }
            textBox1.AutoCompleteCustomSource = lista;
            conexion.Close();
        }

        DataTable datoCordon = new DataTable();
        private void autoComCordon()
        {
            conexion.Open();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM materiaPrima", conexion);
            adaptador.Fill(datoCordon);
            for (int i = 0; i < datoCordon.Rows.Count; i++)
            {
                lista.Add(datoCordon.Rows[i]["descripcion"].ToString());
            }
            textBox15.AutoCompleteCustomSource = lista;
            conexion.Close();
        }

        DataTable datoPlantilla = new DataTable();
        private void autoComPlantilla()
        {
            conexion.Open();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM materiaPrima", conexion);
            adaptador.Fill(datoPlantilla);
            for (int i = 0; i < datoPlantilla.Rows.Count; i++)
            {
                lista.Add(datoPlantilla.Rows[i]["descripcion"].ToString());
            }
            textBox16.AutoCompleteCustomSource = lista;
            conexion.Close();
        }

        DataTable datoCaja = new DataTable();
        private void autoComCaja()
        {
            conexion.Open();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM materiaPrima", conexion);
            adaptador.Fill(datoCaja);
            for (int i = 0; i < datoCaja.Rows.Count; i++)
            {
                lista.Add(datoCaja.Rows[i]["descripcion"].ToString());
            }
            textBox17.AutoCompleteCustomSource = lista;
            conexion.Close();
        }

        DataTable datoHilo = new DataTable();
        private void autoComHilo1()
        {
            conexion.Open();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM materiaPrima", conexion);
            adaptador.Fill(datoHilo);
            for (int i = 0; i < datoHilo.Rows.Count; i++)
            {
                lista.Add(datoHilo.Rows[i]["descripcion"].ToString());
            }
            textBox5.AutoCompleteCustomSource = lista;
            conexion.Close();
        }

        DataTable datoForro = new DataTable();
        private void autoComForro1()
        {
            conexion.Open();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM materiaPrima", conexion);
            adaptador.Fill(datoForro);
            for (int i = 0; i < datoForro.Rows.Count; i++)
            {
                lista.Add(datoForro.Rows[i]["descripcion"].ToString());
            }
            textBox4.AutoCompleteCustomSource = lista;
            conexion.Close();
        }

        DataTable datosCuero2 = new DataTable();
        private void autocompletarCuero2()
        {
            conexion.Open();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM materiaPrima", conexion);
            adaptador.Fill(datosCuero2);
            for (int i = 0; i < datosCuero2.Rows.Count; i++)
            {
                lista.Add(datosCuero2.Rows[i]["descripcion"].ToString());
            }
            textBox3.AutoCompleteCustomSource = lista;
            conexion.Close();
        }
        
        DataTable datosCuero1 = new DataTable();
        private void autocompletarCuero1()
        {
            conexion.Open();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
            SqlDataAdapter adaptador = new SqlDataAdapter("SELECT * FROM materiaPrima", conexion);
            adaptador.Fill(datosCuero1);
            for (int i = 0; i < datosCuero1.Rows.Count; i++)
            {
                lista.Add(datosCuero1.Rows[i]["descripcion"].ToString());
            }
            textBox2.AutoCompleteCustomSource = lista;
            conexion.Close();
        }

        //AUTOCOMPLETAR------------------------------------------------------------------FIN




        //BOTON SALIR
        private void button7_Click(object sender, EventArgs e)
        {
            Dispose();
        }




        //BOTON REGISTRAR / MODIFICAR
        private void button6_Click(object sender, EventArgs e)
        {
            if (!existeMPXA())
            {

                if (textBox2.Text == "" || textBox5.Text == "" || textBox17.Text == "" || textBox16.Text == "" || textBox14.Text == "" || textBox14.Text == "")
                {
                    MessageBox.Show("Completar campos obligatorios *", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                conexion.Open();
                string sql = " insert into materiaPrimaPorArticulo (codigoArt, cuero1, cuero2, forro1, forro2, refuerzo1, " +
                    "refuerzo2, refuerzo3, hilo1, hilo2, costuron, apliqueI, apliqueII, apliqueIII, contrafuerte, elastico, ojalillos, plantillaArmado, caja, plantilla, cordon, horma, fondo, comentario1, comentario2, comentario3 ) values(@codigoArt, @cuero1, @cuero2, @forro1, @forro2, @refuerzo1, @refuerzo2, @refuerzo3, @hilo1, @hilo2, @costuron, @apliqueI, @apliqueII, @apliqueIII, @contrafuerte, @elastico, @ojalillos, @plantillaArmado, @caja, @plantilla, @cordon, @horma, @fondo, @comentario1, @comentario2, @comentario3 )";
                SqlCommand comando = new SqlCommand(sql, conexion);

                comando.Parameters.Add("@codigoArt", SqlDbType.VarChar).Value = textBox10.Text;
                comando.Parameters.Add("@cuero1", SqlDbType.VarChar).Value = textBox2.Text;
                comando.Parameters.Add("@cuero2", SqlDbType.VarChar).Value = textBox3.Text;
                comando.Parameters.Add("@forro1", SqlDbType.VarChar).Value = textBox4.Text;
                comando.Parameters.Add("@forro2", SqlDbType.VarChar).Value = textBox1.Text;
                comando.Parameters.Add("@refuerzo1", SqlDbType.VarChar).Value = textBox11.Text;
                comando.Parameters.Add("@refuerzo2", SqlDbType.VarChar).Value = textBox12.Text;
                comando.Parameters.Add("@refuerzo3", SqlDbType.VarChar).Value = textBox18.Text;
                comando.Parameters.Add("@hilo1", SqlDbType.VarChar).Value = textBox5.Text;
                comando.Parameters.Add("@hilo2", SqlDbType.VarChar).Value = textBox19.Text;
                comando.Parameters.Add("@costuron", SqlDbType.VarChar).Value = textBox6.Text;
                comando.Parameters.Add("@apliqueI", SqlDbType.VarChar).Value = textBox7.Text;
                comando.Parameters.Add("@apliqueII", SqlDbType.VarChar).Value = textBox8.Text;
                comando.Parameters.Add("@apliqueIII", SqlDbType.VarChar).Value = textBox9.Text;
                comando.Parameters.Add("@contrafuerte", SqlDbType.VarChar).Value = textBox20.Text;
                comando.Parameters.Add("@elastico", SqlDbType.VarChar).Value = textBox21.Text;
                comando.Parameters.Add("@ojalillos", SqlDbType.VarChar).Value = textBox22.Text;
                comando.Parameters.Add("@plantillaArmado", SqlDbType.VarChar).Value = textBox23.Text;
                comando.Parameters.Add("@caja", SqlDbType.VarChar).Value = textBox17.Text;
                comando.Parameters.Add("@plantilla", SqlDbType.VarChar).Value = textBox16.Text;
                comando.Parameters.Add("@cordon", SqlDbType.VarChar).Value = textBox15.Text;
                comando.Parameters.Add("@horma", SqlDbType.VarChar).Value = textBox14.Text;
                comando.Parameters.Add("@fondo", SqlDbType.VarChar).Value = textBox24.Text;
                comando.Parameters.Add("@comentario1", SqlDbType.VarChar).Value = textBox29.Text;
                comando.Parameters.Add("@comentario2", SqlDbType.VarChar).Value = textBox31.Text;
                comando.Parameters.Add("@comentario3", SqlDbType.VarChar).Value = textBox25.Text;
                comando.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Datos cargados", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else {
                if (textBox2.Text == "" || textBox5.Text == "" || textBox17.Text == "" || textBox16.Text == "" || textBox14.Text == "" || textBox14.Text == "")
                {
                    MessageBox.Show("Completar campos obligatorios *", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                conexion.Open();
                string sql = " update materiaPrimaPorArticulo set cuero1=@cuero1, cuero2=@cuero2, forro1=@forro1 , forro2=@forro2, refuerzo1=@refuerzo1, refuerzo2=@refuerzo2, refuerzo3=@refuerzo3, hilo1=@hilo1, hilo2=@hilo2, costuron=@costuron, apliqueI=@apliqueI, apliqueII=@apliqueII, apliqueIII=@apliqueIII, contrafuerte=@contrafuerte, elastico=@elastico, ojalillos=@ojalillos, plantillaArmado=@plantillaArmado, caja=@caja, plantilla=@plantilla, cordon=@cordon, horma=@horma, fondo=@fondo, comentario1=@comentario1, comentario2=@comentario2, comentario3=@comentario3 where codigoArt = @codigoArt";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@codigoArt", SqlDbType.VarChar).Value = textBox10.Text;
                comando.Parameters.Add("@cuero1", SqlDbType.VarChar).Value = textBox2.Text;
                comando.Parameters.Add("@cuero2", SqlDbType.VarChar).Value = textBox3.Text;
                comando.Parameters.Add("@forro1", SqlDbType.VarChar).Value = textBox4.Text;
                comando.Parameters.Add("@forro2", SqlDbType.VarChar).Value = textBox1.Text;
                comando.Parameters.Add("@refuerzo1", SqlDbType.VarChar).Value = textBox11.Text;
                comando.Parameters.Add("@refuerzo2", SqlDbType.VarChar).Value = textBox12.Text;
                comando.Parameters.Add("@refuerzo3", SqlDbType.VarChar).Value = textBox18.Text;
                comando.Parameters.Add("@hilo1", SqlDbType.VarChar).Value = textBox5.Text;
                comando.Parameters.Add("@hilo2", SqlDbType.VarChar).Value = textBox19.Text;
                comando.Parameters.Add("@costuron", SqlDbType.VarChar).Value = textBox6.Text;
                comando.Parameters.Add("@apliqueI", SqlDbType.VarChar).Value = textBox7.Text;
                comando.Parameters.Add("@apliqueII", SqlDbType.VarChar).Value = textBox8.Text;
                comando.Parameters.Add("@apliqueIII", SqlDbType.VarChar).Value = textBox9.Text;
                comando.Parameters.Add("@contrafuerte", SqlDbType.VarChar).Value = textBox20.Text;
                comando.Parameters.Add("@elastico", SqlDbType.VarChar).Value = textBox21.Text;
                comando.Parameters.Add("@ojalillos", SqlDbType.VarChar).Value = textBox22.Text;
                comando.Parameters.Add("@plantillaArmado", SqlDbType.VarChar).Value = textBox23.Text;
                comando.Parameters.Add("@caja", SqlDbType.VarChar).Value = textBox17.Text;
                comando.Parameters.Add("@plantilla", SqlDbType.VarChar).Value = textBox16.Text;
                comando.Parameters.Add("@cordon", SqlDbType.VarChar).Value = textBox15.Text;
                comando.Parameters.Add("@horma", SqlDbType.VarChar).Value = textBox14.Text;
                comando.Parameters.Add("@fondo", SqlDbType.VarChar).Value = textBox24.Text;
                comando.Parameters.Add("@comentario1", SqlDbType.VarChar).Value = textBox29.Text;
                comando.Parameters.Add("@comentario2", SqlDbType.VarChar).Value = textBox31.Text;
                comando.Parameters.Add("@comentario3", SqlDbType.VarChar).Value = textBox25.Text;
                int cant = comando.ExecuteNonQuery();
                if (cant == 1)
                {
                    MessageBox.Show("Datos modificados", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("NO existe el artículo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                conexion.Close();
            }
        }



        //¿EL ARTICULO YA TIENE MATERIA PRIMA ASIGNADA? SE BUSCA POR EL NOMBRE (TIENE CODIGO PERO ES VARCHAR/NOMBRE)
        private bool existeMPXA()
        {
            bool existe = false;
            if (textBox10.Text == "")
            {
                MessageBox.Show("El nombre de artículo no puede estar vacío", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                conexion.Open();

                string sql = "select * from materiaPrimaPorArticulo where codigoArt=@codigoArt"; //EL CODIGO DEL ARTICULO ES VARCHAR, VA A SER EL NOMBRE
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.Add("@codigoArt", SqlDbType.VarChar).Value = textBox10.Text;
                SqlDataReader registro = comando.ExecuteReader();
                if (registro.Read())
                    existe = true;
                conexion.Close();

            }
            return existe;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
