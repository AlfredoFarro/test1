using System.Collections.Generic;
using System.Linq;
using BE;

namespace DA
{
    public class ParametroGeneralDA
    {
        readonly AccesoDB db = new AccesoDB();

        public List<ParametroGeneral> ListaNumeroMuestras()
        {
            return (from p in db.dc.ParametroGenerals where p.Grupo == 2 && p.Estado == 1 select p).ToList();
        }
        public List<ParametroGeneral> ListaAnhos()
        {
            return (from p in db.dc.ParametroGenerals where p.Grupo == 1 && p.SubGrupo == 1 && p.Estado == 1 select p).ToList();
        }

        public List<ParametroGeneral> ListaMeses()
        {
            return (from p in db.dc.ParametroGenerals where p.Grupo == 1 && p.SubGrupo == 2 && p.Estado == 1 select p).ToList();
        }

        public List<ParametroGeneral> ListaPruebas()
        {
            return (from p in db.dc.ParametroGenerals where p.Grupo == 3 && p.SubGrupo == 1 && p.Estado == 1 select p).ToList();
        }

        public List<ParametroGeneral> ListarTipoResultados()
        {
            return (from p in db.dc.ParametroGenerals where p.Grupo == 4 && p.SubGrupo == 1 && p.Estado == 1 select p).ToList();
        }

        public List<ParametroGeneral> ListaInstrumentos()
        {
            return (from p in db.dc.ParametroGenerals where p.Grupo == 5 && p.Estado == 1 select p).ToList();
        }

        public List<ParametroGeneral> ListaEstadosPublicacion()
        {
            return (from p in db.dc.ParametroGenerals where p.Grupo == 6 && p.Estado == 1 select p).ToList();
        }


    }
}
