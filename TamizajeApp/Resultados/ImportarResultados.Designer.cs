namespace TamizajeApp
{
    partial class ImportarResultados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportarResultados));
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.dgvArchivos = new System.Windows.Forms.DataGridView();
            this.Archivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbProcesar = new System.Windows.Forms.ToolStripButton();
            this.tslProcesar = new System.Windows.Forms.ToolStripLabel();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArchivos)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.dgvArchivos);
            this.splitContainerMain.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.dgvResultados);
            this.splitContainerMain.Size = new System.Drawing.Size(955, 656);
            this.splitContainerMain.SplitterDistance = 207;
            this.splitContainerMain.TabIndex = 0;
            // 
            // dgvArchivos
            // 
            this.dgvArchivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArchivos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Archivo});
            this.dgvArchivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvArchivos.Location = new System.Drawing.Point(0, 31);
            this.dgvArchivos.Name = "dgvArchivos";
            this.dgvArchivos.RowTemplate.Height = 28;
            this.dgvArchivos.Size = new System.Drawing.Size(207, 625);
            this.dgvArchivos.TabIndex = 1;
            this.dgvArchivos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvArchivos_CellClick);
            // 
            // Archivo
            // 
            this.Archivo.DataPropertyName = "Name";
            this.Archivo.HeaderText = "Archivo";
            this.Archivo.Name = "Archivo";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbProcesar,
            this.tslProcesar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(207, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbProcesar
            // 
            this.tsbProcesar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbProcesar.Image = ((System.Drawing.Image)(resources.GetObject("tsbProcesar.Image")));
            this.tsbProcesar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbProcesar.Name = "tsbProcesar";
            this.tsbProcesar.Size = new System.Drawing.Size(28, 28);
            this.tsbProcesar.Text = "toolStripButton1";
            this.tsbProcesar.Click += new System.EventHandler(this.tsbProcesar_Click);
            // 
            // tslProcesar
            // 
            this.tslProcesar.Name = "tslProcesar";
            this.tslProcesar.Size = new System.Drawing.Size(79, 28);
            this.tslProcesar.Text = "Procesar";
            this.tslProcesar.Click += new System.EventHandler(this.tslProcesar_Click);
            // 
            // dgvResultados
            // 
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResultados.Location = new System.Drawing.Point(0, 0);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.RowTemplate.Height = 28;
            this.dgvResultados.Size = new System.Drawing.Size(744, 656);
            this.dgvResultados.TabIndex = 0;
            // 
            // ImportarResultados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 656);
            this.Controls.Add(this.splitContainerMain);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "ImportarResultados";
            this.Text = "ImportarResultados";
            this.Load += new System.EventHandler(this.ImportarResultados_Load);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArchivos)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.DataGridView dgvArchivos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Archivo;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbProcesar;
        private System.Windows.Forms.ToolStripLabel tslProcesar;
        private System.Windows.Forms.DataGridView dgvResultados;
    }
}