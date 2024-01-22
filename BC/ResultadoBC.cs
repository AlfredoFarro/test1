using System;
using System.Collections.Generic;
using System.Data;
using BE;
using DA;
using System.Linq;

namespace BC
{
    public class ResultadoBC
    {
        readonly ResultadoDA da = new ResultadoDA();

        public List<Vista_Resultado> ObtenerResutados(string codigoMuestra, string codigoCorrelativo)
        {
            return da.ObtenerResutados(codigoMuestra, codigoCorrelativo);
        }

        //public List<Vista_Resultado> ObtenerResutados(string codigoMuestra)
        //{
        //    return da.ObtenerResutados(codigoMuestra);
        //}

        public void RegistrarResultados(List<Resultado> listaResultados)
        {
            da.RegistrarResultados(listaResultados);
        }


        public List<Vista_BuscarResultado> BuscarResultado(string codigoMuestra, int numEnsayo)
        {
            var lista = new List<Vista_BuscarResultado>();
            if ((codigoMuestra != null) && (codigoMuestra.CompareTo(string.Empty) != 0))
            {
                return da.BuscarResultadoxCodigoMuestra(codigoMuestra);
            }
            if (numEnsayo > 0)
            {
                return da.BuscarResultadoxNumEnsayo(numEnsayo);
            }

            return lista;
        }
        //---------------
        public List<Vista_ResultadosGSP> ObtenerListaResultadosGSP(int EnsayoId)
        {
            return da.ObtenerListaResultadosGSP(EnsayoId);
        }

        //---------------


        public List<Vista_BuscarResultado> BuscarResultadoxCodigoMuestra(string codigoMuestra)
        {
            return da.BuscarResultadoxCodigoMuestra(codigoMuestra);
        }
        //public List<Vista_ResultadosEstablecimiento> ObtenerResultadosHospital(int idEstablecimiento, bool usarInicio, bool usarFin, DateTime dateInicio, DateTime dateFin, string apellidosNeonato, string apellidosMadre,string codigoMuestra,string dni)//, int idPrueba, int determinacion)
        //{
        //   return da.ObtenerResultadosHospital(idEstablecimiento, usarInicio,usarFin,dateInicio,dateFin,apellidosNeonato,apellidosMadre,codigoMuestra,dni);
        //}


        public List<Vista_ResultadosEstablecimiento> ObtenerResultadosHospital(string establecimientoId, string inicio, string fin, string apellidosNeonato, string apellidosMadre, string codigoMuestra, string dni)//, int idPrueba, int determinacion)
        {
            int idEstablecimiento = int.Parse(establecimientoId);
            var dateInicio = new DateTime();
            var dateFin = new DateTime();
            bool usarInicio = DateTime.TryParse(inicio, out dateInicio);
            bool usarFin = DateTime.TryParse(fin, out dateFin);

            return da.ObtenerResultadosHospital(idEstablecimiento, usarInicio, usarFin, dateInicio, dateFin, apellidosNeonato, apellidosMadre, codigoMuestra, dni);
        }


        public List<Vista_ResultadosEstablecimiento> ObtenerResultadosHospitalAlterados(int idEstablecimiento, string codigoMuestra, string apellidosNeonato, string apellidosMadre, string dni)
        {
            return da.ObtenerResultadosHospitalAlterados(idEstablecimiento, codigoMuestra, apellidosNeonato, apellidosMadre, dni);
        }
        public List<Resultado> ListaResultadosxEnsayo(int idEnsayo, int estado)
        {
            return da.ListaResultadosxEnsayo(idEnsayo, estado);
        }
        public List<Resultado> ListaResultadosNoPublicados(int idEnsayo)
        {
            return da.ListaResultadosNoPublicados(idEnsayo);
        }
        //public List<VistaResultadoDNI> ObtenerResutadoCodigoMuestra(string codigoMuestra)
        //{
        //    return da.ObtenerResutadoCodigoMuestra(codigoMuestra);
        //}

        public List<Ensayo> EnsayosProcesados(string idPrueba, string fechaResultado, string runId)
        {
            int auxId = int.Parse(idPrueba);
            int auxRunId = 0;
            if (runId.CompareTo(string.Empty) != 0) auxRunId = int.Parse(runId);
            DateTime auxFechaResultado;
            bool usarFecha = DateTime.TryParse(fechaResultado, out auxFechaResultado);
            return da.EnsayosProcesados(auxId, usarFecha, auxFechaResultado, auxRunId);
        }

        public List<Vista_ResultadosMuestra> ResultadosxNeonato(int idNeonato)
        {
            return da.ResultadosxNeonato(idNeonato);
        }

        public List<Vista_MuestraResultado> ObtenerMuestraResultadosNeonato(int idNeonato)
        {
            return da.ObtenerMuestraResultadosNeonato(idNeonato);
        }

        #region ResultadosArchivo

        public List<Vista_ResultadosArchivo> ObtenerResultadosArchivo(string prueba, string dateInicio, string dateFin, bool soloPublicado)
        {
            DateTime inicio;
            DateTime fin;
            bool usarInicio = DateTime.TryParse(dateInicio, out inicio);
            bool usarFin = DateTime.TryParse(dateFin, out fin);

            if (usarFin)
                fin = fin.AddDays(1);

            return da.ObtenerResultadosArchivo(prueba, usarInicio, usarFin, inicio, fin, soloPublicado);
        }

        #endregion

        #region ResultadoConsolidado

        public List<Vista_ResultadosArchivo> ObtenerResultadosConsolidados(int pruebas, DateTime fechaConsolidado)
        {
            var listaPruebas = new List<string>();
            if (pruebas == 0)
            {
                listaPruebas.Add("NTSH");
                listaPruebas.Add("N17OHP");
                return da.ObtenerResultadosConsolidados(listaPruebas, fechaConsolidado);
            }
            else
            {
                listaPruebas.Add("NeoPhe");
                listaPruebas.Add("TGAL");
                return da.ObtenerResultadosConsolidados(listaPruebas, fechaConsolidado);
            }
        }
        //public List<Resultado> ObtenerResultadosCodigoMuestra(string codigoMuestra, DateTime fechaResultado, int pruebas)
        //{
        //    var listaPruebas = new List<string>();
        //    if (pruebas == 0)
        //    {
        //        listaPruebas.Add("NTSH");
        //        listaPruebas.Add("N17OHP");
        //        return da.ObtenerResultadosCodigoMuestra(codigoMuestra, fechaResultado, listaPruebas);
        //    }
        //    else
        //    {
        //        listaPruebas.Add("NeoPhe");
        //        listaPruebas.Add("TGAL");
        //        return da.ObtenerResultadosCodigoMuestra(codigoMuestra, fechaResultado, listaPruebas);
        //    }
        //    //return da.ObtenerResultadosCodigoMuestra(codigoMuestra, fechaResultado, listaPruebas);
        //}

        //public Resultado ObtenerResultadoTest(string codigoMuestra, DateTime fechaResultado, string prueba)
        //{
        //    return da.ObtenerResultadoTest(codigoMuestra, fechaResultado, prueba);
        //}

        public Resultado ObtenerResultadoTest(string codigoMuestra, string codigoCorrelativo, string prueba)
        {
            return da.ObtenerResultadoTest(codigoMuestra, codigoCorrelativo, prueba);
        }
        /*
        public DataTable ObtenerCodigosMuestraMedidos(int pruebas, DateTime fechaConsolidado)
        {
            string listaPruebas;
            if (pruebas == 0)
            {
                listaPruebas = "('NTSH','N17OHP')";
            }
            else
            {
                listaPruebas = "('NeoPhe','TGAL')";
            }
            return da.ObtenerCodigosMuestraMedidos(listaPruebas, fechaConsolidado);

        }
        */
        /*
        public DataTable ObtenerMuestrasResultados(int pruebas, DateTime fechaConsolidado)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Num");
            dt.Columns.Add("Correlativo");
            dt.Columns.Add("ApellidosRN");
            dt.Columns.Add("ApellidosMadre");
            dt.Columns.Add("Establecimiento");
            dt.Columns.Add("FechaNacimiento");
            dt.Columns.Add("FechaToma");
            dt.Columns.Add("Prueba");
            dt.Columns.Add("EdadGestacional");

            if (pruebas == 0)
            {
                dt.Columns.Add("TSH");
                dt.Columns.Add("17OHP");
            }
            else
            {
                dt.Columns.Add("NeoPhe");
                dt.Columns.Add("TGAL");
            }

            DataTable dtCodigos = ObtenerCodigosMuestraMedidos(pruebas, fechaConsolidado);
            //List<DataRow> listaCodigosLinq = dtCodigos.AsEnumerable().ToList();

            string listaCodigos = "(";
            bool primero = true;
            foreach (DataRow dr in dtCodigos.Rows)
            {
                if (primero)
                {
                    listaCodigos = string.Concat("('", dr["CodigoMuestra"].ToString());
                    primero = false;
                }
                else
                {
                    listaCodigos = string.Concat(listaCodigos, "','", dr["CodigoMuestra"].ToString());
                }

            }

            listaCodigos = string.Concat(listaCodigos, "')");
            DataTable pacientes = da.ObtenerPacientes(listaCodigos);

            int i = 1;
            foreach (DataRow drPaciente in pacientes.Rows)
            {
                var row = dt.NewRow();
                row["Num"] = i++;
                row["Correlativo"] = drPaciente["CodigoInternoLab"].ToString();
                row["ApellidosRN"] = drPaciente["ApellidosNeonato"].ToString();
                row["ApellidosMadre"] = drPaciente["ApellidosMadre"].ToString();
                row["Establecimiento"] = drPaciente["Establecimiento"].ToString();
                row["FechaNacimiento"] = drPaciente["FechaNacimiento"].ToString();
                row["FechaToma"] = drPaciente["FechaToma"].ToString();
                //row["Prueba"] = drPaciente[""].ToString();
                row["EdadGestacional"] = drPaciente["EdadGestacional"].ToString();

                if (pruebas == 0)
                {
                    if (ObtenerResultadoTest(drPaciente["CodigoInternoLab"].ToString(), "NTSH") != null)
                        row["TSH"] = ObtenerResultadoTest(drPaciente["CodigoInternoLab"].ToString(), "NTSH").ConcTexto;
                    else
                    {
                        row["TSH"] = "";
                    }

                    if (ObtenerResultadoTest(drPaciente["CodigoInternoLab"].ToString(), "N17OHP") != null)
                        row["17OHP"] = ObtenerResultadoTest(drPaciente["CodigoInternoLab"].ToString(), "N17OHP").ConcTexto;
                    else
                    {
                        row["17OHP"] = "";
                    }
                    //row["17OHP"] = ObtenerResultadoTest(drPaciente["CodigoInternoLab"].ToString(), "17OHP").ConcTexto;
                }
                else
                {
                    row["NeoPhe"] = ObtenerResultadoTest(drPaciente["CodigoInternoLab"].ToString(), "NeoPhe").ConcTexto;
                    row["TGAL"] = ObtenerResultadoTest(drPaciente["CodigoInternoLab"].ToString(), "TGAL").ConcTexto;
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
        */
        #endregion

        #region Publicacion
        public List<Vista_ResultadosGSP> ObtenerResultadosPublicacion(int EnsayoId, string discriminador, string valor, int tipoResultado, string codigoCorrelativo,string publicado)
        {
            double conc = 0;
            if (double.TryParse(valor, out conc))
            {
                return da.ObtenerResultadosPublicacion(EnsayoId, discriminador, conc, tipoResultado, codigoCorrelativo,publicado);
            }
            else
            {
                return da.ObtenerResultadosPublicacion(EnsayoId, string.Empty, conc, tipoResultado, codigoCorrelativo,publicado);
            }

        }



        //public void PublicarResultados(List<string> listaCodigosNoPublicados, int idEnsayo)
        //{
        //    da.PublicarTodosResultadosEnsayo(idEnsayo, string.Empty);
        //    if (listaCodigosNoPublicados.Count() > 0)
        //        da.MarcarNoPublicados(listaCodigosNoPublicados, idEnsayo);
        //}

        public void PublicarResultados(string listaCodigosNoPublicados, int idEnsayo, string usuario, string listaIdDetalleEnsayoNoPublicados)
        {
            da.PublicarTodosResultadosEnsayo(idEnsayo, usuario);    //Publica todos los resultados de un Ensayo
            da.PublicarTodoDetalleEnsayo(idEnsayo);                 //Publica todos los detalles de Ensayo 
            if (listaCodigosNoPublicados.CompareTo(string.Empty) != 0)
                da.MarcarNoPublicados(listaCodigosNoPublicados, idEnsayo);

            if (listaIdDetalleEnsayoNoPublicados.CompareTo(string.Empty) != 0)
                da.MarcarIdDetalleNoPublicados(listaIdDetalleEnsayoNoPublicados);

            //List<Vista_ResultadosGSP> listaResultados = da.ObtenerResultadosPublicados(idEnsayo);
            List<Vista_ResultadosGSP> listaResultados = da.ObtenerResultadosPublicadosINMP(idEnsayo);
            
            string resultadosEnviar = "('";
            string resultadosEnviarNew = "('";
            //string muestrasEnviar = "('";
            //string codigosExportados = string.Empty;
            foreach (var resultado in listaResultados)
            {
                string publicado = "SV";
                if (!bool.Parse(resultado.Publicado.ToString())) publicado = "NV";
                resultadosEnviar = string.Concat(resultadosEnviar,
                                                resultado.CodigoMuestra, "','",
                                                resultado.Prueba, "','",
                                                resultado.ConcTexto, "','",
                                                resultado.Unidad,
                                                "',NULL,",
                                                1,
                                                ",'",
                                                resultado.ResultCode,
                                                "',NULL,'",
                                                resultado.Rango,
                                                "',",
                                                resultado.AssayRunID,
                                                ",'",
                                                resultado.FechaResultado.ToShortDateString(),
                                                "','",
                                                resultado.Pocillo,
                                                "',NULL,1,'",
                                                DateTime.Today.ToShortDateString(),
                                                "','",
                                                DateTime.Today,
                                                "','CIENTIFICA ANDINA','",
                                                publicado,
                                                "'),('");
                
                 //(<CORRELATIVO, varchar(15),>
           //,<NRO_ORDEN, numeric(12,0),>
           //,<DOCUMENTO, varchar(15),>
           //,<COD_EXAMEN, varchar(15),>
           //,<FECHA_RESULTADO, varchar(30),>
           //,<CODIGO_ANALITO, varchar(20),>
           //,<NOMBRE_ANALITO, varchar(200),>
           //,<VALOR_ANALITO, varchar(200),>
           //,<VALOR_REFERENCIAL, text,>
           //,<REFERENCIA_MINIMA, varchar(40),>
           //,<REFERENCIA_MAXIMA, varchar(40),>
           //,<UNIDAD_RESULTADO, varchar(30),>
           //,<USUARIO, varchar(50),>
           //,<USUARIO_VALIDA, varchar(50),>
           //,<FECHA_REGISTRA, varchar(25),>
           //,<USUARIO_ANULA, varchar(50),>
           //,<FECHA_ANULA, varchar(25),>
           //,<IP, varchar(20),>
           //,<MAC, varchar(20),>
           //,<FECHA_ACTUALIZA, varchar(25),>
           //,<USUARIO_ACTUALIZA, varchar(50),>
           //,<ESTADO, int,>)

                resultadosEnviarNew = string.Concat(resultadosEnviarNew,
                                                resultado.CodigoMuestra, "','",     //<CORRELATIVO, varchar(15),>
                                                1, "','",                           //<NRO_ORDEN, numeric(12,0),>
                                                "12345678", "','",                  //<DOCUMENTO, varchar(15),>
                                                "80099", "','",                     //<COD_EXAMEN, varchar(15),>
                                                resultado.FechaResultado.ToShortDateString(), "','", //<FECHA_RESULTADO, varchar(30),>
                                                resultado.Prueba, "','",             //<CODIGO_ANALITO, varchar(20),>
                                                resultado.Prueba, "','",             //<NOMBRE_ANALITO, varchar(200),>
                                                resultado.ConcTexto, "','",         //<VALOR_ANALITO, varchar(200),>
                                                null, "','",                        //<VALOR_REFERENCIAL, text,>
                                                null, "','",                        //<REFERENCIA_MINIMA, varchar(40),>
                                                null, "','",                        //<REFERENCIA_MAXIMA, varchar(40),>
                                                resultado.Unidad, "','",            //<UNIDAD_RESULTADO, varchar(30),>
                                                "12345678", "','",                  //<USUARIO, varchar(50),>
                                                "12345678", "','",                  //<USUARIO_VALIDA, varchar(50),>
                                                DateTime.Today.ToShortDateString(), "','",  //<FECHA_REGISTRA, varchar(25),>
                                                null, "','",                        //<USUARIO_ANULA, varchar(50),>
                                                null, "','",                        //<FECHA_ANULA, varchar(25),>
                                                null, "','",                        //<IP, varchar(20),>
                                                null, "','",                        //<MAC, varchar(20),>
                                                null, "','",                        //<FECHA_ACTUALIZA, varchar(25),>
                                                null, "','",                        //<USUARIO_ACTUALIZA, varchar(50),>
                                                null,                               //<ESTADO, int,>
                                                "'),('");
                //if (resultado.Importado != null)
                //{
                //    bool importado = false;
                //    if (bool.TryParse(resultado.Importado.ToString(), out importado))
                //    {
                //        if (!importado)
                //        {
                //            string strPeso = string.Empty;
                //            string strTalla = string.Empty;

                //            if (resultado.Peso != null)
                //            {
                //                decimal peso = 0;
                //                if (Decimal.TryParse(resultado.Peso.ToString(),out peso))
                //                {
                //                    peso = peso/1000;
                //                    strPeso = peso.ToString().Replace(',', '.');
                //                    if (strPeso.Length > 6) strPeso = strPeso.Substring(0, 6);
                //                }else
                //                {
                //                    strPeso = "NULL";
                //                }

                                
                //            }
                //            else strPeso = "NULL";

                //            if (resultado.Talla != null) strTalla = resultado.Talla.ToString().Replace(',', '.');
                //            else strTalla = "NULL";

                //            muestrasEnviar = string.Concat(muestrasEnviar, resultado.CodigoCorrelativo, "','",
                //                                            resultado.NombreMadre, "','",
                //                                            resultado.ApellidosMadre, "','",
                //                                            resultado.NombresMadre, "','",
                //                                            resultado.Neonato, "','",
                //                                            resultado.DNI, "','",
                //                                            "", "','", //HistoriaClinica
                //                                            resultado.FechaNacimiento.ToString(), "','",
                //                                            resultado.FechaToma.ToString(), "',",
                //                                            1, ",'",
                //                                            resultado.FechaRecepcion.ToString(), "','",
                //                                            resultado.CodigoEstablecimiento, "','",
                //                                            "", "','", //TipoEstablecimiento
                //                                            resultado.Establecimiento, "',",
                //                                            resultado.EdadGestacional, ",'",
                //                                            "", "','", //Departamento
                //                                            "", "','", //Provincia
                //                                            "", "','", //Distrito
                //                                            resultado.Telefono1, "','",
                //                                            resultado.Telefono2, "',",
                //                                            strPeso, ",",
                //                                            strTalla, ",'",
                //                                            resultado.Sexo, "','",
                //                                            resultado.EsPrematuro, "','",
                //                                            "", "','", //EsTransfundido
                //                                            resultado.CodigoCorrelativo, "','",
                //                                            resultado.Notas, "',",
                //                                            resultado.NumMuestra, ",'",
                //                                            DateTime.Today.ToShortDateString(), "',",
                //                                            1, "),('");
                //            codigosExportados = string.Concat(codigosExportados, resultado.idMuestra, ",");
                //        }
                //    }
                //}

            }

            if (resultadosEnviar.Length > 3)
            {
                resultadosEnviar = resultadosEnviar.Remove(resultadosEnviar.Length - 3, 3);
                resultadosEnviarNew = resultadosEnviarNew.Remove(resultadosEnviarNew.Length - 3, 3);
                da.RegistrarResultadosINMP(resultadosEnviar);
                da.RegistrarResultadosINMPNew(resultadosEnviarNew);
            }

            //if (muestrasEnviar.Length > 3)
            //{
            //    muestrasEnviar = muestrasEnviar.Remove(muestrasEnviar.Length - 3, 3);
            //    da.RegistrarMuestrasINMP(muestrasEnviar);
            //}
            //if (codigosExportados.Length > 1)
            //{
            //    codigosExportados = codigosExportados.Remove(codigosExportados.Length - 1, 1);
            //    codigosExportados = string.Concat("(", codigosExportados, ")");
            //    da.MarcarMuestrasExportadas(codigosExportados);
            //}


        }

        public int ObtenerIdDetalleEnsayo(string codigoMuestra, string conc)
        {
            return da.ObtenerIdDetalleEnsayo(codigoMuestra, conc);
        }

        //public void 
        #endregion

        #region InformeResultadosHistoricos

        public DataTable ListaResultadosHistoricosPaciente(int idNeonato, string prueba)
        {
            return da.ListaResultadosHistoricosPaciente(idNeonato, prueba);
        }

        #endregion

        #region ResultadosVerificacion

        public List<Resultado> ObtenerResultadosVerificacion(int inicial, int final)
        {
            return da.ObtenerResultadosVerificacion(inicial, final);
        }

        #endregion

        #region BuscarResultados

        public List<Vista_ResultadosGSP> BuscarResultados(string codigoMuestra, int idEnsayo)
        {
            return da.BuscarResultados(codigoMuestra, idEnsayo);
        }

        public List<Vista_ResultadosGSP> BuscarResultadoPorPublicar(string codigoMuestra, int idEnsayo)
        {
            return da.BuscarResultadoPorPublicar(codigoMuestra, idEnsayo);
        }
        public List<Vista_ResultadosGSP> BuscarResultadoPorPublicar(string codigoMuestra, int idEnsayo, string dni, string apellidosMadre)
        {
            return da.BuscarResultadoPorPublicar(codigoMuestra, idEnsayo,dni,apellidosMadre);
        }

        public DetalleEnsayo ObtenerDetalleEnsayoxId(int idDetalleEnsayo)
        {
            return da.ObtenerDetalleEnsayoxId(idDetalleEnsayo);
        }
        public Resultado ObtenerResultadoxId(int idResultado)
        {
            return da.ObtenerResultadoxId(idResultado);
        }

        public bool ActualizarPublicacion(int idDetalleEnsayo, int idResultado, bool publicado)
        {
            try
            {
                DetalleEnsayo detalle = ObtenerDetalleEnsayoxId(idDetalleEnsayo);
                detalle.Publicado = publicado;
                da.ActualizarDetalleEnsayo(detalle);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
           

        }

        public bool ActualizarPublicacionINMP(string codigoCorrelativo,string conc,string analito,string numEnsayo, string publicadoINMP,int idDetalleEnsayo)
        {
            DataTable dt = da.ObtenerResultadoINMP(codigoCorrelativo, conc, analito, numEnsayo);
            if (dt.Rows.Count > 0)
            {
                try
                {
                    da.ActualizarResultadoINMP(codigoCorrelativo, conc, analito, numEnsayo, publicadoINMP);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
                
            }else
            {
                var resultado = da.ObtenerResultadoGSPidDetalleEnsayo(idDetalleEnsayo);
                string resultadosEnviar = "('";
                resultadosEnviar = string.Concat(resultadosEnviar,
                                                resultado.CodigoMuestra, "','",
                                                resultado.Prueba, "','",
                                                resultado.ConcTexto, "','",
                                                resultado.Unidad,
                                                "',NULL,",
                                                1,
                                                ",'",
                                                resultado.ResultCode,
                                                "',NULL,'",
                                                resultado.Rango,
                                                "',",
                                                resultado.AssayRunID,
                                                ",'",
                                                resultado.FechaResultado.ToShortDateString(),
                                                "','",
                                                resultado.Pocillo,
                                                "',NULL,1,'",
                                                DateTime.Today.ToShortDateString(),
                                                "','",
                                                DateTime.Today,
                                                "','CIENTIFICA ANDINA','",
                                                publicadoINMP,
                                                "')");
                try
                {
                    da.RegistrarResultadosINMP(resultadosEnviar);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
                
            }
        }

        #endregion
    }
}
