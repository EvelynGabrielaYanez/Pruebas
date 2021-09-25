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
    public partial class FrmEdicionUsuario : Form
    {
        Usuario usuario;
        public FrmEdicionUsuario(Usuario usuario)
        {
            InitializeComponent();
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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Falta validar datos
            if (FormListadoUsuarios.tipoDeEmpleado == ETipoUsuario.Cliente)
            {
                FormEmpleado.empleado.ActualizarCliente(txtNombre.Text, txtApellido.Text, this.usuario.Dni);
            }
            else
            {
                ((Administrador)FormEmpleado.empleado).ActualizarEmpleado( txtNombre.Text, txtApellido.Text, this.usuario.Dni, txtNombreUsuario.Text, txtContrasenia.Text, FormListadoUsuarios.tipoDeEmpleado);
            }
            this.Close();
        }

        private void FrmEdicionUsuario_Load(object sender, EventArgs e)
        {
            this.ConfiguraVisibilidadYDatos();
        }

        private void ConfiguraVisibilidadYDatos()
        {
            this.txtNombre.Text = this.usuario.Nombre;
            this.txtApellido.Text = this.usuario.Apellido;
            if (typeof(Cliente) == this.usuario.GetType())
            {
                txtNombreUsuario.Visible = false;
                txtContrasenia.Visible = false;
                lblNombreUsuario.Visible = false;
                lblContrasenia.Visible = false;
            }
            else
            {
                txtNombreUsuario.Text = ((Empleado)this.usuario).NombreUsuario;
                txtContrasenia.Text = ((Empleado)this.usuario).Contrasenia;
            }
        }
    }
}
