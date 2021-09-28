using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yanez.Evelyn._2E.PrimerParcial
{
    public partial class FormCompras : Form
    {
        private ETipoDeProducto tipoDeProducto;
        public FormCompras(ETipoDeProducto tipoDeProducto)
        {
            InitializeComponent();
            this.tipoDeProducto = tipoDeProducto;
        }

        private void FormCompras_Load(object sender, EventArgs e)
        {
            lvProductos.View = View.LargeIcon;
            lvCarroDeCompras.View = View.List;
            panel1.BackColor = Color.FromArgb(125, Color.Indigo);
            this.cmbTipoDeProducto.DataSource = Enum.GetValues(typeof(ETipoDeProducto));
            this.cmbTipoDeProducto.SelectedItem = this.tipoDeProducto;

            this.ActualizarProductos();
            if (FormEmpleado.cliente is not null)
            {
                this.ActualizarCarro();
            }
            else
            {
                lblCarrito.Visible = false;
                lvCarroDeCompras.Visible = false;
                btnTirarProducto.Visible = false;
            }
        }

        private void ActualizarProductos()
        {

            Dictionary<Producto, string> listaFiltrada = new Dictionary<Producto, string>();
            lvProductos.Clear();
            switch ((ETipoDeProducto)this.cmbTipoDeProducto.SelectedItem)
            {
                case ETipoDeProducto.Alimento:
                    listaFiltrada = PetShop.FiltrarListadoProducto(typeof(Alimento));
                    break;
                case ETipoDeProducto.Cama:
                    listaFiltrada = PetShop.FiltrarListadoProducto(typeof(Cama));
                    break;
                case ETipoDeProducto.Juguete:
                    listaFiltrada = PetShop.FiltrarListadoProducto(typeof(Juguete));
                    break;
                case ETipoDeProducto.ArticuloDeCuidado:
                    listaFiltrada = PetShop.FiltrarListadoProducto(typeof(ArticuloDeFarmacia));
                    break;
            }

            ImageList listaDeImagenes = new ImageList();
            ResourceManager adminDeRecursos = Properties.Resources.ResourceManager;
            int i = 0;
            foreach (KeyValuePair<Producto, string> producto in listaFiltrada)
            {
                Bitmap imagen = (Bitmap)adminDeRecursos.GetObject(producto.Value);
                listaDeImagenes.Images.Add(imagen);
                lvProductos.Items.Add(producto.Key.Id.ToString(), producto.Key.Descripcion, i);
                i++;
            }
            listaDeImagenes.ImageSize = new Size(75, 80);
            lvProductos.LargeImageList = listaDeImagenes;
        }

        private void lvProductos_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2 && lvProductos.SelectedItems.Count == 1)
            {
                lvProductos_MouseDoubleClick(sender, e);
                return;
            }
            else if (lvProductos.SelectedItems.Count > 0 && e.Clicks == 1)
            {
                ListViewItem item = lvProductos.SelectedItems[0];
                DoDragDrop(item.Name, DragDropEffects.Copy);
            }
        }

        private void lbCarrroDeCompras_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
            string strId = (string)e.Data.GetData(DataFormats.Text);

            if (int.TryParse(strId, out int id) && !FormEmpleado.cliente.ValidarProductoEnCanasto(id))
            {
                Producto producto = PetShop.BuscarProducto(id);
                // Se agrega el producto al carro del cliente.
                FormEmpleado.cliente[id] = producto;
                this.ActualizarCarro();
            }
        }

        private void cmbTipoDeProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ActualizarProductos();
        }

        private void lvProductos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (int.TryParse(lvProductos.SelectedItems[0].Name, out int id))
            {
                Producto producto = PetShop.BuscarProducto(id);
                FormDatosProducto frmDatosProducto = new FormDatosProducto(producto);
                this.Visible = false;
                frmDatosProducto.ShowDialog();
                this.Visible = true;
                this.ActualizarCarro();
            }
        }
        private void ActualizarCarro()
        {
            lvCarroDeCompras.Clear();
            if (FormEmpleado.cliente is not null)
            {
                int i = 0;
                foreach (KeyValuePair<Producto, int> producto in FormEmpleado.cliente.Carrito)
                {
                    string datosAMostrar = $"{producto.Value} - {producto.Key.Descripcion} - {producto.Key.Marca}";
                    lvCarroDeCompras.Items.Add(producto.Key.Id.ToString(), datosAMostrar,i);
                    i++;
                }
            }
        }

        private void lvCarroDeCompras_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            string strId = (string)e.Data.GetData(DataFormats.Text);

            if (int.TryParse(strId, out int id) && !FormEmpleado.cliente.ValidarProductoEnCanasto(id))
            {
                Producto producto = PetShop.BuscarProducto(id);
                // Se agrega el producto al carro del cliente.
                FormEmpleado.cliente[id] = producto;
                this.ActualizarCarro();
            }
        }

        private void btnVaciarCarrito_Click(object sender, EventArgs e)
        {
            FormEmpleado.cliente.Carrito.Clear();
            this.ActualizarCarro();
        }

        private void btnTirarProducto_Click(object sender, EventArgs e)
        {
            if (this.lvCarroDeCompras.SelectedItems.Count > 0)
            {
                if (int.TryParse(this.lvCarroDeCompras.SelectedItems[0].Name, out int id))
                {
                    Producto producto = PetShop.BuscarProducto(id);
                    if (producto is not null)
                    {
                        FormEmpleado.cliente.Carrito.Remove(producto);
                        this.ActualizarCarro();
                    }
                }
            }
        }
    }
}
