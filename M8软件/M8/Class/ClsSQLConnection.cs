using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace M8.Class
{
    //public delegate void SqlConnectionStateChangeEventHandler(StateChangeEventArgs e);

    public class ClsSQLConnection
    {
        public SqlConnection sqlConn;
        private SqlDataAdapter sqlAdapter;
        public string strConn = null;
        private SqlCommandBuilder sqlBuilder;
        public static StringBuilder strBuilder;
        public string sqlIP = null;
        public int sqlPort = 0;
        public bool IsConnected;
        public event SqlConnectionStateChangeEventHandler SqlConnectionStateChange;
        private static event ReturnObjectUserParaEventHandler EventReturnObjectUserPara;


        public bool Connection()
        {
            try
            {
                strConn = "Data Source=" + sqlIP + "," + sqlPort.ToString() + ";Initial Catalog=M8;User ID=sa;Password=sql0214";
                sqlConn = new SqlConnection(strConn);
                if (sqlConn.State != ConnectionState.Open)
                    sqlConn.Open();
                IsConnected = true;
                return true;

            }
            catch (SqlException ex)
            {
                string strErrMessage = "";
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    strErrMessage = strErrMessage + "Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n";
                }
                IsConnected = false;
                throw new Exception(strErrMessage);
            }
            finally
            {
                if (sqlConn.State != ConnectionState.Closed)
                    sqlConn.Close();
            }
        }

        public void CloseConnection()
        {
            if (sqlConn.State == ConnectionState.Open)
                sqlConn.Close();
        }

        public DataTable QueryDatabase(string strCmdText)
        {
            DataTable dt = new DataTable();
            try
            {
                //DataGridView datagridview = new DataGridView();
                sqlConn = new SqlConnection(strConn);
                sqlAdapter = new SqlDataAdapter();
                sqlBuilder = new SqlCommandBuilder(sqlAdapter);
                sqlAdapter.SelectCommand = new SqlCommand(strCmdText, sqlConn);
                sqlAdapter.Fill(dt);
                //datagridview.DataSource = dt;
                //datagridview.DataMember = "PROJECT";
                return dt;
            }
            catch (SqlException ex)
            {
                string strErrMessage = "";
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    strErrMessage = strErrMessage + "Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n";
                }
                throw new Exception(strErrMessage);
            }
            finally
            {
                if (sqlConn.State != ConnectionState.Closed)
                    sqlConn.Close();
            }
        }

        public object ReturnObjectUsePara(string strCmdText, List<string> listNames, List<SqlDbType> listDBTypes, List<object> listValues)
        {
            try
            {
                DataTable dt = new DataTable();
                sqlConn = new SqlConnection(strConn);
                if (sqlConn.State != ConnectionState.Open)
                    sqlConn.Open();
                SqlCommand sqlCommand = new SqlCommand(strCmdText, sqlConn);
                for (int i = 0; i < listNames.Count; i++)
                {
                    sqlCommand.Parameters.Add(listNames[i], listDBTypes[i]).Value = listValues[i];
                }
                return sqlCommand.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                string strErrMessage = "";
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    strErrMessage = strErrMessage + "Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n";
                }
                throw new Exception(strErrMessage);
            }
            finally
            {
                if (sqlConn.State != ConnectionState.Closed)
                    sqlConn.Close();
            }
        }

        public DataTable QueryDataBaseUsePara(string strCmdText, List<string> listNames, List<SqlDbType> listDBTypes, List<object> listValues)
        {
            try
            {
                DataTable dt = new DataTable();
                sqlConn = new SqlConnection(strConn);
                SqlCommand sqlCommand = new SqlCommand(strCmdText, sqlConn);
                for (int i = 0; i < listNames.Count; i++)
                {
                    sqlCommand.Parameters.Add(listNames[i], listDBTypes[i]).Value = listValues[i];
                }
                sqlAdapter = new SqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dt);
                return dt;

            }
            catch (SqlException ex)
            {
                string strErrMessage = "";
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    strErrMessage = strErrMessage + "Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n";
                }
                throw new Exception(strErrMessage);
            }
            finally
            {
                if (sqlConn.State != ConnectionState.Closed)
                    sqlConn.Close();
            }
        }

        private void sqlConn_StateChange(object sender, StateChangeEventArgs e)
        {
            SqlConnectionStateChange(e);
        }

        public int UpdateTableUsePara(string strCmdText, List<string> listNames, List<SqlDbType> listDBTypes, List<object> listValues)
        {
            try
            {
                DataTable dt = new DataTable();
                sqlConn = new SqlConnection(strConn);
                if (sqlConn.State != ConnectionState.Open)
                    sqlConn.Open();
                SqlCommand sqlCommand = new SqlCommand(strCmdText, sqlConn);
                //SqlParameter ProjectID=new SqlParameter("@intProjectID",
                for (int i = 0; i < listNames.Count; i++)
                {
                    sqlCommand.Parameters.Add(listNames[i], listDBTypes[i]).Value = listValues[i];
                }
                return sqlCommand.ExecuteNonQuery();//执行SQL语句；
            }
            catch (SqlException ex)
            {

                string strErrMessage = "";
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    strErrMessage = strErrMessage + "Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n";
                }
                throw new Exception(strErrMessage);
            }
            finally
            {
                if (sqlConn.State != ConnectionState.Closed)
                    sqlConn.Close();
            }
        }     
    }
}
