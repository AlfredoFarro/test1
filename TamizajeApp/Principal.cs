using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TamizajeApp.Resultados;

namespace TamizajeApp
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }


        private void Principal_Load(object sender, EventArgs e)
        {

        }

        private void btnImportarResultados_Click(object sender, EventArgs e)
        {
            ImportarResultados importar = new ImportarResultados();
            importar.MdiParent = this;
            importar.WindowState = FormWindowState.Maximized;
            importar.Show();
        }

        private void btnRevisarPublicaciones_Click(object sender, EventArgs e)
        {
           
        }

        private void btnPublicarResultados_Click(object sender, EventArgs e)
        {
            PublicarResultados validar = new PublicarResultados();
            validar.MdiParent = this;
            validar.WindowState = FormWindowState.Maximized;
            validar.Show();
        }

        private void btnSincronizar_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
