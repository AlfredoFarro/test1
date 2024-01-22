using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE;

namespace DA
{
    public class EstadisticaDA
    {
        readonly AccesoDB db = new AccesoDB();

        public List<Vista_ResultadosMuestra> ObtenerResultadosRespaldo(int idEstablecimiento, int anho,int mes)
        {
            if (idEstablecimiento == 0)
            {
                if (mes == 0)
                {
                    return db.dc.Vista_ResultadosMuestras.Where(r => r.FechaResultado.Year == anho).ToList();
                }
                else
                {
                    return db.dc.Vista_ResultadosMuestras.Where(r => r.FechaResultado.Year == anho && r.FechaResultado.Month == mes).ToList();
                }
            }
            else
            {
                if (mes == 0)
                {
                    return db.dc.Vista_ResultadosMuestras.Where(r => r.idEstablecimiento == idEstablecimiento &&  r.FechaResultado.Year == anho).ToList();
                }
                else
                {
                    return db.dc.Vista_ResultadosMuestras.Where(r => r.idEstablecimiento == idEstablecimiento && r.FechaResultado.Year == anho && r.FechaResultado.Month == mes).ToList();
                }
            }


        }
    }
}
