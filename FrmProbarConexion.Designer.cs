using System.Drawing;
using System.Windows.Forms;

namespace Visitas
{
    partial class FrmProbarConexion
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.BtnProbar = new System.Windows.Forms.Button();
            this.LblTitulo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnProbar
            // 
            this.BtnProbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(18)))), ((int)(((byte)(64)))));
            this.BtnProbar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnProbar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.BtnProbar.ForeColor = System.Drawing.Color.White;
            this.BtnProbar.Location = new System.Drawing.Point(50, 80);
            this.BtnProbar.Name = "BtnProbar";
            this.BtnProbar.Size = new System.Drawing.Size(300, 50);
            this.BtnProbar.TabIndex = 0;
            this.BtnProbar.Text = "🔌 Probar Conexión";
            this.BtnProbar.UseVisualStyleBackColor = false;
            this.BtnProbar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnProbar.Click += new System.EventHandler(this.BtnProbar_Click);
            // 
            // LblTitulo
            // 
            this.LblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.LblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(18)))), ((int)(((byte)(64)))));
            this.LblTitulo.Location = new System.Drawing.Point(50, 20);
            this.LblTitulo.Name = "LblTitulo";
            this.LblTitulo.Size = new System.Drawing.Size(300, 40);
            this.LblTitulo.TabIndex = 1;
            this.LblTitulo.Text = "Prueba de Conexión";
            this.LblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmProbarConexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(400, 180);
            this.Controls.Add(this.LblTitulo);
            this.Controls.Add(this.BtnProbar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProbarConexion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prueba de Conexión - Control de Visitas";
            this.Load += new System.EventHandler(this.FrmProbarConexion_Load);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button BtnProbar;
        private System.Windows.Forms.Label LblTitulo;
    }
}