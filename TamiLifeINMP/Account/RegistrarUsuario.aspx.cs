using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using BC;
using BE;

namespace TamiLifeSA.Account
{
    public partial class RegistrarUsuario : System.Web.UI.Page
    {
        readonly TipoEstablecimientoBC _tipoEstablecimientoBc = new TipoEstablecimientoBC();
        readonly EstablecimientoBC _establecimientoBc = new EstablecimientoBC();
        readonly UsuarioBC _usuarioBc = new UsuarioBC();

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bool usuarioLogeado = (HttpContext.Current.User != null) &&
                       HttpContext.Current.User.Identity.IsAuthenticated;
                if (!usuarioLogeado)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
                else
                {
                    if (HttpContext.Current.User.IsInRole("Administrador"))
                        Master.CambiarSiteMap("AdminSiteMap");
                    else
                        Response.Redirect("~/Default.aspx");

                }
                CargarTipoEstablecimiento();
                //CargarEstablecimiento();
                CargarRoles();
            }
        }

        

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Create new user.
                int idHospital = int.Parse(ddlEstablecimiento.SelectedValue);
                //MembershipCreateStatus status;
                MembershipUser newUser = Membership.CreateUser(txtUsuario.Text, txtPassword.Text, txtCorreo.Text);//,idHospital,idHospital,true, out status);

                //string role = ddlRoles.SelectedValue;
                Roles.AddUserToRole(newUser.UserName, ddlRoles.SelectedValue);
                //string id = newUser.ProviderUserKey.ToString();

                var usuario = new Usuario();
                usuario.UserId = Guid.Parse(newUser.ProviderUserKey.ToString());
                usuario.IdEstablecimiento = idHospital;
                usuario.Estado = 1;
                usuario.NombreUsuario = newUser.UserName;
                usuario.FechaCreacion = DateTime.Today;
                usuario.CreadoPor = HttpContext.Current.User.Identity.Name;
                usuario.Pass = txtPassword.Text;
                _usuarioBc.CreateUser(usuario);
                // If user created successfully, set password question and answer (if applicable) and 
                // redirect to login page. Otherwise return an error message.
                //if (Membership.RequiresQuestionAndAnswer)
                //{
                //    newUser.ChangePasswordQuestionAndAnswer(txtPassword.Text,
                //                                            "1",
                //                                            "1");
                //}
                //Response.Redirect("Login.aspx");

            }
            catch (MembershipCreateUserException ex)
            {
                Msg.Text = GetErrorMessage(ex.StatusCode);
            }
            catch (HttpException ex)
            {
                Msg.Text = ex.Message;
            }
        }

        public string GetErrorMessage(MembershipCreateStatus status)
        {
            switch (status)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    //return "Username already exists. Please enter a different user name.";
                    return "El usuario ya existe, por favor ingrese un usuario diferente";

                case MembershipCreateStatus.DuplicateEmail:
                    //return "A username for that email address already exists. Please enter a different email address.";
                    return "Un usuario con este correo electronico ya existe, ingrese uno diferente";

                case MembershipCreateStatus.InvalidPassword:
                    //return "The password provided is invalid. Please enter a valid password value.";
                    return "La contraseña ingresada es invalida, por favor ingrese una contraseña valida";

                case MembershipCreateStatus.InvalidEmail:
                    //return "The email address provided is invalid. Please check the value and try again.";
                    return "La dirección de correo es invalida, por favor ingrese un correo valido";

                case MembershipCreateStatus.InvalidAnswer:
                    //return "The password retrieval answer provided is invalid. Please check the value and try again.";
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    //return "The user name provided is invalid. Please check the value and try again.";
                    return "El nombre de usuario ingresado es invalido";

                case MembershipCreateStatus.ProviderError:
                    //return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
                    return "La autenticación retorno un error, por favor verifique sus credenciales";

                case MembershipCreateStatus.UserRejected:
                    //return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
                    return "La creación del usuario fue cancelada, por favor verifique e intente nuevamente";

                default:
                    //return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
                    return "A ocurrido un error inesperado, por favor verifique su acceso e intente nuevamente";
            }
        }

        protected void ddlTipoEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
        }
        #endregion

        #region Metodos
        public void CargarRoles()
        {
            ddlRoles.DataTextField = "roleName";
            ddlRoles.DataValueField = "roleName";
            ddlRoles.DataSource = _usuarioBc.ObtenerRoles();
            ddlRoles.DataBind();
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

        public void CargarEstablecimiento(int tipoEstablecimiento)
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

            var item = new ListItem("--Seleccionar--", "0");
            ddlEstablecimiento.Items.Insert(0, item);
            ddlEstablecimiento.SelectedValue = "0";
        }

        #endregion
    }
}