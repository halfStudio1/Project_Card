using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestUIPanel : BasePanel
{
    public Button Btn_1;
    public Button Btn_2;
    public Button Btn_3;
    public Slider Sld_1;
    public Image Img_1;
    public Text Txt_1;


    private void Start()
    {
        Btn_1.onClick.AddListener(OnBtn_1Click);
        Btn_2.onClick.AddListener(OnBtn_2Click);
        Btn_3.onClick.AddListener(OnBtn_3Click);
        Sld_1.onValueChanged.AddListener(OnSld_1ValueChanged);

    }

    public override void ShowMe()
    {
    }

    public override void HideMe()
    {
        gameObject.SetActive(false);
    }

    private void OnBtn_1Click(){}
    private void OnBtn_2Click(){}
    private void OnBtn_3Click(){}
    private void OnSld_1ValueChanged(float value){}

}
