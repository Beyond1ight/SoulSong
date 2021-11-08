using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishermansBaneQuest : MonoBehaviour
{
    EnemyGroup enemyGroup;
    public Quest questReference;
    public bool isDead;

    void Start()
    {
        enemyGroup = GetComponentInParent<EnemyGroup>();

        if (questReference.inAdventureLog)
        {
            if (questReference.questComplete)
            {
                isDead = true;

                if (isDead)
                {
                    enemyGroup.DespawnGroup();
                }
            }
            else
            {
                enemyGroup.quest = questReference;
                enemyGroup.spawnChance = 99;
            }
        }
        else
        {
            enemyGroup.DespawnGroup();

        }
    }

    void HandleQuestObjective()
    {
        if (Engine.e.battleSystem.enemyGroup = enemyGroup)
        {
            if (Engine.e.battleSystem.state == BattleState.LEVELUPCHECK)
            {
                isDead = true;
                questReference.questComplete = true;
            }
        }
    }

    void Update()
    {
        if (questReference.inAdventureLog && !questReference.questComplete)
        {
            HandleQuestObjective();
        }
    }
}
