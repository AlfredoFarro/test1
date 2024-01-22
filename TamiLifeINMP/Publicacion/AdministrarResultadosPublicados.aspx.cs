using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BC;
using BE;

namespace TamiLifeSA.Publicacion
{
    public partial class AdministrarResultadosPublicados : System.Web.UI.Page
    {
        //Instancias de Negocio --------------------------------------------------
        private readonly ResultadoBC resultadoBC = new ResultadoBC();
        private readonly EnsayoBC ensayoBC = new EnsayoBC();
        private readonly ParametroGeneralBC parametroGeneralBC = new ParametroGeneralBC();
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
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //DateTime aux = new DateTime();
            //dgvResultados.DataSource = resultadoBC.BuscarResultadoxCodigoMuestra(txtCodigoMuestra.Text);

            CargarGrilla();

        }
        
        #endregion

        //Metodos ----------------------------------------------------------------
        private List<Vista_ResultadosGSP> ObtenerResultadosGrilla()
        {
            string dni = txtDNI.Text;
            string apellidosMadre = txtApellidosMadre.Text;
            var listaResultados = new List<Vista_ResultadosGSP>();

            if ((txtCodigoMuestra.Text != null) && (txtCodigoMuestra.Text.CompareTo(string.Empty) != 0))
            {
                listaResultados = resultadoBC.BuscarResultadoPorPublicar(txtCodigoMuestra.Text, 0);
            }

            if (txtRunID.Text.CompareTo(string.Empty) != 0)
            {
                Ensayo ensayoAux;
                int auxNumEnsayo = 0;
                if (int.TryParse(txtRunID.Text, out auxNumEnsayo))
                {
                    ensayoAux = ensayoBC.ObtenerEnsayoRunId(auxNumEnsayo);
                    listaResultados = resultadoBC.BuscarResultadoPorPublicar(null, ensayoAux.idEnsayo);


                }
            }else
            {
                listaResultados = resultadoBC.BuscarResultadoPorPublicar(txtCodigoMuestra.Text, 0, dni, apellidosMadre);
            }

            


            return listaResultados;
        }
        //private List<Vista_ResultadosMuestra> ObtenerResultadosGrilla()
        //{
        //    var listaResultados = new List<Vista_ResultadosMuestra>();

        //    if ((txtCodigoMuestra.Text != null) && (txtCodigoMuestra.Text.CompareTo(string.Empty) != 0))
        //    {
        //        listaResultados = resultadoBC.BuscarResultados(txtCodigoMuestra.Text, 0);

                
        //    }

        //    if (txtRunID.Text.CompareTo(string.Empty) != 0)
        //    {
        //        Ensayo ensayoAux;
        //        int auxNumEnsayo = 0;
        //        if (int.TryParse(txtRunID.Text, out auxNumEnsayo))
        //        {
        //            ensayoAux = ensayoBC.ObtenerEnsayoRunId(auxNumEnsayo);
        //            listaResultados = resultadoBC.BuscarResultados(null, ensayoAux.idEnsayo);

                    
        //        }
        //    }

        //    return listaResultados;
        //}

        private void CargarGrilla()
        {

            var listaResultados = ObtenerResultadosGrilla();
            dgvResultados.DataSource = listaResultados;
            dgvResultados.DataBind();

            lblNumRegistros.Text = "Registros Consultados: " + listaResultados.Count;
            lblNumRegistros.Visible = true;
        }
        

        

       

        protected void dgvResultados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int indice = 0;
            // valorActual = int.Parse(e.Row.Cells[indice].Text);
            //int valorAnterior = int.Parse(hdnCodigoMuestra.Value);

            string codigoActual = e.Row.Cells[0].Text;
            string codigoAnterior = hdnCodigoMuestra.Value;
            int codigoActualInt = 0;
            int codigoAnteriorInt = 0;

            if (int.TryParse(codigoActual, out codigoActualInt))
            {
                if (codigoAnterior.CompareTo("0") == 0)
                {
                    hdnCodigoMuestra.Value = codigoActual;
                }
                else
                {
                    codigoAnteriorInt = int.Parse(hdnCodigoMuestra.Value);
                    codigoAnteriorInt++;
                    //codigoActualInt = int.Parse(codigoActual);

                    if (codigoActualInt != codigoAnteriorInt)
                    {
                        e.Row.Cells[0].BackColor = Color.Yellow;
                    }
                    hdnCodigoMuestra.Value = codigoActual;
                }
            }
        }

        protected void btnPublicar_Click(object sender, EventArgs e)
        {

        }
    }
}