using System.Collections.Generic;
using BE;
using DA;

namespace BC
{
    public class UbigeoBC
    {
        readonly UbigeoDA ubigeoDA = new UbigeoDA();

        public List<Ubigeo> ObtenerDepartamentos()
        {
            return ubigeoDA.ObtenerDepartamentos();
        }
        public List<Ubigeo> ObtenerProvincias(int idDepartamento)
        {
            return ubigeoDA.ObtenerProvincias(idDepartamento);
        }
        public List<Ubigeo> ObtenerDistritos(int idProvincia)
        {
            return ubigeoDA.ObtenerDistritos(idProvincia);
        }
        public Ubigeo ObtenerUbigeo(int idUbigeo)
        {
            return ubigeoDA.ObtenerUbigeo(idUbigeo);
        }
    }
}
