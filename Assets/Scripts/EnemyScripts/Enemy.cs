using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;
public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float health;
    public float maxHealth;
    public float mana;
    public float maxMana;
    public int lvl;
    public int experiencePoints;
    public int damage;
    public int groupIndex = -1;
    public int moneyDropAmount;
    public GameObject[] enemies;
    public Drops[] drops;

    // The higher the number [0-100], the more likely the enemy will use a Drop attack.
    public float choiceAttack;
    public float damageTotal;
    public float hitChance = 99f;
    public bool isPoisoned;
    public bool isConfused;
    public bool isAsleep;
    public bool deathInflicted;
    public bool inflicted;
    public float poisonDmg;
    public int sleepTimer;
    public int confuseTimer;
    public int deathTimer = 3;
    public GameObject deathTimerPopup;
    public float haste;

    public float dodgeChance;
    public float physicalDefense;
    public float fireDefense;
    public float iceDefense;
    public float waterDefense;
    public float holyDefense;
    public float lightningDefense;
    public float shadowDefense;
    public float poisonDefense;
    public float sleepDefense;
    public float confuseDefense;
    public float deathDefense;


    public Item[] itemDrops;
    public Item stealableItem;
    public float stealChance;
    public GrieveWeapons[] grieveWeaponDrops;
    public MacWeapons[] macWeaponDrops;
    public FieldWeapons[] fieldWeaponDrops;
    public RiggsWeapons[] riggsWeaponDrops;
    public ChestArmor[] chestArmorDrops;

    public int itemDropChance;
    public int weaponDropChance;
    public int armorDropChance;

    public EnemyInformation enemyInformation;
    public BattleSystem battleSystem;
    public Transform enemyPos;
    public string worldZone;
    public Vector3 currentBattlePos;

    void Awake()
    {
        battleSystem = Engine.e.battleSystem;
        currentBattlePos = transform.position;

        health = maxHealth;
        mana = maxMana;
        // if (groupIndex == -1)
        // {
        //  GetComponentInParent<EnemyGroup>().GetEnemyIndex(this);
        //  Debug.Log(name + " in group " + GetComponentInParent<EnemyGroup>().gameObject.name + ", scene " + Engine.e.currentScene + " has an index of -1. Should be " + groupIndex);
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "ActiveParty")
        {
            GetComponentInParent<EnemyGroup>().OnTriggerEnter2D(other);

        }
    }

    public void TakePhysicalDamage(int index, float _damage, float hitChance)
    {
        float critChance = 2;

        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR1TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR1)
        {
            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().currentHealth > (Engine.e.activeParty.activeParty[0].GetComponent<Character>().maxHealth * 50 / 100))
            {
                critChance = Random.Range(0, 10);
            }
            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().currentHealth > (Engine.e.activeParty.activeParty[0].GetComponent<Character>().maxHealth * 40 / 100) &&
            Engine.e.activeParty.activeParty[0].GetComponent<Character>().currentHealth < (Engine.e.activeParty.activeParty[0].GetComponent<Character>().maxHealth * 50 / 100))
            {
                critChance = Random.Range(0, 9);
            }
            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().currentHealth > (Engine.e.activeParty.activeParty[0].GetComponent<Character>().maxHealth * 25 / 100) &&
          Engine.e.activeParty.activeParty[0].GetComponent<Character>().currentHealth < (Engine.e.activeParty.activeParty[0].GetComponent<Character>().maxHealth * 40 / 100))
            {
                critChance = Random.Range(0, 8);
            }
        }

        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR2TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR2)
        {
            if (Engine.e.activeParty.activeParty[1].GetComponent<Character>().currentHealth > (Engine.e.activeParty.activeParty[1].GetComponent<Character>().maxHealth * 50 / 100))
            {
                critChance = Random.Range(0, 10);
            }
            if (Engine.e.activeParty.activeParty[1].GetComponent<Character>().currentHealth > (Engine.e.activeParty.activeParty[1].GetComponent<Character>().maxHealth * 40 / 100) &&
            Engine.e.activeParty.activeParty[1].GetComponent<Character>().currentHealth < (Engine.e.activeParty.activeParty[1].GetComponent<Character>().maxHealth * 50 / 100))
            {
                critChance = Random.Range(0, 9);
            }
            if (Engine.e.activeParty.activeParty[1].GetComponent<Character>().currentHealth > (Engine.e.activeParty.activeParty[1].GetComponent<Character>().maxHealth * 25 / 100) &&
          Engine.e.activeParty.activeParty[1].GetComponent<Character>().currentHealth < (Engine.e.activeParty.activeParty[1].GetComponent<Character>().maxHealth * 40 / 100))
            {
                critChance = Random.Range(0, 8);
            }
        }

        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR3TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR3)
        {
            if (Engine.e.activeParty.activeParty[2].GetComponent<Character>().currentHealth > (Engine.e.activeParty.activeParty[2].GetComponent<Character>().maxHealth * 50 / 100))
            {
                critChance = Random.Range(0, 10);
            }
            if (Engine.e.activeParty.activeParty[2].GetComponent<Character>().currentHealth > (Engine.e.activeParty.activeParty[2].GetComponent<Character>().maxHealth * 40 / 100) &&
            Engine.e.activeParty.activeParty[2].GetComponent<Character>().currentHealth < (Engine.e.activeParty.activeParty[2].GetComponent<Character>().maxHealth * 50 / 100))
            {
                critChance = Random.Range(0, 9);
            }
            if (Engine.e.activeParty.activeParty[2].GetComponent<Character>().currentHealth > (Engine.e.activeParty.activeParty[2].GetComponent<Character>().maxHealth * 25 / 100) &&
          Engine.e.activeParty.activeParty[2].GetComponent<Character>().currentHealth < (Engine.e.activeParty.activeParty[2].GetComponent<Character>().maxHealth * 40 / 100))
            {
                critChance = Random.Range(0, 8);
            }
        }

        float elementFireDamageBonus = 0;
        float elementWaterDamageBonus = 0;
        float elementLightningDamageBonus = 0;
        float elementShadowDamageBonus = 0;
        float elementIceDamageBonus = 0;

        if (critChance == 0)
        {
            float critDamage = Mathf.Round((_damage + (_damage * (2 / 3))));
            _damage = _damage + critDamage;
        }

        _damage = Random.Range(_damage, _damage + 5);

        float adjustedDodge = Mathf.Round(hitChance - dodgeChance);
        int hit = Random.Range(0, 99);
        int characterIndex = 0;

        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR1TURN)
        {
            characterIndex = 0;
        }
        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR2TURN)
        {
            characterIndex = 1;
        }
        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR3TURN)
        {
            characterIndex = 2;
        }

        Character character = Engine.e.activeParty.activeParty[characterIndex].GetComponent<Character>();

        if (isAsleep)
        {
            adjustedDodge = 100;
        }

        if (hit < adjustedDodge)
        {

            elementFireDamageBonus += Mathf.Round((character.firePhysicalAttackBonus)
            - (character.firePhysicalAttackBonus * fireDefense / 100));

            elementWaterDamageBonus += Mathf.Round((character.waterPhysicalAttackBonus)
            - (character.waterPhysicalAttackBonus * waterDefense / 100));

            elementLightningDamageBonus += Mathf.Round((character.lightningPhysicalAttackBonus)
            - (character.lightningPhysicalAttackBonus * lightningDefense / 100));

            elementShadowDamageBonus += Mathf.Round((character.shadowPhysicalAttackBonus)
            - (character.shadowPhysicalAttackBonus * shadowDefense / 100));

            elementIceDamageBonus += Mathf.Round((character.icePhysicalAttackBonus)
            - (character.icePhysicalAttackBonus * iceDefense / 100));

            damageTotal = Mathf.Round((_damage) - (_damage * physicalDefense / 100) + elementFireDamageBonus + elementWaterDamageBonus + elementLightningDamageBonus + elementShadowDamageBonus + elementIceDamageBonus);

        }
        else
        {
            damageTotal = 0;
            Debug.Log("Missed!");
            battleSystem.dodgedAttack = true;
        }

        health -= damageTotal;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (isAsleep && damageTotal != 0)
        {
            isAsleep = false;
            inflicted = false;
            if (GetComponent<EnemyMovement>() != null)
            {
                GetComponent<EnemyMovement>().enabled = true;
                GetComponent<SpriteRenderer>().color = Color.white;
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
        }

        if (health <= 0)
        {
            if (GetComponent<Light2D>())
            {
                GetComponent<Light2D>().enabled = false;
            }

            GetComponent<SpriteRenderer>().enabled = false;
            battleSystem.enemyUI[index].SetActive(false);
            battleSystem.enemies[index].GetComponent<Enemy>().isPoisoned = false;
            battleSystem.enemies[index].GetComponent<Enemy>().isConfused = false;
            battleSystem.enemies[index].GetComponent<Enemy>().isAsleep = false;
            battleSystem.enemies[index].GetComponent<Enemy>().deathInflicted = false;
            battleSystem.enemies[index].GetComponent<Enemy>().inflicted = false;
            battleSystem.enemies[index].GetComponent<Enemy>().poisonDmg = 0;

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

    public void TakeDropDamage(int index, Drops dropChoice)
    {
        float charDropDamage = 0;
        int characterIndex = 0;
        GameObject characterLocation = null;

        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR1TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR1)
        {
            characterIndex = 0;
            characterLocation = Engine.e.activeParty.gameObject;
        }
        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR2TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR2)
        {
            characterIndex = 1;
            characterLocation = Engine.e.activePartyMember2.gameObject;

        }
        if (Engine.e.battleSystem.currentInQueue == BattleState.CHAR3TURN || Engine.e.battleSystem.currentInQueue == BattleState.CONFCHAR3)
        {
            characterIndex = 2;
            characterLocation = Engine.e.activePartyMember3.gameObject;

        }

        Character character = Engine.e.activeParty.activeParty[characterIndex].GetComponent<Character>();


        if (dropChoice.dropType == "Fire")
        {
            if (character.fireDropsLevel < 99)
            {
                character.fireDropsExperience += dropChoice.experiencePoints;
                // Level Up
                if (character.fireDropsExperience >= character.fireDropsLvlReq)
                {
                    character.fireDropsLevel += 1f;
                    character.fireDropsExperience -= character.fireDropsLvlReq;
                    character.fireDropsLvlReq = (character.fireDropsLvlReq * 3 / 2);
                    character.fireDefense += 0.5f;
                    GameObject levelUp = Instantiate(Engine.e.battleSystem.levelUpPopup, characterLocation.transform.position, Quaternion.identity);
                    levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Fire Lvl Up! (Lvl: " + character.fireDropsLevel + ")";
                    //levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(212, 1, 1, 255);
                    Destroy(levelUp, 2f);
                }
            }

            Instantiate(battleSystem.fireDropAnim, characterLocation.transform.position, Quaternion.identity);

            charDropDamage = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * character.fireDropsLevel / 2)) + character.fireDropAttackBonus);

            damageTotal = Mathf.Round((charDropDamage) - (charDropDamage * fireDefense / 100));
            battleSystem.enemies[index].gameObject.GetComponent<Enemy>().health -= Mathf.Round(damageTotal);

            Debug.Log(damageTotal);
        }
        if (dropChoice.dropType == "Water")
        {
            if (character.waterDropsLevel < 99)
            {
                character.waterDropsExperience += dropChoice.experiencePoints;
                // Level Up
                if (character.waterDropsExperience >= character.waterDropsLvlReq)
                {
                    character.waterDropsLevel += 1f;
                    character.waterDropsExperience -= character.waterDropsLvlReq;
                    character.waterDropsLvlReq = (character.waterDropsLvlReq * 3 / 2);
                    character.waterDefense += 0.5f;
                    GameObject levelUp = Instantiate(Engine.e.battleSystem.levelUpPopup, characterLocation.transform.position, Quaternion.identity);
                    levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Water Lvl Up! (Lvl: " + character.waterDropsLevel + ")";
                    //levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(37, 124, 255, 255);
                    Destroy(levelUp, 2f);
                }
                Instantiate(battleSystem.waterDropAnim, characterLocation.transform.position, Quaternion.identity);

                charDropDamage = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * character.waterDropsLevel / 2)) + character.waterDropAttackBonus);
                damageTotal = Mathf.Round((charDropDamage) - (charDropDamage * waterDefense / 100));
                battleSystem.enemies[index].gameObject.GetComponent<Enemy>().health -= Mathf.Round(damageTotal);
                Debug.Log(damageTotal);
            }
        }

        if (dropChoice.dropType == "Lightning")
        {
            if (character.lightningDropsLevel < 99)
            {
                character.lightningDropsExperience += dropChoice.experiencePoints;
                // Level Up
                if (character.lightningDropsExperience >= character.lightningDropsLvlReq)
                {
                    character.lightningDropsLevel += 1f;
                    character.lightningDropsExperience -= character.lightningDropsLvlReq;
                    character.lightningDropsLvlReq = (character.lightningDropsLvlReq * 3 / 2);
                    character.lightningDefense += 0.5f;
                    GameObject levelUp = Instantiate(Engine.e.battleSystem.levelUpPopup, characterLocation.transform.position, Quaternion.identity);
                    levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Lightning Lvl Up! (Lvl: " + character.lightningDropsLevel + ")";
                    //levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(255, 233, 64, 255);
                    Destroy(levelUp, 2f);
                }
            }
            Instantiate(battleSystem.lightningDropAnim, characterLocation.transform.position, Quaternion.identity);

            charDropDamage = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * character.lightningDropsLevel / 2)) + character.lightningDropAttackBonus);
            damageTotal = Mathf.Round((charDropDamage) - (charDropDamage * lightningDefense / 100));
            battleSystem.enemies[index].gameObject.GetComponent<Enemy>().health -= Mathf.Round(damageTotal);
            Debug.Log(damageTotal);
        }

        if (dropChoice.dropType == "Shadow")
        {
            if (character.shadowDropsLevel < 99)
            {
                character.shadowDropsExperience += dropChoice.experiencePoints;
                // Level Up
                if (character.shadowDropsExperience >= character.shadowDropsLvlReq)
                {
                    character.shadowDropsLevel += 1f;
                    character.shadowDropsExperience -= character.shadowDropsLvlReq;
                    character.shadowDropsLvlReq = (character.shadowDropsLvlReq * 3 / 2);
                    character.shadowDefense += 0.5f;
                    character.poisonDefense += 0.5f;
                    GameObject levelUp = Instantiate(Engine.e.battleSystem.levelUpPopup, characterLocation.transform.position, Quaternion.identity);
                    levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Shadow Lvl Up! (Lvl: " + character.shadowDropsLevel + ")";
                    levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(170, 23, 255, 255);
                    Destroy(levelUp, 2f);
                }
            }

            if (battleSystem.lastDropChoice.dropName == "Bio" || battleSystem.lastDropChoice.dropName == "Knockout" || battleSystem.lastDropChoice.dropName == "Blind")
            {
                if (battleSystem.lastDropChoice.dropName == "Bio")
                {
                    Instantiate(battleSystem.poisonAnim, characterLocation.transform.position, Quaternion.identity);
                }
                if (battleSystem.lastDropChoice.dropName == "Knockout")
                {
                    Instantiate(battleSystem.sleepAnim, characterLocation.transform.position, Quaternion.identity);
                }
                if (battleSystem.lastDropChoice.dropName == "Blind")
                {
                    Instantiate(battleSystem.confuseAnim, characterLocation.transform.position, Quaternion.identity);
                }
            }
            else
            {
                Instantiate(battleSystem.shadowDropAnim, characterLocation.transform.position, Quaternion.identity);
            }

            charDropDamage = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * character.shadowDropsLevel / 2)) + character.shadowDropAttackBonus);
            damageTotal = Mathf.Round((charDropDamage) - (charDropDamage * shadowDefense / 100));
            battleSystem.enemies[index].gameObject.GetComponent<Enemy>().health -= Mathf.Round(damageTotal);
            Debug.Log(damageTotal);

            if (battleSystem.lastDropChoice.dropName == "Bio")
            {
                InflictPoisonAttack(characterIndex, battleSystem.lastDropChoice.dotDmg);
            }
            if (battleSystem.lastDropChoice.dropName == "Knockout")
            {
                if (!isAsleep)
                {
                    float sleepChance = Random.Range(0, 100);

                    if (sleepDefense < sleepChance)
                    {
                        isAsleep = true;
                        sleepTimer = 0;
                    }
                }
            }

            if (battleSystem.lastDropChoice.dropName == "Blind")
            {
                if (!isConfused)
                {
                    float confuseChance = Random.Range(0, 100);

                    if (confuseDefense < confuseChance)
                    {
                        isConfused = true;
                        confuseTimer = 0;
                    }
                }
            }
        }

        if (dropChoice.dropType == "Ice")
        {
            if (character.iceDropsLevel < 99)
            {
                character.iceDropsExperience += dropChoice.experiencePoints;
                // Level Up
                if (character.iceDropsExperience >= character.iceDropsLvlReq)
                {
                    character.iceDropsLevel += 1f;
                    character.iceDropsExperience -= character.iceDropsLvlReq;
                    character.iceDropsLvlReq = (character.iceDropsLvlReq * 3 / 2);
                    character.iceDefense += 0.5f;
                    GameObject levelUp = Instantiate(Engine.e.battleSystem.levelUpPopup, characterLocation.transform.position, Quaternion.identity);
                    levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Ice Lvl Up! (Lvl: " + character.iceDropsLevel + ")";
                    //levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(102, 238, 255, 255);
                    Destroy(levelUp, 2f);
                }
                Instantiate(battleSystem.iceDropAnim, characterLocation.transform.position, Quaternion.identity);

                charDropDamage = Mathf.Round(dropChoice.dropPower + ((dropChoice.dropPower * character.iceDropsLevel / 2)) + character.iceDropAttackBonus);
                damageTotal = Mathf.Round((charDropDamage) - (charDropDamage * iceDefense / 100));
                battleSystem.enemies[index].gameObject.GetComponent<Enemy>().health -= Mathf.Round(damageTotal);
                Debug.Log(damageTotal);
            }
        }

        if (dropChoice.dropType == "Holy")
        {
            if (character.holyDropsLevel < 99)
            {
                character.holyDropsExperience += dropChoice.experiencePoints;
                // Level Up
                if (character.holyDropsExperience >= character.holyDropsLvlReq)
                {
                    character.holyDropsLevel += 1f;
                    character.holyDropsExperience -= character.holyDropsLvlReq;
                    character.holyDropsLvlReq = (character.holyDropsLvlReq * 3 / 2);
                    //character.iceDefense += 0.5f;
                    GameObject levelUp = Instantiate(Engine.e.battleSystem.levelUpPopup, characterLocation.transform.position, Quaternion.identity);
                    levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Holy Lvl Up! (Lvl: " + character.holyDropsLevel + ")";
                    //levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(102, 238, 255, 255);
                    Destroy(levelUp, 2f);
                }

                Instantiate(battleSystem.holyDropAnim, characterLocation.transform.position, Quaternion.identity);


            }
        }

        Engine.e.battleSystem.damageTotal = damageTotal;

        if (isAsleep)
        {
            if (battleSystem.lastDropChoice.dropName != "Knockout" && battleSystem.lastDropChoice.dropName != "Bio" && battleSystem.lastDropChoice.dropName != "Blind")
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

            if (battleSystem.lastDropChoice.dropName == "Knockout")
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

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (!battleSystem.dropExists)
        {
            if (battleSystem.enemies[index].gameObject.GetComponent<Enemy>().health <= 0)
            {
                if (GetComponent<Light2D>())
                {
                    GetComponent<Light2D>().enabled = false;
                }

                battleSystem.enemies[index].GetComponent<Enemy>().isPoisoned = false;
                battleSystem.enemies[index].GetComponent<Enemy>().isConfused = false;
                battleSystem.enemies[index].GetComponent<Enemy>().isAsleep = false;
                battleSystem.enemies[index].GetComponent<Enemy>().deathInflicted = false;
                battleSystem.enemies[index].GetComponent<Enemy>().inflicted = false;
                battleSystem.enemies[index].GetComponent<Enemy>().poisonDmg = 0;

                battleSystem.enemies[index].gameObject.GetComponent<SpriteRenderer>().enabled = false;


                battleSystem.enemyUI[index].SetActive(false);
            }
        }
    }

    public void UseItem()
    {
        bool failedItemUse = false;

        if (Engine.e.inBattle)
        {
            if (battleSystem.currentInQueue == BattleState.CHAR1TURN)
            {
                Engine.e.itemToBeUsed = battleSystem.char1ItemToBeUsed;
            }
            if (battleSystem.currentInQueue == BattleState.CHAR2TURN)
            {
                Engine.e.itemToBeUsed = battleSystem.char2ItemToBeUsed;
            }
            if (battleSystem.currentInQueue == BattleState.CHAR3TURN)
            {
                Engine.e.itemToBeUsed = battleSystem.char3ItemToBeUsed;
            }

            if (Engine.e.itemToBeUsed.numberHeld == 0)
            {
                failedItemUse = true;
            }
        }

        switch (Engine.e.itemToBeUsed.itemName)
        {
            case "Health Potion":

                if (health == maxHealth || health == 0)
                {
                    if (Engine.e.inBattle)
                    {
                        failedItemUse = true;
                        Engine.e.partyInventoryReference.OpenInventoryMenu();
                        break;
                    }
                    else
                        break;
                }
                else
                {
                    if (!failedItemUse)
                    {
                        if (Engine.e.inBattle)
                        {

                            GameObject healthSprite = Instantiate(Engine.e.gameInventory[Engine.e.itemToBeUsed.itemIndex].GetComponent<Item>().anim, transform.position, Quaternion.identity);
                            GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, transform.position, Quaternion.identity);
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = Engine.e.itemToBeUsed.itemPower.ToString();
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);
                            Destroy(dmgPopup, 1f);
                            Destroy(healthSprite, 1f);


                            health += Engine.e.itemToBeUsed.itemPower;

                            if (health > maxHealth)
                            {
                                health = maxHealth;
                            }

                            Engine.e.partyInventoryReference.SubtractItemFromInventory(Engine.e.itemToBeUsed);
                            break;
                        }
                    }
                    break;
                }

            case "Mana Potion":

                if (mana == maxMana)
                {
                    if (Engine.e.inBattle)
                    {
                        failedItemUse = true;
                        Engine.e.partyInventoryReference.OpenInventoryMenu();

                        break;
                    }
                    break;
                }

                if (!failedItemUse)
                {
                    if (Engine.e.inBattle)
                    {

                        GameObject manaSprite = Instantiate(Engine.e.gameInventory[Engine.e.itemToBeUsed.itemIndex].GetComponent<Item>().anim, transform.position, Quaternion.identity);
                        GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, transform.position, Quaternion.identity);
                        dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = Engine.e.itemToBeUsed.itemPower.ToString();
                        dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 54, 255, 255);
                        Destroy(dmgPopup, 1f);
                        Destroy(manaSprite, 1f);


                        mana += Engine.e.itemToBeUsed.itemPower;

                        if (mana > maxMana)
                        {
                            mana = maxMana;
                        }


                        Engine.e.partyInventoryReference.SubtractItemFromInventory(Engine.e.itemToBeUsed);
                        break;

                    }
                }
                break;
            case "Ashes":
                // Zombie status effect instant kill?
                break;

            case "Antidote":

                if (!isPoisoned)
                {
                    if (Engine.e.inBattle)
                    {
                        failedItemUse = true;
                        Engine.e.partyInventoryReference.OpenInventoryMenu();

                        break;

                    }
                    else
                        break;
                }

                if (!failedItemUse)
                {
                    if (Engine.e.inBattle)
                    {

                        Engine.e.activeParty.GetComponent<SpriteRenderer>().color = Color.white;

                        if (poisonDefense > 100)
                        {
                            health -= 100;

                            if (health < 0)
                            {
                                health = 0;

                                Engine.e.battleSystem.CheckIsDeadEnemy();
                            }

                        }
                        else
                        {
                            GameObject antidote = Instantiate(Engine.e.gameInventory[Engine.e.itemToBeUsed.itemIndex].GetComponent<Item>().anim, Engine.e.activeParty.transform.position, Quaternion.identity);
                            GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.activeParty.transform.position, Quaternion.identity);
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Cured";
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);
                            Destroy(dmgPopup, 1f);
                            Destroy(antidote, 1f);
                            isPoisoned = false;
                            inflicted = false;
                        }
                    }

                    break;

                }
                break;
        }

        if (failedItemUse)
        {
            failedItemUse = false;
        }
        Engine.e.itemToBeUsed = null;
    }

    public void ConfuseTakeDropDamage(int index, Drops dropChoice)
    {
        float charDropDamage = 0;

        if (dropChoice.dropType == "Fire")
        {

            charDropDamage = Mathf.Round(dropChoice.dropPower);

            damageTotal = Mathf.Round((charDropDamage) - (charDropDamage * fireDefense / 100));
            battleSystem.enemies[index].gameObject.GetComponent<Enemy>().health -= Mathf.Round(damageTotal);
            Debug.Log(damageTotal);
        }

        if (dropChoice.dropType == "Water")
        {

            charDropDamage = Mathf.Round(dropChoice.dropPower);
            damageTotal = Mathf.Round((charDropDamage) - (charDropDamage * waterDefense / 100));
            battleSystem.enemies[index].gameObject.GetComponent<Enemy>().health -= Mathf.Round(damageTotal);
            Debug.Log(damageTotal);
        }

        if (dropChoice.dropType == "Lightning")
        {

            charDropDamage = Mathf.Round(dropChoice.dropPower);
            damageTotal = Mathf.Round((charDropDamage) - (charDropDamage * lightningDefense / 100));
            battleSystem.enemies[index].gameObject.GetComponent<Enemy>().health -= Mathf.Round(damageTotal);
            Debug.Log(damageTotal);
        }

        if (dropChoice.dropType == "Shadow")
        {

            charDropDamage = Mathf.Round(dropChoice.dropPower);
            damageTotal = Mathf.Round((charDropDamage) - (charDropDamage * shadowDefense / 100));
            battleSystem.enemies[index].gameObject.GetComponent<Enemy>().health -= Mathf.Round(damageTotal);
            Debug.Log(damageTotal);

            if (battleSystem.lastDropChoice.dropName == "Bio")
            {
                if (!isPoisoned)
                {
                    float poisonChance = Random.Range(0, 100);

                    if (poisonDefense < poisonChance)
                    {
                        isPoisoned = true;
                        inflicted = true;
                        float poisonDmgCalculation = (battleSystem.lastDropChoice.dotDmg) * 4 / 2;
                        poisonDmg = ((poisonDmgCalculation) - (poisonDmgCalculation * poisonDefense / 100));
                    }
                }
            }
            if (battleSystem.lastDropChoice.dropName == "Knockout")
            {
                if (!isAsleep)
                {
                    float sleepChance = Random.Range(0, 100);

                    if (sleepDefense < sleepChance)
                    {
                        isAsleep = true;
                        inflicted = true;
                        sleepTimer = 0;
                    }
                }
            }

            if (battleSystem.lastDropChoice.dropName == "Blind")
            {
                if (!isConfused)
                {
                    float confuseChance = Random.Range(0, 100);

                    if (confuseDefense < confuseChance)
                    {
                        isConfused = true;
                        inflicted = true;
                    }
                }
            }
        }

        if (dropChoice.dropType == "Ice")
        {

            charDropDamage = Mathf.Round(dropChoice.dropPower);
            damageTotal = Mathf.Round((charDropDamage) - (charDropDamage * iceDefense / 100));
            battleSystem.enemies[index].gameObject.GetComponent<Enemy>().health -= Mathf.Round(damageTotal);
            Debug.Log(damageTotal);
        }

        Engine.e.battleSystem.damageTotal = damageTotal;

        if (isAsleep)
        {
            if (battleSystem.lastDropChoice.dropName != "Knockout" && battleSystem.lastDropChoice.dropName != "Bio" && battleSystem.lastDropChoice.dropName != "Blind")
            {
                isAsleep = false;
                isConfused = false;
                inflicted = false;
                if (GetComponent<EnemyMovement>() != null)
                {
                    GetComponent<EnemyMovement>().enabled = true;
                }
                // GetComponent<SpriteRenderer>().color = Color.grey;
            }
        }

        if (isConfused)
        {
            int snapoutChance = Random.Range(0, 100);
            if (confuseDefense > snapoutChance)
            {
                isConfused = false;
                confuseTimer = 0;
                inflicted = false;
                if (GetComponent<EnemyMovement>() != null)
                {
                    GetComponent<EnemyMovement>().enabled = true;
                }
                GetComponent<SpriteRenderer>().color = Color.white;
            }

            if (battleSystem.lastDropChoice.dropName == "Knockout")
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

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (!battleSystem.dropExists)
        {
            if (battleSystem.enemies[index].gameObject.GetComponent<Enemy>().health <= 0)
            {
                if (GetComponent<Light2D>())
                {
                    GetComponent<Light2D>().enabled = false;
                }
                battleSystem.enemies[index].GetComponent<Enemy>().isPoisoned = false;
                battleSystem.enemies[index].GetComponent<Enemy>().isConfused = false;
                battleSystem.enemies[index].GetComponent<Enemy>().isAsleep = false;
                battleSystem.enemies[index].GetComponent<Enemy>().poisonDmg = 0;
                battleSystem.enemies[index].GetComponent<Enemy>().deathInflicted = false;
                battleSystem.enemies[index].GetComponent<Enemy>().inflicted = false;
                battleSystem.enemies[index].gameObject.GetComponent<SpriteRenderer>().enabled = false;


                battleSystem.enemyUI[index].SetActive(false);
            }
        }
    }

    public void TakeSkillDamage(float power, int target)
    {
        damageTotal = Mathf.Round(power);
        Engine.e.battleSystem.damageTotal = damageTotal;
        battleSystem.enemies[target].gameObject.GetComponent<Enemy>().health -= damageTotal;

        if (!battleSystem.skillRangedAttack)
        {
            if (battleSystem.enemies[target].gameObject.GetComponent<Enemy>().health <= 0)
            {
                battleSystem.enemies[target].gameObject.GetComponent<SpriteRenderer>().enabled = false;
                battleSystem.enemyUI[target].SetActive(false);
                battleSystem.enemies[target].GetComponent<Enemy>().isPoisoned = false;
                battleSystem.enemies[target].GetComponent<Enemy>().isConfused = false;
                battleSystem.enemies[target].GetComponent<Enemy>().isAsleep = false;
                battleSystem.enemies[target].GetComponent<Enemy>().deathInflicted = false;
                battleSystem.enemies[target].GetComponent<Enemy>().inflicted = false;
                battleSystem.enemies[target].GetComponent<Enemy>().poisonDmg = 0;

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
                GetComponent<SpriteRenderer>().color = Color.white;

            }
        }
    }

    public void InflictPoisonAttack(int index, float _poisonDmg)
    {
        float poisonChance = Random.Range(0, 100);
        Character character = Engine.e.activeParty.activeParty[index].GetComponent<Character>();

        if (poisonDefense < poisonChance)
        {
            isPoisoned = true;
            inflicted = true;
            float poisonDmgCalculation = Mathf.Round(_poisonDmg + (character.shadowDropsLevel * 6) / 2);
            poisonDmg = ((poisonDmgCalculation) - (poisonDmgCalculation * poisonDefense / 100));
            if (!Engine.e.battleSystem.dropExists)
            {
                GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
    }

    public void InflictDeathAttack()
    {
        //Character character = GameManager.gameManager.activeParty.activeParty[index].GetComponent<Character>();

        int instantDeath = Random.Range(0, 99);

        if (instantDeath > deathDefense)
        {
            TakeSkillDamage(maxHealth, groupIndex);
        }

        else
        {
            float deathChance = Random.Range(0, 100);

            if (deathDefense < deathChance)
            {
                Vector3 deathTimerLoc = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                deathTimerPopup = Instantiate(Engine.e.battleSystem.deathTimerPopup, deathTimerLoc, Quaternion.identity);
                deathTimerPopup.transform.SetParent(this.gameObject.transform);
                deathTimerPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = deathTimer.ToString();
                Engine.e.battleSystem.charUsingSkill = false;

                GameObject result = Instantiate(Engine.e.battleSystem.damagePopup, transform.position, Quaternion.identity);
                deathInflicted = true;
                result.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Death";
                Destroy(result, 1f);

            }
            else
            {
                GameObject result = Instantiate(Engine.e.battleSystem.damagePopup, transform.position, Quaternion.identity);
                result.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Resisted";
                Destroy(result, 1f);
            }
        }
    }

    public void InflictDeathDrop()
    {

        float deathChance = Random.Range(0, 100);

        if (deathDefense < deathChance)
        {
            Vector3 deathTimerLoc = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            deathTimerPopup = Instantiate(Engine.e.battleSystem.deathTimerPopup, deathTimerLoc, Quaternion.identity);
            deathTimerPopup.transform.SetParent(this.gameObject.transform);
            deathTimerPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = deathTimer.ToString();
            Engine.e.battleSystem.charUsingSkill = false;

            GameObject result = Instantiate(Engine.e.battleSystem.damagePopup, transform.position, Quaternion.identity);
            deathInflicted = true;
            result.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Death";
            Destroy(result, 1f);

        }
        else
        {
            GameObject result = Instantiate(Engine.e.battleSystem.damagePopup, transform.position, Quaternion.identity);
            result.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Resisted";
            Destroy(result, 1f);
        }
    }


    public void TakeDeathDamage()
    {
        deathTimer--;
        deathTimerPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = deathTimer.ToString();

        if (deathTimer == 0)
        {
            health = 0;

            GetComponent<SpriteRenderer>().enabled = false;
            battleSystem.enemyUI[groupIndex].SetActive(false);
            isPoisoned = false;
            isConfused = false;
            isAsleep = false;
            deathInflicted = false;
            inflicted = false;
            poisonDmg = 0;

            Destroy(deathTimerPopup);
        }
    }

    public void TakePoisonDamage(int index, float poisonDmg)
    {
        float totalDamage = Mathf.Round((poisonDmg) - (poisonDmg * Engine.e.battleSystem.enemies[index].gameObject.GetComponent<Enemy>().poisonDefense / 100));
        health -= totalDamage;

        if (health < 0)
        {
            health = 0;
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        battleSystem.hud.displayEnemyHealth[index].text = health.ToString();

        if (battleSystem.enemies[index].gameObject.GetComponent<Enemy>().health <= 0)
        {
            battleSystem.enemies[index].gameObject.GetComponent<SpriteRenderer>().enabled = false;
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

        GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, transform.position, Quaternion.identity);
        if (poisonDefense < 100)
        {
            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = totalDamage.ToString();
        }
        else
        {
            float totalDamageText = totalDamage * -1;
            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = totalDamageText.ToString();

        }
        Destroy(dmgPopup, 1f);
    }

    public void StealAttempt(int index, float stealChance)
    {

        if (stealableItem != null)
        {
            float steal = Random.Range(0, 100);

            if (steal < stealChance)
            {
                Engine.e.partyInventoryReference.AddItemToInventory(stealableItem);
                GameObject success = Instantiate(Engine.e.battleSystem.damagePopup, transform.position, Quaternion.identity);
                success.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Stole " + stealableItem.itemName + "!";
                Destroy(success, 2f);
                stealableItem = null;
            }
            else
            {
                GameObject failure = Instantiate(Engine.e.battleSystem.damagePopup, transform.position, Quaternion.identity);
                failure.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Failed!";
                Destroy(failure, 1f);
            }
        }
        else
        {
            GameObject nothing = Instantiate(Engine.e.battleSystem.damagePopup, transform.position, Quaternion.identity);
            nothing.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Nothing to steal!";
            Destroy(nothing, 1f);
        }
    }

    void FixedUpdate()
    {

    }

}

