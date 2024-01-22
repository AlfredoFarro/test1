using System;
using System.Collections.Generic;
using System.Linq;
using BE;

namespace DA
{
    public class MadreDA
    {
        readonly AccesoDB db = new AccesoDB();

        public Madre InsertarMadre(Madre madre)
        {
            db.dc.Madres.InsertOnSubmit(madre);
            try
            {
                db.dc.SubmitChanges();
                return madre;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Madre ActualizarMadre(Madre madre)
        {
            //db.linqDataContext.Muestras. .InsertAllOnSubmit(muestras);
            try
            {
                db.dc.SubmitChanges();
                return madre;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Madre ObtenerMadrexIdMadre(int idMadre)
        {
            //Muestra q = (from m in db.linqDataContext.Muestras where m.CodigoMuestra == codigoMuestra select m).First(); 

            if (idMadre != 0)
            {
                return (from m in db.dc.Madres where m.idMadre == idMadre select m).FirstOrDefault();
                //return q;
            }
            else
            {
                var madre = new Madre {Estado = 0};
                return madre;
            }
        }
        public Madre ObtenerMadrexDNI(string DNI)
        {
            return (from m in db.dc.Madres where m.DNI == DNI && m.Estado == 1 select m).FirstOrDefault();
            //return madreDA.ObtenerMadrexDNI(DNI);
        }

        public bool ExisteMadre(string DNI)
        {
            return db.dc.Madres.Any(m => m.DNI == DNI && m.Estado == 1);
        }

        public List<Madre> ObtenerMadresxDNI(string dni)
        {
            return db.dc.Madres.Where(m => m.DNI == dni && m.Estado == 1).ToList();
            //return madreDA.ObtenerMadrexDNI(DNI);
        }

        public bool liberarMadresDNI(string dni)
        {
            try
            {
                db.ExecuteNoQuery(string.Concat("UPDATE Madre SET Estado = 0 Where DNI = '", dni, "'"));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
