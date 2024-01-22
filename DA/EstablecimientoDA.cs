using System;
using System.Collections.Generic;
using System.Linq;
using BE;

namespace DA
{
    public class EstablecimientoDA
    {
        readonly AccesoDB db = new AccesoDB();
        public List<Establecimiento> ObtenerEstablecimientosxTipo(int tipoEstablecimiento) {
            return (from e in db.dc.Establecimientos where e.idTipoEstablecimiento == tipoEstablecimiento select e).ToList();
                //return q;)
        }
        public List<Establecimiento> ObtenerEstablecimientos(string nombreEstablecimiento, int tipoEstablecimiento)
        {
            var listaEstablecimientos = new List<Establecimiento>();
            //TipoEstablecimiento tipo =
            //    db.dc.TipoEstablecimiento.Where(e => e.idTipoEstablecimiento == tipoEstablecimiento).FirstOrDefault();
            if (nombreEstablecimiento == string.Empty)
            {
                if (tipoEstablecimiento == 0)
                {
                    listaEstablecimientos = (from e in db.dc.Establecimientos select e).ToList();
                }
                else
                {
                    listaEstablecimientos = (from e in db.dc.Establecimientos where e.idTipoEstablecimiento == tipoEstablecimiento select e).ToList();
                    //listaEstablecimientos = (from e in db.dc.Establecimiento where e.idTipoEstablecimiento == tipo.idTipoEstablecimiento select e).ToList();
                }
            }else
            {
                listaEstablecimientos = (from e in db.dc.Establecimientos where e.Nombre.Contains(nombreEstablecimiento) select e).ToList();
            }
            //listaEstablecimientos = (from e in db.linqDataContext.Establecimientos where e.TipoEstablecimiento == tipoEstablecimiento select e).ToList();
            //return q;)
            return listaEstablecimientos;
        }

        public bool RegistrarEstablecimiento(Establecimiento establecimiento)
        {
            db.dc.Establecimientos.InsertOnSubmit(establecimiento);
            try
            {
                db.dc.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
        }

        public int ActualizarEstablecimiento(Establecimiento establecimiento)
        {
            //db.linqDataContext.Muestras. .InsertAllOnSubmit(muestras);
            try
            {
                db.dc.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Establecimiento ObtenerEstablecimientoxIdEstablecimiento(int idEstablecimiento)
        {
            return (from e in db.dc.Establecimientos where e.idEstablecimiento == idEstablecimiento select e).FirstOrDefault();
        }


        #region Metodos_Vista_Establecimientos    

        public Vista_Establecimiento ObtenerVistaEstablecimiento(int idEstablecimiento)
        {
            return db.dc.Vista_Establecimientos.First(e => e.idEstablecimiento == idEstablecimiento);

        }

        public Establecimiento ObtenerEstablecimiento(int idEstablecimiento)
        {
            return db.dc.Establecimientos.First(e => e.idEstablecimiento == idEstablecimiento);

        }

        
        public List<Establecimiento> ObtenerEstablecimientos(string establecimiento, int idTipoEstablecimiento, int idDepartamento, int idProvincia)
        {
            List<Establecimiento> lista = db.dc.Establecimientos.ToList();

            if (idTipoEstablecimiento > 0)
                lista = lista.Where(e => e.idTipoEstablecimiento == idTipoEstablecimiento).ToList();
            if (idDepartamento > 0)
                lista = lista.Where(e => e.Departamento == idDepartamento).ToList();
            if (idProvincia > 0)
                lista = lista.Where(e => e.Provincia == idProvincia).ToList();
            if (establecimiento.CompareTo(string.Empty) != 0)
                lista = lista.Where(e => e.Nombre.Contains(establecimiento)).ToList();

            return lista;
        }
        #endregion
        
    }
}
