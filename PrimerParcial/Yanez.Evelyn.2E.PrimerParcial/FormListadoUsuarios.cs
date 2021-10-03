using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace Yanez.Evelyn._2E.PrimerParcial
{
    public partial class FormListadoUsuarios : Form
    {
        public static ETipoUsuario tipoDeEmpleado;
        static List<Usuario> fuenteDeInformacion;
        int contadorTimer;

        public FormListadoUsuarios()
        {
            InitializeComponent();
            this.contadorTimer = 0;
        }

        private void FormListadoUsuarios_Load(object sender, EventArgs e)
        {
            this.tmrCierreSesion.Start();
            panelTipos.BackColor = Color.FromArgb(125, Color.Indigo);
            pnlActivos.BackColor = Color.FromArgb(100, Color.Silver);
            pnlInactivos.BackColor = Color.FromArgb(100, Color.Silver);
            if (FrmEmpleado.empleado.GetType() == typeof(Administrador))
            {
                this.cmbTipoUsuario.DataSource = Enum.GetValues(typeof(ETipoUsuario));
            }
            else
            {
                this.cmbTipoUsuario.Items.Add(ETipoUsuario.Cliente);
                this.cmbTipoUsuario.SelectedIndex = 0;
            }

            tipoDeEmpleado = (ETipoUsuario)this.cmbTipoUsuario.SelectedItem;
        }

        private void ActualizarDataGrid()
        {
            // Cargo la fuente
            switch (FormListadoUsuarios.tipoDeEmpleado)
            {
                case ETipoUsuario.Administador:
                    fuenteDeInformacion = PetShop.FiltrarListadoUsuario(typeof(Administrador), false);
                    this.dgUsuarios.DataSource = fuenteDeInformacion.Cast<Administrador>().ToList();
                    break;
                case ETipoUsuario.Empleado:
                    fuenteDeInformacion = PetShop.FiltrarListadoUsuario(typeof(Empleado), false);
                    this.dgUsuarios.DataSource = fuenteDeInformacion.Cast<Empleado>().ToList();
                    break;
                case ETipoUsuario.Cliente:
                    fuenteDeInformacion = PetShop.FiltrarListadoUsuario(typeof(Cliente));
                    this.dgUsuarios.DataSource = fuenteDeInformacion.Cast<Cliente>().ToList();
                    // configuro los campos
                    this.dgUsuarios.Columns["Carrito"].Visible = false;
                    this.dgUsuarios.Columns["ProductosComprados"].Visible = false;
                    break;
            }
            // Se corrige la visa de los nombre con camellCase
            if (FormListadoUsuarios.tipoDeEmpleado == ETipoUsuario.Administador ||
                FormListadoUsuarios.tipoDeEmpleado == ETipoUsuario.Empleado)
            {
                this.dgUsuarios.Columns["NombreUsuario"].HeaderText = "Nombre de Usuario";
                this.dgUsuarios.Columns["Contrasenia"].HeaderText = "Contraseña";
            }
        }

        private void cmbTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormListadoUsuarios.tipoDeEmpleado = (ETipoUsuario)this.cmbTipoUsuario.SelectedItem;
            this.ActualizarDataGrid();
            if (FormListadoUsuarios.tipoDeEmpleado == ETipoUsuario.Cliente)
            {
                btnEliminar.Visible = false;
                chbActivos.Visible = false;
                chbInactivos.Visible = false;
                pnlActivos.Visible = false;
                pnlInactivos.Visible = false;
            }
            else
            {
                btnEliminar.Visible = true;
                chbActivos.Visible = true;
                chbInactivos.Visible = true;
                pnlActivos.Visible = true;
                pnlInactivos.Visible = true;
            }
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
                        double dineroInvertido = (double)fila.Cells["saldo"].Value;
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
                if (!((Administrador)FrmEmpleado.empleado).EliminarUsuario(usuarioSeleccionado.Dni))
                {
                    MessageBox.Show("Ocurrio un error al intentar eliminar el usuario");
                }
                else
                {
                    this.HacerSonidoExito();
                    this.ActualizarDataGrid();
                }
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
                if (frmEdicionUsuario.ShowDialog() == DialogResult.OK)
                {
                    this.HacerSonidoExito();
                    this.ActualizarDataGrid();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar el usuario que desea modificar");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FormAgregarUsuario frmAgregarUsuario = new FormAgregarUsuario();
            if (frmAgregarUsuario.ShowDialog() == DialogResult.OK)
            {
                this.HacerSonidoExito();
            }
        }
        private void HacerSonidoExito()
        {
            string sonidoExito = @"C:\Windows\Media\chimes.wav";
            this.ActualizarDataGrid();
            SoundPlayer sonido = new SoundPlayer(sonidoExito);
            sonido.Play();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<Usuario> datosFiltrados = fuenteDeInformacion.ToList().Where(usuario =>
            {
                bool respuesta = true;
                if (int.TryParse(txtBuscar.Text,out int dni))
                    respuesta &= usuario.Dni == dni;
                if (usuario is Empleado)
                {
                    if(chbActivos.Checked != chbInactivos.Checked)
                        respuesta &= ((Empleado)usuario).Activo == (chbActivos.Checked && !chbInactivos.Checked);
                }
                return respuesta;
            }).ToList();

            this.ActualizarFiltrado(datosFiltrados);
        }

        private void ActualizarFiltrado(List<Usuario> datosFiltrados)
        {
            switch (FormListadoUsuarios.tipoDeEmpleado)
            {
                case ETipoUsuario.Administador:
                case ETipoUsuario.Empleado:
                    dgUsuarios.DataSource = datosFiltrados.Cast<Empleado>().ToList();
                    break;
                case ETipoUsuario.Cliente:
                    dgUsuarios.DataSource = datosFiltrados.Cast<Cliente>().ToList();
                    // configuro los campos
                    this.dgUsuarios.Columns["Carrito"].Visible = false;
                    this.dgUsuarios.Columns["ProductosComprados"].Visible = false;
                    break;
            }
        }

        private void tmrCierreSesion_Tick(object sender, EventArgs e)
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

        private void FormListadoUsuarios_MouseMove(object sender, MouseEventArgs e)
        {
            this.contadorTimer = 0;
            this.tmrCierreSesion.Start();
        }

        private void FormListadoUsuarios_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tmrCierreSesion.Stop();
        }
    }
}
