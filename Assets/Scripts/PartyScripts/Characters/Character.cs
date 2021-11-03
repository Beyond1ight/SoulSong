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
    public float poisonDefense;
    public float sleepDefense;
    public float confuseDefense;
    public float deathDefense;


    // Drops
    public Drops[] fireDrops;
    public Drops[] iceDrops;
    public Drops[] holyDrops;
    public Drops[] waterDrops;
    public Drops[] lightningDrops;
    public Drops[] shadowDrops;

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

    public bool canUseFireDrops = false;
    public bool canUseIceDrops = false;
    public bool canUseHolyDrops = false;
    public bool canUseWaterDrops = false;
    public bool canUseLightningDrops = false;
    public bool canUseShadowDrops = false;


    // Skills
    public Skills[] skills;
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
    public float poisonDmg;
    public int sleepTimer = 0;
    public int confuseTimer = 0;
    public int deathTimer = 3;
    public GameObject siphonAnim;
    public GameObject deathTimerPopup;

    public void UseDrop(Drops dropChoice)
    {

        lastDropChoice = dropChoice;

        if (dropChoice.dps)
        {
            Engine.e.battleSystem.ActivateAttackButtons();
        }
        if (dropChoice.support)
        {
            Engine.e.battleSystem.ActivateSupportButtons();
        }

        Engine.e.battleSystem.dropAttack = true;
    }

    public void HolyLight(float healPower, int target)
    {

        float healAmount = healPower + (healPower * holyDropsLevel / 2);
        Engine.e.activeParty.activeParty[target].GetComponent<Character>().currentHealth += healAmount;
        Engine.e.battleSystem.damageTotal = healAmount;
        if (Engine.e.activeParty.activeParty[target].GetComponent<Character>().currentHealth > Engine.e.activeParty.activeParty[target].GetComponent<Character>().maxHealth)
        {
            Engine.e.activeParty.activeParty[target].GetComponent<Character>().currentHealth = Engine.e.activeParty.activeParty[target].GetComponent<Character>().maxHealth;
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

    public void Repent(int target)
    {
        if (holyDropsLevel >= 20)
        {
            if (Engine.e.activeParty.activeParty[target].GetComponent<Character>().isPoisoned)
            {
                Engine.e.activeParty.activeParty[target].GetComponent<Character>().isPoisoned = false;
            }
            if (Engine.e.activeParty.activeParty[target].GetComponent<Character>().isAsleep)
            {
                Engine.e.activeParty.activeParty[target].GetComponent<Character>().isAsleep = false;
            }
            if (Engine.e.activeParty.activeParty[target].GetComponent<Character>().isConfused)
            {
                Engine.e.activeParty.activeParty[target].GetComponent<Character>().isConfused = false;
            }
            if (Engine.e.activeParty.activeParty[target].GetComponent<Character>().deathInflicted)
            {
                Engine.e.activeParty.activeParty[target].GetComponent<Character>().RemoveDeath();
            }
            Engine.e.activeParty.activeParty[target].GetComponent<SpriteRenderer>().color = Color.white;

        }
        else
        {
            if (holyDropsLevel >= 10 && holyDropsLevel < 20)
            {
                if (Engine.e.activeParty.activeParty[target].GetComponent<Character>().isPoisoned)
                {
                    Engine.e.activeParty.activeParty[target].GetComponent<Character>().isPoisoned = false;
                    Engine.e.activeParty.activeParty[target].GetComponent<SpriteRenderer>().color = Color.white;

                }
                if (Engine.e.activeParty.activeParty[target].GetComponent<Character>().isAsleep)
                {
                    Engine.e.activeParty.activeParty[target].GetComponent<Character>().isAsleep = false;
                    Engine.e.activeParty.activeParty[target].GetComponent<SpriteRenderer>().color = Color.white;

                }

            }
            else
            {
                if (holyDropsLevel < 10)
                {
                    if (Engine.e.activeParty.activeParty[target].GetComponent<Character>().isAsleep)
                    {
                        Engine.e.activeParty.activeParty[target].GetComponent<Character>().isAsleep = false;
                        Engine.e.activeParty.activeParty[target].GetComponent<SpriteRenderer>().color = Color.white;

                    }
                }
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


}
