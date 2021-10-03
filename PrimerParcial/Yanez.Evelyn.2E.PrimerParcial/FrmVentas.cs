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
    public partial class FrmVentas : Form
    {
        int contadorTimer;
        public FrmVentas()
        {
            InitializeComponent();
            this.contadorTimer = 0;
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            // inicio el timer
            this.tmrCerrarSesion.Start();

            // Se configuran los paneles
            pnlFiltros.BackColor = Color.FromArgb(125, Color.Indigo);
            // Se configura la apariencia de los botones
            btnFiltrar.BackColor = Color.FromArgb(171, 143, 192);
            btnQuitarFiltro.BackColor = Color.FromArgb(171, 143, 192);

            // Se confgiura los campos de filtro
            chbFiltroPorFecha.Checked = false;
            this.dtpFechaDeNegocio.Enabled = false;
            // Se carga información en dataGridVew
            this.ActualizarDataGridView();
        }
        private void ActualizarDataGridView()
        {
            dgvVentas.DataSource = PetShop.Ventas;
            lblValorRecaudaciónTotal.Text = PetShop.CalcularRecaudacionTotal().ToString();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            DateTime fechaDeNegocio = dtpFechaDeNegocio.Value;
            //Filtra y actualiza el dataGridView
            dgvVentas.DataSource = PetShop.Ventas.Where(venta => {
                bool respuesta = true;
                if (chbFiltroPorFecha.Checked)
                    respuesta &= venta.FechaDeNegocio.Date == fechaDeNegocio.Date;
                if (int.TryParse(txtCliente.Text, out int dniCliente))
                    respuesta &= venta.ClienteDNI == dniCliente;
                if (int.TryParse(txtEmpleado.Text, out int dniEmpleado))
                    respuesta &= venta.EmpleadoDNI == dniEmpleado;

                return respuesta;
            }).ToList();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<Venta> datosFiltrados = PetShop.Ventas.ToList().Where(venta => {
                bool respuesta = true;
                if (int.TryParse(txtBusqueda.Text, out int id))
                    respuesta &= venta.Id == id;

                return respuesta;
            }).ToList();
            dgvVentas.DataSource = datosFiltrados;
        }

        private void chbFiltroPorFecha_CheckStateChanged(object sender, EventArgs e)
        {
            if (chbFiltroPorFecha.Checked)
                this.dtpFechaDeNegocio.Enabled = true;
            else
                this.dtpFechaDeNegocio.Enabled = false;
        }

        private void btnQuitarFiltro_Click(object sender, EventArgs e)
        {
            this.txtCliente.Text = string.Empty;
            this.txtEmpleado.Text = string.Empty;
            chbFiltroPorFecha.Checked = false;
            this.dtpFechaDeNegocio.Enabled = false;
            this.ActualizarDataGridView();
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

        private void FrmVentas_MouseMove(object sender, MouseEventArgs e)
        {
            this.contadorTimer = 0;
            this.tmrCerrarSesion.Start();
        }

        private void FrmVentas_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tmrCerrarSesion.Stop();
        }
    }
}
