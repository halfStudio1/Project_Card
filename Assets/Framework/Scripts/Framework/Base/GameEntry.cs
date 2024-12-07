using UnityEngine;


public class GameEntry : MonoBehaviour
{
    public E_ScenesType startScene;

    private void Start()
    {
        ConfigTableMgr.Instance.InitTable();
        DatasMgr.Instance.LoadData();
        CardMgr.Instance.Init();
        BuffMgr.Instance.Init();

        //ResMgr.Instance.LoadSceneAsync(startScene.ToString());

        if (startScene == E_ScenesType.GameEntry)
            return;
    }
}
