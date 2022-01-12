using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;
public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float currentHealth;
    public float maxHealth;
    public float currentMana;
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

    public float fireDropsLevel, iceDropsLevel, lightningDropsLevel, waterDropsLevel, shadowDropsLevel, holyDropsLevel;
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
            case "Cave Bat":
                GetComponent<CaveBatBattleLogic>().Logic(_enemyTarget);
                break;
        }
    }

    public void GenericMoveSet(int target)
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

        if (GetComponent<Enemy>().drops[0] != null)
        {
            int _choiceAttack = Random.Range(0, 100);

            if (GetComponent<Enemy>().choiceAttack < _choiceAttack)
            {
                Engine.e.battleSystem.enemyMoving = true;
                Engine.e.battleSystem.enemyAttacking = true;
                Engine.e.PhysicalDamageCalculation(target, GetComponent<Enemy>().damage);

            }
            else
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
        else
        {
            Engine.e.battleSystem.enemyMoving = true;
            Engine.e.battleSystem.enemyAttacking = true;
            Engine.e.PhysicalDamageCalculation(target, GetComponent<Enemy>().damage);
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

        currentHealth -= damageTotal;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
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

        if (currentHealth <= 0)
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
            if (!teamAttack)
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
            if (!teamAttack)
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

            if (!teamAttack)
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
            if (!teamAttack)
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
            if (!teamAttack)
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

                    InflictPoisonAttack(currentIndex, battleSystem.lastDropChoice.dotDmg);
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
            if (!teamAttack)
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
            if (dropChoice.dropName != "Knockout" && battleSystem.lastDropChoice.dropName != "Bio" && battleSystem.lastDropChoice.dropName != "Blind")
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

                GetComponent<SpriteRenderer>().enabled = false;


                battleSystem.enemyUI[GetComponentInParent<EnemyGroup>().GetEnemyIndex(this)].SetActive(false);
            }
        }
    }



    public void UseItem(Item item)
    {
        bool failedItemUse = false;

        if (item.numberHeld == 0)
        {
            failedItemUse = true;
        }


        switch (item.itemName)
        {
            case "Health Potion":

                if (currentHealth == maxHealth || currentHealth == 0)
                {
                    failedItemUse = true;
                    break;
                }
                else
                {
                    if (!failedItemUse)
                    {

                        GameObject healthSprite = Instantiate(Engine.e.gameInventory[item.itemIndex].GetComponent<Item>().anim, transform.position, Quaternion.identity);
                        GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, transform.position, Quaternion.identity);
                        dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = item.itemPower.ToString();
                        dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);
                        Destroy(dmgPopup, 1f);
                        Destroy(healthSprite, 1f);


                        currentHealth += item.itemPower;

                        if (currentHealth > maxHealth)
                        {
                            currentHealth = maxHealth;
                        }

                        Engine.e.partyInventoryReference.SubtractItemFromInventory(item);
                        break;
                    }
                    break;
                }

            case "Mana Potion":

                if (currentMana == maxMana)
                {

                    failedItemUse = true;
                    break;

                }
                else
                {

                    if (!failedItemUse)
                    {


                        GameObject manaSprite = Instantiate(Engine.e.gameInventory[item.itemIndex].GetComponent<Item>().anim, transform.position, Quaternion.identity);
                        GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, transform.position, Quaternion.identity);
                        dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = item.itemPower.ToString();
                        dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 54, 255, 255);
                        Destroy(dmgPopup, 1f);
                        Destroy(manaSprite, 1f);


                        currentMana += item.itemPower;

                        if (currentMana > maxMana)
                        {
                            currentMana = maxMana;
                        }


                        Engine.e.partyInventoryReference.SubtractItemFromInventory(item);
                        break;

                    }
                    break;
                }

            case "Ashes":
                // Zombie status effect instant kill?
                break;

            case "Antidote":

                if (!isPoisoned)
                {

                    failedItemUse = true;
                    Engine.e.partyInventoryReference.OpenInventoryMenu();

                    break;
                }

                if (!failedItemUse)
                {



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
                        GetComponent<SpriteRenderer>().color = Color.white;
                        GameObject antidote = Instantiate(Engine.e.gameInventory[item.itemIndex].GetComponent<Item>().anim, Engine.e.activeParty.transform.position, Quaternion.identity);
                        GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.activeParty.transform.position, Quaternion.identity);
                        dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Cured";
                        dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);
                        Destroy(dmgPopup, 1f);
                        Destroy(antidote, 1f);
                        isPoisoned = false;
                        inflicted = false;
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
            battleSystem.enemies[index].gameObject.GetComponent<Enemy>().currentHealth -= Mathf.Round(damageTotal);
            Debug.Log(damageTotal);
        }

        if (dropChoice.dropType == "Water")
        {

            charDropDamage = Mathf.Round(dropChoice.dropPower);
            damageTotal = Mathf.Round((charDropDamage) - (charDropDamage * waterDefense / 100));
            battleSystem.enemies[index].gameObject.GetComponent<Enemy>().currentHealth -= Mathf.Round(damageTotal);
            Debug.Log(damageTotal);
        }

        if (dropChoice.dropType == "Lightning")
        {

            charDropDamage = Mathf.Round(dropChoice.dropPower);
            damageTotal = Mathf.Round((charDropDamage) - (charDropDamage * lightningDefense / 100));
            battleSystem.enemies[index].gameObject.GetComponent<Enemy>().currentHealth -= Mathf.Round(damageTotal);
            Debug.Log(damageTotal);
        }

        if (dropChoice.dropType == "Shadow")
        {

            charDropDamage = Mathf.Round(dropChoice.dropPower);
            damageTotal = Mathf.Round((charDropDamage) - (charDropDamage * shadowDefense / 100));
            battleSystem.enemies[index].gameObject.GetComponent<Enemy>().currentHealth -= Mathf.Round(damageTotal);
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
            battleSystem.enemies[index].gameObject.GetComponent<Enemy>().currentHealth -= Mathf.Round(damageTotal);
            Debug.Log(damageTotal);
        }

        if (dropChoice.dropType == "Holy")
        {

            charDropDamage = Mathf.Round(dropChoice.dropPower);
            damageTotal = Mathf.Round((charDropDamage) - (charDropDamage * holyDefense / 100));

            if (dropChoice.dropName == "Holy Light")
            {
                currentHealth += Mathf.Round(damageTotal);

                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
                Debug.Log(damageTotal);
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

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (!battleSystem.animExists)
        {
            if (battleSystem.enemies[index].gameObject.GetComponent<Enemy>().currentHealth <= 0)
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
        battleSystem.enemies[target].gameObject.GetComponent<Enemy>().currentHealth -= damageTotal;

        if (!battleSystem.skillRangedAttack)
        {
            if (battleSystem.enemies[target].gameObject.GetComponent<Enemy>().currentHealth <= 0)
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
            if (!Engine.e.battleSystem.animExists)
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
            currentHealth = 0;

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
        currentHealth -= totalDamage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        battleSystem.hud.displayEnemyHealth[index].text = currentHealth.ToString();

        if (battleSystem.enemies[index].gameObject.GetComponent<Enemy>().currentHealth <= 0)
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

