using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapInfo;

namespace WinBL.LoadMap
{
    public class LoadMapOperation
    {
        WinDAL.DataBase.DataBase DBI = new WinDAL.DataBase.DataBase();

        public int LoadMap(WinEntity.LoadMap.LoadMapEntity LoadMapEntity) 
        {
            MapInfoApplication mi=DBI.connectMapInfo("", LoadMapEntity.panelID);
            if (mi == null)
                return 0;
            else
                return 1;
        }

        public int miStateExecute(WinEntity.LoadMap.LoadMapEntity LoadMapEntity) 
        {
            int i = DBI.miState(LoadMapEntity.panelID);
            if (i>0)
                return i;
            else return i;
        }

        public int miDoExecute(WinEntity.LoadMap.LoadMapEntity LoadMapEntity)
        {
            int i=DBI.miDo(LoadMapEntity.miDo, LoadMapEntity.panelID);
            return i;
        }

        public string miEvalExecute(WinEntity.LoadMap.LoadMapEntity LoadMapEntity)
        {
            string getEvalValues = DBI.miEval(LoadMapEntity.miEval, LoadMapEntity.panelID);
            if (getEvalValues.Length>0)
                return getEvalValues;
            else return " ";
        }

    }
}
