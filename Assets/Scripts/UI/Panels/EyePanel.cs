using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EyePanel : BasePanel
{
    public Button Btn_End;


    private void Start()
    {
        Btn_End.onClick.AddListener(OnBtn_EndClick);

    }

    public override void ShowMe()
    {
        gameObject.SetActive(true);
    }

    public override void HideMe()
    {
        gameObject.SetActive(false);
    }

    public void Init(int[] cardID)
    {

    }

    private void OnBtn_EndClick(){}

}
