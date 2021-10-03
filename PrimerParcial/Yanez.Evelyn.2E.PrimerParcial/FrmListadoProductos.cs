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
    public partial class FrmListadoProductos : Form
    {
        List<Producto> fuenteDeInformacion;
        int contadorTimer;
        public FrmListadoProductos()
        {
            InitializeComponent();
            fuenteDeInformacion = new List<Producto>();
            this.contadorTimer = 0;
        }

        private void FrmListadoProductos_Load(object sender, EventArgs e)
        {
            // iniciar el temporizador
            this.tmrCerrarSesion.Start();

            this.panelTipos.BackColor = Color.FromArgb(125, Color.Indigo);
            this.cmbTipoDeProducto.DataSource = Enum.GetValues(typeof(ETipoDeProducto));
            this.ActualizarDataGrid();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<Producto> datosFiltrados = fuenteDeInformacion.ToList().Where(usuario =>
            {
                bool respuesta = true;
                if (int.TryParse(txtBuscar.Text, out int id))
                    respuesta &= usuario.Id == id;
                return respuesta;
            }).ToList();

            switch ((ETipoDeProducto)cmbTipoDeProducto.SelectedItem)
            {
                case ETipoDeProducto.Juguete:
                    dgProductos.DataSource = datosFiltrados.Cast<Juguete>().ToList();
                    break;
                case ETipoDeProducto.Cama:
                    dgProductos.DataSource = datosFiltrados.Cast<Cama>().ToList();
                    break;
                case ETipoDeProducto.Alimento:
                    dgProductos.DataSource = datosFiltrados.Cast<Alimento>().ToList();
                    break;
                case ETipoDeProducto.ArticuloDeCuidado:
                    dgProductos.DataSource = datosFiltrados.Cast<ArticuloDeCuidado>().ToList();
                    break;
                case ETipoDeProducto.Farmacia:
                    dgProductos.DataSource = datosFiltrados.Cast<Limpieza>().ToList();
                    break;
                case ETipoDeProducto.Limpieza:
                    dgProductos.DataSource = datosFiltrados.Cast<ArticuloDeFarmacia>().ToList();
                    break;
            }
        }
        private void ActualizarDataGrid()
        {
            // Cargo la fuente
            switch ((ETipoDeProducto)cmbTipoDeProducto.SelectedItem)
            {
                case ETipoDeProducto.Cama:
                    fuenteDeInformacion = PetShop.FiltrarListadoProducto(typeof(Cama)).Keys.ToList();
                    this.dgProductos.DataSource = fuenteDeInformacion.Cast<Cama>().ToList();
                    break;
                case ETipoDeProducto.Alimento:
                    fuenteDeInformacion = PetShop.FiltrarListadoProducto(typeof(Alimento)).Keys.ToList();
                    this.dgProductos.DataSource = fuenteDeInformacion.Cast<Alimento>().ToList();
                    break;
                case ETipoDeProducto.Juguete:
                    fuenteDeInformacion = PetShop.FiltrarListadoProducto(typeof(Juguete)).Keys.ToList();
                    this.dgProductos.DataSource = fuenteDeInformacion.Cast<Juguete>().ToList();
                    break;
                case ETipoDeProducto.ArticuloDeCuidado:
                    fuenteDeInformacion = PetShop.FiltrarListadoProducto(typeof(ArticuloDeCuidado)).Keys.ToList();
                    this.dgProductos.DataSource = fuenteDeInformacion.Cast<ArticuloDeCuidado>().ToList();
                    break;
                case ETipoDeProducto.Farmacia:
                    fuenteDeInformacion = PetShop.FiltrarListadoProducto(typeof(ArticuloDeFarmacia)).Keys.ToList();
                    this.dgProductos.DataSource = fuenteDeInformacion.Cast<ArticuloDeFarmacia>().ToList();
                    break;
                case ETipoDeProducto.Limpieza:
                    fuenteDeInformacion = PetShop.FiltrarListadoProducto(typeof(Limpieza)).Keys.ToList();
                    this.dgProductos.DataSource = fuenteDeInformacion.Cast<Limpieza>().ToList();
                    break;
            }
        }

        private void cmbTipoDeProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ActualizarDataGrid();
        }

        private void tmrCerrarSesion_Tick(object sender, EventArgs e)
        {
            contadorTimer++;
            if (contadorTimer == 100)
            {
                FormCollection formulariosDeLaApp = Application.OpenForms;
                foreach (Form formulario in formulariosDeLaApp)
                {
                    if (formulario.Name != "FrmInicioSesion")
                        formulario.Close();
                }
                MessageBox.Show("Se cerro la sesión por inactividad", "Sesión Finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmListadoProductos_MouseMove(object sender, MouseEventArgs e)
        {
            this.contadorTimer = 0;
            this.tmrCerrarSesion.Start();
        }

        private void FrmListadoProductos_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tmrCerrarSesion.Stop();
        }
    }
}
