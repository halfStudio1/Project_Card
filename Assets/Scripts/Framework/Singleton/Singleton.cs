/// <summary>
/// 单例模式基类
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> where T : class,new()
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = new T();
            return instance;
        }
    }
}
