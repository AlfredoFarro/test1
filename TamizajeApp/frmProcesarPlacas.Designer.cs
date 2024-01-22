namespace TamizajeApp
{
    partial class frmProcesarPlacas
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
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerLista = new System.Windows.Forms.SplitContainer();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLista)).BeginInit();
            this.splitContainerLista.Panel1.SuspendLayout();
            this.splitContainerLista.Panel2.SuspendLayout();
            this.splitContainerLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerLista);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.dgvResultados);
            this.splitContainerMain.Size = new System.Drawing.Size(1075, 630);
            this.splitContainerMain.SplitterDistance = 158;
            this.splitContainerMain.SplitterWidth = 5;
            this.splitContainerMain.TabIndex = 0;
            // 
            // splitContainerLista
            // 
            this.splitContainerLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLista.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerLista.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLista.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainerLista.Name = "splitContainerLista";
            this.splitContainerLista.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerLista.Panel1
            // 
            this.splitContainerLista.Panel1.Controls.Add(this.btnProcesar);
            // 
            // splitContainerLista.Panel2
            // 
            this.splitContainerLista.Panel2.Controls.Add(this.listBoxFiles);
            this.splitContainerLista.Size = new System.Drawing.Size(158, 630);
            this.splitContainerLista.SplitterDistance = 49;
            this.splitContainerLista.SplitterWidth = 5;
            this.splitContainerLista.TabIndex = 0;
            // 
            // btnProcesar
            // 
            this.btnProcesar.Location = new System.Drawing.Point(31, 13);
            this.btnProcesar.Margin = new System.Windows.Forms.Padding(4);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(100, 28);
            this.btnProcesar.TabIndex = 0;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.ItemHeight = 16;
            this.listBoxFiles.Location = new System.Drawing.Point(4, 4);
            this.listBoxFiles.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(151, 548);
            this.listBoxFiles.TabIndex = 1;
            this.listBoxFiles.SelectedIndexChanged += new System.EventHandler(this.listBoxFiles_SelectedIndexChanged);
            // 
            // dgvResultados
            // 
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResultados.Location = new System.Drawing.Point(0, 0);
            this.dgvResultados.Margin = new System.Windows.Forms.Padding(4);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.RowTemplate.Height = 24;
            this.dgvResultados.Size = new System.Drawing.Size(912, 630);
            this.dgvResultados.TabIndex = 0;
            // 
            // frmProcesarPlacas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 630);
            this.Controls.Add(this.splitContainerMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmProcesarPlacas";
            this.Text = "PROCESAR PLACAS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmProcesarPlacas_Load);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerLista.Panel1.ResumeLayout(false);
            this.splitContainerLista.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLista)).EndInit();
            this.splitContainerLista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.DataGridView dgvResultados;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.SplitContainer splitContainerLista;
    }
}