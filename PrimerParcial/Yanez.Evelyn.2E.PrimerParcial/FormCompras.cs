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
        ErrorProvider alertaInformacion;
        ErrorProvider erroresInformacion;
        ETipoDeProducto tipoDeProducto;
        int contadorTimer;

        public FormCompras(ETipoDeProducto tipoDeProducto)
        {
            InitializeComponent();
            this.tipoDeProducto = tipoDeProducto;
            alertaInformacion = new ErrorProvider();
            erroresInformacion = new ErrorProvider();
            contadorTimer = 0;
        }

        private void FormCompras_Load(object sender, EventArgs e)
        {
            lvProductos.View = View.LargeIcon;
            //lvCarroDeCompras.View = View.List;
            panel1.BackColor = Color.FromArgb(125, Color.Indigo);
            this.cmbTipoDeProducto.DataSource = Enum.GetValues(typeof(ETipoDeProducto));
            this.cmbTipoDeProducto.SelectedItem = this.tipoDeProducto;

            // Se carga la informacion para utilizar el carro.
            alertaInformacion.Icon = new Icon(Properties.Resources.icoInformacion, 40, 40);
            alertaInformacion.SetError(lblCarrito,"Arrastrar productos para agregar al carro o hacer doble clic para ver el detalle.");

            this.ActualizarProductos();
            this.ActualizarCarro();
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
                    listaFiltrada = PetShop.FiltrarListadoProducto(typeof(ArticuloDeCuidado));
                    break;
                case ETipoDeProducto.Farmacia:
                    listaFiltrada = PetShop.FiltrarListadoProducto(typeof(ArticuloDeFarmacia));
                    break;
                case ETipoDeProducto.Limpieza:
                    listaFiltrada = PetShop.FiltrarListadoProducto(typeof(Limpieza));
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
            erroresInformacion.SetError(lvProductos, "");
            if (e.Clicks == 2 && lvProductos.SelectedItems.Count == 1)
            {
                lvProductos_MouseDoubleClick(sender, e);
            }
            else if (lvProductos.SelectedItems.Count > 0 && e.Clicks == 1)
            {
                ListViewItem item = lvProductos.SelectedItems[0];
                DoDragDrop(item.Name, DragDropEffects.Copy);
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
            if (FrmEmpleado.cliente is not null)
            {
                int i = 0;
                foreach (KeyValuePair<Producto, int> producto in FrmEmpleado.cliente.Carrito)
                {
                    string datosAMostrar = $"{producto.Value} - {(string)producto.Key}";
                    lvCarroDeCompras.Items.Add(producto.Key.Id.ToString(), datosAMostrar,i);
                    i++;
                }
            }
        }

        private void lvCarroDeCompras_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            string strId = (string)e.Data.GetData(DataFormats.Text);

            if (int.TryParse(strId, out int id) && !FrmEmpleado.cliente.ValidarProductoEnCanasto(id))
            {
                Producto producto = PetShop.BuscarProducto(id);
                if (producto.Stock > 0)
                {
                    // Se agrega el producto al carro del cliente.
                    FrmEmpleado.cliente[id] = producto;
                    this.ActualizarCarro();
                }
                else
                {
                    erroresInformacion.SetError(lvProductos,"No se pudo agregar el producto. No tenemos stock.");
                }
            }
            else
            {
                erroresInformacion.SetError(lvProductos, "El producto no se pudo agregar, ya se encontraba en el canasto.");
            }
        }

        private void btnVaciarCarrito_Click(object sender, EventArgs e)
        {
            FrmEmpleado.cliente.Carrito.Clear();
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
                        FrmEmpleado.cliente.Carrito.Remove(producto);
                        this.ActualizarCarro();
                    }
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void FormCompras_MouseMove(object sender, MouseEventArgs e)
        {
            this.contadorTimer = 0;
            this.tmrCerrarSesion.Start();
        }

        private void FormCompras_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.tmrCerrarSesion.Stop();
        }
    }
}
