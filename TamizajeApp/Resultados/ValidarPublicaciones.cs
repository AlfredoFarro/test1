using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BE;
using BC;
using System.Configuration;
using System.IO;
using LumenWorks.Framework.IO.Csv;

namespace TamizajeApp
{
    public partial class ValidarResultados : Form
    {

        ParametroGeneralBC parametroGeneralBC = new ParametroGeneralBC();
        EnsayoBC ensayoBC = new EnsayoBC();
        ResultadoBC resultadoBC = new ResultadoBC();
        
        int idEnsayo;

        //Utilizado para procesar resultados
        readonly string rutaOrigen = ConfigurationManager.AppSettings["carpetaOrigenLocal"];
        readonly string rutaDestino = ConfigurationManager.AppSettings["carpetaDestinoLocal"];
        PruebaBC pruebaBC = new PruebaBC();
        List<Prueba> listaPruebas; 
         

        public ValidarResultados()
        {
            InitializeComponent();
            dgvEnsayos.AutoGenerateColumns = false;
            dgvResultados.AutoGenerateColumns = false;
        }

        private void tscInstrumentos_Click(object sender, EventArgs e)
        {

        }

        private void ValidarResultados_Load(object sender, EventArgs e)
        {
            tsbSeleccionar.CheckOnClick = true;
            LlenarInstrumentos();
            LlenarEnsayosNoPublicados();
        }

        private void LlenarInstrumentos()
        {
            List<ParametroGeneral> instrumentos = parametroGeneralBC.ListaInstrumentos();
            tscInstrumentos.ComboBox.DataSource = instrumentos;
            tscInstrumentos.ComboBox.ValueMember = "ValorTexto";
            tscInstrumentos.ComboBox.DisplayMember = "ValorTexto";
        }

        private void LlenarEnsayosNoPublicados()
        {
            dgvEnsayos.DataSource = ensayoBC.ListaEnsayos(1);

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
                    LlenarResultados(idEnsayo,1);
                }
                
            }
            
        }

        private void tsbSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvResultados.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
              
                //chk.Value = !(chk.Value);
                if (tsbSeleccionar.Checked)
                {
                    chk.Value = true;
                }
                else
                {
                    //chk.Selected = false;
                    chk.Value = false;
                }
            }


        }

        private void tsbPublicar_Click(object sender, EventArgs e)
        {
            PublicarResultados();
        }

        private void PublicarResultados()
        {
            List<int> listaCodigosNoPublicados = new List<int>();
            //List<string> listaPocillos = new List<string>();

            foreach (DataGridViewRow row in dgvResultados.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                //chk.Selected = true;
                if (!bool.Parse(chk.Value.ToString()))
                {
                    int id = Int32.Parse(row.Cells["idResultado"].Value.ToString());
                    listaCodigosNoPublicados.Add(id);
                }
                //if (chk.Value.ToString().CompareTo("0") == 0)
                //{
                //    int id = Int32.Parse(row.Cells["idResultado"].Value.ToString());
                //    //string pocillo = row.Cells["Pocillo"].Value.ToString();
                //    listaCodigosNoPublicados.Add(id);
                //    //ListaPocillos.Add(pocillo);
                //}
            }

            //List<Resultado> res = new List<Resultado>();
            try
            {
                resultadoBC.PublicarResultados(listaCodigosNoPublicados, idEnsayo);
                MessageBox.Show("Ensayo ID: " + idEnsayo + " correctamente publicado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Procesar Resultados de GSP a BD
        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            ProcesarResultados();
            LlenarInstrumentos();
            LlenarEnsayosNoPublicados();
        }
        private void ProcesarResultados()
        {
            string[] archivos = Directory.GetFiles(rutaOrigen);
            foreach (string archivo in archivos)
            {
                //listaPruebas = pruebaBC.ObtenerPruebas(1);
                RegistrarArchivoResultado(archivo);
                MoverArchivo(archivo);
                int numEnsayo;
                //int.TryParse()
            }
            //dgvResultados.DataSource = string.Empty;
            //ObtenerListaArchivos();
        }

        #region Metodos
        private void MoverArchivo(string archivo)
        {
            string name = Path.GetFileName(archivo);
            string destino = rutaDestino + name;

            File.Move(archivo, destino);
        }

        private void RegistrarArchivoResultado(string archivo)
        {

            string name = Path.GetFileNameWithoutExtension(archivo);
            if (name != null)
            {
                int numEnsayo = Int32.Parse(name);

                Ensayo ensayo = new Ensayo();
                Prueba prueba = new Prueba();
                bool tengoEnsayo = false;
                bool tengoPrueba = false;
                int idEnsayo = 0;
                using (CsvReader csv = new CsvReader(new StreamReader(archivo), true))
                {
                    //int fieldCount = csv.FieldCount;
                    //string[] headers = csv.GetFieldHeaders();

                    List<Resultado> listaResultados = new List<Resultado>();



                    while (csv.ReadNextRecord())
                    {

                        if (csv["Role"].CompareTo("3") == 0)
                        {
                            Resultado resultado = new Resultado();

                            if (!tengoPrueba)
                            {
                                prueba = listaPruebas.Find(p => p.NombreCorto == csv["Analyte"]);
                                tengoPrueba = true;
                            }

                            resultado.Analyte = prueba.NombreCorto;
                            //resultado.Test = prueba.Nombre;
                            resultado.Unidad = prueba.Unidad;
                            //resultado.Rango = prueba.Rango;
                            //resultado.Metodo = prueba.Metodo;
                            resultado.CodigoMuestra = csv["Code"].Replace("=", "").Replace("\"", "");
                            resultado.PlateCode = csv["PlateCode"].Replace("=", "").Replace("\"", "");
                            int well;
                            if (int.TryParse(csv["Well"], out well))
                            {
                                resultado.Well = well;
                            }
                            int linea;
                            if (int.TryParse(csv["Linea"], out linea))
                            {
                                resultado.Linea = linea;
                            }
                            resultado.WellA1 = csv["WellA1"];
                            int role;
                            if (int.TryParse(csv["Role"], out role))
                            {
                                resultado.Role = role;
                            }
                            int counts;
                            if (int.TryParse(csv["Counts"], out counts))
                            {
                                resultado.Counts = counts;
                            }
                            int status;
                            if (int.TryParse(csv["Status"], out status))
                            {
                                resultado.Status = status;
                            }
                            float conc;
                            if (float.TryParse(csv["ConcValue"], out conc))
                            {
                                resultado.ConcValue = conc;
                            }
                            int plate;
                            if (int.TryParse(csv["Plate"], out plate))
                            {
                                resultado.Plate = plate;
                            }

                            resultado.Instrument = csv["Instrument"];
                            resultado.Reportar = 0;
                            resultado.KitLot = csv["Kitlot"].Replace("=", "").Replace("\"", "");
                            resultado.Conc = csv["Conc"];
                            resultado.ResultCode = csv["Result Code"];
                            resultado.RunID = numEnsayo;
                            resultado.Flag = csv["Flag"];
                            DateTime rundate;
                            if (DateTime.TryParse(csv["Run date"], out rundate))
                            {
                                resultado.RunDate = rundate;
                            }

                            resultado.Estado = 1;
                            //resultado.isGSP = 0;
                            //resultado.FechaRegistro = DateTime.Today;
                            if (!tengoEnsayo)
                            {
                                ensayo.Instrument = resultado.Instrument;
                                ensayo.Kitlot = resultado.KitLot;
                                ensayo.idPrueba = prueba.idPrueba;
                                ensayo.Prueba = prueba.NombreCorto;
                                ensayo.RunDate = resultado.RunDate;
                                ensayo.RunID = resultado.RunID;
                                ensayo.Estado = 1;

                                ensayoBC.InsertarEnsayo(ensayo);

                                idEnsayo = ensayo.idEnsayo;
                                tengoEnsayo = true;
                            }
                            resultado.idEnsayo = idEnsayo;
                            listaResultados.Add(resultado);
                        }
                    }


                    if (listaResultados.Count > 0)
                    {
                        resultadoBC.RegistrarResultados(listaResultados);
                        tengoPrueba = false;
                        //  placaBC.registrarPlaca(placa);
                    }


                }
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {

        }

        private void tslPublicar_Click(object sender, EventArgs e)
        {
            PublicarResultados();
        }

        //private void ObtenerListaArchivos()
        //{
        //    FileInfo[] lista = new DirectoryInfo(rutaOrigen).GetFiles().ToArray();
        //    dgvArchivos.DataSource = lista;
        //}
        #endregion
    }
}
