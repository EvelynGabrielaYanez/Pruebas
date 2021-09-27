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
    public partial class FormFinalizarCompra : Form
    {
        public FormFinalizarCompra()
        {
            InitializeComponent();
        }

        private void btnContinuarCompra_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormFinalizarCompra_Load(object sender, EventArgs e)
        {
            // Se carga el carro de compras

            dgvCarrito.Columns.Add("id", "Número");
            dgvCarrito.Columns.Add("precio","Precio");
            dgvCarrito.Columns.Add("stock","Stock");
            dgvCarrito.Columns.Add("cantidad","Cantidad");
            dgvCarrito.Columns.Add("descripcion","Descripción");
            dgvCarrito.Columns.Add("marca","Marca");
            dgvCarrito.Columns["id"].ReadOnly = true;
            dgvCarrito.Columns["precio"].ReadOnly = true;
            dgvCarrito.Columns["stock"].ReadOnly = true;
            dgvCarrito.Columns["stock"].DefaultCellStyle.Format = "G";
            dgvCarrito.Columns["cantidad"].ReadOnly = false;
            dgvCarrito.Columns["descripcion"].ReadOnly = true;
            dgvCarrito.Columns["marca"].ReadOnly = true;
            foreach (KeyValuePair<Producto,int> parProductoCantida in  FormEmpleado.cliente.Carrito)
            {
                Producto producto = parProductoCantida.Key;
                dgvCarrito.Rows.Add(producto.Id, producto.Precio,producto.Stock, parProductoCantida.Value, producto.Descripcion, producto.Marca);
            }
 
        }

        private void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            _= dgvCarrito.DataSource;

            foreach(DataGridViewRow fila in dgvCarrito.Rows )
            {
                int id = (int)fila.Cells["id"].Value;
                int cantidad = int.Parse(fila.Cells["cantidad"].Value.ToString());

                KeyValuePair<Producto,int> parProductoCantidad = FormEmpleado.cliente.BuscarProductoEnCanasto(id);
                FormEmpleado.cliente.ProductosComprados.Remove(parProductoCantidad.Key);
                FormEmpleado.cliente.ProductosComprados.Add(parProductoCantidad.Key, cantidad);
            }

            if (FormEmpleado.cliente.RealizarCompra())
            {
                MessageBox.Show("¡Compra Finalizada con Éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ocurrio un error al realizar la comrpa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
