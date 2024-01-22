using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using System.Data;

namespace DA
{
    public class ResultadoDA
    {
        readonly AccesoDB db = new AccesoDB();

        public void RegistrarResultados(List<Resultado> listaResultados)
        {
            db.dc.Resultados.InsertAllOnSubmit(listaResultados);
            try
            {
                db.dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarDetalleEnsayo(DetalleEnsayo detalle)
        {
            try
            {
                db.dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarResultado(Resultado resultado)
        {
            try
            {
                db.dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DetalleEnsayo ObtenerDetalleEnsayoxId(int idDetalleEnsayo)
        {
            return db.dc.DetalleEnsayos.Where(d => d.idDetalleEnsayo == idDetalleEnsayo).First();
        }
        public Resultado ObtenerResultadoxId(int idResultado)
        {
            return db.dc.Resultados.Where(r => r.idResultado == idResultado).First();
        }


        public List<Vista_ResultadosGSP> ObtenerListaResultadosGSP(int EnsayoId)
        {
            return db.dc.Vista_ResultadosGSPs.Where(r => r.idEnsayo == EnsayoId && r.SampleRole == 3).OrderBy(r => r.SampleIX).ToList();
        }
        //public void ActualizarEstadoResultadoMuestra(List<int> lista)
        //{
        //    foreach (int codigo in lista)
        //    {
        //        var res = (from r in db.dc.GetTable<Resultado>() where r.idResultado == codigo select r).FirstOrDefault();
        //        res.Estado = 0;
        //    }
        //    db.dc.SubmitChanges();
        //}

        //public void PublicarResultados(List<int> listaIdsNoPublicados, int idEnsayo)
        //{ 
        //    //Selecciono todos los resultados del ensayo y los marco como publicados
        //    var resultados = (from r in db.dc.Resultados where r.idEnsayo == idEnsayo && r.Estado == 1 select r).ToList();
        //    resultados.ForEach(r => r.Publicado = true);


        //    //Selecciono los resultados que no deben publicarse y los marco
        //    var resultadosNoPublicados = resultados.Where(r => listaIdsNoPublicados.Contains(r.idResultado)).ToList();
        //    resultadosNoPublicados.ForEach(r => r.Publicado = false);

        //    db.dc.SubmitChanges();
        //}



        public List<Vista_Resultado> ObtenerResutados(string codigoMuestra, string codigoCorrelativo)
        {
            return db.dc.Vista_Resultados.Where(r => (r.CodigoMuestra == codigoMuestra || r.CodigoMuestra == codigoCorrelativo) && r.Publicado == true).ToList();
        }

        //public List<Vista_Resultado> ObtenerResutados(string codigoMuestra)
        //{
        //    return db.dc.Vista_Resultados.Where(r => r.CodigoMuestra == codigoMuestra && r.Publicado == true).ToList();
        //}

        public List<Vista_BuscarResultado> BuscarResultadoxNumEnsayo(int numEnsayo)
        {
            return db.dc.Vista_BuscarResultados.Where(r => r.AssayRunID == numEnsayo).ToList();
        }
        public List<Vista_BuscarResultado> BuscarResultadoxCodigoMuestra(string codigoMuestra)
        {
            return db.dc.Vista_BuscarResultados.Where(r => r.CodigoMuestra == codigoMuestra).ToList();
        }
        public List<Vista_BuscarResultado> BuscarResultadoxidPrueba(int idPrueba)
        {
            return db.dc.Vista_BuscarResultados.Where(r => r.idPrueba == idPrueba).ToList();
        }
        public List<Vista_BuscarResultado> BuscarResultadoxFechaResultado(int idPrueba, DateTime inicio, DateTime fin)
        {
            return db.dc.Vista_BuscarResultados.Where(r => r.idPrueba == idPrueba && r.FechaMedicion >= inicio && r.FechaMedicion <= fin).ToList();
        }

        public List<Resultado> ListaResultadosxEnsayo(int idEnsayo, int estado)
        {
            return (from r in db.dc.Resultados where r.idEnsayo == idEnsayo && r.Estado == estado orderby r.AssayLineNumber ascending select r).ToList();
        }

        public List<Resultado> ListaResultadosNoPublicados(int idEnsayo)
        {
            return (from r in db.dc.Resultados where r.idEnsayo == idEnsayo && r.Publicado == false orderby r.AssayLineNumber ascending select r).ToList();
        }



        //public List<VistaResultadoDNI> ObtenerResutadoCodigoMuestra(string codigoMuestra)
        //{
        //    var resultados = (from r in db.dc.GetTable<VistaResultadoDNI>() where r.CodigoMuestra == codigoMuestra select r).ToList();
        //    return resultados;
        //}

        //public List<Vista_ResultadosEstablecimiento> ObtenerListaResutadosHospital(int idEstablecimiento, string dni, string apellidosNeonato,)
        //{

        //}
        /*
         int idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
            string codigoMuestra = txtCodigoMuestra.Text;
            string apellidosNeonato = txtApellidosNeonato.Text;
            string apellidosMadre = txtApellidosMadre.Text;
            string DNI = txtDNI.Text;
         */
        public List<Vista_ResultadosEstablecimiento> ObtenerResultadosHospitalAlterados(int idEstablecimiento, string codigoMuestra, string apellidosNeonato, string apellidosMadre, string dni)
        {
            var lista = new List<Vista_ResultadosEstablecimiento>();

            if (idEstablecimiento > 0)
            {
                lista = db.dc.Vista_ResultadosEstablecimientos.Where(r => r.idEstablecimiento == idEstablecimiento && r.Publicado == true && r.rdcDeterminationLevel > 20 && r.RecibioMuestra == false && r.RetiradaPanelAlertas == false).ToList();
            }
            else
            {
                lista = db.dc.Vista_ResultadosEstablecimientos.Where(r => r.Publicado == true && r.rdcDeterminationLevel > 20 && r.RecibioMuestra == false && r.RetiradaPanelAlertas == false).ToList();
            }

            if (codigoMuestra.CompareTo(string.Empty) != 0)
                lista = lista.Where(r => r.CodigoMuestra.Contains(codigoMuestra)).ToList();

            if (dni.CompareTo(string.Empty) != 0)
                lista = lista.Where(r => r.DNI.Contains(dni)).ToList();

            if (apellidosMadre.CompareTo(string.Empty) != 0)
                lista = lista.Where(r => r.ApellidosMadre.Contains(apellidosMadre)).ToList();

            if (apellidosNeonato.CompareTo(string.Empty) != 0)
                lista = lista.Where(r => r.ApellidosNeonato.Contains(apellidosNeonato)).ToList();


            return lista.OrderBy(r => r.idMuestra).ToList();
        }

        public List<Vista_ResultadosEstablecimiento> ObtenerResultadosHospital(int idEstablecimiento, bool usarInicio, bool usarFin, DateTime dateInicio, DateTime dateFin, string apellidosNeonato, string apellidosMadre, string codigoMuestra, string dni)
        {
            List<Vista_ResultadosEstablecimiento> lista;

            if (idEstablecimiento > 0)
            {
                lista = db.dc.Vista_ResultadosEstablecimientos.Where(r => r.idEstablecimiento == idEstablecimiento && r.Publicado == true).ToList();
            }
            else
            {
                lista = db.dc.Vista_ResultadosEstablecimientos.Where(r => r.Publicado == true).ToList();
            }

            if (codigoMuestra.CompareTo(string.Empty) != 0)
                lista = lista.Where(r => r.CodigoMuestra.Contains(codigoMuestra)).ToList();

            if (dni.CompareTo(string.Empty) != 0)
                lista = lista.Where(r => r.DNI.Contains(dni)).ToList();

            if (apellidosMadre.CompareTo(string.Empty) != 0)
                lista = lista.Where(r => r.ApellidosMadre.Contains(apellidosMadre)).ToList();

            if (apellidosNeonato.CompareTo(string.Empty) != 0)
                lista = lista.Where(r => r.ApellidosNeonato.Contains(apellidosNeonato)).ToList();

            if (usarInicio && usarFin)
            {
                if (dateInicio == dateFin)
                {
                    dateFin = dateFin.AddDays(1);
                }
                lista = lista.Where(r => r.FechaNacimiento >= dateInicio && r.FechaNacimiento <= dateFin).ToList();
            }
            if (usarInicio && !usarFin)
            {
                lista = lista.Where(r => r.FechaNacimiento >= dateInicio).ToList();
            }

            if (!usarInicio && usarFin)
            {
                lista = lista.Where(r => r.FechaNacimiento <= dateFin).ToList();
            }

            return lista.OrderBy(r => r.CodigoMuestra).ToList();

        }

        public List<Ensayo> EnsayosProcesados(int idPrueba, bool usarFecha, DateTime fechaResultado, int runId)
        {
            if (runId > 0)
            {
                return db.dc.Ensayos.Where(e => e.AssayRunID == runId).ToList();
            }
            if (idPrueba > 0)
            {
                //var listaEnsayos = db.dc.Ensayo.Where(e => e.idPrueba == idPrueba).ToList();
                if (usarFecha)
                {
                    return db.dc.Ensayos.Where(e => e.idPrueba == idPrueba && e.FechaFinish >= fechaResultado).ToList(); //listaEnsayos.Where(e => e.RunDate == fechaResultado).ToList();
                }
                else
                {
                    return db.dc.Ensayos.Where(e => e.idPrueba == idPrueba).ToList();
                }

            }
            else
            {
                return db.dc.Ensayos.Where(e => e.FechaFinish >= fechaResultado).ToList();
            }
        }

        public List<Vista_ResultadosMuestra> ResultadosxNeonato(int idNeonato)
        {
            return db.dc.Vista_ResultadosMuestras.Where(r => r.idNeonato == idNeonato).ToList();
        }

        public List<Vista_MuestraResultado> ObtenerMuestraResultadosNeonato(int idNeonato)
        {
            return db.dc.Vista_MuestraResultados.Where(r => r.idNeonato == idNeonato && r.Estado == 1).ToList();
        }

        #region ResultadosArchivo

        public List<Vista_ResultadosArchivo> ObtenerResultadosArchivo(string prueba, bool usarInicio, bool usarFin, DateTime dateInicio, DateTime dateFin, bool soloPublicados)
        {
            var lista = new List<Vista_ResultadosArchivo>();// = db.dc.Vista_ResultadosArchivos.Where(r => r.TestName == prueba && r.rdcDeterminationLevel > 20).ToList();

            if (usarInicio && usarFin)
            {
                if (dateInicio == dateFin)
                {
                    lista = db.dc.Vista_ResultadosArchivos.Where(r => r.TestName == prueba && r.rdcDeterminationLevel > 20 && r.FechaMedicion.Year == dateInicio.Year && r.FechaMedicion.Month == dateInicio.Month && r.FechaMedicion.Day == dateInicio.Day).ToList();
                }
                else
                {
                    lista = db.dc.Vista_ResultadosArchivos.Where(r => r.TestName == prueba && r.rdcDeterminationLevel > 20 && r.FechaMedicion >= dateInicio && r.FechaMedicion <= dateFin).ToList();

                }

            }
            if (usarInicio && !usarFin)
            {
                lista = db.dc.Vista_ResultadosArchivos.Where(r => r.TestName == prueba && r.rdcDeterminationLevel > 20 && r.FechaMedicion >= dateInicio).ToList();
            }

            if (!usarInicio && usarFin)
            {
                lista = db.dc.Vista_ResultadosArchivos.Where(r => r.TestName == prueba && r.rdcDeterminationLevel > 20 && r.FechaMedicion <= dateFin).ToList();
            }

            if (soloPublicados)
            {
                lista = lista.Where(r => r.Publicado == true).ToList();
            }
            return lista.OrderBy(r => r.FechaMedicion).ToList();
        }

        #endregion

        #region ResultadoConsolidado

        public List<Vista_ResultadosArchivo> ObtenerResultadosConsolidados(List<string> listaPruebas, DateTime fechaConsolidado)
        {
            var lista = db.dc.Vista_ResultadosArchivos.Where(r => r.FechaMedicion.Year == fechaConsolidado.Year && r.FechaMedicion.Month == fechaConsolidado.Month && r.FechaMedicion.Day == fechaConsolidado.Day).ToList();
            lista = lista.Where(r => listaPruebas.Contains(r.TestName)).ToList();

            return lista.OrderBy(r => r.CodigoMuestra).ToList();
        }

        //public List<Resultado> ObtenerResultadosCodigoMuestra(string codigoMuestra,DateTime fechaResultado,List<string> listaPruebas)
        //{
        //    return db.dc.Resultados.Where(r => r.CodigoMuestra == codigoMuestra && r.Publicado == true && listaPruebas.Contains(r.TestName)).ToList();
        //}

        //public Resultado ObtenerResultadoTest(string codigoMuestra, DateTime fechaResultado, string prueba)
        //{
        //    //(r => r.FechaMedicion.Year == fechaConsolidado.Year && r.FechaMedicion.Month == fechaConsolidado.Month && r.FechaMedicion.Day == fechaConsolidado.Day
        //    return db.dc.Resultados.Where(r => r.CodigoMuestra == codigoMuestra && r.TestName == prueba && r.FechaMedicion.Year == fechaResultado.Year && r.FechaMedicion.Month == fechaResultado.Month && r.FechaMedicion.Day == fechaResultado.Day).FirstOrDefault();
        //}

        public Resultado ObtenerResultadoTest(string codigoMuestra, string codigoCorrelativo, string prueba)
        {
            return db.dc.Resultados.Where(r => (r.CodigoMuestra == codigoMuestra || r.CodigoMuestra == codigoCorrelativo) && r.TestName == prueba && r.Publicado == true && r.Estado == 1).FirstOrDefault();
        }
        /*
        public DataTable ObtenerCodigosMuestraMedidos(string listaPruebas,DateTime fechaConsolidado)
        {
            string query = string.Concat("SELECT CodigoMuestra FROM Resultado WHERE TestName IN ", listaPruebas, " AND DATEPART(YEAR,FechaMedicion) = ", fechaConsolidado.Year, " AND DATEPART(MONTH,FechaMedicion) = ", fechaConsolidado.Month, "AND DATEPART(DAY,FechaMedicion) = ", fechaConsolidado.Day, " GROUP BY CodigoMuestra");
            return db.QueryDataTable(query);
        }
        
        public DataTable ObtenerPacientes(string listaCodigos)
        {
            string query = string.Concat("SELECT * FROM Vista_BuscarPaciente WHERE CodigoInternoLab IN", listaCodigos);
            return db.QueryDataTable(query);
        }
        */
        #endregion

        #region InformeResultadosHistoricos

        public DataTable ListaResultadosHistoricosPaciente(int idNeonato, string prueba)
        {
            //var listaMuestra = db.dc.Muestras.Where(n => n.idNeonato == idNeonato).Select(n => n.CodigoInternoLab).ToList();


            //var listaResultados = db.dc.Resultados.Where(r => r.CodigoMuestra.Contains(listaMuestra) )
            //List<spResultadosHistoricosNeonatoResult> listaResultados =
            //    db.dc.spResultadosHistoricosNeonato(idNeonato,prueba).ToList();
            //return listaResultados;
            //db.dc.ExecuteQuery("spResultadosHistoricosNeonato", idNeonato);
            //string query = string.Concat("SELECT idResultado,CodigoMuestra,ConcTexto,ConcValor,ResultCode,Estado,AssayRunID,TestName,FechaMedicion,Unidad,Publicado,rdcDeterminationLevel",
            //                             " FROM Resultado WHERE CodigoMuestra in (SELECT CodigoInternoLab FROM Muestra WHERE idNeonato = ",idNeonato,") and TestName = '",prueba,"' ORDER BY FechaMedicion ASC");
            string query = string.Concat("SELECT idResultado,CodigoMuestra,NumMuestra,ConcTexto,ConcValor,ResultCode,TestName,FechaToma,FechaResultado,Unidad,Publicado,rdcDeterminationLevel",
                                         " FROM Vista_ResultadosMuestra WHERE CodigoMuestra in (SELECT CodigoInternoLab FROM Muestra WHERE idNeonato = ", idNeonato, ") and TestName = '", prueba, "' and Publicado = 1 ORDER BY FechaResultado ASC");
            return db.QueryDataTable(query);
        }

        #endregion

        #region ResultadosVerificacion

        public List<Resultado> ObtenerResultadosVerificacion(int inicial, int final)
        {
            return db.dc.Resultados.Where(r => r.CodigoCorrelativoInt >= inicial && r.CodigoCorrelativoInt <= final && r.Publicado == true).OrderBy(r => r.CodigoCorrelativoInt).ToList();
        }

        #endregion

        #region Publicacion
        public List<Vista_ResultadosGSP> ObtenerResultadosPublicacion(int ensayoId, string discriminador, double valor, int tipoResultado, string codigoCorrelativo, string publicado)
        {
            var lista = new List<Vista_ResultadosGSP>();
            if (publicado.CompareTo("2") == 0)
            {
                if (codigoCorrelativo.CompareTo(string.Empty) != 0)
                {
                    lista = db.dc.Vista_ResultadosGSPs.Where(r => r.idEnsayo == ensayoId && r.EstadoRango == 1 && r.CodigoMuestra == codigoCorrelativo).OrderBy(r => r.AssayLineNumber).ToList();
                }
                else
                {
                    lista = db.dc.Vista_ResultadosGSPs.Where(r => r.idEnsayo == ensayoId && r.EstadoRango == 1).OrderBy(r => r.AssayLineNumber).ToList();
                }


               
            }else
            {

                bool estadoPublicado = false;
                if (publicado.CompareTo("1") == 0) estadoPublicado = true;

                if (codigoCorrelativo.CompareTo(string.Empty) != 0)
                {
                    lista = db.dc.Vista_ResultadosGSPs.Where(r => r.idEnsayo == ensayoId && r.EstadoRango == 1 && r.CodigoMuestra == codigoCorrelativo && r.Publicado == estadoPublicado).OrderBy(r => r.AssayLineNumber).ToList();
                }
                else
                {
                    lista = db.dc.Vista_ResultadosGSPs.Where(r => r.idEnsayo == ensayoId && r.EstadoRango == 1 && r.Publicado == estadoPublicado).OrderBy(r => r.AssayLineNumber).ToList();
                }
            }

            if (tipoResultado == 1)
            {
                lista = lista.Where(r => r.rdcDeterminationLevel == 20).ToList();
            }
            if (tipoResultado == 2)
            {
                lista = lista.Where(r => r.rdcDeterminationLevel > 20).ToList();
            }
            if (discriminador != string.Empty)
            {
                if (discriminador.CompareTo(">") == 0)
                    lista = lista.Where(r => r.ConcValor > valor).OrderBy(r => r.AssayLineNumber).ToList();
                if (discriminador.CompareTo(">=") == 0)
                    lista = lista.Where(r => r.ConcValor >= valor).OrderBy(r => r.AssayLineNumber).ToList();
                if (discriminador.CompareTo("<") == 0)
                    lista = lista.Where(r => r.ConcValor < valor).OrderBy(r => r.AssayLineNumber).ToList();
                if (discriminador.CompareTo("<=") == 0)
                    lista = lista.Where(r => r.ConcValor <= valor).OrderBy(r => r.AssayLineNumber).ToList();
            }

            return lista;
        }

        //public void PublicarResultados(List<int> listaIdNoPublicados, int idEnsayo)
        //{
        //    da.PublicarTodosResultadosEnsayo(idEnsayo);
        //    da.MarcarNoPublicados(listaIdNoPublicados, idEnsayo);
        //}
        public bool PublicarTodosResultadosEnsayo(int idEnsayo, string usuario)
        {
            try
            {
                string query = string.Concat("UPDATE Resultado SET Publicado = 1, PublicadoPor = '", usuario,
                                             "', FechaPublicacion = '", DateTime.Now.ToString("MM/dd/yyyy"), "' WHERE idEnsayo = ", idEnsayo);
                db.ExecuteNoQuery(query);
                //return true; DateTime.Now.ToString("MM/dd/yyyy")
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
            return true;
        }

        public bool PublicarTodoDetalleEnsayo(int idEnsayo)
        {
            try
            {
                string query = string.Concat("UPDATE DetalleEnsayo SET Publicado = 1 WHERE idEnsayo = ", idEnsayo);
                db.ExecuteNoQuery(query);
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
            return true;
        }

        //public bool MarcarNoPublicados(List<string> listaCodigos, int idEnsayo)
        //{
        //    try
        //    {
        //        foreach (string codigo in listaCodigos)
        //        {
        //            db.ExecuteNoQuery(string.Concat("UPDATE Resultado SET Publicado = 0 WHERE idEnsayo = ",idEnsayo," AND CodigoMuestra = '", codigo, "'"));
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //        throw;
        //    }
        //}

        public bool MarcarNoPublicados(string listaCodigos, int idEnsayo)
        {
            try
            {
                db.ExecuteNoQuery(string.Concat("UPDATE Resultado SET Publicado = 0 WHERE idEnsayo = ", idEnsayo, "AND CodigoMuestra IN (", listaCodigos, ")"));
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool MarcarIdDetalleNoPublicados(string listaIdDetalleNoPublicados)
        {
            try
            {
                db.ExecuteNoQuery(string.Concat("UPDATE DetalleEnsayo SET Publicado = 0 WHERE idDetalleEnsayo IN (", listaIdDetalleNoPublicados, ")"));
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        #endregion

        public int ObtenerIdDetalleEnsayo(string codigoMuestra, string conc)
        {
            DetalleEnsayo detalle = db.dc.DetalleEnsayos.Where(d => d.SampleCodigo == codigoMuestra && d.ConcTexto == conc).First();
            return detalle.idDetalleEnsayo;
        }

        #region BuscarResultados
        public List<Vista_ResultadosGSP> BuscarResultadoPorPublicar(string codigoMuestra, int idEnsayo)
        {
            if ((codigoMuestra != null) && (codigoMuestra.CompareTo(string.Empty) != 0))
            {
                return db.dc.Vista_ResultadosGSPs.Where(r => r.CodigoCorrelativo == codigoMuestra).OrderBy(r => r.FechaResultado).ToList();
            }
            else
            {
                if (idEnsayo > 0)
                    return db.dc.Vista_ResultadosGSPs.Where(r => r.idEnsayo == idEnsayo).OrderBy(r => r.AssayLineNumber).ToList();
                else
                    return null;
            }
        }
        public List<Vista_ResultadosGSP> BuscarResultadoPorPublicar(string codigoMuestra, int idEnsayo, string dni, string apellidosMadre)
        {
            var lista = new List<Vista_ResultadosGSP>();

            if (idEnsayo > 0)
            {
                lista = db.dc.Vista_ResultadosGSPs.Where(r => r.idEnsayo == idEnsayo).OrderBy(r => r.AssayLineNumber).ToList();
            }else
            {
                if ((codigoMuestra != null) && (codigoMuestra.CompareTo(string.Empty) != 0))
                {
                    lista = db.dc.Vista_ResultadosGSPs.Where(r => r.CodigoCorrelativo == codigoMuestra).OrderBy(r => r.FechaResultado).ToList();
                }else
                {
                    if (dni != null && dni.CompareTo(string.Empty) != 0)
                    {
                        lista = db.dc.Vista_ResultadosGSPs.Where(r => r.DNI == dni).OrderBy(r => r.CodigoMuestra).ThenBy(r => r.FechaResultado).ToList();
                    }
                    else
                    {
                        if (apellidosMadre != null && apellidosMadre.CompareTo(string.Empty) != 0)
                            lista = db.dc.Vista_ResultadosGSPs.Where(r => r.ApellidosMadre.Contains(apellidosMadre)).ToList();
                    }
                }
            }

            return lista;
        }

        public List<Vista_ResultadosGSP> BuscarResultados(string codigoMuestra, int ensayoId)
        {
            if ((codigoMuestra != null) && (codigoMuestra.CompareTo(string.Empty) != 0))
            {
                return db.dc.Vista_ResultadosGSPs.Where(r => r.CodigoMuestra == codigoMuestra).OrderBy(r => r.AssayLineNumber).ToList();
            }
            else
            {
                if (ensayoId > 0)
                    return db.dc.Vista_ResultadosGSPs.Where(r => r.idEnsayo == ensayoId).OrderBy(r => r.AssayLineNumber).ToList();
                else
                    return null;
            }
        }

        #endregion

        public List<Vista_ResultadosGSP> ObtenerResultadosPublicados(int ensayoId)
        {
            return db.dc.Vista_ResultadosGSPs.Where(r => r.idEnsayo == ensayoId && r.Publicado == true && r.EstadoRango == 1).OrderBy(r => r.AssayLineNumber).ToList();
        }

        public List<Vista_ResultadosGSP> ObtenerResultadosPublicadosINMP(int ensayoId)
        {
            return db.dc.Vista_ResultadosGSPs.Where(r => r.idEnsayo == ensayoId && r.EstadoRango == 1).OrderBy(r => r.AssayLineNumber).ToList();
        }



        #region INMP
        //Nueva Interfaz
        public void RegistrarResultadosINMPNew(string resultadosEnviar)
        {
            if (resultadosEnviar.Length > 0)
            {
                string query = string.Concat("INSERT INTO [dbo].[LABO_INTER_WINSISTAMIZAJE] ([CORRELATIVO]",
                                                   ",[NRO_ORDEN]",
                                                   ",[DOCUMENTO]",
                                                   ",[COD_EXAMEN]",
                                                   ",[FECHA_RESULTADO]",
                                                   ",[CODIGO_ANALITO]",
                                                   ",[NOMBRE_ANALITO]",
                                                   ",[VALOR_ANALITO]",
                                                   ",[VALOR_REFERENCIAL]",
                                                   ",[REFERENCIA_MINIMA]",
                                                   ",[REFERENCIA_MAXIMA]",
                                                   ",[UNIDAD_RESULTADO]",
                                                   ",[USUARIO]",
                                                   ",[USUARIO_VALIDA]",
                                                   ",[FECHA_REGISTRA]",
                                                   ",[USUARIO_ANULA]",
                                                   ",[FECHA_ANULA]",
                                                   ",[IP]",
                                                   ",[MAC]",
                                                   ",[FECHA_ACTUALIZA]",
                                                   ",[USUARIO_ACTUALIZA]",
                                                   ",[ESTADO])",
                                             " VALUES ", resultadosEnviar);
                db.ExecuteNoQueryINMPNew(query);
            }
        }
        //Publicacion de resultados y muestras en la interfaz inicial del INMP
        public void RegistrarResultadosINMP(string resultadosEnviar)
        {
            if (resultadosEnviar.Length > 0)
            {
                string query = string.Concat("INSERT INTO [dbo].[Resultado_INMP] ([CodigoMuestra]",
                                             ",[Analito]",
                                             ",[Concentracion]",
                                             ",[Unidad]",
                                             ",[Resultado]",
                                             ",[Estado]",
                                             ",[ResultCode]",
                                             ",[Test]",
                                             ",[Rango]",
                                             ",[NumEnsayo]",
                                             ",[FechaResultado]",
                                             ",[Pocillo]",
                                             ",[Flag]",
                                             ",[isGSP]",
                                             ",[FechaRegistro]",
                                             ",[Fech_migra]",
	                                         ",[Empresa]",
	                                         ",[Estadreg])",
                                             " VALUES ", resultadosEnviar);
                db.ExecuteNoQueryINMP(query);
            }
        }

        public void RegistrarMuestrasINMP(string muestrasEnviar)
        {
            if (muestrasEnviar.Length > 0)
            {
                string query = string.Concat("INSERT INTO [dbo].[Muestra_INMP]",
                                  "([CodigoMuestra]",
                                  ",[NombreMadre]",
                                  ",[ApellidoMadre]",
                                  ",[Madre]",
                                  ",[Neonato]",
                                  ",[DNI]",
                                  ",[HistoriaClinica]",
                                  ",[FechaNacimiento]",
                                  ",[FechaToma]",
                                  ",[Estado]",
                                  ",[FechaRecepcion]",
                                  ",[CodigoEstablecimiento]",
                                  ",[TipoEstablecimiento]",
                                  ",[Establecimiento]",
                                  ",[EdadGestacional]",
                                  ",[Departamento]",
                                  ",[Provincia]",
                                  ",[Distrito]",
                                  ",[Telefono]",
                                  ",[Telefono2]",
                                  ",[Peso]",
                                  ",[Talla]",
                                  ",[Sexo]",
                                  ",[esPrematuro]",
                                  ",[esTransfundido]",
                                  ",[CodigoCorrelativo]",
                                  ",[Notas]",
                                  ",[NumRepeticion]",
                                  ",[FechaRegistro]",
                                  ",[TipoMigracion]) ",
                                  " VALUES ", muestrasEnviar);
                db.ExecuteNoQueryINMP(query);
            }
        }

        public void MarcarMuestrasExportadas(string listaIdMuestrasExportadas)
        {
            string query = string.Concat("UPDATE Muestra SET Importado = 1 WHERE idMuestra in ",
                                         listaIdMuestrasExportadas);
            db.ExecuteNoQuery(query);
        }

        public DataTable ObtenerResultadoINMP(string codigoMuestra, string conc, string analito,string numEnsayo)
        {
            string query = string.Concat("SELECT * FROM Resultado_INMP WHERE CodigoMuestra = '", codigoMuestra, "' AND Concentracion = '", conc, "' AND Analito= '", analito, "' AND NumEnsayo = ",numEnsayo);
            return db.QueryDataTableINMP(query);
        }
        public void ActualizarResultadoINMP(string codigoMuestra, string conc, string analito, string numEnsayo,string estadoReg)
        {
            string query = string.Concat("UPDATE Resultado_INMP SET Estadreg = '", estadoReg, "' WHERE CodigoMuestra = '", codigoMuestra, "' AND Concentracion = '", conc, "' AND Analito= '", analito, "' AND NumEnsayo = ", numEnsayo);
            db.ExecuteNoQueryINMP(query);
        }
        
        public Vista_ResultadosGSP ObtenerResultadoGSPidDetalleEnsayo(int idDetalleEnsayo)
        {
            return db.dc.Vista_ResultadosGSPs.Where(d => d.idDetalleEnsayo == idDetalleEnsayo).First();
        }

        #endregion
    }
}
