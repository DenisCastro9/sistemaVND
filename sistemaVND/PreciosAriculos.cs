using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Aspose.Html.Converters;
using Aspose.Html.Saving;

namespace sistemaVND
{
    public partial class PreciosAriculos : Form
    {
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public PreciosAriculos()
        {
            InitializeComponent();
;
            cargarTodosArtRegistrados();

        }

        private void numeroRegistros()
        {
            int numeroRegistros = dataGridView5.RowCount;
            lblRes.Text = numeroRegistros.ToString();
        }

        private void cargarTodosArtRegistrados()
        {
            conexion.Open();
            string sql = "select nombre as Nombre, precioNino as 'Precio talle niño', precioDama as 'Precio talle dama', precioHombre as 'Precio talle hombre' from ArticulosGeneral ";
            SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);

            // Función para limpiar y convertir una cadena a decimal con formato "$"
            string FormatearPrecio(string precio)
            {
                if (string.IsNullOrEmpty(precio) || precio == "NULL")
                {
                    return null; // Retornar null si el precio es vacio
                }

                decimal valorDecimal;
                if (decimal.TryParse(precio.Replace("$", "").Replace(",", "."), NumberStyles.Currency, CultureInfo.InvariantCulture, out valorDecimal))
                {
                    if (valorDecimal == 0)
                    {
                        return null; // Retornar null si el valor decimal es igual a 0
                    }
                    return valorDecimal.ToString("C2", CultureInfo.GetCultureInfo("es-AR"));
                }

                return null; // Si la conversión falla, retornar null
            }

            // Agregar columnas con precios formateados correctamente
            tabla.Columns.Add("Precio talle niño (formateado)", typeof(string));
            tabla.Columns.Add("Precio talle dama (formateado)", typeof(string));
            tabla.Columns.Add("Precio talle hombre (formateado)", typeof(string));

            // Formatear precios en las nuevas columnas
            foreach (DataRow row in tabla.Rows)
            {
                row["Precio talle niño (formateado)"] = FormatearPrecio(row["Precio talle niño"].ToString());
                row["Precio talle dama (formateado)"] = FormatearPrecio(row["Precio talle dama"].ToString());
                row["Precio talle hombre (formateado)"] = FormatearPrecio(row["Precio talle hombre"].ToString());
            }

            // Eliminar columnas originales de precio
            tabla.Columns.Remove("Precio talle niño");
            tabla.Columns.Remove("Precio talle dama");
            tabla.Columns.Remove("Precio talle hombre");

            // Cambiar nombre de las nuevas columnas
            tabla.Columns["Precio talle niño (formateado)"].ColumnName = "Precio talle niño";
            tabla.Columns["Precio talle dama (formateado)"].ColumnName = "Precio talle dama";
            tabla.Columns["Precio talle hombre (formateado)"].ColumnName = "Precio talle hombre";


            dataGridView5.DataSource = tabla;
            conexion.Close();
            dataGridView5.ClearSelection();
            numeroRegistros();
        }





        private void buscador(string busqueda)
        {
            string sql = "select nombre as Nombre, precioNino as 'Precio talle niño', precioDama as 'Precio talle dama', precioHombre as 'Precio talle hombre' from ArticulosGeneral  WHERE nombre LIKE @busqueda";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");

            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);

            // Función para limpiar y convertir una cadena a decimal con formato "$"
            string FormatearPrecio(string precio)
            {
                if (string.IsNullOrEmpty(precio) || precio == "NULL")
                {
                    return null; // Retornar null si el precio es vacio
                }

                decimal valorDecimal;
                if (decimal.TryParse(precio.Replace("$", "").Replace(",", "."), NumberStyles.Currency, CultureInfo.InvariantCulture, out valorDecimal))
                {
                    if (valorDecimal == 0)
                    {
                        return null; // Retornar null si el valor decimal es igual a 0
                    }
                    return valorDecimal.ToString("C2", CultureInfo.GetCultureInfo("es-AR"));
                }

                return null; // Si la conversión falla, retornar null
            }

            // Agregar columnas con precios formateados correctamente
            tabla.Columns.Add("Precio talle niño (formateado)", typeof(string));
            tabla.Columns.Add("Precio talle dama (formateado)", typeof(string));
            tabla.Columns.Add("Precio talle hombre (formateado)", typeof(string));

            // Formatear precios en las nuevas columnas
            foreach (DataRow row in tabla.Rows)
            {
                row["Precio talle niño (formateado)"] = FormatearPrecio(row["Precio talle niño"].ToString());
                row["Precio talle dama (formateado)"] = FormatearPrecio(row["Precio talle dama"].ToString());
                row["Precio talle hombre (formateado)"] = FormatearPrecio(row["Precio talle hombre"].ToString());
            }


            // Eliminar columnas originales de precio
            tabla.Columns.Remove("Precio talle niño");
            tabla.Columns.Remove("Precio talle dama");
            tabla.Columns.Remove("Precio talle hombre");

            // Cambiar nombre de las nuevas columnas
            tabla.Columns["Precio talle niño (formateado)"].ColumnName = "Precio talle niño";
            tabla.Columns["Precio talle dama (formateado)"].ColumnName = "Precio talle dama";
            tabla.Columns["Precio talle hombre (formateado)"].ColumnName = "Precio talle hombre";


            dataGridView5.DataSource = tabla;
            conexion.Close();
            dataGridView5.ClearSelection();
            numeroRegistros();

        }



        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string busqueda = txtBusqueda.Text.Trim();
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                cargarTodosArtRegistrados();
            }
            else
            {
                buscador(busqueda);
            }
        }



        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void PreciosAriculos_Load(object sender, EventArgs e)
        {
            dataGridView5.ClearSelection();
            numeroRegistros();
        }







        //BOTON GENERAR PDF
        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = DateTime.Now.ToString("ddMMyyyy-ListadoDePrecios.pdf") + ".pdf";


            string html_text = Properties.Resources.PlantillaListadoPrecios.ToString();
            html_text = html_text.Replace("@FECHA", DateTime.UtcNow.ToShortDateString());
            string filas = string.Empty;
            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Nombre"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Precio talle niño"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Precio talle dama"].Value.ToString() + "</td>"; //USAR SIEMPRE LOS NOMBRES QUE PUSISTE EN EL DATAGRID
                filas += "<td>" + row.Cells["Precio talle hombre"].Value.ToString() + " </td>";
                filas += "</tr>";
            }
            html_text = html_text.Replace("@FILAS", filas);

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                // Inicializar PdfSaveOptions 
                var options = new PdfSaveOptions();
                // Invoque el método ConvertHTML para convertir el código HTML a PDF
                Converter.ConvertHTML(html_text, ".", options, guardar.FileName); //USA SIEMPRE LA SEGUNDA OPCION
                MessageBox.Show("PDF generado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void dataGridView5_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }
    }
}
