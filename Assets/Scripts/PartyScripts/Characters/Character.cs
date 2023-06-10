using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    // General Information
    public string characterName;
    public bool[] characterClass;
    public float[] classEXP;
    public string currentClass;
    public bool[] classUnlocked;
    public int lvl;
    public float currentHealth, maxHealth, currentMana, maxMana, currentEnergy, maxEnergy, haste, experiencePoints, levelUpReq,
    strength, intelligence, dropCostReduction, skillCostReduction,
    fireDropsLevel, fireDropsExperience, fireDropsLvlReq, firePhysicalAttackBonus, fireDropAttackBonus,
    iceDropsLevel, iceDropsExperience, iceDropsLvlReq, icePhysicalAttackBonus, iceDropAttackBonus,
    waterDropsLevel, waterDropsExperience, waterDropsLvlReq, waterPhysicalAttackBonus, waterDropAttackBonus,
    lightningDropsLevel, lightningDropsExperience, lightningDropsLvlReq, lightningPhysicalAttackBonus, lightningDropAttackBonus,
    shadowDropsLevel, shadowDropsExperience, shadowDropsLvlReq, shadowPhysicalAttackBonus, shadowDropAttackBonus,
    holyDropsLevel, holyDropsExperience, holyDropsLvlReq, holyPhysicalAttackBonus, holyDropAttackBonus,
    skillScale;
    public bool healthCapped, manaCapped, energyCapped = true;
    public bool canUse2HWeapon, canDualWield = false;
    public bool canSelectNewClass = false;

    // Negative Status Effects
    public bool isInParty, isInActiveParty, isLeader, isPoisoned, isAsleep, isConfused, miterInflicted, haltInflicted, deathInflicted, inflicted;

    // Beneficial Status Effects
    public bool isProtected, isEncompassed, isHastened = false;

    public int availableSkillPoints;
    public float healthOffset, manaOffset, energyOffset, strengthOffset, intelligenceOffset, stealChance;
    // Defenses
    public float physicalDefense, fireDefense, iceDefense, waterDefense, lightningDefense, shadowDefense, holyDefense,
    poisonDefense, sleepDefense, confuseDefense, deathDefense, dodgeChance, hitChance, critChance;

    // Default values (before any boosts). Mainly to reset values after status effects wear off
    public float maxHealthBase, maxManaBase, maxEnergyBase, strengthBase, intelligenceBase, dodgeChanceBase, critChanceBase,
    hasteBase, firePhysicalAttackBonusBase, icePhysicalAttackBonusBase, lightningPhysicalAttackBonusBase, waterPhysicalAttackBonusBase,
    shadowPhysicalAttackBonusBase, holyPhysicalAttackBonusBase, fireDefenseBase, iceDefenseBase, lightningDefenseBase, waterDefenseBase,
    shadowDefenseBase, holyDefenseBase, sleepDefenseBase, poisonDefenseBase, confuseDefenseBase, deathDefenseBase;


    // Drops & Skills
    public Drops[] drops;
    public Skills[] skills;


    public bool canUseFireDrops, canUseIceDrops, canUseLightningDrops, canUseWaterDrops, canUseShadowDrops, canUseHolyDrops = false;

    public Item weaponRight, weaponLeft, chestArmor, legArmor, accessory1, accessory2;
    public int skillIndex;
    public int skillTotal;
    public float poisonDmg;
    public int sleepTimer, confuseTimer = 0, deathTimer = 3;
    public int activePartyIndex, partyIndex; // activePartyIndex is Mostly for battles
    public bool inSwitchQueue = false;

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

            if (dropChoice.targetAll)
            {
                Engine.e.battleSystem.targetAll = true;
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

                    if (skillChoice.skillIndex == 25)
                    {
                        Engine.e.battleSystem.charSkillSwitchCheck = true;
                        Engine.e.battleSystem.ActivateAvailableCharSwitchButtons();
                    }
                }
            }

            if (Engine.e.battleSystem.state == BattleState.CHAR2TURN)
            {
                if (skillChoice.physicalDps)
                {
                    Engine.e.battleSystem.char2SkillPhysicalAttack = true;
                    Engine.e.battleSystem.char2SkillAttack = true;
                    Engine.e.battleSystem.char2Attacking = true;
                    if (skillChoice.skillIndex == 1)
                    {
                        Engine.e.battleSystem.ActivateTargetSpriteEnemiesAll();
                    }
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

                    if (skillChoice.skillIndex == 25)
                    {
                        Engine.e.battleSystem.charSkillSwitchCheck = true;
                        Engine.e.battleSystem.ActivateAvailableCharSwitchButtons();
                    }
                }
            }

            if (Engine.e.battleSystem.state == BattleState.CHAR3TURN)
            {
                if (skillChoice.physicalDps)
                {
                    Engine.e.battleSystem.char3SkillPhysicalAttack = true;
                    Engine.e.battleSystem.char3SkillAttack = true;
                    Engine.e.battleSystem.char3Attacking = true;

                    if (skillChoice.skillIndex == 1)
                    {
                        Engine.e.battleSystem.ActivateTargetSpriteEnemiesAll();
                    }
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

                    if (skillChoice.skillIndex == 25)
                    {
                        Engine.e.battleSystem.charSkillSwitchCheck = true;
                        Engine.e.battleSystem.ActivateAvailableCharSwitchButtons();
                    }
                }
            }

            if (skillChoice.targetAll)
            {
                Engine.e.battleSystem.targetAll = true;
            }

            Engine.e.battleSystem.ActivateTargetButtons();
        }
    }

    public void SetBattleDamagePopupText(string _textDisplayed, Color _color)
    {
        if (activePartyIndex == 0)
        {
            Engine.e.battleSystem.damagePopup[0].transform.position = Engine.e.activeParty.gameObject.transform.position;
            Engine.e.battleSystem.damagePopup[0].transform.GetChild(0).GetComponent<TextMeshPro>().color = _color;
            Engine.e.battleSystem.damagePopup[0].transform.GetChild(0).GetComponent<TextMeshPro>().text = _textDisplayed;

            Engine.e.battleSystem.dmgText1Active = true;
        }
        if (activePartyIndex == 1)
        {
            Engine.e.battleSystem.damagePopup[1].transform.position = Engine.e.activePartyMember2.transform.position;
            Engine.e.battleSystem.damagePopup[1].transform.GetChild(0).GetComponent<TextMeshPro>().color = _color;
            Engine.e.battleSystem.damagePopup[1].transform.GetChild(0).GetComponent<TextMeshPro>().text = _textDisplayed;

            Engine.e.battleSystem.dmgText2Active = true;
        }
        if (activePartyIndex == 2)
        {
            Engine.e.battleSystem.damagePopup[2].transform.position = Engine.e.activePartyMember3.transform.position;
            Engine.e.battleSystem.damagePopup[2].transform.GetChild(0).GetComponent<TextMeshPro>().color = _color;
            Engine.e.battleSystem.damagePopup[2].transform.GetChild(0).GetComponent<TextMeshPro>().text = _textDisplayed;

            Engine.e.battleSystem.dmgText3Active = true;
        }
    }

    public void InflictDeath()
    {
        GameObject thisCharacterGOLoc = null;

        if (activePartyIndex == 0)
        {
            thisCharacterGOLoc = Engine.e.activeParty.gameObject;
        }
        if (activePartyIndex == 1)
        {
            thisCharacterGOLoc = Engine.e.activePartyMember2;
        }
        if (activePartyIndex == 2)
        {
            thisCharacterGOLoc = Engine.e.activePartyMember3;
        }

        int instantDeath = Random.Range(0, 99);

        float deathChance = Random.Range(0, 100);

        if (!deathInflicted)
        {
            if (deathDefense < deathChance)
            {
                Engine.e.battleSystem.SetDamagePopupTextOne(thisCharacterGOLoc.transform.position, "Death", Color.white);

                Engine.e.battleSystem.charUsingSkill = false;

                if (activePartyIndex == 0)
                {
                    Engine.e.battleSystem.char1Context.transform.position = thisCharacterGOLoc.transform.position;
                    Engine.e.battleSystem.char1Context.transform.GetChild(0).GetComponent<TextMeshPro>().text = deathTimer.ToString();
                    Engine.e.battleSystem.char1Context.SetActive(true);
                }
                if (activePartyIndex == 1)
                {
                    Engine.e.battleSystem.char2Context.transform.position = thisCharacterGOLoc.transform.position;
                    Engine.e.battleSystem.char2Context.transform.GetChild(0).GetComponent<TextMeshPro>().text = deathTimer.ToString();
                    Engine.e.battleSystem.char2Context.SetActive(true);
                }
                if (activePartyIndex == 2)
                {
                    Engine.e.battleSystem.char3Context.transform.position = thisCharacterGOLoc.transform.position;
                    Engine.e.battleSystem.char3Context.transform.GetChild(0).GetComponent<TextMeshPro>().text = deathTimer.ToString();
                    Engine.e.battleSystem.char3Context.SetActive(true);
                }

                deathInflicted = true;
            }
            else
            {
                Engine.e.battleSystem.SetDamagePopupTextOne(thisCharacterGOLoc.transform.position, "Resisted!", Color.white);
            }
        }
        else
        {
            Engine.e.battleSystem.SetDamagePopupTextOne(thisCharacterGOLoc.transform.position, "Resisted!", Color.white);
        }
    }

    public bool TakeDeathDamage()
    {

        deathTimer--;

        if (activePartyIndex == 0)
        {
            Engine.e.battleSystem.char1Context.transform.GetChild(0).GetComponent<TextMeshPro>().text = deathTimer.ToString();
        }
        if (activePartyIndex == 1)
        {
            Engine.e.battleSystem.char2Context.transform.GetChild(0).GetComponent<TextMeshPro>().text = deathTimer.ToString();
        }
        if (activePartyIndex == 2)
        {
            Engine.e.battleSystem.char3Context.transform.GetChild(0).GetComponent<TextMeshPro>().text = deathTimer.ToString();
        }

        if (deathTimer == 0)
        {
            currentHealth = 0;

            isPoisoned = false;
            isConfused = false;
            isAsleep = false;
            deathInflicted = false;
            poisonDmg = 0;
            deathTimer = 3;

            if (activePartyIndex == 0)
            {
                Engine.e.battleSystem.char1Context.transform.GetChild(0).GetComponent<TextMeshPro>().text = deathTimer.ToString();
                Engine.e.battleSystem.char1Context.SetActive(false);
            }
            if (activePartyIndex == 1)
            {
                Engine.e.battleSystem.char2Context.transform.GetChild(0).GetComponent<TextMeshPro>().text = deathTimer.ToString();
                Engine.e.battleSystem.char2Context.SetActive(false);
            }
            if (activePartyIndex == 2)
            {
                Engine.e.battleSystem.char3Context.transform.GetChild(0).GetComponent<TextMeshPro>().text = deathTimer.ToString();
                Engine.e.battleSystem.char3Context.SetActive(false);
            }

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

    public bool TakePhysicalDamage(int index, float _dmg)
    {
        float _crit = 0;
        float adjustedDodge = Mathf.Round(hitChance - dodgeChance);
        int hit = Random.Range(0, 100);
        bool teamAttack = false;
        Character attackingCharacter = null;
        GameObject thisCharacterGOLoc = null;
        float adjustedCritChance = 0;

        float elementFireDamageBonus = 0;
        float elementWaterDamageBonus = 0;
        float elementLightningDamageBonus = 0;
        float elementShadowDamageBonus = 0;
        float elementIceDamageBonus = 0;

        if (activePartyIndex == 0)
        {
            thisCharacterGOLoc = Engine.e.activeParty.gameObject;
        }
        if (activePartyIndex == 1)
        {
            thisCharacterGOLoc = Engine.e.activePartyMember2;
        }
        if (activePartyIndex == 2)
        {
            thisCharacterGOLoc = Engine.e.activePartyMember3;
        }

        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR1TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR1)
        {
            attackingCharacter = Engine.e.activeParty.activeParty[0].GetComponent<Character>();
            teamAttack = true;

        }
        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR2TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR2)
        {
            attackingCharacter = Engine.e.activeParty.activeParty[1].GetComponent<Character>();
            teamAttack = true;
        }
        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR3TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR3)
        {
            attackingCharacter = Engine.e.activeParty.activeParty[2].GetComponent<Character>();
            teamAttack = true;
        }

        if (teamAttack)
        {
            adjustedCritChance = attackingCharacter.critChance;

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
        }

        _crit = Random.Range(0, 100);

        if (teamAttack)
        {
            if (_crit < adjustedCritChance)
            {
                float critDamage = Mathf.Round((_dmg + (_dmg * (2 / 3))));
                _dmg = _dmg + critDamage;
            }
        }
        else
        {
            if (_crit <= 10)
            {
                float critDamage = Mathf.Round((_dmg + (_dmg * (2 / 3))));
                _dmg = _dmg + critDamage;
            }
        }

        float adjustedPhysicalDmg = Mathf.Round((_dmg) - (_dmg * (physicalDefense / 100)));

        if (teamAttack) // Consider removing this
        {
            adjustedPhysicalDmg = Random.Range(adjustedPhysicalDmg, (adjustedPhysicalDmg + attackingCharacter.lvl));
        }

        if (isAsleep)
        {
            adjustedDodge = 100;
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
                // activeParty.GetComponent<SpriteRenderer>().color = Color.white;

                //  GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        if (hit < adjustedDodge)
        {
            if (teamAttack)
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
            currentHealth -= adjustedPhysicalDmg;
            Engine.e.battleSystem.damageTotal = adjustedPhysicalDmg;
            Engine.e.battleSystem.SetDamagePopupTextOne(thisCharacterGOLoc.transform.position, adjustedPhysicalDmg.ToString(), Color.white);
        }
        else
        {
            Engine.e.battleSystem.dodgedAttack = true;
            adjustedPhysicalDmg = 0;
            Engine.e.battleSystem.SetDamagePopupTextOne(thisCharacterGOLoc.transform.position, "Dodged!", Color.white);

        }



        if (currentHealth <= 0)
        {
            currentHealth = 0;

            isPoisoned = false;
            isConfused = false;
            isAsleep = false;
            inflicted = false;
            poisonDmg = 0f;
            // activeParty.activeParty[index].GetComponent<SpriteRenderer>().color = Color.white;

            switch (index)
            {
                case 0:
                    Engine.e.battleSystem.char1ATB = 0;
                    Engine.e.battleSystem.char1ATBGuage.value = 0;

                    Engine.e.battleSystem.DeactivateChar1MenuButtons();

                    if (Engine.e.battleSystem.state == BattleState.CHAR1TURN)
                    {
                        Engine.e.battleSystem.state = BattleState.ATBCHECK;
                    }

                    break;
                case 1:
                    Engine.e.battleSystem.char2ATB = 0;
                    Engine.e.battleSystem.char2ATBGuage.value = 0;

                    Engine.e.battleSystem.DeactivateChar2MenuButtons();

                    if (Engine.e.battleSystem.state == BattleState.CHAR2TURN)
                    {
                        Engine.e.battleSystem.state = BattleState.ATBCHECK;
                    }
                    break;
                case 2:
                    Engine.e.battleSystem.char3ATB = 0;
                    Engine.e.battleSystem.char3ATBGuage.value = 0;

                    Engine.e.battleSystem.DeactivateChar3MenuButtons();

                    if (Engine.e.battleSystem.state == BattleState.CHAR3TURN)
                    {
                        Engine.e.battleSystem.state = BattleState.ATBCHECK;
                    }
                    break;
            }
        }

        //.battleSystem.hud.displayHealth[battleSystem.previousTargetReferenceEnemy].text = activeParty.activeParty[battleSystem.previousTargetReferenceEnemy].gameObject.GetComponent<Character>().currentHealth.ToString();


        if (Engine.e.activeParty.activeParty[0].gameObject.GetComponent<Character>().currentHealth <= 0)
        {
            if (Engine.e.activeParty.activeParty[2] != null && Engine.e.activeParty.activeParty[1].gameObject.GetComponent<Character>().currentHealth <= 0
            && Engine.e.activeParty.activeParty[2].gameObject.GetComponent<Character>().currentHealth <= 0)
            {
                return true;
            }

            if (Engine.e.activeParty.activeParty[2] == null && Engine.e.activeParty.activeParty[1] != null)
            {
                if (Engine.e.activeParty.activeParty[1].gameObject.GetComponent<Character>().currentHealth <= 0)
                {
                    return true;

                }
            }

            if (Engine.e.activeParty.activeParty[2] == null && Engine.e.activeParty.activeParty[1] == null)
            {
                return true;
            }
        }
        return false;
    }

    public void InflictBio(float _poisonDmg)
    {
        float poisonChance = Random.Range(0, 100);

        if (poisonDefense < poisonChance)
        {
            isPoisoned = true;
            inflicted = true;

            poisonDmg = Mathf.Round((_poisonDmg) - (_poisonDmg * poisonDefense / 100));

        }
    }

    public bool TakePoisonDamage(float poisonDmg)
    {
        GameObject thisCharacterGOLoc = null;

        if (activePartyIndex == 0)
        {
            thisCharacterGOLoc = Engine.e.activeParty.gameObject;
        }
        if (activePartyIndex == 1)
        {
            thisCharacterGOLoc = Engine.e.activePartyMember2;
        }
        if (activePartyIndex == 2)
        {
            thisCharacterGOLoc = Engine.e.activePartyMember3;
        }

        float totalDamage = Mathf.Round((poisonDmg) - (poisonDmg * poisonDefense / 100));
        currentHealth -= totalDamage;

        if (poisonDefense < 100)
        {
            Engine.e.battleSystem.SetDamagePopupTextOne(thisCharacterGOLoc.transform.position, totalDamage.ToString(), Color.white);
        }
        else
        {
            float totalDamageText = totalDamage * -1;
            Engine.e.battleSystem.SetDamagePopupTextOne(thisCharacterGOLoc.transform.position, totalDamageText.ToString(), Color.green);

        }
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isPoisoned = false;
            isConfused = false;
            isAsleep = false;
            poisonDmg = 0f;
            inflicted = false;
            GetComponent<SpriteRenderer>().color = Color.white;

        }

        //Engine.e.battleSystem.hud.displayHealth[index].text = currentHealth.ToString();

        if (currentHealth <= 0)
        {
            if (Engine.e.activeParty.activeParty[2] != null && Engine.e.activeParty.activeParty[1].gameObject.GetComponent<Character>().currentHealth <= 0
            && Engine.e.activeParty.activeParty[2].gameObject.GetComponent<Character>().currentHealth <= 0)
            {
                return true;
            }

            if (Engine.e.activeParty.activeParty[2] == null && Engine.e.activeParty.activeParty[1] != null)
            {
                if (Engine.e.activeParty.activeParty[1].gameObject.GetComponent<Character>().currentHealth <= 0)
                {

                    return true;

                }
            }

            if (Engine.e.activeParty.activeParty[2] == null && Engine.e.activeParty.activeParty[1] == null)
            {
                return true;
            }
        }
        return false;
    }

    public void DropEffect(Drops dropChoice)
    {
        float dropValueOutcome = 0;
        int currentIndex = 0;
        Character characterAttacking = null;
        GameObject thisCharacterGOLoc = null;
        Enemy enemyAttacking = null;
        bool teamAttack = false; // If "false," an active party member is attacking 
        float damageTotal = 0;
        float characterIntelligenceScale = 0f;

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

        if (activePartyIndex == 0)
        {
            thisCharacterGOLoc = Engine.e.activeParty.gameObject;
        }
        if (activePartyIndex == 1)
        {
            thisCharacterGOLoc = Engine.e.activePartyMember2;
        }
        if (activePartyIndex == 2)
        {
            thisCharacterGOLoc = Engine.e.activePartyMember3;
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
            characterIntelligenceScale = characterAttacking.intelligence * 2 / 6;
        }
        else
        {
            enemyAttacking = Engine.e.battleSystem.enemies[currentIndex].GetComponent<Enemy>();
        }

        if (dropChoice.dropType == "Fire")
        {
            if (teamAttack)
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.fireDropsLevel / 2) * characterIntelligenceScale) + characterAttacking.fireDropAttackBonus);
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * fireDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
                SetBattleDamagePopupText(dropValueOutcome.ToString(), Color.white);

            }
            else
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.fireDropsLevel / 2)));
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * fireDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
                SetBattleDamagePopupText(dropValueOutcome.ToString(), Color.white);

            }
        }

        if (dropChoice.dropType == "Ice")
        {
            if (teamAttack)
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.iceDropsLevel / 2) * characterIntelligenceScale) + characterAttacking.iceDropAttackBonus);
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * iceDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
                SetBattleDamagePopupText(dropValueOutcome.ToString(), Color.white);

            }
            else
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.iceDropsLevel / 2)));
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * iceDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
                SetBattleDamagePopupText(dropValueOutcome.ToString(), Color.white);

            }
        }

        if (dropChoice.dropType == "Lightning")
        {
            // Instantiate(battleSystem.fireDropAnim, currentLocation.transform.position, Quaternion.identity);

            if (teamAttack)
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.lightningDropsLevel / 2) * characterIntelligenceScale) + characterAttacking.lightningDropAttackBonus);
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * lightningDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
                SetBattleDamagePopupText(dropValueOutcome.ToString(), Color.white);
            }
            else
            {
                dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.lightningDropsLevel / 2)));
                damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * lightningDefense / 100));
                currentHealth -= Mathf.Round(damageTotal);
                SetBattleDamagePopupText(dropValueOutcome.ToString(), Color.white);
            }
        }

        if (dropChoice.dropType == "Water")
        {
            if (dropChoice.dropName == "Bloom")
            {
                if (teamAttack)
                {
                    dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.waterDropsLevel / 2) * characterIntelligenceScale));
                    damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * waterDefense / 100));
                    currentHealth += damageTotal;

                }
                else
                {
                    dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.waterDropsLevel / 2)));
                    damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * waterDefense / 100));
                    currentHealth += damageTotal;
                }

                SetBattleDamagePopupText(damageTotal.ToString(), Color.green);

                if (currentHealth >= maxHealth)
                {
                    currentHealth = maxHealth;
                }
            }
            else
            {
                if (teamAttack)
                {

                    dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.waterDropsLevel / 2) * characterIntelligenceScale) + characterAttacking.waterDropAttackBonus);
                    damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * waterDefense / 100));
                    currentHealth -= Mathf.Round(damageTotal);
                    SetBattleDamagePopupText(dropValueOutcome.ToString(), Color.white);
                }
                else
                {
                    dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.waterDropsLevel / 2)));
                    damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * waterDefense / 100));
                    currentHealth -= Mathf.Round(damageTotal);
                    SetBattleDamagePopupText(dropValueOutcome.ToString(), Color.white);
                }
            }
        }

        if (dropChoice.dropType == "Shadow")
        {
            switch (dropChoice.dropName)
            {
                case "Dark Embrace":
                    if (teamAttack)
                    {
                        dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.shadowDropsLevel / 2) * characterIntelligenceScale) + characterAttacking.shadowDropAttackBonus);
                        damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * shadowDefense / 100));
                        currentHealth -= Mathf.Round(damageTotal);
                    }
                    else
                    {
                        dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.shadowDropsLevel / 2)));
                        damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * shadowDefense / 100));
                        currentHealth -= Mathf.Round(damageTotal);
                    }
                    SetBattleDamagePopupText(dropValueOutcome.ToString(), Color.white);
                    break;
                case "Bio":

                    float poisonChance = Random.Range(0, 100);

                    if (poisonDefense < poisonChance)
                    {
                        isPoisoned = true;
                        inflicted = true;

                        if (teamAttack)
                        {
                            float poisonDmgCalculation = Mathf.Round(dropChoice.dropPower + (characterAttacking.shadowDropsLevel * 6) / 2);
                            poisonDmg = ((poisonDmgCalculation) - (poisonDmgCalculation * poisonDefense / 100));
                        }
                        else
                        {
                            float poisonDmgCalculation = Mathf.Round(dropChoice.dropPower + (enemyAttacking.shadowDropsLevel * 6) / 2);
                            poisonDmg = ((poisonDmgCalculation) - (poisonDmgCalculation * poisonDefense / 100));
                        }
                        SetBattleDamagePopupText("Poisoned!", Color.white);
                    }
                    else
                    {
                        SetBattleDamagePopupText("Resisted!", Color.white);
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
                            SetBattleDamagePopupText("Sleeping!", Color.white);

                        }
                        else
                        {
                            SetBattleDamagePopupText("Resisted!", Color.white);
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
                            haltInflicted = false;
                            confuseTimer = 0;
                            SetBattleDamagePopupText("Confused!", Color.white);

                            if (activePartyIndex == 0)
                            {
                                Engine.e.battleSystem.DeactivateChar1MenuButtons();
                                if (Engine.e.battleSystem.char1Ready == true)
                                {
                                    Engine.e.battleSystem.char1Ready = false;
                                }
                            }
                            if (activePartyIndex == 1)
                            {
                                Engine.e.battleSystem.DeactivateChar2MenuButtons();
                                if (Engine.e.battleSystem.char2Ready == true)
                                {
                                    Engine.e.battleSystem.char2Ready = false;
                                }
                            }
                            if (activePartyIndex == 2)
                            {
                                Engine.e.battleSystem.DeactivateChar3MenuButtons();
                                if (Engine.e.battleSystem.char3Ready == true)
                                {
                                    Engine.e.battleSystem.char3Ready = false;
                                }
                            }
                        }
                        else
                        {
                            SetBattleDamagePopupText("Resisted!", Color.white);
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
                        dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * characterAttacking.holyDropsLevel / 2) * characterIntelligenceScale));
                        damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * holyDefense / 100));
                        currentHealth += damageTotal;

                    }
                    else
                    {
                        dropValueOutcome = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * enemyAttacking.holyDropsLevel / 2)));
                        damageTotal = Mathf.Round((dropValueOutcome) - (dropValueOutcome * holyDefense / 100));
                        currentHealth += damageTotal;
                    }

                    SetBattleDamagePopupText(damageTotal.ToString(), Color.green);

                    if (currentHealth >= maxHealth)
                    {
                        currentHealth = maxHealth;
                    }

                    break;
                case "Repent":
                    if (teamAttack)
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
                    }
                    else
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
                    SetBattleDamagePopupText("Cured!", Color.green);
                    inflicted = false;

                    break;
            }
        }

        if (dropChoice.dropType == "Ethereal")
        {
            switch (dropChoice.dropName)
            {
                case "Protect":

                    if (!isProtected)
                    {
                        float defenseIncrease = physicalDefense + (physicalDefense * 40 / 100);
                        physicalDefense = defenseIncrease;

                        isProtected = true;

                        SetBattleDamagePopupText("Protected!", Color.green);
                    }
                    break;
                case "Encompass":

                    if (!isEncompassed)
                    {
                        float fireDefenseIncrease = fireDefense + (fireDefense * 40 / 100);
                        fireDefense = fireDefenseIncrease;
                        float iceDefenseIncrease = iceDefense + (iceDefense * 40 / 100);
                        iceDefense = fireDefenseIncrease;
                        float lightningDefenseIncrease = lightningDefense + (lightningDefense * 40 / 100);
                        lightningDefense = fireDefenseIncrease;
                        float waterDefenseIncrease = waterDefense + (waterDefense * 40 / 100);
                        waterDefense = fireDefenseIncrease;
                        float shadowDefenseIncrease = shadowDefense + (shadowDefense * 40 / 100);
                        shadowDefense = fireDefenseIncrease;
                        float holyDefenseIncrease = holyDefense + (holyDefense * 40 / 100);
                        holyDefense = fireDefenseIncrease;

                        isEncompassed = true;
                        SetBattleDamagePopupText("Encompassed!", Color.green);
                    }
                    break;
                case "Adrenaline":

                    if (!isHastened)
                    {
                        float hasteIncrease = Mathf.Round(haste + (haste * 25 / 100));
                        haste = hasteIncrease;


                        isHastened = true;
                        SetBattleDamagePopupText("Hastened!", Color.green);
                    }
                    break;
            }
        }

        if (teamAttack)
        {
            characterAttacking.GetDropExperience(dropChoice);
        }

        if (damageTotal >= 0)
        {
            Engine.e.battleSystem.damageTotal = damageTotal;
        }
        else
        {
            Engine.e.battleSystem.damageTotal = (damageTotal * -1);
        }

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

    public void EquipWeapon()
    {

    }

    public void SetCurrentClass()
    {

    }
}
