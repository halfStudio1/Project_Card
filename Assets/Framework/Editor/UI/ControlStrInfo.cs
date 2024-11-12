/// <summary>
/// 控件动态生成代码
/// </summary>
public class ControlStrInfo
{
    //声明
    public string nameStr;
    //事件监听
    public string listenerStr;
    //事件监听响应
    public string funcStr;
    //多个控件拼接
    public static ControlStrInfo operator + (ControlStrInfo one, ControlStrInfo two)
    {
        one.nameStr += two.nameStr;
        one.listenerStr += two.listenerStr;
        one.funcStr += two.funcStr;
        return one;
    }
}
