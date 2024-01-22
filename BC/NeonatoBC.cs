using System.Collections.Generic;
using System.Data;
using BE;
using DA;

namespace BC
{
    public class NeonatoBC
    {
        readonly NeonatoDA da = new NeonatoDA();

        public Neonato InsertarNeonato(Neonato neonato)
        {
           return da.InsertarNeonato(neonato);
        }
        public Neonato ActualizarNeonato(Neonato neonato)
        {
           return da.ActualizarNeonato(neonato);
        }
        public Neonato ObtenerNeonatoxIdNeonato(int idNeonato)
        {
            return da.ObtenerNeonatoxIdNeonato(idNeonato);
        }
        public List<Neonato> ObtenerNeonatosxIdMadre(int idMadre)
        {
            return da.ObtenerNeonatosxIdMadre(idMadre);
        }

        public DataTable ObtenerDataTableNeonatosxidMadre(int idMadre)
        {
            return da.ObtenerDataTableNeonatosxidMadre(idMadre);
        }
        
    }
}
