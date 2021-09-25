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
        public FormEmpleado()
        {
            InitializeComponent();
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


        private void configurarUsuariosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FormListadoUsuarios frmListadoUsuarios = new FormListadoUsuarios();
            this.Visible = false;
            frmListadoUsuarios.ShowDialog();
            this.Visible = true;
        }
    }
}
