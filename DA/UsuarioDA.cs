using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BE;

namespace DA
{
    public class UsuarioDA
    {
        readonly AccesoDB db = new AccesoDB();
        public Usuario GetUsuario(string UserId)
        {
            var id = Guid.Parse(UserId);
            return (from u in db.dc.Usuarios where u.UserId == id select u).First();
        }

        public Usuario ObtenerUsuario(string usuario)
        {
            return (from u in db.dc.Usuarios where u.NombreUsuario == usuario select u).First();
        }

        public void CreateUser(Usuario usuarioNuevo)
        {
            db.dc.Usuarios.InsertOnSubmit(usuarioNuevo);
            db.dc.SubmitChanges();
        }

        public DataTable ObtenerRoles()
        {
            return db.Query_DataTable_Seguridad("select roleName from [dbo].[aspnet_Roles]");
        }

        public List<Vista_Usuario> ObtenerUsuarios(string usuario, int idEstablecimiento)
        {
            if (usuario.CompareTo(string.Empty) != 0)
            {
                return db.dc.Vista_Usuarios.Where(u => u.NombreUsuario == usuario ).ToList();
            }
            if (idEstablecimiento > 0)
            {
                return db.dc.Vista_Usuarios.Where(u => u.IdEstablecimiento == idEstablecimiento).ToList();
            }
            return db.dc.Vista_Usuarios.ToList();
        }

        public DataTable ObtenerUsuariosDT(string usuario, int idEstablecimiento, bool estaBloqueado)
        {
            string query = 
                string.Concat(
                    "SELECT m.IsLockedOut,m.FailedPasswordAttemptCount,* FROM  [aspnetdb].[dbo].[vw_aspnet_MembershipUsers] m INNER JOIN [dbo].[Vista_Usuario] u ON m.UserName = u.NombreUsuario WHERE u.Estado = 1");
            if (usuario.CompareTo(string.Empty) != 0)
            {
                query = string.Concat(query, " AND u.NombreUsuario LIKE '%", usuario,"%'");
            }
            if (idEstablecimiento > 0)
            {
                query = string.Concat(query, " AND u.IdEstablecimiento = ", idEstablecimiento);
            }
            if (estaBloqueado)
            {
                query = string.Concat(query, " AND m.IsLockedOut = ", 1);
            }
            return db.QueryDataTable(query);
        }

        public List<Usuario> ObtenerUsuariosDigitadores()
        {
            return db.dc.Usuarios.Where(u => u.Role == 4).ToList();
        }
    }
}
