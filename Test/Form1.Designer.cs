namespace Test
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dsddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBoxTransparent1 = new System.Windows.Forms.PictureBoxTransparent();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTransparent1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dsddToolStripMenuItem,
            this.sddToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
            // 
            // dsddToolStripMenuItem
            // 
            this.dsddToolStripMenuItem.Name = "dsddToolStripMenuItem";
            this.dsddToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.dsddToolStripMenuItem.Text = "dsdd";
            // 
            // sddToolStripMenuItem
            // 
            this.sddToolStripMenuItem.Name = "sddToolStripMenuItem";
            this.sddToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.sddToolStripMenuItem.Text = "sdd";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem6});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(111, 26);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(110, 22);
            this.toolStripMenuItem6.Text = "002520";
            // 
            // pictureBoxTransparent1
            // 
            this.pictureBoxTransparent1.AutoRefreshBackground = false;
            this.pictureBoxTransparent1.AutoRefreshBackgroundInterval = 100;
            this.pictureBoxTransparent1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxTransparent1.Location = new System.Drawing.Point(128, 102);
            this.pictureBoxTransparent1.Name = "pictureBoxTransparent1";
            this.pictureBoxTransparent1.Size = new System.Drawing.Size(207, 141);
            this.pictureBoxTransparent1.TabIndex = 2;
            this.pictureBoxTransparent1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(300, 199);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 338);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.pictureBoxTransparent1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTransparent1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dsddToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sddToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.PictureBoxTransparent pictureBoxTransparent1;
        private System.Windows.Forms.Button button1;
    }
}

