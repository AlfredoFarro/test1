using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Web;
using BE;
using iTextSharp.text;
using iTextSharp.text.pdf;
using BC;
using System.Data;

namespace TamiLifeSA
{
    public class Reportes
    {
        private readonly ResultadoBC _resultadoBc = new ResultadoBC();

        //private readonly ResultadoBC _resultadoBc = new ResultadoBC();

        //Fuentes
        readonly Font _arial12NormalBlack = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);
        readonly Font _arial10NormalBlack = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK);
        readonly Font _arial8NormalBlack = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK);

        readonly Font _arial14BoldBlack = FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.BLACK);
        readonly Font _arial12BoldBlack = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);
        readonly Font _arial10BoldBlack = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);
        readonly Font _arial8BoldBlack = FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK);

        readonly Font _arial12BoldRed = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.RED);
        readonly Font _arial8BoldRed = FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.RED);

        readonly Phrase _phraseBlanco = new Phrase { new Chunk(" ", FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)) };

        Phrase _phrase;
        PdfPCell _cell;
        PdfPTable _table;

        #region ReporteResultadosNeonato

        public byte[] ReporteResultadosNeonato(Madre madre, Neonato neonato, List<Vista_MuestraResultado> listaResultados)
        {
            string responsable1 = ConfigurationManager.AppSettings["responsables1"];
            string responsable2 = ConfigurationManager.AppSettings["responsables2"];
            string responsable3 = ConfigurationManager.AppSettings["responsables3"];
            string responsable4 = ConfigurationManager.AppSettings["responsables4"];

            var document = new Document(PageSize.A4, 80f, 80f, 40f, 40f);

            using (var memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                //document.SetMargins(20f, 10f, 10f, 10f);
                document.Open();

                document.NewPage();

                //Header Table
                _table = new PdfPTable(3) { TotalWidth = 500f, LockedWidth = true };
                _table.SetWidths(new[] { 0.4f, 0.3f, 0.4f });

                _cell = ImageCell("~/Images/logoEssalud.png", 50f, Element.ALIGN_LEFT);
                _table.AddCell(_cell);

                var phrase2 = new Phrase { new Chunk(" ", _arial12BoldRed) };
                _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" \n", _arial10NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                document.Add(_table);

                _table = new PdfPTable(3) { TotalWidth = 500f, LockedWidth = true };
                _table.SetWidths(new[] { 0.4f, 0.3f, 0.4f });


                //var phrase1 = new Phrase { new Chunk(muestraCompleta.Muestra.Establecimiento.Nombre, _arial12BoldBlack) };
                //_cell = PhraseCell(phrase1, Element.ALIGN_LEFT);
                //_cell.VerticalAlignment = Element.ALIGN_TOP;
                //_table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                document.Add(_table);


                _table = new PdfPTable(2) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 20f };
                _table.SetWidths(new[] { 1f, 1f });

                _cell = PhraseCell(new Phrase("LABORATORIO DE TAMIZAJE NEONATAL", _arial14BoldBlack), Element.ALIGN_CENTER);
                _cell.Colspan = 2;
                _table.AddCell(_cell);

                _cell = PhraseCell(new Phrase("INFORME DE RESULTADOS", _arial10BoldBlack), Element.ALIGN_CENTER);
                _cell.Colspan = 2;
                _table.AddCell(_cell);

                _cell = PhraseCell(new Phrase(), Element.ALIGN_CENTER);
                _cell.Colspan = 2;
                _table.AddCell(_cell);

                _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                DateTime fechaAux;
                string fechaNac = string.Empty;
                if (DateTime.TryParse(neonato.FechaNacimiento.ToString(), out fechaAux))
                {
                    fechaNac = fechaAux.ToShortDateString();
                }
                

                _phrase = new Phrase
                              {
                                  new Chunk(" \n \n", _arial10NormalBlack),
                                  new Chunk("Recién Nacido: " + neonato.Apellidos + " , " + neonato.Nombres + "\n \n", _arial10NormalBlack),
                                  new Chunk("Fecha de Nacimiento: " + fechaNac + "\n \n", _arial10NormalBlack),
                                  new Chunk("Peso(Kg): " + neonato.Peso + "    Talla(cm):" + neonato.Talla + "\n \n", _arial10NormalBlack),
                                  new Chunk("Madre: " + madre.Apellidos + " , " + madre.Nombres + "\n \n", _arial10NormalBlack),
                                  new Chunk("Teléfono: " + madre.Telefono1 + "\n \n",_arial10NormalBlack)
                              };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _table.AddCell(_cell);

                string sexo;
                if (neonato.Sexo == 1)
                {
                    sexo = "Femenino";
                }
                else
                {
                    sexo = "Masculino";
                }

                _phrase = new Phrase
                             {
                                 new Chunk(" \n \n", _arial10NormalBlack),
                                 new Chunk(" \n \n", _arial10NormalBlack),
                                 new Chunk("Sexo: " + sexo + "\n \n", _arial10NormalBlack),
                                 new Chunk("Semana Gest.: " + neonato.EdadGestacional + "\n \n",_arial10NormalBlack),
                                 new Chunk("DNI: " + madre.DNI + "\n \n", _arial10NormalBlack)

                             };

               
                _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);
                document.Add(_table);

                //string notasMuestra = muestraCompleta.Muestra.Notas;

                _table = new PdfPTable(5) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 5f };
                _table.SetWidths(new[] { 0.4f, 0.20f, 0.20f, 0.15f, 0.2f });

                #region encabezado
                _phrase = new Phrase { new Chunk("ERROR CONGENITO DEL METABOLISMO", _arial10BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("PRUEBA", _arial10BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("RESULTADO", _arial10BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("UNIDAD", _arial10BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("REFERENCIA", _arial10BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                #endregion
                //Por cada resultado de ensayo se crea una linea en la grilla del reporte.
                int lineasBlancas = 4;
                #region resultados



                if (listaResultados.Count > 0)
                {
                    foreach (var resultado in listaResultados)
                    {
                        _phrase = new Phrase { new Chunk(resultado.NombrePrueba, _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(resultado.TestName, _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase();
                        if (resultado.rdcDeterminationLevel > 20)
                        {
                            _phrase.Add(new Chunk(resultado.ConcTexto, _arial12BoldRed));
                        }
                        else
                        {
                            _phrase.Add(new Chunk(resultado.ConcTexto, _arial10NormalBlack));
                        }

                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(resultado.Unidad, _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(resultado.Rango, _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        lineasBlancas--;
                    }
                }

                document.Add(_table);


                if (lineasBlancas > 0)
                {
                    _table = new PdfPTable(1) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 2f };

                    for (int i = 0; i < lineasBlancas; i++)
                    {
                        _phrase = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);
                    }

                    document.Add(_table);
                }

                #endregion
                #region PieReporte

                _table = new PdfPTable(2) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 10f };
                _table.SetWidths(new[] { 0.5f, 0.5f });

                //cell.Colspan = 2;

                DateTime diaImpresion = DateTime.Today;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
                _phrase = new Phrase { new Chunk("FECHA DE IMPRESION: " + diaImpresion.ToLongDateString().ToUpper() + "\n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("OBSERVACIONES:", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);



                _phrase = new Phrase { new Chunk("PROCESADO POR : \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    " + responsable1 + "\n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    " + responsable2 + "\n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    " + responsable3 + "\n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    " + responsable4 + "\n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);



                _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);



                document.Add(_table);

                #region firmaDoctora

                _table = new PdfPTable(3) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 20f };
                _table.SetWidths(new[] { 0.3f, 0.4f, 0.3f });

                _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);
                _cell = ImageCell("~/Images/firmaDr2.jpg", 30f, Element.ALIGN_LEFT);
                _table.AddCell(_cell);

                //segunda linea
                _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColorTop = BaseColor.BLACK;
                _table.AddCell(_cell);
                _phrase = new Phrase { new Chunk("--------------------------------------------------", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                //tercera linea
                _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("RESPONSABLE DE APROBACIÓN ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                document.Add(_table);
                #endregion

                #endregion

                //nueva pagina
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                return bytes;

            }
        }

        #endregion

        #region ReporteResultados
        public byte[] ReporteResultados(MuestraCompletaBE muestraCompleta, List<Vista_Resultado> listaResultados)
        {
            string responsable1 = ConfigurationManager.AppSettings["responsables1"];
            string responsable2 = ConfigurationManager.AppSettings["responsables2"];
            string responsable3 = ConfigurationManager.AppSettings["responsables3"];
            string responsable4 = ConfigurationManager.AppSettings["responsables4"];

            var document = new Document(PageSize.A4, 80f, 80f, 40f, 40f);

            using (var memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                //document.SetMargins(20f, 10f, 10f, 10f);
                document.Open();

                document.NewPage();

                //Header Table
                _table = new PdfPTable(3) { TotalWidth = 500f, LockedWidth = true };
                _table.SetWidths(new[] { 0.4f, 0.3f, 0.4f });

                _cell = ImageCell("~/Images/LogoINMP.jpg", 50f, Element.ALIGN_LEFT);
                _table.AddCell(_cell);

                var phrase2 = new Phrase { new Chunk(" ", _arial12BoldRed) };
                _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" \n", _arial10NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                document.Add(_table);

                _table = new PdfPTable(3) { TotalWidth = 500f, LockedWidth = true };
                _table.SetWidths(new[] { 0.4f, 0.3f, 0.4f });


                var phrase1 = new Phrase { new Chunk(muestraCompleta.Muestra.Establecimiento.Nombre, _arial12BoldBlack) };
                _cell = PhraseCell(phrase1, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                document.Add(_table);


                _table = new PdfPTable(2) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 20f };
                _table.SetWidths(new[] { 1f, 1f });

                _cell = PhraseCell(new Phrase("LABORATORIO DE TAMIZAJE NEONATAL", _arial14BoldBlack), Element.ALIGN_CENTER);
                _cell.Colspan = 2;
                _table.AddCell(_cell);

                _cell = PhraseCell(new Phrase("INFORME DE RESULTADOS", _arial10BoldBlack), Element.ALIGN_CENTER);
                _cell.Colspan = 2;
                _table.AddCell(_cell);

                _cell = PhraseCell(new Phrase(), Element.ALIGN_CENTER);
                _cell.Colspan = 2;
                _table.AddCell(_cell);

                _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                string pesoNeo = string.Empty;
                decimal pesoNeonato;
                if (decimal.TryParse(muestraCompleta.Neonato.Peso.ToString(),out pesoNeonato))
                {
                    pesoNeo = string.Format("{0:0.##}", muestraCompleta.Neonato.Peso);
                    //pesoNeo = pesoNeonato.ToString("N2");
                }

                _phrase = new Phrase
                              {
                                  new Chunk(" \n \n", _arial10NormalBlack),
                                  new Chunk("Codigo de Muestra: " + muestraCompleta.Muestra.CodigoMuestra + "\n \n", _arial10NormalBlack),
                                  new Chunk("Recién Nacido: " + muestraCompleta.Neonato.Apellidos + " , " + muestraCompleta.Neonato.Nombres + "\n \n", _arial10NormalBlack),
                                  new Chunk("Madre: " + muestraCompleta.Madre.Apellidos + " , " + muestraCompleta.Madre.Nombres + "\n \n", _arial10NormalBlack),
                                  new Chunk("DNI: " + muestraCompleta.Madre.DNI + "\n \n", _arial10NormalBlack),new Chunk("Teléfono: " + muestraCompleta.Madre.Telefono1 + "\n \n",_arial10NormalBlack),
                                  new Chunk("Semana Gest.: " + muestraCompleta.Neonato.EdadGestacional + "\n \n",_arial10NormalBlack),
                                  //new Chunk("Peso(g): " + muestraCompleta.Neonato.Peso + "    Talla(cm):" + muestraCompleta.Neonato.Talla + "\n \n", _arial10NormalBlack)
                                  new Chunk("Peso(g): " + pesoNeo + "    Talla(cm):" + muestraCompleta.Neonato.Talla + "\n \n", _arial10NormalBlack)
                              };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _table.AddCell(_cell);

                _phrase = new Phrase
                             {
                                 new Chunk(" \n \n", _arial10NormalBlack),
                                 new Chunk(" \n \n", _arial10NormalBlack),
                                 new Chunk("Correlativo: " + muestraCompleta.Muestra.CodigoInternoLab + "\n \n",_arial12BoldBlack)
                             };

                DateTime fechaAux;
                if (DateTime.TryParse(muestraCompleta.Neonato.FechaNacimiento.ToString(), out fechaAux))
                {
                    _phrase.Add(new Chunk("Fecha de Nacimiento: " + fechaAux.ToShortDateString() + "\n \n", _arial10NormalBlack));
                }
                else
                {
                    _phrase.Add(new Chunk("Fecha de Nacimiento: " + " " + "\n \n", _arial10NormalBlack));
                }
                if (DateTime.TryParse(muestraCompleta.Muestra.FechaToma.ToString(), out fechaAux))
                {
                    _phrase.Add(new Chunk("Fecha de Toma de Muestra: " + fechaAux.ToShortDateString() + "\n \n", _arial10NormalBlack));
                }
                else
                {
                    _phrase.Add(new Chunk("Fecha de Toma de Muestra: " + " " + "\n \n", _arial10NormalBlack));
                }
                _phrase.Add(new Chunk("Número de Muestra: " + muestraCompleta.Muestra.NumMuestra + "\n \n", _arial10NormalBlack));
                string sexo;
                if (muestraCompleta.Neonato.Sexo == 1)
                {
                    sexo = "Femenino";
                }
                else
                {
                    sexo = "Masculino";
                }
                _phrase.Add(new Chunk("Sexo: " + sexo + "\n \n", _arial10NormalBlack));

                if (DateTime.TryParse(muestraCompleta.Muestra.FechaRecepcion.ToString(), out fechaAux))
                {
                    _phrase.Add(new Chunk("Fecha de Recepción: " + fechaAux.ToShortDateString() + "\n \n", _arial10NormalBlack));
                }
                else
                {
                    _phrase.Add(new Chunk("Fecha de Recepción: " + " " + "\n \n", _arial10NormalBlack));
                }

                _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);
                document.Add(_table);

                string notasMuestra = muestraCompleta.Muestra.Notas;

                _table = new PdfPTable(5) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 5f };
                _table.SetWidths(new[] { 0.4f, 0.20f, 0.20f, 0.15f, 0.2f });

                #region encabezado
                _phrase = new Phrase { new Chunk("ERROR CONGENITO DEL METABOLISMO", _arial10BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("PRUEBA", _arial10BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("RESULTADO", _arial10BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("UNIDAD", _arial10BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("REFERENCIA", _arial10BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                #endregion
                //Por cada resultado de ensayo se crea una linea en la grilla del reporte.
                int lineasBlancas = 4;
                #region resultados



                if (listaResultados.Count > 0)
                {
                    foreach (var resultado in listaResultados)
                    {
                        _phrase = new Phrase { new Chunk(resultado.Nombre, _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(resultado.TestName, _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase();
                        if (resultado.rdcDeterminationLevel > 20)
                        {
                            _phrase.Add(new Chunk(resultado.ConcTexto, _arial12BoldRed));
                        }
                        else
                        {
                            _phrase.Add(new Chunk(resultado.ConcTexto, _arial10NormalBlack));
                        }

                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(resultado.Unidad, _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(resultado.Rango, _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        lineasBlancas--;
                    }
                }

                document.Add(_table);


                if (lineasBlancas > 0)
                {
                    _table = new PdfPTable(1) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 2f };

                    for (int i = 0; i < lineasBlancas; i++)
                    {
                        _phrase = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);
                    }

                    document.Add(_table);
                }

                #endregion
                #region PieReporte

                _table = new PdfPTable(2) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 10f };
                _table.SetWidths(new[] { 0.5f, 0.5f });

                //cell.Colspan = 2;

                DateTime diaImpresion = DateTime.Today;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
                _phrase = new Phrase { new Chunk("FECHA DE IMPRESION: " + diaImpresion.ToLongDateString().ToUpper() + "\n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("OBSERVACIONES:", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    " + notasMuestra, _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);



                _phrase = new Phrase { new Chunk("PROCESADO POR : \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    " + responsable1 + "\n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    " + responsable2 + "\n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    " + responsable3 + "\n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    " + responsable4 + "\n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);



                _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);



                document.Add(_table);

                #region firmaDoctora

                _table = new PdfPTable(3) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 20f };
                _table.SetWidths(new[] { 0.3f, 0.4f, 0.3f });

                _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);
                _cell = ImageCell("~/Images/firma_dra.jpg", 30f, Element.ALIGN_LEFT);
                _table.AddCell(_cell);

                //segunda linea
                _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColorTop = BaseColor.BLACK;
                _table.AddCell(_cell);
                _phrase = new Phrase { new Chunk("--------------------------------------------------", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                //tercera linea
                _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("RESPONSABLE DE APROBACIÓN ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                document.Add(_table);
                #endregion

                #endregion

                //nueva pagina
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                return bytes;

            }
        }
        #endregion
        #region InformeHistoricoResultados
        public byte[] ReporteHistoricoResultados(MuestraCompletaBE muestraCompleta)
        {
            string responsable1 = ConfigurationManager.AppSettings["responsables1"];
            string responsable2 = ConfigurationManager.AppSettings["responsables2"];
            string responsable3 = ConfigurationManager.AppSettings["responsables3"];
            string responsable4 = ConfigurationManager.AppSettings["responsables4"];

            var document = new Document(PageSize.A4, 80f, 80f, 40f, 40f);

            using (var memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                //document.SetMargins(20f, 10f, 10f, 10f);
                document.Open();

                document.NewPage();

                //Header Table
                _table = new PdfPTable(3) { TotalWidth = 500f, LockedWidth = true };
                _table.SetWidths(new[] { 0.4f, 0.3f, 0.4f });

                _cell = ImageCell("~/Images/logoEssalud.png", 50f, Element.ALIGN_LEFT);
                _table.AddCell(_cell);

                var phrase2 = new Phrase { new Chunk(" ", _arial12BoldRed) };
                _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" \n", _arial10NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                document.Add(_table);

                _table = new PdfPTable(3) { TotalWidth = 500f, LockedWidth = true };
                _table.SetWidths(new[] { 0.4f, 0.3f, 0.4f });


                var phrase1 = new Phrase { new Chunk(muestraCompleta.Muestra.Establecimiento.Nombre, _arial12BoldBlack) };
                _cell = PhraseCell(phrase1, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                document.Add(_table);


                _table = new PdfPTable(2) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 20f };
                _table.SetWidths(new[] { 1f, 1f });

                _cell = PhraseCell(new Phrase(" ", _arial14BoldBlack), Element.ALIGN_CENTER);
                _cell.Colspan = 2;
                _table.AddCell(_cell);

                _cell = PhraseCell(new Phrase("LABORATORIO DE TAMIZAJE NEONATAL", _arial14BoldBlack), Element.ALIGN_CENTER);
                _cell.Colspan = 2;
                _table.AddCell(_cell);

                _cell = PhraseCell(new Phrase("INFORME HISTORICO", _arial10BoldBlack), Element.ALIGN_CENTER);
                _cell.Colspan = 2;
                _table.AddCell(_cell);

                _cell = PhraseCell(new Phrase(), Element.ALIGN_CENTER);
                _cell.Colspan = 2;
                _table.AddCell(_cell);

                _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase
                              {
                                  new Chunk(" \n \n", _arial10NormalBlack),
                                  new Chunk("Recién Nacido: " + muestraCompleta.Neonato.Apellidos + " , " + muestraCompleta.Neonato.Nombres + "\n \n", _arial10NormalBlack),
                                  new Chunk("Peso(Kg): " + muestraCompleta.Neonato.Peso + "    Talla(cm):" + muestraCompleta.Neonato.Talla + "\n \n", _arial10NormalBlack),
                                  new Chunk("Madre: " + muestraCompleta.Madre.Apellidos + " , " + muestraCompleta.Madre.Nombres + "\n \n", _arial10NormalBlack),
                                  new Chunk("Teléfono: " + muestraCompleta.Madre.Telefono1 + "\n \n",_arial10NormalBlack),
                                  new Chunk(" " + "\n \n",_arial10NormalBlack),
                              };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _table.AddCell(_cell);

                _phrase = new Phrase
                             {
                                 new Chunk(" \n \n", _arial10NormalBlack), 
                                 //new Chunk(" E.G: " + muestraCompleta.Neonato.EdadGestacional +"\n \n", _arial10NormalBlack)    
                             };

                DateTime fechaAux;
                if (DateTime.TryParse(muestraCompleta.Neonato.FechaNacimiento.ToString(), out fechaAux))
                {
                    _phrase.Add(new Chunk("Fecha de Nacimiento: " + fechaAux.ToShortDateString() + "\n \n", _arial10NormalBlack));
                }
                else
                {
                    _phrase.Add(new Chunk("Fecha de Nacimiento: " + " " + "\n \n", _arial10NormalBlack));
                }
                
                _phrase.Add(new Chunk("Edad Gestacional(Sem): " + muestraCompleta.Neonato.EdadGestacional + "\n \n", _arial10NormalBlack));
                _phrase.Add(new Chunk("DNI: " + muestraCompleta.Madre.DNI + "\n \n", _arial10NormalBlack));
                _phrase.Add(new Chunk(" " + "\n \n", _arial10NormalBlack));
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);
                document.Add(_table);



                //string notasMuestra = muestraCompleta.Muestra.Notas;

                _table = new PdfPTable(6) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 5f };
                _table.SetWidths(new[] { 0.3f, 0.20f, 0.16f, 0.15f, 0.16f, 0.18f });

                #region encabezado
                _phrase = new Phrase { new Chunk("PRUEBA DE TAMIZAJE", _arial10BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("CORRELATIVO", _arial10BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("Nro. MUESTRA", _arial10BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("FECHA TOMA", _arial10BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);


                _phrase = new Phrase { new Chunk("FECHA RESULTADO", _arial10BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("RESULTADO", _arial10BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);



                #endregion
                //Por cada resultado de ensayo se crea una linea en la grilla del reporte.


                //int lineasBlancas = 4;
                #region resultados

                var pruebaBc = new PruebaBC();
                var listaPruebas = pruebaBc.ObtenerPruebas(1);

                foreach (var prueba in listaPruebas)
                {
                    var dtResultados = _resultadoBc.ListaResultadosHistoricosPaciente(muestraCompleta.Neonato.idNeonato, prueba.NombreCorto);
                    if (dtResultados.Rows.Count > 0)
                    {
                        _phrase = new Phrase { new Chunk(prueba.NombreCorto, _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _cell.BorderColorLeft = BaseColor.WHITE;
                        _cell.BorderColorRight = BaseColor.WHITE;
                        _cell.BorderColorTop = BaseColor.BLACK;
                        _cell.BorderColorBottom = BaseColor.WHITE;
                        _cell.Border = 1;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _cell.BorderColorLeft = BaseColor.WHITE;
                        _cell.BorderColorRight = BaseColor.WHITE;
                        _cell.BorderColorTop = BaseColor.BLACK;
                        _cell.BorderColorBottom = BaseColor.WHITE;
                        _cell.Border = 1;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _cell.BorderColorLeft = BaseColor.WHITE;
                        _cell.BorderColorRight = BaseColor.WHITE;
                        _cell.BorderColorTop = BaseColor.BLACK;
                        _cell.BorderColorBottom = BaseColor.WHITE;
                        _cell.Border = 1;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _cell.BorderColorLeft = BaseColor.WHITE;
                        _cell.BorderColorRight = BaseColor.WHITE;
                        _cell.BorderColorTop = BaseColor.BLACK;
                        _cell.BorderColorBottom = BaseColor.WHITE;
                        _cell.Border = 1;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _cell.BorderColorLeft = BaseColor.WHITE;
                        _cell.BorderColorRight = BaseColor.WHITE;
                        _cell.BorderColorTop = BaseColor.BLACK;
                        _cell.BorderColorBottom = BaseColor.WHITE;
                        _cell.Border = 1;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _cell.BorderColorLeft = BaseColor.WHITE;
                        _cell.BorderColorRight = BaseColor.WHITE;
                        _cell.BorderColorTop = BaseColor.BLACK;
                        _cell.BorderColorBottom = BaseColor.WHITE;
                        _cell.Border = 1;
                        _table.AddCell(_cell);
                        //dr["Correlativo"].ToString()
                        //int i = 1;
                        foreach (DataRow dr in dtResultados.Rows)
                        {
                            _phrase = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                            _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                            _cell.VerticalAlignment = Element.ALIGN_TOP;
                            //_cell.BorderColor = BaseColor.BLACK;
                            _table.AddCell(_cell);

                            //Correlativo
                            _phrase = new Phrase { new Chunk(dr["CodigoMuestra"].ToString(), _arial10NormalBlack) };
                            _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                            _cell.VerticalAlignment = Element.ALIGN_TOP;
                            //_cell.BorderColor = BaseColor.BLACK;
                            _table.AddCell(_cell);

                            //#Muestra
                            _phrase = new Phrase { new Chunk(dr["NumMuestra"].ToString(), _arial10NormalBlack) };
                            _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                            _cell.VerticalAlignment = Element.ALIGN_TOP;
                            //_cell.BorderColor = BaseColor.BLACK;
                            _table.AddCell(_cell);

                            //Fecha Medicion
                            DateTime fechaToma = DateTime.Parse(dr["FechaToma"].ToString());
                            _phrase = new Phrase { new Chunk(fechaToma.ToShortDateString(), _arial10NormalBlack) };
                            _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                            _cell.VerticalAlignment = Element.ALIGN_TOP;
                            //_cell.BorderColor = BaseColor.BLACK;
                            _table.AddCell(_cell);

                            //Fecha Medicion
                            DateTime fechaMedicion = DateTime.Parse(dr["FechaResultado"].ToString());
                            _phrase = new Phrase { new Chunk(fechaMedicion.ToShortDateString(), _arial10NormalBlack) };
                            _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                            _cell.VerticalAlignment = Element.ALIGN_TOP;
                            //_cell.BorderColor = BaseColor.BLACK;
                            _table.AddCell(_cell);

                            //Valor
                            Font fuenteValorConcentracion = _arial10NormalBlack;
                            int determinacion = int.Parse(dr["rdcDeterminationLevel"].ToString());
                            if (determinacion > 20)
                            {
                                fuenteValorConcentracion = _arial12BoldBlack;
                            }
                            _phrase = new Phrase();
                            _phrase.Add(new Chunk(dr["ConcTexto"].ToString(), fuenteValorConcentracion));
                            _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                            _cell.VerticalAlignment = Element.ALIGN_TOP;
                            //_cell.BorderColor = BaseColor.BLACK;
                            _table.AddCell(_cell);
                            //lineasBlancas--;
                            //i++;
                        }

                    }
                }

                document.Add(_table);


                #endregion
                #region PieReporte

                _table = new PdfPTable(2) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 10f };
                _table.SetWidths(new[] { 0.5f, 0.5f });

                //cell.Colspan = 2;

                _phrase = new Phrase { new Chunk("    ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);


                DateTime diaImpresion = DateTime.Today;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
                _phrase = new Phrase { new Chunk("FECHA DE IMPRESION: " + diaImpresion.ToLongDateString().ToUpper() + "\n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                //document.Add(_table);

                _phrase = new Phrase { new Chunk("PROCESADO POR : \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    " + responsable1 + "\n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    " + responsable2 + "\n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    " + responsable3 + "\n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    " + responsable4 + "\n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);



                _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);



                document.Add(_table);

                #region firmaDoctora

                _table = new PdfPTable(3) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 20f };
                _table.SetWidths(new[] { 0.3f, 0.4f, 0.3f });

                _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);
                _cell = ImageCell("~/Images/firmaDr2.jpg", 30f, Element.ALIGN_LEFT);
                _table.AddCell(_cell);

                //segunda linea
                _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColorTop = BaseColor.BLACK;
                _table.AddCell(_cell);
                _phrase = new Phrase { new Chunk("--------------------------------------------------", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                //tercera linea
                _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("RESPONSABLE DE APROBACIÓN ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                document.Add(_table);
                #endregion


                #endregion

                //nueva pagina
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                return bytes;

            }
        }
        #endregion
        #region ReporteEnvio
        public byte[] ReporteEnvio(Envio envio, List<Vista_MuestrasxEnvio> listaMuestras)
        {
            var document = new Document(PageSize.A4, 80f, 80f, 40f, 40f);
            //document.
            using (var memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                //document.SetMargins(20f, 10f, 10f, 10f);
                document.Open();
                document.NewPage();
                //header
                _table = new PdfPTable(3) { TotalWidth = 500f, LockedWidth = true };
                _table.SetWidths(new[] { 0.4f, 0.3f, 0.4f });

                _cell = ImageCell("~/Images/logoEssalud.png", 50f, Element.ALIGN_LEFT);
                _table.AddCell(_cell);

                var phrase2 = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" \n \n Número de Carta: " + envio.CodigoEnvio, _arial12BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                document.Add(_table);

                _table = new PdfPTable(2) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 20f };
                _table.SetWidths(new[] { 1f, 1f });

                _cell = PhraseCell(new Phrase("REPORTE DE MUESTRAS ENVIADAS", _arial12BoldBlack), Element.ALIGN_CENTER);
                _cell.Colspan = 2;
                _table.AddCell(_cell);

                _cell = PhraseCell(new Phrase(), Element.ALIGN_CENTER);
                _cell.Colspan = 2;
                _table.AddCell(_cell);

                _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase
                              {
                                  new Chunk(" \n", _arial10NormalBlack),
                                  new Chunk("RED ASISTENCIAL: " + envio.Establecimiento.TipoEstablecimiento.Nombre + "\n \n",_arial10NormalBlack),
                                  new Chunk("ESTABLECIMIENTO: ", _arial10NormalBlack),new Chunk(envio.Establecimiento.Nombre + "\n \n", _arial10BoldBlack),
                                  new Chunk("FECHA DE CREACIÓN: " + DateTime.Parse(envio.FechaCreacion.ToString()).ToShortDateString() + "\n \n", _arial10NormalBlack),
                                  new Chunk("#TARJETAS ENVIADAS: " + envio.NumTarjetas + "\n \n", _arial10NormalBlack)
                              };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _table.AddCell(_cell);

                _phrase = new Phrase
                              {
                                  new Chunk(" \n ", _arial10NormalBlack),
                                  new Chunk(" \n \n", _arial10NormalBlack),
                                  new Chunk(" \n \n", _arial10NormalBlack),
                                  new Chunk(" \n \n", _arial10NormalBlack),
                                  new Chunk(" \n \n", _arial10NormalBlack)
                              };
                _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);
                document.Add(_table);

                //-------------------------------------

                //Encabezado Tabla de muestras
                _table = new PdfPTable(7) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 5f };
                _table.SetWidths(new[] { 0.15f, 0.4f, 0.15f, 0.15f, 0.15f, 0.15f, 0.15f });

                #region encabezado
                _phrase = new Phrase { new Chunk("CODIGO", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("APELLIDOS", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("FECHA NAC", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("FECHA TOMA", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("DNI", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("EDAD GEST.", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("#MUESTRA", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                #endregion
                //Por cada resultado de ensayo se crea una linea en la grilla del reporte.
                int lineasBlancas = 4;
                #region resultados

                if (listaMuestras.Count > 0)
                {
                    foreach (var muestra in listaMuestras)
                    {
                        _phrase = new Phrase { new Chunk(muestra.CodigoMuestra, _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(muestra.Apellidos, _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        DateTime fechaAux;

                        _phrase = new Phrase();
                        if (DateTime.TryParse(muestra.FechaNacimiento.ToString(), out fechaAux))
                        {
                            _phrase.Add(new Chunk(fechaAux.ToShortDateString(), _arial8NormalBlack));
                        }
                        else
                        {
                            _phrase.Add(new Chunk(" ", _arial8NormalBlack));
                        }

                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase();
                        if (DateTime.TryParse(muestra.FechaToma.ToString(), out fechaAux))
                        {
                            _phrase.Add(new Chunk(fechaAux.ToShortDateString(), _arial8NormalBlack));
                        }
                        else
                        {
                            _phrase.Add(new Chunk(" ", _arial8NormalBlack));
                        }

                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(muestra.DNI, _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(muestra.EdadGestacional.ToString(), _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(muestra.NumMuestra.ToString(), _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        lineasBlancas--;
                    }
                }

                document.Add(_table);


                if (lineasBlancas > 0)
                {
                    _table = new PdfPTable(1) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 2f };

                    for (int i = 0; i < lineasBlancas; i++)
                    {
                        _phrase = new Phrase { new Chunk(" ", _arial12NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);
                    }

                    document.Add(_table);
                }

                #endregion

                #region PieReporte

                _table = new PdfPTable(1) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 10f };

                DateTime diaImpresion = DateTime.Today;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
                _phrase = new Phrase { new Chunk("FECHA DE IMPRESION: " + diaImpresion.ToLongDateString().ToUpper() + " \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("OBSERVACIONES: " + envio.Notas + " \n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);


                document.Add(_table);
                #endregion

                //nueva pagina
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                return bytes;
            }
        }
        #endregion
        #region ReporteResultadosConsolidados

        public byte[] ReporteResultadosConsolidados(bool esNtsh, string fechaResultado, DataTable dt)
        {
            var document = new Document(PageSize.A4, 80f, 80f, 40f, 40f);
            //document.
            using (var memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                //document.SetMargins(20f, 10f, 10f, 10f);
                document.Open();

                document.NewPage();
                //---------------------------------------
                //Header Table
                _table = new PdfPTable(3) { TotalWidth = 500f, LockedWidth = true };
                _table.SetWidths(new[] { 0.4f, 0.3f, 0.4f });

                _cell = ImageCell("~/Images/logoEssalud.png", 50f, Element.ALIGN_LEFT);
                _table.AddCell(_cell);

                _cell = PhraseCell(_phraseBlanco, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                document.Add(_table);

                _table = new PdfPTable(2) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 20f };
                _table.SetWidths(new[] { 1f, 1f });

                _cell = PhraseCell(new Phrase("TAMIZAJE NEONATAL", _arial14BoldBlack), Element.ALIGN_CENTER);
                _cell.Colspan = 2;
                _table.AddCell(_cell);

                if (esNtsh)
                {
                    _cell = PhraseCell(new Phrase("NTSH - N17OHP", _arial10BoldBlack), Element.ALIGN_CENTER);
                    _cell.Colspan = 2;
                    _table.AddCell(_cell);
                }
                else
                {
                    _cell = PhraseCell(new Phrase("NeoPhe - TGAL", _arial10BoldBlack), Element.ALIGN_CENTER);
                    _cell.Colspan = 2;
                    _table.AddCell(_cell);
                }


                _cell = PhraseCell(new Phrase("Fecha de Procesamiento: " + fechaResultado, _arial10BoldBlack), Element.ALIGN_CENTER);
                _cell.Colspan = 2;
                _table.AddCell(_cell);

                _cell = PhraseCell(_phraseBlanco, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _cell = PhraseCell(_phraseBlanco, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                document.Add(_table);

                _table = new PdfPTable(9) { TotalWidth = 550f, LockedWidth = true, SpacingBefore = 5f };
                _table.SetWidths(new[] { 0.08f, 0.178f, 0.4f, 0.4f, 0.4f, 0.16f, 0.08f, 0.13f, 0.11f });

                #region encabezado
                _phrase = new Phrase { new Chunk("#", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("Código Correlativo", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("Apellidos RN", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("Apellidos Madre", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("Centro Asistencial", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("Fecha Nacim.", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("Num", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                if (esNtsh)
                {
                    _phrase = new Phrase { new Chunk("TSH", _arial8BoldBlack) };
                    _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                    _cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    _cell.BorderColor = BaseColor.BLACK;
                    _table.AddCell(_cell);

                    _phrase = new Phrase { new Chunk("17OHP", _arial8BoldBlack) };
                    _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                    _cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    _cell.BorderColor = BaseColor.BLACK;
                    _table.AddCell(_cell);
                }
                else
                {
                    _phrase = new Phrase { new Chunk("NeoPhe", _arial8BoldBlack) };
                    _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                    _cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    _cell.BorderColor = BaseColor.BLACK;
                    _table.AddCell(_cell);

                    _phrase = new Phrase { new Chunk("TGAL", _arial8BoldBlack) };
                    _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                    _cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    _cell.BorderColor = BaseColor.BLACK;
                    _table.AddCell(_cell);
                }

                #endregion
                //Por cada resultado de ensayo se crea una linea en la grilla del reporte.
                #region resultados

                int num = 1;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        _phrase = new Phrase { new Chunk(num++.ToString(), _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(dr["Correlativo"].ToString(), _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(dr["ApellidosRN"].ToString(), _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(dr["ApellidosMadre"].ToString(), _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        string establecimiento = dr["Establecimiento"].ToString();
                        if (establecimiento.Length > 28)
                        {
                            establecimiento = establecimiento.Substring(0, 28);
                        }

                        _phrase = new Phrase { new Chunk(establecimiento, _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        string fechaNac = string.Empty;
                        DateTime fechaNacimiento;
                        if (DateTime.TryParse(dr["FechaNacimiento"].ToString(), out fechaNacimiento))
                        {
                            fechaNac = fechaNacimiento.ToShortDateString();
                        }

                        _phrase = new Phrase { new Chunk(fechaNac, _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(dr["NumMuestra"].ToString(), _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        if (esNtsh)
                        {
                            _phrase = new Phrase { new Chunk(dr["NTSH"].ToString(), _arial8NormalBlack) };
                            _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                            _cell.VerticalAlignment = Element.ALIGN_TOP;
                            _cell.BorderColor = BaseColor.BLACK;
                            _table.AddCell(_cell);

                            _phrase = new Phrase { new Chunk(dr["N17OHP"].ToString(), _arial8NormalBlack) };
                            _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                            _cell.VerticalAlignment = Element.ALIGN_TOP;
                            _cell.BorderColor = BaseColor.BLACK;
                            _table.AddCell(_cell);
                        }
                        else
                        {
                            _phrase = new Phrase { new Chunk(dr["NeoPhe"].ToString(), _arial8NormalBlack) };
                            _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                            _cell.VerticalAlignment = Element.ALIGN_TOP;
                            _cell.BorderColor = BaseColor.BLACK;
                            _table.AddCell(_cell);

                            _phrase = new Phrase { new Chunk(dr["TGAL"].ToString(), _arial8NormalBlack) };
                            _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                            _cell.VerticalAlignment = Element.ALIGN_TOP;
                            _cell.BorderColor = BaseColor.BLACK;
                            _table.AddCell(_cell);
                        }


                    }
                }

                document.Add(_table);



                #endregion

                #region PieReporte

                _table = new PdfPTable(2) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 10f };
                _table.SetWidths(new[] { 0.5f, 0.5f });

                //cell.Colspan = 2;

                DateTime diaImpresion = DateTime.Today;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
                _phrase = new Phrase { new Chunk("FECHA DE IMPRESION: " + diaImpresion.ToLongDateString().ToUpper() + "\n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);


                document.Add(_table);

                #endregion

                //nueva pagina
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                return bytes;


            }
        }

        #endregion
        #region ReporteResultadosEstablecimientoConsolidados

        public byte[] ReporteResultadosxEstablecimiento(string establecimiento, string rangoFechas, List<Vista_BuscarPaciente> listaMuestras)
        {
            var document = new Document(PageSize.A4, 80f, 80f, 40f, 40f);
            //document.
            using (var memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                //document.SetMargins(20f, 10f, 10f, 10f);
                document.Open();

                document.NewPage();
                //Header Table
                _table = new PdfPTable(3) { TotalWidth = 500f, LockedWidth = true };
                _table.SetWidths(new[] { 0.4f, 0.3f, 0.4f });

                _cell = ImageCell("~/Images/logoEssalud.png", 50f, Element.ALIGN_LEFT);
                _table.AddCell(_cell);

                var phrase2 = new Phrase { new Chunk(" ", _arial12BoldBlack) };
                _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" \n", _arial10NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                document.Add(_table);

                _table = new PdfPTable(2) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 20f };
                _table.SetWidths(new[] { 1f, 1f });

                _cell = PhraseCell(new Phrase("REPORTE DE RESULTADOS DE TAMIZAJE NEONATAL", _arial14BoldBlack), Element.ALIGN_CENTER);
                _cell.Colspan = 2;
                _table.AddCell(_cell);

                _cell = PhraseCell(new Phrase(establecimiento.ToUpper(), _arial10BoldBlack), Element.ALIGN_CENTER);
                _cell.Colspan = 2;
                _table.AddCell(_cell);

                _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                document.Add(_table);

                _table = new PdfPTable(11) { TotalWidth = 550f, LockedWidth = true, SpacingBefore = 5f };
                _table.SetWidths(new[] { 0.07f, 0.16f, 0.38f, 0.38f, 0.16f, 0.16f, 0.16f, 0.09f, 0.12f, 0.12f, 0.09f });

                #region encabezado
                _phrase = new Phrase { new Chunk("#", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("Correlativo", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("Apellidos RN", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("Apellidos Madre", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("DNI", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("Fecha Nac.", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("Fecha Toma", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("TSH", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("17OHP", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("NeoPhe", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("TGAL", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                #endregion
                //Por cada resultado de ensayo se crea una linea en la grilla del reporte.

                #region resultados

                int num = 1;
                if (listaMuestras.Count > 0)
                {
                    foreach (var muestra in listaMuestras)
                    {
                        _phrase = new Phrase { new Chunk(num++.ToString(), _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(muestra.CodigoInternoLab, _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(muestra.ApellidosNeonato, _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(muestra.ApellidosMadre, _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(muestra.DNI, _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        DateTime fechaAux;
                        _phrase = new Phrase();
                        if (DateTime.TryParse(muestra.FechaNacimiento.ToString(), out fechaAux))
                        {

                            _phrase.Add(new Chunk(fechaAux.ToShortDateString(), _arial8NormalBlack));

                        }
                        else
                        {
                            _phrase.Add(new Chunk(" ", _arial8NormalBlack));
                        }
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);


                        _phrase = new Phrase();
                        if (DateTime.TryParse(muestra.FechaToma.ToString(), out fechaAux))
                        {

                            _phrase.Add(new Chunk(fechaAux.ToShortDateString(), _arial8NormalBlack));

                        }
                        else
                        {
                            _phrase.Add(new Chunk(" ", _arial8NormalBlack));
                        }
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);


                        string aux = string.Empty;
                        Font fuenteResultado = new Font();
                        Resultado resultadoAux = _resultadoBc.ObtenerResultadoTest(muestra.CodigoMuestra, muestra.CodigoInternoLab, "NTSH");
                        if (resultadoAux != null)
                        {
                            aux = resultadoAux.ConcTexto;
                            if ((resultadoAux.ResultCode.Contains("20")))
                            {
                                fuenteResultado = _arial8NormalBlack;
                            }
                            else
                            {
                                fuenteResultado = _arial8BoldRed;
                            }

                        }


                        _phrase = new Phrase { new Chunk(aux, fuenteResultado) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);


                        aux = string.Empty;
                        resultadoAux = _resultadoBc.ObtenerResultadoTest(muestra.CodigoMuestra, muestra.CodigoInternoLab, "N17OHP");
                        if (resultadoAux != null)
                        {
                            aux = resultadoAux.ConcTexto;
                            if ((resultadoAux.ResultCode.Contains("20")))
                            {
                                fuenteResultado = _arial8NormalBlack;
                            }
                            else
                            {
                                fuenteResultado = _arial8BoldRed;
                            }

                        }

                        _phrase = new Phrase { new Chunk(aux, fuenteResultado) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);


                        aux = string.Empty;
                        resultadoAux = _resultadoBc.ObtenerResultadoTest(muestra.CodigoMuestra, muestra.CodigoInternoLab, "NeoPhe");
                        if (resultadoAux != null)
                        {
                            aux = resultadoAux.ConcTexto;
                            if ((resultadoAux.ResultCode.Contains("20")))
                            {
                                fuenteResultado = _arial8NormalBlack;
                            }
                            else
                            {
                                fuenteResultado = _arial8BoldRed;
                            }

                        }

                        _phrase = new Phrase { new Chunk(aux, fuenteResultado) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        aux = string.Empty;
                        resultadoAux = _resultadoBc.ObtenerResultadoTest(muestra.CodigoMuestra, muestra.CodigoInternoLab, "TGAL");
                        if (resultadoAux != null)
                        {
                            aux = resultadoAux.ConcTexto;
                            if ((resultadoAux.ResultCode.Contains("20")))
                            {
                                fuenteResultado = _arial8NormalBlack;
                            }
                            else
                            {
                                fuenteResultado = _arial8BoldRed;
                            }

                        }

                        _phrase = new Phrase { new Chunk(aux, fuenteResultado) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);
                    }
                }

                document.Add(_table);



                #endregion

                #region PieReporte

                _table = new PdfPTable(2) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 10f };
                _table.SetWidths(new[] { 0.5f, 0.5f });

                DateTime diaImpresion = DateTime.Today;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
                _phrase = new Phrase { new Chunk("FECHA DE IMPRESION: " + diaImpresion.ToLongDateString().ToUpper() + "\n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);


                document.Add(_table);

                #endregion

                //nueva pagina
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                return bytes;


            }
        }

        #endregion
        #region ReportesResultadosEstablecimiento

        public byte[] ReporteResultadosxEstablecimiento(List<Vista_BuscarPaciente> listaPacientes)
        {
            string responsable1 = ConfigurationManager.AppSettings["responsables1"];
            string responsable2 = ConfigurationManager.AppSettings["responsables2"];
            string responsable3 = ConfigurationManager.AppSettings["responsables3"];
            string responsable4 = ConfigurationManager.AppSettings["responsables4"];

            var document = new Document(PageSize.A4, 80f, 80f, 40f, 40f);
            //document.
            using (var memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                //document.SetMargins(20f, 10f, 10f, 10f);
                document.Open();

                foreach (var paciente in listaPacientes)
                {
                    List<Vista_Resultado> listaResultados = _resultadoBc.ObtenerResutados(paciente.CodigoMuestra, paciente.CodigoInternoLab);
                    if (listaResultados.Count > 0)
                    {
                        document.NewPage();
                        //---------------------------------------
                        //Header Table
                        _table = new PdfPTable(3) { TotalWidth = 500f, LockedWidth = true };
                        _table.SetWidths(new[] { 0.4f, 0.3f, 0.4f });

                        _cell = ImageCell("~/Images/logoEssalud.png", 50f, Element.ALIGN_LEFT);
                        _table.AddCell(_cell);

                        var phrase2 = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                        _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        //cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        //Company Name and Address
                        _phrase = new Phrase { new Chunk(" \n", _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        document.Add(_table);

                        _table = new PdfPTable(3) { TotalWidth = 500f, LockedWidth = true };
                        _table.SetWidths(new[] { 0.4f, 0.3f, 0.4f });


                        var phrase1 = new Phrase { new Chunk(paciente.Establecimiento, _arial10NormalBlack) };
                        _cell = PhraseCell(phrase1, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        document.Add(_table);


                        _table = new PdfPTable(2) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 20f };
                        _table.SetWidths(new[] { 1f, 1f });

                        _cell = PhraseCell(new Phrase("LABORATORIO DE TAMIZAJE NEONATAL", _arial14BoldBlack), Element.ALIGN_CENTER);
                        _cell.Colspan = 2;
                        _table.AddCell(_cell);

                        _cell = PhraseCell(new Phrase("INFORME DE RESULTADOS", _arial10BoldBlack), Element.ALIGN_CENTER);
                        _cell.Colspan = 2;
                        _table.AddCell(_cell);

                        _cell = PhraseCell(new Phrase(), Element.ALIGN_CENTER);
                        _cell.Colspan = 2;
                        _table.AddCell(_cell);

                        _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase
                                      {
                                          new Chunk(" \n \n", _arial10NormalBlack),new Chunk("Codigo de Muestra: " + paciente.CodigoMuestra + "\n \n",_arial10NormalBlack),
                                          new Chunk("Recién Nacido: " + paciente.ApellidosNeonato + " , " +paciente.NombresNeonato + "\n \n", _arial10NormalBlack),
                                          new Chunk("Madre: " + paciente.ApellidosMadre + " , " + paciente.NombresMadre + "\n \n", _arial10NormalBlack),
                                          new Chunk("DNI: " + paciente.DNI + "\n \n", _arial10NormalBlack),new Chunk("Teléfono: " + paciente.Telefono1 + "\n \n", _arial10NormalBlack),
                                          new Chunk("Semana Gest.: " + paciente.EdadGestacional + "\n \n",_arial10NormalBlack),
                                          new Chunk("Peso(Kg): " + paciente.Peso + "    Talla(cm):" + paciente.Talla + "\n \n",_arial10NormalBlack)
                                      };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        _table.AddCell(_cell);

                        _phrase = new Phrase
                                     {
                                         new Chunk(" \n \n", _arial10NormalBlack),
                                         new Chunk(" \n \n", _arial10NormalBlack),
                                         new Chunk("Correlativo: " + paciente.CodigoInternoLab + "\n \n",_arial10NormalBlack)
                                     };

                        DateTime fechaAux;
                        if (DateTime.TryParse(paciente.FechaNacimiento.ToString(), out fechaAux))
                        {
                            _phrase.Add(new Chunk("Fecha de Nacimiento: " + fechaAux.ToShortDateString() + "\n \n", _arial10NormalBlack));
                        }
                        else
                        {
                            _phrase.Add(new Chunk("Fecha de Nacimiento: " + " " + "\n \n", _arial10NormalBlack));
                        }
                        if (DateTime.TryParse(paciente.FechaToma.ToString(), out fechaAux))
                        {
                            _phrase.Add(new Chunk("Fecha de Toma de Muestra: " + fechaAux.ToShortDateString() + "\n \n", _arial10NormalBlack));
                        }
                        else
                        {
                            _phrase.Add(new Chunk("Fecha de Toma de Muestra: " + " " + "\n \n", _arial10NormalBlack));
                        }

                        _phrase.Add(new Chunk("Número de Muestra: " + paciente.NumMuestra + "\n \n", _arial10NormalBlack));

                        //string sexo;
                        string sexo = paciente.Sexo == 1 ? "Femenino" : "Masculino";
                        _phrase.Add(new Chunk("Sexo: " + sexo + "\n \n", _arial10NormalBlack));

                        if (DateTime.TryParse(paciente.FechaRecepcion.ToString(), out fechaAux))
                        {
                            _phrase.Add(new Chunk("Fecha de Recepción: " + fechaAux.ToShortDateString() + "\n \n", _arial10NormalBlack));
                        }
                        else
                        {
                            _phrase.Add(new Chunk("Fecha de Recepción: " + " " + "\n \n", _arial10NormalBlack));
                        }

                        _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);
                        document.Add(_table);

                        _table = new PdfPTable(5) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 5f };
                        _table.SetWidths(new[] { 0.4f, 0.20f, 0.20f, 0.15f, 0.2f });

                        #region encabezado
                        _phrase = new Phrase { new Chunk("ERROR CONGENITO DEL METABOLISMO", _arial10BoldBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("PRUEBA", _arial10BoldBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("RESULTADO", _arial10BoldBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("UNIDAD", _arial10BoldBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("REFERENCIA", _arial10BoldBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        #endregion
                        //Por cada resultado de ensayo se crea una linea en la grilla del reporte.
                        int lineasBlancas = 4;
                        #region resultados

                        if (listaResultados.Count > 0)
                        {
                            foreach (var resultado in listaResultados)
                            {
                                _phrase = new Phrase { new Chunk(resultado.Nombre, _arial10NormalBlack) };
                                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                                _cell.VerticalAlignment = Element.ALIGN_TOP;
                                _cell.BorderColor = BaseColor.BLACK;
                                _table.AddCell(_cell);

                                _phrase = new Phrase { new Chunk(resultado.TestName, _arial10NormalBlack) };
                                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                                _cell.VerticalAlignment = Element.ALIGN_TOP;
                                _cell.BorderColor = BaseColor.BLACK;
                                _table.AddCell(_cell);

                                _phrase = new Phrase();
                                if (resultado.rdcDeterminationLevel > 20)
                                {
                                    _phrase.Add(new Chunk(resultado.ConcTexto, _arial12BoldRed));
                                }
                                else
                                {
                                    _phrase.Add(new Chunk(resultado.ConcTexto, _arial10NormalBlack));
                                }

                                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                                _cell.VerticalAlignment = Element.ALIGN_TOP;
                                _cell.BorderColor = BaseColor.BLACK;
                                _table.AddCell(_cell);

                                _phrase = new Phrase { new Chunk(resultado.Unidad, _arial10NormalBlack) };
                                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                                _cell.VerticalAlignment = Element.ALIGN_TOP;
                                _cell.BorderColor = BaseColor.BLACK;
                                _table.AddCell(_cell);

                                _phrase = new Phrase { new Chunk(resultado.Rango, _arial10NormalBlack) };
                                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                                _cell.VerticalAlignment = Element.ALIGN_TOP;
                                _cell.BorderColor = BaseColor.BLACK;
                                _table.AddCell(_cell);

                                lineasBlancas--;
                            }
                        }

                        document.Add(_table);


                        if (lineasBlancas > 0)
                        {
                            _table = new PdfPTable(1) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 2f };

                            for (int i = 0; i < lineasBlancas; i++)
                            {
                                _phrase = new Phrase { new Chunk(" ", _arial10NormalBlack) };
                                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                                _cell.VerticalAlignment = Element.ALIGN_TOP;
                                _table.AddCell(_cell);
                            }

                            document.Add(_table);
                        }

                        #endregion
                        //DrawLine(writer, 25f, document.Top - 79f, document.PageSize.Width - 25f, document.Top - 79f, BaseColor.BLACK);
                        //DrawLine(writer, 25f, document.Top - 300f, document.PageSize.Width - 25f, document.Top - 300f, BaseColor.BLACK);
                        //DrawLine(writer, 25f, document.Top - 400f, document.PageSize.Width - 25f, document.Top - 400f, BaseColor.BLACK);
                        #region PieReporte

                        _table = new PdfPTable(2) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 10f };
                        _table.SetWidths(new[] { 0.5f, 0.5f });
                        //cell.Colspan = 2;

                        DateTime diaImpresion = DateTime.Today;
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
                        _phrase = new Phrase { new Chunk("FECHA DE IMPRESION: " + diaImpresion.ToLongDateString().ToUpper() + "\n", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.Colspan = 2;
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("    ", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.Colspan = 2;
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("OBSERVACIONES:", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.Colspan = 2;
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("    " + paciente.Notas, _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.Colspan = 2;
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        //cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("    ", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.Colspan = 2;
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("    ", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.Colspan = 2;
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);



                        _phrase = new Phrase { new Chunk("PROCESADO POR : \n", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("    " + responsable1 + "\n", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("    " + responsable2 + "\n", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("    " + responsable3 + "\n", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("    " + responsable4 + "\n", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);



                        _phrase = new Phrase { new Chunk("    \n", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);



                        document.Add(_table);

                        //firmaDoctora

                        _table = new PdfPTable(3) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 20f };
                        _table.SetWidths(new[] { 0.3f, 0.4f, 0.3f });

                        _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);
                        _cell = ImageCell("~/Images/firma_dra.jpg", 30f, Element.ALIGN_LEFT);
                        _table.AddCell(_cell);

                        //segunda linea
                        _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColorTop = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("--------------------------------------------------", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        //tercera linea
                        _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(" ", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk("RESPONSABLE DE APROBACIÓN ", _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _table.AddCell(_cell);

                        document.Add(_table);


                        #endregion
                    }
                }
                //nueva pagina
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                return bytes;

            }
        }

        #endregion
        #region INMP

        //public byte[] ReporteResultadosxEstablecimiento(string establecimiento, string rangoFechas, List<Vista_BuscarPaciente> listaMuestras)
        public byte[] ReporteResultadosGSP(Ensayo ensayo, List<Vista_ResultadosGSP> listaResultados)
        {
            var document = new Document(PageSize.A4, 80f, 80f, 30f, 30f);
            //document.
            using (var memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                //document.SetMargins(20f, 10f, 10f, 10f);
                document.Open();

                document.NewPage();
                //Header Table
                _table = new PdfPTable(3) { TotalWidth = 500f, LockedWidth = true };
                _table.SetWidths(new[] { 0.4f, 0.3f, 0.4f });

                _cell = ImageCell("~/Images/LogoINMP.jpg", 50f, Element.ALIGN_LEFT);
                _table.AddCell(_cell);

                var phrase2 = new Phrase { new Chunk(" ", _arial12BoldBlack) };
                _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk(" \n", _arial10NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_RIGHT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                document.Add(_table);

                _table = new PdfPTable(2) { TotalWidth = 500f, LockedWidth = true };
                _table.SetWidths(new[] { 1f, 1f });

                _cell = PhraseCell(new Phrase("INFORME DE RESULTADOS", _arial14BoldBlack), Element.ALIGN_CENTER);
                _cell.Colspan = 2;
                _table.AddCell(_cell);

                _cell = PhraseCell(new Phrase(ensayo.TestName + " - " + ensayo.FechaFinish.ToShortDateString() + " - " + ensayo.AssayRunID , _arial10BoldBlack), Element.ALIGN_CENTER);
                _cell.Colspan = 2;
                _table.AddCell(_cell);

                _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _cell = PhraseCell(phrase2, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                document.Add(_table);

                _table = new PdfPTable(7) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 5f };
                _table.SetWidths(new[] { 0.07f, 0.07f, 0.1f, 0.38f, 0.38f, 0.16f, 0.16f });

                #region encabezado
                _phrase = new Phrase { new Chunk("#", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("POS", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("SEC", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("NOMBRES DE LA MADRE", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("PUESTO DE SALUD", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("RESULTADO", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("ESTADO", _arial8BoldBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _cell.BorderColor = BaseColor.BLACK;
                _table.AddCell(_cell);

              

                #endregion
                //Por cada resultado de ensayo se crea una linea en la grilla del reporte.

                #region resultados

                int num = 1;
                if (listaResultados.Count > 0)
                {
                    foreach (var resultado in listaResultados)
                    {
                        _phrase = new Phrase { new Chunk(num++.ToString(), _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(resultado.Pocillo, _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(resultado.CodigoMuestra, _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(resultado.NombresMadre , _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(resultado.Establecimiento, _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);

                        _phrase = new Phrase { new Chunk(resultado.ConcTexto, _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_TOP;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);


                        string estadoResultado = string.Empty;
                        if ((resultado.ResultCode.Contains("20")))
                        {
                            estadoResultado = "NORMAL";
                            _phrase = new Phrase { new Chunk(estadoResultado, _arial8NormalBlack) };
                        }
                        else
                        {
                            estadoResultado = "PATOLOGICO";
                            _phrase = new Phrase { new Chunk(estadoResultado, _arial8BoldRed) };
                        }

                        //_phrase = new Phrase { new Chunk(estadoResultado, _arial8NormalBlack) };
                        _cell = PhraseCell(_phrase, Element.ALIGN_CENTER);
                        _cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        _cell.BorderColor = BaseColor.BLACK;
                        _table.AddCell(_cell);


                    }
                }

                document.Add(_table);



                #endregion

                #region PieReporte

                _table = new PdfPTable(2) { TotalWidth = 500f, LockedWidth = true, SpacingBefore = 10f };
                _table.SetWidths(new[] { 0.5f, 0.5f });

                DateTime diaImpresion = DateTime.Today;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
                _phrase = new Phrase { new Chunk("FECHA DE IMPRESION: " + diaImpresion.ToLongDateString().ToUpper() + "\n", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);

                _phrase = new Phrase { new Chunk("    ", _arial8NormalBlack) };
                _cell = PhraseCell(_phrase, Element.ALIGN_LEFT);
                _cell.Colspan = 2;
                _cell.VerticalAlignment = Element.ALIGN_TOP;
                _table.AddCell(_cell);


                document.Add(_table);

                #endregion

                //nueva pagina
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                return bytes;
            }
        }
        #endregion
        #region Comunes
        private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, BaseColor color)
        {
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetColorStroke(color);
            contentByte.MoveTo(x1, y1);
            contentByte.LineTo(x2, y2);
            contentByte.Stroke();
        }
        private static PdfPCell PhraseCell(Phrase phrase, int align)
        {
            var cell = new PdfPCell(phrase)
                           {
                               BorderColor = BaseColor.WHITE,
                               VerticalAlignment = Element.ALIGN_TOP,
                               HorizontalAlignment = align,
                               PaddingBottom = 2f,
                               PaddingTop = 0f
                           };
            return cell;
        }
        private static PdfPCell ImageCell(string path, float scale, int align)
        {
            var image = Image.GetInstance(HttpContext.Current.Server.MapPath(path));
            image.ScalePercent(scale);
            var cell = new PdfPCell(image)
                           {
                               BorderColor = BaseColor.WHITE,
                               VerticalAlignment = Element.ALIGN_TOP,
                               HorizontalAlignment = align,
                               PaddingBottom = 0f,
                               PaddingTop = 0f
                           };
            return cell;
        }
        #endregion
    }
}