using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BC;
using BE;
using System.Web.Security;

namespace TamizajePortal.Usuario
{
    public partial class LoginAux : Page
    {
        UsuarioBC usuarioBC = new UsuarioBC();
        protected void Page_Load(object sender, EventArgs e)
        {
            //RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!Page.IsPostBack)
            {
                Master.CambiarTitulo("INGRESAR");

            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtContrasena.Text.Trim();
            BE.Usuario usuarioBE;


            if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(password))
            {

                usuarioBE = usuarioBC.ValidarUsuario(usuario, password);

                if (usuarioBE != null)
                {
                    FormsAuthentication.SetAuthCookie(usuario, false);
                    
                    Session["idUsuario"] = usuarioBE.idUsuario.ToString();
                    //FormsAuthentication.RedirectFromLoginPage(usuario, true);
                    //Response.Redirect("~/Default.aspx");
                    Response.Redirect("~/Muestra/RegistrarMuestra.aspx");

                    //{  var main = new Principal(usuario);
                    //    //main.MdiParent = this;

                    //    main.WindowState = FormWindowState.Maximized;
                    //    main.Show();
                    //    this.Hide();
                }
                else
                {
                    CustomValidator cv = new CustomValidator();
                    cv.IsValid = false;
                    cv.ErrorMessage = "Usuario o contraseña invalidos";
                    //cv.ValidationGroup = 
                    //lblMensaje.Text = " incorrectos";
                    //lblMensaje.Visible = true;
                    this.Page.Validators.Add(cv);
                }
            }
        }

    }
}
