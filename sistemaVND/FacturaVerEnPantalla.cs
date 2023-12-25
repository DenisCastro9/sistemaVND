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
    public partial class FacturaVerEnPantalla : Form
    {
        int tipoFactura;
        int condicionDeVenta;
        int condIva;
        int numeroRemito;
        int numeroPedido;
        int cantidad = 0;
        private SqlConnection conexion = new SqlConnection("data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true");
        public FacturaVerEnPantalla(int tipoFactura, int nroFactura, string fecha, int condDeVenta, int alicuotaIva, int idP, int idR, double importeNeto, double importeIva, double importeTributos, double importeFinal)
        {
            this.tipoFactura =0;
            this.condicionDeVenta = 0;
            this.condIva =0;
            this.numeroRemito = 0;
            this.numeroPedido = 0;
            InitializeComponent();
            this.tipoFactura = tipoFactura;
            this.condicionDeVenta = condDeVenta;
            this.condIva = alicuotaIva;
            this.numeroRemito = idR;
            this.numeroPedido = idP;
            label7.Text = Convert.ToString(nroFactura);
            //label11.Text = Convert.ToShortDateString().ToString(fecha);
            label11.Text = Convert.ToString(fecha);
            label35.Text = Convert.ToString(importeNeto);
            label34.Text = Convert.ToString(importeIva);
            label45.Text = Convert.ToString(importeTributos);
            label47.Text = Convert.ToString(importeFinal);




            cargarTipoFactura();
            cargarCondicionDeVenta();
            cargarIva();
            cargarNroRemitoNroPedidoYCliente();
            cargarDescripcionDelPedido();
        }

       

        private void cargarTipoFactura()
        {
            if (tipoFactura == 0) { label56.Text = "A"; }
            if (tipoFactura == 1) { label56.Text = "B"; }
        }


        private void cargarCondicionDeVenta()
        {
            if (condicionDeVenta == 0) { label30.Text = "Contado - Clausula Dolar"; }
            if (condicionDeVenta == 1) { label30.Text = "Tarjeta de credito"; }
            if (condicionDeVenta == 2) { label30.Text = "Cuenta corriente"; }
            if (condicionDeVenta == 3) { label30.Text = "Cheque"; }
            if (condicionDeVenta == 4) { label30.Text = "Otro"; }
        }

        private void cargarIva()
        {
            if (condIva == 0) { label55.Text = "Exento"; }
            if (condIva == 1) { label55.Text = "10,5"; }
            if (condIva == 2) { label55.Text = "21"; }
            if (condIva == 3) { label55.Text = "27"; }
        }


        private void cargarNroRemitoNroPedidoYCliente()
        {
            conexion.Open();
            string sql = "   select r.numero as NroRemito, P.numero as NroPedido, c.razonSocial, c.id, c.calle, c.altura, c.barrio, l.nombreLocalidad, pr.nombreProvincia, c.condicionIva, c.ingresosBrutos, c.cuit, p.totalPares  from remito as r join pedido as p on r.idPedido = p.numero join cliente as c on p.idCliente = c.id  join localidad as l on c.idLocalidad = l.idLocalidad  join provincia as pr on c.idProvincia = pr.idprovincia  where r.numero = @nroR ";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nroR", numeroRemito);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                label33.Text = registro["NroRemito"].ToString();
                label49.Text = registro["NroPedido"].ToString();
                label25.Text = registro["razonSocial"].ToString();
                label32.Text = registro["id"].ToString();

                label26.Text = registro["calle"].ToString() + " " + registro["altura"].ToString() + " " + registro["barrio"].ToString() + " " + registro["nombreLocalidad"].ToString() + " " + registro["nombreProvincia"].ToString();

                label27.Text = registro["condicionIva"].ToString();
                label23.Text = registro["ingresosBrutos"].ToString();
                label24.Text = registro["cuit"].ToString();
                label50.Text = registro["totalPares"].ToString();


            }
            else
            {
                MessageBox.Show("Éste pedido no tiene datos asignados", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            conexion.Close();
        }



        private void cargarDescripcionDelPedido()
        {
            contarRegistros();
            conexion.Open();
            string sql = "   select a.nombre as articulo, a.talle, d.cantidad, a.precioUnitario from detalleDePedido as d join articulo as a on d.idArticulo = a.idarticulo join pedido as p on p.numero = d.numero where p.numero = @nroPedido";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nroPedido", numeroPedido);
            SqlDataReader registro = comando.ExecuteReader();
            for (int i = 0; i < cantidad; i++)
            {
                if (registro.Read())

                {
                    string nombreArticulo = registro["articulo"].ToString();
                    string talleArticulo = registro["talle"].ToString();
                    int cantidadArticulo = Convert.ToInt32(registro["cantidad"]);
                    decimal precioUnitario = Convert.ToDecimal(registro["precioUnitario"]);

                    decimal importe = cantidadArticulo * precioUnitario; // Calcula el importe para este artículo

                    label51.Text +=  $"{nombreArticulo} {talleArticulo} / {cantidadArticulo}\n";
                    label52.Text += $"$ {precioUnitario}\n";
                    label53.Text += $"$ {importe}\n";
                }

                else
                {
                    MessageBox.Show("Éste pedido no tiene datos asignados", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            conexion.Close();
        }



        private void contarRegistros()
        {
            conexion.Open();
            string sql = " select count(*) as cantidad from detalleDePedido join pedido on detalleDePedido.numero=pedido.numero where pedido.numero = @nroPed";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nroPed", numeroPedido);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                cantidad = Convert.ToInt32(registro["cantidad"].ToString());
            }
            conexion.Close();
        }


        //EL PEDIDO PASA A ESTADO FACTURADO: 5
        private void cambiarEstadoDePedido()
        {
            conexion.Open();
            string sql = " update pedido set idEstadoPedido=5 where numero=@idPedido";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@idPedido", numeroPedido); //id del pedido
            comando.ExecuteNonQuery();
            conexion.Close();
        }



      

        //REGISTRAR FACTURA ES ACA...
        private void button1_Click(object sender, EventArgs e)
        {

            String query = "  insert into factura (numero, fecha, montoTotal, tipoFactura, porcentajeAlicuotaIva, importeIva, importeTributos, idRemito, idCliente, idDetalleFactura, idFormaDePago) values (@numero, @fecha, @montoTotal, @tipoFactura, @porcentajeAlicuotaIva, @importeIva, @importeTributos, @idRemito, @idCliente, @idDetalleFactura, @idFormaDePago)";
            SqlCommand command = new SqlCommand(query, conexion);
            command.Parameters.AddWithValue("@numero", Convert.ToInt32(label7.Text));//numero de factura
            command.Parameters.AddWithValue("@fecha", DateTime.Now.Date);//fecha de factura
            command.Parameters.AddWithValue("@montoTotal", Convert.ToDecimal(label47.Text));//monto final VER SI SE GUARDA BIEN ASI O CAMBIAR A DECIMAL O DOUBLE

            if (tipoFactura == 0)
            { command.Parameters.Add("@tipoFactura", SqlDbType.VarChar).Value = 'A'; }
            if (tipoFactura == 1)
            { command.Parameters.Add("@tipoFactura", SqlDbType.VarChar).Value = 'B'; }

            if (condIva == 0)
            { command.Parameters.Add("@porcentajeAlicuotaIva", SqlDbType.Int).Value = 0; }
            if (condIva == 1)
            { command.Parameters.Add("@porcentajeAlicuotaIva", SqlDbType.Int).Value = 10; }
            if (condIva == 2)
            { command.Parameters.Add("@porcentajeAlicuotaIva", SqlDbType.Int).Value = 21; }
            if (condIva == 3)
            { command.Parameters.Add("@porcentajeAlicuotaIva", SqlDbType.Int).Value = 27; }

            command.Parameters.AddWithValue("@importeIva", Convert.ToDecimal(label34.Text));
            command.Parameters.AddWithValue("@importeTributos", Convert.ToDecimal(label45.Text));
            command.Parameters.AddWithValue("@idRemito", numeroRemito);
            command.Parameters.AddWithValue("@idCliente", Convert.ToInt32(label32.Text));//id del cliente
            command.Parameters.AddWithValue("@idDetalleFactura", Convert.ToInt32(label7.Text));//COMO AGREGO EL DETALLE DE FACTURA, ES EL MISMO NRO
            if (condicionDeVenta == 0)
            { command.Parameters.Add("@idFormaDePago", SqlDbType.Int).Value = 1; }
            if (condicionDeVenta == 1)
            { command.Parameters.Add("@idFormaDePago", SqlDbType.Int).Value = 2; }
            if (condicionDeVenta == 2)
            { command.Parameters.Add("@idFormaDePago", SqlDbType.Int).Value = 3; }
            if (condicionDeVenta == 3)
            { command.Parameters.Add("@idFormaDePago", SqlDbType.Int).Value = 4; }
            if (condicionDeVenta == 4)
            { command.Parameters.Add("@idFormaDePago", SqlDbType.Int).Value = 5; }

            conexion.Open();

            int result = command.ExecuteNonQuery();
            if (result < 0)
            {
                Console.WriteLine("No se pudo crear la factura");
            }
            else
            {
                query = "  insert into detalleFactura (numero, idPedido) values (@numero, @idPedido)"; //inserto en la tabla detalle factura
                command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@numero", Convert.ToInt32(label7.Text));
                command.Parameters.AddWithValue("@idPedido", numeroPedido);
                int result2 = command.ExecuteNonQuery();
                if (result2 < 0)
                {
                    Console.WriteLine("No se pudo crear el Detalle de la factura");
                }
                else
                {
                    MessageBox.Show("Factura registrada", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conexion.Close();
                    cambiarEstadoDePedido();

                    FacturasRegistradas f = new FacturasRegistradas();
                    this.Close();

                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

      
    }

}
