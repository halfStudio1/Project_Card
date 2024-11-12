using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPanelScriptGenerator : EditorWindow
{
    private string panelScript;
    private string panelName;

    //ScrollView的Pos信息
    private Vector2 nowPos;

    //记录控件的名字和类型，防止重复
    private Dictionary<string, Type> controlTypeDic = new Dictionary<string, Type>();

    [MenuItem("GameObject/UI/自动生成Panel脚本")]
    private static void CreateToolPanel()
    {
        UIPanelScriptGenerator win = EditorWindow.GetWindow<UIPanelScriptGenerator>("自动生成Panel脚本工具");
        win.Show();
        win.InitInfo();
    }

    /// <summary>
    /// 初始化面板显示的信息
    /// </summary>
    public void InitInfo()
    {
        //清空信息
        panelScript = "";
        controlTypeDic.Clear();
        nowPos = Vector2.zero;

        //读取当前选择的panel
        GameObject panel = Selection.activeGameObject;
        if (panel == null)
            return;

        panelName = panel.name;

        #region 获取所有的控件

        ControlStrInfo strInfo = new ControlStrInfo();

        ControlStrInfo controlInfo = FindControl<Button>(panel);
        if (controlInfo == null)
            return;
        strInfo += controlInfo;

        controlInfo = FindControl<Toggle>(panel);
        if (controlInfo == null)
            return;
        strInfo += controlInfo;

        controlInfo = FindControl<Slider>(panel);
        if (controlInfo == null)
            return;
        strInfo += controlInfo;

        controlInfo = FindControl<InputField>(panel);
        if (controlInfo == null)
            return;
        strInfo += controlInfo;

        controlInfo = FindControl<Dropdown>(panel);
        if (controlInfo == null)
            return;
        strInfo += controlInfo;

        controlInfo = FindControl<ScrollRect>(panel);
        if (controlInfo == null)
            return;
        strInfo += controlInfo;

        controlInfo = FindControl<Image>(panel);
        if (controlInfo == null)
            return;
        strInfo += controlInfo;

        controlInfo = FindControl<Text>(panel);
        if (controlInfo == null)
            return;
        strInfo += controlInfo;

        controlInfo = FindControl<TextMeshPro>(panel);
        if (controlInfo == null)
            return;
        strInfo += controlInfo;

        #endregion

        //读取UI模板
        TextAsset baseStr = AssetDatabase.LoadAssetAtPath<TextAsset>(UIEditorConfig.UITemplatePath);
        //将生成的字符串写到模板中
        panelScript = string.Format(baseStr.text,
                                     panel.name,
                                     strInfo.nameStr,
                                     strInfo.listenerStr,
                                     strInfo.funcStr);
    }

    private ControlStrInfo FindControl<T>(GameObject obj) where T : UIBehaviour
    {
        ControlStrInfo info = new ControlStrInfo();
        T[] controls = obj.GetComponentsInChildren<T>();
        Type type = typeof(T);
        for (int i = 0; i < controls.Length; i++)
        {
            //跳过不需要生成代码的控件
            if (!controls[i].name.Contains("_"))
                continue;
            if (!UIEditorConfig.defaultName.Contains(controls[i].name.Substring(0, controls[i].name.IndexOf("_"))))
                continue;

            if (controlTypeDic.ContainsKey(controls[i].name))
            {
                //同名对象，控件类型也相同
                if (controlTypeDic[controls[i].name] == type)
                {
                    EditorUtility.DisplayDialog("重复控件名", $"有两个控件类型相同的对象重名了：{controls[i].name}", "确定");
                    return null;
                }
                //同名对象，控件类型不同
                else
                {
                    continue;
                }
            }

            controlTypeDic.Add(controls[i].name, type);

            info.nameStr += $"    public {type.Name} {controls[i].gameObject.name};\r\n";
            switch (type.Name)
            {
                case "Button":
                    info.listenerStr += $"        {controls[i].gameObject.name}.onClick.AddListener(On{controls[i].gameObject.name}Click);\r\n";
                    info.funcStr += $"    private void On{controls[i].gameObject.name}Click(){{}}\r\n";
                    break;
                case "Toggle":
                    info.listenerStr += $"        {controls[i].gameObject.name}.onValueChanged.AddListener(On{controls[i].gameObject.name}ValueChanged);\r\n";
                    info.funcStr += $"    private void On{controls[i].gameObject.name}ValueChanged(bool value){{}}\r\n";
                    break;
                case "Slider":
                    info.listenerStr += $"        {controls[i].gameObject.name}.onValueChanged.AddListener(On{controls[i].gameObject.name}ValueChanged);\r\n";
                    info.funcStr += $"    private void On{controls[i].gameObject.name}ValueChanged(float value){{}}\r\n";
                    break;
                default:
                    break;
            }
        }
        return info;
    }

    private void OnGUI()
    {
        if (panelScript != "")
        {
            nowPos = EditorGUILayout.BeginScrollView(nowPos);
            GUILayout.Label(panelScript);
            EditorGUILayout.EndScrollView();


            #region 两个保存按钮
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("选择保存路径", GUILayout.Height(40)))
            {
                string path = EditorUtility.SaveFilePanel("脚本保存路径", UIEditorConfig.folderPath, panelName, "cs");
                if (path != "")
                {
                    if (!File.Exists(path))
                    {
                        File.WriteAllText(path, panelScript);

                        EditorPrefs.SetBool("GeneratUIScript", true);
                        EditorPrefs.SetString("GeneratePanelName", panelName);
                        AssetDatabase.Refresh();
                    }
                }
            }
            if (GUILayout.Button("保存到默认路径", GUILayout.Height(40)))
            {
                string path = UIEditorConfig.folderPath + panelName + ".cs";
                if (!File.Exists(path))
                {
                    File.WriteAllText(path, panelScript);
                    EditorPrefs.SetBool("GeneratUIScript", true);
                    EditorPrefs.SetString("GeneratePanelName", panelName);
                    AssetDatabase.Refresh();
                }
            }

            GUILayout.EndHorizontal();
            #endregion


        }
        else
        {
            GUILayout.Label("存在同名同类型控件！");
        }
    }

    //生成脚本编译后，自动附加
    [UnityEditor.Callbacks.DidReloadScripts]
    public static void AddScript2Panel()
    {
        if (!EditorPrefs.GetBool("GeneratUIScript"))
            return;
        EditorPrefs.SetBool("GeneratUIScript", false);

        string panelName = EditorPrefs.GetString("GeneratePanelName");
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var cSharpAssembly = assemblies.First(assembly => assembly.GetName().Name == "Assembly-CSharp");
        BasePanel panel = (BasePanel)Selection.activeGameObject.AddComponent(cSharpAssembly.GetType(panelName));
        BasePanelEditor.AutoBind(panel);

        Debug.Log("生成脚本成功，并已自动附加到Panel上了");
        EditorPrefs.DeleteKey("GeneratePanelName");

        //关闭面板
        UIPanelScriptGenerator win = EditorWindow.GetWindow<UIPanelScriptGenerator>();
        win.Close();
    }
}
