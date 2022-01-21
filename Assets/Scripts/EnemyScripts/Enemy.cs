using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;
public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float currentHealth, maxHealth, currentMana, maxMana, haste, experiencePoints, choiceAttack, damageTotal, hitChance, poisonDmg;
    public int lvl, physicalDamage, groupIndex, moneyLootAmount, sleepTimer, confuseTimer, deathTimer = 3;
    public Drops[] drops;
    public bool isPoisoned, isConfused, isAsleep, deathInflicted, inflicted;
    public float physicalDefense, dodgeChance, fireDefense, iceDefense, lightningDefense, waterDefense, shadowDefense, holyDefense, poisonDefense, sleepDefense, confuseDefense, deathDefense, instantDeathDefense,
    fireDropsLevel, iceDropsLevel, lightningDropsLevel, waterDropsLevel, shadowDropsLevel, holyDropsLevel, stealChance;

    public Item[] itemDrops;
    public Item stealableItem;
    public int itemDropChance;

    public BattleSystem battleSystem;
    public Transform enemyPos;
    public string worldZone;
    public Vector3 currentBattlePos;
    public GameObject enemyContext;

    void Awake()
    {
        battleSystem = Engine.e.battleSystem;
        currentBattlePos = transform.position;

        currentHealth = maxHealth;
        currentMana = maxMana;
        // if (groupIndex == -1)
        // {
        //  GetComponentInParent<EnemyGroup>().GetEnemyIndex(this);
        //  Debug.Log(name + " in group " + GetComponentInParent<EnemyGroup>().gameObject.name + ", scene " + Engine.e.currentScene + " has an index of -1. Should be " + groupIndex);
        // }
    }

    public void BattleLogic(Enemy _enemy, int _enemyTarget)
    {
        switch (_enemy.enemyName)
        {
            // Regular Enemies
            case "Cave Bat":
                GetComponent<CaveBatBattleLogic>().Logic(_enemyTarget);
                break;

            // Bosses
            case "Xelient the Aggressor":
                GetComponent<XelientBattleLogic>().Logic(_enemyTarget);
                break;
        }
    }

    public void GenericMoveSet(int target)
    {

        GameObject targetGOLoc = null;

        if (target == 0)
        {
            targetGOLoc = Engine.e.activeParty.gameObject;
        }
        if (target == 1)
        {
            targetGOLoc = Engine.e.activePartyMember2;
        }
        if (target == 2)
        {
            targetGOLoc = Engine.e.activePartyMember3;
        }

        if (drops[0] != null)
        {
            int _choiceAttack = Random.Range(0, 100);


            if (choiceAttack < _choiceAttack) // The higher the number (of choiceAttack)[0-100], the more likely the enemy will use a Drop attack.
            {
                Engine.e.battleSystem.enemyMoving = true;
                Engine.e.battleSystem.enemyAttacking = true;
                Engine.e.battleSystem.isDead = Engine.e.activeParty.activeParty[target].GetComponent<Character>().TakePhysicalDamage(target, physicalDamage);

            }
            else
            {
                int enemyDropChoice = Random.Range(0, drops.Length);

                if (currentMana >= drops[enemyDropChoice].dropCost)
                {
                    Engine.e.battleSystem.enemyAttackDrop = true;

                    Engine.e.battleSystem.lastDropChoice = drops[enemyDropChoice];
                    Engine.e.battleSystem.HandleDropAnim(this.gameObject, targetGOLoc, drops[enemyDropChoice]);

                    Engine.e.activeParty.activeParty[target].GetComponent<Character>().DropEffect(drops[enemyDropChoice]);

                    currentMana -= GetComponent<Enemy>().drops[enemyDropChoice].dropCost;

                    Engine.e.battleSystem.enemyAttacking = false;

                }
                else
                {
                    Engine.e.battleSystem.enemyMoving = true;
                    Engine.e.battleSystem.enemyAttacking = true;
                    Engine.e.battleSystem.isDead = Engine.e.activeParty.activeParty[target].GetComponent<Character>().TakePhysicalDamage(target, GetComponent<Enemy>().physicalDamage);
                }
            }
        }
        else
        {
            Engine.e.battleSystem.enemyMoving = true;
            Engine.e.battleSystem.enemyAttacking = true;
            Engine.e.battleSystem.isDead = Engine.e.activeParty.activeParty[target].GetComponent<Character>().TakePhysicalDamage(target, GetComponent<Enemy>().physicalDamage);
        }
        Engine.e.battleSystem.partyCheckNext = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "ActiveParty")
        {
            GetComponentInParent<EnemyGroup>().OnTriggerEnter2D(other);

        }
    }

    public void TakePhysicalDamage(int index, float _dmg, float hitChance)
    {
        float _crit = 0;
        bool teamAttack = false;
        float adjustedDodge = Mathf.Round(hitChance - dodgeChance);
        int hit = Random.Range(0, 99);
        Character attackingCharacter = null;

        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR1TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR1)
        {
            attackingCharacter = Engine.e.activeParty.activeParty[0].GetComponent<Character>();
        }

        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR2TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR2)
        {
            attackingCharacter = Engine.e.activeParty.activeParty[1].GetComponent<Character>();
        }

        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR3TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR3)
        {
            attackingCharacter = Engine.e.activeParty.activeParty[2].GetComponent<Character>();
        }

        float elementFireDamageBonus = 0;
        float elementWaterDamageBonus = 0;
        float elementLightningDamageBonus = 0;
        float elementShadowDamageBonus = 0;
        float elementIceDamageBonus = 0;

        _crit = Random.Range(0, 100);

        float adjustedCritChance = attackingCharacter.critChance;

        float desperationHealth1 = attackingCharacter.maxHealth / 2;
        float desperationHealth2 = attackingCharacter.maxHealth * (0.4f);
        float desperationHealth3 = attackingCharacter.maxHealth * (0.3f);


        if (attackingCharacter.currentHealth <= desperationHealth1)
        {
            adjustedCritChance += 5f;

            if (attackingCharacter.currentHealth <= desperationHealth2)
            {
                adjustedCritChance += 10f;
            }

            if (attackingCharacter.currentHealth <= desperationHealth3)
            {
                adjustedCritChance += 15f;
            }
        }

        if (teamAttack)
        {
            if (_crit <= 10)
            {
                float critDamage = Mathf.Round((_dmg + (_dmg * (2 / 3))));
                _dmg = _dmg + critDamage;
            }
        }
        else
        {
            if (_crit < adjustedCritChance)
            {
                float critDamage = Mathf.Round((_dmg + (_dmg * (2 / 3))));
                _dmg = _dmg + critDamage;
            }
        }

        float adjustedPhysicalDmg = Mathf.Round((_dmg) - (_dmg * (physicalDefense / 100)));

        if (!teamAttack)
        {
            adjustedPhysicalDmg = Random.Range(adjustedPhysicalDmg, (adjustedPhysicalDmg + attackingCharacter.lvl));
        }

        if (isAsleep)
        {
            adjustedDodge = 100;
        }

        if (hit < adjustedDodge)
        {
            if (!teamAttack)
            {
                elementFireDamageBonus += Mathf.Round((attackingCharacter.firePhysicalAttackBonus)
                - (attackingCharacter.firePhysicalAttackBonus * fireDefense / 100));

                elementWaterDamageBonus += Mathf.Round((attackingCharacter.waterPhysicalAttackBonus)
                - (attackingCharacter.waterPhysicalAttackBonus * waterDefense / 100));

                elementLightningDamageBonus += Mathf.Round((attackingCharacter.lightningPhysicalAttackBonus)
                - (attackingCharacter.lightningPhysicalAttackBonus * lightningDefense / 100));

                elementShadowDamageBonus += Mathf.Round((attackingCharacter.shadowPhysicalAttackBonus)
                - (attackingCharacter.shadowPhysicalAttackBonus * shadowDefense / 100));

                elementIceDamageBonus += Mathf.Round((attackingCharacter.icePhysicalAttackBonus)
                - (attackingCharacter.icePhysicalAttackBonus * iceDefense / 100));

                adjustedPhysicalDmg = Mathf.Round((adjustedPhysicalDmg) + elementFireDamageBonus + elementWaterDamageBonus + elementLightningDamageBonus + elementShadowDamageBonus + elementIceDamageBonus);

            }

            Engine.e.battleSystem.damageTotal = adjustedPhysicalDmg;
            currentHealth -= adjustedPhysicalDmg;
            Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, adjustedPhysicalDmg.ToString(), Color.white);
        }
        else
        {
            adjustedPhysicalDmg = 0;
            battleSystem.dodgedAttack = true;
            Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, "Dodged!", Color.white);

        }



        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (isAsleep && adjustedPhysicalDmg != 0)
        {
            isAsleep = false;
            inflicted = false;
            if (GetComponent<EnemyMovement>() != null)
            {
                GetComponent<EnemyMovement>().enabled = true;
                GetComponentInChildren<SpriteRenderer>().color = Color.white;
            }
        }

        if (isConfused)
        {
            int snapoutChance = Random.Range(0, 100);
            if (confuseDefense > snapoutChance)
            {
                isConfused = false;
                inflicted = false;
                confuseTimer = 0;
                if (GetComponent<EnemyMovement>() != null)
                {
                    GetComponent<EnemyMovement>().enabled = true;
                }
                GetComponentInChildren<SpriteRenderer>().color = Color.white;
            }
        }

        if (currentHealth <= 0)
        {
            if (GetComponent<Light2D>())
            {
                GetComponent<Light2D>().enabled = false;
            }

            GetComponent<SpriteRenderer>().enabled = false;
            battleSystem.enemyUI[index].SetActive(false);
            isPoisoned = false;
            isConfused = false;
            isAsleep = false;
            deathInflicted = false;
            inflicted = false;
            poisonDmg = 0;

            switch (index)
            {
                case 0:
                    battleSystem.enemy1ATB = 0;
                    break;
                case 1:
                    battleSystem.enemy2ATB = 0;
                    break;
                case 2:
                    battleSystem.enemy3ATB = 0;
                    break;
                case 3:
                    battleSystem.enemy4ATB = 0;
                    break;
            }
        }
    }

    public void DropEffect(Drops dropChoice)
    {
        float dropValueOutcome = 0;
        int currentIndex = 0;
        Character characterAttacking = null;
        Enemy enemyAttacking = null;
        bool teamAttack = false; // If "false," an active party member is attacking 

        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR1TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR1)
        {
            currentIndex = 0;
            characterAttacking = Engine.e.activeParty.activeParty[0].GetComponent<Character>();

        }
        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR2TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR2)
        {
            currentIndex = 1;
            characterAttacking = Engine.e.activeParty.activeParty[1].GetComponent<Character>();
        }
        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR3TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR3)
        {
            currentIndex = 2;
            characterAttacking = Engine.e.activeParty.activeParty[2].GetComponent<Character>();
        }

        if (Engine.e.battleSystem.currentInQueue == BattleState.ENEMY1TURN)
        {
            currentIndex = 0;
            teamAttack = true;
        }

        if (Engine.e.battleSystem.currentInQueue == BattleState.ENEMY2TURN)
        {
            currentIndex = 1;
            teamAttack = true;

        }

        if (Engine.e.battleSystem.currentInQueue == BattleState.ENEMY3TURN)
        {
            currentIndex = 2;
            teamAttack = true;
        }

        if (Engine.e.battleSystem.currentInQueue == BattleState.ENEMY4TURN)
        {
            currentIndex = 3;
            teamAttack = true;
        }

        if (!teamAttack)
        {
            characterAttacking = Engine.e.activeParty.activeParty[currentIndex].GetComponent<Character>();
        }
        else
        {
            enemyAttacking = Engine.e.battleSystem.enemies[currentIndex].GetComponent<Enemy>();
        }

        if (dropChoice.dropType == "Fire")
        {
            if (teamAttack)
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.fireDropsLevel / 2)));
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * fireDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
                Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, dropValueOutcome.ToString(), Color.white);
            }
            else
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.fireDropsLevel / 2)) + characterAttacking.fireDropAttackBonus);
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * fireDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
                Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, dropValueOutcome.ToString(), Color.white);
            }
        }

        if (dropChoice.dropType == "Ice")
        {
            if (teamAttack)
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.iceDropsLevel / 2)));
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * iceDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
                Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, dropValueOutcome.ToString(), Color.white);
            }
            else
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.iceDropsLevel / 2)) + characterAttacking.iceDropAttackBonus);
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * iceDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
                Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, dropValueOutcome.ToString(), Color.white);
            }
        }

        if (dropChoice.dropType == "Lightning")
        {
            if (teamAttack)
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.lightningDropsLevel / 2)));
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * lightningDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
            }
            else
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.lightningDropsLevel / 2)) + characterAttacking.lightningDropAttackBonus);
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * lightningDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
                Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, dropValueOutcome.ToString(), Color.white);
            }
        }

        if (dropChoice.dropType == "Water")
        {
            if (teamAttack)
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.waterDropsLevel / 2)));
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * waterDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
                Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, dropValueOutcome.ToString(), Color.white);
            }
            else
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.waterDropsLevel / 2)) + characterAttacking.waterDropAttackBonus);
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * waterDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
                Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, dropValueOutcome.ToString(), Color.white);
            }
        }

        if (dropChoice.dropType == "Shadow")
        {

            switch (dropChoice.dropName)
            {
                case "Dark Embrace":
                    if (teamAttack)
                    {
                        dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.shadowDropsLevel / 2)));
                        damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * shadowDefense / 100));
                        currentHealth -= Mathf.Round(damageTotal);
                    }
                    else
                    {
                        dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.shadowDropsLevel / 2)) + characterAttacking.shadowDropAttackBonus);
                        damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * shadowDefense / 100));
                        currentHealth -= Mathf.Round(damageTotal);
                    }

                    Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, dropValueOutcome.ToString(), Color.white);

                    break;
                case "Bio":
                    float poisonChance = Random.Range(0, 100);

                    if (poisonDefense < poisonChance)
                    {
                        isPoisoned = true;
                        inflicted = true;

                        if (teamAttack)
                        {
                            float poisonDmgCalculation = Mathf.Round(dropChoice.dropPower + (enemyAttacking.shadowDropsLevel * 6) / 2);
                            poisonDmg = ((poisonDmgCalculation) - (poisonDmgCalculation * poisonDefense / 100));

                        }
                        else
                        {
                            float poisonDmgCalculation = Mathf.Round(dropChoice.dropPower + (characterAttacking.shadowDropsLevel * 6) / 2);
                            poisonDmg = ((poisonDmgCalculation) - (poisonDmgCalculation * poisonDefense / 100));

                        }
                        Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, "Poisoned!", Color.white);
                    }
                    else
                    {

                        Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, "Resisted!", Color.white);
                    }
                    break;
                case "Knockout":
                    if (!isAsleep)
                    {
                        float sleepChance = Random.Range(0, 100);

                        if (sleepDefense < sleepChance)
                        {
                            isAsleep = true;
                            sleepTimer = 0;

                            Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, "Sleeping!", Color.white);
                        }
                        else
                        {
                            Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, "Resisted!", Color.white);
                        }
                    }
                    break;
                case "Blind":
                    if (!isConfused)
                    {
                        float confuseChance = Random.Range(0, 100);

                        if (confuseDefense < confuseChance)
                        {
                            isConfused = true;
                            confuseTimer = 0;

                            Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, "Confused!", Color.white);
                        }
                        else
                        {
                            Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, "Resisted!", Color.white);
                        }
                    }
                    break;
                case "Death":
                    InflictDeath();
                    break;
            }
        }

        if (dropChoice.dropType == "Holy")
        {
            switch (dropChoice.dropName)
            {
                case "Holy Light":
                    if (teamAttack)
                    {
                        dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.holyDropsLevel / 2)));
                        damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * holyDefense / 100));
                        currentHealth += damageTotal;
                    }
                    else
                    {
                        dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.holyDropsLevel / 2)));
                        damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * holyDefense / 100));
                        currentHealth += damageTotal;
                        Debug.Log(damageTotal);
                    }

                    Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, damageTotal.ToString(), Color.green);

                    if (currentHealth >= maxHealth)
                    {
                        currentHealth = maxHealth;
                    }

                    break;
                case "Repent":

                    if (teamAttack)
                    {
                        if (enemyAttacking.holyDropsLevel >= 20)
                        {
                            if (isAsleep)
                            {
                                isAsleep = false;
                            }
                            if (isPoisoned)
                            {
                                isPoisoned = false;
                            }
                            if (isConfused)
                            {
                                isConfused = false;
                                confuseTimer = 0;
                            }
                            if (deathInflicted)
                            {
                                deathInflicted = false;
                                deathTimer = 3;
                                //deathTimerPopup.SetActive(false);
                            }
                        }
                        else
                        {
                            if (enemyAttacking.holyDropsLevel < 20 && enemyAttacking.holyDropsLevel >= 10)
                            {
                                if (isAsleep)
                                {
                                    isAsleep = false;
                                }
                                if (isPoisoned)
                                {
                                    isPoisoned = false;
                                }
                            }
                            else
                            {
                                if (isAsleep)
                                {
                                    isAsleep = false;
                                }
                            }
                        }

                    }
                    else
                    {
                        if (characterAttacking.holyDropsLevel >= 20)
                        {
                            if (isAsleep)
                            {
                                isAsleep = false;
                            }
                            if (isPoisoned)
                            {
                                isPoisoned = false;
                            }
                            if (isConfused)
                            {
                                isConfused = false;
                                confuseTimer = 0;
                            }
                            if (deathInflicted)
                            {
                                deathInflicted = false;
                                deathTimer = 3;
                                //deathTimerPopup.SetActive(false);
                            }
                        }
                        else
                        {
                            if (characterAttacking.holyDropsLevel < 20 && characterAttacking.holyDropsLevel >= 10)
                            {
                                if (isAsleep)
                                {
                                    isAsleep = false;
                                }
                                if (isPoisoned)
                                {
                                    isPoisoned = false;
                                }
                            }
                            else
                            {
                                if (isAsleep)
                                {
                                    isAsleep = false;
                                }
                            }
                        }
                    }
                    Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, "Cured!", Color.green);

                    inflicted = false;

                    break;
            }
        }


        Engine.e.battleSystem.damageTotal = damageTotal;

        if (!teamAttack)
        {
            characterAttacking.GetDropExperience(dropChoice);
        }

        if (isAsleep)
        {
            if (dropChoice.dropName != "Knockout" && battleSystem.lastDropChoice.dropName != "Bio" && battleSystem.lastDropChoice.dropName != "Blind")
            {
                isAsleep = false;
                isConfused = false;
                inflicted = false;

                if (GetComponent<EnemyMovement>() != null)
                {
                    GetComponent<EnemyMovement>().enabled = true;
                }
                GetComponentInChildren<SpriteRenderer>().color = Color.grey;
            }
        }

        if (isConfused)
        {
            int snapoutChance = Random.Range(0, 100);
            if (confuseDefense > snapoutChance)
            {
                isConfused = false;
                inflicted = false;
                confuseTimer = 0;

                if (GetComponent<EnemyMovement>() != null)
                {
                    GetComponent<EnemyMovement>().enabled = true;
                }
                GetComponentInChildren<SpriteRenderer>().color = Color.white;
            }

            if (dropChoice.dropName == "Knockout")
            {
                isAsleep = true;
                isConfused = false;
                if (GetComponent<EnemyMovement>() != null)
                {
                    GetComponent<EnemyMovement>().enabled = false;
                }


                GetComponentInChildren<SpriteRenderer>().color = Color.grey;
            }
        }

        if (!battleSystem.animExists)
        {
            if (currentHealth <= 0)
            {
                if (GetComponent<Light2D>())
                {
                    GetComponent<Light2D>().enabled = false;
                }

                isPoisoned = false;
                isConfused = false;
                isAsleep = false;
                deathInflicted = false;
                inflicted = false;
                poisonDmg = 0;

                GetComponentInChildren<SpriteRenderer>().enabled = false;


                battleSystem.enemyUI[GetComponentInParent<EnemyGroup>().GetEnemyIndex(this)].SetActive(false);
            }
        }
    }

    public void UseItem(Item item)
    {
        bool failedItemUse = false;
        GameObject spawnGOLoc = null;

        if (item.numberHeld == 0)
        {
            failedItemUse = true;
        }

        if (battleSystem.currentInQueue == BattleState.CHAR1TURN)
        {
            spawnGOLoc = Engine.e.activeParty.gameObject;
        }
        if (battleSystem.currentInQueue == BattleState.CHAR2TURN)
        {
            spawnGOLoc = Engine.e.activePartyMember2;

        }
        if (battleSystem.currentInQueue == BattleState.CHAR3TURN)
        {
            spawnGOLoc = Engine.e.activePartyMember3;
        }

        switch (item.itemName)
        {
            case "Health Potion":


                Engine.e.battleSystem.HandleItemAnim(spawnGOLoc, this.gameObject, item);
                Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, item.itemPower.ToString(), Color.green);

                currentHealth += item.itemPower;

                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }

                break;


            case "Mana Potion":


                Engine.e.battleSystem.HandleItemAnim(spawnGOLoc, this.gameObject, item);
                Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, item.itemPower.ToString(), Color.blue);
                // Destroy(manaSprite, 1f);


                currentMana += item.itemPower;

                if (currentMana > maxMana)
                {
                    currentMana = maxMana;
                }

                break;



            case "Ashes":
                // Zombie status effect instant kill?
                break;

            case "Antidote":


                if (poisonDefense > 100)
                {
                    currentHealth -= 100;

                    if (currentHealth < 0)
                    {
                        currentHealth = 0;

                        Engine.e.battleSystem.CheckIsDeadEnemy();
                    }

                }
                else
                {

                    Engine.e.battleSystem.HandleItemAnim(spawnGOLoc, this.gameObject, item);
                    Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, "Cured!", Color.green);
                    // Destroy(antidote, 1f);
                    isPoisoned = false;
                    inflicted = false;
                }


                break;


        }


        if (!item.targetAll)
        {
            Engine.e.partyInventoryReference.SubtractItemFromInventory(item);
        }

        Engine.e.itemToBeUsed = null;
    }

    public void TakeSkillDamage(float power, int target)
    {
        damageTotal = Mathf.Round(power);
        Engine.e.battleSystem.damageTotal = damageTotal;
        currentHealth -= damageTotal;

        if (!battleSystem.skillRangedAttack)
        {
            if (currentHealth <= 0)
            {
                battleSystem.enemies[target].gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
                battleSystem.enemyUI[target].SetActive(false);
                isPoisoned = false;
                isConfused = false;
                isAsleep = false;
                deathInflicted = false;
                inflicted = false;
                poisonDmg = 0;

                if (GetComponent<Light2D>())
                {
                    GetComponent<Light2D>().enabled = false;
                }

            }
        }

        if (isAsleep)
        {
            isAsleep = false;
            inflicted = false;
        }

        if (isConfused)
        {
            int snapoutChance = Random.Range(0, 100);
            if (confuseDefense > snapoutChance)
            {
                isConfused = false;
                inflicted = false;
                confuseTimer = 0;

                if (GetComponent<EnemyMovement>() != null)
                {
                    GetComponent<EnemyMovement>().enabled = true;
                }
                GetComponentInChildren<SpriteRenderer>().color = Color.white;

            }
        }
    }

    public void InflictBio(int index, float _poisonDmg)
    {
        float poisonChance = Random.Range(0, 100);
        Character character = Engine.e.activeParty.activeParty[index].GetComponent<Character>();

        if (poisonDefense < poisonChance)
        {
            isPoisoned = true;
            inflicted = true;
            float poisonDmgCalculation = (_poisonDmg + (character.shadowDropsLevel * 6) / 2);
            poisonDmg = Mathf.Round((poisonDmgCalculation) - (poisonDmgCalculation * poisonDefense / 100));
            if (!Engine.e.battleSystem.animExists)
            {
                GetComponentInChildren<SpriteRenderer>().color = Color.green;
            }
        }
    }

    public void InflictDeath()
    {
        //Character character = GameManager.gameManager.activeParty.activeParty[index].GetComponent<Character>();


        float deathChance = Random.Range(0, 100);

        if (deathDefense < deathChance)
        {
            Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, "Death", Color.white);

            enemyContext.transform.position = this.transform.position;
            enemyContext.transform.GetChild(0).GetComponent<TextMeshPro>().text = deathTimer.ToString();
            enemyContext.SetActive(true);

            Engine.e.battleSystem.charUsingSkill = false;

            deathInflicted = true;

        }
        else
        {
            Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, "Resisted!", Color.white);
        }
    }

    public void TakeDeathDamage()
    {
        deathTimer--;

        enemyContext.transform.GetChild(0).GetComponent<TextMeshPro>().text = deathTimer.ToString();

        if (deathTimer == 0)
        {
            currentHealth = 0;

            GetComponentInChildren<SpriteRenderer>().enabled = false;
            battleSystem.enemyUI[groupIndex].SetActive(false);

            isPoisoned = false;
            isConfused = false;
            isAsleep = false;
            deathInflicted = false;
            inflicted = false;
            poisonDmg = 0;

            deathTimer = 3;

            enemyContext.transform.GetChild(0).GetComponent<TextMeshPro>().text = deathTimer.ToString();
            enemyContext.SetActive(false);

        }
    }

    public void TakePoisonDamage(int index, float poisonDmg)
    {
        float totalDamage = Mathf.Round((poisonDmg) - (poisonDmg * poisonDefense / 100));
        currentHealth -= totalDamage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }


        if (battleSystem.enemies[index].gameObject.GetComponent<Enemy>().currentHealth <= 0)
        {
            battleSystem.enemies[index].gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            battleSystem.enemyUI[index].SetActive(false);
            battleSystem.enemies[index].GetComponent<Enemy>().isPoisoned = false;
            battleSystem.enemies[index].GetComponent<Enemy>().isConfused = false;
            battleSystem.enemies[index].GetComponent<Enemy>().isAsleep = false;
            battleSystem.enemies[index].GetComponent<Enemy>().poisonDmg = 0;
            battleSystem.enemies[index].GetComponent<Enemy>().deathInflicted = false;
            battleSystem.enemies[index].GetComponent<Enemy>().inflicted = false;


            if (GetComponent<Light2D>())
            {
                GetComponent<Light2D>().enabled = false;
            }
        }

        if (poisonDefense < 100)
        {
            Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, totalDamage.ToString(), Color.white);
        }
        else
        {
            float totalDamageText = totalDamage * -1;
            Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, totalDamageText.ToString(), Color.white);

        }
    }

    public void StealAttempt(int index, float stealChance)
    {

        if (stealableItem != null)
        {
            float steal = Random.Range(0, 100);

            if (steal < stealChance)
            {
                Engine.e.partyInventoryReference.AddItemToInventory(stealableItem);
                Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, "Stole " + stealableItem.itemName + "!", Color.white);
                stealableItem = null;
            }
            else
            {
                Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, "Failed!", Color.white);
            }
        }
        else
        {
            Engine.e.battleSystem.SetDamagePopupTextOne(this.transform.position, "Nothing to Steal!", Color.white);
        }
    }

}

