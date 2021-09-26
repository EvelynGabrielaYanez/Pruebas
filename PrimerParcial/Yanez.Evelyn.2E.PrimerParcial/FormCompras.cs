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
            ImageList imgs = new ImageList();
            int i = 0;
            foreach (KeyValuePair<Producto,string> producto in PetShop.productos)
            {
                //imgs.Images.Add(Image.FromFile(producto.Value));
                //string[] fila = {i.ToString() ,producto.Key.Descripcion, producto.Key.Marca, producto.Key.Id.ToString()};
                //var nuevoItem = new ListViewItem(fila);
                //lvProductos.Items.Add(nuevoItem);
                lvProductos.Items.Add(producto.Key.Id.ToString(),producto.Key.Descripcion, i);
                i++;
            }
 
            //lvProductos.SmallImageList = imgs;
            /*
            //ImageList imgs = new ImageList();
            Image imagen = Image.FromFile("");
            imgs.Images.Add(Image.FromFile(""));

            lvProductos.SmallImageList = imgs;*/
        }

        private void lvProductos_MouseDown(object sender, MouseEventArgs e)
        {
            if (lvProductos.SelectedItems.Count > 0)
            {
                ListViewItem item = lvProductos.SelectedItems[0];
                //MessageBox.Show(item.Text);
                DragDropEffects pruebaDragAnd = DoDragDrop(item.Name, DragDropEffects.All);
            }
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
            string id = (string)e.Data.GetData(DataFormats.Text);
            Producto producto = PetShop.BuscarProducto(int.Parse(id));
            ListBox lst = new ListBox();
            listBox1.Items.Add( producto.Id + "-" + producto.Descripcion);
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {

            ListBox lst = sender as ListBox;

            // Get the ListBox item data.
            DragItem drag_item = (DragItem)e.Data.GetData(typeof(DragItem));


            ListBox lstp = (ListBox)sender;
            _ = lstp.Items;
            /*
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string str = (string)e.Data.GetData(
                    DataFormats.StringFormat);

                listBox1.Items.Add(str);
            }*/
        }

    }
    public class DragItem
    {
        public ListBox Client;
        public int Index;
        public object Item;

        public DragItem(ListBox client, int index, object item)
        {
            Client = client;
            Index = index;
            Item = item;
        }
    }
}
