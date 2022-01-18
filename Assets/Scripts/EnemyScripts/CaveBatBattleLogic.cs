using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBatBattleLogic : MonoBehaviour
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

        if (Engine.e.battleSystem.enemies[0].currentHealth < Engine.e.battleSystem.enemies[0].maxHealth / 2
        || (Engine.e.battleSystem.enemies[1] != null && Engine.e.battleSystem.enemies[1].currentHealth < Engine.e.battleSystem.enemies[1].maxHealth / 2)
        || (Engine.e.battleSystem.enemies[2] != null && Engine.e.battleSystem.enemies[2].currentHealth < Engine.e.battleSystem.enemies[2].maxHealth / 2)
        || (Engine.e.battleSystem.enemies[3] != null && Engine.e.battleSystem.enemies[3].currentHealth < Engine.e.battleSystem.enemies[3].maxHealth / 2)
        && GetComponent<Enemy>().currentMana >= GetComponent<Enemy>().drops[0].dropCost)
        {

            Engine.e.battleSystem.attackingTeam = true;
            Engine.e.battleSystem.enemyAttackDrop = true;
            Engine.e.battleSystem.lastDropChoice = GetComponent<Enemy>().drops[0];
            Engine.e.battleSystem.enemies[GetComponentInParent<EnemyGroup>().GetLowestHealthEnemy()].GetComponent<Enemy>().DropEffect(Engine.e.battleSystem.enemies[enemyPos].GetComponent<Enemy>().drops[0]);

            Engine.e.battleSystem.HandleDropAnim(this.gameObject, this.gameObject, GetComponent<Enemy>().drops[0]);
            GetComponent<Enemy>().currentMana -= GetComponent<Enemy>().drops[0].dropCost;

            Engine.e.battleSystem.enemyAttacking = false;

        }
        else
        {
            GetComponent<Enemy>().GenericMoveSet(target);
        }

        Engine.e.battleSystem.partyCheckNext = false;

    }
}
