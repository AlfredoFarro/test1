using System.Collections.Generic;
using System.Linq;
using BE;

namespace DA
{
    public class PruebaDA
    {
        readonly AccesoDB db = new AccesoDB();

        public List<Prueba> ObtenerPruebas(int estado)
        {
            var pruebas = (from p in db.dc.GetTable<Prueba>() where p.Estado == 1 select p).ToList();
            return pruebas;
        }
    }
}
