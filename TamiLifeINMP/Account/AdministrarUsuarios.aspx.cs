using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BC;
using BE;


namespace TamiLifeSA.Account
{
    public partial class AdministrarUsuarios : Page
    {
        readonly UsuarioBC _usuarioBc = new UsuarioBC();
        readonly EstablecimientoBC _establecimientoBc = new EstablecimientoBC();
        readonly TipoEstablecimientoBC _tipoEstablecimientoBc = new TipoEstablecimientoBC();

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
                    }
                }
                CargarTipoEstablecimiento();
                //string tipo = ddlTipoEstablecimiento.SelectedValue;
                int tipoAux = int.Parse(ddlTipoEstablecimiento.SelectedValue);
                CargarEstablecimiento(tipoAux);
            }
        }

        protected void dgvResultados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvResultados.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void dgvMuestras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("editar") == 0)
            {
                //string dni = e.CommandArgument.ToString();
                string usuarioAux = e.CommandArgument.ToString();
                Usuario usuario = _usuarioBc.ObtenerUsuario(usuarioAux);
                lblUsuario.Text = usuario.NombreUsuario;
               

                mpeAttendanceReport.Show();
            }
            if (e.CommandName.CompareTo("desbloquear") == 0)
            {
                //string dni = e.CommandArgument.ToString();
                string usuarioAux = e.CommandArgument.ToString();
                Usuario usuario = _usuarioBc.ObtenerUsuario(usuarioAux);
                lblUsuario.Text = usuario.NombreUsuario;
                MembershipUser mu = Membership.GetUser(usuario.NombreUsuario);
                if (mu.UnlockUser())
                {
                    CargarGrilla();
                }
                
                //mpeAttendanceReport.Show();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        #region Metodos
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
        private void CargarEstablecimiento(int tipoEstablecimiento)
        {
            ddlEstablecimiento.DataTextField = "Nombre";
            ddlEstablecimiento.DataValueField = "idEstablecimiento";
            var establecimientos = new List<Establecimiento>();
            if (tipoEstablecimiento != 0)
            {
                establecimientos = _establecimientoBc.ObtenerEstablecimientos(string.Empty, tipoEstablecimiento);

            }
            ddlEstablecimiento.DataSource = establecimientos;
            ddlEstablecimiento.DataBind();

            if (tipoEstablecimiento == 0)
            {
                var item = new ListItem("--Seleccionar--", "0");
                ddlEstablecimiento.Items.Insert(0, item);
                ddlEstablecimiento.SelectedValue = "0";
            }
        }
        private void CargarGrilla()
        {
            string usuario = txtUser.Text;
            int idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
            bool estado = bool.Parse(ddlEstado.SelectedValue);
            //List<Vista_Usuario> listaUsuarios = usuarioBC.ObtenerUsuarios(usuario, idEstablecimiento);
            DataTable listaUsuarios = _usuarioBc.ObtenerUsuarios(usuario, idEstablecimiento, estado);
            dgvResultados.DataSource = listaUsuarios;
            dgvResultados.DataBind();
            lblNumRegistros.Text = "Registros Consultados: " + listaUsuarios.Rows.Count;
            lblNumRegistros.Visible = true;
        }
        #endregion

        protected void ddlTipoEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
        }

        protected void dgvResultados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //var row = e.Row.FindControl("Estado");
            //e.Row.DataItem = ""

            //var data = e.Row.DataItem as DataRowView;
            //if (data != null)
            //{
            //    var lbtDownload = e.Row.FindControl("Estado");
            //    if (int.Parse(e.Row.DataItem.ToString()) == 1)
            //    {
            //        string edad = "23";
            //    }
            //}
        }

        protected void btnGuardarPopup_Click(object sender, EventArgs e)
        {
            string username = lblUsuario.Text;
            string password = txtPassword.Text;
            MembershipUser mu = Membership.GetUser(username);
            if (mu != null)
            {
                mu.ChangePassword(mu.ResetPassword(), password);
            }
            
        }
    }
}