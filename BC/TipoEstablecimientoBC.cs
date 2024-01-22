using System.Collections.Generic;
using BE;
using DA;

namespace BC
{
    public class TipoEstablecimientoBC
    {
        readonly TipoEstablecimientoDA da = new TipoEstablecimientoDA();

        public List<TipoEstablecimiento> ObtenerTipoEstablecimiento()
        {
            return da.ObtenerTipoEstablecimiento();
        }

        public TipoEstablecimiento ObtenerTipoEstablecimientoxId(int idTipoEstablecimiento)
        {
            return da.ObtenerTipoEstablecimientoxId(idTipoEstablecimiento);
        }
    }
}
