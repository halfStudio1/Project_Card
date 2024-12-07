using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyView : MonoBehaviour
{
    public EnemyObj enemyObj;

    public Image healthBar;
    public Image enemyImage;

    public void Init(EnemyObj enemyObj)
    {
        this.enemyObj = enemyObj;
    }
}
