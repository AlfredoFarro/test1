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
    public partial class RegistrarUsuario : Page
    {
        int codPagina = 2;
        string pagina = string.Empty;
        readonly UsuarioBC usuarioBC = new UsuarioBC();
        readonly PerfilBC perfilBC = new PerfilBC();
        readonly EstablecimientoBC establecimientoBC = new EstablecimientoBC();
        readonly TipoEstablecimientoBC tipoEstablecimientoBC = new TipoEstablecimientoBC();
        BE.Usuario usuario;
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
                CargarTipoEstablecimiento();
                //CargarEstablecimiento(1);
                CargarPerfil();
                if (Request["idUsuario"] != null)
                {
                    hdnIdUsuario.Value = Request["idUsuario"];
                    Master.CambiarTitulo("EDITAR USUARIO");
                    usuario = usuarioBC.ObtenerUsuario(int.Parse(hdnIdUsuario.Value));

                    txtNombres.Text = usuario.Nombres;
                    txtApellidos.Text = usuario.Apellidos;
                    txtCargo.Text = usuario.Cargo;
                    //ddlEstablecimiento.SelectedValue = usuario.idEstablecimiento.ToString();
                    CargarEstablecimientoEdicion(int.Parse(usuario.idEstablecimiento.ToString()));
                    txtEmail.Text = usuario.Email;
                    txtCelular.Text = usuario.Celular;
                    ddlPerfil.SelectedValue = usuario.idPerfil.ToString();
                    txtUsuario.Text = usuario.Nombre;

                    //lblContraseña.Visible = false;
                    //lblConfirmarContraseña.Visible = false;
                    //txtContrasena.Visible = false;
                    //txtConfirmarContrasena.Visible = false;
                    //rfvContrasena.Enabled = false;
                    //rfvConfirmarContrasena.Visible = false;
                    //txtContrasena.Text = usuario.Password;
                    //txtConfirmarContrasena.Text = usuario.Password;
                }
                else
                {
                    Master.CambiarTitulo("REGISTRAR USUARIO");
                    CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
                }
            }
        }
        private void CargarEstablecimientoEdicion(int idEstablecimiento)
        {
            //int idEstablecimiento = idEstablecimiento;
            Vista_Establecimientos establecimiento = establecimientoBC.ObtenerEstablecimiento(idEstablecimiento);
            ddlTipoEstablecimiento.SelectedValue = establecimiento.idTipoEstablecimiento.ToString();
            CargarEstablecimiento(establecimiento.idTipoEstablecimiento);
            ddlEstablecimiento.SelectedValue = idEstablecimiento.ToString();
        }
        private void CargarTipoEstablecimiento()
        {
            ddlTipoEstablecimiento.DataSource = tipoEstablecimientoBC.ObtenerTipoEstablecimiento();
            ddlTipoEstablecimiento.DataTextField = "Nombre";
            ddlTipoEstablecimiento.DataValueField = "idTipoEstablecimiento";
            ddlTipoEstablecimiento.DataBind();
        }
        public void CargarEstablecimiento(int tipoEstablecimiento)
        {
            //List<Establecimiento> establecimietos = establecimientoDA.obtenerEstablecimientosxTipo(tipoEstablecimiento);
            List<Establecimiento> establecimientos = establecimientoBC.ObtenerEstablecimientos(string.Empty, tipoEstablecimiento);
            ddlEstablecimiento.DataSource = establecimientos;
            ddlEstablecimiento.DataTextField = "Nombre";
            ddlEstablecimiento.DataValueField = "idEstablecimiento";
            ddlEstablecimiento.DataBind();
        }

        public void CargarPerfil()
        {
            List<Perfil> perfiles = perfilBC.ObtenerListaPerfiles();
            ddlPerfil.DataSource = perfiles;
            ddlPerfil.DataTextField = "Nombre";
            ddlPerfil.DataValueField = "idPerfil";
            ddlPerfil.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if ((hdnIdUsuario.Value != null) && (hdnIdUsuario.Value.CompareTo(String.Empty) != 0))
            {
                usuario = usuarioBC.ObtenerUsuario(int.Parse(hdnIdUsuario.Value));
                usuario.Nombres = txtNombres.Text;
                usuario.Apellidos = txtApellidos.Text;
                usuario.Cargo = txtCargo.Text;
                usuario.idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
                usuario.idPerfil = int.Parse(ddlPerfil.SelectedValue);
                usuario.Email = txtEmail.Text;
                usuario.Celular = txtCelular.Text;
                usuario.Nombre = txtUsuario.Text;
                usuario.Password = txtContrasena.Text;
                usuarioBC.ActualizarUsuario();
                //establecimientoBC.ActualizarEstablecimiento(establecimiento);
                hdnIdUsuario.Value = null;
                //Response.Redirect(Request.UrlReferrer.ToString());
                Response.Redirect("~/Usuario/AdministrarUsuarios.aspx");
            }
            else
            {
                usuario = new BE.Usuario();
                usuario.Nombres = txtNombres.Text;
                usuario.Apellidos = txtApellidos.Text;
                usuario.Cargo = txtCargo.Text;
                usuario.idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
                usuario.idPerfil = int.Parse(ddlPerfil.SelectedValue);
                usuario.Email = txtEmail.Text;
                usuario.Celular = txtCelular.Text;
                usuario.Nombre = txtUsuario.Text;
                usuario.Password = txtContrasena.Text;

                usuarioBC.RegistrarUsuario(usuario);
                Response.Redirect("~/Usuario/AdministrarUsuarios.aspx");
            }

            
        }

        protected void ddlTipoEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Usuario/AdministrarUsuarios.aspx");
        }
    }
}