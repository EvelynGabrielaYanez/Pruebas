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
    public partial class FormCompras : Form
    {
        private ETipoDeProducto tipoDeProducto;
        public FormCompras(ETipoDeProducto tipoDeProducto)
        {
            InitializeComponent();
            this.tipoDeProducto = (ETipoDeProducto)tipoDeProducto;
        }

        private void FormCompras_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(125, Color.Indigo);
            this.cmbTipoDeProducto.DataSource = Enum.GetValues(typeof(ETipoDeProducto));
            this.cmbTipoDeProducto.SelectedItem = this.tipoDeProducto;

            this.ACtualizarProductos();
            this.lbCarroDeCompras.HorizontalScrollbar = true;
            if (FormEmpleado.cliente is not null)
            {
                foreach (KeyValuePair<Producto, int> producto in FormEmpleado.cliente.Carrito)
                {
                    this.lbCarroDeCompras.Items.Add($"{producto.Key.Descripcion} - {producto.Key.Marca}");
                }
            }
            else
            {
                lblCarrito.AllowDrop = false;// No funciono, lo tengo que arreglar
                lvProductos.AllowDrop = false;
            }
            // Carga los productos al listbox del carro
        }

        private void ACtualizarProductos()
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
                    listaFiltrada = PetShop.FiltrarListadoProducto(typeof(ArticuloDeCuidado));
                    break;
            }
            //ImageList imgs = new ImageList();
            int i = 0;
            foreach (KeyValuePair<Producto, string> producto in listaFiltrada)
            {
                //imgs.Images.Add(Image.FromFile(producto.Value));
                //string[] fila = {i.ToString() ,producto.Key.Descripcion, producto.Key.Marca, producto.Key.Id.ToString()};
                //var nuevoItem = new ListViewItem(fila);
                //lvProductos.Items.Add(nuevoItem);
                lvProductos.Items.Add(producto.Key.Id.ToString(), producto.Key.Descripcion, i);
                i++;
            }


        }

        private void lvProductos_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2 && lvProductos.SelectedItems.Count == 1)
            {
                lvProductos_MouseDoubleClick(sender, e);
                return;
            }
            if (lvProductos.SelectedItems.Count > 0)
            {
                ListViewItem item = lvProductos.SelectedItems[0];
                DoDragDrop(item.Name, DragDropEffects.All);
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
                // Se agrega a la lista.
                this.lbCarroDeCompras.Items.Add($"{producto.Descripcion} - {producto.Marca}");
            }
        }

        private void cmbTipoDeProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ACtualizarProductos();
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
            }


        }
    }
}
