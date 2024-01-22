using System.Collections.Generic;
using BE;
using DA;

namespace BC
{
    public class ParametroGeneralBC
    {
        readonly ParametroGeneralDA da = new ParametroGeneralDA();
        public List<ParametroGeneral> ListaAnhos()
        {
            return da.ListaAnhos();
        }

        public List<ParametroGeneral> ListaMeses()
        {
            return da.ListaMeses();
        }

        public List<ParametroGeneral> ListaPruebas()
        {
            return da.ListaPruebas();
        }

        public List<ParametroGeneral> ListarTipoResultados()
        {
            return da.ListarTipoResultados();
        }

        public List<ParametroGeneral> ListaInstrumentos()
        {
            return da.ListaInstrumentos();
        }

        public List<ParametroGeneral> ListaNumeroMuestras()
        {
            return da.ListaNumeroMuestras();
        }

        public List<ParametroGeneral> ListaEstadosPublicacion()
        {
            return da.ListaEstadosPublicacion();
        }
    }
}
