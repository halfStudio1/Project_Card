using System.Collections.Generic;
using UnityEngine;
public class UIEditorConfig
{
    //命名前缀为这样才能生成脚本
    public static List<string> defaultName = new List<string>()
    {
        //Button
        "Btn",
        //Text
        "Txt",
        //TextMeshPro
        "Tmp",
        //Toggle
        "Tog",
        //Slider
        "Sld",
        //Image
        "Img",
        //InputField
        "Ipf",
        //Dropdown
        "Dpd",
        //ScrollRect
        "Scr",
    };

    public static string folderPath = Application.dataPath + "/Scripts/GameMain/UI/Panels/"; // 指定保存路径
    public static string UITemplatePath = "Assets/Framework/Editor/UI/UITemplate.txt";
}
