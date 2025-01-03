
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using SimpleJSON;


namespace cfg.buff
{
public sealed partial class Buff : Luban.BeanBase
{
    public Buff(JSONNode _buf) 
    {
        { if(!_buf["id"].IsNumber) { throw new SerializationException(); }  Id = _buf["id"]; }
        { if(!_buf["name"].IsString) { throw new SerializationException(); }  Name = _buf["name"]; }
        { if(!_buf["imgName"].IsString) { throw new SerializationException(); }  ImgName = _buf["imgName"]; }
        { if(!_buf["info"].IsString) { throw new SerializationException(); }  Info = _buf["info"]; }
        { if(!_buf["buffType"].IsNumber) { throw new SerializationException(); }  BuffType = (E_BuffType)_buf["buffType"].AsInt; }
        { if(!_buf["priority"].IsNumber) { throw new SerializationException(); }  Priority = _buf["priority"]; }
        { if(!_buf["maxStack"].IsNumber) { throw new SerializationException(); }  MaxStack = _buf["maxStack"]; }
    }

    public static Buff DeserializeBuff(JSONNode _buf)
    {
        return new buff.Buff(_buf);
    }

    /// <summary>
    /// 编号
    /// </summary>
    public readonly int Id;
    /// <summary>
    /// buff的名字
    /// </summary>
    public readonly string Name;
    /// <summary>
    /// 图片的名字
    /// </summary>
    public readonly string ImgName;
    /// <summary>
    /// buff的描述
    /// </summary>
    public readonly string Info;
    /// <summary>
    /// Buff类型
    /// </summary>
    public readonly E_BuffType BuffType;
    /// <summary>
    /// 优先级
    /// </summary>
    public readonly int Priority;
    /// <summary>
    /// 最大层数
    /// </summary>
    public readonly int MaxStack;
   
    public const int __ID__ = 9300270;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "id:" + Id + ","
        + "name:" + Name + ","
        + "imgName:" + ImgName + ","
        + "info:" + Info + ","
        + "buffType:" + BuffType + ","
        + "priority:" + Priority + ","
        + "maxStack:" + MaxStack + ","
        + "}";
    }
}

}

