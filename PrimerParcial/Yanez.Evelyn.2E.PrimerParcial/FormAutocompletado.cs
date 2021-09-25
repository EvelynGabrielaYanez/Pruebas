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
    public partial class FrmAutocompletar : Form
    {
        public static Empleado empleado; 
        public FrmAutocompletar()
        {
            InitializeComponent();
        }

        private void ChbEmpleado_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cmbEmpleado.Checked)
                this.cmbAdministrador.Checked = false;

        }

        private void cmbAdministrador_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cmbAdministrador.Checked)
                this.cmbEmpleado.Checked = false;
        }

        private void FrmAutocompletar_Load(object sender, EventArgs e)
        {
            this.cmbEmpleado.Checked = true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            empleado = null;
            if (this.cmbEmpleado.Checked)
            {
                List<Usuario> lista = PetShop.FiltrarListado(typeof(Empleado));
                if (lista.Count != 0)
                    FrmAutocompletar.empleado = (Empleado)lista[0];
            }
            else
            {
                List<Usuario> lista = PetShop.FiltrarListado(typeof(Administrador));
                if (lista.Count != 0)
                    FrmAutocompletar.empleado = (Administrador)lista[0];

            }
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
