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
    public partial class menuPrincipal : Form
    {
        private string areaM, nombre, clave;

        public menuPrincipal(string area, string nom, string cla)
        {
            InitializeComponent();
            this.areaM = area;
            this.nombre = nom;
            this.clave = cla;
            label5.Text = area;
            label6.Text = nom;


        }

        SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        SqlDataAdapter adaptador;
        DateTime fechaEgreso = DateTime.Now;

        private void egresoMenu()
        {
            adaptador = new SqlDataAdapter();

            SqlCommand fechaEgreso = new SqlCommand("UPDATE usuarios SET fechaEgreso = @fechaEgreso WHERE nombre = @nombre AND clave = @clave", conexion);
            fechaEgreso.Parameters.AddWithValue("@nombre", nombre);
            fechaEgreso.Parameters.AddWithValue("@clave", clave);
            adaptador.UpdateCommand = fechaEgreso;
            adaptador.UpdateCommand.Parameters.Add(new SqlParameter("@fechaEgreso", SqlDbType.DateTime));
            adaptador.UpdateCommand.Parameters["@fechaEgreso"].Value = DateTime.Now;

        }


  

        private void salir_Click(object sender, EventArgs e)
        {
            egresoMenu();
            adaptador.UpdateCommand.Parameters["@fechaEgreso"].Value = fechaEgreso;
            try
            {
                conexion.Open();
                adaptador.UpdateCommand.ExecuteNonQuery();
                //MessageBox.Show("Se Guardo La fecha y hora de salida del sistema", "sistema");
            }
            catch (SqlException excepcion)
            {
                MessageBox.Show(excepcion.ToString());
            }
            finally
            {
                conexion.Close();
            }
            Application.Exit();
        }





     

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string usuario =label6.Text;
            RegistrarPedido np = new RegistrarPedido(usuario);
            np.ShowDialog();
        }


        private void articulosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            
        }


        private void menuPrincipal_Load(object sender, EventArgs e)
        {
            if (areaM == "Administración")
            {
            }
            else
            {
                if (areaM == "Ventas")
                {
                    depositoToolStripMenuItem.Visible = false;
                    fabricacionToolStripMenuItem.Visible = false;
                    informesToolStripMenuItem.Visible = false;
                    administracionDeUsuariosToolStripMenuItem.Visible = false;
                    panel3.Visible = false;

                    
                    Size nuevoTamano = new Size(196, 700);
                    menuStrip1.Size = nuevoTamano;

                    Point ubicacionFecha = new Point(303, 225);
                    panel5.Location = ubicacionFecha;
                    panel5.Size = new Size(369, 232);
                    
                    Point ubicacionFechaDigital = new Point(47, 52);
                    Point ubicacionHoraDigital = new Point(93, 105);
                    lblDateTime.Location = ubicacionFechaDigital;
                    lblDateTime2.Location = ubicacionHoraDigital;
                    lblDateTime2.Font = new Font(lblDateTime2.Font.FontFamily, 30);
                    lblDateTime.Font = new Font(lblDateTime.Font.FontFamily, 36);

                    menuStrip1.Padding = new Padding(50, menuStrip1.Padding.Top, menuStrip1.Padding.Right, menuStrip1.Padding.Bottom);


                }
                else
                {
                    if (areaM == "Fabricación")
                    {
                        areaDeVentasToolStripMenuItem.Visible = false;
                        depositoToolStripMenuItem.Visible = false;
                        informesToolStripMenuItem.Visible = false;
                        administracionDeUsuariosToolStripMenuItem.Visible = false;
                        panel1.Visible = false;
                        panel2.Visible = false;
                        panel3.Visible = false;


                        Point ubicacionFecha = new Point(505, 217);
                        panel5.Location = ubicacionFecha;
                        panel5.Size = new Size(369, 232);

                        Point ubicacionFechaDigital = new Point(47, 52);
                        Point ubicacionHoraDigital = new Point(93, 105);
                        lblDateTime.Location = ubicacionFechaDigital;
                        lblDateTime2.Location = ubicacionHoraDigital;
                        lblDateTime2.Font = new Font(lblDateTime2.Font.FontFamily, 30);
                        lblDateTime.Font = new Font(lblDateTime.Font.FontFamily, 36);
                    }
                    else
                    {
                        if (areaM == "Depósito")
                        {
                            areaDeVentasToolStripMenuItem.Visible = false;
                            informesToolStripMenuItem.Visible = false;
                            administracionDeUsuariosToolStripMenuItem.Visible = false;
                            fabricacionToolStripMenuItem.Visible = false;
                            panel1.Visible = false;
                            panel2.Visible = false;

                            Point nuevaUbicacion = new Point(857, 156);
                            panel3.Location = nuevaUbicacion;

                            Point ubicacionFecha = new Point(303, 225);
                            panel5.Location = ubicacionFecha;
                            panel5.Size = new Size(369, 232);

                            Point ubicacionFechaDigital = new Point(47, 52);
                            Point ubicacionHoraDigital = new Point(93, 105);
                            lblDateTime.Location = ubicacionFechaDigital;
                            lblDateTime2.Location = ubicacionHoraDigital;
                            lblDateTime2.Font = new Font(lblDateTime2.Font.FontFamily, 30);
                            lblDateTime.Font = new Font(lblDateTime.Font.FontFamily, 36);

                            menuStrip1.Padding = new Padding(50, menuStrip1.Padding.Top, menuStrip1.Padding.Right, menuStrip1.Padding.Bottom);

                        }
                    }
                }
            }
        }



        private void ingresosYEgresosDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void clientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RegistrarCliente rc = new RegistrarCliente();
            rc.Show(this);
        }

        private void facturadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuscarPedidoParaFactura r = new BuscarPedidoParaFactura();
            r.Show(this);
        }

        private void consultarFacturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FacturasRegistradas r = new FacturasRegistradas();
            r.Show(this);
        }

        private void emisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuscarPedidoParaRemito bp = new BuscarPedidoParaRemito();
            bp.Show();
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultarRemito cr = new ConsultarRemito();
            cr.Show(this);
        }



        private void todosLosPedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerTodosLosPedidos vt = new VerTodosLosPedidos();
            vt.Show(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = label6.Text;
            modificarUsuario mu = new modificarUsuario(usuario);
            mu.Show(this);
        }

        private void modificarCreditoDeUnClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeleccionarClienteModificarCredito scmc = new SeleccionarClienteModificarCredito();
            scmc.Show(this);
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            VerTodosLosPedidos vt = new VerTodosLosPedidos();
            vt.Show(this);
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            ConsultarOrdenF cof = new ConsultarOrdenF();
            cof.Show(this);
        }

    

        private void ordenesDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultarOrdenDeCompra coc = new ConsultarOrdenDeCompra();
            coc.Show(this);
        }

        private void materiaPrimaNUEVOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string area = label5.Text;
            consultarMateriaPrima cm = new consultarMateriaPrima(area);
            cm.Show(this);
        }

        private void configuracionesDelDepositoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigDeposito cd = new ConfigDeposito();
            cd.Show(this);
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            registrarProveedor rp = new registrarProveedor();
            rp.Show(this);
        }

        private void gestionarArticulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArticulosRegistrados ra = new ArticulosRegistrados();
            ra.Show(this);
        }

        private void fabricacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string area = label5.Text;
            Fabricacion f = new Fabricacion(area);
            f.ShowDialog();
        }

        private void materiaPrimaPorArticuloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeleccionarArticuloParaMateriaPrima mpa = new SeleccionarArticuloParaMateriaPrima();
            mpa.Show(this);
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            FacturasRegistradas r = new FacturasRegistradas();
            r.Show(this);
        }


        //BOTON SALIR
        private void button2_Click(object sender, EventArgs e)
        {
            egresoMenu();            
            adaptador.UpdateCommand.Parameters["@fechaEgreso"].Value = fechaEgreso;
            try
            {
                conexion.Open();
                adaptador.UpdateCommand.ExecuteNonQuery();
                //MessageBox.Show("Se Guardo La fecha y hora de salida del sistema", "sistema");
            }
            catch (SqlException excepcion)
            {
                MessageBox.Show(excepcion.ToString());
            }
            finally
            {
                conexion.Close();
            }
            Application.Exit();
        }

      

        private void iconButton2_Click_1(object sender, EventArgs e)
        {
            RegistrarCliente rc = new RegistrarCliente();
            rc.Show();
        }

        private void consultarOrdenesDeFabricacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultarOrdenF cof=new ConsultarOrdenF();
            cof.Show(this);
        }

        private void preciosDeArticulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeleccionarArtModifPrecio sf = new SeleccionarArtModifPrecio();
            sf.Show(this);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            registrarTransportista r = new registrarTransportista();
            r.ShowDialog(this);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            EstadisticaArticuloStock esArt = new EstadisticaArticuloStock();
            esArt.ShowDialog();
        }

        private void reporteClientesMasFrecuentesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EstadisticaClientesVentas aesc = new EstadisticaClientesVentas();
            aesc.ShowDialog();
        }

        private void reporteDeVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EstadisticaVentas esven = new EstadisticaVentas();
            esven.ShowDialog();
        }

        private void reporteDeOrdenesDeFabricacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EstadisticaOrdenesF esof = new EstadisticaOrdenesF();
            esof.ShowDialog();
        }

        private void eliminarUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void ingresosYEgresosDeUsuariosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            consultarIngresoEgreso cie = new consultarIngresoEgreso();
            cie.ShowDialog();
        }

        private void eliminarUsuariosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            eliminarUsuario eu = new eliminarUsuario();
            eu.Show(this);
        }

        private void consultarUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            consultarUsuarios conu = new consultarUsuarios();
            conu.Show(this);
        }

        private void reporteClientesMasFrecuentesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EstadisticaClientesVentas ecv = new EstadisticaClientesVentas();
            ecv.ShowDialog();
        }

        private void reporteDeVentasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EstadisticaVentas esven = new EstadisticaVentas();
            esven.ShowDialog();
        }

        private void reporteArticulosEnStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EstadisticaArticuloStock esArt = new EstadisticaArticuloStock();
            esArt.ShowDialog();
        }

        private void reporteMateriaPrimaEnStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EstadisticaMPstock estmp = new EstadisticaMPstock();
            estmp.ShowDialog();
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            string area = label5.Text;
            consultarMateriaPrima cm = new consultarMateriaPrima(area);
            cm.Show(this);
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            VerStockArticulos st = new VerStockArticulos();
            st.Show(this);
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {

        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            registrarProveedor rp = new registrarProveedor();
            rp.Show(this);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ConsultarOrdenDeCompra coc = new ConsultarOrdenDeCompra();
            coc.Show(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Texto de advertencia", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblDateTime2.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void reporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EstadisticaMPstock estmp = new EstadisticaMPstock();
            estmp.ShowDialog();
        }




        
        private void stockDeArticulosToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            VerStockArticulos st = new VerStockArticulos();
            st.Show(this);
        }


    }
}
