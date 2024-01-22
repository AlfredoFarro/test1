using System.Collections.Generic;
using System.Linq;
using BE;

namespace DA
{
    public class TipoEstablecimientoDA
    {
        readonly AccesoDB _db = new AccesoDB();

        public List<TipoEstablecimiento> ObtenerTipoEstablecimiento()
        {
            //return (from t in _db.dc.TipoEstablecimientos where t.Estado == 1 select t).OrderBy(e => e.Nombre).ToList();
            return _db.dc.TipoEstablecimientos.Where(e => e.Estado == 1).OrderBy(e => e.Nombre).ToList();
        }

        public TipoEstablecimiento ObtenerTipoEstablecimientoxId(int idTipoEstablecimiento)
        {
            return _db.dc.TipoEstablecimientos.Where(t => t.idTipoEstablecimiento == idTipoEstablecimiento).FirstOrDefault();
        }
    }
}
