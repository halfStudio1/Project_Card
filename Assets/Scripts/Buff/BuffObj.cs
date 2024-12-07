using UnityEngine.Events;

public class BuffObj
{
    public cfg.buff.Buff buff;

    //存在时间
    public int time;

    //层数
    public int stack;

    //携带者
    public EnemyObj enemyObj;

    //buff被添加
    public UnityAction onAdd;

    //buff被移除
    public UnityAction onRemove;

    //buff被触发
    public UnityAction onOccur;


    public virtual void OnAddStack(int stack)
    {
        this.stack += stack;
    }

    public virtual void OnAdd(EnemyObj enemyObj)
    {
        this.enemyObj = enemyObj;
    }

    public virtual void OnRemove(EnemyObj enemyObj)
    {
        this.enemyObj = null;
        Clear();
    }

    public virtual void Clear()
    {
        buff = null;
        time = 0;
        stack = 0;
        enemyObj = null;
        onAdd = null;
        onRemove = null;
        onOccur = null;
    }
}
