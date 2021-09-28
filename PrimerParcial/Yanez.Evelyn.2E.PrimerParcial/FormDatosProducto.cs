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
        public FormDatosProducto(Producto producto)
        {
            InitializeComponent();
            this.producto = producto;
        }

        private void FormDatosProducto_Load(object sender, EventArgs e)
        {
            // Se configura la apariencia
            pnlCarro.BackColor = Color.FromArgb(125, Color.Indigo);
            pnlDetalleProducto.BackColor = Color.FromArgb(125, Color.Silver);
            btnAgregarQuitarCarro.BackColor = Color.FromArgb(125, Color.Silver);

            // Se completan los campos
            lblValorDescripcion.Text = this.producto.Descripcion;
            lblValorMarca.Text = this.producto.Marca;
            lblValorTipoProducto.Text = this.producto.ObtenerTipoDeProducto();
            lblValorProveedor.Text = this.producto.Proveedor;
            lblValorNumero.Text = this.producto.Id.ToString();
            lblValorPrecio.Text = $"$ {this.producto.Precio}";
            lblValorStock.Text = this.producto.Stock.ToString();


            this.CalcularLabelTotal();

            if (FormEmpleado.cliente is not null)
            {
                this.ActualizarVista();
            }
            else
            {
                btnAgregarQuitarCarro.Enabled = false;
                lblCarrito.Enabled = false;
                lblCarrito.Visible = false;
                lbCarroDeCompras.Visible = false;
            }
        }

        private void nudCantidad_ValueChanged(object sender, EventArgs e)
        {
            this.CalcularLabelTotal();
        }
        private void CalcularLabelTotal()
        {
            lblValorTotal.Text = $"$ {(double)nudCantidad.Value * this.producto.Precio}";
        }

        private void btnAgregarCarro_Click(object sender, EventArgs e)
        {
            if (FormEmpleado.cliente.ValidarProductoEnCanasto(this.producto.Id))
            {
                FormEmpleado.cliente.Carrito.Remove(this.producto);
            }
            else
            {
                FormEmpleado.cliente.Carrito.Add(this.producto, (int)nudCantidad.Value);
            }
            this.ActualizarVista();
        }

        private void btnActualizarCantidad_Click(object sender, EventArgs e)
        {
            if (FormEmpleado.cliente.ValidarProductoEnCanasto(this.producto.Id))
            {
                FormEmpleado.cliente.Carrito.Remove(this.producto);
                FormEmpleado.cliente.Carrito.Add(this.producto, (int)nudCantidad.Value);
            }
            this.ActualizarVista();
        }

        private void ActualizarVista()
        {
            this.lbCarroDeCompras.Items.Clear();
            foreach (KeyValuePair<Producto, int> producto in FormEmpleado.cliente.Carrito)
            {
                this.lbCarroDeCompras.Items.Add($"{producto.Value} - {producto.Key.Descripcion} - {producto.Key.Marca}");
            }
            if (FormEmpleado.cliente.ProductosComprados.TryGetValue(this.producto, out int cantidad))
                nudCantidad.Value = cantidad;
            else
                nudCantidad.Value = 0;
            if (FormEmpleado.cliente.ValidarProductoEnCanasto(this.producto.Id))
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
    }
}
