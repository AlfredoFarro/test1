using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using BE;
using BC;
using System.Text;

namespace TamiLifeSA.Establecimientos
{
    public partial class AdministrarEstablecimientos : System.Web.UI.Page
    {
        //Instancias de Negocio --------------------------------------------------
        readonly UbigeoBC _ubigeoBc = new UbigeoBC();
        readonly EstablecimientoBC _establecimientoBc = new EstablecimientoBC();
        readonly TipoEstablecimientoBC _tipoEstablecimientoBc = new TipoEstablecimientoBC();

        //Eventos ----------------------------------------------------------------
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
                //Master.CambiarTitulo("ADMINISTRAR ESTABLECIMIENTOS");


                CargarTipoEstablecimiento();
                CargarDepartamentos();
                CargarProvincias(0);
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }
        protected void dgvEstablecimientos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("editar") == 0)
            {
                Response.Redirect("~/Establecimientos/RegistrarEstablecimiento.aspx?idEstablecimiento=" + e.CommandArgument);
            }
            
            if (e.CommandName.CompareTo("reporte") == 0)
            {

            }
        
        }
        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlDepartamento.SelectedValue.CompareTo("0") != 0)
                CargarProvincias(int.Parse(ddlDepartamento.SelectedValue));
        }
        protected void dgvResultados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvResultados.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportGridToCSV();
        }

        //Metodos ----------------------------------------------------------------
        private void CargarDepartamentos()
        {
            ddlDepartamento.DataSource = _ubigeoBc.ObtenerDepartamentos();
            ddlDepartamento.DataTextField = "Nombre";
            ddlDepartamento.DataValueField = "idUbigeo";
            ddlDepartamento.DataBind();
            var item = new ListItem("--Seleccionar--", "0");
            ddlDepartamento.Items.Insert(0, item);
            ddlDepartamento.SelectedValue = "0";
        }
        private void CargarProvincias(int idDepartamento)
        {
            ddlProvincia.DataTextField = "Nombre";
            ddlProvincia.DataValueField = "idUbigeo";
            var item = new ListItem("--Seleccionar--", "0");

            if (idDepartamento != 0)
            {
                ddlProvincia.DataSource = _ubigeoBc.ObtenerProvincias(idDepartamento);
                ddlProvincia.DataBind();
            }
            else
            {
                ddlProvincia.Items.Clear();
            }
            ddlProvincia.Items.Insert(0, item);
            ddlProvincia.SelectedValue = "0";
        }
        private void CargarTipoEstablecimiento()
        {
            ddlTipoEstablecimiento.DataSource = _tipoEstablecimientoBc.ObtenerTipoEstablecimiento();
            ddlTipoEstablecimiento.DataTextField = "Nombre";
            ddlTipoEstablecimiento.DataValueField = "idTipoEstablecimiento";
            ddlTipoEstablecimiento.DataBind();
            var item = new ListItem("--Seleccionar--", "0");
            ddlTipoEstablecimiento.Items.Insert(0, item);
            ddlTipoEstablecimiento.SelectedValue = "0";
        }
        private void CargarGrilla()
        {
            int codTipoEstablecimiento = int.Parse(ddlTipoEstablecimiento.SelectedValue);
            int codDepartament = int.Parse(ddlDepartamento.SelectedValue);
            int codProvincia = int.Parse(ddlProvincia.SelectedValue);
            List<Establecimiento> establecimientos = _establecimientoBc.ObtenerEstablecimientos(txtEstablecimiento.Text, codTipoEstablecimiento, codDepartament, codProvincia);
            dgvResultados.DataSource = establecimientos;
            dgvResultados.DataBind();

            if (establecimientos.Any())
            {
                lblNumRegistros.Text = "Registros Consultados: " + establecimientos.Count(); ;
                lblNumRegistros.Visible = true;
            }
            else
                lblNumRegistros.Visible = false;
        }
        private void ExportGridToCSV()
        {
            CargarGrilla();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Establecimientos.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            dgvResultados.AllowPaging = false;
            dgvResultados.DataBind();

            var columnbind = new StringBuilder();
            for (int k = 1; k < dgvResultados.Columns.Count - 2; k++)
            {
                columnbind.Append(dgvResultados.Columns[k].HeaderText + ',');
            }

            columnbind.Append("\r\n");
            for (int i = 0; i < dgvResultados.Rows.Count; i++)
            {
                for (int k = 1; k < dgvResultados.Columns.Count - 2; k++)
                {

                    columnbind.Append(dgvResultados.Rows[i].Cells[k].Text + ',');
                }

                columnbind.Append("\r\n");
            }
            Response.Output.Write(columnbind.ToString());
            Response.Flush();
            Response.End();

        }
    }
}