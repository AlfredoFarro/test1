using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BC;
using BE;
using System.Configuration;
using System.IO;
using LumenWorks.Framework.IO.Csv;

namespace TamizajeApp
{
    public partial class ImportarResultados : Form
    {

        #region Atributos

        PruebaBC pruebaBC = new PruebaBC();
        //RangoBC rangoBC = new RangoBC();
        ResultadoBC resultadoBC = new ResultadoBC();
        EnsayoBC ensayoBC = new EnsayoBC();

        readonly string rutaOrigen = ConfigurationManager.AppSettings["carpetaOrigenLocal"];
        readonly string rutaDestino = ConfigurationManager.AppSettings["carpetaDestinoLocal"];

        //string rutaOrigen = ConfigurationManager.AppSettings["carpetaOrigenServerGSP"].ToString();
        //string rutaDestino = ConfigurationManager.AppSettings["carpetaDestinoServerGSP"].ToString();

        List<Prueba> listaPruebas;  // 1 = es el estado que indica pruebas activas.


        #endregion

        public ImportarResultados()
        {
            InitializeComponent();
        }

        private void tsbProcesar_Click(object sender, EventArgs e)
        {
            ProcesarResultados();
        }

        private void tslProcesar_Click(object sender, EventArgs e)
        {
            ProcesarResultados();
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
            dgvResultados.DataSource = string.Empty;
            ObtenerListaArchivos();
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
                            if (int.TryParse(csv["Well"], out well)){
                                resultado.Well = well;
                            }
                            int linea;
                            if (int.TryParse(csv["Linea"], out linea))
                            {
                                resultado.Linea = linea;
                            }
                            resultado.WellA1 = csv["WellA1"];
                            int role;
                            if (int.TryParse(csv["Role"], out role)) {
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
                            if (DateTime.TryParse(csv["Run date"],out rundate)){
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
        
        private void ObtenerListaArchivos()
        {
            FileInfo[] lista = new DirectoryInfo(rutaOrigen).GetFiles().ToArray();
            dgvArchivos.DataSource = lista;
        }
        #endregion

        private void ImportarResultados_Load(object sender, EventArgs e)
        {
            dgvArchivos.AutoGenerateColumns = false;
            ObtenerListaArchivos();
        }

        private void dgvArchivos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvArchivos.CurrentRow != null)
            {
                var filename = dgvArchivos.CurrentRow.Cells["Archivo"].Value.ToString();
                filename = rutaOrigen + filename;

                using (CachedCsvReader csv = new
                CachedCsvReader(new StreamReader(filename), true))
                {
                    // Field headers will automatically be used as column names
                    dgvResultados.DataSource = csv;
                }
            }
        }
    }
}
