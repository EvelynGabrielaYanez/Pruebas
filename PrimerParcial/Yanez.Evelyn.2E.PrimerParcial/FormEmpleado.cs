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
    public partial class FormEmpleado : Form
    {
        public static Empleado empleado;
        public static Cliente cliente;
        public FormEmpleado()
        {
            InitializeComponent();
            FormEmpleado.empleado = null;
            FormEmpleado.cliente = null;
        }

        private void FormEmpleado_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea cerrar sesión?", "Cierre de sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true ;
        }

        private void FormEmpleado_Load(object sender, EventArgs e)
        {
            // Se configura la apariencia de los paneles
            panel1.BackColor = Color.FromArgb(125,Color.Indigo);
            panelAlimentos.BackColor = Color.FromArgb(125,Color.Silver);
            panelCamas.BackColor = Color.FromArgb(125,Color.Silver);
            panelJuguetes.BackColor = Color.FromArgb(125,Color.Silver);
            panelArticulosDeCuidado.BackColor = Color.FromArgb(125,Color.Silver);
            msMenu.BringToFront();
            panel1.SendToBack();

            // Se configura la apariencia de los botones
            btnBuscarCliente.BackColor = Color.FromArgb(171, 143, 192);
            btnDescartarCompra.BackColor = Color.FromArgb(171, 143, 192);
            btnFinalizarCompra.BackColor = Color.FromArgb(171, 143, 192);

            // Configura el auto completado del dni del cliente
            AutoCompleteStringCollection fuenteDeAutoCompletado = new AutoCompleteStringCollection();
            // Busca la lista de clientes para autocompletar
            List<Cliente> clientes = PetShop.FiltrarListado(typeof(Cliente)).Cast<Cliente>().ToList();
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

        }


        private void FormEmpleado_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Y <= 10)
            {
                msMenu.Visible = true;
            }
            else
            {
                msMenu.Visible = false;
            }
        }


        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (panel1.PointToClient(Cursor.Position).Y <= 10)
            {
                msMenu.Visible = true;
            }
            else
            {
                msMenu.Visible = false;
            }
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
            FormCompras frmCompras = new FormCompras(ETipoDeProducto.Alimento);
            this.Visible = false;
            frmCompras.ShowDialog();
            this.Visible = true;
        }

        private void btnArticulosDeCuidado_Click(object sender, EventArgs e)
        {
            FormCompras frmCompras = new FormCompras(ETipoDeProducto.ArticuloDeCuidado);
            this.Visible = false;
            frmCompras.ShowDialog();
            this.Visible = true;
        }

        private void btnJuguetes_Click(object sender, EventArgs e)
        {
            FormCompras frmCompras = new FormCompras(ETipoDeProducto.Juguete);
            this.Visible = false;
            frmCompras.ShowDialog();
            this.Visible = true;
        }

        private void btnCamas_Click(object sender, EventArgs e)
        {
            FormCompras frmCompras = new FormCompras(ETipoDeProducto.Cama);
            this.Visible = false;
            frmCompras.ShowDialog();
            this.Visible = true;
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            int dni = Usuario.ValidarDNI(txtDniCliente.Text);
            if (dni != 0)
            {
                FormEmpleado.cliente = new Cliente(dni, "", "");
                Usuario usuario = PetShop.BuscarUsuario(FormEmpleado.cliente);
                if (usuario != null && usuario.GetType() == typeof(Cliente))
                {
                    FormEmpleado.cliente = (Cliente)usuario;
                }
                else
                {
                    if (MessageBox.Show("No existe un cliente con el DNI indicado. \n¿Desea registrarlo?", "Cliente No Registrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
            if (FormEmpleado.cliente != null)
            {
                lblNombreDelCliente.Text = $"Nombre: {FormEmpleado.cliente.Nombre}";
                lblApellido.Text = $"Apellido: {FormEmpleado.cliente.Apellido}";
                lblDniCliente.Text = $"DNI: {FormEmpleado.cliente.Dni}";
                lblSaldo.Text = $"Saldo: {FormEmpleado.cliente.Saldo}";
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
            FormEmpleado.cliente = null;
            this.ConfigurarDatosCliente();
            txtDniCliente.Text = string.Empty;
        }

        private void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            FormFinalizarCompra frmFinalizarCompra = new FormFinalizarCompra();
            this.Visible = false;
            if (frmFinalizarCompra.ShowDialog() == DialogResult.OK)
            {
                this.DescartarComra();
            }
            this.Visible = true;
        }

    }
}
