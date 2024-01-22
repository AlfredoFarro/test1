using System;
using System.Collections.Generic;
using BE;
using DA;

namespace BC
{
    public class EnsayoBC
    {
        readonly EnsayoDA da = new EnsayoDA();

        public Ensayo InsertarEnsayo(Ensayo ensayo)
        {
            return da.InsertarEnsayo(ensayo);
        }
        public Ensayo ActualizarEnsayo(Ensayo ensayo)
        {
            return da.ActualizarEnsayo(ensayo);
        }

        public void PublicarEnsayo(int idEnsayo,string usuario)
        {
            var ensayo = ObtenerEnsayoId(idEnsayo);
            ensayo.Publicado = true;
            ensayo.FechaPublicacion = DateTime.Now;
            ensayo.PublicadoPor = usuario;
            ActualizarEnsayo(ensayo);
            //da.PublicarEnsayo(idEnsayo,usuario)
        }

        public Ensayo ObtenerEnsayoId(int idEnsayo)
        {
            return da.ObtenerEnsayoId(idEnsayo);
        }

        #region Publicacion
        
        public List<Ensayo> ObtenerEnsayos(string prueba,string inicio,string fin,string publicado, string equipo)
        //(bool estaPublicado, bool usarFechaInicial, bool usarFechaFinal, int idPrueba, DateTime fechaResultadoInicial, DateTime fechaResultadoFinal)
        {
            DateTime fechaInicio;
            DateTime fechaFin;
            bool usarInicio = DateTime.TryParse(inicio, out fechaInicio);
            bool usarFin = DateTime.TryParse(fin, out fechaFin);
            int idPrueba = int.Parse(prueba);
            bool estaPublicado = (publicado.CompareTo("1") == 0);

            return da.ObtenerEnsayos(estaPublicado, usarInicio, usarFin, idPrueba, fechaInicio, fechaFin, equipo);
        }

        #endregion
        #region BuscarResultados

        public Ensayo ObtenerEnsayoRunId(int assayRunId)
        {
            return da.ObtenerEnsayoRunId(assayRunId);
        }

        #endregion
    }
}
