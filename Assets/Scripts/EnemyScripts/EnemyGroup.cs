using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class EnemyGroup : MonoBehaviour
{
    public Quest quest;
    public int spawnChance;
    public static EnemyGroup enemyGroup;
    public Enemy[] enemies;
    public List<Item> itemDrops;
    public Item item;
    public BattleSystem battleSystem;
    public int groupExperienceLevel = 0;
    public CinemachineVirtualCamera battleCamera;
    public List<string> enemyLootReferenceText;
    public bool moveToPosition = false;
    public GameObject char1SwitchPos, char2SwitchPos, char3SwitchPos;
    public bool groupInBattle = false;
    public List<int> remainingEnemies;
    int randomEnemyIndex;
    int nextRemainingEnemyIndex;

    void Start()
    {
        GroupExperienceValue();

        battleSystem = Engine.e.battleSystem;

        if (spawnChance != 0)
        {
            int deSpawn = Random.Range(0, 100);

            if (deSpawn > spawnChance)
            {
                DespawnGroup();
            }
        }
        else
        {
            DespawnGroup();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.name == "ActiveParty")
        {
            moveToPosition = true;
            battleSystem.enemies = new Enemy[4];
            battleSystem.enemyGroup = this;
            battleCamera.gameObject.SetActive(true);

            for (int i = 0; i < enemies.Length; i++)
            {
                for (int j = i; j < battleSystem.enemies.Length; j++)
                {
                    if (battleSystem.enemies[j] == null)
                        battleSystem.enemies[i] = enemies[i];
                }
            }


            //GetComponent<Teleport>().OnTriggerEnter2D(other);

            Engine.e.battleSystem.hud.SetEnemyGroupHUD();

            Engine.e.battleSystem.hud.SetPlayerHUD();

            if (battleCamera != null)
            {
                Engine.e.mainCamera.GetComponent<CinemachineVirtualCamera>().Priority = -1;
            }

            Engine.e.BeginBattle();

        }
    }


    public int GroupExperienceValue()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
                groupExperienceLevel += enemies[i].experiencePoints;
        }
        return groupExperienceLevel;
    }

    public void GroupItemDrops()
    {
        Engine.e.battleSystem.enemyLootReference.GetComponent<TMP_Text>().text = string.Empty;
        Engine.e.battleSystem.enemyLootCountReference.GetComponent<TMP_Text>().text = string.Empty;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                // EXP Gained
                Engine.e.enemyLootReferenceExp.text = string.Empty;
                Engine.e.enemyLootReferenceExp.text = "Exp Gained: " + groupExperienceLevel;

                // Money
                Engine.e.partyMoney += enemies[i].GetComponent<Enemy>().moneyDropAmount;
                Engine.e.enemyLootReferenceG.text = string.Empty;
                Engine.e.enemyLootReferenceG.text = "G: " + enemies[i].GetComponent<Enemy>().moneyDropAmount;

                //Items
                for (int j = 0; j < enemies[i].gameObject.GetComponent<Enemy>().itemDrops.Length; j++)
                {
                    if (enemies[i].gameObject.GetComponent<Enemy>().itemDrops[j] != null)
                    {
                        int itemDropChance = Random.Range(0, 100);

                        int numberDropped = 1;


                        if (itemDropChance <= enemies[i].gameObject.GetComponent<Enemy>().itemDropChance)
                        {
                            Debug.Log(enemies[i].gameObject.GetComponent<Enemy>().itemDrops[j].itemName);
                            Engine.e.partyInventoryReference.AddItemToInventory(enemies[i].gameObject.GetComponent<Enemy>().itemDrops[j]);

                            if (!Engine.e.battleSystem.enemyLootReference.GetComponent<TMP_Text>().text.Contains(enemies[i].gameObject.GetComponent<Enemy>().itemDrops[j].itemName))
                            {
                                Engine.e.battleSystem.enemyLootReference.GetComponent<TMP_Text>().text += "\n";

                                Engine.e.battleSystem.enemyLootReference.GetComponent<TMP_Text>().text += enemies[i].gameObject.GetComponent<Enemy>().itemDrops[j].itemName;
                            }
                            else
                            {
                                numberDropped++;
                                Engine.e.battleSystem.enemyLootCountReference.GetComponent<TMP_Text>().text += "\n" + numberDropped;
                            }
                        }
                    }
                }
            }
        }
    }

    public void HandleQuestObjective()
    {

    }

    public void DestroyGroup()
    {
        Destroy(this.gameObject);
    }

    public void DespawnGroup()
    {
        this.gameObject.SetActive(false);
    }

    public bool CheckEndBattle()
    {
        if (battleSystem.enemies[0] != null && battleSystem.enemies[1] == null && battleSystem.enemies[2] == null && battleSystem.enemies[3] == null)
        {
            if (battleSystem.enemies[0].gameObject.GetComponent<Enemy>().health <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        if (battleSystem.enemies[1] != null && battleSystem.enemies[2] == null && battleSystem.enemies[3] == null)
        {
            if (battleSystem.enemies[0].gameObject.GetComponent<Enemy>().health <= 0
            && battleSystem.enemies[1].gameObject.GetComponent<Enemy>().health <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        if (battleSystem.enemies[1] != null && battleSystem.enemies[2] != null && battleSystem.enemies[3] == null)
        {
            if (battleSystem.enemies[0].gameObject.GetComponent<Enemy>().health <= 0
            && battleSystem.enemies[1].gameObject.GetComponent<Enemy>().health <= 0
            && battleSystem.enemies[2].gameObject.GetComponent<Enemy>().health <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        if (battleSystem.enemies[1] != null && battleSystem.enemies[2] != null && battleSystem.enemies[3] != null)
        {
            if (battleSystem.enemies[0].gameObject.GetComponent<Enemy>().health <= 0
            && battleSystem.enemies[1].gameObject.GetComponent<Enemy>().health <= 0
            && battleSystem.enemies[2].gameObject.GetComponent<Enemy>().health <= 0
            && battleSystem.enemies[3].gameObject.GetComponent<Enemy>().health <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    void MoveToPosition()
    {

        Vector3 leaderPos = Vector3.MoveTowards(Engine.e.activeParty.transform.position, GetComponent<Teleport>().toLocation.transform.position, 4 * Time.deltaTime);
        Vector3 activeParty2Pos = Vector3.MoveTowards(Engine.e.activePartyMember2.transform.position, GetComponent<Teleport>().activeParty2Location.transform.position, 4 * Time.deltaTime);
        Vector3 activeParty3Pos = Vector3.MoveTowards(Engine.e.activePartyMember3.transform.position, GetComponent<Teleport>().activeParty3Location.transform.position, 4 * Time.deltaTime);

        Engine.e.activeParty.GetComponent<Rigidbody2D>().MovePosition(leaderPos);
        if (Engine.e.party[1] != null)
        {
            Engine.e.activePartyMember2.GetComponent<Rigidbody2D>().MovePosition(activeParty2Pos);
        }
        if (Engine.e.party[2] != null)
        {
            Engine.e.activePartyMember3.GetComponent<Rigidbody2D>().MovePosition(activeParty3Pos);
        }

        if (Engine.e.activeParty.GetComponent<Rigidbody2D>().transform.position == GetComponent<Teleport>().toLocation.transform.position
        && Engine.e.activePartyMember2.GetComponent<Rigidbody2D>().transform.position == GetComponent<Teleport>().activeParty2Location.transform.position
        && Engine.e.activePartyMember3.GetComponent<Rigidbody2D>().transform.position == GetComponent<Teleport>().activeParty3Location.transform.position)
        {

            moveToPosition = false;
        }
    }

    public int GetRandomRemainingEnemy()
    {
        remainingEnemies = new List<int>();

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                if (enemies[i].GetComponent<Enemy>().health > 0)
                {
                    remainingEnemies.Add(enemies[i].GetComponent<Enemy>().groupIndex);
                }
            }
        }

        Engine.e.randomIndex = new System.Random();
        randomEnemyIndex = Engine.e.randomIndex.Next(remainingEnemies.Count);
        return remainingEnemies[randomEnemyIndex];
    }

    public int GetNextRemainingEnemy()
    {
        remainingEnemies = new List<int>();

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                remainingEnemies.Add(enemies[i].GetComponent<Enemy>().groupIndex);
                if (enemies[i].GetComponent<Enemy>().isAsleep)
                {
                    enemies[i].GetComponent<Enemy>().sleepTimer++;
                    if (enemies[i].GetComponent<Enemy>().sleepTimer == 3)
                    {
                        enemies[i].GetComponent<Enemy>().isAsleep = false;
                        enemies[i].GetComponent<Enemy>().sleepTimer = 0;
                        if (enemies[i].GetComponent<EnemyMovement>() != null)
                        {
                            enemies[i].GetComponent<EnemyMovement>().enabled = true;
                        }
                        enemies[i].GetComponent<SpriteRenderer>().color = Color.white;

                    }
                    if (enemies[i].GetComponent<Enemy>().isPoisoned)
                    {
                        enemies[i].GetComponent<Enemy>().TakePoisonDamage(i, enemies[i].GetComponent<Enemy>().poisonDmg);
                    }

                    if (enemies[i].GetComponent<Enemy>().deathInflicted)
                    {
                        enemies[i].GetComponent<Enemy>().TakeDeathDamage();
                    }
                }
            }
        }


        if (Engine.e.battleSystem.state == BattleState.ENEMY1TURN)
        {
            for (int i = 1; i < remainingEnemies.Count; i++)
            {
                if (enemies[i] != null)
                {
                    if (enemies[i].GetComponent<Enemy>().health > 0 && !enemies[i].GetComponent<Enemy>().isAsleep)
                    {
                        nextRemainingEnemyIndex = remainingEnemies[i];
                        break;
                    }
                }
            }
        }
        if (Engine.e.battleSystem.state == BattleState.ENEMY2TURN)
        {
            for (int i = 2; i < remainingEnemies.Count; i++)
            {
                if (enemies[i] != null)
                {
                    if (enemies[i].GetComponent<Enemy>().health > 0 && !enemies[i].GetComponent<Enemy>().isAsleep)
                    {
                        nextRemainingEnemyIndex = remainingEnemies[i];
                        break;
                    }
                }
            }
        }
        if (Engine.e.battleSystem.state == BattleState.ENEMY3TURN)
        {

            if (enemies[3] != null)
            {
                if (enemies[3].GetComponent<Enemy>().health > 0 && !enemies[3].GetComponent<Enemy>().isAsleep)
                {
                    nextRemainingEnemyIndex = remainingEnemies[3];
                }
            }
        }

        if (Engine.e.battleSystem.state == BattleState.ENEMY4TURN || Engine.e.battleSystem.state == BattleState.CHAR1TURN || Engine.e.battleSystem.state == BattleState.CHAR2TURN ||
        Engine.e.battleSystem.state == BattleState.CHAR3TURN)
        {
            for (int i = 0; i < remainingEnemies.Count; i++)
            {
                if (enemies[i] != null)
                {
                    if (enemies[i].GetComponent<Enemy>().health > 0 && !enemies[i].GetComponent<Enemy>().isAsleep)
                    {
                        nextRemainingEnemyIndex = remainingEnemies[i];
                        break;
                    }
                }
            }
        }
        Debug.Log(nextRemainingEnemyIndex);
        return nextRemainingEnemyIndex;
    }


    private void FixedUpdate()
    {
        if (moveToPosition)
            MoveToPosition();
    }

    public List<Enemy> GetEnemiesInGroup()
    {
        List<Enemy> enemiesInGroup = new List<Enemy>();

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemiesInGroup.Add(enemies[i].GetComponent<Enemy>());
            }
        }
        return enemiesInGroup;
    }

    public List<Enemy> GetEnemyInGroup(string _enemyName)
    {
        List<Enemy> enemiesInGroup = new List<Enemy>();

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null && enemies[i].GetComponent<Enemy>().enemyName == _enemyName)
            {
                enemiesInGroup.Add(enemies[i].GetComponent<Enemy>());
            }
        }
        return enemiesInGroup;
    }

    public void GetEnemyIndex(Enemy enemy)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                if (enemies[i].GetComponent<Enemy>().name == enemy.name)
                {
                    if (enemies[i].GetComponent<Enemy>().groupIndex == -1)
                    {
                        enemies[i].GetComponent<Enemy>().groupIndex = i;
                        break;
                    }
                }
            }
        }
    }
}

