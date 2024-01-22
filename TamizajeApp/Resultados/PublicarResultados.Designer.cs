namespace TamizajeApp.Resultados
{
    partial class PublicarResultados
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.spcMain = new System.Windows.Forms.SplitContainer();
            this.dtpFechaResultadoFinal = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbEstadoPublicacion = new System.Windows.Forms.ComboBox();
            this.btnPublicar = new System.Windows.Forms.Button();
            this.dtpFechaResultadoInicio = new System.Windows.Forms.DateTimePicker();
            this.cmbPrueba = new System.Windows.Forms.ComboBox();
            this.lblPrueba = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.spcEnsayos = new System.Windows.Forms.SplitContainer();
            this.dgvEnsayos = new System.Windows.Forms.DataGridView();
            this.NumEnsayo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Instrumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prueba = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaResultado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaPublicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spcResultados = new System.Windows.Forms.SplitContainer();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.Publicar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.idResultado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pocillo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoMuestra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Analito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Conc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Linea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResultCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.spcMain)).BeginInit();
            this.spcMain.Panel1.SuspendLayout();
            this.spcMain.Panel2.SuspendLayout();
            this.spcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcEnsayos)).BeginInit();
            this.spcEnsayos.Panel1.SuspendLayout();
            this.spcEnsayos.Panel2.SuspendLayout();
            this.spcEnsayos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnsayos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spcResultados)).BeginInit();
            this.spcResultados.Panel1.SuspendLayout();
            this.spcResultados.Panel2.SuspendLayout();
            this.spcResultados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.SuspendLayout();
            // 
            // spcMain
            // 
            this.spcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spcMain.Location = new System.Drawing.Point(0, 0);
            this.spcMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.spcMain.Name = "spcMain";
            this.spcMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcMain.Panel1
            // 
            this.spcMain.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.spcMain.Panel1.Controls.Add(this.dtpFechaResultadoFinal);
            this.spcMain.Panel1.Controls.Add(this.label1);
            this.spcMain.Panel1.Controls.Add(this.cmbEstadoPublicacion);
            this.spcMain.Panel1.Controls.Add(this.btnPublicar);
            this.spcMain.Panel1.Controls.Add(this.dtpFechaResultadoInicio);
            this.spcMain.Panel1.Controls.Add(this.cmbPrueba);
            this.spcMain.Panel1.Controls.Add(this.lblPrueba);
            this.spcMain.Panel1.Controls.Add(this.label4);
            this.spcMain.Panel1.Controls.Add(this.btnBuscar);
            // 
            // spcMain.Panel2
            // 
            this.spcMain.Panel2.Controls.Add(this.spcEnsayos);
            this.spcMain.Size = new System.Drawing.Size(1118, 532);
            this.spcMain.SplitterDistance = 60;
            this.spcMain.SplitterWidth = 3;
            this.spcMain.TabIndex = 0;
            // 
            // dtpFechaResultadoFinal
            // 
            this.dtpFechaResultadoFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaResultadoFinal.Location = new System.Drawing.Point(735, 8);
            this.dtpFechaResultadoFinal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpFechaResultadoFinal.Name = "dtpFechaResultadoFinal";
            this.dtpFechaResultadoFinal.ShowCheckBox = true;
            this.dtpFechaResultadoFinal.Size = new System.Drawing.Size(134, 22);
            this.dtpFechaResultadoFinal.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "Estado:";
            // 
            // cmbEstadoPublicacion
            // 
            this.cmbEstadoPublicacion.FormattingEnabled = true;
            this.cmbEstadoPublicacion.Location = new System.Drawing.Point(83, 7);
            this.cmbEstadoPublicacion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbEstadoPublicacion.Name = "cmbEstadoPublicacion";
            this.cmbEstadoPublicacion.Size = new System.Drawing.Size(160, 24);
            this.cmbEstadoPublicacion.TabIndex = 20;
            // 
            // btnPublicar
            // 
            this.btnPublicar.BackColor = System.Drawing.Color.Gray;
            this.btnPublicar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPublicar.ForeColor = System.Drawing.SystemColors.Window;
            this.btnPublicar.Location = new System.Drawing.Point(993, 4);
            this.btnPublicar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPublicar.Name = "btnPublicar";
            this.btnPublicar.Size = new System.Drawing.Size(94, 29);
            this.btnPublicar.TabIndex = 1;
            this.btnPublicar.Text = "Publicar";
            this.btnPublicar.UseVisualStyleBackColor = false;
            this.btnPublicar.Click += new System.EventHandler(this.btnPublicar_Click);
            // 
            // dtpFechaResultadoInicio
            // 
            this.dtpFechaResultadoInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaResultadoInicio.Location = new System.Drawing.Point(587, 8);
            this.dtpFechaResultadoInicio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpFechaResultadoInicio.Name = "dtpFechaResultadoInicio";
            this.dtpFechaResultadoInicio.ShowCheckBox = true;
            this.dtpFechaResultadoInicio.Size = new System.Drawing.Size(134, 22);
            this.dtpFechaResultadoInicio.TabIndex = 17;
            // 
            // cmbPrueba
            // 
            this.cmbPrueba.FormattingEnabled = true;
            this.cmbPrueba.Location = new System.Drawing.Point(330, 7);
            this.cmbPrueba.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbPrueba.Name = "cmbPrueba";
            this.cmbPrueba.Size = new System.Drawing.Size(107, 24);
            this.cmbPrueba.TabIndex = 16;
            // 
            // lblPrueba
            // 
            this.lblPrueba.AutoSize = true;
            this.lblPrueba.Location = new System.Drawing.Point(258, 10);
            this.lblPrueba.Name = "lblPrueba";
            this.lblPrueba.Size = new System.Drawing.Size(58, 17);
            this.lblPrueba.TabIndex = 15;
            this.lblPrueba.Text = "Prueba:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(452, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Fecha Resultado:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Gray;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.SystemColors.Window;
            this.btnBuscar.Location = new System.Drawing.Point(884, 4);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(94, 29);
            this.btnBuscar.TabIndex = 11;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // spcEnsayos
            // 
            this.spcEnsayos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcEnsayos.Location = new System.Drawing.Point(0, 0);
            this.spcEnsayos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.spcEnsayos.Name = "spcEnsayos";
            // 
            // spcEnsayos.Panel1
            // 
            this.spcEnsayos.Panel1.Controls.Add(this.dgvEnsayos);
            // 
            // spcEnsayos.Panel2
            // 
            this.spcEnsayos.Panel2.Controls.Add(this.spcResultados);
            this.spcEnsayos.Size = new System.Drawing.Size(1118, 469);
            this.spcEnsayos.SplitterDistance = 319;
            this.spcEnsayos.TabIndex = 0;
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
            this.Prueba,
            this.FechaResultado,
            this.FechaPublicacion});
            this.dgvEnsayos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEnsayos.Location = new System.Drawing.Point(0, 0);
            this.dgvEnsayos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvEnsayos.Name = "dgvEnsayos";
            this.dgvEnsayos.ReadOnly = true;
            this.dgvEnsayos.RowTemplate.Height = 28;
            this.dgvEnsayos.Size = new System.Drawing.Size(319, 469);
            this.dgvEnsayos.TabIndex = 2;
            this.dgvEnsayos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEnsayos_CellClick);
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
            this.ID.Width = 40;
            // 
            // Instrumento
            // 
            this.Instrumento.DataPropertyName = "Instrument";
            this.Instrumento.HeaderText = "Instrumento";
            this.Instrumento.Name = "Instrumento";
            this.Instrumento.ReadOnly = true;
            // 
            // Prueba
            // 
            this.Prueba.DataPropertyName = "Prueba";
            this.Prueba.HeaderText = "Prueba";
            this.Prueba.Name = "Prueba";
            this.Prueba.ReadOnly = true;
            this.Prueba.Width = 60;
            // 
            // FechaResultado
            // 
            this.FechaResultado.DataPropertyName = "RunDate";
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.FechaResultado.DefaultCellStyle = dataGridViewCellStyle1;
            this.FechaResultado.FillWeight = 120F;
            this.FechaResultado.HeaderText = "F. Result";
            this.FechaResultado.Name = "FechaResultado";
            this.FechaResultado.ReadOnly = true;
            this.FechaResultado.Width = 80;
            // 
            // FechaPublicacion
            // 
            this.FechaPublicacion.DataPropertyName = "FechaPublicacion";
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.FechaPublicacion.DefaultCellStyle = dataGridViewCellStyle2;
            this.FechaPublicacion.FillWeight = 120F;
            this.FechaPublicacion.HeaderText = "F. Public";
            this.FechaPublicacion.Name = "FechaPublicacion";
            this.FechaPublicacion.ReadOnly = true;
            this.FechaPublicacion.Width = 80;
            // 
            // spcResultados
            // 
            this.spcResultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcResultados.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spcResultados.Location = new System.Drawing.Point(0, 0);
            this.spcResultados.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.spcResultados.Name = "spcResultados";
            this.spcResultados.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcResultados.Panel1
            // 
            this.spcResultados.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.spcResultados.Panel1.Controls.Add(this.chkAll);
            // 
            // spcResultados.Panel2
            // 
            this.spcResultados.Panel2.Controls.Add(this.dgvResultados);
            this.spcResultados.Size = new System.Drawing.Size(795, 469);
            this.spcResultados.SplitterDistance = 40;
            this.spcResultados.SplitterWidth = 3;
            this.spcResultados.TabIndex = 0;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(54, 7);
            this.chkAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(118, 21);
            this.chkAll.TabIndex = 0;
            this.chkAll.Text = "Marcar Todos";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
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
            this.Conc,
            this.Unidad,
            this.Linea,
            this.ResultCode});
            this.dgvResultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResultados.Location = new System.Drawing.Point(0, 0);
            this.dgvResultados.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.RowTemplate.Height = 28;
            this.dgvResultados.Size = new System.Drawing.Size(795, 426);
            this.dgvResultados.TabIndex = 2;
            // 
            // Publicar
            // 
            this.Publicar.DataPropertyName = "Publicado";
            this.Publicar.FalseValue = "false";
            this.Publicar.HeaderText = "Publicar";
            this.Publicar.Name = "Publicar";
            this.Publicar.TrueValue = "true";
            this.Publicar.Width = 60;
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
            this.Pocillo.Width = 60;
            // 
            // CodigoMuestra
            // 
            this.CodigoMuestra.DataPropertyName = "CodigoMuestra";
            this.CodigoMuestra.HeaderText = "Codigo";
            this.CodigoMuestra.Name = "CodigoMuestra";
            this.CodigoMuestra.ReadOnly = true;
            this.CodigoMuestra.Width = 80;
            // 
            // Analito
            // 
            this.Analito.DataPropertyName = "Analyte";
            this.Analito.HeaderText = "Analito";
            this.Analito.Name = "Analito";
            this.Analito.ReadOnly = true;
            this.Analito.Width = 60;
            // 
            // Conc
            // 
            this.Conc.DataPropertyName = "Conc";
            this.Conc.HeaderText = "Conc";
            this.Conc.Name = "Conc";
            this.Conc.ReadOnly = true;
            this.Conc.Width = 60;
            // 
            // Unidad
            // 
            this.Unidad.DataPropertyName = "Unidad";
            this.Unidad.HeaderText = "Unidad";
            this.Unidad.Name = "Unidad";
            this.Unidad.ReadOnly = true;
            this.Unidad.Width = 60;
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
            // PublicarResultados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 532);
            this.Controls.Add(this.spcMain);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "PublicarResultados";
            this.Text = "Publicar Resultados";
            this.Load += new System.EventHandler(this.PublicarResultados_Load);
            this.spcMain.Panel1.ResumeLayout(false);
            this.spcMain.Panel1.PerformLayout();
            this.spcMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcMain)).EndInit();
            this.spcMain.ResumeLayout(false);
            this.spcEnsayos.Panel1.ResumeLayout(false);
            this.spcEnsayos.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcEnsayos)).EndInit();
            this.spcEnsayos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnsayos)).EndInit();
            this.spcResultados.Panel1.ResumeLayout(false);
            this.spcResultados.Panel1.PerformLayout();
            this.spcResultados.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcResultados)).EndInit();
            this.spcResultados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spcMain;
        private System.Windows.Forms.SplitContainer spcResultados;
        private System.Windows.Forms.Button btnPublicar;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.DataGridView dgvEnsayos;
        private System.Windows.Forms.DataGridView dgvResultados;
        private System.Windows.Forms.SplitContainer spcEnsayos;
        private System.Windows.Forms.DateTimePicker dtpFechaResultadoInicio;
        private System.Windows.Forms.ComboBox cmbPrueba;
        private System.Windows.Forms.Label lblPrueba;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEstadoPublicacion;
        private System.Windows.Forms.DateTimePicker dtpFechaResultadoFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumEnsayo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Instrumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prueba;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaResultado;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaPublicacion;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Publicar;
        private System.Windows.Forms.DataGridViewTextBoxColumn idResultado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pocillo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoMuestra;
        private System.Windows.Forms.DataGridViewTextBoxColumn Analito;
        private System.Windows.Forms.DataGridViewTextBoxColumn Conc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Linea;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResultCode;

    }
}