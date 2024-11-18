using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TipsPanel : BasePanel
{

    public RectTransform tips;
    //private float speed = 5f;
    private void Start()
    {

    }

    public override void ShowMe()
    {
    }

    public override void HideMe()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        // 获取鼠标在屏幕上的位置
        //Vector2 mouseScreenPosition = Input.mousePosition;


        //// 获取鼠标在屏幕上的位置
        //Vector3 mouseScreenPosition = Input.mousePosition;

        //// 将Z轴设为Canvas的距离，以保持UI深度
        //mouseScreenPosition.z = Camera.main.WorldToScreenPoint(tips.transform.position).z;

        //// 将鼠标位置从屏幕坐标转换为世界坐标
        //Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        //// 设置物体的位置为鼠标位置，根据速度调整移动速度
        //tips.transform.position = Vector3.Lerp(tips.transform.position, mouseWorldPosition, speed * Time.deltaTime);

    }

}
