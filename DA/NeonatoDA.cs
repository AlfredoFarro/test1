using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BE;

namespace DA
{
    public class NeonatoDA
    {
        readonly AccesoDB db = new AccesoDB();

        public Neonato InsertarNeonato(Neonato neonato)
        {
            db.dc.Neonatos.InsertOnSubmit(neonato);
            try
            {
                db.dc.SubmitChanges();
                return neonato;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Neonato ActualizarNeonato(Neonato neonato)
        {
            try
            {
                db.dc.SubmitChanges();
                return neonato;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public Muestra ObtenerMuestra(string codigoMuestra)
        //{
        //    if (codigoMuestra.CompareTo(string.Empty) != 0)
        //    {
        //        return (from m in db.dc.Muestras where m.CodigoMuestra == codigoMuestra select m).FirstOrDefault();
        //    }
        //    var muestra = new Muestra {Estado = 0};
        //    return muestra;
        //}
        public Neonato ObtenerNeonatoxIdNeonato(int idNeonato)
        {
            if (idNeonato != 0)
            {
                return (from m in db.dc.Neonatos where m.idNeonato == idNeonato select m).FirstOrDefault();
            }
            var neonato = new Neonato {Estado = 0};
            return neonato;
        }
        public List<Neonato> ObtenerNeonatosxIdMadre(int idMadre)
        {
            return (from n in db.dc.Neonatos where n.idMadre == idMadre && n.Estado == 1 select n).ToList();
        }

        public DataTable ObtenerDataTableNeonatosxidMadre(int idMadre)
        {
            var listaNeonatos = ObtenerNeonatosxIdMadre(idMadre);
            var dt = new DataTable();
            if (listaNeonatos.Count > 0)
            {
                
                dt.Columns.Add("idNeonato");
                dt.Columns.Add("Neonato");

                foreach (var neonato in listaNeonatos)
                {
                    DataRow dr = dt.NewRow();
                    dr["idNeonato"] = neonato.idNeonato;
                    DateTime fechaNac;
                    if (DateTime.TryParse(neonato.FechaNacimiento.ToString(), out fechaNac))
                    {
                        dr["Neonato"] = string.Concat(neonato.Apellidos, " - ", neonato.Nombres," - ",fechaNac.ToString());
                    }else
                    {
                        dr["Neonato"] = neonato.Nombres;
                    }
                   
                    dt.Rows.Add(dr);

                }
                
            }
            return dt;
        }

    }
}
