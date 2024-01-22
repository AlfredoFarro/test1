using System.Collections.Generic;
using BE;
using DA;

namespace BC
{
    public class EstablecimientoBC
    {
        readonly EstablecimientoDA da = new EstablecimientoDA();

        public List<Establecimiento> ObtenerEstablecimientosxTipo(int tipoEstablecimiento)
        {
            return da.ObtenerEstablecimientosxTipo(tipoEstablecimiento);
        }
        public List<Establecimiento> ObtenerEstablecimientos(string nombreEstablecimiento, int tipoEstablecimiento)
        {
            return da.ObtenerEstablecimientos(nombreEstablecimiento, tipoEstablecimiento);
        }

        public bool RegistrarEstablecimiento(Establecimiento establecimiento)
        {
            return da.RegistrarEstablecimiento(establecimiento);
        }

        public int ActualizarEstablecimiento(Establecimiento establecimiento)
        {
            return da.ActualizarEstablecimiento(establecimiento);
        }

        public Establecimiento ObtenerEstablecimientoxIdEstablecimiento(int idEstablecimiento)
        {
            return da.ObtenerEstablecimientoxIdEstablecimiento(idEstablecimiento);
        }

        public List<Establecimiento> ObtenerEstablecimientos(string establecimiento, int idTipoEstablecimiento, int idDepartamento, int idProvincia)
        {
            return da.ObtenerEstablecimientos(establecimiento, idTipoEstablecimiento, idDepartamento, idProvincia);
        }

        public Vista_Establecimiento ObtenerVistaEstablecimiento(int idEstablecimiento)
        {
            return da.ObtenerVistaEstablecimiento(idEstablecimiento);
        }
        #region Envio

        public int ObtenerUltimoCodigoEnvio(int idEstablecimiento)
        {
            var establecimiento = da.ObtenerEstablecimiento(idEstablecimiento);

            int ultimoCodigoEnvio;
            if (int.TryParse(establecimiento.UltimoCodigoEnvio.ToString(), out ultimoCodigoEnvio))
            {
                ultimoCodigoEnvio = ultimoCodigoEnvio + 1;
                return ultimoCodigoEnvio;
            }
            return 1;
        }

        #endregion

    }
}
