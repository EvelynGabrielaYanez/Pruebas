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
    public partial class FrmPerfilEmpleado : Form
    {
        int contadorTimer;
        public FrmPerfilEmpleado()
        {
            InitializeComponent();
        }

        private void FrmPerfilEmpleado_Load(object sender, EventArgs e)
        {
            // Se inicializa el timer
            this.contadorTimer = 0;
            this.tmrPerfilEmpleado.Start();

            // Se configura la apariencia de los paneles
            pnlEmpleado.BackColor = Color.FromArgb(125, Color.Indigo);
            pnlDatos.BackColor = Color.FromArgb(125, Color.Silver);

            // Se configura la apariencia de los botones
            btnVolver.BackColor = Color.FromArgb(171, 143, 192);

            // Se configura la imgen segun tipo de empleado
            if (FrmEmpleado.empleado.GetType() == typeof(Empleado))
                picImagenUsuario.Image = Properties.Resources.imgEmpleado;
            else
                picImagenUsuario.Image = Properties.Resources.imgAdmin;

            // Se cargan los datos del empleado
            lblValorApellido.Text = FrmEmpleado.empleado.Apellido != string.Empty ? FrmEmpleado.empleado.Apellido : "No registrado";
            lblValorNombre.Text = FrmEmpleado.empleado.Nombre != string.Empty ? FrmEmpleado.empleado.Nombre  : "No registrado";
            lblValorNombreUsuario.Text = FrmEmpleado.empleado.NombreUsuario;
            lblValorDNI.Text = FrmEmpleado.empleado.Dni.ToString();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tmrPerfilEmpleado_Tick(object sender, EventArgs e)
        {
            this.contadorTimer++;
            if (this.contadorTimer == 100)
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

        private void FrmPerfilEmpleado_MouseMove(object sender, MouseEventArgs e)
        {
            this.contadorTimer = 0;
            this.tmrPerfilEmpleado.Start();
        }

        private void FrmPerfilEmpleado_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tmrPerfilEmpleado.Stop();
        }
    }
}
