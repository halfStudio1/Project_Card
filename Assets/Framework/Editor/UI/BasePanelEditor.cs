using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BasePanel),true)]
public class BasePanelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("自动绑定"))
        {
            BasePanel panel = (BasePanel)target;
            AutoBind(panel);
        }

        base.OnInspectorGUI();
    }

    /// <summary>
    /// 找到panel下的控件并绑定
    /// </summary>
    /// <param name="panel"></param>
    public static void AutoBind(BasePanel panel)
    {
        //获取脚本里的所有
        var fields = panel.GetType().GetFields();

        foreach (var field in fields)
        {
            //跳过不需要绑定的控件
            if (!field.Name.Contains("_"))
                continue;
            if (!UIEditorConfig.defaultName.Contains(field.Name.Substring(0, field.Name.IndexOf("_"))))
                continue;

            BindField(panel, field);
        }
    }
    private static void BindField(BasePanel panel, System.Reflection.FieldInfo field)
    {
        var component = FindChildRecursive(panel.transform, field.Name).GetComponent(field.FieldType);
        field.SetValue(panel, component);
    }

    //查找子物体
    public static Transform FindChildRecursive(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
            {
                return child;
            }
            Transform result = FindChildRecursive(child, childName);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }


}
