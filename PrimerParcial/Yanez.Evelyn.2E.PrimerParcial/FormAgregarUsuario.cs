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
        public FormAgregarUsuario()
        {
            InitializeComponent();
        }

        protected virtual void btnAceptar_Click(object sender, EventArgs e)
        {
            int dni = Usuario.ValidarDNI(txtDni.Text);
            if (dni != 0)
            {
                bool retorno;
                if (FormListadoUsuarios.tipoDeEmpleado == ETipoUsuario.Cliente)
                {
                    retorno = FormEmpleado.empleado.AgregarCliente(txtNombre.Text, txtApellido.Text, dni);
                }
                else
                {
                    retorno = ((Administrador)FormEmpleado.empleado).AgregarEmpleado(txtNombre.Text, txtApellido.Text, dni, txtNombreUsuario.Text, txtContrasenia.Text, FormListadoUsuarios.tipoDeEmpleado);
                }
                if (!retorno)
                {
                    MessageBox.Show("Ya se encuentra cargado un usuario con ese DNI");
                }
                this.Close();
            }
        }
        protected virtual void FormAgregarUsuario_Load(object sender, EventArgs e)
        {
            this.ConfiguraVisibilidad();
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

        }


    }
}
