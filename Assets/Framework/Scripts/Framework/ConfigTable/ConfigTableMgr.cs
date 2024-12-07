using cfg;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigTableMgr : Singleton<ConfigTableMgr>
{
    private Tables _table;
    public void InitTable()
    {
        //string gameConfDir = Application.streamingAssetsPath + "/GenerateDatas/json";
        string gameConfDir = Application.dataPath + "/Res/ConfigTables/json";

        _table = new cfg.Tables(file => JSON.Parse(File.ReadAllText($"{gameConfDir}/{file}.json")));

    }

    public Dictionary<int, cfg.buff.Buff> GetBuffDic()
    {
        return _table.TbBuff.DataMap;
    }
    public Dictionary<int, cfg.card.Cards> GetCardDic()
    {
        return _table.TbCards.DataMap;
    }
}
