using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BC;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace TamizajePortal.Usuario
{
    public partial class Login : Page
    {
        UsuarioBC usuarioBC =  new UsuarioBC();
        private BE.Usuario usuarioBE;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Master.CambiarTitulo("INGRESAR");
            }
        }

        protected void LoginControl_Authenticate(object sender, AuthenticateEventArgs e)
        {
            //bool authenticated = this.ValidateCredentials(LoginControl.UserName, LoginControl.Password);
            string usuario = LoginControl.UserName;
            string password = LoginControl.Password;
            if (this.EsAlphaNumerico(usuario) && usuario.Length <= 50 && password.Length <= 50)
            {
                usuarioBE = usuarioBC.ValidarUsuario(usuario, password);
                if (usuarioBE != null)
                {
                    Session["idUsuario"] = usuarioBE.idUsuario.ToString();
                    FormsAuthentication.RedirectFromLoginPage(LoginControl.UserName, LoginControl.RememberMeSet);
                }
            }
            
        }

        public bool EsAlphaNumerico(string text)
        {
            return Regex.IsMatch(text, "^[a-zA-Z0-9]+$");
        }

    }
}