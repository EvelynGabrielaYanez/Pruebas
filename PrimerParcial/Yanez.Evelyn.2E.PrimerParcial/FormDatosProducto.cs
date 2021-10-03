using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yanez.Evelyn._2E.PrimerParcial
{
    public partial class FormDatosProducto : Form
    {
        Producto producto;
        int contadorTimer;
        public FormDatosProducto(Producto producto)
        {
            InitializeComponent();
            this.producto = producto;
            this.contadorTimer = 0;
        }

        private void FormDatosProducto_Load(object sender, EventArgs e)
        {
            // Inicio el contador
            this.tmrCerrarSesion.Start();

            // Se configura la apariencia
            this.pnlCarro.BackColor = Color.FromArgb(125, Color.Indigo);
            this.pnlDetalleProducto.BackColor = Color.FromArgb(125, Color.Silver);
            this.btnAgregarQuitarCarro.BackColor = Color.FromArgb(125, Color.Silver);

            // Se completan los campos
            this.lblValorDescripcion.Text = this.producto.Descripcion;
            this.lblValorMarca.Text = this.producto.Marca;
            this.lblValorTipoProducto.Text = this.producto.ObtenerTipoDeProducto();
            this.lblValorProveedor.Text = this.producto.Proveedor;
            this.lblValorNumero.Text = this.producto.Id.ToString();
            this.lblValorPrecio.Text = $"$ {this.producto.Precio}";
            this.lblValorStock.Text = this.producto.Stock.ToString();
            this.nudCantidad.Minimum = 0;
            this.nudCantidad.Maximum = decimal.MaxValue;

            this.CalcularPrecioTotal();

            this.ActualizarVista();

        }
        private void CalcularPrecioTotal()
        {
            lblValorTotal.Text = $"$ {(double)nudCantidad.Value * this.producto.Precio}";
        }

        private void btnAgregarCarro_Click(object sender, EventArgs e)
        {
            if (FrmEmpleado.cliente.ValidarProductoEnCanasto(this.producto.Id))
            {
                FrmEmpleado.cliente.Carrito.Remove(this.producto);
            }
            else
            {
                if(this.producto.Stock > 0)
                    FrmEmpleado.cliente.Carrito.Add(this.producto, (int)nudCantidad.Value);
            }
            this.ActualizarVista();
        }

        private void btnActualizarCantidad_Click(object sender, EventArgs e)
        {
            if (FrmEmpleado.cliente.ValidarProductoEnCanasto(this.producto.Id) && this.producto.Stock > 0 )
            {
                FrmEmpleado.cliente.Carrito.Remove(this.producto);
                FrmEmpleado.cliente.Carrito.Add(this.producto, (int)nudCantidad.Value);
            }
            this.ActualizarVista();
        }

        private void ActualizarVista()
        {
            this.lbCarroDeCompras.Items.Clear();
            foreach (KeyValuePair<Producto, int> producto in FrmEmpleado.cliente.Carrito)
            {
                this.lbCarroDeCompras.Items.Add($"{producto.Value} - {(string)producto.Key}");
            }
            if (FrmEmpleado.cliente.Carrito.TryGetValue(this.producto, out int cantidad))
                nudCantidad.Value = cantidad;
            else
                nudCantidad.Value = 1;
            if (FrmEmpleado.cliente.ValidarProductoEnCanasto(this.producto.Id))
            {
                btnAgregarQuitarCarro.Text = "Quitar del Carro";
                btnActualizarCantidad.Visible = true;
            }
            else
            {
                btnAgregarQuitarCarro.Text = "Agregar al Carro";
                btnActualizarCantidad.Visible = false;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nudCantidad_ValueChanged(object sender, EventArgs e)
        {
            this.ValidarYCalcular();
        }
        private void ValidarYCalcular() 
        {
            if (nudCantidad.Value > this.producto.Stock)
                nudCantidad.Value = this.producto.Stock;

            // Calcula el total
            this.CalcularPrecioTotal();
        }

        private void FormDatosProducto_MouseMove(object sender, MouseEventArgs e)
        {
            this.contadorTimer = 0;
            this.tmrCerrarSesion.Start();
        }

        private void tmrCerrarSesion_Tick(object sender, EventArgs e)
        {
            this.contadorTimer++;
            if (contadorTimer == 100)
            {
                FrmEmpleado.ignorarFormClosing = true;
                FormCollection formulariosDeLaApp = Application.OpenForms;
                foreach (Form formulario in formulariosDeLaApp)
                {
                    if (formulario.Name != "FrmInicioSesion")
                        formulario.Close();
                }
                MessageBox.Show("Se cerro la sesión por inactividad", "Sesión Finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FormDatosProducto_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tmrCerrarSesion.Stop();
        }
    }
}
