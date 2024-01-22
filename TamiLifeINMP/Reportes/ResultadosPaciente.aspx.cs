using System;
using System.Configuration;
using System.Data;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Threading;
using System.Globalization;
using BE;
using BC;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace TamizajePortal.Reportes
{
    public partial class ResultadosPaciente : System.Web.UI.Page
    {
        string nombreResponsables = ConfigurationManager.AppSettings["responsables"];
        readonly ResultadoBC resultadoBC = new ResultadoBC();
        readonly MuestraBC muestraBC = new MuestraBC();
        readonly MuestraCompletaBC muestraCompletaBC = new MuestraCompletaBC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["DNI"] != null)
            {
                //btnImprimir.Enabled = true;
                txtDNI.Text = Request["DNI"];
                hdfDNI.Value = Request["DNI"];
                CargarGrilla();
            }
            else
            {
                btnImprimir.Enabled = false;
                dgvResultado.DataSource = null;
                dgvResultado.DataBind();
            }
        }

        private void CargarGrilla()
        {
            string dni = txtDNI.Text;

            if (String.CompareOrdinal(dni, "") != 0)
            {

                List<VistaResultadoDNI> listaResultados = resultadoBC.ReporteDNI(dni);
                if (listaResultados.Count > 0)
                {
                    dgvResultado.DataSource = listaResultados;
                    dgvResultado.DataBind();
                    Session["dni"] = dni;

                    VistaResultadoDNI resultado = listaResultados[0];

                    lblNombreMadre.Text = resultado.Madre;
                    lblDNI.Text = resultado.DNI;
                    lblEstablecimiento.Text = resultado.Establecimiento;
                    DateTime nacimiento = DateTime.Parse(resultado.FechaNacimiento.ToString());
                    lblFechaNacimiento.Text = nacimiento.ToShortDateString();
                    lblHClinica.Text = resultado.HistoriaClinica;

                    btnImprimir.Enabled = true;
                    panelResultados.Visible = true;
                    //db.RegistrarLogReporte(log);
                }
                else
                {
                    btnImprimir.Enabled = false;
                    lblMensaje.Text = "No se encontraron resultados";
                    lblMensaje.Visible = true;
                }
            }
            else
            {
                lblMensaje.Text = "Ingrese su DNI";
                lblMensaje.Visible = true;
            }
        }
         
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
                CargarGrilla();
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            string dni = Session["dni"].ToString();
            //List<MuestraCompletaBE> listaMuestras2 = muestraCompletaBC.ObtenerMuestrasxDNI(dni);
            List<Vista_CodigoMuestraxDNI> listaMuestras = muestraBC.ObtenerCantidadMuestras(dni);

            if (listaMuestras.Count > 0)
            {
                ReportePDF(listaMuestras);
            }
        }

        #region reporteImpreso
        private void ReportePDF(List<Vista_CodigoMuestraxDNI> listaMuestras)
        {
            int estiloFuenteTexto = Font.NORMAL;
            Document document = new Document(PageSize.A4, 80f, 80f, 40f, 40f);
            //document.
            Font NormalFont = FontFactory.GetFont("Arial", 12, estiloFuenteTexto, BaseColor.BLACK);
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                BaseColor color = null;


                //int textSize = 8;
                int textSize = 8;
                int textSizeOtros = 6;

                //document.SetMargins(20f, 10f, 10f, 10f);
                document.Open();

                foreach (Vista_CodigoMuestraxDNI muestra in listaMuestras)
                {
                    document.NewPage();
                    string codigoMuestra = muestra.CodigoMuestra;
                    List<VistaResultadoDNI> listaResultadosDNI = resultadoBC.ObtenerResutadoCodigoMuestra(codigoMuestra);
                    VistaResultadoDNI resultadoDNI = listaResultadosDNI[0];
                    //Header Table
                    table = new PdfPTable(3);
                    table.TotalWidth = 500f;
                    table.LockedWidth = true;
                    table.SetWidths(new float[] { 0.4f, 0.3f, 0.4f });

                    Phrase phrase1 = new Phrase();
                    phrase1.Add(new Chunk("Ministerio de Salud \nInstituto Nacional Materno Perinatal \nServicio Genetica", FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                    cell = PhraseCell(phrase1, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    //cell.BorderColor = BaseColor.BLACK;
                    table.AddCell(cell);

                    Phrase phrase2 = new Phrase();
                    phrase2.Add(new Chunk(" ", FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.RED)));
                    cell = PhraseCell(phrase2, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    //cell.BorderColor = BaseColor.BLACK;
                    table.AddCell(cell);

                    //Company Name and Address
                    phrase = new Phrase();
                    phrase.Add(new Chunk("Laboratorio de Errores Innatos del \nMetabolismo y Tamizaje Neonatal\n", FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    //cell.BorderColor = BaseColor.BLACK;
                    table.AddCell(cell);

                    //Separater Line
                    color = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#A9A9A9"));
                    //iTextSharp.text.pdf.PdfLine line = new PdfLine();

                    document.Add(table);

                    table = new PdfPTable(2);
                    table.SetWidths(new float[] { 1f, 1f });
                    table.TotalWidth = 500f;
                    table.LockedWidth = true;
                    table.SpacingBefore = 20f;
                    //table.HorizontalAlignment = Element.ALIGN_RIGHT;


                    //Employee Details
                    cell = PhraseCell(new Phrase("INFORME DE TAMIZAJE NEONATAL", FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell.Colspan = 2;
                    //cell.BorderColor = BaseColor.BLACK;
                    table.AddCell(cell);

                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                    cell.Colspan = 2;
                    //cell.PaddingBottom = 30f;
                    //cell.BorderColor = BaseColor.BLACK;
                    table.AddCell(cell);

                    cell = PhraseCell(phrase2, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    //cell.BorderColor = BaseColor.BLACK;
                    table.AddCell(cell);

                    cell = PhraseCell(phrase2, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    //cell.BorderColor = BaseColor.BLACK;
                    table.AddCell(cell);

                    //Photo
                    //cell = ImageCell(string.Format("~/photos/{0}.jpg", "perfil"/*dr["EmployeeId"]*/), 25f, PdfPCell.ALIGN_CENTER);
                    //table.AddCell(cell);

                    //Name

                    phrase = new Phrase();
                    phrase.Add(new Chunk(" \n \n", FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                    phrase.Add(new Chunk("NOMBRE DEL NEONATO: " + resultadoDNI.Neonato + "\n \n", FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                    phrase.Add(new Chunk("AUTOGENERADO / HISTORIA: " + resultadoDNI.HistoriaClinica + "\n \n", FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                    phrase.Add(new Chunk("DNI DE LA MADRE: " + resultadoDNI.DNI + "\n \n", FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                    DateTime recepcion;
                    string recep = string.Empty;
                    if (DateTime.TryParse(resultadoDNI.FechaRecepcion.ToString(), out recepcion))
                        recep = recepcion.ToShortDateString();
                    phrase.Add(new Chunk("FECHA DE RECEPCION : " + recep + "\n \n", FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    //cell.BorderColor = BaseColor.BLACK;
                    table.AddCell(cell);

                    DateTime FECHA = DateTime.Today;

                    string AUX = FECHA.ToShortDateString();



                    phrase = new Phrase();
                    phrase.Add(new Chunk(" \n \n", FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                    phrase.Add(new Chunk("CODIGO CORRELATIVO: " + resultadoDNI.CodigoCorrelativo + "\n \n", FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                    phrase.Add(new Chunk("CODIGO TARJETA: " + resultadoDNI.CodigoMuestra + "\n \n", FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                    DateTime nacimiento = DateTime.Parse(resultadoDNI.FechaNacimiento.ToString());
                    phrase.Add(new Chunk("FECHA DE NACIMIENTO: " + nacimiento.ToShortDateString() + "\n \n", FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                    DateTime toma = DateTime.Parse(resultadoDNI.FechaToma.ToString());
                    phrase.Add(new Chunk("FECHA DE TOMA DE MUESTRA: " + toma.ToShortDateString() + "\n \n", FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    //cell.BorderColor = BaseColor.BLACK;
                    table.AddCell(cell);
                    document.Add(table);


                    table = new PdfPTable(1);
                    table.TotalWidth = 500f;
                    table.LockedWidth = true;

                    phrase = new Phrase();
                    phrase.Add(new Chunk("FECHA DE RESULTADO: " + resultadoDNI.FechaResultado.ToShortDateString() + "\n \n", FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                    //phrase.Add(new Chunk("ERROR CONGENITO DEL METABOLISMO", FontFactory.GetFont("Arial", textSize, Font.BOLD, BaseColor.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    //cell.BorderColor = BaseColor.BLACK;
                    table.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("ESTABLECIMIENTO : " + resultadoDNI.Establecimiento + "\n \n", FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                    //phrase.Add(new Chunk("ERROR CONGENITO DEL METABOLISMO", FontFactory.GetFont("Arial", textSize, Font.BOLD, BaseColor.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    //cell.BorderColor = BaseColor.BLACK;
                    table.AddCell(cell);

                    document.Add(table);

                    string notasMuestra = resultadoDNI.Notas;

                    table = new PdfPTable(5);
                    table.TotalWidth = 500f;
                    table.LockedWidth = true;
                    table.SetWidths(new float[] { 0.4f, 0.2f, 0.2f, 0.2f, 0.2f });
                    table.LockedWidth = true;
                    table.SpacingBefore = 5f;

                    phrase = new Phrase();
                    phrase.Add(new Chunk("ERROR CONGENITO DEL METABOLISMO", FontFactory.GetFont("Arial", textSize, Font.BOLD, BaseColor.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    cell.BorderColor = BaseColor.BLACK;
                    table.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("ANALITO", FontFactory.GetFont("Arial", textSize, Font.BOLD, BaseColor.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    cell.BorderColor = BaseColor.BLACK;
                    table.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("METODO", FontFactory.GetFont("Arial", textSize, Font.BOLD, BaseColor.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    cell.BorderColor = BaseColor.BLACK;
                    table.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("RESULTADO", FontFactory.GetFont("Arial", textSize, Font.BOLD, BaseColor.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    cell.BorderColor = BaseColor.BLACK;
                    table.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("VALOR REFERENCIAL", FontFactory.GetFont("Arial", textSize, Font.BOLD, BaseColor.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    cell.BorderColor = BaseColor.BLACK;
                    table.AddCell(cell);

                    //Por cada resultado de ensayo se crea una linea en la grilla del reporte.
                    int lineasBlancas = 4;
                    #region resultados

                    if (listaResultadosDNI.Count > 0) //(dt.Rows.Count > 0)
                    {
                        foreach (VistaResultadoDNI resultado_actual in listaResultadosDNI)
                        {
                            phrase = new Phrase();
                            phrase.Add(new Chunk(resultado_actual.Test, FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                            cell.BorderColor = BaseColor.BLACK;
                            table.AddCell(cell);

                            phrase = new Phrase();
                            phrase.Add(new Chunk(resultado_actual.Analito, FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                            cell.BorderColor = BaseColor.BLACK;
                            table.AddCell(cell);

                            phrase = new Phrase();
                            phrase.Add(new Chunk(resultado_actual.Metodo, FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                            cell.BorderColor = BaseColor.BLACK;
                            table.AddCell(cell);

                            phrase = new Phrase();
                            phrase.Add(new Chunk(resultado_actual.Conc + " " + resultado_actual.Unidad, FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                            cell.BorderColor = BaseColor.BLACK;
                            table.AddCell(cell);

                            phrase = new Phrase();
                            phrase.Add(new Chunk(resultado_actual.Rango + " " + resultado_actual.Unidad, FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                            cell.BorderColor = BaseColor.BLACK;
                            table.AddCell(cell);

                            lineasBlancas--;
                        }
                    }

                    document.Add(table);


                    if (lineasBlancas > 0)
                    {
                        table = new PdfPTable(1);
                        table.TotalWidth = 500f;
                        table.LockedWidth = true;
                        //table.SetWidths(new float[] { 0.4f, 0.2f, 0.2f, 0.2f, 0.2f });
                        table.LockedWidth = true;
                        table.SpacingBefore = 2f;

                        for (int i = 0; i < lineasBlancas; i++)
                        {
                            phrase = new Phrase();
                            phrase.Add(new Chunk(" ", FontFactory.GetFont("Arial", textSize, estiloFuenteTexto, BaseColor.BLACK)));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                            table.AddCell(cell);
                        }

                        document.Add(table);
                    }

                    #endregion
                    DrawLine(writer, 25f, document.Top - 79f, document.PageSize.Width - 25f, document.Top - 79f, BaseColor.BLACK);
                    DrawLine(writer, 25f, document.Top - 300f, document.PageSize.Width - 25f, document.Top - 300f, BaseColor.BLACK);
                    //DrawLine(writer, 25f, document.Top - 400f, document.PageSize.Width - 25f, document.Top - 400f, BaseColor.BLACK);

                    #region PieReporte
                    table = new PdfPTable(1);
                    table.TotalWidth = 500f;
                    table.LockedWidth = true;
                    //table.SetWidths(new float[] { 0.2f, 0.8f });
                    table.LockedWidth = true;
                    table.SpacingBefore = 10f;

                    phrase = new Phrase();
                    phrase.Add(new Chunk("PROCESADO POR : " + nombreResponsables, FontFactory.GetFont("Arial", textSizeOtros, estiloFuenteTexto, BaseColor.BLACK)));
                    //phrase.Add(new Chunk("ERROR CONGENITO DEL METABOLISMO", FontFactory.GetFont("Arial", textSize, Font.BOLD, BaseColor.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    //cell.BorderColor = BaseColor.BLACK;
                    table.AddCell(cell);


                    phrase = new Phrase();
                    DateTime diaImpresion = DateTime.Today;
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
                    phrase.Add(new Chunk("FECHA DE IMPRESION: " + diaImpresion.ToLongDateString().ToUpper(), FontFactory.GetFont("Arial", textSizeOtros, estiloFuenteTexto, BaseColor.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    //cell.BorderColorTop = BaseColor.BLACK;
                    table.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("OBSERVACIONES:", FontFactory.GetFont("Arial", textSizeOtros, estiloFuenteTexto, BaseColor.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    //cell.BorderColor = BaseColor.BLACK;
                    table.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("        " + notasMuestra, FontFactory.GetFont("Arial", textSizeOtros, estiloFuenteTexto, BaseColor.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    table.AddCell(cell);

                    document.Add(table);
                    #region firmaDoctora
                    table = new PdfPTable(3);
                    table.TotalWidth = 500f;
                    table.LockedWidth = true;
                    table.SetWidths(new float[] { 0.3f, 0.4f, 0.3f });
                    table.LockedWidth = true;
                    table.SpacingBefore = 20f;

                    phrase = new Phrase();
                    phrase.Add(new Chunk(" ", FontFactory.GetFont("Arial", textSizeOtros, estiloFuenteTexto, BaseColor.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    cell.BorderColorTop = BaseColor.BLACK;
                    //cell = ImageCell("~/photos/firma_claudia.jpg", 30f, PdfPCell.ALIGN_LEFT);
                    table.AddCell(cell);

                    phrase = new Phrase();
                    phrase.Add(new Chunk(" ", FontFactory.GetFont("Arial", textSizeOtros, estiloFuenteTexto, BaseColor.BLACK)));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    cell.BorderColorTop = BaseColor.BLACK;
                    //cell = ImageCell("~/photos/firma_gladys.jpg", 30f, PdfPCell.ALIGN_LEFT);
                    table.AddCell(cell);

                    //cell = ImageCell("~/photos/firma_dra.jpg", 30f, PdfPCell.ALIGN_LEFT);
                    //table.AddCell(cell);

                    document.Add(table);
                    #endregion
                    #endregion
                }


                //nueva pagina
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Session["dni"] + ".pdf");
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + txtDNI.Text + ".pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
            }
        }


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
            PdfPCell cell = new PdfPCell(phrase);
            cell.BorderColor = BaseColor.WHITE;
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 2f;
            cell.PaddingTop = 0f;
            return cell;
        }
        private static PdfPCell ImageCell(string path, float scale, int align)
        {
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path));
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.BorderColor = BaseColor.WHITE;
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 0f;
            cell.PaddingTop = 0f;
            return cell;
        }
        #endregion
    }
}