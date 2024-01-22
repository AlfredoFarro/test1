using System;
using System.Collections.Generic;
using BE;
using DA;

namespace BC
{
    public class MuestraBC
    {
        readonly MuestraDA da = new MuestraDA();
        readonly ResultadoBC resultadoBC = new ResultadoBC();

        public void InsertarMuestras(List<Muestra> muestras)
        {
            da.InsertarMuestras(muestras);
        }
        public Muestra InsertarMuestra(Muestra muestra)
        {

            return da.InsertarMuestra(muestra);
        }

        public Muestra ObtenerMuestraxCodigoMuestra(string codigoMuestra)
        {
            return da.ObtenerMuestraxCodigoMuestra(codigoMuestra);
        }

        public Muestra ObtenerMuestraxIdMuestra(int idMuestra)
        {
            return da.ObtenerMuestraxIdMuestra(idMuestra);
        }

        //public List<Vista_BuscarPaciente> ObtenerMuestras(string codigoMuestra, string ApellidosNeonato, string apellidosMadre, string DNI, string HClinica, int anho, int mes)
        //{
        //    return da.ObtenerMuestras(codigoMuestra, ApellidosNeonato, apellidosMadre, DNI, HClinica,anho,mes);
        //}

        public List<Vista_BuscarPaciente> ObtenerMuestras(int estado,int idEstablecimiento, string codigoMuestra, string codigoCorrelativo, string apellidosNeonato, string apellidosMadre, string dni, string dateInicio, string dateFin,string dateInicioToma, string dateFinToma)
        {
            //DateTime inicio;
            //DateTime fin;
            //bool usarInicio = DateTime.TryParse(dateInicio, out inicio);
            //bool usarFin = DateTime.TryParse(dateFin,out fin);

            //if (usarFin) fin.AddDays(1);

            //DateTime inicioToma;
            //DateTime finToma;
            //bool usarInicioToma = DateTime.TryParse(dateInicioToma, out inicioToma);
            //bool usarFinToma = DateTime.TryParse(dateFinToma, out finToma);

            //if (usarFinToma) finToma = finToma.AddDays(1);

            DateTime inicio = DateTime.Now;
            DateTime fin = DateTime.Now;
            DateTime inicioToma = DateTime.Now;
            DateTime finToma = DateTime.Now;
            bool usarInicio = false;
            bool usarFin = false;
            bool usarInicioToma = false;
            bool usarFinToma = false;

            if (dateInicio.CompareTo(string.Empty) != 0)
            {
                usarInicio = DateTime.TryParse(dateInicio, out inicio);
            }
            if (dateFin.CompareTo(string.Empty) != 0)
            {
                usarFin = DateTime.TryParse(dateFin, out fin);
            }

            if (usarFin) fin.AddDays(1);

            if (dateInicioToma.CompareTo(string.Empty) != 0)
            {
                usarInicioToma = DateTime.TryParse(dateInicioToma, out inicioToma);
            }
            if (dateFinToma.CompareTo(string.Empty) != 0)
            {
                usarFinToma = DateTime.TryParse(dateFinToma, out finToma);
            }
            if (usarFinToma) finToma = finToma.AddDays(1);

            return da.ObtenerMuestras(estado,idEstablecimiento, codigoMuestra, codigoCorrelativo, apellidosNeonato, apellidosMadre, dni, usarInicio, usarFin, inicio, fin, usarInicioToma, usarFinToma, inicioToma, finToma);
        }

        public List<Vista_BuscarPaciente> ObtenerMuestras(int idEstablecimiento, string codigoMuestra, bool exportada, string digitador)
        {
            return da.ObtenerMuestras(idEstablecimiento, codigoMuestra, exportada, digitador);
        }


        public bool ActualizarMuestra(Muestra muestra)
        {
            return da.ActualizarMuestra(muestra);
        }

        public bool ExisteMuestra(string codigoMuestra)
        {
            return da.ExisteMuestra(codigoMuestra);
        }

        public bool ExisteCorrelativo(string codigoCorrelativo)
        {
            return da.ExisteCorrelativo(codigoCorrelativo);
        }
        public bool CoincideCorrelativoMuestra(string codigoMuestra, string codigoCorrelativo)
        {
            return da.CoincideCorrelativoMuestra(codigoMuestra, codigoCorrelativo);
        }

        //public List<Vista_CodigoMuestraxDNI> ObtenerCantidadMuestras(string dni)
        //{
        //    return da.ObtenerCantidadMuestras(dni);
        //}

        public List<Muestra> ObtenerMuestrasxIdNeonato(int idNeonato)
        {
            return da.ObtenerMuestrasxIdNeonato(idNeonato);
        }

        public List<Muestra> ObtenerMuestrasBloqueadas()
        {
            return da.ObtenerMuestrasBloqueadas();
        }

        public List<Vista_MuestrasxNeonato> ObtenerMuestrasxNeonato(int idNeonato)
        {
            return da.ObtenerMuestrasxNeonato(idNeonato);
        }
        public List<Muestra> ObtenerListaMuestrasxEnvio(int idEnvio)
        {
            return da.ObtenerListaMuestrasxEnvio(idEnvio);
        }
        public List<Vista_Muestra> ObtenerListaMuestrasRechazadasEstablecimiento(int idEstablecimiento, string codigoMuestra, string apellidosNeonato, string apellidosMadre, string dni)
        {
            return da.ObtenerListaMuestrasRechazadasEstablecimiento(idEstablecimiento, codigoMuestra, apellidosNeonato, apellidosMadre, dni);
        }

        public List<Vista_Muestra> ObtenerListaMuestrasRechazadas(int idEstablecimiento, string apellidosNeonato, string codigoCorrelativo, string codigoMuestra, string dni, string inicioRecepcion, string finRecepcion)
        {
            DateTime inicio;
            DateTime fin;
            bool usarInicio = DateTime.TryParse(inicioRecepcion, out inicio);
            bool usarFin = DateTime.TryParse(finRecepcion, out fin);

            if (usarFin) fin.AddDays(1);

            return da.ObtenerListaMuestrasRechazadas(idEstablecimiento,apellidosNeonato,codigoCorrelativo,codigoMuestra,dni,usarInicio,usarFin,inicio,fin);
        }

        public List<Vista_ResultadosEstablecimiento> ObtenerMuestrasxEstablecimiento(int idEstablecimiento, string codigoMuestra, string apellidosNeonato, string apellidosMadre, string dni,int determinacion)
        {
            return da.ObtenerMuestrasxEstablecimiento(idEstablecimiento, codigoMuestra, apellidosNeonato,apellidosMadre, dni,determinacion);
        }

        public bool MarcarMuestrasRecibidas(string usuario, List<string> listaCodigosMuestras)
        {
            return da.MarcarMuestrasRecibidas(usuario, listaCodigosMuestras);
        }

        public bool MarcarMuestrasRecibidRetiradas(string usuario, List<string> listaCodigosMuestras)
        {
            return da.MarcarMuestrasRetiradas(usuario, listaCodigosMuestras);
        }

        //public void MarcarMuestraRecibida(string usuario, string dni, string nombres, string apellidos, string fechaNacimiento, string codigoMuestra)
        public void MarcarMuestraRecibida(string usuario,string codigoMuestra, int idNeonato)
        {
            //DateTime fechaAux;
            //if (DateTime.TryParse(fechaNacimiento, out fechaAux))
            //{
            //    da.MarcarMuestraRecibidaRechazadas(usuario, dni, nombres, apellidos, fechaAux, codigoMuestra);
            //    da.MarcarMuestraRecibidaAlteradas(usuario, dni, nombres, apellidos, fechaAux, codigoMuestra);
            //}
            da.MarcarMuestraRecibidaRechazadas(usuario, codigoMuestra, idNeonato);
            da.MarcarMuestraRecibidaAlteradas(usuario, codigoMuestra ,idNeonato);
        }

        #region Exportar

        public void ExportarMuestrasINMP()
        {
            da.ExportarMuestrasINMP();
        }

        public void ExportarMuestrasINMP(string listaCodigosCorrelativos)
        {
            da.ExportarMuestrasINMP(listaCodigosCorrelativos);
        }

        #endregion

        

        #region Digitacion
        public List<Vista_Muestra> ObtenerMuestrasDigitadasPendientesEnvio(int idEstablecimiento)
        {
            return da.ObtenerMuestrasDigitadasPendientesEnvio(idEstablecimiento);
        }

        public List<Vista_Muestra> ObtenerMuestrasDigitadasEdicion(int idEstablecimiento)
        {
            return da.ObtenerMuestrasDigitadasEdicion(idEstablecimiento);
        }

        /*
         *  
         */
        public List<Vista_BuscarPaciente> ObtenerMuestrasDigitadas(int idEstablecimiento, string apellidosNeonato, string apellidosMadre, string fechaDigitadoInicial, string fechaDigitadoFinal, string codigoMuestra, string DNI)
        {
            bool usarFechaInicial = false;
            bool usarFechaFinal = false;
            DateTime fechaInicialAux;
            DateTime fechaFinalAux;
            usarFechaInicial = DateTime.TryParse(fechaDigitadoInicial, out fechaInicialAux);
            usarFechaFinal = DateTime.TryParse(fechaDigitadoFinal, out fechaFinalAux);

            return da.ObtenerMuestrasDigitadas(idEstablecimiento, apellidosNeonato, apellidosMadre, usarFechaInicial, usarFechaFinal, fechaInicialAux, fechaFinalAux, codigoMuestra, DNI);
        }



        public bool EliminarMuestra(string idMuestra)
        {
            int id;
            if (int.TryParse(idMuestra, out id))
            {
                //var muestraBC = new MuestraBC();

                Muestra muestra = ObtenerMuestraxIdMuestra(id);
                var resultados = resultadoBC.ObtenerResutados(muestra.CodigoMuestra, muestra.CodigoInternoLab); //revisar

                if (resultados.Count == 0)
                {
                    muestra.Estado = 0;
                    return ActualizarMuestra(muestra);
                }
                
            }
            return false;
            
        }
        #endregion

        #region Envio
        public List<Vista_Muestra> ObtenerVistaMuestrasEnvio(int idEnvio)
        {
            //return db.dc.Vista_Muestras.Where(m => m.idEstablecimiento == idEstablecimiento && m.MuestraEnviada == false).ToList();
            return da.ObtenerVistaMuestrasEnvio(idEnvio);
        }

        public List<Muestra> ObtenerMuestrasEnvio(int idEnvio)
        {
            //return db.dc.Vista_Muestras.Where(m => m.idEstablecimiento == idEstablecimiento && m.MuestraEnviada == false).ToList();
            return da.ObtenerMuestrasEnvio(idEnvio);
        }

        /*
         * Descripción: Obtiene las muestras con idEnvioAnterior iguales al idEnvio esto sirve para poder retornar al estado original el envio cuando se cancela 
         * la edición
         */
        public List<Muestra> ObtenerMuestrasCancelarEdicionEnvio(int idEnvio)
        {
            //return db.dc.Vista_Muestras.Where(m => m.idEstablecimiento == idEstablecimiento && m.MuestraEnviada == false).ToList();
            return da.ObtenerMuestrasCancelarEdicionEnvio(idEnvio);
        }
        public List<Muestra> ObtenerMuestrasELiminadasEnvio(int idEnvio)
        {
            return da.ObtenerMuestrasEliminadasEnvio(idEnvio);
        }
        public List<Muestra> ObtenerMuestrasAgregadasEnvio(int idEnvio)
        {
            return da.ObtenerMuestrasAgregadasEnvio(idEnvio);
        }
        #endregion

        #region ResultadosVerificacion

        public List<Vista_Muestra> ObtenerMuestrasVerificacion(int inicial, int final)
        {
            return da.ObtenerMuestrasVerificacion(inicial, final);
        }

        #endregion
    }
}
