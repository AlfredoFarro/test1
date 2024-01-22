namespace TamizajeApp
{
    partial class ValidarResultados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ValidarResultados));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvEnsayos = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscInstrumentos = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbSeleccionar = new System.Windows.Forms.ToolStripButton();
            this.tslSeleccionar = new System.Windows.Forms.ToolStripLabel();
            this.tsbPublicar = new System.Windows.Forms.ToolStripButton();
            this.tslPublicar = new System.Windows.Forms.ToolStripLabel();
            this.Publicar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.idResultado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pocillo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoMuestra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Analito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Concentracion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Linea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResultCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumEnsayo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Instrumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaResultado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prueba = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnsayos)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvEnsayos);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvResultados);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer1.Size = new System.Drawing.Size(1233, 563);
            this.splitContainer1.SplitterDistance = 360;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvEnsayos
            // 
            this.dgvEnsayos.AllowUserToAddRows = false;
            this.dgvEnsayos.AllowUserToDeleteRows = false;
            this.dgvEnsayos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEnsayos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumEnsayo,
            this.ID,
            this.Instrumento,
            this.FechaResultado,
            this.Prueba});
            this.dgvEnsayos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEnsayos.Location = new System.Drawing.Point(0, 33);
            this.dgvEnsayos.Name = "dgvEnsayos";
            this.dgvEnsayos.ReadOnly = true;
            this.dgvEnsayos.RowTemplate.Height = 28;
            this.dgvEnsayos.Size = new System.Drawing.Size(360, 530);
            this.dgvEnsayos.TabIndex = 1;
            this.dgvEnsayos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEnsayos_CellClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tscInstrumentos,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(360, 33);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(113, 30);
            this.toolStripLabel1.Text = "Instrumento:";
            // 
            // tscInstrumentos
            // 
            this.tscInstrumentos.Name = "tscInstrumentos";
            this.tscInstrumentos.Size = new System.Drawing.Size(180, 33);
            this.tscInstrumentos.Click += new System.EventHandler(this.tscInstrumentos_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // dgvResultados
            // 
            this.dgvResultados.AllowUserToAddRows = false;
            this.dgvResultados.AllowUserToDeleteRows = false;
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Publicar,
            this.idResultado,
            this.Pocillo,
            this.CodigoMuestra,
            this.Analito,
            this.Concentracion,
            this.Unidad,
            this.Linea,
            this.ResultCode});
            this.dgvResultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResultados.Location = new System.Drawing.Point(0, 31);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.RowTemplate.Height = 28;
            this.dgvResultados.Size = new System.Drawing.Size(869, 532);
            this.dgvResultados.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSeleccionar,
            this.tslSeleccionar,
            this.tsbPublicar,
            this.tslPublicar});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(869, 31);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbSeleccionar
            // 
            this.tsbSeleccionar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSeleccionar.Image = ((System.Drawing.Image)(resources.GetObject("tsbSeleccionar.Image")));
            this.tsbSeleccionar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSeleccionar.Name = "tsbSeleccionar";
            this.tsbSeleccionar.Size = new System.Drawing.Size(28, 28);
            this.tsbSeleccionar.Text = "toolStripButton1";
            this.tsbSeleccionar.CheckedChanged += new System.EventHandler(this.tsbSeleccionar_CheckedChanged);
            // 
            // tslSeleccionar
            // 
            this.tslSeleccionar.Name = "tslSeleccionar";
            this.tslSeleccionar.Size = new System.Drawing.Size(147, 28);
            this.tslSeleccionar.Text = "Seleccionar Todo";
            // 
            // tsbPublicar
            // 
            this.tsbPublicar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPublicar.Image = ((System.Drawing.Image)(resources.GetObject("tsbPublicar.Image")));
            this.tsbPublicar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPublicar.Name = "tsbPublicar";
            this.tsbPublicar.Size = new System.Drawing.Size(28, 28);
            this.tsbPublicar.Text = "toolStripButton2";
            this.tsbPublicar.Click += new System.EventHandler(this.tsbPublicar_Click);
            // 
            // tslPublicar
            // 
            this.tslPublicar.Name = "tslPublicar";
            this.tslPublicar.Size = new System.Drawing.Size(74, 28);
            this.tslPublicar.Text = "Publicar";
            this.tslPublicar.Click += new System.EventHandler(this.tslPublicar_Click);
            // 
            // Publicar
            // 
            this.Publicar.DataPropertyName = "Publicado";
            this.Publicar.FalseValue = "0";
            this.Publicar.HeaderText = "Publicar";
            this.Publicar.Name = "Publicar";
            this.Publicar.TrueValue = "1";
            // 
            // idResultado
            // 
            this.idResultado.DataPropertyName = "idResultado";
            this.idResultado.HeaderText = "idResultado";
            this.idResultado.Name = "idResultado";
            this.idResultado.ReadOnly = true;
            this.idResultado.Visible = false;
            // 
            // Pocillo
            // 
            this.Pocillo.DataPropertyName = "WellA1";
            this.Pocillo.HeaderText = "Pocillo";
            this.Pocillo.Name = "Pocillo";
            this.Pocillo.ReadOnly = true;
            // 
            // CodigoMuestra
            // 
            this.CodigoMuestra.DataPropertyName = "CodigoMuestra";
            this.CodigoMuestra.HeaderText = "Codigo";
            this.CodigoMuestra.Name = "CodigoMuestra";
            this.CodigoMuestra.ReadOnly = true;
            // 
            // Analito
            // 
            this.Analito.DataPropertyName = "Analyte";
            this.Analito.HeaderText = "Analito";
            this.Analito.Name = "Analito";
            this.Analito.ReadOnly = true;
            // 
            // Concentracion
            // 
            this.Concentracion.DataPropertyName = "Conc";
            this.Concentracion.HeaderText = "Concentracion";
            this.Concentracion.Name = "Concentracion";
            this.Concentracion.ReadOnly = true;
            // 
            // Unidad
            // 
            this.Unidad.DataPropertyName = "Unidad";
            this.Unidad.HeaderText = "Unidad";
            this.Unidad.Name = "Unidad";
            this.Unidad.ReadOnly = true;
            // 
            // Linea
            // 
            this.Linea.DataPropertyName = "Linea";
            this.Linea.HeaderText = "Linea";
            this.Linea.Name = "Linea";
            this.Linea.ReadOnly = true;
            this.Linea.Visible = false;
            // 
            // ResultCode
            // 
            this.ResultCode.DataPropertyName = "ResultCode";
            this.ResultCode.HeaderText = "ResultCode";
            this.ResultCode.Name = "ResultCode";
            // 
            // NumEnsayo
            // 
            this.NumEnsayo.DataPropertyName = "idEnsayo";
            this.NumEnsayo.HeaderText = "idEnsayo";
            this.NumEnsayo.Name = "NumEnsayo";
            this.NumEnsayo.ReadOnly = true;
            this.NumEnsayo.Visible = false;
            this.NumEnsayo.Width = 50;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "RunID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 50;
            // 
            // Instrumento
            // 
            this.Instrumento.DataPropertyName = "Instrument";
            this.Instrumento.HeaderText = "Instrumento";
            this.Instrumento.Name = "Instrumento";
            this.Instrumento.ReadOnly = true;
            // 
            // FechaResultado
            // 
            this.FechaResultado.DataPropertyName = "RunDate";
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.FechaResultado.DefaultCellStyle = dataGridViewCellStyle2;
            this.FechaResultado.FillWeight = 120F;
            this.FechaResultado.HeaderText = "F. Resultado";
            this.FechaResultado.Name = "FechaResultado";
            this.FechaResultado.ReadOnly = true;
            // 
            // Prueba
            // 
            this.Prueba.DataPropertyName = "Prueba";
            this.Prueba.HeaderText = "Prueba";
            this.Prueba.Name = "Prueba";
            this.Prueba.ReadOnly = true;
            this.Prueba.Width = 60;
            // 
            // ValidarResultados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 563);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ValidarResultados";
            this.Text = "ValidarResultados";
            this.Load += new System.EventHandler(this.ValidarResultados_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnsayos)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscInstrumentos;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbSeleccionar;
        private System.Windows.Forms.ToolStripLabel tslSeleccionar;
        private System.Windows.Forms.DataGridView dgvEnsayos;
        private System.Windows.Forms.DataGridView dgvResultados;
        private System.Windows.Forms.ToolStripButton tsbPublicar;
        private System.Windows.Forms.ToolStripLabel tslPublicar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Publicar;
        private System.Windows.Forms.DataGridViewTextBoxColumn idResultado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pocillo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoMuestra;
        private System.Windows.Forms.DataGridViewTextBoxColumn Analito;
        private System.Windows.Forms.DataGridViewTextBoxColumn Concentracion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Linea;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResultCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumEnsayo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Instrumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaResultado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prueba;
    }
}