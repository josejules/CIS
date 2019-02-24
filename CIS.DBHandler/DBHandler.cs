using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace CIS.DBHandler
{
    public class DBHandler
    {
        private MySqlConnection conn = new MySqlConnection();
        MySqlTransaction trans = null;
        bool hasTransaction = false;

        public DBHandler()
        {
            //conn.ConnectionString = ConfigurationManager.ConnectionStrings["login"].ConnectionString;
        }
        public void Openconnection()
        {
            if (conn.State != ConnectionState.Open)
            {
                string connectionString = Convert.ToString(ConfigurationSettings.AppSettings["connectionString"]);

                //conn.ConnectionString = @"server=localhost;database=cis;uid=root;pwd=root;pooling=false;";
                conn.ConnectionString = @connectionString;
                conn.Open();
            }
        }
        public void CloseConnection()
        {
            if (conn.State != ConnectionState.Closed && !hasTransaction)
            {
                conn.Close();
            }
        }
        public string ExecuteQuery(string query)
        {
            string squery = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                Openconnection();
                cmd.Connection = conn;
                cmd.CommandText = query;
                object obj = cmd.ExecuteScalar();
                if (obj != null)
                {
                    squery = obj.ToString();
                }
                return squery;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "<br>" + query);
            }
            finally
            {
                CloseConnection();
            }
        }

        public string ExecuteTransactionQuery(string query)
        {
            string squery = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                //Openconnection();
                cmd.Connection = conn;
                cmd.CommandText = query;
                object obj = cmd.ExecuteScalar();
                if (obj != null)
                {
                    squery = obj.ToString();
                }
                return squery;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "<br>" + query);
            }
            finally
            {
                //CloseConnection();
            }
        }

        public void commitTransaction(TransactionType transType)
        {
            try
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    if (transType == TransactionType.Commit)
                    {
                        trans.Commit();
                    }
                    else if (transType == TransactionType.Rollback)
                    {
                        trans.Rollback();
                    }
                    hasTransaction = false;
                }
            }
            catch (Exception ex)
            {
                //trans.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public int ExecuteCommand(string squery)
        {
            try
            {
                Openconnection();
                MySqlCommand mcmd = new MySqlCommand(squery, conn);
                mcmd.CommandType = CommandType.Text;
                int value = mcmd.ExecuteNonQuery();
                return value;
            }
            catch (Exception ex)
            {
                hasTransaction = false;
                commitTransaction(TransactionType.Rollback);
                throw new Exception(ex.Message + "<BR>" + squery);
            }
            finally
            {
                if (!hasTransaction)
                    CloseConnection();
            }
        }

        public int ExecuteCommandTransaction(string squery)
        {
            try
            {
                Openconnection();
                hasTransaction = true;
                trans = conn.BeginTransaction(); //Transaction Begin
                MySqlCommand mcmd = new MySqlCommand(squery, conn);
                mcmd.CommandType = CommandType.Text;
                mcmd.Transaction = trans;
                int value = mcmd.ExecuteNonQuery();
                return value;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message + "<BR>" + squery);
            }
        }

        public DataTable ExecuteTable(string squery)
        {
            DataSet ds = new DataSet();
            try
            {
                Openconnection();
                MySqlCommand cmd = new MySqlCommand(squery, conn);
                cmd.CommandType = CommandType.Text;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<br>" + squery);
            }
            finally
            {
                CloseConnection();
            }
            return ds.Tables[0];
        }

        public object ExecuteScalar(string squery)
        {
            try
            {
                Openconnection();
                MySqlCommand mcmd = new MySqlCommand(squery, conn);
                mcmd.CommandType = CommandType.Text;
                return mcmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<BR>" + squery);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
