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
    public partial class FrmEmpleado : Form
    {
        public static Empleado empleado;
        public static Cliente cliente;
        int contadorTimer;
        public static bool ignorarFormClosing;
        public FrmEmpleado()
        {
            InitializeComponent();
            FrmEmpleado.empleado = null;
            FrmEmpleado.cliente = null;
            ignorarFormClosing = false;
        }

        private void FormEmpleado_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ignorarFormClosing)
            {
                if (MessageBox.Show("¿Seguro que desea cerrar sesión?", "Cierre de sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    e.Cancel = true;
            }
        }

        private void FormEmpleado_Load(object sender, EventArgs e)
        {
            // Inicializa el timer
            tmrMenuPrincipal.Start();
            // Se configura la apariencia de los paneles
            panel1.BackColor = Color.FromArgb(125,Color.Indigo);
            panelAlimentos.BackColor = Color.FromArgb(125,Color.Silver);
            panelCamas.BackColor = Color.FromArgb(125,Color.Silver);
            panelJuguetes.BackColor = Color.FromArgb(125,Color.Silver);
            panelArticulosDeCuidado.BackColor = Color.FromArgb(125,Color.Silver);
            msMenu.BringToFront();
            panel1.SendToBack();

            // configuro la apariencia del menustrip segun el tipo de empleado
            if (FrmEmpleado.empleado.GetType() == typeof(Administrador))
                msMenu.BackColor = Color.FromArgb(171, 143, 192);
            else
                msMenu.BackColor = Color.Silver;

            // Se configura la apariencia de los botones
            btnBuscarCliente.BackColor = Color.FromArgb(171, 143, 192);
            btnDescartarCompra.BackColor = Color.FromArgb(171, 143, 192);
            btnFinalizarCompra.BackColor = Color.FromArgb(171, 143, 192);

            // Configura el auto completado del dni del cliente
            AutoCompleteStringCollection fuenteDeAutoCompletado = new AutoCompleteStringCollection();
            // Busca la lista de clientes para autocompletar
            List<Cliente> clientes = PetShop.FiltrarListadoUsuario(typeof(Cliente)).Cast<Cliente>().ToList();
            foreach (Cliente cliente in clientes)
            {
                fuenteDeAutoCompletado.Add(cliente.Dni.ToString());
            }

            // configura el textBox del DNI del cliente
            this.txtDniCliente.AutoCompleteCustomSource = fuenteDeAutoCompletado;
            this.txtDniCliente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.txtDniCliente.AutoCompleteSource = AutoCompleteSource.CustomSource;

            // Completa los labels del Cliente por defecto
            lblNombreDelCliente.Text = $"Nombre:";
            lblApellido.Text = $"Apellido:";
            lblDniCliente.Text = $"DNI:";
            lblSaldo.Text = $"Saldo:";

            // Se bloquean los botones de finalizacion y descarte de venta.
            btnFinalizarCompra.Enabled = false;
            btnDescartarCompra.Enabled = false;

            // se configuran el menu segun el tipo de empleado
            this.VistaSegunEmpleado();
        }

        private void VistaSegunEmpleado()
        {
            if (typeof(Empleado) == FrmEmpleado.empleado.GetType())
                msMenu.Items[0].Visible = false;
        }


        private void FormEmpleado_MouseMove(object sender, MouseEventArgs e)
        {
            this.contadorTimer = 0;
            this.tmrMenuPrincipal.Start();
            if (e.Y <= 10)
                msMenu.Visible = true;
            else
                msMenu.Visible = false;
        }


        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (panel1.PointToClient(Cursor.Position).Y <= 10)
                msMenu.Visible = true;
            else
                msMenu.Visible = false;
        }
        private void configurarUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormListadoUsuarios frmListadoUsuarios = new FormListadoUsuarios();
            this.Visible = false;
            frmListadoUsuarios.ShowDialog();
            this.Visible = true;
        }

        private void btnAlimentos_Click(object sender, EventArgs e)
        {
            if (FrmEmpleado.cliente is not null)
            {
                FormCompras frmCompras = new FormCompras(ETipoDeProducto.Alimento);
                this.Visible = false;
                frmCompras.ShowDialog();
                this.Visible = true;
            }
        }

        private void btnArticulosDeCuidado_Click(object sender, EventArgs e)
        {
            if (FrmEmpleado.cliente is not null)
            {
                FormCompras frmCompras = new FormCompras(ETipoDeProducto.ArticuloDeCuidado);
                this.Visible = false;
                frmCompras.ShowDialog();
                this.Visible = true;
            }
        }

        private void btnJuguetes_Click(object sender, EventArgs e)
        {
            if (FrmEmpleado.cliente is not null)
            {
                FormCompras frmCompras = new FormCompras(ETipoDeProducto.Juguete);
                this.Visible = false;
                frmCompras.ShowDialog();
                this.Visible = true;
            }
        }

        private void btnCamas_Click(object sender, EventArgs e)
        {
            if (FrmEmpleado.cliente is not null)
            {
                FormCompras frmCompras = new FormCompras(ETipoDeProducto.Cama);
                this.Visible = false;
                frmCompras.ShowDialog();
                this.Visible = true;
            }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            int dni = Usuario.ValidarDNI(txtDniCliente.Text);
            if (dni != 0)
            {
                FrmEmpleado.cliente = new Cliente(dni, "", "");
                Usuario usuario = PetShop.BuscarCliente(FrmEmpleado.cliente);
                if (usuario != null && usuario.GetType() == typeof(Cliente))
                {
                    FrmEmpleado.cliente = (Cliente)usuario;
                }
                else
                {
                    if (MessageBox.Show("No existe un cliente con el DNI indicado.\n¿Desea registrarlo?", "Cliente No Registrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        FormListadoUsuarios frmListadoUsuarios = new FormListadoUsuarios();
                        this.Visible = false;
                        frmListadoUsuarios.ShowDialog();
                        this.Visible = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("El DNI ingresado es invalido");
            }
            this.ConfigurarDatosCliente();
        }

        private void ConfigurarDatosCliente()
        {
            if (FrmEmpleado.cliente != null)
            {
                lblNombreDelCliente.Text = $"Nombre: {FrmEmpleado.cliente.Nombre}";
                lblApellido.Text = $"Apellido: {FrmEmpleado.cliente.Apellido}";
                lblDniCliente.Text = $"DNI: {FrmEmpleado.cliente.Dni}";
                lblSaldo.Text = $"Saldo: {FrmEmpleado.cliente.Saldo}";
                btnFinalizarCompra.Enabled = true;
                btnDescartarCompra.Enabled = true;
            }
            else
            {
                lblNombreDelCliente.Text = $"Nombre:";
                lblApellido.Text = $"Apellido:";
                lblDniCliente.Text = $"DNI:";
                lblSaldo.Text = $"Saldo:";
                btnFinalizarCompra.Enabled = false;
                btnDescartarCompra.Enabled = false;
            }
        }

        private void cerrarSesionTSM_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDescartarCompra_Click(object sender, EventArgs e)
        {
            this.DescartarComra();
        }
        private void DescartarComra()
        {
            FrmEmpleado.cliente = null;
            this.ConfigurarDatosCliente();
            txtDniCliente.Text = string.Empty;
        }

        private void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            FormFinalizarCompra frmFinalizarCompra = new FormFinalizarCompra(FrmEmpleado.cliente);
            this.Visible = false;
            if (frmFinalizarCompra.ShowDialog() == DialogResult.OK)
                this.DescartarComra();
            else
                this.lblSaldo.Text = $"Saldo: ${FrmEmpleado.cliente.Saldo}";

            this.Visible = true;
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListadoProductos frmListadoProductos = new FrmListadoProductos();
            this.Visible = false;
            frmListadoProductos.ShowDialog();
            this.Visible = true;
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVentas frmVentas = new FrmVentas();
            this.Visible = false;
            frmVentas.ShowDialog();
            this.Visible = true;
        }

        private void cerrarSeciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPerfilEmpleado frmPerfilEmpleado = new FrmPerfilEmpleado();
            this.Visible = false;
            frmPerfilEmpleado.ShowDialog();
            this.Visible = true;
        }

        private void tmrMenuPrincipal_Tick(object sender, EventArgs e)
        {
            this.contadorTimer++;
            if (this.contadorTimer == 100)
            {
                ignorarFormClosing = true;
                FormCollection formulariosDeLaApp = Application.OpenForms;
                foreach (Form formulario in formulariosDeLaApp)
                {
                    if (formulario.Name != "FrmInicioSesion" && formulario.Name != "FrmEmpleado")
                        formulario.Close();
                }
                MessageBox.Show("Se cerro la sesión por inactividad","Sesión Finalizada",MessageBoxButtons.OK,MessageBoxIcon.Information);

                this.Close();
            }
        }

        private void FrmEmpleado_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                this.tmrMenuPrincipal.Stop();
            }
        }
    }
}
