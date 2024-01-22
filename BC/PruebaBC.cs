using System.Collections.Generic;
using BE;
using DA;

namespace BC
{
    public class PruebaBC
    {
        readonly PruebaDA pruebaDA = new PruebaDA();
        public List<Prueba> ObtenerPruebas(int estado)
        {
            return pruebaDA.ObtenerPruebas(estado);
        }
    }
}
