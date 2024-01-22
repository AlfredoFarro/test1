using System;
using System.Windows.Forms;
using BE;
using BC;

namespace TamizajeApp
{
    public partial class Login : Form
    {
        UsuarioBC usuarioBC = new UsuarioBC();
        //Usuario usuarioBE = new Usuario();

        public Login()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {      
            string usuario = txtUsuario.Text;
            string contrasena = txtContrasena.Text;
            epUsuario.Clear();
            epContrasena.Clear();
            //consultar usuario
            if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasena))
            {

                var usuarioBE = usuarioBC.ValidarUsuario(usuario, contrasena);
                if (usuarioBE != null)
                {
                    Principal principal = new Principal();
                    principal.Show();
                    Hide();
                }
                else
                {
                    lblMensaje.Visible = true;
                    //epIngresar.SetError(btnIngresar,"Usuario o Contraseña Incorrectos");
                }
                
            }
            else
            {
                if (string.IsNullOrEmpty(txtUsuario.Text))
                {
                    epUsuario.SetError(txtUsuario, "Ingrese un usuario");
                }
                if (string.IsNullOrEmpty(txtContrasena.Text))
                {
                    epUsuario.SetError(txtContrasena, "Ingrese un password");
                }
            }
        }

        private void txtContrasena_TextChanged(object sender, EventArgs e)
        {

        }

        


    }
}
