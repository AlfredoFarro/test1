using System;
using System.Web;
using System.Text;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Configuration;
using System.Collections.Generic;
using BC;
using BE;

namespace TamiLifeSA.Resultados
{
    public partial class BuscarResultado : System.Web.UI.Page
    {
        //Instancias de Negocio --------------------------------------------------
        private readonly ResultadoBC resultadoBC = new ResultadoBC();
        private readonly EnsayoBC ensayoBC = new EnsayoBC();
        //private readonly ParametroGeneralBC parametroGeneralBC = new ParametroGeneralBC();
        
        //Eventos ----------------------------------------------------------------
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                bool usuarioLogeado = (HttpContext.Current.User != null) &&
                      HttpContext.Current.User.Identity.IsAuthenticated;
                if (usuarioLogeado)
                {
                    if (HttpContext.Current.User.IsInRole("Administrador"))
                    {
                        Master.CambiarSiteMap("AdminSiteMap");
                    }
                    else
                    {
                        if (HttpContext.Current.User.IsInRole("Central"))
                            Master.CambiarSiteMap("CentralSiteMap");
                        else
                        {
                            Response.Redirect("~/Default.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //DateTime aux = new DateTime();
            //dgvResultados.DataSource = resultadoBC.BuscarResultadoxCodigoMuestra(txtCodigoMuestra.Text);
            CargarGrilla();

        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportGridToCSV();
        }
        #endregion

        //Metodos ----------------------------------------------------------------
        private List<Vista_ResultadosGSP> ObtenerResultadosGrilla()
        {
            var listaResultados = new List<Vista_ResultadosGSP>();

            if ((txtCodigoMuestra.Text != null) && (txtCodigoMuestra.Text.CompareTo(string.Empty) != 0))
            {
                listaResultados = resultadoBC.BuscarResultados(txtCodigoMuestra.Text, 0);

                lblFechaResultado.Visible = false;
                lblNumEnsayo.Visible = false;
                lblPrueba.Visible = false;
            }

            if (txtRunID.Text.CompareTo(string.Empty) != 0)
            {
                Ensayo ensayoAux;
                int auxNumEnsayo = 0;
                if (int.TryParse(txtRunID.Text, out auxNumEnsayo))
                {
                    ensayoAux = ensayoBC.ObtenerEnsayoRunId(auxNumEnsayo);
                    listaResultados = resultadoBC.BuscarResultados(null, ensayoAux.idEnsayo);

                    lblFechaResultado.Text = "Fecha de Proceso: " + ensayoAux.FechaFinish.ToShortDateString();
                    lblFechaResultado.Visible = true;

                    lblNumEnsayo.Text = "N° Ensayo: " + auxNumEnsayo;
                    lblNumEnsayo.Visible = true;

                    lblPrueba.Text = "Prueba: " + ensayoAux.TestName;
                    lblPrueba.Visible = true;
                }
            }

            return listaResultados;
        }

        private void CargarGrilla()
        {
            var listaResultados = ObtenerResultadosGrilla();
            dgvResultados.DataSource = listaResultados;
            dgvResultados.DataBind();

            lblNumRegistros.Text = "Registros Consultados: " + listaResultados.Count;
            lblNumRegistros.Visible = true;
        }
        private void ExportGridToCSV()
        {
            //CargarGrilla();

            List<Vista_ResultadosGSP> listaResultados = ObtenerResultadosGrilla();

            Encoding encoding = Encoding.UTF8;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename= Export.csv");
            Response.Charset = encoding.EncodingName;
            Response.ContentEncoding = Encoding.Unicode;
            Response.ContentType = "application/text";

            string tabulador = ConfigurationManager.AppSettings["tabulador"];
            var columnbind = new StringBuilder();

            if (txtRunID.Text.CompareTo(string.Empty) != 0)
            {
                Ensayo ensayoAux;
                int auxNumEnsayo = 0;
                if (int.TryParse(txtRunID.Text, out auxNumEnsayo))
                {
                    ensayoAux = ensayoBC.ObtenerEnsayoRunId(auxNumEnsayo);
                    columnbind.Append("N° de Ensayo:" + tabulador + ensayoAux.AssayRunID + "\r\n");
                    columnbind.Append("Fecha de Proceso:" + tabulador + ensayoAux.FechaFinish.ToShortDateString() + "\r\n");
                    columnbind.Append("Prueba:" + tabulador + ensayoAux.TestName + "\r\n");
                }
            }
            //Encabezado
            string headers = string.Concat("Código Muestra", tabulador,
                                            "Apellidos RN", tabulador,
                                            "Apellidos de Mamá", tabulador,
                                            "Establecimiento Origen", tabulador,
                                            "Conc", tabulador,
                                            "N de Muestra", tabulador,
                                            "Fecha de Nacimiento", tabulador,
                                            "Fecha de Toma de Muestra", tabulador,
                                            "Semanas de Gestación", tabulador);
            columnbind.Append(headers);
            columnbind.Append("\r\n");

            //Resultados
            foreach (Vista_ResultadosGSP resultado in listaResultados)
            {
                string linea = string.Concat(resultado.CodigoMuestra, tabulador,
                                                 resultado.Apellidos, tabulador,
                                                 resultado.ApellidosMadre, tabulador,
                                                 resultado.Establecimiento, tabulador,
                                                 resultado.ConcTexto, tabulador,
                                                 resultado.NumMuestra, tabulador);

                if (resultado.FechaNacimiento != null)
                {
                    DateTime fechaNacimiento;
                    if (DateTime.TryParse(resultado.FechaNacimiento.ToString(), out fechaNacimiento))
                    {
                        linea = string.Concat(linea, fechaNacimiento.ToShortDateString(), tabulador);
                    }
                    else
                    {
                        linea = string.Concat(linea, "", tabulador);
                    }
                }
                else
                {
                    linea = string.Concat(linea, "", tabulador);
                }

                if (resultado.FechaToma != null)
                {
                    DateTime fechaToma;
                    if (DateTime.TryParse(resultado.FechaToma.ToString(), out fechaToma))
                    {
                        linea = string.Concat(linea, fechaToma.ToShortDateString(), tabulador);
                    }
                    else
                    {
                        linea = string.Concat(linea, "", tabulador);
                    }
                }
                else
                {
                    linea = string.Concat(linea, "", tabulador);
                }

                linea = string.Concat(linea,resultado.EdadGestacional, tabulador);
                columnbind.Append(linea);
                columnbind.Append("\r\n");
            }
            Response.Output.Write(columnbind.ToString());
            Response.Flush();
            Response.End();

        }

        private void ActualizarPublicacion()
        {
            foreach (GridViewRow row in dgvResultados.Rows)
            {
                string codigoCorrelativo = row.Cells[0].Text;   //SampleCodigo
                string conc = row.Cells[4].Text;                //ConcText
                string analito = row.Cells[5].Text;
                string numEnsayo = row.Cells[6].Text;           //AssayRunID
                string publicadoINMP = "NV";
                bool publicado = false;
                // Access the CheckBox
                var cb = (CheckBox)row.FindControl("chkAgregar");
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        publicadoINMP = "SV";
                        publicado = true;
                        
                    }else
                    {
                        publicadoINMP = "NV";
                    }
                }
                string idDetalleEnsayo = ((HiddenField)row.Cells[0].FindControl("hdnIdDetalleEnsayo")).Value;
                string idResultado = ((HiddenField)row.Cells[0].FindControl("hdnIdResultado")).Value;
                resultadoBC.ActualizarPublicacion(int.Parse(idDetalleEnsayo), int.Parse(idResultado), publicado);
                resultadoBC.ActualizarPublicacionINMP(codigoCorrelativo, conc, analito, numEnsayo, publicadoINMP, int.Parse(idDetalleEnsayo));
            }
            
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarPublicacion();
        }

    }
}