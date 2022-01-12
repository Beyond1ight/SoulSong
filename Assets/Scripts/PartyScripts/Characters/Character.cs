using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public string characterName;
    public float maxHealth;
    public float currentHealth;
    public float maxMana;
    public float currentMana;
    public float maxEnergy;
    public float currentEnergy;
    public bool healthCapped, manaCapped, energyCapped = true;
    public float haste;
    public float experiencePoints;
    public float levelUpReq;
    public int lvl;
    public float baseDamage;
    public float physicalDamage;
    public bool isInParty;
    public bool isInActiveParty;
    public bool isLeader;
    public bool canUseMagic;
    public int availableSkillPoints;
    public float healthOffset, manaOffset, energyOffset, strengthOffset;

    // Defenses
    public float dodgeChance;
    public float physicalDefense;
    public float fireDefense;
    public float iceDefense;
    public float waterDefense;
    public float lightningDefense;
    public float shadowDefense;
    public float holyDefense;
    public float poisonDefense;
    public float sleepDefense;
    public float confuseDefense;
    public float deathDefense;


    // Drops & Skills
    public Drops[] drops;
    public Skills[] skills;

    public float dropCostReduction = 0f;
    public float skillCostReduction = 0f;
    public float fireDropsLevel, fireDropsExperience, fireDropsLvlReq;
    public float firePhysicalAttackBonus;
    public float fireDropAttackBonus;
    public float iceDropsLevel, iceDropsExperience, iceDropsLvlReq;
    public float icePhysicalAttackBonus;
    public float iceDropAttackBonus;
    public float waterDropsLevel, waterDropsExperience, waterDropsLvlReq;
    public float waterPhysicalAttackBonus;
    public float waterDropAttackBonus;

    public float lightningDropsLevel, lightningDropsExperience, lightningDropsLvlReq;
    public float lightningPhysicalAttackBonus;
    public float lightningDropAttackBonus;

    public float shadowDropsLevel, shadowDropsExperience, shadowDropsLvlReq;
    public float shadowPhysicalAttackBonus;
    public float shadowDropAttackBonus;

    public float holyDropsLevel, holyDropsExperience, holyDropsLvlReq;
    public float holyPhysicalAttackBonus;
    public float holyDropAttackBonus;


    public bool canUseFireDrops = false;
    public bool canUseIceDrops = false;
    public bool canUseHolyDrops = false;
    public bool canUseWaterDrops = false;
    public bool canUseLightningDrops = false;
    public bool canUseShadowDrops = false;

    public float spellPower;
    public float skillPower;
    public Drops lastDropChoice;
    public Skills lastSkillChoice;
    public float skillScale = 1f;
    public float stealChance;
    public float hitChance = 99f;

    public Item weapon;
    public Item chestArmor;
    public int skillIndex;
    public int skillTotal;
    public bool isPoisoned;
    public bool isAsleep;
    public bool isConfused;
    public bool deathInflicted;
    public bool inflicted;
    public float poisonDmg;
    public int sleepTimer = 0;
    public int confuseTimer = 0;
    public int deathTimer = 3;
    public GameObject siphonAnim;
    public GameObject deathTimerPopup;

    public void UseDrop(Drops dropChoice)
    {
        if (Engine.e.inBattle)
        {
            if (Engine.e.battleSystem.state == BattleState.CHAR1TURN)
            {
                if (dropChoice.dps)
                {
                    Engine.e.battleSystem.char1DropAttack = true;
                    Engine.e.battleSystem.char1Attacking = true;
                }
                else
                {
                    Engine.e.battleSystem.char1DropSupport = true;
                    Engine.e.battleSystem.char1Supporting = true;
                }
            }

            if (Engine.e.battleSystem.state == BattleState.CHAR2TURN)
            {
                if (dropChoice.dps)
                {
                    Engine.e.battleSystem.char2DropAttack = true;
                    Engine.e.battleSystem.char2Attacking = true;
                }
                else
                {
                    Engine.e.battleSystem.char2DropSupport = true;
                    Engine.e.battleSystem.char2Supporting = true;
                }
            }

            if (Engine.e.battleSystem.state == BattleState.CHAR3TURN)
            {
                if (dropChoice.dps)
                {
                    Engine.e.battleSystem.char3DropAttack = true;
                    Engine.e.battleSystem.char3Attacking = true;
                }
                else
                {
                    Engine.e.battleSystem.char3DropSupport = true;
                    Engine.e.battleSystem.char3Supporting = true;
                }
            }

            Engine.e.battleSystem.ActivateTargetButtons();
        }
    }

    public void UseSkill(Skills skillChoice)
    {
        if (Engine.e.inBattle)
        {
            if (Engine.e.battleSystem.state == BattleState.CHAR1TURN)
            {
                if (skillChoice.physicalDps)
                {
                    Engine.e.battleSystem.char1SkillPhysicalAttack = true;
                    Engine.e.battleSystem.char1SkillAttack = true;
                    Engine.e.battleSystem.char1Attacking = true;
                }
                if (skillChoice.rangedDps)
                {
                    Engine.e.battleSystem.char1SkillRangedAttack = true;
                    Engine.e.battleSystem.char1SkillAttack = true;
                    Engine.e.battleSystem.char1Attacking = true;
                }
                if (skillChoice.selfSupport)
                {
                    Engine.e.battleSystem.char1SkillSelfSupport = true;
                    Engine.e.battleSystem.char1Supporting = true;
                }
                if (skillChoice.targetSupport)
                {
                    Engine.e.battleSystem.char1SkillTargetSupport = true;
                    Engine.e.battleSystem.char1Supporting = true;
                }
            }

            if (Engine.e.battleSystem.state == BattleState.CHAR2TURN)
            {
                if (skillChoice.physicalDps)
                {
                    Engine.e.battleSystem.char2SkillPhysicalAttack = true;
                    Engine.e.battleSystem.char2SkillAttack = true;
                    Engine.e.battleSystem.char2Attacking = true;
                }
                if (skillChoice.rangedDps)
                {
                    Engine.e.battleSystem.char2SkillRangedAttack = true;
                    Engine.e.battleSystem.char2SkillAttack = true;
                    Engine.e.battleSystem.char2Attacking = true;
                }
                if (skillChoice.selfSupport)
                {
                    Engine.e.battleSystem.char2SkillSelfSupport = true;
                    Engine.e.battleSystem.char2Supporting = true;
                }
                if (skillChoice.targetSupport)
                {
                    Engine.e.battleSystem.char2SkillTargetSupport = true;
                    Engine.e.battleSystem.char2Supporting = true;
                }
            }

            if (Engine.e.battleSystem.state == BattleState.CHAR3TURN)
            {
                if (skillChoice.physicalDps)
                {
                    Engine.e.battleSystem.char3SkillPhysicalAttack = true;
                    Engine.e.battleSystem.char3SkillAttack = true;
                    Engine.e.battleSystem.char3Attacking = true;
                }
                if (skillChoice.rangedDps)
                {
                    Engine.e.battleSystem.char3SkillRangedAttack = true;
                    Engine.e.battleSystem.char3SkillAttack = true;
                    Engine.e.battleSystem.char3Attacking = true;
                }
                if (skillChoice.selfSupport)
                {
                    Engine.e.battleSystem.char3SkillSelfSupport = true;
                    Engine.e.battleSystem.char3Supporting = true;
                }
                if (skillChoice.targetSupport)
                {
                    Engine.e.battleSystem.char3SkillTargetSupport = true;
                    Engine.e.battleSystem.char3Supporting = true;
                }
            }

            Engine.e.battleSystem.ActivateTargetButtons();
        }
    }

    public void Revive(float healPower, int target)
    {
        float healAmount = healPower + (healPower * holyDropsLevel / 2);
        if (Engine.e.activeParty.activeParty[target].GetComponent<Character>().currentHealth <= 0)
        {
            Engine.e.activeParty.activeParty[target].GetComponent<Character>().currentHealth += healAmount;
            Engine.e.battleSystem.damageTotal = healAmount;
            if (Engine.e.activeParty.activeParty[target].GetComponent<Character>().currentHealth > Engine.e.activeParty.activeParty[target].GetComponent<Character>().maxHealth)
            {
                Engine.e.activeParty.activeParty[target].GetComponent<Character>().currentHealth = Engine.e.activeParty.activeParty[target].GetComponent<Character>().maxHealth;
            }
        }
    }

    public void SoothingRain(float healPower)
    {
        Engine.e.weatherRain.SetActive(true);
        float healAmount = healPower + (healPower * waterDropsLevel / 2);
        for (int i = 0; i < Engine.e.activeParty.activeParty.Length; i++)
        {
            if (Engine.e.activeParty.activeParty[i] != null && Engine.e.activeParty.activeParty[i].GetComponent<Character>().currentHealth > 0)
            {
                Engine.e.activeParty.activeParty[i].GetComponent<Character>().currentHealth += healAmount;
                Engine.e.battleSystem.damageTotal = healAmount;
                if (Engine.e.activeParty.activeParty[i].GetComponent<Character>().currentHealth > Engine.e.activeParty.activeParty[i].GetComponent<Character>().maxHealth)
                {
                    Engine.e.activeParty.activeParty[i].GetComponent<Character>().currentHealth = Engine.e.activeParty.activeParty[i].GetComponent<Character>().maxHealth;
                }
            }
        }
    }

    public void InflictDeathAttack()
    {
        GameObject characterLoc = null;
        Character character = Engine.e.activeParty.activeParty[Engine.e.charBeingTargeted].GetComponent<Character>();
        Debug.Log(character.characterName);

        if (Engine.e.charBeingTargeted == 0)
        {
            characterLoc = Engine.e.activeParty.gameObject;
        }
        if (Engine.e.charBeingTargeted == 1)
        {
            characterLoc = Engine.e.activePartyMember2.gameObject;
        }
        if (Engine.e.charBeingTargeted == 2)
        {
            characterLoc = Engine.e.activePartyMember3.gameObject;
        }

        int instantDeath = Random.Range(0, 99);

        float deathChance = Random.Range(0, 100);

        if (!deathInflicted)
        {
            if (deathDefense < deathChance)
            {
                Vector3 deathTimerLoc = new Vector3(characterLoc.transform.position.x, characterLoc.transform.position.y + 1, characterLoc.transform.position.z);
                deathTimerPopup = Instantiate(Engine.e.battleSystem.deathTimerPopup, deathTimerLoc, Quaternion.identity);
                deathTimerPopup.transform.SetParent(characterLoc.gameObject.transform);
                deathTimerPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = character.deathTimer.ToString();
                Engine.e.battleSystem.charUsingSkill = false;

                GameObject result = Instantiate(Engine.e.battleSystem.damagePopup, characterLoc.transform.position, Quaternion.identity);
                character.deathInflicted = true;
                result.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Death";
                Destroy(result, 1f);
            }
            else
            {
                GameObject result = Instantiate(Engine.e.battleSystem.damagePopup, characterLoc.transform.position, Quaternion.identity);
                result.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Resisted";
                Destroy(result, 1f);
            }
        }
        else
        {
            GameObject result = Instantiate(Engine.e.battleSystem.damagePopup, characterLoc.transform.position, Quaternion.identity);
            result.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Resisted";
            Destroy(result, 1f);
        }
    }

    public bool TakeDeathDamage(int index)
    {

        deathTimer--;
        deathTimerPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = deathTimer.ToString();

        if (deathTimer == 0)
        {
            currentHealth = 0;

            isPoisoned = false;
            isConfused = false;
            isAsleep = false;
            deathInflicted = false;
            poisonDmg = 0;
            deathTimer = 3;

            Destroy(deathTimerPopup);

            Engine.e.battleSystem.hud.displayHealth[index].text = currentHealth.ToString();

            if (currentHealth <= 0)
            {
                if (Engine.e.activeParty.activeParty[2] != null && Engine.e.activeParty.activeParty[1].gameObject.GetComponent<Character>().currentHealth <= 0
                && Engine.e.activeParty.activeParty[2].gameObject.GetComponent<Character>().currentHealth <= 0 && Engine.e.activeParty.activeParty[0].gameObject.GetComponent<Character>().currentHealth <= 0)
                {
                    return true;
                }

                if (Engine.e.activeParty.activeParty[2] == null && Engine.e.activeParty.activeParty[1] != null)
                {
                    if (Engine.e.activeParty.activeParty[1].gameObject.GetComponent<Character>().currentHealth <= 0
                    && Engine.e.activeParty.activeParty[0].gameObject.GetComponent<Character>().currentHealth <= 0)
                    {

                        return true;

                    }
                }

                if (Engine.e.activeParty.activeParty[2] == null && Engine.e.activeParty.activeParty[1] == null && Engine.e.activeParty.activeParty[0].gameObject.GetComponent<Character>().currentHealth <= 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void RemoveDeath()
    {
        deathInflicted = false;
        deathTimer = 3;
        Destroy(deathTimerPopup);

    }

    public void ResetDropChoice()
    {
        spellPower = 0;
    }

    public void ResetSkillChoice()
    {
        skillPower = 0;
    }

    public Drops GetDropChoice()
    {
        return lastDropChoice;
    }

    public float GetFireDropLevel()
    {
        return fireDropsLevel;
    }

    public bool KnowsSkill(Skills _skill)
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i] == _skill)
            {
                return true;
            }
        }
        return false;
    }

    public bool KnowsDrop(Drops _drop)
    {
        for (int i = 0; i < drops.Length; i++)
        {
            if (drops[i] == _drop)
            {
                return true;
            }
        }
        return false;
    }

    public void TeachDrop(Drops _drop)
    {
        drops[_drop.dropIndex] = _drop;
        _drop.isKnown = true;
    }
    public void TeachSkill(Skills _skill)
    {
        skills[_skill.skillIndex] = _skill;
        _skill.isKnown = true;
    }

    public void DropEffect(Drops dropChoice)
    {
        float dropValueOutcome = 0;
        int currentIndex = 0;
        Character characterAttacking = null;
        Enemy enemyAttacking = null;
        bool teamAttack = false; // If "false," an active party member is attacking 
        float damageTotal = 0;

        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR1TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR1)
        {
            currentIndex = 0;
            characterAttacking = Engine.e.activeParty.activeParty[0].GetComponent<Character>();
            teamAttack = true;
        }
        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR2TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR2)
        {
            currentIndex = 1;
            characterAttacking = Engine.e.activeParty.activeParty[1].GetComponent<Character>();
            teamAttack = true;
        }
        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR3TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR3)
        {
            currentIndex = 2;
            characterAttacking = Engine.e.activeParty.activeParty[2].GetComponent<Character>();
            teamAttack = true;
        }

        if (Engine.e.battleSystem.currentInQueue == BattleState.ENEMY1TURN)
        {
            currentIndex = 0;
        }

        if (Engine.e.battleSystem.currentInQueue == BattleState.ENEMY2TURN)
        {
            currentIndex = 1;
        }

        if (Engine.e.battleSystem.currentInQueue == BattleState.ENEMY3TURN)
        {
            currentIndex = 2;
        }

        if (Engine.e.battleSystem.currentInQueue == BattleState.ENEMY4TURN)
        {
            currentIndex = 3;
        }

        if (teamAttack)
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
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.fireDropsLevel / 2)) + characterAttacking.fireDropAttackBonus);
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * fireDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
                characterAttacking.GetDropExperience(dropChoice);
            }
            else
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.fireDropsLevel / 2)));
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * fireDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
            }
        }

        if (dropChoice.dropType == "Ice")
        {
            if (teamAttack)
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.iceDropsLevel / 2)) + characterAttacking.iceDropAttackBonus);
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * iceDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
                characterAttacking.GetDropExperience(dropChoice);
            }
            else
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.iceDropsLevel / 2)));
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * iceDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
            }
        }

        if (dropChoice.dropType == "Lightning")
        {
            // Instantiate(battleSystem.fireDropAnim, currentLocation.transform.position, Quaternion.identity);

            if (teamAttack)
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.lightningDropsLevel / 2)) + characterAttacking.lightningDropAttackBonus);
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * lightningDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
                characterAttacking.GetDropExperience(dropChoice);
            }
            else
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.lightningDropsLevel / 2)));
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * lightningDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
            }
        }

        if (dropChoice.dropType == "Water")
        {
            if (teamAttack)
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.waterDropsLevel / 2)) + characterAttacking.waterDropAttackBonus);
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * waterDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
                characterAttacking.GetDropExperience(dropChoice);
            }
            else
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.waterDropsLevel / 2)));
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * waterDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
            }
        }

        if (dropChoice.dropType == "Shadow")
        {
            if (teamAttack)
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.shadowDropsLevel / 2)) + characterAttacking.shadowDropAttackBonus);
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * shadowDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
                characterAttacking.GetDropExperience(dropChoice);
            }
            else
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.shadowDropsLevel / 2)));
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * shadowDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
            }

            switch (dropChoice.dropName)
            {
                case "Bio":

                    //InflictPoisonAttack(currentIndex, battleSystem.lastDropChoice.dotDmg);

                    break;
                case "Knockout":
                    if (!isAsleep)
                    {
                        float sleepChance = Random.Range(0, 100);

                        if (sleepDefense < sleepChance)
                        {
                            isAsleep = true;
                            sleepTimer = 0;
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
                        }
                    }
                    break;
            }
        }

        if (dropChoice.dropType == "Holy")
        {
            if (teamAttack)
            {
                characterAttacking.GetDropExperience(dropChoice);

                switch (dropChoice.dropName)
                {
                    case "Holy Light":
                        dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.holyDropsLevel / 2)));
                        damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * holyDefense / 100));
                        currentHealth += damageTotal;

                        if (currentHealth >= maxHealth)
                        {
                            currentHealth = maxHealth;
                        }

                        break;
                    case "Repent":

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
                        inflicted = false;

                        break;

                }
            }
            else
            {
                switch (dropChoice.dropName)
                {
                    case "Holy Light":
                        dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.holyDropsLevel / 2)));
                        damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * holyDefense / 100));

                        currentHealth += damageTotal;

                        if (currentHealth >= maxHealth)
                        {
                            currentHealth = maxHealth;
                        }

                        break;
                    case "Repent":
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

                        inflicted = false;
                        break;
                }
            }
        }


        Engine.e.battleSystem.damageTotal = damageTotal;

        if (isAsleep)
        {
            if (dropChoice.dropName != "Knockout" && dropChoice.dropName != "Bio" && dropChoice.dropName != "Blind")
            {
                isAsleep = false;
                isConfused = false;
                inflicted = false;

                if (GetComponent<EnemyMovement>() != null)
                {
                    GetComponent<EnemyMovement>().enabled = true;
                }
                GetComponent<SpriteRenderer>().color = Color.grey;
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
                GetComponent<SpriteRenderer>().color = Color.white;
            }

            if (dropChoice.dropName == "Knockout")
            {
                isAsleep = true;
                isConfused = false;
                if (GetComponent<EnemyMovement>() != null)
                {
                    GetComponent<EnemyMovement>().enabled = false;
                }


                GetComponent<SpriteRenderer>().color = Color.grey;
            }
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (!Engine.e.battleSystem.animExists)
        {
            if (currentHealth <= 0)
            {

                isPoisoned = false;
                isConfused = false;
                isAsleep = false;
                deathInflicted = false;
                inflicted = false;
                poisonDmg = 0;
            }
        }
    }

    public void GetDropExperience(Drops _dropChoice)
    {
        if (_dropChoice.dropType == "Fire")
        {
            if (fireDropsLevel < 99)
            {
                fireDropsExperience += _dropChoice.experiencePoints;
                // Level Up
                if (fireDropsExperience >= fireDropsLvlReq)
                {
                    fireDropsLevel += 1f;
                    fireDropsExperience -= fireDropsLvlReq;
                    fireDropsLvlReq = (fireDropsLvlReq * 3 / 2);
                    fireDefense += 0.5f;
                    GameObject levelUp = Instantiate(Engine.e.battleSystem.levelUpPopup, transform.position, Quaternion.identity);
                    levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Fire Lvl Up! (Lvl: " + fireDropsLevel + ")";
                    //levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(212, 1, 1, 255);
                    Destroy(levelUp, 2f);
                }
            }
        }

        if (_dropChoice.dropType == "Ice")
        {
            if (iceDropsLevel < 99)
            {
                iceDropsExperience += _dropChoice.experiencePoints;
                // Level Up
                if (iceDropsExperience >= iceDropsLvlReq)
                {
                    iceDropsLevel += 1f;
                    iceDropsExperience -= iceDropsLvlReq;
                    iceDropsLvlReq = (iceDropsLvlReq * 3 / 2);
                    iceDefense += 0.5f;
                    GameObject levelUp = Instantiate(Engine.e.battleSystem.levelUpPopup, transform.position, Quaternion.identity);
                    levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Ice Lvl Up! (Lvl: " + iceDropsLevel + ")";
                    //levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(212, 1, 1, 255);
                    Destroy(levelUp, 2f);
                }
            }
        }

        if (_dropChoice.dropType == "Lightning")
        {
            if (lightningDropsLevel < 99)
            {
                lightningDropsExperience += _dropChoice.experiencePoints;
                // Level Up
                if (lightningDropsExperience >= lightningDropsLvlReq)
                {
                    lightningDropsLevel += 1f;
                    lightningDropsExperience -= fireDropsLvlReq;
                    lightningDropsLvlReq = (fireDropsLvlReq * 3 / 2);
                    lightningDefense += 0.5f;
                    GameObject levelUp = Instantiate(Engine.e.battleSystem.levelUpPopup, transform.position, Quaternion.identity);
                    levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Lightning Lvl Up! (Lvl: " + lightningDropsLevel + ")";
                    //levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(212, 1, 1, 255);
                    Destroy(levelUp, 2f);
                }
            }
        }

        if (_dropChoice.dropType == "Water")
        {
            if (waterDropsLevel < 99)
            {
                waterDropsExperience += _dropChoice.experiencePoints;
                // Level Up
                if (waterDropsExperience >= waterDropsLvlReq)
                {
                    waterDropsLevel += 1f;
                    waterDropsExperience -= waterDropsLvlReq;
                    waterDropsLvlReq = (waterDropsLvlReq * 3 / 2);
                    waterDefense += 0.5f;
                    GameObject levelUp = Instantiate(Engine.e.battleSystem.levelUpPopup, transform.position, Quaternion.identity);
                    levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Water Lvl Up! (Lvl: " + waterDropsLevel + ")";
                    //levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(212, 1, 1, 255);
                    Destroy(levelUp, 2f);
                }
            }
        }

        if (_dropChoice.dropType == "Shadow")
        {
            if (shadowDropsLevel < 99)
            {
                shadowDropsExperience += _dropChoice.experiencePoints;
                // Level Up
                if (shadowDropsExperience >= shadowDropsLvlReq)
                {
                    shadowDropsLevel += 1f;
                    shadowDropsExperience -= shadowDropsLvlReq;
                    shadowDropsLvlReq = (shadowDropsLvlReq * 3 / 2);
                    shadowDefense += 0.5f;
                    GameObject levelUp = Instantiate(Engine.e.battleSystem.levelUpPopup, transform.position, Quaternion.identity);
                    levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Shadow Lvl Up! (Lvl: " + shadowDropsLevel + ")";
                    //levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(212, 1, 1, 255);
                    Destroy(levelUp, 2f);
                }
            }
        }

        if (_dropChoice.dropType == "Holy")
        {
            if (holyDropsLevel < 99)
            {
                holyDropsExperience += _dropChoice.experiencePoints;
                // Level Up
                if (holyDropsExperience >= holyDropsLvlReq)
                {
                    holyDropsLevel += 1f;
                    holyDropsExperience -= holyDropsLvlReq;
                    holyDropsLvlReq = (holyDropsLvlReq * 3 / 2);
                    holyDefense += 0.5f;
                    GameObject levelUp = Instantiate(Engine.e.battleSystem.levelUpPopup, transform.position, Quaternion.identity);
                    levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Holy Lvl Up! (Lvl: " + holyDropsLevel + ")";
                    //levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(212, 1, 1, 255);
                    Destroy(levelUp, 2f);
                }
            }
        }
    }
}
