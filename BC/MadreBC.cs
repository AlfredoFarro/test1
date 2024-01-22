using System.Collections.Generic;
using BE;
using DA;

namespace BC
{
    public class MadreBC
    {
        readonly MadreDA da = new MadreDA();

        public Madre InsertarMadre(Madre madre)
        {
            return da.InsertarMadre(madre);
        }
        public Madre ActualizarMadre(Madre madre)
        {
            return da.ActualizarMadre(madre);
        }

        public Madre ObtenerMadrexIdMadre(int idMadre)
        {
            return da.ObtenerMadrexIdMadre(idMadre);
        }

        public Madre ObtenerMadrexDNI(string DNI)
        {
            return da.ObtenerMadrexDNI(DNI);
        }

        public bool ExisteMadre(string DNI)
        {
            return da.ExisteMadre(DNI);
        }

        public List<Madre> ObtenerMadresxDNI(string dni)
        {
            return da.ObtenerMadresxDNI(dni);
        }

        public bool liberarMadresDNI(string dni)
        {
            return da.liberarMadresDNI(dni);
        }
    }
}
