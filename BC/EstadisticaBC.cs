using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DA;
using BE;

namespace BC
{
    public class EstadisticaBC
    {
        EstadisticaDA da = new EstadisticaDA();

        public List<Vista_ResultadosMuestra> ObtenerResultadosRespaldo(int idEstablecimiento, int anho, int mes)
        {
            return da.ObtenerResultadosRespaldo(idEstablecimiento, anho, mes);
        }
    }
}
