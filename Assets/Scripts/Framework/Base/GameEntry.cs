using UnityEngine;

/// <summary>
/// 游戏中的所有场景
/// </summary>
public enum E_ScenesType
{
    GameEntry,
    TestScene_1,
}
public class GameEntry : MonoBehaviour
{
    public E_ScenesType startScene;

    private void Start()
    {
        if(startScene == E_ScenesType.GameEntry)
            return;

        ResMgr.Instance.LoadSceneAsync(startScene.ToString());
    }
}
