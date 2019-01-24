using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBL.Settings
{
    
    public class Settings
    {
        WinEntity.Settings.Settings setting = new WinEntity.Settings.Settings();
        WinDAL.DataBase.DataBase DB = new WinDAL.DataBase.DataBase();
        int index = 0;

        public int updateSetting(WinEntity.Settings.Settings setting) 
        {
            index = DB.cmd("UPDATE SETTINGS SET MAINTEXT='"+setting.MAINTEXT+"',TEXT1='"+setting.TEXT1+"',TEXT2='"+setting.TEXT2+"',TEXT3='"+setting.TEXT3+"',TEXT4='"+setting.TEXT4+"' where ID=1");
            return index;
        }

        public DataRow getSingleRow() 
        {
            return DB.getRowTable("select * from SETTINGS where ID=1");
        }

        public int BackUp() 
        {
            DateTime d = DateTime.Now;
            string dd = d.Millisecond+"_"+d.Day + "_" + d.Month;
            string dbname = "WINRESPRO";

            string sqlQuery1 = "USE " + dbname + ";";
            string sqlQuery2 = "BACKUP DATABASE " + dbname + " TO DISK='D:\\WinRESPRO_BackUps\\" + dbname + "_" + dd + ".Bak'";

            index=DB.cmd(sqlQuery1);
            index=DB.cmd(sqlQuery2);

            return index;
           
        }
    }
}
