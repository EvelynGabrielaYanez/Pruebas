using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace Yanez.Evelyn._2E.PrimerParcial
{
    public partial class FormListadoUsuarios : Form
    {
        public static ETipoUsuario tipoDeEmpleado;
        public FormListadoUsuarios()
        {
            InitializeComponent();
        }

        private void FormListadoUsuarios_Load(object sender, EventArgs e)
        {
            panelTipos.BackColor = Color.FromArgb(125, Color.Indigo);
            this.cmbTipoUsuario.DataSource = Enum.GetValues(typeof(ETipoUsuario));
            tipoDeEmpleado = (ETipoUsuario)this.cmbTipoUsuario.SelectedItem;
        }

        private void ActualizarDataGrid()
        {
            switch ((ETipoUsuario)this.cmbTipoUsuario.SelectedItem)
            {
                case ETipoUsuario.Administador:
                    this.dgUsuarios.DataSource = PetShop.FiltrarListado(typeof(Administrador)).Cast<Administrador>().ToList();
                    break;
                case ETipoUsuario.Empleado:
                    this.dgUsuarios.DataSource = PetShop.FiltrarListado(typeof(Empleado)).Cast<Empleado>().ToList();
                    break;
                case ETipoUsuario.Cliente:
                    this.dgUsuarios.DataSource = PetShop.FiltrarListado(typeof(Cliente)).Cast<Cliente>().ToList();
                    break;
            }  
        }

        private void cmbTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipoDeEmpleado = (ETipoUsuario)this.cmbTipoUsuario.SelectedItem;
            this.ActualizarDataGrid();
        }

        private Usuario ObtenerDatosDeSeleccion()
        {
            Usuario usuario = null;
            int indiceFila = dgUsuarios.CurrentRow.Index;
            if (indiceFila >= 0)
            {
                DataGridViewRow fila = dgUsuarios.Rows[indiceFila];
                int dni = (int)fila.Cells["dni"].Value;
                string nombre = fila.Cells["nombre"].Value.ToString();
                string apellido = fila.Cells["apellido"].Value.ToString();
                switch ((ETipoUsuario)this.cmbTipoUsuario.SelectedItem)
                {
                    case ETipoUsuario.Administador:
                    case ETipoUsuario.Empleado:
                        string nombreUsuario = fila.Cells["nombreUsuario"].Value.ToString();
                        string contrasenia = fila.Cells["contrasenia"].Value.ToString();
                        usuario = new Empleado(dni, nombre, apellido, nombreUsuario, contrasenia);
                        break;
                    case ETipoUsuario.Cliente:
                        double dineroInvertido = (double)fila.Cells["dineroInvertido"].Value;
                        usuario = new Cliente(dni, nombre, apellido);
                        break;
                }
            }
            return usuario;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Usuario usuarioSeleccionado = this.ObtenerDatosDeSeleccion();
            if (usuarioSeleccionado != null)
            {
                if (!FormEmpleado.empleado.EliminarUsuario(usuarioSeleccionado.Dni))
                {
                    MessageBox.Show("Ocurrio un error al intentar eliminar el usuario");
                }
                this.ActualizarDataGrid();
            }
            else
            {
                MessageBox.Show("Debe seleccionar el usuario que desea eliminar");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Usuario usuario = this.ObtenerDatosDeSeleccion();
            if (usuario != null)
            {
                FrmEdicionUsuario frmEdicionUsuario = new FrmEdicionUsuario(usuario);
                frmEdicionUsuario.ShowDialog();
                this.ActualizarDataGrid();
            }
            else
            {
                MessageBox.Show("Debe seleccionar el usuario que desea modificar");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FormAgregarUsuario frmAgregarUsuario = new FormAgregarUsuario();
            frmAgregarUsuario.ShowDialog();
            this.ActualizarDataGrid();
        }
    }
}
