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
    public partial class FrmCargarSaldo : Form
    {
        Cliente cliente;
        int contadorTimer;
        public FrmCargarSaldo(Cliente cliente)
        {
            InitializeComponent();
            this.cliente = cliente;
            nudMontoIngresado.Maximum = decimal.MaxValue;
            nudMontoIngresado.Minimum = 1;
            this.contadorTimer = 0;
        }

        private void FrmCargarSaldo_Load(object sender, EventArgs e)
        {
            this.tmrCerrarSesion.Start();
            this.pnlFondo.BackColor = Color.FromArgb(100, Color.Silver);
            this.ConfigurarVistas();
        }
        public void ConfigurarVistas()
        {
            this.lblValorApellido.Text = this.cliente.Apellido;
            this.lblValorNombre.Text = this.cliente.Nombre;
            this.lblValorDni.Text = this.cliente.Dni.ToString();
            this.lblValorSaldoActual.Text = this.cliente.Saldo.ToString();
        }
        private void btnCargarSaldo_Click(object sender, EventArgs e)
        {
            FrmEmpleado.cliente.CargarSaldo((double)nudMontoIngresado.Value);
            this.Close();
        }

        private void nudMontoIngresado_ValueChanged(object sender, EventArgs e)
        {
            this.CalcularTotal();
        }

        private void CalcularTotal()
        {
            lblValorSaldoFinal.Text = (this.cliente.Saldo + (double)this.nudMontoIngresado.Value).ToString();
        }

        private void tmrCerrarSesion_Tick(object sender, EventArgs e)
        {
            this.contadorTimer = 0;
            this.tmrCerrarSesion.Start();
        }

        private void FrmCargarSaldo_MouseMove(object sender, MouseEventArgs e)
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

        private void FrmCargarSaldo_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tmrCerrarSesion.Stop();
        }
    }
}
