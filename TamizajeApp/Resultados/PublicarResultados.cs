using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BC;
using BE;

namespace TamizajeApp.Resultados
{
    public partial class PublicarResultados : Form
    {
        readonly EnsayoBC ensayoBC = new EnsayoBC();
        readonly ResultadoBC resultadoBC = new ResultadoBC();
        readonly ParametroGeneralBC parametroGeneralBC = new ParametroGeneralBC();
        readonly PruebaBC pruebaBC = new PruebaBC();

        int idEnsayo;
       

        public PublicarResultados()
        {
            InitializeComponent();
            dgvEnsayos.AutoGenerateColumns = false;
            dgvResultados.AutoGenerateColumns = false;
        }

        private void PublicarResultados_Load(object sender, EventArgs e)
        {
            LlenarEnsayosNoPublicados();
            LlenarComboBoxEstado();
            LlenarComboBocPruebas();
            dtpFechaResultadoInicio.Checked = false;
            dtpFechaResultadoFinal.Checked = false;
        }

        private void LlenarComboBocPruebas()
        {
            List<Prueba> listaPruebas = pruebaBC.ObtenerPruebasCombo();
            cmbPrueba.DataSource = listaPruebas;
            cmbPrueba.ValueMember = "idPrueba";
            cmbPrueba.DisplayMember = "NombreCorto";

            cmbPrueba.SelectedValue = 0;
        }

        private void LlenarComboBoxEstado()
        {
            List<ParametroGeneral> ListaEstadosPublicacion = parametroGeneralBC.ListaEstadosPublicacion();
            
            //List<ParametroGeneral> EstadosPublicacion = parametroGeneralBC.ListaInstrumentos();
            cmbEstadoPublicacion.DataSource = ListaEstadosPublicacion;
            cmbEstadoPublicacion.ValueMember = "ValorEntero";
            cmbEstadoPublicacion.DisplayMember = "ValorTexto";

            cmbEstadoPublicacion.SelectedValue = 0;
        }
        private void LlenarEnsayosNoPublicados()
        {
            dgvEnsayos.DataSource = ensayoBC.ObtenerEnsayosNoPublicados();

        }
        private void LlenarResultados(int idEnsayo, int estado)
        {
            dgvResultados.DataSource = resultadoBC.ListaResultadosNoPublicados(idEnsayo);
        }

        private void dgvEnsayos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEnsayos.CurrentRow != null)
            {
                int idEnsayoAux = int.Parse(dgvEnsayos.CurrentRow.Cells["NumEnsayo"].Value.ToString());
                if (idEnsayoAux != idEnsayo)
                {
                    idEnsayo = idEnsayoAux;
                    LlenarResultados(idEnsayo, 1);
                }

            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvResultados.Rows)
            {
                var  chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = chkAll.Checked;
            }
        }

        private void btnPublicar_Click(object sender, EventArgs e)
        {
            PublicarSeleccion();
        }

        private void PublicarSeleccion()
        {
            List<int> listaIdNoPublicados = new List<int>();

            foreach (DataGridViewRow row in dgvResultados.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                
                if (!bool.Parse(chk.Value.ToString()))
                {
                    int id = Int32.Parse(row.Cells["idResultado"].Value.ToString());
                    listaIdNoPublicados.Add(id);
                }
            }
            try
            {
                resultadoBC.PublicarResultados(listaIdNoPublicados, idEnsayo);
                ensayoBC.PublicarEnsayo(idEnsayo);
                MessageBox.Show("Ensayo ID: " + idEnsayo + " correctamente publicado");
            }
            catch (Exception e)
            {
                MessageBox.Show("Problemas en la publicación error: " + e.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            bool usarFechaInicial = dtpFechaResultadoInicio.Checked;
            bool usarFechaFinal = dtpFechaResultadoFinal.Checked;
            bool estadoPublicado = (int.Parse(cmbEstadoPublicacion.SelectedValue.ToString()) != 0);
            int idPrueba;
            idPrueba = int.Parse(cmbPrueba.SelectedValue.ToString());
            DateTime fechaResultadoInicial = dtpFechaResultadoInicio.Value;
            DateTime fechaResultadoFinal = dtpFechaResultadoFinal.Value;

            List<Ensayo> listaEnsayo = ensayoBC.ObtenerEnsayos(estadoPublicado,usarFechaInicial,usarFechaFinal,idPrueba,fechaResultadoInicial,fechaResultadoFinal);
            dgvEnsayos.DataSource = listaEnsayo;
            //dgvEnsayos

        }

        private void txtIdEnsayo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            //{
            //    e.Handled = true;
            //}

            //// only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
            
        }
      
    }
}
