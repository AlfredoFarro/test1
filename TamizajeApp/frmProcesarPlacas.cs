using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LumenWorks;
using LumenWorks.Framework.IO.Csv;
using System.IO;
using System.Configuration;
using BC;
using BE;


namespace TamizajeApp
{
    public partial class frmProcesarPlacas : Form
    {
        ResultadoBC resultadoBC = new ResultadoBC();
        //PlacaBC placaBC = new PlacaBC();

        string rutaOrigen = ConfigurationManager.AppSettings["carpetaOrigenLocal"].ToString();
        string rutaDestino = ConfigurationManager.AppSettings["carpetaDestinoLocal"].ToString();

        //string rutaOrigen = ConfigurationManager.AppSettings["carpetaOrigenServerGSP"].ToString();
        //string rutaDestino = ConfigurationManager.AppSettings["carpetaDestinoServerGSP"].ToString();

        string irt = ConfigurationManager.AppSettings["IRT"].ToString();
        string phe = ConfigurationManager.AppSettings["PHE"].ToString();
        string ntsh = ConfigurationManager.AppSettings["NTSH"].ToString();
        string n17p = ConfigurationManager.AppSettings["17OHP"].ToString();

        //string[] directorio = new string[]();

        public frmProcesarPlacas()
        {
            InitializeComponent();
        }

        private void registrarArchivoResultado(string archivo)
        {

            string name = Path.GetFileNameWithoutExtension(archivo);
            int numEnsayo = Int32.Parse(name);


            using (CsvReader csv = new CsvReader(new StreamReader(archivo), true))
            {
                int fieldCount = csv.FieldCount;

                string[] headers = csv.GetFieldHeaders();

                List<Resultado> listaResultados = new List<Resultado>();

                while (csv.ReadNextRecord())
                {

                    if (csv["Role"].CompareTo("3") == 0)
                    {
                        Resultado resultado = new Resultado();

                        resultado.Analito = csv["Analyte"];
                        if (resultado.Analito.CompareTo("NTSH") == 0)
                        {
                            resultado.Test = ntsh;
                            resultado.Unidad = "uU/ml";
                            resultado.Rango = "<10.00";
                        }

                        if (resultado.Analito.CompareTo("IRT") == 0)
                        {
                            resultado.Test = irt;
                            resultado.Unidad = "ng/ml";
                            resultado.Rango = "<60.00";
                        }

                        if (resultado.Analito.CompareTo("N17OHP") == 0)
                        {
                            resultado.Test = n17p;
                            resultado.Unidad = "ng/ml";
                            resultado.Rango = "<15.00";
                        }
                        if (resultado.Analito.CompareTo("NeoPhe") == 0)
                        {
                            resultado.Test = phe;
                            resultado.Unidad = "mg/dl";
                            resultado.Rango = "<2.50";
                        }

                        resultado.CodigoMuestra = csv["Code"].Replace("=", "").Replace("\"", "");
                        resultado.Pocillo = csv["Pocillo"];
                        resultado.Concentracion = csv["Conc"];
                        resultado.ResultCode = csv["Result Code"];
                        resultado.NumEnsayo = numEnsayo;
                        resultado.Flag = csv["Flag"];
                        resultado.FechaResultado = csv["Run date"].Substring(0, 10);
                        resultado.Estado = 0;
                        resultado.isGSP = 0;
                  
                        resultado.FechaRegistro = DateTime.Today;
                        resultado.idUsuario = 1;
                        resultado.ConcentracionDecimal = decimal.Parse(csv["ValuePlain"]);
                        resultado.FechaResultadoDate = DateTime.Parse(csv["Run date"]);

                        resultado.Placa = int.Parse(csv["Plate"]);
                        resultado.CodigoPlaca = csv["PlateCode"].Replace("=", "").Replace("\"", "");
                        resultado.Instrumento = csv["Instrumento"];

                        listaResultados.Add(resultado);

                        
                    }
                }

                //Placa placa = new Placa();

                //placa.NumEnsayo = numEnsayo;
                //placa.CodigoPlaca = csv["PlateCode"].Replace("=", "").Replace("\"", ""); ;
                ////placa.KitLot = csv["Kitlot"].Replace("=", "").Replace("\"", ""); ;
                //placa.FechaResultado = csv["Run date"].Substring(0, 10);
                //placa.FechaResultadoDate = DateTime.Parse(csv["Run date"]);
                //placa.Estado = 1;
                //placa.Aprobada = 0;
                //placa.fechaRegistro = DateTime.Today;
                //placa.Analito = csv["Analyte"];
                if (listaResultados.Count > 0)
                {
                    resultadoBC.RegistrarResultados(listaResultados);
                    //  placaBC.registrarPlaca(placa);
                }


            }
        }

        private void ProcesarArchivos(string[] listaArchivosResultado)
        {
            foreach (string file in listaArchivosResultado)
            {
                registrarArchivoResultado(file);
            }
        }

        private void MoverArchivo(string archivo)
        {
            string name = Path.GetFileName(archivo);
            string destino = rutaDestino + name;

            File.Move(archivo, destino);
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            string[] archivos = Directory.GetFiles(rutaOrigen);

            foreach (string archivo in archivos)
            {
                RegistrarArchivoResultado(archivo);
                MoverArchivo(archivo);
            }
            obtenerListaArchivos();
        }

        private void frmProcesarPlacas_Load(object sender, EventArgs e)
        {
            obtenerListaArchivos();
        }

        private void obtenerListaArchivos()
        {
            string[] directorio = Directory.GetFiles(rutaOrigen, "*.csv");
            string[] directorioNombres = Directory.GetFiles(rutaOrigen, "*.csv").Select(Path.GetFileName).ToArray();

            if (directorio.Length > 0)
            {
                for (int i = 0; i < directorio.Length; i++)
                {
                    Archivo archivo = new Archivo(directorioNombres[i], directorio[i]);
                    listBoxFiles.Items.Add(archivo);
                }

                listBoxFiles.DisplayMember = "nombre";
                listBoxFiles.DataSource = "ruta";
            }
            else {
                listBoxFiles.Items.Clear();
                dgvResultados.DataSource = string.Empty;
            }
        }

        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string filename = listBoxFiles.GetItemText(listBoxFiles.SelectedItem);
            Archivo archivo = (Archivo)listBoxFiles.SelectedItem;

            using (CachedCsvReader csv = new
            CachedCsvReader(new StreamReader(archivo.ruta), true))
            {
                // Field headers will automatically be used as column names
                dgvResultados.DataSource = csv;
            }
            //string name = "cesar";
        }

        public class Archivo {
            public string nombre { get; set; }
            public string ruta { get; set; }

            public Archivo(string nombre,string ruta) {
                this.nombre = nombre;
                this.ruta = ruta;
            }
        }
    }
}
