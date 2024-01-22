namespace TamizajeApp
{
    partial class Principal
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
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnRevisarPublicaciones = new System.Windows.Forms.Button();
            this.btnPublicarResultados = new System.Windows.Forms.Button();
            this.btnImportarResultados = new System.Windows.Forms.Button();
            this.btnSincronizar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.Window;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(182, 556);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TamizajeApp.Properties.Resources.LogoCiansaTiny;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(178, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnRevisarPublicaciones
            // 
            this.btnRevisarPublicaciones.BackColor = System.Drawing.Color.LightGray;
            this.btnRevisarPublicaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevisarPublicaciones.ForeColor = System.Drawing.Color.Black;
            this.btnRevisarPublicaciones.Location = new System.Drawing.Point(3, 211);
            this.btnRevisarPublicaciones.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRevisarPublicaciones.Name = "btnRevisarPublicaciones";
            this.btnRevisarPublicaciones.Size = new System.Drawing.Size(178, 56);
            this.btnRevisarPublicaciones.TabIndex = 9;
            this.btnRevisarPublicaciones.Text = "REVISAR PUBLICACIONES";
            this.btnRevisarPublicaciones.UseVisualStyleBackColor = false;
            this.btnRevisarPublicaciones.Click += new System.EventHandler(this.btnRevisarPublicaciones_Click);
            // 
            // btnPublicarResultados
            // 
            this.btnPublicarResultados.BackColor = System.Drawing.Color.LightGray;
            this.btnPublicarResultados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPublicarResultados.ForeColor = System.Drawing.Color.Black;
            this.btnPublicarResultados.Location = new System.Drawing.Point(3, 148);
            this.btnPublicarResultados.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPublicarResultados.Name = "btnPublicarResultados";
            this.btnPublicarResultados.Size = new System.Drawing.Size(178, 56);
            this.btnPublicarResultados.TabIndex = 8;
            this.btnPublicarResultados.Text = "PUBLICAR RESULTADOS";
            this.btnPublicarResultados.UseVisualStyleBackColor = false;
            this.btnPublicarResultados.Click += new System.EventHandler(this.btnPublicarResultados_Click);
            // 
            // btnImportarResultados
            // 
            this.btnImportarResultados.BackColor = System.Drawing.Color.LightGray;
            this.btnImportarResultados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportarResultados.ForeColor = System.Drawing.Color.Black;
            this.btnImportarResultados.Location = new System.Drawing.Point(3, 85);
            this.btnImportarResultados.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnImportarResultados.Name = "btnImportarResultados";
            this.btnImportarResultados.Size = new System.Drawing.Size(178, 56);
            this.btnImportarResultados.TabIndex = 7;
            this.btnImportarResultados.Text = "IMPORTAR RESULTADOS";
            this.btnImportarResultados.UseVisualStyleBackColor = false;
            this.btnImportarResultados.Click += new System.EventHandler(this.btnImportarResultados_Click);
            // 
            // btnSincronizar
            // 
            this.btnSincronizar.BackColor = System.Drawing.Color.LightGray;
            this.btnSincronizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSincronizar.ForeColor = System.Drawing.Color.Black;
            this.btnSincronizar.Location = new System.Drawing.Point(3, 271);
            this.btnSincronizar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSincronizar.Name = "btnSincronizar";
            this.btnSincronizar.Size = new System.Drawing.Size(178, 56);
            this.btnSincronizar.TabIndex = 11;
            this.btnSincronizar.Text = "SINCRONIZAR DB";
            this.btnSincronizar.UseVisualStyleBackColor = false;
            this.btnSincronizar.Click += new System.EventHandler(this.btnSincronizar_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1118, 556);
            this.Controls.Add(this.btnSincronizar);
            this.Controls.Add(this.btnRevisarPublicaciones);
            this.Controls.Add(this.btnPublicarResultados);
            this.Controls.Add(this.btnImportarResultados);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.splitter1);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Principal";
            this.Text = "TamiLifeDesktop";
            this.Load += new System.EventHandler(this.Principal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnRevisarPublicaciones;
        private System.Windows.Forms.Button btnPublicarResultados;
        private System.Windows.Forms.Button btnImportarResultados;
        private System.Windows.Forms.Button btnSincronizar;




    }
}