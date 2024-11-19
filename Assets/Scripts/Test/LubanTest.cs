using Luban;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Build.Pipeline;
using UnityEngine;

public class LubanTest : MonoBehaviour
{
    private void InitTable()
    {
        string gameConfDir = "Assets/Scripts/Test/";
         var tables = new cfg.Tables(file => JSON.Parse(File.ReadAllText($"{gameConfDir}/{file}.json")));
    }
}
