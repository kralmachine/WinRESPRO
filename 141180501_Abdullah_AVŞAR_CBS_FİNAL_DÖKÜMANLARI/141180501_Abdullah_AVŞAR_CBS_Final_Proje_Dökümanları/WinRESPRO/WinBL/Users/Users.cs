using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinDAL;
using WinEntity;

namespace WinBL.Users
{

    public class Users
    {
        WinDAL.DataBase.DataBase DB = new WinDAL.DataBase.DataBase();
        WinEntity.Users.UserEntity UserEntity = new WinEntity.Users.UserEntity();
        int index = 0;
        object getValue = "";
        public int UserLogin(WinEntity.Users.UserEntity UserEntity) 
        {
            index=DB.UserLogin("select * from USERS where USERNAME='"+UserEntity.Username+"' AND PASSWORD='"+UserEntity.Password+"'");
            return index;
        }

        public int UserAdd(WinEntity.Users.UserEntity UserEntity) 
        {
            index = DB.cmd("INSERT INTO USERS (USERNAME,PASSWORD,EMAIL,AUTHORITYID) VALUES ('" + UserEntity.Username + "','" + UserEntity.Password + "','" + UserEntity.UserEmail + "',0)");
            getValue = DB.cmdSingle("select top 1 ID from USERS order by ID desc");
            if (index > 0)
            {
                index = DB.cmd("INSERT INTO AUTHORITY (USER_ID,FILE_OPERATIONS,STATIC_OPERATIONS,GRAPH_OPERATIONS,USER_OPERATIONS,NEW_WINDY_POWER,EMAIL_OPERATIONS,SYSTEM_FILES) VALUES (" + getValue + ",0,0,0,0,0,0,0)");
                getValue = DB.cmdSingle("select top 1 ID from AUTHORITY order by ID desc");
                index = DB.cmd("UPDATE USERS SET AUTHORITYID=" + getValue + " where EMAIL='" + UserEntity.UserEmail + "'");
            }
            return index;
        }

        public DataTable getUserList() 
        {
            return DB.getTable("select * from USERS");
        }

        public int UpdateAuthority(int[] authorities,string username) 
        {
            index = DB.cmd("UPDATE AUTHORITY SET FILE_OPERATIONS=" + authorities[0] + ",STATIC_OPERATIONS=" + authorities[1] + ",GRAPH_OPERATIONS=" + authorities[2] + ",USER_OPERATIONS=" + authorities[3] + ",NEWS_WINDY_POWER="+authorities[4]+",EMAIL_OPERATIONS=" + authorities[5] + ",SYSTEM_FILES=" + authorities[6] + " WHERE USER_ID="+Convert.ToInt16(getUserID("select ID from USERS where USERNAME='" + username + "'")));
            return index;
        }

        public int getUserID(string sql) 
        {
            getValue = DB.cmdSingle(sql);
            return Convert.ToInt16(getValue);
        }

        public int getUserID(WinEntity.Users.UserEntity UserEntity) 
        {
            getValue = DB.cmdSingle("select * from USERS where USERNAME='" + UserEntity.Username.Trim() + "' AND PASSWORD='" + UserEntity.Password.Trim() + "'");
            return Convert.ToInt16(getValue);
        }

        public DataRow getAuthority(WinEntity.Users.UserEntity UserEntity) 
        {
            return DB.getRowTable("select * from AUTHORITY WHERE USER_ID="+UserEntity.UserID);
        }


    }
}
