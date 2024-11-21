using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//战斗数据
public class BattleData
{
    //玩家数据
    public PlayerBattleData playerBattleData = new PlayerBattleData();
    //敌人数据
    public EnemyBattleData enemyBattleData = new EnemyBattleData();
    //行动队列
    public Queue<CommandBase> commandQueue = new Queue<CommandBase>();

    //结算
    public async void Settle()
    {
        await ReallySettle();
    }
    public async UniTask ReallySettle()
    {
        while (commandQueue.Count > 0)
        {
            CommandBase command = commandQueue.Dequeue();

            switch (command.type)
            {
                case E_CommandType.None:
                    break;
                case E_CommandType.Attack:
                    enemyBattleData.GetHurt(command);
                    break;
                case E_CommandType.Fold:
                    BattleController.Instance.SelectFold(command);
                    break;
            }

            //如果一个行动没有做完，就不会进行下一个行动
            while (!command.isCompelete)
                await UniTask.Yield();
        }
        Debugger.LogPink("结算完成");
    }
}
