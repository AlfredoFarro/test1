using System.Collections.Generic;
using System.Data;
using BE;
using DA;

namespace BC
{
    public class UsuarioBC
    {
        readonly UsuarioDA da = new UsuarioDA();

        public Usuario GetUsuario(string UserId)
        {
            return da.GetUsuario(UserId);
        }

        public Usuario ObtenerUsuario(string usuario)
        {
            return da.ObtenerUsuario(usuario);
        }

        public void CreateUser(Usuario usuarioNuevo)
        {
            da.CreateUser(usuarioNuevo);
        }

        public DataTable ObtenerRoles()
        {
            return da.ObtenerRoles();
        }
        public List<Vista_Usuario> ObtenerUsuarios(string usuario, int idEstablecimiento)
        {
            //int id = int.Parse(idEstablecimiento);
            return da.ObtenerUsuarios(usuario, idEstablecimiento);
        }
        public DataTable ObtenerUsuarios(string usuario, int idEstablecimiento,bool estaBloqueado)
        {
            //int id = int.Parse(idEstablecimiento);
            return da.ObtenerUsuariosDT(usuario, idEstablecimiento, estaBloqueado);
        }
        public List<Usuario> ObtenerUsuariosDigitadores()
        {
            return da.ObtenerUsuariosDigitadores();
        }
       
    }
}
