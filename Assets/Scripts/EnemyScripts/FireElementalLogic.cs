using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElementalLogic : MonoBehaviour
{
    public void Logic(int target)
    {
        int enemyPos = 0;

        if (Engine.e.battleSystem.currentInQueue == BattleState.ENEMY1TURN)
        {
            enemyPos = 0;
        }

        if (Engine.e.battleSystem.currentInQueue == BattleState.ENEMY2TURN)
        {
            enemyPos = 1;
        }
        if (Engine.e.battleSystem.currentInQueue == BattleState.ENEMY3TURN)
        {
            enemyPos = 2;
        }
        if (Engine.e.battleSystem.currentInQueue == BattleState.ENEMY4TURN)
        {
            enemyPos = 3;
        }

        GetComponent<Enemy>().GenericMoveSet(target);


        Engine.e.battleSystem.partyCheckNext = false;

    }
}
