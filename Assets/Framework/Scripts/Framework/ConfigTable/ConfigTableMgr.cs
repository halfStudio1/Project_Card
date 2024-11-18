using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigTableMgr : Singleton<ConfigTableMgr>
{
    cfg.Tables tables;
    public void InitTable()
    {
        string gameConfDir = Application.streamingAssetsPath + "/GenerateDatas/json";

        tables = new cfg.Tables(file => JSON.Parse(File.ReadAllText($"{gameConfDir}/{file}.json")));

        Debugger.LogPink("table初始化成功");
    }

    public Dictionary<int, cfg.card.Cards> GetCardDic()
    {
        return tables.TbCards.DataMap;
    }
}
