using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using TamizajeApp.Exportar;

namespace TamizajeApp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void tslImportar_Click(object sender, EventArgs e)
        {
            AbrirImportarResultados();
        }

        private void tslValidar_Click(object sender, EventArgs e)
        {
            AbritValidarResultados();
        }

        private void tsbImportar_Click(object sender, EventArgs e)
        {
            AbrirImportarResultados();
        }

        private void tsbValidar_Click(object sender, EventArgs e)
        {
            AbritValidarResultados();
        }

        private void AbrirImportarResultados()
        {
            ImportarResultados importar = new ImportarResultados();
            importar.MdiParent = this;
            importar.Show();
        }

        private void AbritValidarResultados()
        {
            ValidarResultados validar = new ValidarResultados();
            validar.MdiParent = this;
            validar.Show();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            //ExportarDBF expor = new ExportarDBF();
            //expor.MdiParent = this;
            //expor.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

    }
}
