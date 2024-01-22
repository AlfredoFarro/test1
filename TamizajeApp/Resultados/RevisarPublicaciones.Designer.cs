namespace TamizajeApp.Resultados
{
    partial class RevisarPublicaciones
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbInstrumento = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPrueba = new System.Windows.Forms.Label();
            this.cmbPrueba = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dgvEnsayos = new System.Windows.Forms.DataGridView();
            this.NumEnsayo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Instrumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prueba = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaResultado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaPublicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnsayos)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dateTimePicker1);
            this.splitContainer1.Panel1.Controls.Add(this.cmbPrueba);
            this.splitContainer1.Panel1.Controls.Add(this.lblPrueba);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.cmbInstrumento);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnBuscar);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvEnsayos);
            this.splitContainer1.Size = new System.Drawing.Size(1258, 665);
            this.splitContainer1.SplitterDistance = 80;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Gray;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.SystemColors.Window;
            this.btnBuscar.Location = new System.Drawing.Point(903, 16);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(106, 36);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Instrumento:";
            // 
            // cmbInstrumento
            // 
            this.cmbInstrumento.FormattingEnabled = true;
            this.cmbInstrumento.Location = new System.Drawing.Point(134, 21);
            this.cmbInstrumento.Name = "cmbInstrumento";
            this.cmbInstrumento.Size = new System.Drawing.Size(150, 28);
            this.cmbInstrumento.TabIndex = 4;
            this.cmbInstrumento.Text = "GSP-20210224";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(571, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Fecha Resultado:";
            // 
            // lblPrueba
            // 
            this.lblPrueba.AutoSize = true;
            this.lblPrueba.Location = new System.Drawing.Point(336, 24);
            this.lblPrueba.Name = "lblPrueba";
            this.lblPrueba.Size = new System.Drawing.Size(64, 20);
            this.lblPrueba.TabIndex = 8;
            this.lblPrueba.Text = "Prueba:";
            // 
            // cmbPrueba
            // 
            this.cmbPrueba.FormattingEnabled = true;
            this.cmbPrueba.Location = new System.Drawing.Point(406, 21);
            this.cmbPrueba.Name = "cmbPrueba";
            this.cmbPrueba.Size = new System.Drawing.Size(120, 28);
            this.cmbPrueba.TabIndex = 9;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(712, 24);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(150, 26);
            this.dateTimePicker1.TabIndex = 10;
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
            this.dgvEnsayos.Name = "dgvEnsayos";
            this.dgvEnsayos.ReadOnly = true;
            this.dgvEnsayos.RowTemplate.Height = 28;
            this.dgvEnsayos.Size = new System.Drawing.Size(1258, 581);
            this.dgvEnsayos.TabIndex = 3;
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
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.FechaResultado.DefaultCellStyle = dataGridViewCellStyle2;
            this.FechaResultado.FillWeight = 120F;
            this.FechaResultado.HeaderText = "F. Resultado";
            this.FechaResultado.Name = "FechaResultado";
            this.FechaResultado.ReadOnly = true;
            // 
            // FechaPublicacion
            // 
            this.FechaPublicacion.HeaderText = "F. Publicación";
            this.FechaPublicacion.Name = "FechaPublicacion";
            this.FechaPublicacion.ReadOnly = true;
            // 
            // RevisarPublicaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1258, 665);
            this.Controls.Add(this.splitContainer1);
            this.Name = "RevisarPublicaciones";
            this.Text = "Revisar Publicaciones";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnsayos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox cmbPrueba;
        private System.Windows.Forms.Label lblPrueba;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbInstrumento;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvEnsayos;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumEnsayo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Instrumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prueba;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaResultado;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaPublicacion;
    }
}