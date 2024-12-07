using UnityEngine.Events;

public enum E_AttackType
{
    Normal,
    Bleed,
    Penetrate,
    Poison,
}
public class AttackObj
{
    public int damage = 0;

    public int attackTime = 1;

    public EnemyObj enemyObj;
    public PlayerObj playerObj;

    public E_AttackType attackType = E_AttackType.Normal;

    public UnityAction<EnemyObj> attackEndAction;

    public UnityAction<EnemyObj> attackAction;

    public void Clear()
    {
        damage = 0;
        attackTime = 1;
        enemyObj = null;
        playerObj = null;
        attackType = E_AttackType.Normal;
        attackEndAction = null;
        attackAction = null;
    }

    public void TakeDamage()
    {
        for (int i = 0; i < attackTime; i++)
        {
            attackAction?.Invoke(enemyObj);
            enemyObj.ReadyHurt(this);
        }
        attackEndAction?.Invoke(enemyObj);

        Clear();
    }
}
