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

        private void cerrarSesionTSM_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea cerrar sesión?", "Cierre de sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }

        private void FormEmpleado_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show("¿Seguro que desea cerrar el programa?", "Cierre de programa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            if (MessageBox.Show("¿Seguro que desea cerrar sesión?", "Cierre de sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true ;
        }


        private void cerrarSesionTSM_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormEmpleado_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(125,Color.Indigo);
            panelAlimentos.BackColor = Color.FromArgb(125,Color.Silver);
            panelCamas.BackColor = Color.FromArgb(125,Color.Silver);
            panelJuguetes.BackColor = Color.FromArgb(125,Color.Silver);
            panelArticulosDeCuidado.BackColor = Color.FromArgb(125,Color.Silver);
            msMenu.BringToFront();

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

            // Oculta los labels con los datos del cliente
            lblNombreDelCliente.Visible = false;
            lblApellido.Visible = false;
            lblDniCliente.Visible = false;
            lblSaldo.Visible = false;
            lblDatosDelCliente.Visible = false;
        }

        private void btnArticulosDeCuidado_Click(object sender, EventArgs e)
        {

        }

        private void btnJuguetes_Click(object sender, EventArgs e)
        {
            FormCompras frmCompras = new FormCompras();
            this.Visible = false;
            frmCompras.ShowDialog();
            this.Visible = true;
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

        private void configurarUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormListadoUsuarios frmListadoUsuarios = new FormListadoUsuarios();
            this.Visible = false;
            frmListadoUsuarios.ShowDialog();
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
                    MessageBox.Show("No existe un cliente con el DNI indicado. \nDe no estar registrado debe hacerlo desde la configuración de usuarios.");
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
                lblNombreDelCliente.Visible = true;
                lblApellido.Visible = true;
                lblDniCliente.Visible = true;
                lblSaldo.Visible = true;
                lblDatosDelCliente.Visible = true;
            }
            else {
                lblNombreDelCliente.Visible = false;
                lblApellido.Visible = false;
                lblDniCliente.Visible = false;
                lblSaldo.Visible = false;
                lblDatosDelCliente.Visible = false;
            }

            
        }
    }
}
