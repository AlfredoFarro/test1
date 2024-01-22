using System;
using System.Collections.Generic;
using DA;
using BE;

namespace BC
{
    public class EnvioBC
    {
        readonly EnvioDA da = new EnvioDA();
        readonly MuestraBC muestraBC = new MuestraBC();
        readonly EstablecimientoBC establecimientoBC = new EstablecimientoBC();

        public Envio InsertarEnvio(Envio envio)
        {
            return da.InsertarEnvio(envio);
        }

        public Envio ActualizarEnvio(Envio envio)
        {

            return da.ActualizarEnvio(envio);
        }

        public List<Vista_Envio> ObtenerEnvios(int idEstablecimiento, bool usarInicio, bool usarFin, DateTime dateInicio, DateTime dateFin, int estadoEnvio)
        {
            if (estadoEnvio > 0)
            {
                if (estadoEnvio == 1) //Envios aun NO recibidos
                    return da.ObtenerEnvios(idEstablecimiento, usarInicio, usarFin, dateInicio, dateFin, false);
                else // estadoEnvio == 2 envios recibidos
                {
                    return da.ObtenerEnvios(idEstablecimiento, usarInicio, usarFin, dateInicio, dateFin, true);
                }
            }else
            {
                return da.ObtenerEnvios(idEstablecimiento, usarInicio, usarFin, dateInicio, dateFin);
            }
           
        }

        public bool RegistrarEnvio(Envio envio, List<string> listaCodigosMuestras)
        {
            try
            {
                //Envio envioAux = InsertarEnvio(envio);
                InsertarEnvio(envio);
                if (MarcarMuestrasEnviadas(envio, listaCodigosMuestras,0))
                {
                    var establecimiento = establecimientoBC.ObtenerEstablecimientoxIdEstablecimiento(int.Parse(envio.idEstablecimiento.ToString()));
                    establecimiento.UltimoCodigoEnvio = envio.CodigoEnvio;
                    try
                    {
                        establecimientoBC.ActualizarEstablecimiento(establecimiento);
                        return true;
                    }
                    catch (Exception ex)
                    {

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                //throw;
                return false;
            }
            return false;
        }

        public bool MarcarMuestrasEnviadas(Envio envio, List<string> listaCodigosMuestras, int estadoEdicion)
        {
            return da.MarcarMuestrasEnviadas(envio, listaCodigosMuestras,estadoEdicion);
        }

        /*
         * Descripción: Marcar las muestras agregadas al envio durante la edición
         */
        public void MarcarMuestrasAgregadasEnvio(int idEnvio,List<string> listaCodigosMuestras)
        {
            foreach (var codigoMuestra in listaCodigosMuestras)
            {
                var muestra = muestraBC.ObtenerMuestraxCodigoMuestra(codigoMuestra);
                muestra.idEnvio = idEnvio;
                muestra.MuestraEnviada = true;
                muestra.EstadoEdicion = 2;
                muestraBC.ActualizarMuestra(muestra);
            }
        }

       

        public bool RegistrarEnvio(Envio envio,int idEstablecimiento, List<int> listaIdMuestra)
        {
            try
            {
                Envio envioAux = InsertarEnvio(envio);
                return MarcarMuestrasEnviadas(idEstablecimiento, envioAux.idEnvio, listaIdMuestra);
            }
            catch (Exception ex)
            {
                //throw;
                return false;
            }
        }

        public bool MarcarMuestrasEnviadas(int idEstablecimiento,int idEnvio ,List<int> listaIdMuestras)
        {
             return da.MarcarMuestrasEnviadas(idEstablecimiento, idEnvio,listaIdMuestras);
        }

        public List<Vista_MuestrasxEnvio> ObtenerListaMuestrasxEnvio(int idEnvio)
        {
            return da.ObtenerListaMuestrasxEnvio(idEnvio);
        }

        public bool EliminarEnvio(int idEnvio, string usuario)
        {
            try
            {
                List<Muestra> listaMuestras = muestraBC.ObtenerListaMuestrasxEnvio(idEnvio);
                foreach (var muestra in listaMuestras)
                {
                    muestra.MuestraEnviada = false;
                    muestra.idEnvio = null;
                    muestraBC.ActualizarMuestra(muestra);
                }

                Envio envio = ObtenerEnvio(idEnvio);
                envio.Estado = 0;
                envio.ModificadoPor = usuario;
                envio.FechaModificacion = DateTime.Now;
                ActualizarEnvio(envio);
                //db.dc.SubmitChanges();
                return true;
                //return da.EliminarEnvio(idEnvio,usuario);
            }
            catch (Exception ex)
            {
                return false;    
                throw ex;
            }
        }

        public Envio ObtenerEnvio(int idEnvio)
        {
            return da.ObtenerEnvio(idEnvio);
        }

        public bool QuitarMuestraEnvio(int idMuestra)
        {
            try
            {
                var muestra = muestraBC.ObtenerMuestraxIdMuestra(idMuestra);
                if (muestra.EstadoEdicion == 2)//Agregado durante la edicion
                {
                    
                    muestra.idEnvio = null;
                    muestra.MuestraEnviada = false;
                    muestra.EstadoEdicion = 0; //Se libera la muestra por completo

                }
                if (muestra.EstadoEdicion == 0)//muestra eliminada del grupo original de muestras del envio
                {
                    muestra.MuestraEnviada = false;
                    muestra.EstadoEdicion = 1; //Estado Eliminado
                }
                
                muestraBC.ActualizarMuestra(muestra);
                return true;
            }
            catch (Exception)
            {
                //throw;
                return false;
            }
        }

        public void CancelarCambiosMuestrasEnvio(int idEnvio)
        {
            var listaMuestrasEliminadas = muestraBC.ObtenerMuestrasELiminadasEnvio(idEnvio);
            var listaMuestrasAgregadas = muestraBC.ObtenerMuestrasAgregadasEnvio(idEnvio);

            if (listaMuestrasEliminadas.Count > 0)
            {
                foreach (var muestra in listaMuestrasEliminadas)
                {
                    muestra.EstadoEdicion = 0;
                    muestra.MuestraEnviada = true;
                    muestraBC.ActualizarMuestra(muestra);
                }
            }
            if (listaMuestrasAgregadas.Count > 0)
            {
                foreach (var muestra in listaMuestrasAgregadas)
                {
                    //muestra.MuestraEnviada = true;
                    muestra.idEnvio = null;
                    muestra.EstadoEdicion = 0;
                    muestra.MuestraEnviada = false;
                    muestraBC.ActualizarMuestra(muestra);
                }
            } 
        }

        /*
         *  Decripción: Se procede a realizar los cambios en las muestras que han sido eliminadas o agregadas al envio ya que esta operación fue confirmada
         */
        public void GuardarCambiosMuestrasEnvio(int idEnvio)
        {
            var listaMuestrasEliminadas = muestraBC.ObtenerMuestrasELiminadasEnvio(idEnvio);
            var listaMuestrasAgregadas = muestraBC.ObtenerMuestrasAgregadasEnvio(idEnvio);

            if (listaMuestrasEliminadas.Count > 0)
            {
                foreach (var muestra in listaMuestrasEliminadas)
                {
                    muestra.idEnvioAnterior = muestra.idEnvio;
                    muestra.idEnvio = null;
                    muestra.MuestraEnviada = false;
                    muestra.EstadoEdicion = 0;
                    muestraBC.ActualizarMuestra(muestra);
                }
            }
            if (listaMuestrasAgregadas.Count > 0)
            {
                foreach (var muestra in listaMuestrasAgregadas)
                {
                    muestra.MuestraEnviada = true;
                    muestra.EstadoEdicion = 0;
                    muestraBC.ActualizarMuestra(muestra);
                }
            } 
        }

        public void MarcarEnvioRecibido(string usuario, int idEnvio)
        {
            var envio = ObtenerEnvio(idEnvio);
            envio.EnvioRecibido = true;
            envio.ModificadoPor = usuario;
            envio.FechaModificacion = DateTime.Today;
            ActualizarEnvio(envio);
        }
    }
}
