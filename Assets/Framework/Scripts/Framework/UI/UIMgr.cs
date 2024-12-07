using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Canvas层级
/// </summary>
public enum E_CanvasType
{
    Bottom,
    Middle,
    Top,
    System
}

/// <summary>
/// 管理所有UI面板的管理器
/// 注意：面板预设体名要和面板类名一致！！！！！
/// </summary>
public class UIMgr : SingletonMono<UIMgr>
{
    public Camera UICamera;
    public Canvas Bottom;
    public Canvas Middle;
    public Canvas Top;
    public Canvas System;

    /// <summary>
    /// 主要用于里式替换原则 在字典中 用父类容器装载子类对象
    /// </summary>
    private abstract class BasePanelInfo { }

    /// <summary>
    /// 用于存储面板信息 和加载完成的回调函数的
    /// </summary>
    /// <typeparam name="T">面板的类型</typeparam>
    private class PanelInfo<T> : BasePanelInfo where T : BasePanel
    {
        public T panel;
        public UnityAction<T> callBack;
        public bool isHide;

        public PanelInfo(UnityAction<T> callBack)
        {
            this.callBack += callBack;
        }
    }

    /// <summary>
    /// 用于存储所有的面板对象
    /// </summary>
    private Dictionary<string, BasePanelInfo> panelDic = new Dictionary<string, BasePanelInfo>();

    /// <summary>
    /// 显示面板
    /// </summary>
    /// <typeparam name="T">面板的类型</typeparam>
    /// <param name="canvasType">面板显示在哪一个层级</param>
    /// <param name="callBack">由于可能是异步加载 因此通过委托回调的形式 将加载完成的面板传递出去进行使用</param>
    /// <param name="isSync">是否采用同步加载 默认为false</param>
    public void ShowPanel<T>(E_CanvasType canvasType = E_CanvasType.Middle, UnityAction<T> callBack = null, bool isSync = false) where T : BasePanel
    {
        //获取面板名 预设体名必须和面板类名一致 
        string panelName = typeof(T).Name;
        //存在面板
        if (panelDic.ContainsKey(panelName))
        {
            //取出字典中已经占好位置的数据
            PanelInfo<T> panelInfo = panelDic[panelName] as PanelInfo<T>;
            //正在异步加载中
            if (panelInfo.panel == null)
            {
                //如果之前显示了又隐藏 现在又想显示 那么直接设为false
                panelInfo.isHide = false;

                //如果正在异步加载 应该等待它加载完毕 只需要记录回调函数 加载完后去调用即可
                if (callBack != null)
                    panelInfo.callBack += callBack;
            }
            else//已经加载结束
            {
                //如果是失活状态 直接激活面板 就可以显示了
                if (!panelInfo.panel.gameObject.activeSelf)
                    panelInfo.panel.gameObject.SetActive(true);

                //如果要显示面板 会执行一次面板的默认显示逻辑
                panelInfo.panel.ShowMe();
                //如果存在回调 直接返回出去即可
                callBack?.Invoke(panelInfo.panel);
                //清除回调，防止重复调用
                panelInfo.callBack = null;
            }
            return;
        }

        //不存在面板 先存入字典当中 占个位置 之后如果又显示 我才能得到字典中的信息进行判断
        panelDic.Add(panelName, new PanelInfo<T>(callBack));


        ResMgr.Instance.LoadAssetAsync<GameObject>(panelName, (res) =>
        {
            //取出字典中已经占好位置的数据
            PanelInfo<T> panelInfo = panelDic[panelName] as PanelInfo<T>;
            //表示异步加载结束前 就想要隐藏该面板了 
            if (panelInfo.isHide)
            {
                panelDic.Remove(panelName);
                return;
            }

            //层级的处理
            Transform father;
            switch (canvasType)
            {
                case E_CanvasType.Bottom:
                    father = Bottom.transform;
                    break;
                case E_CanvasType.Middle:
                    father = Middle.transform;
                    break;
                case E_CanvasType.Top:
                    father = Top.transform;
                    break;
                case E_CanvasType.System:
                    father = System.transform;
                    break;
                default:
                    father = Middle.transform;
                    break;
            }

            //将面板预设体创建到对应父对象下 并且保持原本的缩放大小
            GameObject panelObj = GameObject.Instantiate(res, father, false);

            //获取对应UI组件返回出去
            T panel = panelObj.GetComponent<T>();
            //显示面板时执行的默认方法
            panel.ShowMe();
            //传出去使用
            panelInfo.callBack?.Invoke(panel);
            //回调执行完 将其清空 避免内存泄漏
            panelInfo.callBack = null;
            //存储panel
            panelInfo.panel = panel;
        });
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <typeparam name="T">面板类型</typeparam>
    public void HidePanel<T>(bool isDestory = false) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            //取出字典中已经占好位置的数据
            PanelInfo<T> panelInfo = panelDic[panelName] as PanelInfo<T>;
            //但是正在加载中
            if (panelInfo.panel == null)
            {
                //修改隐藏表示 表示 这个面板即将要隐藏
                panelInfo.isHide = true;
                //既然要隐藏了 回调函数都不会调用了 直接置空
                panelInfo.callBack = null;
            }
            else//已经加载结束
            {
                //执行默认的隐藏面板想要做的事情
                panelInfo.panel.HideMe();
                //如果要销毁  就直接将面板销毁从字典中移除记录
                if (isDestory)
                {
                    //销毁面板
                    GameObject.Destroy(panelInfo.panel.gameObject);
                    //从容器中移除
                    panelDic.Remove(panelName);
                }
                //如果不销毁 那么就只是失活 下次再显示的时候 直接复用即可
                else
                {
                    //现在失活由具体UI自行管理，为了适配HideUI的动画
                    //panelInfo.panel.gameObject.SetActive(false);
                }

            }
        }
    }

    /// <summary>
    /// 获取面板
    /// </summary>
    /// <typeparam name="T">面板的类型</typeparam>
    public void GetPanel<T>(UnityAction<T> callBack) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            //取出字典中已经占好位置的数据
            PanelInfo<T> panelInfo = panelDic[panelName] as PanelInfo<T>;
            //正在加载中
            if (panelInfo.panel == null)
            {
                //加载中 应该等待加载结束 再通过回调传递给外部去使用
                panelInfo.callBack += callBack;
            }
            else if (!panelInfo.isHide)//加载结束 并且没有隐藏
            {
                callBack?.Invoke(panelInfo.panel);
            }
        }
    }
}
