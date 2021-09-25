
namespace Yanez.Evelyn._2E.PrimerParcial
{
    partial class FrmInicioSecion
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInicioSecion));
            this.txtNombreUsuario = new System.Windows.Forms.TextBox();
            this.txtNombreContrasenia = new System.Windows.Forms.TextBox();
            this.btnIniciarSesion = new System.Windows.Forms.Button();
            this.txtDni = new System.Windows.Forms.TextBox();
            this.lblOlvidoLaContrania = new System.Windows.Forms.LinkLabel();
            this.lblAutocompletar = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // txtNombreUsuario
            // 
            this.txtNombreUsuario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtNombreUsuario.Location = new System.Drawing.Point(42, 33);
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.PlaceholderText = "Ingrese el usuario";
            this.txtNombreUsuario.Size = new System.Drawing.Size(177, 25);
            this.txtNombreUsuario.TabIndex = 1;
            // 
            // txtNombreContrasenia
            // 
            this.txtNombreContrasenia.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtNombreContrasenia.Location = new System.Drawing.Point(42, 79);
            this.txtNombreContrasenia.Name = "txtNombreContrasenia";
            this.txtNombreContrasenia.PasswordChar = '*';
            this.txtNombreContrasenia.PlaceholderText = "Ingrese la contraseña";
            this.txtNombreContrasenia.Size = new System.Drawing.Size(177, 25);
            this.txtNombreContrasenia.TabIndex = 2;
            // 
            // btnIniciarSesion
            // 
            this.btnIniciarSesion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnIniciarSesion.Location = new System.Drawing.Point(42, 179);
            this.btnIniciarSesion.Name = "btnIniciarSesion";
            this.btnIniciarSesion.Size = new System.Drawing.Size(177, 25);
            this.btnIniciarSesion.TabIndex = 4;
            this.btnIniciarSesion.Text = "Iniciar Sesión";
            this.btnIniciarSesion.UseVisualStyleBackColor = true;
            this.btnIniciarSesion.Click += new System.EventHandler(this.btnIniciarSesion_Click);
            // 
            // txtDni
            // 
            this.txtDni.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtDni.Location = new System.Drawing.Point(42, 126);
            this.txtDni.Name = "txtDni";
            this.txtDni.PlaceholderText = "Ingrese el DNI";
            this.txtDni.Size = new System.Drawing.Size(177, 25);
            this.txtDni.TabIndex = 3;
            // 
            // lblOlvidoLaContrania
            // 
            this.lblOlvidoLaContrania.ActiveLinkColor = System.Drawing.Color.White;
            this.lblOlvidoLaContrania.AutoSize = true;
            this.lblOlvidoLaContrania.BackColor = System.Drawing.Color.Transparent;
            this.lblOlvidoLaContrania.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblOlvidoLaContrania.ForeColor = System.Drawing.Color.Transparent;
            this.lblOlvidoLaContrania.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lblOlvidoLaContrania.LinkColor = System.Drawing.Color.White;
            this.lblOlvidoLaContrania.Location = new System.Drawing.Point(42, 248);
            this.lblOlvidoLaContrania.Name = "lblOlvidoLaContrania";
            this.lblOlvidoLaContrania.Size = new System.Drawing.Size(177, 19);
            this.lblOlvidoLaContrania.TabIndex = 5;
            this.lblOlvidoLaContrania.TabStop = true;
            this.lblOlvidoLaContrania.Text = "¿Olvidaste la contraseña?";
            this.lblOlvidoLaContrania.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOlvidoLaContrania.VisitedLinkColor = System.Drawing.Color.White;
            // 
            // lblAutocompletar
            // 
            this.lblAutocompletar.ActiveLinkColor = System.Drawing.Color.White;
            this.lblAutocompletar.AutoSize = true;
            this.lblAutocompletar.BackColor = System.Drawing.Color.Transparent;
            this.lblAutocompletar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblAutocompletar.ForeColor = System.Drawing.Color.Transparent;
            this.lblAutocompletar.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lblAutocompletar.LinkColor = System.Drawing.Color.White;
            this.lblAutocompletar.Location = new System.Drawing.Point(77, 219);
            this.lblAutocompletar.Name = "lblAutocompletar";
            this.lblAutocompletar.Size = new System.Drawing.Size(110, 19);
            this.lblAutocompletar.TabIndex = 6;
            this.lblAutocompletar.TabStop = true;
            this.lblAutocompletar.Text = "Autocompletar";
            this.lblAutocompletar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAutocompletar.VisitedLinkColor = System.Drawing.Color.White;
            this.lblAutocompletar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblAutocompletar_LinkClicked);
            // 
            // FrmInicioSecion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(263, 303);
            this.Controls.Add(this.lblAutocompletar);
            this.Controls.Add(this.lblOlvidoLaContrania);
            this.Controls.Add(this.txtDni);
            this.Controls.Add(this.btnIniciarSesion);
            this.Controls.Add(this.txtNombreContrasenia);
            this.Controls.Add(this.txtNombreUsuario);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInicioSecion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio de Sesión";
            this.Load += new System.EventHandler(this.FrmInicioSecion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.TextBox txtNombreContrasenia;
        private System.Windows.Forms.Button btnIniciarSesion;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.LinkLabel lblOlvidoLaContrania;
        private System.Windows.Forms.LinkLabel lblAutocompletar;
    }
}

