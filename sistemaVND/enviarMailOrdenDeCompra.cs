using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace sistemaVND
{
    public partial class enviarMailOrdenDeCompra : Form
    {
        int numeroOrdenC = 0;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public enviarMailOrdenDeCompra(int numeroOrden, string mailProveedor)
        {
            InitializeComponent();
            this.numeroOrdenC = numeroOrden;
            txbDestino.Text = mailProveedor;
        }

        private string path = @"C:\pdfVNDORDENDECOMPRA";


        private void enviarMailOrdenDeCompra_Load(object sender, EventArgs e)
        {
            fileSystemWatcher1.Path = path;
            getFiless();
        }


        private void getFiless()
        {
            
            string[] lst = Directory.GetFiles(path);
            comboBox1.Items.Clear();
            foreach(var sFile in lst)
            {
                //comboBox1.Items.Add(sFile);
                string fileName = Path.GetFileName(sFile);
                comboBox1.Items.Add(new FileItem { FileName = fileName, FullPath = sFile });

            }

        }

        public class FileItem
        {
            public string FileName { get; set; }
            public string FullPath { get; set; }

            public override string ToString()
            {
                return FileName; // Esto es lo que se mostrará en el ComboBox
            }
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            getFiless();
        }

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            getFiless();
        }



        private bool EsCorreoValido(string correo)
        {
            // Expresión regular para validar el formato de un correo electrónico
            string patronCorreo = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Verificar si el correo cumple con el formato de la expresión regular
            return Regex.IsMatch(correo, patronCorreo);
        }


        private void button1_Click(object sender, EventArgs e)
        {

            MessageBoxButtons botones = MessageBoxButtons.YesNo;
            DialogResult dr = MessageBox.Show("¿Desea enviar de la orden de compra?", "Enviar orden de compra",
                botones, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                conexion.Open();
                string sql = "update ordenDeCompra set idEstadoOrdenC=2 where numero=@numero";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@numero", numeroOrdenC);
                comando.ExecuteNonQuery();
                conexion.Close();


                //ACA HACE EL ENVIO:
                if (comboBox1.SelectedItem is FileItem selectedFile)
                {
                    string emailOrigen = "denis12castr3o@gmail.com";
                    string emailDestino = txbDestino.Text;
                    string contraseña = "iylksyavwuwdzcqo";
                    string pathEnvio = selectedFile.FullPath;

                    if (EsCorreoValido(txbDestino.Text))
                    {

                        MailMessage oMailMessage = new MailMessage(emailOrigen, emailDestino, txbAsunto.Text, txbMensaje.Text);
                        oMailMessage.Attachments.Add(new Attachment(pathEnvio));

                        SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
                        oSmtpClient.EnableSsl = true;
                        oSmtpClient.UseDefaultCredentials = false;
                        oSmtpClient.Port = 587;
                        oSmtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, contraseña);

                        try
                        {
                            oSmtpClient.Send(oMailMessage);
                            MessageBox.Show("Email enviado correctamente", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al enviar el email: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        finally
                        {
                            oSmtpClient.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ingrese una dirección de correo electrónico válida", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("Seleccionar un archivo válido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                this.Close();

            }
            else if (dr == DialogResult.No)
            {
                return;
            }            
    }







        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
