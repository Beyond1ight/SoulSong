using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class DropMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject drop;
    bool dmgPopupDisplay, teamAttack, enemyAttack, teamTarget = false;
    int target = 0;
    GameObject currentGO, targetGO; // currentGO: The Current Turn. targetGO: The Current Turn's target

    // Start is called before the first frame update
    void Awake()
    {
        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR1TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR1)
        {
            teamAttack = true;
            teamTarget = Engine.e.battleSystem.char1TargetingTeam;
            target = Engine.e.battleSystem.char1Target;
            currentGO = Engine.e.activeParty.activeParty[0].gameObject;

            if (!teamTarget)
            {
                targetGO = Engine.e.battleSystem.enemies[target].gameObject;
            }
            else
            {
                if (target == 0)
                {
                    targetGO = Engine.e.activeParty.gameObject;
                }
                if (target == 1)
                {
                    targetGO = Engine.e.activePartyMember2.gameObject;
                }
                if (target == 2)
                {
                    targetGO = Engine.e.activePartyMember3.gameObject;
                }
            }
        }

        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR2TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR2)
        {
            teamAttack = true;
            teamTarget = Engine.e.battleSystem.char2TargetingTeam;
            target = Engine.e.battleSystem.char2Target;
            currentGO = Engine.e.activeParty.activeParty[1].gameObject;

            if (!teamTarget)
            {
                targetGO = Engine.e.battleSystem.enemies[target].gameObject;
            }
            else
            {
                if (target == 0)
                {
                    targetGO = Engine.e.activeParty.gameObject;
                }
                if (target == 1)
                {
                    targetGO = Engine.e.activePartyMember2.gameObject;
                }
                if (target == 2)
                {
                    targetGO = Engine.e.activePartyMember3.gameObject;
                }
            }
        }
        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR3TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR3)
        {
            teamAttack = true;
            teamTarget = Engine.e.battleSystem.char3TargetingTeam;
            target = Engine.e.battleSystem.char3Target;
            currentGO = Engine.e.activeParty.activeParty[2].gameObject;

            if (!teamTarget)
            {
                targetGO = Engine.e.battleSystem.enemies[target].gameObject;
            }
            else
            {
                if (target == 0)
                {
                    targetGO = Engine.e.activeParty.gameObject;
                }
                if (target == 1)
                {
                    targetGO = Engine.e.activePartyMember2.gameObject;
                }
                if (target == 2)
                {
                    targetGO = Engine.e.activePartyMember3.gameObject;
                }
            }
        }

        if (Engine.e.battleSystem.currentInQueue == BattleState.ENEMY1TURN)
        {
            enemyAttack = true;
            target = Engine.e.battleSystem.enemy1AttackTarget;
            currentGO = Engine.e.battleSystem.enemies[0].gameObject;

            if (!teamTarget)
            {
                if (target == 0)
                {
                    targetGO = Engine.e.activeParty.gameObject;
                }
                if (target == 1)
                {
                    targetGO = Engine.e.activePartyMember2.gameObject;
                }
                if (target == 2)
                {
                    targetGO = Engine.e.activePartyMember3.gameObject;
                }
            }
            else
            {
                targetGO = Engine.e.battleSystem.enemies[target].gameObject;
            }
        }

        if (Engine.e.battleSystem.currentInQueue == BattleState.ENEMY2TURN)
        {
            enemyAttack = true;
            target = Engine.e.battleSystem.enemy2AttackTarget;
            currentGO = Engine.e.battleSystem.enemies[1].gameObject;

            if (!teamTarget)
            {
                if (target == 0)
                {
                    targetGO = Engine.e.activeParty.gameObject;
                }
                if (target == 1)
                {
                    targetGO = Engine.e.activePartyMember2.gameObject;
                }
                if (target == 2)
                {
                    targetGO = Engine.e.activePartyMember3.gameObject;
                }
            }
            else
            {
                targetGO = Engine.e.battleSystem.enemies[target].gameObject;
            }
        }
        if (Engine.e.battleSystem.currentInQueue == BattleState.ENEMY3TURN)
        {
            enemyAttack = true;
            target = Engine.e.battleSystem.enemy3AttackTarget;
            currentGO = Engine.e.battleSystem.enemies[2].gameObject;

            if (!teamTarget)
            {
                if (target == 0)
                {
                    targetGO = Engine.e.activeParty.gameObject;
                }
                if (target == 1)
                {
                    targetGO = Engine.e.activePartyMember2.gameObject;
                }
                if (target == 2)
                {
                    targetGO = Engine.e.activePartyMember3.gameObject;
                }
            }
            else
            {
                targetGO = Engine.e.battleSystem.enemies[target].gameObject;
            }
        }
        if (Engine.e.battleSystem.currentInQueue == BattleState.ENEMY4TURN)
        {
            enemyAttack = true;
            target = Engine.e.battleSystem.enemy4AttackTarget;
            currentGO = Engine.e.battleSystem.enemies[3].gameObject;

            if (!teamTarget)
            {
                if (target == 0)
                {
                    targetGO = Engine.e.activeParty.gameObject;
                }
                if (target == 1)
                {
                    targetGO = Engine.e.activePartyMember2.gameObject;
                }
                if (target == 2)
                {
                    targetGO = Engine.e.activePartyMember3.gameObject;
                }
            }
            else
            {
                targetGO = Engine.e.battleSystem.enemies[target].gameObject;
            }
        }

        Engine.e.battleSystem.animExists = true;

        if (Engine.e.timeOfDay < 300 || Engine.e.timeOfDay > 700)
        {
            GetComponent<Light2D>().intensity = 0.5f;
        }
        else
        {
            GetComponent<Light2D>().intensity = 1f;
        }
    }

    public IEnumerator CheckDistance()
    {
        //Vector3 targetPos = Vector3.MoveTowards(transform.position, GameManager.gameManager.battleSystem.leaderPos, 4 * Time.deltaTime);
        Vector3 targetPos = Vector3.MoveTowards(transform.position, targetGO.transform.position, 5f * Time.deltaTime);
        rb.MovePosition(targetPos);

        //if (Vector3.Distance(rb.transform.position, GameManager.gameManager.battleSystem.leaderPos) < 0.1)
        if (Vector3.Distance(rb.transform.position, targetGO.transform.position) < 0.1)
        {
            if (dmgPopupDisplay == false)
            {

                dmgPopupDisplay = true;

                if (Engine.e.battleSystem.lastDropChoice != null)
                {
                    if (targetGO.GetComponent<Enemy>())
                    {


                        if (targetGO.GetComponent<Enemy>().currentHealth <= 0)
                        {

                            if (targetGO.GetComponent<EnemyMovement>())
                            {
                                targetGO.GetComponent<EnemyMovement>().enabled = false;
                            }

                            if (targetGO.GetComponent<Light2D>())
                            {
                                targetGO.GetComponent<Light2D>().enabled = false;
                            }

                            yield return new WaitForSeconds(1f);

                            // targetGO.GetComponent<SpriteRenderer>().enabled = false;

                            Engine.e.battleSystem.enemyUI[target].SetActive(false);

                        }

                        if (!targetGO.GetComponent<Enemy>().isPoisoned && !targetGO.GetComponent<Enemy>().isAsleep)
                        {
                            //   targetGO.GetComponent<SpriteRenderer>().color = Color.white;
                        }
                        else
                        {
                            if (targetGO.GetComponent<Enemy>().isPoisoned)
                            {
                                //  targetGO.GetComponent<SpriteRenderer>().color = Color.green;
                            }

                            if (targetGO.GetComponent<Enemy>().isAsleep)
                            {
                                //    targetGO.GetComponent<SpriteRenderer>().color = Color.grey;
                            }
                        }

                    }
                    else
                    {
                        if (targetGO.GetComponent<Character>())
                        {
                            if (Engine.e.battleSystem.lastDropChoice.dropType == "Shadow")
                            {

                                if (Engine.e.battleSystem.lastDropChoice.dropName == "Bio")
                                {

                                    if (targetGO.GetComponent<Character>().isPoisoned)
                                    {
                                    }
                                    else
                                    {
                                        if (targetGO.GetComponent<Character>().poisonDefense < 100)
                                        {
                                        }
                                        else
                                        {
                                        }
                                    }
                                }

                                if (Engine.e.battleSystem.lastDropChoice.dropName == "Knockout")
                                {

                                    if (targetGO.GetComponent<Character>().isAsleep)
                                    {
                                    }
                                    else
                                    {
                                        if (targetGO.GetComponent<Character>().sleepDefense < 100)
                                        {
                                        }
                                        else
                                        {
                                        }
                                    }
                                }

                                if (Engine.e.battleSystem.lastDropChoice.dropName == "Blind")
                                {

                                    if (targetGO.GetComponent<Character>().isConfused)
                                    {
                                    }
                                    else
                                    {
                                        if (targetGO.GetComponent<Character>().sleepDefense < 100)
                                        {
                                        }
                                        else
                                        {
                                        }
                                    }
                                }

                                if (Engine.e.battleSystem.lastDropChoice.dropName == "Death")
                                {
                                    targetGO.GetComponent<Character>().InflictDeath();
                                }
                                //   Destroy(dmgPopup, 1f);
                            }
                            if (Engine.e.battleSystem.lastDropChoice.dropType == "Holy")
                            {
                                if (Engine.e.battleSystem.lastDropChoice.dropName == "Holy Light")
                                {
                                    targetGO = Engine.e.activeParty.activeParty[target].gameObject;
                                    // HolyLight(currentMove, Engine.e.battleSystem.lastDropChoice.dropPower, targetGO);

                                    //  Destroy(dmgPopup, 1f);
                                }
                            }


                            // targetGO.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
                            GetComponent<ParticleSystem>().Emit(1);
                            //Engine.e.battleSystem.hud.displayHealth[Engine.e.battleSystem.previousTargetReferenceChar].text = Engine.e.activeParty.activeParty[Engine.e.battleSystem.previousTargetReferenceChar].GetComponent<Character>().currentHealth.ToString();


                            if (!targetGO.GetComponent<Character>().isPoisoned && !targetGO.GetComponent<Character>().isAsleep)
                            {
                                //   targetGO.GetComponent<SpriteRenderer>().color = Color.white;
                                //   targetGO.GetComponent<SpriteRenderer>().color = Color.white;
                            }
                            else
                            {
                                if (targetGO.GetComponent<Character>().isPoisoned)
                                {

                                    //    targetGO.GetComponent<SpriteRenderer>().color = Color.green;
                                    //   targetGO.GetComponent<SpriteRenderer>().color = Color.green;
                                }

                                if (targetGO.GetComponent<Character>().isAsleep)
                                {
                                    //targetGO.GetComponent<SpriteRenderer>().color = Color.grey;
                                    //targetGO.GetComponent<SpriteRenderer>().color = Color.grey;
                                }
                            }
                        }
                    }
                }
            }
            yield return new WaitForSeconds(1.0f);
            Engine.e.battleSystem.animExists = false;
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(CheckDistance());
        /* if (teamAttack)
         {
             if (!teamTarget)
             {
                 StartCoroutine(CheckDistanceToEnemy());
             }
             else
             {
                 StartCoroutine(CheckDistanceToTeam());
             }
         }
         if (enemyAttack)
         {
             if (!teamTarget)
             {
                 StartCoroutine(CheckDistanceToTeam());
             }
             else
             {
                 StartCoroutine(CheckDistanceToEnemy());
             }
         }
     }*/
    }
}