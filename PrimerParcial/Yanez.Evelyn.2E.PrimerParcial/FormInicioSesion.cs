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
    public partial class FrmInicioSecion : Form
    {
        static Empleado empleado;
        public FrmInicioSecion()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            // Valida el DNI ingresado
            int dni = Usuario.ValidarDNI(this.txtDni.Text);
            if (dni != 0)
            {
                Empleado bufferEmpleado = new Empleado(dni, this.txtNombreUsuario.Text, this.txtNombreContrasenia.Text);
                // Busca el empleado en el listado de empelados del PetSop y valida los datos ingresados
                if (PetShop.BuscarYValidarUsuario(bufferEmpleado, out Usuario empleado))
                {
                    FormEmpleado frmEmpleado = new FormEmpleado();
                    FormEmpleado.empleado = (Empleado)empleado;
                    this.Visible = false;
                    frmEmpleado.ShowDialog();
                    this.Visible = true;
                }
            }
        }

        private void lblAutocompletar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmAutocompletar frmAutocompletar = new FrmAutocompletar();
            if (DialogResult.Cancel != frmAutocompletar.ShowDialog())
            {
                Empleado empleado = FrmAutocompletar.empleado;
                if (empleado != null)
                    this.CompletarDatos(empleado);
                else
                    MessageBox.Show("No hay ningun usuario cargado con el tipo pedido.");
            }

        }
        private void CompletarDatos(Empleado empelado)
        {
            this.txtNombreContrasenia.Text = empelado.Contrasenia;
            this.txtNombreUsuario.Text = empelado.NombreUsuario;
            this.txtDni.Text = empelado.Dni.ToString();
        }

        private void FrmInicioSecion_Load(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
