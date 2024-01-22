using System.Collections.Generic;
using System.Linq;
using BE;

namespace DA
{
    public class UbigeoDA
    {
        readonly AccesoDB db = new AccesoDB();

        public List<Ubigeo> ObtenerDepartamentos()
        {
            return (from d in db.dc.Ubigeos where d.CodDepartamento > 0 && d.CodProvincia == 0 select d).ToList();
        }
        public List<Ubigeo> ObtenerProvincias(int idDepartamento)
        {
            Ubigeo departamento = db.dc.Ubigeos.Where(u => u.idUbigeo == idDepartamento).First();
            return (from d in db.dc.Ubigeos where d.CodDepartamento == departamento.CodDepartamento && d.CodProvincia > 0 && d.CodDistrito == 0 select d).ToList();
        }
        public List<Ubigeo> ObtenerDistritos(int idProvincia)
        {
            Ubigeo provincia = db.dc.Ubigeos.Where(u => u.idUbigeo == idProvincia).First();
            return (from d in db.dc.Ubigeos where d.CodDepartamento == provincia.CodDepartamento && d.CodProvincia == provincia.CodProvincia && d.CodDistrito > 0 select d).ToList();
        }
        public Ubigeo ObtenerUbigeo(int idUbigeo)
        {
            return (from d in db.dc.Ubigeos where d.idUbigeo == idUbigeo select d).FirstOrDefault();
        }
        //public List<Ubigeo> ObtenerDistritos(int idubigeo)
        //{
        //    Ubigeo ubigeoAux = (from u in db.dc.Ubigeo where u.idUbigeo == idubigeo select u).First();

        //    return (from d in db.dc.Ubigeo where d.codDepartamento == ubigeoAux.codDepartamento && d.codProvincia == ubigeoAux.codProvincia && d.codDistrito > 0 select d).ToList();
        //}
    }
}
