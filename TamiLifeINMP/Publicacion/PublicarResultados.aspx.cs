using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using BC;
using BE;

namespace TamiLifeSA.Publicacion
{
    public partial class PublicarResultados : System.Web.UI.Page
    {
        //Instancias de Negocio --------------------------------------------------
        private readonly ResultadoBC resultadoBC = new ResultadoBC();
        private readonly EnsayoBC ensayoBC = new EnsayoBC();
        private readonly ParametroGeneralBC parametroGeneralBC = new ParametroGeneralBC();

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
                        {
                            Master.CambiarSiteMap("CentralSiteMap");
                        }
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

                if (Request["ID"] != null)
                {
                    hdnIdEnsayo.Value = Request["ID"];
                    int ensayoId = int.Parse(hdnIdEnsayo.Value);
                    Ensayo ensayo = ensayoBC.ObtenerEnsayoId(ensayoId);
                    lblEnsayo.Text = ensayo.AssayRunID.ToString();
                    lblPrueba.Text = ensayo.TestName;
                    lblUnidad.Text = ensayo.Prueba.Unidad;
                    lblFechaResultado.Text = ensayo.FechaFinish.ToShortDateString();
                    lblKitLot.Text = ensayo.Kitlot;
                    lblEquipo.Text = ensayo.Instrument;
                    CargarTipoResultado();
                    var publicado = bool.Parse(ensayo.Publicado.ToString());
                    btnPublicar.Enabled = !publicado;
                    btnPublicar.Visible = !publicado;
                    chkAll.Visible = !publicado;

                    lblTipoResultado.Visible = publicado;
                    ddlTipoResultados.Visible = publicado;
                    lblCodigoCorrelativo.Visible = publicado;
                    txtCodigoCorrelativo.Visible = publicado;
                    btnActualizar.Visible = publicado;
                    btnFiltrar.Visible = publicado;
                    ddlEstadoPublicado.Visible = publicado;
                    lblPublicado.Visible = publicado;

                    CargarGrilla(ensayoId, 1, string.Empty, string.Empty, 0,string.Empty,"2");
                    CargarTipoResultado();
                }
                else
                {
                    Response.Redirect("~/Publicacion/AdministrarPublicacion.aspx");
                }
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigoCorrelativo.Text;
            int tipoResultado = int.Parse(ddlTipoResultados.SelectedItem.Value);
            string operador = ddlOperador.SelectedItem.Value;
            string concentracion = txtConcentracion.Text;
            int idEnsayo = int.Parse(hdnIdEnsayo.Value);
            string publicado = ddlEstadoPublicado.SelectedItem.Value;
            CargarGrilla(idEnsayo, 1, operador, concentracion, tipoResultado,codigo,publicado);
        }


        protected void btnPublicar_Click(object sender, EventArgs e)
        {
            PublicarSeleccion();
            dgvResultados.DataSource = string.Empty;
            chkAll.Checked = false;
            Response.Redirect("~/Publicacion/AdministrarPublicacion.aspx");
        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            CambiarEstadoCheckBox(chkAll.Checked);
        }

        private void PublicarSeleccion()
        {
            int idEnsayo = int.Parse(hdnIdEnsayo.Value);
            string listaCodigosNoPublicados = string.Empty;
            string listaIdDetalleEnsayoNoPublicado = string.Empty;
            int i = 0;
            foreach (GridViewRow row in dgvResultados.Rows)
            {
                // Access the CheckBox
                var cb = (CheckBox)row.FindControl("chkAgregar");
                if (cb != null)
                {
                    if (!cb.Checked)
                    {
                        string idDetalleEnsayo = ((HiddenField)row.Cells[0].FindControl("hdnIdDetalleEnsayo")).Value;
                        if (i == 0)
                        {
                            //string codigoMuestra = row.Cells[3].Text;
                            //string conc = row.Cells[5].Text;
                            //int idDetalle = resultadoBC.ObtenerIdDetalleEnsayo(codigoMuestra, conc);
                            listaIdDetalleEnsayoNoPublicado = string.Concat(idDetalleEnsayo);
                            listaCodigosNoPublicados = string.Concat("'", row.Cells[3].Text, "'"); //codigo correlativo no publicado
                        }
                        else
                        {
                            listaIdDetalleEnsayoNoPublicado = string.Concat(listaIdDetalleEnsayoNoPublicado, ",", idDetalleEnsayo);
                            listaCodigosNoPublicados = string.Concat(listaCodigosNoPublicados, ",'", row.Cells[3].Text, "'");
                        }
                        i++;
                    }
                }
            }
            try
            {
                //resultadoBC.PublicarResultados(listaCodigosResultadosNoPublicados, idEnsayo);
                resultadoBC.PublicarResultados(listaCodigosNoPublicados, idEnsayo, HttpContext.Current.User.Identity.Name, listaIdDetalleEnsayoNoPublicado);
                //resultadoBC.
                ensayoBC.PublicarEnsayo(idEnsayo, HttpContext.Current.User.Identity.Name);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void CargarTipoResultado()
        {
            List<ParametroGeneral> listaTipoResultado = parametroGeneralBC.ListarTipoResultados();
            ddlTipoResultados.DataSource = listaTipoResultado;
            ddlTipoResultados.DataTextField = "ValorTexto";
            ddlTipoResultados.DataValueField = "ValorEntero";
            ddlTipoResultados.DataBind();
        }

        private void CargarGrilla(int EnsayoId, int estado, string operador, string concentracion, int tipoResultado, string codigoCorrelativo, string publicado)
        {
            List<Vista_ResultadosGSP> listaResultados = resultadoBC.ObtenerResultadosPublicacion(EnsayoId, operador, concentracion, tipoResultado, codigoCorrelativo,publicado);
            dgvResultados.DataSource = listaResultados;
            dgvResultados.DataBind();
            lblNumRegistros.Text = "Registros Consultados: " + listaResultados.Count();
            lblNumRegistros.Visible = true;
        }

        private void CambiarEstadoCheckBox(bool estadoCheckBox)
        {
            // Iterate through the Products.Rows property
            foreach (GridViewRow row in dgvResultados.Rows)
            {
                // Access the CheckBox
                var cb = (CheckBox)row.FindControl("chkAgregar");
                if (cb != null)
                    cb.Checked = estadoCheckBox;
            }
        }

        protected void dgvResultados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            //string codigoAnterior = hdnCorrelativoAnterior.Value;
            
            //if (e.Row.Cells[3].Text.CompareTo(codigoAnterior)==0)
            //{
            //    e.Row.Font.Bold = true;
            //} else
            //{
            //    hdnCorrelativoAnterior.Value = e.Row.Cells[3].Text;
            //}

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string rep = ((HiddenField) e.Row.Cells[0].FindControl("hdnRepeticion")).Value;
                if (rep.CompareTo("False")==0)
                {
                    e.Row.Font.Bold = true;
                }

                int determinationLevel = Convert.ToInt32(e.Row.Cells[9].Text);

                if (determinationLevel == 20)
                {
                    e.Row.Cells[8].Text = "NORMAL";

                }

                if (determinationLevel == 40)
                {
                    e.Row.Cells[8].Text = "BORDERLINE";
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                }

                if (determinationLevel == 60)
                {
                    e.Row.Cells[8].Text = "HIGH";
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                }

                //string peso = e.Row.Cells[14].Text;
                //if (peso.Length > 4)
                //{
                //    e.Row.Cells[14].Text = peso.Remove(peso.Length - 4, 4);
                //}
                
            }
            e.Row.Cells[9].Visible = false;
        }

        private void ActualizarPublicacion()
        {
            foreach (GridViewRow row in dgvResultados.Rows)
            {
                string codigoCorrelativo = row.Cells[3].Text;   //SampleCodigo
                string conc = row.Cells[5].Text;                //ConcText
                string analito = lblPrueba.Text;
                string numEnsayo = lblEnsayo.Text;           //AssayRunID
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

                    }
                    else
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

        protected void ddlTipoResultados_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string codigoCorrelativo = txtCodigoCorrelativo.Text;
            //int tipoResultado = int.Parse(ddlTipoResultados.SelectedItem.Value);
            //string operador = ddlOperador.SelectedItem.Value;
            //string concentracion = txtConcentracion.Text;
            //int idEnsayo = int.Parse(hdnIdEnsayo.Value);
            //CargarGrilla(idEnsayo, 1, operador, concentracion, tipoResultado, codigoCorrelativo);
        }


    }
}