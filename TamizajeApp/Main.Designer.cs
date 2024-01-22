namespace TamizajeApp
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbImportar = new System.Windows.Forms.ToolStripButton();
            this.tslImportar = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbValidar = new System.Windows.Forms.ToolStripButton();
            this.tslValidar = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbImportar,
            this.tslImportar,
            this.toolStripSeparator1,
            this.tsbValidar,
            this.tslValidar,
            this.toolStripButton1,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1159, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbImportar
            // 
            this.tsbImportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImportar.Image = ((System.Drawing.Image)(resources.GetObject("tsbImportar.Image")));
            this.tsbImportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImportar.Name = "tsbImportar";
            this.tsbImportar.Size = new System.Drawing.Size(28, 28);
            this.tsbImportar.Text = "toolStripButton1";
            this.tsbImportar.Click += new System.EventHandler(this.tsbImportar_Click);
            // 
            // tslImportar
            // 
            this.tslImportar.Name = "tslImportar";
            this.tslImportar.Size = new System.Drawing.Size(174, 28);
            this.tslImportar.Text = "Importar Resultados";
            this.tslImportar.Click += new System.EventHandler(this.tslImportar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbValidar
            // 
            this.tsbValidar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbValidar.Image = ((System.Drawing.Image)(resources.GetObject("tsbValidar.Image")));
            this.tsbValidar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbValidar.Name = "tsbValidar";
            this.tsbValidar.Size = new System.Drawing.Size(28, 28);
            this.tsbValidar.Text = "toolStripButton2";
            this.tsbValidar.Click += new System.EventHandler(this.tsbValidar_Click);
            // 
            // tslValidar
            // 
            this.tslValidar.Name = "tslValidar";
            this.tslValidar.Size = new System.Drawing.Size(171, 28);
            this.tslValidar.Text = "Resultados x Validar";
            this.tslValidar.Click += new System.EventHandler(this.tslValidar_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(182, 28);
            this.toolStripLabel1.Text = "Resultados Validados";
            this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1159, 632);
            this.Controls.Add(this.toolStrip1);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.IsMdiContainer = true;
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbImportar;
        private System.Windows.Forms.ToolStripLabel tslImportar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbValidar;
        private System.Windows.Forms.ToolStripLabel tslValidar;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    }
}