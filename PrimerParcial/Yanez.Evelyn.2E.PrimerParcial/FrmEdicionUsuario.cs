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
    public partial class FrmEdicionUsuario : FormAgregarUsuario
    {
        Usuario usuario;
        public FrmEdicionUsuario(Usuario usuario) : base()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            if (usuario != null)
            {
                this.usuario = usuario;
            }
            else
            {
                DialogResult = DialogResult.Abort;
                this.Close();
            }

        }
        protected override void btnAceptar_Click(object sender, EventArgs e)
        {
            if (this.ValidarDatos())
            {
                int dni = Usuario.ValidarDNI(txtDni.Text);
                if (dni != 0)
                {
                    bool retorno;
                    if (FormListadoUsuarios.tipoDeEmpleado == ETipoUsuario.Cliente)
                    {
                        retorno = FrmEmpleado.empleado.ActualizarCliente(this.txtNombre.Text, this.txtApellido.Text, dni);
                    }
                    else
                    {
                        retorno = ((Administrador)FrmEmpleado.empleado).ActualizarEmpleado(this.txtNombre.Text, this.txtApellido.Text, dni, this.txtNombreUsuario.Text, this.txtContrasenia.Text, FormListadoUsuarios.tipoDeEmpleado);
                    }
                    if (!retorno)
                        MessageBox.Show("Ya se encuentra cargado un usuario con ese DNI");
                    else
                    {
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }

                }
                else
                    base.errorProvider.SetError(txtDni, "El dni ingresado es invalido (Campo Obligatorio)");
            }
        }
        protected override void FormAgregarUsuario_Load(object sender, EventArgs e)
        {
            base.FormAgregarUsuario_Load(sender,e);
            this.txtContrasenia.Enabled = false;
            this.txtNombreUsuario.Enabled = false;
            this.txtDni.Enabled = false;
            this.CargarDatosUsuario();

        }

        private void CargarDatosUsuario()
        {
            if (FormListadoUsuarios.tipoDeEmpleado != ETipoUsuario.Cliente)
            {
                this.txtNombreUsuario.Text = ((Empleado)this.usuario).NombreUsuario;
                this.txtContrasenia.Text = ((Empleado)this.usuario).Contrasenia;
            }

            txtDni.Text = this.usuario.Dni.ToString();
            txtApellido.Text = usuario.Apellido;
            txtNombre.Text = usuario.Nombre;
        }
    }
}
