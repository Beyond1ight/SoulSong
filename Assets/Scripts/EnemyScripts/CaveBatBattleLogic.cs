using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBatBattleLogic : MonoBehaviour
{
    int moveCount = 0;

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
        || (Engine.e.battleSystem.enemies[3] != null && Engine.e.battleSystem.enemies[3].currentHealth < Engine.e.battleSystem.enemies[3].maxHealth / 2))
        {
            if (GetComponent<Enemy>().currentMana >= GetComponent<Enemy>().drops[0].dropCost)
            {
                Engine.e.battleSystem.lastDropChoice = GetComponent<Enemy>().drops[0];
                Engine.e.battleSystem.enemies[GetComponentInParent<EnemyGroup>().GetLowestHealthEnemy()].GetComponent<Enemy>().ConfuseTakeDropDamage(enemyPos, Engine.e.battleSystem.enemies[enemyPos].GetComponent<Enemy>().drops[0]);

                GetComponent<Enemy>().currentMana -= GetComponent<Enemy>().drops[0].dropCost;
                Engine.e.battleSystem.enemyAttacking = false;

            }
        }
        else
        {
            if (moveCount == 0)
            {
                Engine.e.battleSystem.enemyMoving = true;
                Engine.e.battleSystem.enemyAttacking = true;
                Engine.e.PhysicalDamageCalculation(target, GetComponent<Enemy>().damage);


            }

            if (moveCount == 1)
            {

                int enemyDropChoice = Random.Range(0, GetComponent<Enemy>().drops.Length);

                if (GetComponent<Enemy>().currentMana >= GetComponent<Enemy>().drops[enemyDropChoice].dropCost)
                {
                    Engine.e.battleSystem.enemyAttackDrop = true;

                    Engine.e.battleSystem.lastDropChoice = GetComponent<Enemy>().drops[enemyDropChoice];
                    Engine.e.InstantiateEnemyDropEnemy(enemyPos, enemyDropChoice);

                    Engine.e.battleSystem.isDead = Engine.e.TakeElementalDamage(target, GetComponent<Enemy>().drops[enemyDropChoice].dropPower, GetComponent<Enemy>().drops[enemyDropChoice].dropType);
                    GetComponent<Enemy>().currentMana -= GetComponent<Enemy>().drops[enemyDropChoice].dropCost;

                    Engine.e.battleSystem.enemyAttacking = false;
                }
                else
                {
                    Engine.e.battleSystem.enemyMoving = true;
                    Engine.e.battleSystem.enemyAttacking = true;
                    Engine.e.PhysicalDamageCalculation(target, GetComponent<Enemy>().damage);

                }

            }
        }

        moveCount++;
        Engine.e.battleSystem.partyCheckNext = false;

    }
}
