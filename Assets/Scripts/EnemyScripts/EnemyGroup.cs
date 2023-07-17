using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using System.Linq;

public class EnemyGroup : MonoBehaviour
{
    public Quest quest;
    public int spawnChance;
    public static EnemyGroup enemyGroup;
    public Enemy[] enemies;
    public List<Item> itemDrops;
    public Item item;
    public BattleSystem battleSystem;
    public float groupExperienceLevel = 0;
    public CinemachineVirtualCamera battleCamera;
    public List<string> enemyLootReferenceText;
    public bool moveToPosition = false, startBattle = false;
    public GameObject char1SwitchPos, char2SwitchPos, char3SwitchPos;
    public bool groupInBattle = false;
    public List<Enemy> remainingEnemies;
    int randomEnemyIndex;
    int nextRemainingEnemyIndex;
    public bool inWorld;
    public bool camSet = false;

    void Start()
    {

        GetComponent<EnemyGroupGenerator>().GenerateEnemyGroup();
        SetGroupIndexes(); // Consider moving into GenerateEnemyGroup()

        if (inWorld)
        {
            GroupExperienceValue();
        }

        battleSystem = Engine.e.battleSystem;

    }

    public void OnTriggerEnter2D(Collider2D other)
    {



        if (other.name == "ActiveParty")
        {
            moveToPosition = true;
            startBattle = false;
            camSet = false;
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
        }

        Engine.e.inBattle = true;
        if (battleCamera != null)
        {
            Engine.e.mainVirtualCamera.GetComponent<CinemachineVirtualCamera>().Priority = -1;

        }
    }


    public float GroupExperienceValue()
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
                Engine.e.partyMoney += enemies[i].GetComponent<Enemy>().moneyLootAmount;
                Engine.e.enemyLootReferenceG.text = string.Empty;
                Engine.e.enemyLootReferenceG.text = "G: " + enemies[i].GetComponent<Enemy>().moneyLootAmount;

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
            if (battleSystem.enemies[0].gameObject.GetComponent<Enemy>().currentHealth <= 0)
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
            if (battleSystem.enemies[0].gameObject.GetComponent<Enemy>().currentHealth <= 0
            && battleSystem.enemies[1].gameObject.GetComponent<Enemy>().currentHealth <= 0)
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
            if (battleSystem.enemies[0].gameObject.GetComponent<Enemy>().currentHealth <= 0
            && battleSystem.enemies[1].gameObject.GetComponent<Enemy>().currentHealth <= 0
            && battleSystem.enemies[2].gameObject.GetComponent<Enemy>().currentHealth <= 0)
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
            if (battleSystem.enemies[0].gameObject.GetComponent<Enemy>().currentHealth <= 0
            && battleSystem.enemies[1].gameObject.GetComponent<Enemy>().currentHealth <= 0
            && battleSystem.enemies[2].gameObject.GetComponent<Enemy>().currentHealth <= 0
            && battleSystem.enemies[3].gameObject.GetComponent<Enemy>().currentHealth <= 0)
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

        if (Engine.e.party[2] != null && Engine.e.party[1] != null)
        {
            if (Engine.e.activeParty.GetComponent<Rigidbody2D>().transform.position == GetComponent<Teleport>().toLocation.transform.position
            && Engine.e.activePartyMember2.GetComponent<Rigidbody2D>().transform.position == GetComponent<Teleport>().activeParty2Location.transform.position
            && Engine.e.activePartyMember3.GetComponent<Rigidbody2D>().transform.position == GetComponent<Teleport>().activeParty3Location.transform.position)
            {


                moveToPosition = false;

            }
        }
        else
        {
            if (Engine.e.party[2] == null && Engine.e.party[1] != null)
            {
                if (Engine.e.activeParty.GetComponent<Rigidbody2D>().transform.position == GetComponent<Teleport>().toLocation.transform.position
                && Engine.e.activePartyMember2.GetComponent<Rigidbody2D>().transform.position == GetComponent<Teleport>().activeParty2Location.transform.position)
                {

                    moveToPosition = false;

                }
            }
            else
            {
                if (Engine.e.party[2] == null && Engine.e.party[1] == null)
                {
                    if (Engine.e.activeParty.GetComponent<Rigidbody2D>().transform.position == GetComponent<Teleport>().toLocation.transform.position)
                    {

                        moveToPosition = false;

                    }
                }
            }
        }
    }

    void InitialMoveToPosition()
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

        if (Engine.e.party[2] != null && Engine.e.party[1] != null)
        {
            if (Engine.e.activeParty.GetComponent<Rigidbody2D>().transform.position == GetComponent<Teleport>().toLocation.transform.position
            && Engine.e.activePartyMember2.GetComponent<Rigidbody2D>().transform.position == GetComponent<Teleport>().activeParty2Location.transform.position
            && Engine.e.activePartyMember3.GetComponent<Rigidbody2D>().transform.position == GetComponent<Teleport>().activeParty3Location.transform.position)
            {


                moveToPosition = false;
                if (battleCamera != null)
                {
                    Engine.e.mainVirtualCamera.GetComponent<CinemachineVirtualCamera>().Priority = -1;

                }
                startBattle = true;
                Engine.e.BeginBattle();

            }
        }
        else
        {
            if (Engine.e.party[2] == null && Engine.e.party[1] != null)
            {
                if (Engine.e.activeParty.GetComponent<Rigidbody2D>().transform.position == GetComponent<Teleport>().toLocation.transform.position
                && Engine.e.activePartyMember2.GetComponent<Rigidbody2D>().transform.position == GetComponent<Teleport>().activeParty2Location.transform.position)
                {

                    moveToPosition = false;
                    startBattle = true;
                    Engine.e.BeginBattle();


                }
            }
            else
            {
                if (Engine.e.party[2] == null && Engine.e.party[1] == null)
                {
                    if (Engine.e.activeParty.GetComponent<Rigidbody2D>().transform.position == GetComponent<Teleport>().toLocation.transform.position)
                    {

                        moveToPosition = false;
                        if (battleCamera != null)
                        {
                            Engine.e.mainVirtualCamera.GetComponent<CinemachineVirtualCamera>().Priority = -1;
                            camSet = true;
                        }
                        startBattle = true;
                        Engine.e.BeginBattle();


                    }
                }
            }
        }
    }


    public void SetGroupIndexes()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].groupIndex = i;
            }
        }
    }

    public int GetRandomRemainingEnemy()
    {
        remainingEnemies = new List<Enemy>();

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                if (enemies[i].GetComponent<Enemy>().currentHealth > 0)
                {
                    remainingEnemies.Add(enemies[i]);
                }
            }
        }

        int randomEnemyIndex = Random.Range(0, remainingEnemies.Count);
        return remainingEnemies[randomEnemyIndex].groupIndex;
    }
    /*
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
                        if (enemies[i].GetComponent<Enemy>().currentHealth > 0 && !enemies[i].GetComponent<Enemy>().isAsleep)
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
                        if (enemies[i].GetComponent<Enemy>().currentHealth > 0 && !enemies[i].GetComponent<Enemy>().isAsleep)
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
                    if (enemies[3].GetComponent<Enemy>().currentHealth > 0 && !enemies[3].GetComponent<Enemy>().isAsleep)
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
                        if (enemies[i].GetComponent<Enemy>().currentHealth > 0 && !enemies[i].GetComponent<Enemy>().isAsleep)
                        {
                            nextRemainingEnemyIndex = remainingEnemies[i];
                            break;
                        }
                    }
                }
            }
            Debug.Log(nextRemainingEnemyIndex);
            return nextRemainingEnemyIndex;
        }*/


    private void FixedUpdate()
    {
        if (moveToPosition && startBattle)
        {
            MoveToPosition();
        }
        if (moveToPosition && !startBattle)
        {
            InitialMoveToPosition();
        }
    }

    public List<Enemy> GetRemainingEnemiesInGroup()
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

    public int GetLowestHealthEnemy()
    {

        List<Enemy> enemyList = new List<Enemy>();

        enemyList = GetRemainingEnemiesInGroup();

        enemyList = enemyList.OrderBy(enemy => enemy.currentHealth).ToList();

        Debug.Log(enemyList[0].groupIndex);
        return enemyList[0].groupIndex;
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

    public int GetEnemyIndex(Enemy _enemy)
    {
        int _index = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                if (enemies[i].GetComponent<Enemy>().name == _enemy.name)
                {
                    if (enemies[i].GetComponent<Enemy>().groupIndex == -1)
                    {
                        enemies[i].GetComponent<Enemy>().groupIndex = i;
                        _index = i;
                        break;
                    }
                }
            }
        }

        return _index;
    }
}

