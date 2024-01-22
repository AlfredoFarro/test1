using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BC;
using System.Text;

namespace TamizajePortal.Tarjetas
{
    public partial class AdministrarEnvios : Page
    {
        readonly EstablecimientoBC establecimientoBC = new EstablecimientoBC();
        readonly TipoEstablecimientoBC tipoEstablecimientoBC = new TipoEstablecimientoBC();
        readonly ParametroGeneralBC parametroGeneralBC = new ParametroGeneralBC();
        readonly UsuarioBC usuarioBC = new UsuarioBC();
        readonly EnvioBC bc = new EnvioBC();
        readonly TarjetaBC tarjetasBC = new TarjetaBC();

        int codPagina = 15;
        string pagina = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!usuarioBC.VerificarPermiso(out pagina, Session["idUsuario"], codPagina))
            {
                Response.Redirect(pagina);
            }
            else
            {
                Master.ActivarMenu();
            }
            //ListItem item = new ListItem("--Seleccionar--", "0");
            if (!Page.IsPostBack)
            {
                Master.CambiarTitulo("Administrar Envios");
                CargarTipoEstablecimiento();
                CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
                CargarAnho();
                CargarMes();
            }
        }

        private void CargarMes()
        {
            ddlMes.DataSource = parametroGeneralBC.ListaMeses();
            ddlMes.DataTextField = "ValorTexto";
            ddlMes.DataValueField = "ValorEntero";
            ddlMes.DataBind();
            
            
            ListItem item = new ListItem("--Todos--", "0");
            ddlMes.Items.Insert(0, item);
            ddlMes.SelectedValue = "0";
        }

        private void CargarAnho()
        {
            ddlAnho.DataSource = parametroGeneralBC.ListaAnhos();
            ddlAnho.DataTextField = "ValorTexto";
            ddlAnho.DataValueField = "ValorEntero";
            ddlAnho.DataBind();
            ListItem item = new ListItem("--Todos--", "0");
            ddlAnho.Items.Insert(0, item);
            ddlAnho.SelectedValue = "0";
        }

        private void CargarTipoEstablecimiento()
        {
            ddlTipoEstablecimiento.DataSource = tipoEstablecimientoBC.ObtenerTipoEstablecimiento();
            ddlTipoEstablecimiento.DataTextField = "Nombre";
            ddlTipoEstablecimiento.DataValueField = "idTipoEstablecimiento";
            ddlTipoEstablecimiento.DataBind();
            ListItem item = new ListItem("--Todos--", "0");
            ddlTipoEstablecimiento.Items.Insert(0, item);
            ddlTipoEstablecimiento.SelectedValue = "0";
        }
        public void CargarEstablecimiento(int tipoEstablecimiento)
        {
            ddlEstablecimiento.DataTextField = "Nombre";
            ddlEstablecimiento.DataValueField = "idEstablecimiento";
            List<Establecimiento> establecimientos = new List<Establecimiento>();
            if (tipoEstablecimiento != 0)
            {
                establecimientos = establecimientoBC.ObtenerEstablecimientos(string.Empty, tipoEstablecimiento);

            }
            ddlEstablecimiento.DataSource = establecimientos;
            ddlEstablecimiento.DataBind();
            ListItem item = new ListItem("--Todos--", "0");
            ddlEstablecimiento.Items.Insert(0, item);
            ddlEstablecimiento.SelectedValue = "0";

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //
            //int idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
            //int anho = int.Parse(ddlAnho.SelectedValue);
            //int mes = int.Parse(ddlMes.SelectedValue);

            CargarGrilla();
        }

        protected void ddlTipoEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportGridToCSV();
        }
        private void CargarGrilla()
        {
            int idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
            int idTipoEstablecimiento = int.Parse(ddlTipoEstablecimiento.SelectedValue);
            int anho = int.Parse(ddlAnho.SelectedValue);
            int mes = int.Parse(ddlMes.SelectedValue);
            
            List<Vista_ListaEnvios> lista = bc.ObtenerEnvios(idTipoEstablecimiento,idEstablecimiento,anho,mes);
            dgvResultados.DataSource = lista;
            dgvResultados.DataBind();

            lblNumRegistros.Text = "Registros Consultados: " + lista.Count();
            lblNumRegistros.Visible = true;

        }
        private void ExportGridToCSV()
        {
            CargarGrilla();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Export.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            dgvResultados.AllowPaging = false;
            dgvResultados.DataBind();

            StringBuilder columnbind = new StringBuilder();
            for (int k = 0; k < dgvResultados.Columns.Count; k++)
            {

                columnbind.Append(dgvResultados.Columns[k].HeaderText + ',');
            }

            columnbind.Append("\r\n");
            for (int i = 0; i < dgvResultados.Rows.Count; i++)
            {
                for (int k = 0; k < dgvResultados.Columns.Count; k++)
                {

                    columnbind.Append(dgvResultados.Rows[i].Cells[k].Text + ',');
                }

                columnbind.Append("\r\n");
            }
            Response.Output.Write(columnbind.ToString());
            Response.Flush();
            Response.End();

        }

        protected void dgvResultados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvResultados.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void dgvResultados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("Editar") == 0)
            {
                Response.Redirect("~/Envios/RegistrarEnvio.aspx?idEnvio=" + e.CommandArgument);
            }
            if (e.CommandName.CompareTo("Eliminar") == 0)
            {
                int idEnvio = int.Parse(e.CommandArgument.ToString());
                Envio envio = bc.ObtenerEnvio(idEnvio);
                envio.Estado = 0; 
                bc.ActualizarEnvio(envio);
                tarjetasBC.ActualizarEstadoTarjetas( idEnvio,0);
                CargarGrilla();
                //Response.Redirect("~/Envios/RegistrarEnvio.aspx?idEnvio=" + e.CommandArgument);
            }
        }

        protected void ddlAnho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(ddlAnho.SelectedValue) > 0)
            {
                ddlMes.Enabled = true;
            }
            else
            {
                ddlMes.SelectedValue = "0";
                ddlMes.Enabled = false;
            }
        }
    }
}