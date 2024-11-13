using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigTableMgr : Singleton<ConfigTableMgr>
{

    public void InitTable()
    {
        string gameConfDir = Application.streamingAssetsPath + "/GenerateDatas/json";

        var tables = new cfg.Tables(file => JSON.Parse(File.ReadAllText($"{gameConfDir}/{file}.json")));

    }
}
