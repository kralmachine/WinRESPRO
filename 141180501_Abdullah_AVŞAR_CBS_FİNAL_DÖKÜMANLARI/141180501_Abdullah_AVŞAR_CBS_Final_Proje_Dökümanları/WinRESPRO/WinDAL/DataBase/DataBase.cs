using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapInfo;
using System.Windows.Forms;

namespace WinDAL.DataBase
{
    public class DataBase:IDataBase
    {
        
        MapInfoApplication mi = null;
        public System.Data.SqlClient.SqlConnection sqlConnection()
        {
            SqlConnection conn = new SqlConnection("Data Source=KRALMACHINE;initial Catalog=WINRESPRO;uid=sa;password=1234");
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
                SqlConnection.ClearAllPools();
            }
            return conn;
        }

        public int cmd(string sqlQuery)
        {
            int index = 0;
            SqlCommand cmd = this.sqlConnection().CreateCommand();
            cmd.CommandText = sqlQuery;
            cmd.CommandType = System.Data.CommandType.Text;
            try
            {
                index = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " Hata :" + sqlQuery);
            }
            finally
            {
                cmd.Dispose();
                this.sqlConnection().Close();
            }
            return index;
        }

        public object cmdSingle(string sqlQuery)
        {
            object index = 0;
            SqlCommand cmd = this.sqlConnection().CreateCommand();
            cmd.CommandText = sqlQuery;
            cmd.CommandType = System.Data.CommandType.Text;
            try
            {
                index = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " Hata :" + sqlQuery);
            }
            finally
            {
                cmd.Dispose();
                this.sqlConnection().Close();
            }
            return index;
        }

        public System.Data.DataTable getTable(string sqlQuery)
        {
            DataTable getTable = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(sqlQuery, this.sqlConnection());
            try
            {
                adp.Fill(getTable);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " Hata :" + sqlQuery);
            }
            finally
            {
                adp.Dispose();
                this.sqlConnection().Close();
                getTable.Dispose();
            }
            return getTable;
        }

        public System.Data.DataRow getRowTable(string sqlQuery)
        {
            DataTable getDataRow = getTable(sqlQuery);
            if (getDataRow.Rows.Count == 0)
                return null;
            else
                return getDataRow.Rows[0];
        }

        public string getRowSingle(string sqlQuery)
        {
            DataTable getsingleRow = getTable(sqlQuery);
            if (getsingleRow.Rows.Count == 0)
                return null;
            else
                return getsingleRow.Rows[0][0].ToString();
        }

        public int UserLogin(string sqlQuery) 
        {
            SqlDataReader dr;
            SqlConnection con = this.sqlConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sqlQuery;
            dr = cmd.ExecuteReader();
            if (dr.Read())
                return 1;
            else
                return -1;
        }

        public MapInfo.MapInfoApplication connectMapInfo(string path,string panel)
        {
            if (mi == null) {
                mi = new MapInfoApplication();
                int p = Convert.ToInt32(panel);
                mi.Do("set next document parent " + p.ToString() + " style 1");
                mi.Do("set application window " + p.ToString());
                mi.Do("run application \"" + AppDomain.CurrentDomain.BaseDirectory + "/WinRESPRO_v1.wor" + "\"");
            }

            return mi;
        }

        public int miDo(string mido,string panel)
        {
            MapInfoApplication mi = connectMapInfo("", panel);
            mi.Do(mido);
            if (mi == null)
                return 0;
            else return 1;
        }

        public string miEval(string mieval,string panel)
        {
            string getOperations = null;
            MapInfoApplication mi = connectMapInfo("", panel);
            getOperations=mi.Eval(mieval);
            return getOperations;
        }

        public int miState(string panel) 
        {
            MapInfoApplication mi = connectMapInfo("", panel);
            if (mi == null)
                return 0;
            else return 1;
        }


        void IDataBase.miDo(string mido, string panel)
        {
            throw new NotImplementedException();
        }
    }
}
