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
    public partial class FormAgregarUsuario : Form
    {
        protected ErrorProvider errorProvider;
        int contadorTimer;
        public FormAgregarUsuario()
        {
            InitializeComponent();
            errorProvider = new ErrorProvider();
            DialogResult = DialogResult.Cancel;
            this.contadorTimer = 0;
        }

        protected virtual void btnAceptar_Click(object sender, EventArgs e)
        {
            if(this.ValidarDatos())
            {
                int dni = Usuario.ValidarDNI(txtDni.Text);
                if (dni != 0)
                {
                    bool retorno;
                    if (FormListadoUsuarios.tipoDeEmpleado == ETipoUsuario.Cliente)
                    {
                        retorno = FrmEmpleado.empleado.AgregarCliente(txtNombre.Text, txtApellido.Text, dni);
                    }
                    else
                    {
                        retorno = ((Administrador)FrmEmpleado.empleado).AgregarEmpleado(txtNombre.Text, txtApellido.Text, dni, txtNombreUsuario.Text, txtContrasenia.Text, FormListadoUsuarios.tipoDeEmpleado);
                    }
                    if (!retorno)
                        MessageBox.Show("Ya se encuentra cargado un usuario con ese DNI");
                    else
                    {
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }

                }
            }
            else
                errorProvider.SetError(txtDni, "El dni ingresado es invalido (Campo Obligatorio)");
        }
        protected virtual void FormAgregarUsuario_Load(object sender, EventArgs e)
        {
            // Se confgiuran los colores
            this.pnlFondo.BackColor = Color.FromArgb(100, Color.Silver);
            // Configura los campos visibles
            this.ConfiguraVisibilidad();
        }

        protected bool ValidarDatos()
        {
            bool respuesta = true;
            errorProvider.SetError(txtNombreUsuario, "");
            errorProvider.SetError(txtContrasenia, "");
            errorProvider.SetError(txtDni, "");

            if (txtDni.Text.Trim() == string.Empty)
            {
                respuesta = false;
                errorProvider.SetError(txtDni, "Ingresar el DNI (Campo Obligatorio)");
            }
            if (txtNombreUsuario.Text.Trim() == string.Empty && txtNombreUsuario.Visible)
            {
                respuesta = false;
                errorProvider.SetError(txtNombreUsuario, "Ingresar la Contraseña (Campo Obligatorio)");
            }
            if (txtContrasenia.Text.Trim() == string.Empty && txtContrasenia.Visible)
            {
                respuesta = false;
                errorProvider.SetError(txtContrasenia, "Ingresar la Contraseña (Campo Obligatorio)");
            }
            return respuesta;
        }

        private void ConfiguraVisibilidad()
        {
            if (FormListadoUsuarios.tipoDeEmpleado == ETipoUsuario.Cliente)
            {
                this.txtNombreUsuario.Visible = false;
                this.txtContrasenia.Visible = false;
                this.lblNombreUsuario.Visible = false;
                this.lblContrasenia.Visible = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tmrCerrarSesion_Tick(object sender, EventArgs e)
        {
            contadorTimer++;
            if (contadorTimer == 100)
            {
                FrmEmpleado.ignorarFormClosing = true;
                FormCollection formulariosDeLaApp = Application.OpenForms;
                foreach (Form formulario in formulariosDeLaApp)
                {
                    if (formulario.Name != "FrmInicioSesion" && formulario.Name != "FrmEmpleado")
                        formulario.Close();
                }
                MessageBox.Show("Se cerro la sesión por inactividad", "Sesión Finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
        }

        private void FormAgregarUsuario_MouseMove(object sender, MouseEventArgs e)
        {
            this.contadorTimer = 0;
            this.tmrCerrarSesion.Start();
        }

        private void FormAgregarUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tmrCerrarSesion.Stop();
        }
    }
}
