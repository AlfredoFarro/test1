using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BE;
using System;

namespace DA
{
    public class AccesoDB
    {
        public string conexionTamiLife { get; set; }
        public string conexionSeguridad { get; set; }
        public string conexionINMP { get; set; }
        public string conexionINMPNew { get; set; }
        public TamizajeDBDataContext dc { get; set; }

        public AccesoDB()
        {

            conexionTamiLife = ConfigurationManager.ConnectionStrings["TamiLifeSA"].ConnectionString;
            //conexionSG = ConfigurationManager.ConnectionStrings["WIN32SQLServerExpress.LifeCycleConnectionString"].ConnectionString;
            conexionSeguridad = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            conexionINMP = ConfigurationManager.ConnectionStrings["INMP"].ConnectionString;
            conexionINMPNew = ConfigurationManager.ConnectionStrings["INMPNew"].ConnectionString;
            dc = new TamizajeDBDataContext(conexionTamiLife);
            
        }

        public DataTable Query_DataTable_Seguridad(string query)
        {
            var dt = new DataTable();
            var cn = new SqlConnection(conexionSeguridad);

            cn.Open();

            var adapter = new SqlDataAdapter(query, cn);

            adapter.Fill(dt);
            cn.Close();
            return dt;
        }

        public void ActualizarRegistros()
        {
            try
            {
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable QueryDataTable(string query)
        {
            SqlConnection cn = null;
            try
            {
                var dt = new DataTable();
                cn = new SqlConnection(conexionTamiLife);

                cn.Open();

                var adapter = new SqlDataAdapter(query, cn);

                adapter.Fill(dt);
                cn.Close();
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cn != null && cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                }
            }
        }
        public bool ExecuteNoQuery(string query)
        {
            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(conexionTamiLife);
                cn.Open();
                SqlCommand cm = new SqlCommand(query, cn);
                cm.ExecuteNonQuery();
                cn.Close();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cn != null && cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                }
            }

        }
        #region INMP
        //Interfaz Nueva
        public bool ExecuteNoQueryINMPNew(string query)
        {
            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(conexionINMPNew);
                cn.Open();
                SqlCommand cm = new SqlCommand(query, cn);
                cm.ExecuteNonQuery();
                cn.Close();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cn != null && cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                }
            }

        }

        public DataTable QueryDataTableINMPNew(string query)
        {
            SqlConnection cn = null;
            try
            {
                var dt = new DataTable();
                cn = new SqlConnection(conexionINMPNew);

                cn.Open();

                var adapter = new SqlDataAdapter(query, cn);

                adapter.Fill(dt);
                cn.Close();
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cn != null && cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                }
            }
        }

        //Interfaz Original
        public bool ExecuteNoQueryINMP(string query)
        {
            SqlConnection cn = null;

            try
            {
                cn = new SqlConnection(conexionINMP);
                cn.Open();
                SqlCommand cm = new SqlCommand(query, cn);
                cm.ExecuteNonQuery();
                cn.Close();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cn != null && cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                }
            }

        }

        public DataTable QueryDataTableINMP(string query)
        {
            SqlConnection cn = null;
            try
            {
                var dt = new DataTable();
                cn = new SqlConnection(conexionINMP);

                cn.Open();

                var adapter = new SqlDataAdapter(query, cn);

                adapter.Fill(dt);
                cn.Close();
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cn != null && cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                }
            }
        }

        #endregion
    }
}