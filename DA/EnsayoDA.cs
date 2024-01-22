using System;
using System.Collections.Generic;
using System.Linq;
using BE;

namespace DA
{
    public class EnsayoDA
    {
        readonly AccesoDB db = new AccesoDB();
        public Ensayo InsertarEnsayo(Ensayo ensayo)
        {
            db.dc.Ensayos.InsertOnSubmit(ensayo);
            try
            {
                db.dc.SubmitChanges();
                return ensayo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Ensayo ActualizarEnsayo(Ensayo ensayo)
        {
            try
            {
                db.dc.SubmitChanges();
                return ensayo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Ensayo ObtenerEnsayo(int idEnsayo)
        {
            return (from e in db.dc.GetTable<Ensayo>() where e.idEnsayo == idEnsayo select e).First();
        }

        public Ensayo ObtenerEnsayoId(int idEnsayo)
        {
            return db.dc.Ensayos.Where(e => e.idEnsayo == idEnsayo).First();
        }

        #region Publicacion

        public List<Ensayo> ObtenerEnsayos(bool estaPublicado, bool usarFechaInicial, bool usarFechaFinal, int idPrueba, DateTime fechaResultadoInicial, DateTime fechaResultadoFinal, string equipo)
        {
            var listaEnsayos = new List<Ensayo>();

            if (idPrueba > 0)
                listaEnsayos = db.dc.Ensayos.Where(e => e.idPrueba == idPrueba && e.Publicado == estaPublicado).OrderBy(e => e.FechaFinish).ToList();
            else
                listaEnsayos = db.dc.Ensayos.Where(e => e.Publicado == estaPublicado).OrderBy(e => e.FechaFinish).ToList();

            if (equipo.CompareTo("0") != 0)
                listaEnsayos = listaEnsayos.Where(e => e.Instrument == equipo).OrderBy(e => e.FechaFinish).ToList();

            if (usarFechaInicial)
                listaEnsayos = listaEnsayos.Where(e => e.FechaFinish > fechaResultadoInicial).OrderBy(e => e.FechaFinish).ToList();

            if (usarFechaFinal)
                listaEnsayos = listaEnsayos.Where(e => e.FechaFinish <= fechaResultadoFinal).OrderBy(e => e.FechaFinish).ToList();

            return listaEnsayos;
        }

        #endregion
        #region BuscarResultados

        public Ensayo ObtenerEnsayoRunId(int assayRunId) 
        {
            return db.dc.Ensayos.Where(e => e.AssayRunID == assayRunId).FirstOrDefault();
        }

        #endregion
    }
}
