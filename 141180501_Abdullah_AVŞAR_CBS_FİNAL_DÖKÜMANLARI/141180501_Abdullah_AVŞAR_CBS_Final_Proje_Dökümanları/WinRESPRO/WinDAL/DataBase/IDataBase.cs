using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapInfo;
using System.Windows.Forms;

namespace WinDAL.DataBase
{
    interface IDataBase
    {
        System.Data.SqlClient.SqlConnection sqlConnection();
        int cmd(string sqlQuery);
        object cmdSingle(string sqlQuery);
        System.Data.DataTable getTable(string sqlQuery);
        System.Data.DataRow getRowTable(string sqlQuery);
        string getRowSingle(string sqlQuery);

        MapInfoApplication connectMapInfo(string path,string panel);
        void miDo(string mido,string panel);
        string miEval(string mieval,string panel);
    }
}
