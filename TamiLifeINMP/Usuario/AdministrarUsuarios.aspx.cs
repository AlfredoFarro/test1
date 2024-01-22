using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BC;

namespace TamizajePortal.Usuario
{
    public partial class AdministrarUsuarios :Page
    {
        int codPagina = 1;
        string pagina = string.Empty;

        readonly UsuarioBC usuarioBC = new UsuarioBC();
        readonly PerfilBC perfilBC = new PerfilBC();
        readonly EstablecimientoBC establecimientoBC = new EstablecimientoBC();
        readonly TipoEstablecimientoBC tipoEstablecimientoBC = new TipoEstablecimientoBC();
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
            if (!Page.IsPostBack)
            {
                Master.CambiarTitulo("ADMINISTRAR USUARIOS");
                CargarPerfil();
                CargarTipoEstablecimiento();
                CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            int idTipoEstablecimiento = int.Parse(ddlTipoEstablecimiento.SelectedValue.ToString());
            int idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue.ToString());
            string usuario = txtUsuario.Text;
            int idPerfil = int.Parse(ddlPerfil.SelectedValue.ToString());
            List<Vista_Usuarios> usuarios = usuarioBC.ObtenerUsuarios(usuario, idPerfil, idTipoEstablecimiento, idEstablecimiento);
            dgvResultados.DataSource = usuarios;
            dgvResultados.DataBind();
            lblNumRegistros.Text = "Registros Consultados: " + usuarios.Count();
            lblNumRegistros.Visible = true;
        }

        protected void dgvResultados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("Editar") == 0)
            {
                Response.Redirect("~/Usuario/RegistrarUsuario.aspx?idUsuario=" + e.CommandArgument);
            }
            if (e.CommandName.CompareTo("Agregar") == 0)
            {
                Response.Redirect("~/Usuario/RegistrarUsuario.aspx?idUsuario=" + e.CommandArgument);
            }
        }

        public void CargarPerfil()
        {
            List<Perfil> perfiles = perfilBC.ObtenerListaPerfiles();
            ddlPerfil.DataSource = perfiles;
            ddlPerfil.DataTextField = "Nombre";
            ddlPerfil.DataValueField = "idPerfil";
            ddlPerfil.DataBind();

            ListItem item = new ListItem("--Seleccionar--", "0");
            ddlPerfil.Items.Insert(0, item);
            ddlPerfil.SelectedValue = "0";
        }
        private void CargarTipoEstablecimiento()
        {
            ddlTipoEstablecimiento.DataSource = tipoEstablecimientoBC.ObtenerTipoEstablecimiento();
            ddlTipoEstablecimiento.DataTextField = "Nombre";
            ddlTipoEstablecimiento.DataValueField = "idTipoEstablecimiento";
            ddlTipoEstablecimiento.DataBind();

            ListItem item = new ListItem("--Seleccionar--", "0");
            ddlTipoEstablecimiento.Items.Insert(0, item);
            ddlTipoEstablecimiento.SelectedValue = "0";
        }
        public void CargarEstablecimiento(int tipoEstablecimiento)
        {
            //List<Establecimiento> establecimietos = establecimientoDA.obtenerEstablecimientosxTipo(tipoEstablecimiento);
            List<Establecimiento> establecimientos = establecimientoBC.ObtenerEstablecimientos(string.Empty, tipoEstablecimiento);
            ddlEstablecimiento.DataSource = establecimientos;
            ddlEstablecimiento.DataTextField = "Nombre";
            ddlEstablecimiento.DataValueField = "idEstablecimiento";
            ddlEstablecimiento.DataBind();

            ListItem item = new ListItem("--Seleccionar--", "0");
            ddlEstablecimiento.Items.Insert(0, item);
            ddlEstablecimiento.SelectedValue = "0";
        }

        protected void ddlTipoEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idTipoEstablecimiento = int.Parse(ddlTipoEstablecimiento.SelectedValue);

            if (idTipoEstablecimiento != 0)
            {
                CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
                ddlEstablecimiento.Enabled = true;
            }
            else
            {
                ddlEstablecimiento.SelectedValue = "0";
                ddlEstablecimiento.Enabled = false;
            }
            
        }

        protected void dgvResultados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvResultados.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }
    }
}