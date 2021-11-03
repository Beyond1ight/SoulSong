using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Cinemachine;
using UnityEngine.SceneManagement;

public enum BattleState { START, CHAR1TURN, CHAR2TURN, CHAR3TURN, ENEMY1TURN, ENEMY2TURN, ENEMY3TURN, ENEMY4TURN, CONFCHAR1, CONFCHAR2, CONFCHAR3, ATBCHECK, QUEUECHECK, WON, LOST, LEVELUP, LEVELUPCHECK }
public class BattleSystem : MonoBehaviour
{

    // General Info
    public BattleState state;
    public BattleState currentState;
    //public BattleState nextInQueue;
    public BattleState currentInQueue;
    public Queue<BattleState> battleQueue;
    public ActiveParty activeParty;
    public GameObject[] enemies;
    public EnemyGroup enemyGroup;
    public bool hpRestore, mpRestore;
    public bool physicalAttack = false;
    public bool skillPhysicalAttack = false, skillRangedAttack = false, skillTargetSupport = false, skillSelfSupport = false;
    public bool dropAttack = false;
    public bool usingItem = false;
    public float damageTotal;
    public float skillBoostTotal;
    int randTarget;
    public int target;
    public bool isDead;
    public float enemyGroupExperiencePoints;
    public bool dropExists = false;
    public bool enemyMoving = false, charMoving = false;
    public Vector3 leaderPos, activeParty2Pos, activeParty3Pos;
    public GameObject[] availableChar1Buttons, availableChar2Buttons, availableChar3Buttons;
    bool charAtBattlePos = true;
    bool charAttacking = false;
    bool enemyAtBattlePos = true;
    bool enemyAttacking = false;
    bool charAttackDrop = false, enemyAttackDrop = false;
    bool confuseAttack = false;
    public Drops lastDropChoice;
    public Skills lastSkillChoice;
    public bool charUsingSkill;
    public int teamTargetChoice;
    public bool attackingTeam = false;
    public float char1ATB, char2ATB, char3ATB, enemy1ATB, enemy2ATB, enemy3ATB, enemy4ATB, ATBReady;
    public Slider char1ATBGuage, char2ATBGuage, char3ATBGuage, enemy1ATBGuage, enemy2ATBGuage, enemy3ATBGuage, enemy4ATBGuage;

    // UI References
    public bool inBattleMenu = false;

    public GameObject char1UI;
    public GameObject char2UI;
    public GameObject char3UI;

    public GameObject[] enemyUI;
    public GameObject[] allyTargetButtons;
    public GameObject[] targetButtons;
    public TextMeshProUGUI dialogueText;

    public GameObject[] returnToPanelButton;
    public GameObject[] skillAllyTargetButtons, skillEnemyTargetButtons;
    public GameObject battleItemMenu;
    public BattleItemSlot[] battleItems;
    public GameObject[] char1MenuButtons;
    public GameObject[] char2MenuButtons;
    public GameObject[] char3MenuButtons;

    // Skills
    public GameObject[] skillButtons;

    // Drops
    public GameObject[] dropsButtons;
    public GameObject fireDropAnim, enemyFireDropAnim;
    public GameObject iceDropAnim, enemyIceDropAnim;
    public GameObject waterDropAnim, enemyWaterDropAnim;
    public GameObject lightningDropAnim, enemyLightningDropAnim;
    public GameObject holyDropAnim, enemyHolyDropAnim;
    public GameObject shadowDropAnim, enemyShadowDropAnim, poisonAnim, enemyPoisonAnim;
    public GameObject sleepAnim, enemySleepAnim, confuseAnim, enemyConfuseAnim;
    public GameObject siphonAnim;

    public GameObject[] switchButtons;
    public GameObject skillListReference;
    public GameObject dropsListReference;


    // Misc Info/References
    public string[] charIndexName;
    int currentIndex;
    int nextIndex;
    public bool dodgedAttack = false;
    public bool battleSwitchButtons = false;
    public bool dontDisplayDmgPopup = false;
    public BattleHUD hud;
    int currentMoveIndex;
    int enemyDropChoice;
    int charDropChoice;
    public GameObject damagePopup, levelUpPopup, deathTimerPopup;
    public Material originalMaterial;
    public Material damageFlash;
    public bool failedItemUse = false;
    public int skillTarget;
    public GameObject spriteTarget;
    int skillTierChoice;
    bool checkingForRandomDrop = false;
    int randomDropIndex;
    bool partyCheckNext = false;
    List<Drops> charRandomDropList;
    public bool char1Ready = false, char2Ready = false, char3Ready = false;
    bool targetCheck = false;
    int charAttackingIndex = 0;
    GameObject characterAttacking = null;
    Character characterAttackIndex = null;
    public GameObject characterDropTarget;
    bool confuseTargetCheck = false;
    bool char1MenuButtonsActive = false, char2MenuButtonsActive = false, char3MenuButtonsActive = false;
    bool pressUp, pressDown;
    public int inventoryPointerIndex = 0, vertMove = 0;

    public int char1AttackTarget, char1SupportTarget, char2AttackTarget, char2SupportTarget, char3AttackTarget, char3SupportTarget;
    public bool char1Attacking, char1Supporting, char2Attacking, char2Supporting, char3Attacking, char3Supporting;
    public bool char1UsingItem, char2UsingItem, char3UsingItem;
    public bool char1DropAttack, char1SkillAttack, char2DropAttack, char2SkillAttack, char3DropAttack, char3SkillAttack;
    public Skills char1SkillChoice, char2SkillChoice, char3SkillChoice;
    public bool char1PhysicalAttack, char1SkillPhysicalAttack, char1SkillRangedAttack, char2PhysicalAttack, char2SkillPhysicalAttack, char2SkillRangedAttack,
    char3PhysicalAttack, char3SkillPhysicalAttack, char3SkillRangedAttack;
    public bool char1ConfusedReady, char2ConfusedReady, char3ConfusedReady;
    public int enemy1AttackTarget, enemy2AttackTarget, enemy3AttackTarget, enemy4AttackTarget;
    public bool enemy1Ready, enemy2Ready, enemy3Ready, enemy4Ready;
    public bool checkNextInQueue;
    public int previousTargetReferenceChar, previousTargetReferenceEnemy; // For HUD and Drop Reference

    [SerializeField]
    public bool grieveStatBoost = false, macStatBoost = false, fieldStatBoost = false, riggsStatBoost;
    [SerializeField]
    float grievePhysicalBoost, grieveHealthBoost, grieveManaBoost, grieveEnergyBoost, grieveDodgeBoost, macPhysicalBoost, macHealthBoost,
    macManaBoost, macEnergyBoost, macDodgeBoost, fieldPhysicalBoost, fieldHealthBoost, fieldManaBoost, fieldEnergyBoost, fieldDodgeBoost,
    riggsPhysicalBoost, riggsHealthBoost, riggsManaBoost, riggsEnergyBoost, riggsDodgeBoost;

    public IEnumerator SetupBattle()
    {
        Engine.e.activeParty.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        charAttacking = false;
        enemyAttacking = false;
        enemyAtBattlePos = true;
        damageTotal = 0;
        isDead = false;
        enemyGroup.groupInBattle = true;
        confuseAttack = false;
        ATBReady = 100;

        char1ATB += 20;
        char2ATB = 0;
        char3ATB = 0;
        enemy1ATB = 0;
        enemy2ATB = 0;
        enemy3ATB = 0;
        enemy4ATB = 0;

        battleQueue = new Queue<BattleState>(7);

        Engine.e.partyInventoryReference.AddItemsToBattleInventory();

        leaderPos = enemyGroup.GetComponent<Teleport>().toLocation.transform.position;
        activeParty2Pos = enemyGroup.GetComponent<Teleport>().activeParty2Location.transform.position;
        activeParty3Pos = enemyGroup.GetComponent<Teleport>().activeParty3Location.transform.position;

        state = BattleState.START;
        dialogueText.text = string.Empty;
        enemyGroupExperiencePoints = enemyGroup.groupExperienceLevel;
        charIndexName = new string[activeParty.activeParty.Length];

        charIndexName[0] = activeParty.activeParty[0].gameObject.GetComponent<Character>().characterName;

        for (int i = 0; i < Engine.e.activeParty.activeParty.Length; i++)
        {
            if (Engine.e.activeParty.activeParty[1] != null)
            {
                char2UI.SetActive(true);
                charIndexName[1] = activeParty.activeParty[1].gameObject.GetComponent<Character>().characterName;
            }
            if (Engine.e.activeParty.activeParty[2] != null)
            {
                char3UI.SetActive(true);

                charIndexName[2] = activeParty.activeParty[2].gameObject.GetComponent<Character>().characterName;
            }
        }

        EnemyGroup.enemyGroup = this.enemyGroup;
        ActivateEnemyUI();


        yield return new WaitForSeconds(2f);
        //  state = BattleState.CHAR1TURN;
        state = BattleState.ATBCHECK;
        // Char1Turn();

    }

    public void HandleQueue()
    {

        if (!isDead)
        {
            if (state == currentInQueue && state != BattleState.ATBCHECK)
            {
                state = BattleState.ATBCHECK;
            }

            // ATB Management
            if (currentInQueue != BattleState.ENEMY1TURN && currentInQueue != BattleState.ENEMY2TURN
                   && currentInQueue != BattleState.ENEMY3TURN && currentInQueue != BattleState.ENEMY4TURN)
            {

                if (!battleQueue.Contains(BattleState.CHAR1TURN) && !battleQueue.Contains(BattleState.CONFCHAR1))
                {
                    if (activeParty.activeParty[0].GetComponent<Character>().currentHealth > 0
                    && !activeParty.activeParty[0].GetComponent<Character>().isAsleep)
                    {
                        if (char1ATB < ATBReady)
                        {
                            //char1ATB += (activeParty.activeParty[0].GetComponent<Character>().haste * 10) / 50;
                            char1ATB += activeParty.activeParty[0].GetComponent<Character>().haste * Time.deltaTime;
                            char1ATBGuage.value = char1ATB;

                        }
                        else
                        {
                            char1ATB = ATBReady;
                            char1ATBGuage.value = char1ATB;

                            if (!activeParty.activeParty[0].GetComponent<Character>().isConfused)
                            {
                                if (state == BattleState.ATBCHECK
                                    && !enemyMoving && !enemyAttackDrop && !confuseTargetCheck && !confuseAttack)
                                {
                                    Char1Turn();
                                }
                                else
                                {
                                    if (!char1Ready)
                                    {
                                        if (!activeParty.activeParty[0].GetComponent<Character>().isConfused && !inBattleMenu)
                                        {
                                            ActivateChar1MenuButtons();
                                            char1Ready = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                char1Ready = true;
                                battleQueue.Enqueue(BattleState.CONFCHAR1);
                            }
                        }
                    }
                }

                if (!battleQueue.Contains(BattleState.CHAR2TURN) && !battleQueue.Contains(BattleState.CONFCHAR2))
                {
                    if (activeParty.activeParty[1] != null)
                    {
                        if (activeParty.activeParty[1].GetComponent<Character>().currentHealth > 0
                        && !activeParty.activeParty[1].GetComponent<Character>().isAsleep)
                        {
                            if (char2ATB < ATBReady)
                            {

                                char2ATB += activeParty.activeParty[1].GetComponent<Character>().haste * Time.deltaTime;
                                char2ATBGuage.value = char2ATB;

                            }
                            else
                            {
                                char2ATB = ATBReady;
                                char2ATBGuage.value = char2ATB;

                                if (!activeParty.activeParty[1].GetComponent<Character>().isConfused)
                                {
                                    if (state == BattleState.ATBCHECK
                                        && !enemyMoving && !enemyAttackDrop && !confuseTargetCheck && !confuseAttack)
                                    {
                                        Char2Turn();
                                    }
                                    else
                                    {
                                        if (!char2Ready)
                                        {
                                            if (!activeParty.activeParty[1].GetComponent<Character>().isConfused && !inBattleMenu)
                                            {
                                                ActivateChar2MenuButtons();
                                                char2Ready = true;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    char2Ready = true;
                                    battleQueue.Enqueue(BattleState.CONFCHAR2);
                                }
                            }
                        }
                    }
                }

                if (!battleQueue.Contains(BattleState.CHAR3TURN) && !battleQueue.Contains(BattleState.CONFCHAR3))
                {
                    if (activeParty.activeParty[2] != null)
                    {
                        if (activeParty.activeParty[2].GetComponent<Character>().currentHealth > 0
                        && !activeParty.activeParty[2].GetComponent<Character>().isAsleep)
                        {
                            if (char3ATB < ATBReady)
                            {
                                char3ATB += activeParty.activeParty[2].GetComponent<Character>().haste * Time.deltaTime;
                                char3ATBGuage.value = char3ATB;
                            }
                            else
                            {
                                char3ATB = ATBReady;
                                char3ATBGuage.value = char3ATB;

                                if (!activeParty.activeParty[2].GetComponent<Character>().isConfused)
                                {
                                    if (state == BattleState.ATBCHECK
                                        && !enemyMoving && !enemyAttackDrop && !confuseTargetCheck && !confuseAttack)
                                    {
                                        Char3Turn();
                                    }
                                    else
                                    {
                                        if (!char3Ready)
                                        {
                                            if (!activeParty.activeParty[2].GetComponent<Character>().isConfused && !inBattleMenu)
                                            {
                                                ActivateChar3MenuButtons();
                                                char3Ready = true;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    char3Ready = true;
                                    battleQueue.Enqueue(BattleState.CONFCHAR3);
                                }
                            }
                        }
                    }
                }

                if (!battleQueue.Contains(BattleState.ENEMY1TURN))
                {
                    if (enemies[0].GetComponent<Enemy>().health > 0 && !enemies[0].GetComponent<Enemy>().isAsleep)
                    {
                        if (enemy1ATB < ATBReady)
                        {
                            float randomVariation = Random.Range(0.65f, 1f);
                            enemy1ATB += ((enemies[0].GetComponent<Enemy>().haste * randomVariation) * Time.deltaTime);
                            enemy1ATBGuage.value = enemy1ATB;
                        }
                        else
                        {
                            enemy1ATB = ATBReady;
                            enemy1Ready = true;
                            battleQueue.Enqueue(BattleState.ENEMY1TURN);
                        }
                    }

                    if (!battleQueue.Contains(BattleState.ENEMY2TURN))
                    {
                        if (enemies[1] != null)
                        {
                            if (enemies[1].GetComponent<Enemy>().health > 0 && !enemies[1].GetComponent<Enemy>().isAsleep)
                            {
                                if (enemy2ATB < ATBReady)
                                {
                                    float randomVariation = Random.Range(0.65f, 1f);
                                    enemy2ATB += ((enemies[1].GetComponent<Enemy>().haste * randomVariation) * Time.deltaTime);
                                    enemy2ATBGuage.value = enemy2ATB;

                                }
                                else
                                {
                                    enemy2ATB = ATBReady;
                                    enemy2Ready = true;
                                    battleQueue.Enqueue(BattleState.ENEMY2TURN);
                                }
                            }
                        }
                    }

                    if (!battleQueue.Contains(BattleState.ENEMY3TURN))
                    {
                        if (enemies[2] != null)
                        {
                            if (enemies[2].GetComponent<Enemy>().health > 0 && !enemies[2].GetComponent<Enemy>().isAsleep)
                            {
                                if (enemy3ATB < ATBReady)
                                {
                                    float randomVariation = Random.Range(0.65f, 1f);
                                    enemy3ATB += ((enemies[2].GetComponent<Enemy>().haste * randomVariation) * Time.deltaTime);
                                    enemy3ATBGuage.value = enemy3ATB;
                                }
                                else
                                {
                                    enemy3ATB = ATBReady;
                                    enemy3Ready = true;
                                    battleQueue.Enqueue(BattleState.ENEMY3TURN);
                                }
                            }
                        }
                    }

                    if (!battleQueue.Contains(BattleState.ENEMY4TURN))
                    {
                        if (enemies[3] != null)
                        {
                            if (enemies[3].GetComponent<Enemy>().health > 0 && !enemies[3].GetComponent<Enemy>().isAsleep)
                            {
                                if (enemy4ATB < ATBReady)
                                {
                                    float randomVariation = Random.Range(0.65f, 1f);
                                    enemy4ATB += ((enemies[3].GetComponent<Enemy>().haste * randomVariation) * Time.deltaTime);
                                    enemy4ATBGuage.value = enemy4ATB;
                                }
                                else
                                {
                                    enemy4ATB = ATBReady;
                                    enemy4Ready = true;
                                    battleQueue.Enqueue(BattleState.ENEMY4TURN);
                                }
                            }
                        }
                    }
                }
            }

            // Queue Control
            if (battleQueue.Count == 0)
            {
                currentInQueue = BattleState.QUEUECHECK;
            }
            else
            {
                if (currentInQueue == BattleState.QUEUECHECK)
                {
                    currentInQueue = battleQueue.Peek();
                }

                if (currentInQueue == BattleState.CHAR1TURN)
                {

                    if (char1Attacking)
                    {
                        physicalAttack = char1PhysicalAttack;
                        dropAttack = char1DropAttack;
                        char1PhysicalAttack = false;
                        char1Attacking = false;
                        char1Ready = false;
                        if (!char1SkillAttack)
                        {
                            StartCoroutine(CharAttack(char1AttackTarget));
                        }
                        else
                        {
                            char1SkillAttack = false;
                            CharSkills(char1SkillChoice);
                        }
                    }

                    if (char1Supporting)
                    {

                        usingItem = char1UsingItem;
                        dropAttack = char1DropAttack;
                        Engine.e.charBeingTargeted = char1SupportTarget;
                        char1DropAttack = false;
                        char1UsingItem = false;
                        char1Supporting = false;
                        StartCoroutine(CharSupport(char1SupportTarget));

                    }

                }

                if (currentInQueue == BattleState.CHAR2TURN)
                {
                    if (!activeParty.activeParty[0].GetComponent<Character>().isConfused)
                    {
                        if (char2Attacking)
                        {
                            physicalAttack = char2PhysicalAttack;
                            // skillPhysicalAttack = char2SkillPhysicalAttack;
                            dropAttack = char2DropAttack;
                            //char2SkillPhysicalAttack = false;
                            char2PhysicalAttack = false;
                            char2Attacking = false;

                            if (!char2SkillAttack)
                            {
                                StartCoroutine(CharAttack(char2AttackTarget));
                            }
                            else
                            {
                                char2SkillAttack = false;
                                CharSkills(char2SkillChoice);
                            }
                        }


                        if (char2Supporting)
                        {

                            usingItem = char2UsingItem;
                            dropAttack = char2DropAttack;
                            Engine.e.charBeingTargeted = char2SupportTarget;
                            char2DropAttack = false;
                            char2UsingItem = false;
                            char2Supporting = false;
                            StartCoroutine(CharSupport(char2SupportTarget));
                        }
                    }
                    else
                    {
                        char2Ready = false;
                        Char2Turn();
                    }
                }

                if (currentInQueue == BattleState.CHAR3TURN)
                {
                    if (char3Attacking)
                    {
                        physicalAttack = char3PhysicalAttack;
                        skillPhysicalAttack = char3SkillPhysicalAttack;
                        dropAttack = char3DropAttack;
                        char3SkillPhysicalAttack = false;
                        char3PhysicalAttack = false;
                        char3Attacking = false;

                        if (!char3SkillAttack)
                        {
                            StartCoroutine(CharAttack(char3AttackTarget));
                        }
                        else
                        {
                            char3SkillAttack = false;
                            CharSkills(char3SkillChoice);
                        }
                    }

                    if (char3Supporting)
                    {

                        usingItem = char3UsingItem;
                        dropAttack = char3DropAttack;
                        Engine.e.charBeingTargeted = char3SupportTarget;
                        char3DropAttack = false;
                        char3UsingItem = false;
                        char3Supporting = false;
                        StartCoroutine(CharSupport(char3SupportTarget));
                    }
                }

                if (currentInQueue == BattleState.CONFCHAR1 && char1Ready)
                {
                    Char1ConfusedTurn();
                }

                if (currentInQueue == BattleState.CONFCHAR2 && char2Ready)
                {
                    Char2ConfusedTurn();
                }

                if (currentInQueue == BattleState.CONFCHAR3 && char3Ready)
                {
                    Char3ConfusedTurn();
                }

                if (currentInQueue == BattleState.ENEMY1TURN && enemy1Ready)
                {
                    Enemy1Turn();
                }
                if (currentInQueue == BattleState.ENEMY2TURN && enemy2Ready)
                {
                    Enemy2Turn();
                }
                if (currentInQueue == BattleState.ENEMY3TURN && enemy3Ready)
                {
                    Enemy3Turn();
                }
                if (currentInQueue == BattleState.ENEMY4TURN && enemy4Ready)
                {
                    Enemy4Turn();
                }
            }
        }
    }

    public IEnumerator CharAttack(int enemyTarget)
    {

        DeactivateAttackButtons();
        inBattleMenu = false;
        int index = 0;

        if (currentInQueue == BattleState.CHAR1TURN)
        {
            index = 0;
        }
        if (currentInQueue == BattleState.CHAR2TURN)
        {
            index = 1;
        }
        if (currentInQueue == BattleState.CHAR3TURN)
        {
            index = 2;
        }


        if (enemies[enemyTarget].GetComponent<Enemy>().health <= 0)
        {
            target = enemyGroup.GetRandomRemainingEnemy();
        }
        else
        {
            target = enemyTarget;

        }

        previousTargetReferenceChar = target;

        dialogueText.text = string.Empty;

        if (physicalAttack == true)
        {
            charMoving = true;
            targetCheck = true;
            physicalAttack = false;
            charAttacking = true;
        }

        if (skillPhysicalAttack == true)
        {
            charUsingSkill = true;
            charMoving = true;
            targetCheck = true;

            skillPhysicalAttack = false;
            charAttacking = true;

        }

        if (skillRangedAttack == true)
        {
            targetCheck = true;
            charMoving = false;
            charAttackDrop = true;
            skillRangedAttack = false;
            enemies[enemyTarget].GetComponent<Enemy>().TakeSkillDamage(damageTotal, enemyTarget);
            hud.displayMana[index].text = activeParty.activeParty[index].gameObject.GetComponent<Character>().currentMana.ToString();

            if (enemies[enemyTarget].gameObject.GetComponent<Enemy>().health <= 0)
            {
                enemies[enemyTarget].gameObject.GetComponent<Enemy>().health = 0;

                isDead = EnemyGroup.enemyGroup.CheckEndBattle();
                yield return new WaitForSeconds(0.1f);
            }
        }

        if (dropAttack == true)
        {
            charMoving = false;
            targetCheck = true;

            charAttackDrop = true;

            enemies[enemyTarget].gameObject.GetComponent<Enemy>().TakeDropDamage(enemyTarget, activeParty.activeParty[index].GetComponent<Character>().GetDropChoice());
            activeParty.activeParty[index].gameObject.GetComponent<Character>().currentMana -= Mathf.Round(activeParty.activeParty[index].GetComponent<Character>().GetDropChoice().dropCost
            - (activeParty.activeParty[index].GetComponent<Character>().GetDropChoice().dropCost * activeParty.activeParty[index].GetComponent<Character>().dropCostReduction / 100) + 0.45f);
            activeParty.activeParty[index].gameObject.GetComponent<Character>().ResetDropChoice();
            dropAttack = false;
            hud.displayMana[index].text = activeParty.activeParty[index].gameObject.GetComponent<Character>().currentMana.ToString();

            if (enemies[enemyTarget].gameObject.GetComponent<Enemy>().health <= 0)
            {
                enemies[enemyTarget].gameObject.GetComponent<Enemy>().health = 0;

                isDead = EnemyGroup.enemyGroup.CheckEndBattle();
                yield return new WaitForSeconds(0.1f);
            }

            charAttacking = false;
        }
    }

    public IEnumerator AttackMoveToTargetChar()
    {

        Enemy enemy = enemies[target].GetComponent<Enemy>();

        if (targetCheck)
        {
            targetCheck = false;

            if (currentInQueue == BattleState.CHAR1TURN || currentInQueue == BattleState.CONFCHAR1)
            {
                charAttackingIndex = 0;
                characterAttacking = Engine.e.activeParty.gameObject;
                characterAttackIndex = Engine.e.activeParty.activeParty[0].GetComponent<Character>();
            }
            if (currentInQueue == BattleState.CHAR2TURN || currentInQueue == BattleState.CONFCHAR2)
            {
                charAttackingIndex = 1;
                characterAttacking = Engine.e.activePartyMember2;
                characterAttackIndex = Engine.e.activeParty.activeParty[1].GetComponent<Character>();

            }
            if (currentInQueue == BattleState.CHAR3TURN || currentInQueue == BattleState.CONFCHAR3)
            {
                charAttackingIndex = 2;
                characterAttacking = Engine.e.activePartyMember3;
                characterAttackIndex = Engine.e.activeParty.activeParty[2].GetComponent<Character>();

            }

            if (confuseAttack)
            {
                confuseAttack = false;
                Debug.Log("Adding confuse timer");
                activeParty.activeParty[charAttackingIndex].GetComponent<Character>().confuseTimer++;
                if (activeParty.activeParty[charAttackingIndex].GetComponent<Character>().confuseTimer == 3)
                {
                    activeParty.activeParty[charAttackingIndex].GetComponent<Character>().isConfused = false;
                    activeParty.activeParty[charAttackingIndex].GetComponent<Character>().confuseTimer = 0;
                }

                if (activeParty.activeParty[charAttackingIndex].GetComponent<Character>().isPoisoned)
                {
                    GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, characterAttacking.transform.position, Quaternion.identity);

                    isDead = Engine.e.TakePoisonDamage(currentIndex, activeParty.activeParty[charAttackingIndex].GetComponent<Character>().poisonDmg);

                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = activeParty.activeParty[charAttackingIndex].GetComponent<Character>().poisonDmg.ToString();
                    Destroy(dmgPopup, 1f);
                }

                if (activeParty.activeParty[charAttackingIndex].GetComponent<Character>().deathInflicted)
                {
                    isDead = activeParty.activeParty[charAttackingIndex].GetComponent<Character>().TakeDeathDamage(charAttackingIndex);
                }
                if (Engine.e.activeParty.activeParty[Engine.e.charBeingTargeted].GetComponent<Character>().currentHealth <= 0)
                {
                    Engine.e.activeParty.activeParty[Engine.e.charBeingTargeted].GetComponent<Character>().currentHealth = 0;

                    if (isDead)
                    {
                        state = BattleState.LOST;
                        yield return new WaitForSeconds(2f);
                        StartCoroutine(EndBattle());
                    }
                }
            }

            if (Engine.e.battleModeActive && !activeParty.activeParty[charAttackingIndex].GetComponent<Character>().isConfused)
            {
                //    ActiveCheckNext();
                //state = BattleState.ATBCHECK;

            }
        }


        if (charAttacking && charAtBattlePos)
        {
            switch (charAttackingIndex)
            {
                case 0:
                    char1ATB = 0;
                    char1Ready = false;
                    break;
                case 1:
                    char2ATB = 0;
                    char2Ready = false;
                    break;
                case 2:
                    char3ATB = 0;
                    char3Ready = false;
                    break;
            }

            enemyGroup.moveToPosition = false;
            Vector3 targetPos = Vector3.MoveTowards(characterAttacking.GetComponent<Rigidbody2D>().transform.position, enemies[target].transform.position, 8f * Time.deltaTime);
            characterAttacking.GetComponent<Rigidbody2D>().MovePosition(targetPos);

            if (Vector3.Distance(characterAttacking.transform.position, enemies[target].transform.position) < 1)
            {

                charAttacking = false;
                charAtBattlePos = false;

                if (!charUsingSkill)
                {
                    enemy.TakePhysicalDamage(target, characterAttackIndex.physicalDamage, characterAttackIndex.hitChance);
                }
                else
                {
                    switch (skillTierChoice)
                    {
                        case 0:
                            enemy.TakeSkillDamage(damageTotal, target);
                            break;
                        case 10:
                            enemy.StealAttempt(target, characterAttackIndex.stealChance);
                            break;
                        case 11:
                            float hitChanceReduction = 10 + Mathf.Round(characterAttackIndex.skillScale * 10 / 25);
                            enemy.hitChance -= hitChanceReduction;
                            break;
                        case 13:
                            enemy.TakeSkillDamage(damageTotal, target);
                            enemy.InflictPoisonAttack(charAttackingIndex, 10);
                            break;
                        case 14:
                            enemy.InflictDeathAttack();
                            break;
                    }
                }

                if (!dontDisplayDmgPopup)
                {
                    GameObject dmgPopup = Instantiate(damagePopup, enemies[target].transform.position, Quaternion.identity);

                    if (dodgedAttack == true)
                    {
                        dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Dodged";
                        dodgedAttack = false;

                    }
                    else
                    {
                        enemies[target].GetComponent<SpriteRenderer>().material = damageFlash;
                        dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = enemy.damageTotal.ToString();

                        yield return new WaitForSeconds(0.2f);
                        enemies[target].GetComponent<SpriteRenderer>().material = originalMaterial;
                    }

                    Destroy(dmgPopup, 0.8f);
                }
                else
                {
                    dontDisplayDmgPopup = false;
                }
            }
            hud.displayEnemyHealth[target].text = enemies[target].gameObject.GetComponent<Enemy>().health.ToString();
        }


        if (!charAttacking && !charAtBattlePos)
        {
            enemyGroup.moveToPosition = true;

            if (charAttackingIndex == 0)
            {
                if (characterAttacking.transform.position == leaderPos)
                {
                    charAtBattlePos = true;
                }
            }
            if (charAttackingIndex == 1)
            {
                if (characterAttacking.transform.position == activeParty2Pos)
                {
                    charAtBattlePos = true;
                }
            }
            if (charAttackingIndex == 2)
            {
                if (characterAttacking.transform.position == activeParty3Pos)
                {
                    charAtBattlePos = true;
                }
            }

            if (charAtBattlePos && !charAttacking)
            {
                charMoving = false;
                physicalAttack = false;
                skillPhysicalAttack = false;
                skillRangedAttack = false;
                dropAttack = false;
                charUsingSkill = false;
                targetCheck = false;

                battleQueue.Dequeue();
                currentInQueue = BattleState.QUEUECHECK;
                //state = BattleState.ATBCHECK;
                //currentInQueue = nextInQueue;

                if (enemies[target].gameObject.GetComponent<Enemy>().health <= 0)
                {
                    enemies[target].gameObject.GetComponent<Enemy>().health = 0;

                    isDead = EnemyGroup.enemyGroup.CheckEndBattle();

                    if (isDead)
                    {
                        state = BattleState.LEVELUPCHECK;
                        yield return new WaitForSeconds(2f);
                        StartCoroutine(LevelUpCheck());
                    }
                }
                if (Engine.e.battleModeActive && activeParty.activeParty[charAttackingIndex].GetComponent<Character>().isConfused)
                {
                    //    ActiveCheckNext();
                    state = BattleState.ATBCHECK;

                }


                yield return new WaitForSeconds(0.3f);

                if (!Engine.e.battleModeActive)
                {
                    state = BattleState.ATBCHECK;
                }
                else
                {


                }
                // StartCoroutine(CheckNext());
            }
        }
    }

    IEnumerator CharDropAttack()
    {

        if (!dropExists)
        {
            charAttackDrop = false;
            // currentInQueue = nextInQueue;
        }
        else
        {
            if (state == currentInQueue)
                state = BattleState.ATBCHECK;

        }
        if (targetCheck)
        {
            targetCheck = false;

            if (currentInQueue == BattleState.CHAR1TURN || state == BattleState.CONFCHAR1)
            {
                char1ATB = 0;
                char1Ready = false;
            }
            if (currentInQueue == BattleState.CHAR2TURN || state == BattleState.CONFCHAR2)
            {
                char2ATB = 0;
                char2Ready = false;
            }
            if (currentInQueue == BattleState.CHAR3TURN || state == BattleState.CONFCHAR3)
            {
                char3ATB = 0;
                char3Ready = false;
            }
            // ActiveCheckNext();

            if (Engine.e.battleModeActive)
            {
                //  state = BattleState.ATBCHECK;
                //state = currentState;
            }
        }
        if (!dropExists && !charAttackDrop)
        {
            battleQueue.Dequeue();

            if (battleQueue.Count > 0)
            {
                currentInQueue = battleQueue.Peek();
            }
            else
            {
                currentInQueue = BattleState.ATBCHECK;
            }

            //currentInQueue = nextInQueue;
            //if (s)
            //state = BattleState.ATBCHECK;

            yield return new WaitForSeconds(0.3f);

            if (isDead)
            {
                state = BattleState.LEVELUPCHECK;
                yield return new WaitForSeconds(1f);
                StartCoroutine(LevelUpCheck());
            }
            else
            {

                if (!Engine.e.battleModeActive)
                {
                    state = BattleState.ATBCHECK;
                }

            }
            //StartCoroutine(CheckNext());
        }
    }
    private void EnableButtonInteraction()
    {
        if (char1Ready)
        {
            for (int i = 0; i < char1MenuButtons.Length; i++)
            {
                char1MenuButtons[i].GetComponent<Button>().interactable = true;
            }
        }
        if (char2Ready)
        {
            for (int i = 0; i < char2MenuButtons.Length; i++)
            {
                char2MenuButtons[i].GetComponent<Button>().interactable = true;
            }
        }
        if (char3Ready)
        {
            for (int i = 0; i < char3MenuButtons.Length; i++)
            {
                char3MenuButtons[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    private void DisableButtonInteraction()
    {
        if (char1Ready)
        {
            for (int i = 0; i < char1MenuButtons.Length; i++)
            {
                char1MenuButtons[i].GetComponent<Button>().interactable = false;
            }
        }
        if (char2Ready)
        {
            for (int i = 0; i < char2MenuButtons.Length; i++)
            {
                char2MenuButtons[i].GetComponent<Button>().interactable = false;
            }
        }
        if (char3Ready)
        {
            for (int i = 0; i < char3MenuButtons.Length; i++)
            {
                char3MenuButtons[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    public IEnumerator CharConfuseAttack()
    {
        int index = 0;
        GameObject character = null;
        //if (state == BattleState.CHAR1TURN)
        //{
        if (currentInQueue == BattleState.CONFCHAR1)
        {
            index = 0;
            character = Engine.e.activeParty.gameObject;
            target = char1AttackTarget;
        }
        if (currentInQueue == BattleState.CONFCHAR2)
        {
            index = 1;
            character = Engine.e.activePartyMember2;
            target = char2AttackTarget;

        }
        if (currentInQueue == BattleState.CONFCHAR3)
        {
            index = 2;
            character = Engine.e.activePartyMember3;
            target = char3AttackTarget;

        }

        yield return new WaitForSeconds(0.3f);

        if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().isPoisoned)
        {
            Engine.e.TakePoisonDamage(index, Engine.e.activeParty.activeParty[index].GetComponent<Character>().poisonDmg);
        }
        Debug.Log("entered CharConfuseAttack");

        int choiceAttack = Random.Range(0, 100);

        if (choiceAttack < 70)
        {
            charMoving = true;
            physicalAttack = true;
            charAttacking = true;
            targetCheck = true;

        }
        else
        {
            if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().currentMana >= 15)
            {
                charAttackDrop = true;

                Debug.Log("herewego");
                confuseAttack = false;
                checkingForRandomDrop = true;
                if (checkingForRandomDrop == true)
                {
                    checkingForRandomDrop = false;
                    CharRandomDropChoice();
                }

                if (lastDropChoice != null)
                {
                    if (teamTargetChoice == 0)
                    {
                        if (lastDropChoice.dps)
                        {
                            physicalAttack = false;
                            if (lastDropChoice.dropType == "Fire")
                            {
                                Instantiate(enemyFireDropAnim, character.transform.position, Quaternion.identity);
                            }
                            if (lastDropChoice.dropType == "Water")
                            {
                                Instantiate(enemyWaterDropAnim, character.transform.position, Quaternion.identity);
                            }
                            if (lastDropChoice.dropType == "Lightning")
                            {
                                Instantiate(enemyLightningDropAnim, character.transform.position, Quaternion.identity);
                            }
                            if (lastDropChoice.dropType == "Shadow")
                            {
                                if (lastDropChoice.dropName == "Bio" || lastDropChoice.dropName == "Knockout" || lastDropChoice.dropName == "Blind")
                                {
                                    if (lastDropChoice.dropName == "Bio")
                                    {
                                        Instantiate(enemyPoisonAnim, character.transform.position, Quaternion.identity);
                                    }
                                    if (lastDropChoice.dropName == "Knockout")
                                    {
                                        Instantiate(enemySleepAnim, character.transform.position, Quaternion.identity);
                                    }
                                    if (lastDropChoice.dropName == "Blind")
                                    {
                                        Instantiate(enemyConfuseAnim, character.transform.position, Quaternion.identity);
                                    }
                                }
                                else
                                    Instantiate(enemyShadowDropAnim, character.transform.position, Quaternion.identity);
                            }
                            if (lastDropChoice.dropType == "Ice")
                            {
                                Instantiate(enemyIceDropAnim, character.transform.position, Quaternion.identity);
                            }
                            if (lastDropChoice.dropType == "Holy")
                            {
                                Instantiate(enemyHolyDropAnim, character.transform.position, Quaternion.identity);
                            }

                            isDead = Engine.e.TakeElementalDamage(target, lastDropChoice.dropPower, lastDropChoice.dropType);
                            // ActiveCheckNext();
                            if (currentInQueue == BattleState.CONFCHAR1)
                            {
                                char1ATB = 0;
                            }
                            if (currentInQueue == BattleState.CONFCHAR2)
                            {
                                char2ATB = 0;
                            }
                            if (currentInQueue == BattleState.CONFCHAR3)
                            {
                                char3ATB = 0;
                            }

                            if (currentInQueue == BattleState.ENEMY1TURN)
                            {
                                enemy1ATB = 0;
                            }
                            if (currentInQueue == BattleState.ENEMY2TURN)
                            {
                                enemy2ATB = 0;
                            }
                            if (currentInQueue == BattleState.ENEMY3TURN)
                            {
                                enemy3ATB = 0;
                            }
                            if (currentInQueue == BattleState.ENEMY4TURN)
                            {
                                enemy4ATB = 0;
                            }
                            Debug.Log("State should be changing here.");
                            confuseAttack = false;
                            state = currentState;
                        }
                        else
                        {
                            switch (lastDropChoice.dropName)
                            {
                                case "Holy Light":
                                    Instantiate(holyDropAnim, character.transform.position, Quaternion.identity);
                                    activeParty.activeParty[index].GetComponent<Character>().HolyLight(lastDropChoice.dropPower, randTarget);
                                    break;

                            }

                            if (currentInQueue == BattleState.CONFCHAR1)
                            {
                                char1ATB = 0;
                            }
                            if (currentInQueue == BattleState.CONFCHAR2)
                            {
                                char2ATB = 0;
                            }
                            if (currentInQueue == BattleState.CONFCHAR3)
                            {
                                char3ATB = 0;
                            }

                            if (currentInQueue == BattleState.ENEMY1TURN)
                            {
                                enemy1ATB = 0;
                            }
                            if (currentInQueue == BattleState.ENEMY2TURN)
                            {
                                enemy2ATB = 0;
                            }
                            if (currentInQueue == BattleState.ENEMY3TURN)
                            {
                                enemy3ATB = 0;
                            }
                            if (currentInQueue == BattleState.ENEMY4TURN)
                            {
                                enemy4ATB = 0;
                            }
                            confuseAttack = false;

                            //state = BattleState.ATBCHECK;

                        }
                    }
                    else
                    {

                        enemies[target].gameObject.GetComponent<Enemy>().TakeDropDamage(target, lastDropChoice);

                        if (enemies[target].gameObject.GetComponent<Enemy>().health <= 0)
                        {
                            enemies[target].gameObject.GetComponent<Enemy>().health = 0;

                            isDead = EnemyGroup.enemyGroup.CheckEndBattle();

                            if (currentInQueue == BattleState.CONFCHAR1)
                            {
                                char1ATB = 0;
                            }
                            if (currentInQueue == BattleState.CONFCHAR2)
                            {
                                char2ATB = 0;
                            }
                            if (currentInQueue == BattleState.CONFCHAR3)
                            {
                                char3ATB = 0;
                            }

                            if (currentInQueue == BattleState.ENEMY1TURN)
                            {
                                enemy1ATB = 0;
                            }
                            if (currentInQueue == BattleState.ENEMY2TURN)
                            {
                                enemy2ATB = 0;
                            }
                            if (currentInQueue == BattleState.ENEMY3TURN)
                            {
                                enemy3ATB = 0;
                            }
                            if (currentInQueue == BattleState.ENEMY4TURN)
                            {
                                enemy4ATB = 0;
                            }
                            confuseAttack = false;

                            //state = BattleState.ATBCHECK;
                            yield return new WaitForSeconds(0.1f);

                        }
                    }
                }

                if (activeParty.activeParty[index].GetComponent<Character>().isConfused)
                {
                    activeParty.activeParty[index].GetComponent<Character>().confuseTimer++;
                    if (activeParty.activeParty[index].GetComponent<Character>().confuseTimer == 3)
                    {
                        activeParty.activeParty[index].GetComponent<Character>().isConfused = false;
                        activeParty.activeParty[index].GetComponent<Character>().confuseTimer = 0;
                    }
                }
                if (activeParty.activeParty[index].GetComponent<Character>().isPoisoned)
                {
                    GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, character.transform.position, Quaternion.identity);

                    isDead = Engine.e.TakePoisonDamage(currentIndex, activeParty.activeParty[index].GetComponent<Character>().poisonDmg);

                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = activeParty.activeParty[index].GetComponent<Character>().poisonDmg.ToString();
                    Destroy(dmgPopup, 1f);
                }

                if (activeParty.activeParty[index].GetComponent<Character>().deathInflicted)
                {
                    isDead = activeParty.activeParty[index].GetComponent<Character>().TakeDeathDamage(currentIndex);
                }

                confuseAttack = false;
                Debug.Log("Cost: " + lastDropChoice.dropCost);
                activeParty.activeParty[index].GetComponent<Character>().currentMana -= Mathf.Round(activeParty.activeParty[index].GetComponent<Character>().GetDropChoice().dropCost
            - (activeParty.activeParty[index].GetComponent<Character>().GetDropChoice().dropCost * activeParty.activeParty[index].GetComponent<Character>().dropCostReduction / 100) + 0.45f);
                activeParty.activeParty[index].GetComponent<Character>().ResetDropChoice();

                hud.displayMana[index].text = activeParty.activeParty[index].gameObject.GetComponent<Character>().currentMana.ToString();
                if (currentInQueue == BattleState.CONFCHAR1)
                {
                    char1ATB = 0;
                }
                if (currentInQueue == BattleState.CONFCHAR2)
                {
                    char2ATB = 0;
                }
                if (currentInQueue == BattleState.CONFCHAR3)
                {
                    char3ATB = 0;
                }

                if (currentInQueue == BattleState.ENEMY1TURN)
                {
                    enemy1ATB = 0;
                }
                if (currentInQueue == BattleState.ENEMY2TURN)
                {
                    enemy2ATB = 0;
                }
                if (currentInQueue == BattleState.ENEMY3TURN)
                {
                    enemy3ATB = 0;
                }
                if (currentInQueue == BattleState.ENEMY4TURN)
                {
                    enemy4ATB = 0;
                }
                //state = currentState;
                /*
                                if (currentState == BattleState.ATBCHECK)
                                {
                                    dialogueText.text = string.Empty;
                                }

                                if (currentState == BattleState.CHAR1TURN)
                                {
                                    if (activeParty.activeParty[0].GetComponent<Character>().isConfused)
                                    {
                                        dialogueText.text = string.Empty;
                                        state = BattleState.ATBCHECK;
                                    }
                                    else
                                    {
                                        dialogueText.text = string.Empty;
                                        dialogueText.text = activeParty.activeParty[0].GetComponent<Character>().characterName;
                                    }
                                }
                                if (currentState == BattleState.CHAR2TURN)
                                {
                                    if (activeParty.activeParty[1].GetComponent<Character>().isConfused)
                                    {
                                        dialogueText.text = string.Empty;
                                        state = BattleState.ATBCHECK;
                                    }
                                    else
                                    {
                                        dialogueText.text = string.Empty;
                                        dialogueText.text = activeParty.activeParty[1].GetComponent<Character>().characterName;
                                    }
                                }
                                if (currentState == BattleState.CHAR3TURN)
                                {
                                    if (activeParty.activeParty[2].GetComponent<Character>().isConfused)
                                    {
                                        dialogueText.text = string.Empty;
                                        state = BattleState.ATBCHECK;
                                    }
                                    else
                                    {
                                        dialogueText.text = string.Empty;
                                        dialogueText.text = activeParty.activeParty[2].GetComponent<Character>().characterName;
                                    }
                                */
            }
            else
            {

                charMoving = true;
                physicalAttack = true;
                charAttacking = true;
                targetCheck = true;

            }
        }
    }



    public IEnumerator CharSupport(int allyTarget)
    {

        int index = 0;
        GameObject character = null;
        inBattleMenu = false;

        if (currentInQueue == BattleState.CHAR1TURN)
        {
            index = 0;
            character = Engine.e.activeParty.gameObject;

        }
        if (currentInQueue == BattleState.CHAR2TURN)
        {
            index = 1;
            character = Engine.e.activePartyMember2;

        }
        if (currentInQueue == BattleState.CHAR3TURN)
        {
            index = 2;
            character = Engine.e.activePartyMember3;

        }

        DeactivateSupportButtons();
        DeactivateDropsUI();
        DeactivateSkillsUI();

        if (usingItem)
        {
            Engine.e.UseItem(allyTarget);

            if (failedItemUse)
            {
                if (index == 0)
                {
                    usingItem = false;

                    Char1Turn();
                }
                if (index == 1)
                {
                    usingItem = false;
                    Char2Turn();
                }
                if (index == 2)
                {
                    usingItem = false;
                    Char3Turn();
                }
                if (char1ATB >= 100)
                {
                    Engine.e.battleSystem.ActivateChar1MenuButtons();
                }

                if (Engine.e.activeParty.activeParty[1] != null)
                {
                    if (char2ATB >= 100)
                    {
                        Engine.e.battleSystem.ActivateChar2MenuButtons();
                    }
                }

                if (Engine.e.activeParty.activeParty[2] != null)
                {
                    if (char3ATB >= 100)
                    {
                        Engine.e.battleSystem.ActivateChar3MenuButtons();
                    }
                }
            }

            hud.displayHealth[allyTarget].text = activeParty.activeParty[allyTarget].gameObject.GetComponent<Character>().currentHealth.ToString();
            hud.displayMana[allyTarget].text = activeParty.activeParty[allyTarget].gameObject.GetComponent<Character>().currentMana.ToString();
            hud.displayEnergy[allyTarget].text = activeParty.activeParty[allyTarget].gameObject.GetComponent<Character>().currentEnergy.ToString();


            battleItemMenu.SetActive(false);

            yield return new WaitForSeconds(1f);
            if (!Engine.e.battleModeActive)
            {
                state = BattleState.ATBCHECK;
            }
        }

        if (dropAttack)
        {
            dropAttack = false;
            if (activeParty.activeParty[index].GetComponent<Character>().currentMana >= activeParty.activeParty[index].GetComponent<Character>().lastDropChoice.dropCost)
            {
                {
                    charAttackDrop = true;

                    switch (activeParty.activeParty[index].GetComponent<Character>().lastDropChoice.dropName)
                    {
                        case "Holy Light":
                            if (activeParty.activeParty[allyTarget].GetComponent<Character>().currentHealth > 0)
                            {
                                Instantiate(holyDropAnim, Engine.e.activeParty.transform.position, Quaternion.identity);
                                activeParty.activeParty[index].GetComponent<Character>().HolyLight(activeParty.activeParty[index].GetComponent<Character>().lastDropChoice.dropPower, allyTarget);
                                activeParty.activeParty[index].GetComponent<Character>().currentMana -= activeParty.activeParty[index].GetComponent<Character>().lastDropChoice.dropCost;

                            }
                            else
                            {
                                //  yield return this;
                            }
                            break;

                        case "Raise":
                            if (activeParty.activeParty[allyTarget].GetComponent<Character>().currentHealth <= 0)
                            {
                                Instantiate(holyDropAnim, Engine.e.activeParty.transform.position, Quaternion.identity);
                                activeParty.activeParty[index].GetComponent<Character>().Revive(activeParty.activeParty[index].GetComponent<Character>().lastDropChoice.dropPower, allyTarget);
                                activeParty.activeParty[index].GetComponent<Character>().currentMana -= activeParty.activeParty[index].GetComponent<Character>().lastDropChoice.dropCost;

                            }
                            else
                            {

                                charAttackDrop = false;

                                if (index == 0)
                                {
                                    Char1Turn();
                                }
                                if (index == 1)
                                {
                                    Char2Turn();
                                }
                                if (index == 2)
                                {
                                    Char3Turn();
                                }
                            }

                            break;

                        case "Repent":

                            Instantiate(holyDropAnim, Engine.e.activeParty.transform.position, Quaternion.identity);
                            activeParty.activeParty[index].GetComponent<Character>().Repent(allyTarget);
                            activeParty.activeParty[index].GetComponent<Character>().currentMana -= activeParty.activeParty[index].GetComponent<Character>().lastDropChoice.dropCost;
                            break;

                        case "Soothing Rain":

                            Instantiate(holyDropAnim, Engine.e.activeParty.transform.position, Quaternion.identity);
                            activeParty.activeParty[index].GetComponent<Character>().SoothingRain(activeParty.activeParty[index].GetComponent<Character>().lastDropChoice.dropPower);
                            activeParty.activeParty[index].GetComponent<Character>().currentMana -= activeParty.activeParty[index].GetComponent<Character>().lastDropChoice.dropCost;


                            break;

                    }

                    if (lastDropChoice.dropType == "Holy")
                    {

                        if (activeParty.activeParty[index].GetComponent<Character>().holyDropsLevel < 99)
                        {
                            activeParty.activeParty[index].GetComponent<Character>().holyDropsExperience += lastDropChoice.experiencePoints;
                            // Level Up
                            if (activeParty.activeParty[index].GetComponent<Character>().holyDropsExperience >= activeParty.activeParty[index].GetComponent<Character>().holyDropsLvlReq)
                            {
                                activeParty.activeParty[index].GetComponent<Character>().holyDropsLevel += 1f;
                                activeParty.activeParty[index].GetComponent<Character>().holyDropsExperience -= activeParty.activeParty[index].GetComponent<Character>().holyDropsLvlReq;
                                activeParty.activeParty[index].GetComponent<Character>().holyDropsLvlReq = (activeParty.activeParty[index].GetComponent<Character>().holyDropsLvlReq * 3 / 2);
                                GameObject levelUp = Instantiate(levelUpPopup, character.transform.position, Quaternion.identity);
                                levelUp.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Holy Lvl Up (Lvl: " + activeParty.activeParty[index].GetComponent<Character>().holyDropsLevel + ")";
                                Destroy(levelUp, 2f);
                            }
                        }
                    }

                }
            }
            else
            {
                Debug.Log("Exception Thrown");
            }

            hud.displayHealth[allyTarget].text = activeParty.activeParty[allyTarget].gameObject.GetComponent<Character>().currentHealth.ToString();
            hud.displayMana[allyTarget].text = activeParty.activeParty[allyTarget].gameObject.GetComponent<Character>().currentMana.ToString();
            hud.displayEnergy[allyTarget].text = activeParty.activeParty[allyTarget].gameObject.GetComponent<Character>().currentEnergy.ToString();

            if (currentInQueue == BattleState.CHAR1TURN)
            {
                char1ATB = 0;
                char1ATBGuage.value = char1ATB;
                char1Ready = false;
            }
            if (currentInQueue == BattleState.CHAR2TURN)
            {
                char2ATB = 0;
                char2ATBGuage.value = char1ATB;
                char2Ready = false;
            }
            if (currentInQueue == BattleState.CHAR3TURN)
            {
                char3ATB = 0;
                char3ATBGuage.value = char1ATB;
                char3Ready = false;


                //state = BattleState.ATBCHECK;
            }
            state = BattleState.ATBCHECK;

        }

        if (skillTargetSupport == true)
        {
            if (currentInQueue == BattleState.CHAR1TURN)
            {
                char1ATB = 0;
                char1ATBGuage.value = char1ATB;
                char1Ready = false;
            }
            if (currentInQueue == BattleState.CHAR2TURN)
            {
                char2ATB = 0;
                char2ATBGuage.value = char1ATB;
                char2Ready = false;
            }
            if (currentInQueue == BattleState.CHAR3TURN)
            {
                char3ATB = 0;
                char3ATBGuage.value = char1ATB;
                char3Ready = false;


                //state = BattleState.ATBCHECK;
            }
            state = BattleState.ATBCHECK;

        }

        if (skillSelfSupport == true)
        {
            charAttackDrop = true;
            Engine.e.charBeingTargeted = 0;
            Instantiate(holyDropAnim, character.transform.position, Quaternion.identity);

            if (currentInQueue == BattleState.CHAR1TURN)
            {
                char1ATB = 0;
                char1ATBGuage.value = char1ATB;
                char1Ready = false;
            }
            if (currentInQueue == BattleState.CHAR2TURN)
            {
                char2ATB = 0;
                char2ATBGuage.value = char1ATB;
                char2Ready = false;
            }
            if (currentInQueue == BattleState.CHAR3TURN)
            {
                char3ATB = 0;
                char3ATBGuage.value = char1ATB;
                char3Ready = false;


                //state = BattleState.ATBCHECK;
            }
            state = BattleState.ATBCHECK;
        }

        if (activeParty.activeParty[index].GetComponent<Character>().isPoisoned)
        {
            GameObject dmgPopup = Instantiate(damagePopup, character.transform.position, Quaternion.identity);
            isDead = Engine.e.TakePoisonDamage(index, activeParty.activeParty[index].GetComponent<Character>().poisonDmg);
            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = activeParty.activeParty[index].GetComponent<Character>().poisonDmg.ToString();
            Destroy(dmgPopup, 1f);
        }

        hud.displayHealth[index].text = activeParty.activeParty[index].GetComponent<Character>().currentHealth.ToString();
        hud.displayMana[index].text = activeParty.activeParty[index].GetComponent<Character>().currentMana.ToString();
        hud.displayEnergy[index].text = activeParty.activeParty[index].GetComponent<Character>().currentEnergy.ToString();


        dialogueText.text = string.Empty;


        yield return new WaitForSeconds(0.1f);

    }
    public IEnumerator CharSwitch(int index)
    {

        DeactivateSupportButtons();
        DeactivateDropsUI();
        DeactivateSkillsUI();
        DeactivateChar1SwitchButtons();

        if (state == BattleState.CHAR1TURN)
        {
            DeactivateChar1MenuButtons();
            DeactivateChar1SwitchButtons();
            char1ATB = 0;
            Engine.e.activeParty.InstantiateBattleLeader(index);

            hud.displayNames[0].text = activeParty.activeParty[0].gameObject.GetComponent<Character>().characterName;
            hud.displayHealth[0].text = activeParty.activeParty[0].gameObject.GetComponent<Character>().currentHealth.ToString();
            hud.displayMana[0].text = activeParty.activeParty[0].gameObject.GetComponent<Character>().currentMana.ToString();

        }
        if (state == BattleState.CHAR2TURN)
        {
            DeactivateChar2MenuButtons();
            DeactivateChar2SwitchButtons();
            char2ATB = 0;
            Engine.e.activeParty.InstantiateBattleActiveParty2(index);

            hud.displayNames[1].text = activeParty.activeParty[1].gameObject.GetComponent<Character>().characterName;
            hud.displayHealth[1].text = activeParty.activeParty[1].gameObject.GetComponent<Character>().currentHealth.ToString();
            hud.displayMana[1].text = activeParty.activeParty[1].gameObject.GetComponent<Character>().currentMana.ToString();

        }
        if (state == BattleState.CHAR3TURN)
        {
            DeactivateChar3MenuButtons();
            DeactivateChar3SwitchButtons();
            char3ATB = 0;
            Engine.e.activeParty.InstantiateBattleActiveParty3(index);

            hud.displayNames[2].text = activeParty.activeParty[2].gameObject.GetComponent<Character>().characterName;
            hud.displayHealth[2].text = activeParty.activeParty[2].gameObject.GetComponent<Character>().currentHealth.ToString();
            hud.displayMana[2].text = activeParty.activeParty[2].gameObject.GetComponent<Character>().currentMana.ToString();
        }


        dialogueText.text = string.Empty;

        yield return new WaitForSeconds(1f);

        StartCoroutine(CheckNext());
    }

    void CharSkills(Skills skill)
    {
        int index = 0;
        Character character = Engine.e.activeParty.activeParty[index].GetComponent<Character>();
        GameObject characterGameObject = null;
        DeactivateSkillTargetButtons();

        if (currentInQueue == BattleState.CHAR1TURN)
        {
            index = 0;
            characterGameObject = Engine.e.activeParty.gameObject;
            target = char1AttackTarget;
        }
        if (currentInQueue == BattleState.CHAR2TURN)
        {
            index = 1;
            characterGameObject = Engine.e.activePartyMember2;
            target = char2AttackTarget;

        }
        if (currentInQueue == BattleState.CHAR3TURN)
        {
            index = 2;
            characterGameObject = Engine.e.activePartyMember3;
            target = char3AttackTarget;

        }

        characterDropTarget = characterGameObject;

        float skillModifier;
        skillBoostTotal = 0f;


        skillModifier = ((character.skillScale) / 2);

        switch (skillTierChoice)
        {
            case 0:
                skillPhysicalAttack = true;
                damageTotal = character.physicalDamage * 2;
                break;

            case 1:

                break;

            case 5:
                skillPhysicalAttack = true;
                skillRangedAttack = true;
                mpRestore = true;
                character.currentMana += (skill.skillPointReturn + (skill.skillPointReturn * skillModifier));
                skillBoostTotal = (skill.skillPointReturn + (skill.skillPointReturn * skillModifier));
                Instantiate(siphonAnim, enemies[target].transform.position, Quaternion.identity);

                if (character.currentMana > character.maxMana)
                {
                    character.currentMana = character.maxMana;
                }
                damageTotal = skillBoostTotal;
                break;

            case 10:
                skillPhysicalAttack = true;
                dontDisplayDmgPopup = true;
                damageTotal = 0;
                break;
            case 11:
                skillPhysicalAttack = true;
                dontDisplayDmgPopup = true;
                damageTotal = 0;
                break;
            case 12:
                switch (character.characterName)
                {
                    case "Grieve":
                        if (grieveDodgeBoost == 0)
                        {
                            grieveStatBoost = true;
                            grieveDodgeBoost = 10f;
                            character.dodgeChance += grieveDodgeBoost;
                        }
                        else
                        {
                            Debug.Log("Doesn't Stack bitch");
                        }
                        break;

                    case "Mac":
                        if (macDodgeBoost == 0)
                        {
                            macStatBoost = true;
                            macDodgeBoost = 10f;
                            character.dodgeChance += macDodgeBoost;
                        }
                        else
                        {
                            Debug.Log("Doesn't Stack bitch");
                        }
                        break;

                    case "Field":
                        if (fieldDodgeBoost == 0)
                        {
                            fieldStatBoost = true;
                            fieldDodgeBoost = 10f;
                            character.dodgeChance += fieldDodgeBoost;
                        }
                        else
                        {
                            Debug.Log("Doesn't Stack bitch");
                        }
                        break;

                    case "Riggs":
                        if (riggsDodgeBoost == 0)
                        {
                            riggsStatBoost = true;
                            riggsDodgeBoost = 10f;
                            character.dodgeChance += riggsDodgeBoost;
                        }
                        else
                        {
                            Debug.Log("Doesn't Stack bitch");
                        }
                        break;
                }

                skillSelfSupport = true;
                damageTotal = 0;
                break;
            case 13:
                skillPhysicalAttack = true;
                damageTotal = Mathf.Round(skill.skillPower + (skill.skillPower * character.shadowDropsLevel / 20) * skillModifier);
                break;
            case 14:
                skillPhysicalAttack = true;
                damageTotal = 99999;
                break;
            case 15:
                switch (character.characterName)
                {
                    case "Grieve":
                        if (grievePhysicalBoost == 0)
                        {
                            grieveStatBoost = true;
                            grievePhysicalBoost = Mathf.Round(character.physicalDamage + (character.skillScale * 10 / 25));
                            character.physicalDamage += grievePhysicalBoost;
                        }
                        else
                        {
                            Debug.Log("Doesn't Stack bitch");
                        }
                        break;

                    case "Mac":
                        if (macPhysicalBoost == 0)
                        {
                            macStatBoost = true;
                            macPhysicalBoost = Mathf.Round(character.physicalDamage + (character.skillScale * 10 / 25));
                            character.physicalDamage += macPhysicalBoost;
                        }
                        else
                        {
                            Debug.Log("Doesn't Stack bitch");
                        }
                        break;

                    case "Field":
                        if (fieldPhysicalBoost == 0)
                        {
                            fieldStatBoost = true;
                            fieldPhysicalBoost = Mathf.Round(character.physicalDamage + (character.skillScale * 10 / 25));
                            character.physicalDamage += fieldPhysicalBoost;
                        }
                        else
                        {
                            Debug.Log("Doesn't Stack bitch");
                        }
                        break;

                    case "Riggs":
                        if (riggsPhysicalBoost == 0)
                        {
                            riggsStatBoost = true;
                            riggsPhysicalBoost = Mathf.Round(character.physicalDamage + (character.skillScale * 10 / 25));
                            character.physicalDamage += riggsPhysicalBoost;
                        }
                        else
                        {
                            Debug.Log("Doesn't Stack bitch");
                        }
                        break;
                }
                skillSelfSupport = true;
                damageTotal = 0;
                break;
            case 16:
                skillRangedAttack = true;
                damageTotal = Mathf.Round(skill.skillPower + (skill.skillPower * character.holyDropsLevel / 20) * skillModifier);
                Instantiate(lightningDropAnim, characterGameObject.transform.position, Quaternion.identity);
                break;

        }

        if (skill.skillName != "Steal")
        {
            activeParty.activeParty[index].GetComponent<Character>().currentEnergy -= Mathf.Round((skill.skillCost
            - skill.skillCost * activeParty.activeParty[index].GetComponent<Character>().skillCostReduction / 100) + 0.45f);
        }
        hud.displayEnergy[index].text = activeParty.activeParty[index].gameObject.GetComponent<Character>().currentEnergy.ToString();
        DeactivateSkillsUI();

        if (skill.selfSupport)
        {
            DeactivateAttackButtons();
        }


        if (skillPhysicalAttack || skillRangedAttack)
        {
            StartCoroutine(CharAttack(target));
        }
        if (skillSelfSupport)
        {
            StartCoroutine(CharSupport(index));
        }
        if (skillTargetSupport)
        {
            StartCoroutine(CharSupport(target));
        }

        Engine.e.char1BattlePanel.SetActive(true);

        if (Engine.e.activeParty.activeParty[1] != null)
        {
            Engine.e.char2BattlePanel.SetActive(true);
        }
        if (Engine.e.activeParty.activeParty[2] != null)
        {
            Engine.e.char3BattlePanel.SetActive(true);
        }
    }

    public IEnumerator ConfuseAttackMoveToTargetChar()
    {

        int index = 0;
        GameObject character = null;
        GameObject targetCharacter = null;
        if (currentInQueue == BattleState.CHAR1TURN || currentInQueue == BattleState.CONFCHAR1)
        {
            index = 0;
            character = Engine.e.activeParty.gameObject;
            target = char1AttackTarget;
        }
        if (currentInQueue == BattleState.CHAR2TURN || currentInQueue == BattleState.CONFCHAR2)
        {
            index = 1;
            character = Engine.e.activePartyMember2;
            target = char2AttackTarget;
        }
        if (currentInQueue == BattleState.CHAR3TURN || currentInQueue == BattleState.CONFCHAR3)
        {
            index = 2;
            character = Engine.e.activePartyMember3;
            target = char3AttackTarget;
        }

        if (randTarget == 0)
        {
            targetCharacter = Engine.e.activeParty.gameObject;
        }
        if (randTarget == 1)
        {
            targetCharacter = Engine.e.activePartyMember2.gameObject;
        }
        if (randTarget == 2)
        {
            targetCharacter = Engine.e.activePartyMember3.gameObject;
        }

        // ActiveCheckNext();

        if (charAttacking && charAtBattlePos)
        {

            enemyGroup.moveToPosition = false;
            Vector3 targetPos = Vector3.MoveTowards(character.GetComponent<Rigidbody2D>().transform.position, targetCharacter.GetComponent<Rigidbody2D>().transform.position, 6 * Time.deltaTime);
            character.GetComponent<Rigidbody2D>().MovePosition(targetPos);

            if (Vector3.Distance(character.transform.position, targetCharacter.GetComponent<Rigidbody2D>().transform.position) < 1)
            {

                charAttacking = false;
                charAtBattlePos = false;

                isDead = Engine.e.TakeDamage(target, damageTotal, Engine.e.activeParty.activeParty[index].GetComponent<Character>().hitChance);

                GameObject dmgPopup = Instantiate(damagePopup, character.transform.position, Quaternion.identity);

                if (dodgedAttack == true)
                {
                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Dodged";
                    dodgedAttack = false;

                }
                else
                {
                    targetCharacter.GetComponent<SpriteRenderer>().material = damageFlash;
                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = damageTotal.ToString();

                    yield return new WaitForSeconds(0.2f);
                    targetCharacter.GetComponent<SpriteRenderer>().material = originalMaterial;
                }

                Destroy(dmgPopup, 0.8f);
            }
        }

        hud.displayHealth[Engine.e.charBeingTargeted].text = Engine.e.activeParty.activeParty[Engine.e.charBeingTargeted].GetComponent<Character>().currentHealth.ToString();
        targetCheck = false;
        confuseAttack = false;

        if (!charAttacking && !charAtBattlePos)
        {
            enemyGroup.moveToPosition = true;

            if (index == 0)
            {
                if (character.transform.position == leaderPos)
                {
                    charAtBattlePos = true;
                }
            }
            if (index == 1)
            {
                if (character.transform.position == activeParty2Pos)
                {
                    charAtBattlePos = true;
                }
            }
            if (index == 2)
            {
                if (character.transform.position == activeParty3Pos)
                {
                    charAtBattlePos = true;
                }
            }

            if (charAtBattlePos && !charAttacking)
            {
                charMoving = false;
                attackingTeam = false;
                physicalAttack = false;
                targetCheck = false;
                confuseAttack = false;

                battleQueue.Dequeue();
                currentInQueue = BattleState.QUEUECHECK;

                Debug.Log("Adding confuse timer");
                activeParty.activeParty[index].GetComponent<Character>().confuseTimer++;
                if (activeParty.activeParty[index].GetComponent<Character>().confuseTimer == 3)
                {
                    activeParty.activeParty[index].GetComponent<Character>().isConfused = false;
                    activeParty.activeParty[index].GetComponent<Character>().confuseTimer = 0;
                }

                if (activeParty.activeParty[index].GetComponent<Character>().isPoisoned)
                {
                    GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, character.transform.position, Quaternion.identity);

                    isDead = Engine.e.TakePoisonDamage(currentIndex, activeParty.activeParty[index].GetComponent<Character>().poisonDmg);

                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = activeParty.activeParty[index].GetComponent<Character>().poisonDmg.ToString();
                    Destroy(dmgPopup, 1f);
                }

                if (activeParty.activeParty[index].GetComponent<Character>().deathInflicted)
                {
                    isDead = activeParty.activeParty[index].GetComponent<Character>().TakeDeathDamage(index);
                }
                if (Engine.e.activeParty.activeParty[Engine.e.charBeingTargeted].GetComponent<Character>().currentHealth <= 0)
                {
                    Engine.e.activeParty.activeParty[Engine.e.charBeingTargeted].GetComponent<Character>().currentHealth = 0;

                    if (isDead)
                    {
                        state = BattleState.LOST;
                        yield return new WaitForSeconds(2f);
                        StartCoroutine(EndBattle());
                    }
                }

                if (index == 0)
                {
                    char1ATB = 0;
                }
                if (index == 1)
                {
                    char2ATB = 0;
                }
                if (index == 2)
                {
                    char3ATB = 0;
                }

                if (Engine.e.battleModeActive)
                {
                    Debug.Log("The stored current state is " + currentState + ". The state at this moment is " + state);
                    state = currentState;
                    Debug.Log("The stored current state is " + currentState + ". The state at this moment is now and should be" + state);


                    if (currentState == BattleState.ATBCHECK)
                    {
                        dialogueText.text = string.Empty;
                    }
                    if (currentState == BattleState.CHAR1TURN || state == BattleState.CONFCHAR1)
                    {
                        dialogueText.text = string.Empty;
                        dialogueText.text = activeParty.activeParty[0].GetComponent<Character>().characterName;
                    }
                    if (currentState == BattleState.CHAR2TURN || state == BattleState.CONFCHAR2)
                    {
                        dialogueText.text = string.Empty;
                        dialogueText.text = activeParty.activeParty[1].GetComponent<Character>().characterName;
                    }
                    if (currentState == BattleState.CHAR3TURN || state == BattleState.CONFCHAR3)
                    {
                        dialogueText.text = string.Empty;
                        dialogueText.text = activeParty.activeParty[2].GetComponent<Character>().characterName;
                    }
                }
                yield return new WaitForSeconds(1f);

                // StartCoroutine(CheckNext());
                if (!Engine.e.battleModeActive)
                {
                    state = BattleState.ATBCHECK;
                }
            }
        }
    }

    void CharRandomDropChoice()
    {

        int index = 0;

        if (currentInQueue == BattleState.CHAR1TURN || currentInQueue == BattleState.CONFCHAR1)
        {
            index = 0;
        }
        if (currentInQueue == BattleState.CHAR2TURN || currentInQueue == BattleState.CONFCHAR2)
        {
            index = 1;
        }
        if (currentInQueue == BattleState.CHAR3TURN || currentInQueue == BattleState.CONFCHAR3)
        {
            index = 2;
        }

        checkingForRandomDrop = false;
        lastDropChoice = null;

        charRandomDropList = new List<Drops>();

        if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().canUseFireDrops)
        {
            for (int i = 0; i < Engine.e.activeParty.activeParty[index].GetComponent<Character>().fireDrops.Length; i++)
            {
                if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().fireDrops[i] != null)
                {
                    if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().currentMana >= Engine.e.activeParty.activeParty[index].GetComponent<Character>().fireDrops[i].dropCost)
                    {
                        charRandomDropList.Add(Engine.e.activeParty.activeParty[index].GetComponent<Character>().fireDrops[i]);
                    }
                }
            }
        }

        if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().canUseIceDrops)
        {
            for (int i = 0; i < Engine.e.activeParty.activeParty[index].GetComponent<Character>().iceDrops.Length; i++)
            {
                if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().iceDrops[i] != null)
                {
                    if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().currentMana >= Engine.e.activeParty.activeParty[index].GetComponent<Character>().iceDrops[i].dropCost)
                    {
                        charRandomDropList.Add(Engine.e.activeParty.activeParty[index].GetComponent<Character>().iceDrops[i]);
                    }
                }
            }
        }

        if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().canUseLightningDrops)
        {
            for (int i = 0; i < Engine.e.activeParty.activeParty[index].GetComponent<Character>().lightningDrops.Length; i++)
            {
                if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().lightningDrops[i] != null)
                {
                    if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().currentMana >= Engine.e.activeParty.activeParty[index].GetComponent<Character>().lightningDrops[i].dropCost)
                    {
                        charRandomDropList.Add(Engine.e.activeParty.activeParty[index].GetComponent<Character>().lightningDrops[i]);
                    }
                }
            }
        }

        if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().canUseWaterDrops)
        {
            for (int i = 0; i < Engine.e.activeParty.activeParty[index].GetComponent<Character>().waterDrops.Length; i++)
            {
                if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().waterDrops[i] != null)
                {
                    if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().currentMana >= Engine.e.activeParty.activeParty[index].GetComponent<Character>().waterDrops[i].dropCost)
                    {
                        charRandomDropList.Add(Engine.e.activeParty.activeParty[index].GetComponent<Character>().waterDrops[i]);
                    }
                }
            }
        }

        if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().canUseShadowDrops)
        {
            for (int i = 0; i < Engine.e.activeParty.activeParty[index].GetComponent<Character>().shadowDrops.Length; i++)
            {
                if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().shadowDrops[i] != null && Engine.e.activeParty.activeParty[index].GetComponent<Character>().shadowDrops[i].dropName != "Death")
                {
                    if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().currentMana >= Engine.e.activeParty.activeParty[0].GetComponent<Character>().shadowDrops[i].dropCost)
                    {
                        charRandomDropList.Add(Engine.e.activeParty.activeParty[index].GetComponent<Character>().shadowDrops[i]);
                    }
                }
            }
        }

        if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().canUseHolyDrops)
        {
            for (int i = 0; i < Engine.e.activeParty.activeParty[index].GetComponent<Character>().holyDrops.Length; i++)
            {
                if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().holyDrops[i] != null && Engine.e.activeParty.activeParty[index].GetComponent<Character>().holyDrops[i].dropName != "Raise"
                && Engine.e.activeParty.activeParty[index].GetComponent<Character>().holyDrops[i].dropName != "Repent" && Engine.e.activeParty.activeParty[index].GetComponent<Character>().holyDrops[i].dropName != "Dispel")
                {
                    if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().currentMana >= Engine.e.activeParty.activeParty[index].GetComponent<Character>().holyDrops[i].dropCost)
                    {
                        charRandomDropList.Add(Engine.e.activeParty.activeParty[index].GetComponent<Character>().holyDrops[i]);
                    }
                }
            }
        }

        randomDropIndex = Random.Range(0, charRandomDropList.Count);

        lastDropChoice = charRandomDropList[randomDropIndex];
    }

    IEnumerator EnemyAttack()
    {
        Debug.Log("Should only see this once");
        int index = 0;

        if (currentInQueue == BattleState.ENEMY1TURN)
        {
            index = 0;
        }
        if (currentInQueue == BattleState.ENEMY2TURN)
        {
            index = 1;
        }
        if (currentInQueue == BattleState.ENEMY3TURN)
        {
            index = 2;
        }
        if (currentInQueue == BattleState.ENEMY4TURN)
        {
            index = 3;
        }

        randTarget = Engine.e.GetRandomRemainingPartyMember();
        previousTargetReferenceEnemy = randTarget;
        Debug.Log(randTarget);

        yield return new WaitForSeconds(1f);


        if (enemies[index].GetComponent<Enemy>().drops[0] != null)
        {
            int choiceAttack = Random.Range(0, 100);

            if (enemies[index].GetComponent<Enemy>().choiceAttack < choiceAttack)
            {
                enemyMoving = true;
                enemyAttacking = true;
                Engine.e.PhysicalDamageCalculation(randTarget, enemies[index].gameObject.GetComponent<Enemy>().damage);

            }
            else
            {
                enemyDropChoice = Random.Range(0, enemies[index].GetComponent<Enemy>().drops.Length);

                if (enemies[index].GetComponent<Enemy>().mana >= enemies[index].GetComponent<Enemy>().drops[enemyDropChoice].dropCost)
                {
                    enemyAttackDrop = true;

                    lastDropChoice = enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice];

                    if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropType == "Fire")
                    {
                        Instantiate(enemyFireDropAnim, enemies[index].transform.position, Quaternion.identity);
                    }
                    if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropType == "Water")
                    {
                        Instantiate(enemyWaterDropAnim, enemies[index].transform.position, Quaternion.identity);
                    }
                    if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropType == "Lightning")
                    {
                        Instantiate(enemyLightningDropAnim, enemies[index].transform.position, Quaternion.identity);
                    }
                    if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropType == "Shadow")
                    {
                        if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropName == "Bio" || enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropName == "Knockout"
                        || enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropName == "Blind")
                        {
                            if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropName == "Bio")
                            {
                                Instantiate(enemyPoisonAnim, enemies[index].transform.position, Quaternion.identity);
                            }
                            if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropName == "Knockout")
                            {
                                Instantiate(enemySleepAnim, enemies[index].transform.position, Quaternion.identity);
                            }
                            if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropName == "Blind")
                            {
                                Instantiate(enemyConfuseAnim, enemies[index].transform.position, Quaternion.identity);
                            }
                        }
                        else
                            Instantiate(enemyShadowDropAnim, enemies[index].transform.position, Quaternion.identity);
                    }
                    if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropType == "Ice")
                    {
                        Instantiate(enemyIceDropAnim, enemies[index].transform.position, Quaternion.identity);
                    }

                    if (currentInQueue == BattleState.CONFCHAR1)
                    {
                        char1ATB = 0;
                    }
                    if (currentInQueue == BattleState.CONFCHAR2)
                    {
                        char2ATB = 0;
                    }
                    if (currentInQueue == BattleState.CONFCHAR3)
                    {
                        char3ATB = 0;
                    }

                    if (currentInQueue == BattleState.ENEMY1TURN)
                    {
                        enemy1ATB = 0;
                    }
                    if (currentInQueue == BattleState.ENEMY2TURN)
                    {
                        enemy2ATB = 0;
                    }
                    if (currentInQueue == BattleState.ENEMY3TURN)
                    {
                        enemy3ATB = 0;
                    }
                    if (currentInQueue == BattleState.ENEMY4TURN)
                    {
                        enemy4ATB = 0;
                    }

                    isDead = Engine.e.TakeElementalDamage(randTarget, enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropPower, enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropType);
                    enemies[index].GetComponent<Enemy>().mana -= enemies[index].GetComponent<Enemy>().drops[enemyDropChoice].dropCost;

                    if (enemies[index].GetComponent<EnemyMovement>() != null)
                    {
                        enemies[index].GetComponent<EnemyMovement>().enabled = true;
                    }

                    if (enemies[index].GetComponent<Enemy>().isConfused)
                    {
                        enemies[index].GetComponent<Enemy>().confuseTimer++;
                        if (enemies[index].GetComponent<Enemy>().confuseTimer == 3)
                        {
                            enemies[index].GetComponent<Enemy>().isConfused = false;
                            enemies[index].GetComponent<Enemy>().confuseTimer = 0;
                        }
                    }

                    if (enemies[index].GetComponent<Enemy>().isPoisoned)
                    {
                        enemies[index].GetComponent<Enemy>().TakePoisonDamage(index, enemies[index].GetComponent<Enemy>().poisonDmg);


                    }

                    if (enemies[index].GetComponent<Enemy>().deathInflicted)
                    {
                        enemies[index].GetComponent<Enemy>().TakeDeathDamage();
                    }

                    enemyAttacking = false;

                }
                else
                {
                    enemyMoving = true;
                    enemyAttacking = true;
                    Engine.e.PhysicalDamageCalculation(randTarget, enemies[index].gameObject.GetComponent<Enemy>().damage);
                }
            }
        }
        else
        {
            enemyMoving = true;
            enemyAttacking = true;
            Engine.e.PhysicalDamageCalculation(randTarget, enemies[index].gameObject.GetComponent<Enemy>().damage);
        }
        partyCheckNext = false;
    }

    IEnumerator AttackMoveToTargetEnemy()
    {
        int index = 0;
        int enemyTarget = 0;
        GameObject character = null;

        if (currentInQueue == BattleState.ENEMY1TURN)
        {
            index = 0;
            enemyTarget = enemy1AttackTarget;
        }
        if (currentInQueue == BattleState.ENEMY2TURN)
        {
            index = 1;
            enemyTarget = enemy2AttackTarget;

        }
        if (currentInQueue == BattleState.ENEMY3TURN)
        {
            index = 2;
            enemyTarget = enemy3AttackTarget;

        }
        if (currentInQueue == BattleState.ENEMY4TURN)
        {
            index = 3;
            enemyTarget = enemy4AttackTarget;

        }

        if (enemyTarget == 0)
        {
            character = Engine.e.activeParty.gameObject;
        }

        if (enemyTarget == 1)
        {
            character = Engine.e.activePartyMember2.gameObject;
        }

        if (enemyTarget == 2)
        {
            character = Engine.e.activePartyMember3.gameObject;
        }

        if (enemyAttacking && enemyAtBattlePos)
        {
            Vector3 targetPos = Vector3.MoveTowards(enemies[index].gameObject.transform.position, character.transform.position, 8 * Time.deltaTime);

            enemies[index].GetComponent<Rigidbody2D>().MovePosition(targetPos);

            if (Vector3.Distance(enemies[index].transform.position, character.transform.position) < 1)
            {
                enemyAttacking = false;
                enemyAtBattlePos = false;


                isDead = Engine.e.TakeDamage(enemyTarget, damageTotal, enemies[index].GetComponent<Enemy>().hitChance);
                GameObject dmgPopup = Instantiate(damagePopup, character.transform.position, Quaternion.identity);

                if (dodgedAttack)
                {
                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Dodged";
                    dodgedAttack = false;
                }
                else
                {
                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = damageTotal.ToString();
                    character.GetComponent<SpriteRenderer>().material = damageFlash;
                    yield return new WaitForSeconds(0.2f);
                    character.GetComponent<SpriteRenderer>().material = originalMaterial;
                }
                Destroy(dmgPopup, 0.8f);
            }
        }

        if (!enemyAttacking && !enemyAtBattlePos)
        {
            Vector3 returnPos = Vector3.MoveTowards(enemies[index].gameObject.transform.position, enemies[index].GetComponent<Enemy>().currentBattlePos, 6 * Time.deltaTime);

            enemies[index].GetComponent<Rigidbody2D>().MovePosition(returnPos);

            if (enemies[index].gameObject.transform.position == enemies[index].GetComponent<Enemy>().currentBattlePos)
            {
                enemyAtBattlePos = true;
            }

            if (enemyAtBattlePos && !enemyAttacking)
            {

                if (enemies[index].GetComponent<Enemy>().isConfused)
                {
                    enemies[index].GetComponent<Enemy>().confuseTimer++;
                    if (enemies[index].GetComponent<Enemy>().confuseTimer == 3)
                    {
                        enemies[index].GetComponent<Enemy>().isConfused = false;
                        enemies[index].GetComponent<Enemy>().confuseTimer = 0;
                    }
                }

                if (enemies[index].GetComponent<Enemy>().isPoisoned)
                {
                    enemies[index].GetComponent<Enemy>().TakePoisonDamage(index, enemies[index].GetComponent<Enemy>().poisonDmg);


                }

                if (enemies[index].GetComponent<Enemy>().deathInflicted)
                {
                    enemies[index].GetComponent<Enemy>().TakeDeathDamage();
                }
                if (enemies[index].GetComponent<EnemyMovement>() != null)
                {
                    enemies[index].GetComponent<EnemyMovement>().enabled = true;
                }
                enemyMoving = false;
                switch (index)
                {
                    case 0:
                        enemy1ATB = 0;
                        break;
                    case 1:
                        enemy2ATB = 0;
                        break;
                    case 2:
                        enemy3ATB = 0;
                        break;
                    case 3:
                        enemy4ATB = 0;
                        break;
                }

                //ActiveCheckNext();
                if (Engine.e.battleModeActive)
                {
                    battleQueue.Dequeue();
                    currentInQueue = BattleState.QUEUECHECK;

                    /*if (currentState == BattleState.ATBCHECK)
                    {
                        dialogueText.text = string.Empty;
                    }
                    if (currentState == BattleState.CHAR1TURN)
                    {
                        if (!activeParty.activeParty[0].GetComponent<Character>().isConfused)
                        {
                            dialogueText.text = string.Empty;
                            dialogueText.text = activeParty.activeParty[0].GetComponent<Character>().characterName;
                        }
                        else
                        {
                            state = BattleState.ATBCHECK;
                        }
                    }
                    if (currentState == BattleState.CHAR2TURN)
                    {
                        if (!activeParty.activeParty[1].GetComponent<Character>().isConfused)
                        {
                            dialogueText.text = string.Empty;
                            dialogueText.text = activeParty.activeParty[1].GetComponent<Character>().characterName;
                        }
                        else
                        {
                            state = BattleState.ATBCHECK;
                        }
                    }
                    if (currentState == BattleState.CHAR3TURN)
                    {
                        if (!activeParty.activeParty[2].GetComponent<Character>().isConfused)
                        {
                            dialogueText.text = string.Empty;
                            dialogueText.text = activeParty.activeParty[2].GetComponent<Character>().characterName;
                        }
                        else
                        {
                            state = BattleState.ATBCHECK;
                        }
                    }*/
                }

                yield return new WaitForSeconds(0.3f);

                if (isDead)
                {
                    state = BattleState.LOST;
                    yield return new WaitForSeconds(2f);
                    StartCoroutine(EndBattle());
                }
                else

                //StartCoroutine(CheckNext());
                if (!Engine.e.battleModeActive)
                {
                    state = BattleState.ATBCHECK;
                }
            }
        }
    }



    IEnumerator ConfuseAttackMoveToTargetEnemy()
    {
        int index = 0;

        if (state == BattleState.ENEMY1TURN)
        {
            index = 0;
        }
        if (state == BattleState.ENEMY2TURN)
        {
            index = 1;
        }
        if (state == BattleState.ENEMY3TURN)
        {
            index = 2;
        }
        if (state == BattleState.ENEMY4TURN)
        {
            index = 3;
        }


        if (enemyAttacking && enemyAtBattlePos)
        {
            Vector3 targetPos = Vector3.MoveTowards(enemies[index].gameObject.transform.position, enemies[randTarget].transform.position, 6 * Time.deltaTime);

            enemies[index].GetComponent<Rigidbody2D>().MovePosition(targetPos);

            if (Vector3.Distance(enemies[index].transform.position, enemies[randTarget].transform.position) < 1)
            {
                enemyAttacking = false;
                enemyAtBattlePos = false;

                enemies[randTarget].GetComponent<Enemy>().TakePhysicalDamage(randTarget, damageTotal, enemies[index].GetComponent<Enemy>().hitChance);
                GameObject dmgPopup = Instantiate(damagePopup, enemies[randTarget].transform.position, Quaternion.identity);
                if (dodgedAttack)
                {
                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Dodged";
                    dodgedAttack = false;
                }
                else
                {
                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = damageTotal.ToString();
                    enemies[randTarget].GetComponent<SpriteRenderer>().material = damageFlash;
                    yield return new WaitForSeconds(0.2f);
                    enemies[randTarget].GetComponent<SpriteRenderer>().material = originalMaterial;
                }
                Destroy(dmgPopup, 0.8f);
            }
        }

        if (!enemyAttacking && !enemyAtBattlePos)
        {
            Vector3 returnPos = Vector3.MoveTowards(enemies[index].gameObject.transform.position, enemies[index].GetComponent<Enemy>().currentBattlePos, 6 * Time.deltaTime);

            enemies[index].GetComponent<Rigidbody2D>().MovePosition(returnPos);

            if (enemies[index].gameObject.transform.position == enemies[index].GetComponent<Enemy>().currentBattlePos)
            {
                enemyAtBattlePos = true;
            }

            if (enemyAtBattlePos && !enemyAttacking)
            {
                if (enemies[index].GetComponent<EnemyMovement>() != null)
                {
                    enemies[index].GetComponent<EnemyMovement>().enabled = true;
                }

                enemyMoving = false;

                /*  switch (index)
                  {
                      case 0:
                          char1ATB = 0;
                          break;
                      case 1:
                          char2ATB = 0;
                          break;
                      case 2:
                          char3ATB = 0;
                          break;
                  }*/

                //ActiveCheckNext();
                if (Engine.e.battleModeActive)
                {
                    state = currentState;

                    if (currentState == BattleState.ATBCHECK)
                    {
                        dialogueText.text = string.Empty;
                    }
                    if (currentState == BattleState.CHAR1TURN)
                    {
                        dialogueText.text = string.Empty;
                        dialogueText.text = activeParty.activeParty[0].GetComponent<Character>().characterName;
                    }
                    if (currentState == BattleState.CHAR2TURN)
                    {
                        dialogueText.text = string.Empty;
                        dialogueText.text = activeParty.activeParty[1].GetComponent<Character>().characterName;
                    }
                    if (currentState == BattleState.CHAR3TURN)
                    {
                        dialogueText.text = string.Empty;
                        dialogueText.text = activeParty.activeParty[2].GetComponent<Character>().characterName;
                    }
                }

                yield return new WaitForSeconds(0.3f);

                if (isDead)
                {
                    state = BattleState.LOST;
                    yield return new WaitForSeconds(2f);
                    StartCoroutine(EndBattle());
                }
                else

                    // StartCoroutine(CheckNext());
                    state = BattleState.ATBCHECK;

            }
        }
    }


    IEnumerator EnemyDropAttack()
    {

        if (!dropExists)
        {
            enemyAttackDrop = false;
            // currentInQueue = nextInQueue;

        }
        else
        {
            if (state == currentInQueue)
                state = BattleState.ATBCHECK;
        }

        //   ActiveCheckNext();
        if (Engine.e.battleModeActive)
        {
            //state = BattleState.ATBCHECK;
            if (currentInQueue == BattleState.CONFCHAR1)
            {
                char1ATB = 0;
            }
            if (currentInQueue == BattleState.CONFCHAR2)
            {
                char2ATB = 0;
            }
            if (currentInQueue == BattleState.CONFCHAR3)
            {
                char3ATB = 0;
            }

            if (currentInQueue == BattleState.ENEMY1TURN)
            {
                enemy1ATB = 0;
                if (enemies[0].GetComponent<EnemyMovement>() != null)
                {
                    enemies[0].GetComponent<EnemyMovement>().enabled = true;
                }
            }
            if (currentInQueue == BattleState.ENEMY2TURN)
            {
                enemy2ATB = 0;
                if (enemies[1].GetComponent<EnemyMovement>() != null)
                {
                    enemies[1].GetComponent<EnemyMovement>().enabled = true;
                }
            }
            if (currentInQueue == BattleState.ENEMY3TURN)
            {
                enemy3ATB = 0;
                if (enemies[2].GetComponent<EnemyMovement>() != null)
                {
                    enemies[2].GetComponent<EnemyMovement>().enabled = true;
                }
            }
            if (currentInQueue == BattleState.ENEMY4TURN)
            {
                enemy4ATB = 0;
                if (enemies[3].GetComponent<EnemyMovement>() != null)
                {
                    enemies[3].GetComponent<EnemyMovement>().enabled = true;
                }
            }
        }
        /*state = currentState;

        if (currentState == BattleState.ATBCHECK)
        {
            dialogueText.text = string.Empty;
        }

        if (currentState == BattleState.CHAR1TURN)
        {
            if (activeParty.activeParty[0].GetComponent<Character>().isConfused)
            {
                dialogueText.text = string.Empty;
                state = BattleState.ATBCHECK;
            }
            else
            {
                dialogueText.text = string.Empty;
                dialogueText.text = activeParty.activeParty[0].GetComponent<Character>().characterName;
            }
        }
        if (currentState == BattleState.CHAR2TURN)
        {
            if (activeParty.activeParty[1].GetComponent<Character>().isConfused)
            {
                dialogueText.text = string.Empty;
                state = BattleState.ATBCHECK;
            }
            else
            {
                dialogueText.text = string.Empty;
                dialogueText.text = activeParty.activeParty[1].GetComponent<Character>().characterName;
            }
        }
        if (currentState == BattleState.CHAR3TURN)
        {
            if (activeParty.activeParty[2].GetComponent<Character>().isConfused)
            {
                dialogueText.text = string.Empty;
                state = BattleState.ATBCHECK;
            }
            else
            {
                dialogueText.text = string.Empty;
                dialogueText.text = activeParty.activeParty[2].GetComponent<Character>().characterName;
            }
        }
    }*/
        if (!dropExists && !enemyAttackDrop)
        {
            battleQueue.Dequeue();

            if (battleQueue.Count > 0)
            {
                currentInQueue = battleQueue.Peek();
            }
            else
            {
                currentInQueue = BattleState.ATBCHECK;
            }
            yield return new WaitForSeconds(0.3f);

            if (isDead)
            {
                state = BattleState.LOST;
                yield return new WaitForSeconds(2f);
                StartCoroutine(EndBattle());

            }
            else
            {
                // StartCoroutine(CheckNext());
                if (!Engine.e.battleModeActive)
                    state = BattleState.ATBCHECK;

            }
        }
    }

    IEnumerator EnemyConfuseAttack()
    {
        int index = 0;

        if (currentInQueue == BattleState.ENEMY1TURN)
        {
            index = 0;
        }
        if (currentInQueue == BattleState.ENEMY2TURN)
        {
            index = 1;
        }
        if (currentInQueue == BattleState.ENEMY3TURN)
        {
            index = 2;
        }
        if (currentInQueue == BattleState.ENEMY4TURN)
        {
            index = 3;
        }

        yield return new WaitForSeconds(0.3f);

        //  if (enemies[index].GetComponent<Enemy>().isPoisoned)
        //  {
        //      enemies[index].GetComponent<Enemy>().TakePoisonDamage(index, enemies[index].GetComponent<Enemy>().poisonDmg);
        //  }  

        if (enemies[index].GetComponent<Enemy>().drops[0] != null)
        {
            int choiceAttack = Random.Range(0, 100);

            if (enemies[index].GetComponent<Enemy>().choiceAttack < choiceAttack)
            {
                enemyMoving = true;
                enemyAttacking = true;

                if (teamTargetChoice == 0)
                {
                    Engine.e.PhysicalDamageCalculation(randTarget, enemies[index].gameObject.GetComponent<Enemy>().damage);
                }
                else
                {
                    if (enemies[randTarget] != null)
                    {
                        enemies[randTarget].GetComponent<Enemy>().TakePhysicalDamage(randTarget, enemies[index].GetComponent<Enemy>().damageTotal, enemies[index].GetComponent<Enemy>().hitChance);
                    }
                }
            }
            else
            {
                enemyDropChoice = Random.Range(0, enemies[index].GetComponent<Enemy>().drops.Length);

                if (enemies[index].GetComponent<Enemy>().mana >= enemies[index].GetComponent<Enemy>().drops[enemyDropChoice].dropCost)
                {
                    physicalAttack = false;
                    confuseAttack = false;

                    enemyAttackDrop = true;

                    lastDropChoice = enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice];

                    if (teamTargetChoice == 0)
                    {
                        if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropType == "Fire")
                        {
                            Instantiate(enemyFireDropAnim, enemies[index].transform.position, Quaternion.identity);
                        }
                        if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropType == "Water")
                        {
                            Instantiate(enemyWaterDropAnim, enemies[index].transform.position, Quaternion.identity);
                        }
                        if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropType == "Lightning")
                        {
                            Instantiate(enemyLightningDropAnim, enemies[index].transform.position, Quaternion.identity);
                        }
                        if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropType == "Shadow")
                        {
                            if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropName == "Bio" || enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropName == "Knockout"
                            || enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropName == "Blind")
                            {
                                if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropName == "Bio")
                                {
                                    Instantiate(enemyPoisonAnim, enemies[index].transform.position, Quaternion.identity);
                                }
                                if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropName == "Knockout")
                                {
                                    Instantiate(enemySleepAnim, enemies[index].transform.position, Quaternion.identity);
                                }
                                if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropName == "Blind")
                                {
                                    Instantiate(enemyConfuseAnim, enemies[index].transform.position, Quaternion.identity);
                                }
                            }
                            else
                                Instantiate(enemyShadowDropAnim, enemies[index].transform.position, Quaternion.identity);
                        }
                        if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropType == "Ice")
                        {
                            Instantiate(enemyIceDropAnim, enemies[index].transform.position, Quaternion.identity);
                        }
                        isDead = Engine.e.TakeElementalDamage(randTarget, enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropPower, enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropType);
                    }
                    else
                    {

                        if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropType == "Fire")
                        {
                            Instantiate(fireDropAnim, enemies[index].transform.position, Quaternion.identity);
                        }
                        if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropType == "Water")
                        {
                            Instantiate(waterDropAnim, enemies[index].transform.position, Quaternion.identity);
                        }
                        if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropType == "Lightning")
                        {
                            Instantiate(lightningDropAnim, enemies[index].transform.position, Quaternion.identity);
                        }
                        if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropType == "Shadow")
                        {
                            if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropName == "Bio" || enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropName == "Knockout"
                            || enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropName == "Blind")
                            {
                                if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropName == "Bio")
                                {
                                    Instantiate(poisonAnim, enemies[index].transform.position, Quaternion.identity);
                                }
                                if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropName == "Knockout")
                                {
                                    Instantiate(sleepAnim, enemies[index].transform.position, Quaternion.identity);
                                }
                                if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropName == "Blind")
                                {
                                    Instantiate(confuseAnim, enemies[index].transform.position, Quaternion.identity);
                                }
                            }
                            else
                                Instantiate(shadowDropAnim, enemies[index].transform.position, Quaternion.identity);
                        }
                        if (enemies[index].gameObject.GetComponent<Enemy>().drops[enemyDropChoice].dropType == "Ice")
                        {
                            Instantiate(iceDropAnim, enemies[index].transform.position, Quaternion.identity);
                        }

                        if (enemies[target] != null)
                        {
                            enemies[target].GetComponent<Enemy>().ConfuseTakeDropDamage(randTarget, enemies[index].GetComponent<Enemy>().drops[enemyDropChoice]);
                        }
                        if (currentInQueue == BattleState.CHAR1TURN)
                        {
                            char1ATB = 0;
                        }
                        if (currentInQueue == BattleState.CHAR2TURN)
                        {
                            char2ATB = 0;
                        }
                        if (currentInQueue == BattleState.CHAR3TURN)
                        {
                            char3ATB = 0;
                        }

                        if (currentInQueue == BattleState.ENEMY1TURN)
                        {
                            enemy1ATB = 0;
                        }
                        if (currentInQueue == BattleState.ENEMY2TURN)
                        {
                            enemy2ATB = 0;
                        }
                        if (currentInQueue == BattleState.ENEMY3TURN)
                        {
                            enemy3ATB = 0;
                        }
                        if (currentInQueue == BattleState.ENEMY4TURN)
                        {
                            enemy4ATB = 0;
                        }
                        enemies[index].GetComponent<Enemy>().mana -= enemies[index].GetComponent<Enemy>().drops[enemyDropChoice].dropCost;

                        if (enemies[index].GetComponent<EnemyMovement>() != null)
                        {
                            enemies[index].GetComponent<EnemyMovement>().enabled = true;
                        }

                        if (enemies[index].GetComponent<Enemy>().isConfused)
                        {
                            enemies[index].GetComponent<Enemy>().confuseTimer++;
                            if (enemies[index].GetComponent<Enemy>().confuseTimer == 3)
                            {
                                enemies[index].GetComponent<Enemy>().isConfused = false;
                                enemies[index].GetComponent<Enemy>().confuseTimer = 0;
                            }
                        }

                        if (enemies[index].GetComponent<Enemy>().isPoisoned)
                        {
                            enemies[index].GetComponent<Enemy>().TakePoisonDamage(index, enemies[index].GetComponent<Enemy>().poisonDmg);


                        }

                        if (enemies[index].GetComponent<Enemy>().deathInflicted)
                        {
                            enemies[index].GetComponent<Enemy>().TakeDeathDamage();
                        }
                    }
                }
                else
                {
                    enemyMoving = true;
                    enemyAttacking = true;
                    if (teamTargetChoice == 0)
                    {
                        Engine.e.PhysicalDamageCalculation(randTarget, enemies[index].gameObject.GetComponent<Enemy>().damage);
                    }
                    else
                    {
                        enemies[randTarget].GetComponent<Enemy>().TakePhysicalDamage(randTarget, enemies[index].GetComponent<Enemy>().damageTotal, enemies[index].GetComponent<Enemy>().hitChance);
                    }
                }
            }
        }
        else
        {
            enemyMoving = true;
            enemyAttacking = true;
            if (teamTargetChoice == 0)
            {
                Engine.e.PhysicalDamageCalculation(randTarget, enemies[index].gameObject.GetComponent<Enemy>().damage);
            }
            else
            {
                enemies[randTarget].GetComponent<Enemy>().TakePhysicalDamage(randTarget, enemies[index].GetComponent<Enemy>().damageTotal, enemies[index].GetComponent<Enemy>().hitChance);
            }
        }
        confuseAttack = false;
        if (enemies[index].GetComponent<Enemy>().isConfused)
        {
            enemies[index].GetComponent<Enemy>().confuseTimer++;
            if (enemies[index].GetComponent<Enemy>().confuseTimer == 3)
            {
                enemies[index].GetComponent<Enemy>().isConfused = false;
                enemies[index].GetComponent<Enemy>().confuseTimer = 0;
            }
        }

        if (enemies[index].GetComponent<Enemy>().isPoisoned)
        {
            enemies[index].GetComponent<Enemy>().TakePoisonDamage(index, enemies[index].GetComponent<Enemy>().poisonDmg);


        }

        if (enemies[index].GetComponent<Enemy>().deathInflicted)
        {
            enemies[index].GetComponent<Enemy>().TakeDeathDamage();
        }
        if (enemies[index].gameObject.GetComponent<Enemy>().health <= 0)
        {
            enemies[index].gameObject.GetComponent<Enemy>().health = 0;

            isDead = EnemyGroup.enemyGroup.CheckEndBattle();

            if (isDead)
            {
                state = BattleState.LEVELUPCHECK;
                yield return new WaitForSeconds(2f);
                StartCoroutine(LevelUpCheck());
            }
        }
        if (state == BattleState.CONFCHAR1)
        {
            char1ATB = 0;
        }
        if (state == BattleState.CONFCHAR2)
        {
            char2ATB = 0;
        }
        if (state == BattleState.CONFCHAR3)
        {
            char3ATB = 0;
        }

        if (state == BattleState.ENEMY1TURN)
        {
            enemy1ATB = 0;
        }
        if (state == BattleState.ENEMY2TURN)
        {
            enemy2ATB = 0;
        }
        if (state == BattleState.ENEMY3TURN)
        {
            enemy3ATB = 0;
        }
        if (state == BattleState.ENEMY4TURN)
        {
            enemy4ATB = 0;
        }
        state = currentState;

        if (currentState == BattleState.ATBCHECK)
        {
            dialogueText.text = string.Empty;
        }

        if (currentState == BattleState.CHAR1TURN)
        {
            if (activeParty.activeParty[0].GetComponent<Character>().isConfused)
            {
                dialogueText.text = string.Empty;
                state = BattleState.ATBCHECK;
            }
            else
            {
                dialogueText.text = string.Empty;
                dialogueText.text = activeParty.activeParty[0].GetComponent<Character>().characterName;
            }
        }
        if (currentState == BattleState.CHAR2TURN)
        {
            if (activeParty.activeParty[1].GetComponent<Character>().isConfused)
            {
                dialogueText.text = string.Empty;
                state = BattleState.ATBCHECK;
            }
            else
            {
                dialogueText.text = string.Empty;
                dialogueText.text = activeParty.activeParty[1].GetComponent<Character>().characterName;
            }
        }
        if (currentState == BattleState.CHAR3TURN)
        {
            if (activeParty.activeParty[2].GetComponent<Character>().isConfused)
            {
                dialogueText.text = string.Empty;
                state = BattleState.ATBCHECK;
            }
            else
            {
                dialogueText.text = string.Empty;
                dialogueText.text = activeParty.activeParty[2].GetComponent<Character>().characterName;
            }
        }
    }

    IEnumerator LevelUpCheck()
    {
        if (state == BattleState.LEVELUPCHECK)
        {
            DeactivateEnemyUI();
            DeactivateChar1MenuButtons();
            DeactivateDropsUI();
            DeactivateSkillsUI();
            DeactivateChar2SwitchButtons();

            if (activeParty.activeParty[1] != null)
            {
                DeactivateChar2MenuButtons();
                DeactivateDropsUI();
                DeactivateSkillsUI();
                DeactivateChar2SwitchButtons();
            }

            if (activeParty.activeParty[2] != null)
            {
                DeactivateChar3MenuButtons();
                DeactivateDropsUI();
                DeactivateSkillsUI();
                DeactivateChar3SwitchButtons();
            }
            enemyGroup.GroupItemDrops();

            Engine.e.enemyPanel.SetActive(false);
            Engine.e.enemyLootPanelReference.SetActive(true);
            Engine.e.GiveExperiencePoints(enemyGroupExperiencePoints);
            if (Engine.e.char1LevelUp == true || Engine.e.char2LevelUp == true || Engine.e.char3LevelUp == true)
            {
                {
                    yield return new WaitForSeconds(0.1f);

                    state = BattleState.LEVELUP;
                    StartCoroutine(LevelUp());
                }
            }
            else
            {
                yield return new WaitForSeconds(3f);

                state = BattleState.WON;
                StartCoroutine(EndBattle());

                if (activeParty.activeParty[0].GetComponent<Character>().currentHealth <= 0)
                {
                    activeParty.activeParty[0].GetComponent<Character>().currentHealth = 1;
                }
                if (activeParty.activeParty[1] != null && activeParty.activeParty[1].GetComponent<Character>().currentHealth <= 0)
                {
                    activeParty.activeParty[1].GetComponent<Character>().currentHealth = 1;
                }
                if (activeParty.activeParty[2] != null && activeParty.activeParty[2].GetComponent<Character>().currentHealth <= 0)
                {
                    activeParty.activeParty[2].GetComponent<Character>().currentHealth = 1;
                }
            }
        }
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            Engine.e.activeParty.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            enemyGroup.battleCamera.gameObject.SetActive(false);
            Engine.e.char1LevelUp = false;
            Engine.e.char2LevelUp = false;
            Engine.e.char3LevelUp = false;

            enemyGroup.DestroyGroup();
            ResetPartyMemberStats();

            isDead = false;

            dialogueText.text = string.Empty;
            dialogueText.text = "Victory.";

            Engine.e.battleMenu.battleMenuUI.SetActive(false);
            Engine.e.enemyLootPanelReference.SetActive(false);
            Engine.e.enemyPanel.SetActive(true);
            Engine.e.enemyLootReference.GetComponent<TMP_Text>().text = string.Empty;
            Engine.e.inBattle = false;
            Engine.e.storeDialogueReference.gameObject.SetActive(true);

            for (int i = 0; i < battleItems.Length; i++)
            {
                battleItems[i].item = null;
                battleItems[i].itemName.GetComponent<TMP_Text>().text = string.Empty;
                battleItems[i].itemCount.GetComponent<TMP_Text>().text = string.Empty;

            }

            if (Engine.e.activeParty.activeParty[1] != null)
            {
                char2UI.SetActive(true);
                charIndexName[1] = activeParty.activeParty[1].gameObject.GetComponent<Character>().characterName;
            }
            if (Engine.e.activeParty.activeParty[2] != null)
            {
                char3UI.SetActive(true);
                charIndexName[2] = activeParty.activeParty[2].gameObject.GetComponent<Character>().characterName;
            }
            if (enemyGroup.battleCamera != null)
            {
                Engine.e.mainCamera.GetComponent<CinemachineVirtualCamera>().Priority = 10;

                yield return new WaitForSeconds(2f);

            }

            //state = BattleState.START;
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            if (state == BattleState.LOST)
            {
                dialogueText.text = string.Empty;
                dialogueText.text = "Game Over.";
                yield return new WaitForSeconds(5f);
                SceneManager.UnloadSceneAsync(Engine.e.currentScene);
                Engine.e.inBattle = false;
                Engine.e.battleSystemMenu.SetActive(false);
                Engine.e.mainMenu.SetActive(true);

            }
        }
        //       GameManager.gameManager.battleMusic.Stop();
    }

    IEnumerator LevelUp()
    {
        if (state == BattleState.LEVELUP)
        {
            if (Engine.e.char1LevelUp == true)
            {
                Engine.e.char1Panel.SetActive(false);
                Engine.e.char1LevelUpPanel.SetActive(true);
            }
            if (Engine.e.char2LevelUp == true)
            {
                Engine.e.char2Panel.SetActive(false);
                Engine.e.char2LevelUpPanel.SetActive(true);
            }
            if (Engine.e.char3LevelUp == true)
            {
                Engine.e.char3Panel.SetActive(false);
                Engine.e.char3LevelUpPanel.SetActive(true);
            }
        }

        dialogueText.text = string.Empty;

        yield return new WaitForSeconds(7f);

        Engine.e.char1LevelUpPanel.SetActive(false);
        Engine.e.char2LevelUpPanel.SetActive(false);
        Engine.e.char3LevelUpPanel.SetActive(false);

        Engine.e.char1Panel.SetActive(true);

        if (activeParty.activeParty[1] != null)
        {
            Engine.e.char2Panel.SetActive(true);
        }
        if (activeParty.activeParty[2] != null)
        {
            Engine.e.char3Panel.SetActive(true);
        }

        state = BattleState.WON;
        StartCoroutine(EndBattle());
    }

    public void Char1Turn()
    {
        if (failedItemUse)
        {
            char1ATB = ATBReady;
            failedItemUse = false;
        }

        state = BattleState.CHAR1TURN;


        dialogueText.text = Engine.e.activeParty.activeParty[0].gameObject.GetComponent<Character>().characterName;


        ActivateChar1MenuButtons();
        DeactivateDropsUI();
        ActivateCharDropsUI();
        DeactivateSkillsUI();
        ActivateCharSkillsUI();
        EnableButtonInteraction();

        inBattleMenu = false;
        battleItemMenu.SetActive(false);
        physicalAttack = false;
        dropAttack = false;
        skillPhysicalAttack = false;
        skillRangedAttack = false;
        skillTargetSupport = false;
        skillSelfSupport = false;
        usingItem = false;
        failedItemUse = false;
        attackingTeam = false;
        partyCheckNext = false;

        char1Attacking = false;
        char1DropAttack = false;
        char1SkillAttack = false;
        char1SkillPhysicalAttack = false;
        char1SkillRangedAttack = false;
        char1Supporting = false;
        char1UsingItem = false;

        char1SkillChoice = null; // Could be used for "quick last skill choice"

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(char1MenuButtons[0]);

    }

    public void Char1ConfusedTurn()
    {
        char1Ready = false;

        dialogueText.text = Engine.e.activeParty.activeParty[0].gameObject.GetComponent<Character>().characterName;


        if (activeParty.activeParty[0].GetComponent<Character>().isPoisoned)
        {
            GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.activeParty.transform.position, Quaternion.identity);
            isDead = Engine.e.TakePoisonDamage(0, activeParty.activeParty[0].GetComponent<Character>().poisonDmg);
            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = activeParty.activeParty[0].GetComponent<Character>().poisonDmg.ToString();
            Destroy(dmgPopup, 1f);
        }

        confuseTargetCheck = true;
        confuseAttack = true;
        StartCoroutine(ConfuseTargetFinder());

    }

    public void Char2Turn()
    {
        if (failedItemUse)
        {
            char2ATB = ATBReady;
            failedItemUse = false;
        }


        state = BattleState.CHAR2TURN;


        dialogueText.text = Engine.e.activeParty.activeParty[1].gameObject.GetComponent<Character>().characterName;


        ActivateChar2MenuButtons();
        DeactivateDropsUI();
        ActivateCharDropsUI();
        DeactivateSkillsUI();
        ActivateCharSkillsUI();
        EnableButtonInteraction();

        inBattleMenu = false;
        battleItemMenu.SetActive(false);
        physicalAttack = false;
        dropAttack = false;
        skillPhysicalAttack = false;
        skillRangedAttack = false;
        skillTargetSupport = false;
        skillSelfSupport = false;
        usingItem = false;
        failedItemUse = false;
        attackingTeam = false;
        partyCheckNext = false;

        char2Attacking = false;
        char2DropAttack = false;
        char2SkillAttack = false;
        char2SkillPhysicalAttack = false;
        char2SkillRangedAttack = false;
        char2Supporting = false;
        char2UsingItem = false;

        char2SkillChoice = null; // Could be used for "quick last skill choice"

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(char2MenuButtons[0]);

    }
    public void Char2ConfusedTurn()
    {
        char2Ready = false;


        dialogueText.text = Engine.e.activeParty.activeParty[1].gameObject.GetComponent<Character>().characterName;



        if (activeParty.activeParty[1].GetComponent<Character>().isPoisoned)
        {
            GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.activePartyMember2.transform.position, Quaternion.identity);
            isDead = Engine.e.TakePoisonDamage(1, activeParty.activeParty[1].GetComponent<Character>().poisonDmg);
            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = activeParty.activeParty[1].GetComponent<Character>().poisonDmg.ToString();
            Destroy(dmgPopup, 1f);
        }
        confuseTargetCheck = true;
        confuseAttack = true;
        StartCoroutine(ConfuseTargetFinder());

    }

    public void Char3Turn()
    {
        if (failedItemUse)
        {
            char3ATB = ATBReady;
            failedItemUse = false;
        }

        state = BattleState.CHAR3TURN;


        dialogueText.text = Engine.e.activeParty.activeParty[2].gameObject.GetComponent<Character>().characterName;


        ActivateChar3MenuButtons();
        DeactivateDropsUI();
        ActivateCharDropsUI();
        DeactivateSkillsUI();
        ActivateCharSkillsUI();
        EnableButtonInteraction();

        inBattleMenu = false;
        battleItemMenu.SetActive(false);
        physicalAttack = false;
        dropAttack = false;
        skillPhysicalAttack = false;
        skillRangedAttack = false;
        skillTargetSupport = false;
        skillSelfSupport = false;
        usingItem = false;
        failedItemUse = false;
        attackingTeam = false;
        partyCheckNext = false;

        char3Attacking = false;
        char3DropAttack = false;
        char3SkillAttack = false;
        char3SkillPhysicalAttack = false;
        char3SkillRangedAttack = false;
        char3Supporting = false;
        char3UsingItem = false;

        char3SkillChoice = null; // Could be used for "quick last skill choice"

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(char3MenuButtons[0]);

    }

    public void Char3ConfusedTurn()
    {
        char3Ready = false;


        dialogueText.text = Engine.e.activeParty.activeParty[2].gameObject.GetComponent<Character>().characterName;



        if (activeParty.activeParty[2].GetComponent<Character>().isPoisoned)
        {
            GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.activePartyMember3.transform.position, Quaternion.identity);
            isDead = Engine.e.TakePoisonDamage(2, activeParty.activeParty[2].GetComponent<Character>().poisonDmg);
            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = activeParty.activeParty[2].GetComponent<Character>().poisonDmg.ToString();
            Destroy(dmgPopup, 1f);
        }
        confuseTargetCheck = true;
        confuseAttack = true;
        StartCoroutine(ConfuseTargetFinder());

    }
    void Enemy1Turn()
    {
        enemy1Ready = false;
        if (enemies[0].GetComponent<EnemyMovement>() != null)
        {
            enemies[0].GetComponent<EnemyMovement>().enabled = false;
        }

        //DisableButtonInteraction();
        partyCheckNext = false;
        dialogueText.text = string.Empty;
        dialogueText.text = enemies[0].GetComponent<Enemy>().enemyName;

        if (!enemies[0].GetComponent<Enemy>().isConfused)
        {
            StartCoroutine(EnemyAttack());
        }
        else
        {
            confuseTargetCheck = true;
            confuseAttack = true;
            StartCoroutine(ConfuseTargetFinder());
        }
    }

    void Enemy2Turn()
    {
        enemy2Ready = false;

        if (enemies[1].GetComponent<EnemyMovement>() != null)
        {
            enemies[1].GetComponent<EnemyMovement>().enabled = false;
        }

        dialogueText.text = string.Empty;
        dialogueText.text = enemies[1].GetComponent<Enemy>().enemyName;
        partyCheckNext = false;

        if (!enemies[1].GetComponent<Enemy>().isConfused)
        {
            StartCoroutine(EnemyAttack());
        }
        else
        {
            confuseTargetCheck = true;
            confuseAttack = true;
            StartCoroutine(ConfuseTargetFinder());
        }
    }

    void Enemy3Turn()
    {
        enemy3Ready = false;

        if (enemies[2].GetComponent<EnemyMovement>() != null)
        {
            enemies[2].GetComponent<EnemyMovement>().enabled = false;
        }

        dialogueText.text = string.Empty;
        dialogueText.text = enemies[2].GetComponent<Enemy>().enemyName;
        partyCheckNext = false;

        if (!enemies[2].GetComponent<Enemy>().isConfused)
        {
            StartCoroutine(EnemyAttack());
        }
        else
        {
            confuseTargetCheck = true;
            confuseAttack = true;
            StartCoroutine(ConfuseTargetFinder());
        }
    }

    void Enemy4Turn()
    {
        enemy4Ready = false;

        if (enemies[3].GetComponent<EnemyMovement>() != null)
        {
            enemies[3].GetComponent<EnemyMovement>().enabled = false;
        }

        dialogueText.text = string.Empty;
        dialogueText.text = enemies[3].GetComponent<Enemy>().enemyName;
        if (!enemies[3].GetComponent<Enemy>().isConfused)
        {
            StartCoroutine(EnemyAttack());
        }
        else
        {
            confuseTargetCheck = true;
            confuseAttack = true;
            StartCoroutine(ConfuseTargetFinder());
        }

    }

    IEnumerator ConfuseTargetFinder()
    {
        if (confuseTargetCheck)
        {
            Debug.Log("entered ConfuseTargetFinder");
            confuseTargetCheck = false;
            yield return new WaitForSeconds(0.3f);

            teamTargetChoice = Random.Range(0, 2);
            int index = 0;
            bool partyAttacking = false;

            //if (state == BattleState.CHAR1TURN)
            //{
            if (currentInQueue == BattleState.CONFCHAR1)
            {
                index = 0;
                partyAttacking = true;
            }
            if (currentInQueue == BattleState.CONFCHAR2)
            {
                index = 1;
                partyAttacking = true;

            }
            if (currentInQueue == BattleState.CONFCHAR3)
            {
                index = 2;
                partyAttacking = true;

            }

            if (currentInQueue == BattleState.ENEMY1TURN)
            {
                index = 0;
            }
            if (currentInQueue == BattleState.ENEMY2TURN)
            {
                index = 1;
            }
            if (currentInQueue == BattleState.ENEMY3TURN)
            {
                index = 2;
            }
            if (currentInQueue == BattleState.ENEMY4TURN)
            {
                index = 3;
            }

            if (partyAttacking)
            {
                if (teamTargetChoice == 0)
                {
                    randTarget = Engine.e.GetRandomRemainingPartyMember();
                    target = randTarget;
                    Engine.e.PhysicalDamageCalculation(randTarget, Engine.e.activeParty.activeParty[index].GetComponent<Character>().physicalDamage);
                    attackingTeam = true;
                }
                else
                {
                    randTarget = enemyGroup.GetRandomRemainingEnemy();
                    target = randTarget;

                }

                Debug.Log("Team Choice: " + teamTargetChoice + " Index: " + randTarget);
                StartCoroutine(CharConfuseAttack());
            }
            else
            {
                if (teamTargetChoice == 0)
                {
                    randTarget = Engine.e.GetRandomRemainingPartyMember();
                    target = randTarget;
                    Engine.e.PhysicalDamageCalculation(randTarget, enemies[index].GetComponent<Enemy>().damage);
                }
                else
                {
                    randTarget = enemyGroup.GetRandomRemainingEnemy();
                    target = randTarget;
                    attackingTeam = true;

                }
                Debug.Log("Team Choice: " + teamTargetChoice + " Index: " + randTarget);

                StartCoroutine(EnemyConfuseAttack());
            }
        }
    }

    public void AttackEnemyButton(int enemyTarget)
    {

        if (state == BattleState.CHAR1TURN || currentState == BattleState.CHAR1TURN)
        {
            char1AttackTarget = enemyTarget;
            char1PhysicalAttack = true;
            char1DropAttack = dropAttack;
            char1SkillPhysicalAttack = skillPhysicalAttack;
            char1Attacking = true;
            char1Ready = false;

            DeactivateChar1MenuButtons();

        }

        if (state == BattleState.CHAR2TURN || currentState == BattleState.CHAR2TURN)
        {
            char2AttackTarget = enemyTarget;
            char2PhysicalAttack = true;
            char2DropAttack = dropAttack;
            char2SkillPhysicalAttack = skillPhysicalAttack;
            char2Attacking = true;
            char2Ready = false;

            DeactivateChar2MenuButtons();

        }
        if (state == BattleState.CHAR3TURN || currentState == BattleState.CHAR3TURN)
        {
            char3AttackTarget = enemyTarget;
            char3PhysicalAttack = true;
            char3DropAttack = dropAttack;
            char3SkillPhysicalAttack = skillPhysicalAttack;
            char3Attacking = true;
            char3Ready = false;

            DeactivateChar3MenuButtons();

        }

        if (!Engine.e.battleModeActive)
        {
            if (enemies[enemyTarget].gameObject.GetComponent<Enemy>().health > 0)
            {
                if (!charMoving && !dropExists && !enemyMoving
                && state != BattleState.ENEMY1TURN && state != BattleState.ENEMY2TURN
                && state != BattleState.ENEMY3TURN && state != BattleState.ENEMY4TURN)
                {
                    SetPhysicalAttack();
                    StartCoroutine(CharAttack(enemyTarget));
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            if (state == BattleState.CHAR1TURN || currentState == BattleState.CHAR1TURN)
            {
                battleQueue.Enqueue(BattleState.CHAR1TURN);
            }

            if (state == BattleState.CHAR2TURN || currentState == BattleState.CHAR2TURN)
            {
                battleQueue.Enqueue(BattleState.CHAR2TURN);

            }

            if (state == BattleState.CHAR3TURN || currentState == BattleState.CHAR3TURN)
            {
                battleQueue.Enqueue(BattleState.CHAR3TURN);
            }

            DeactivateAttackButtons();
            DeactivateTargetSprite();

            state = BattleState.ATBCHECK;

        }
    }

    public void SupportAllyButton(int allyTarget)
    {
        if (state == BattleState.CHAR1TURN || currentState == BattleState.CHAR1TURN)
        {
            char1SupportTarget = allyTarget;
            char1UsingItem = usingItem;
            char1DropAttack = dropAttack;
            char1Supporting = true;

            battleQueue.Enqueue(BattleState.CHAR1TURN);

        }
        if (state == BattleState.CHAR2TURN || currentState == BattleState.CHAR2TURN)
        {
            char2SupportTarget = allyTarget;
            char2UsingItem = usingItem;
            char2DropAttack = dropAttack;

            char2Supporting = true;

            battleQueue.Enqueue(BattleState.CHAR2TURN);

        }
        if (state == BattleState.CHAR3TURN || currentState == BattleState.CHAR3TURN)
        {
            char3SupportTarget = allyTarget;
            char3UsingItem = usingItem;
            char3Supporting = true;
            char3DropAttack = dropAttack;

            battleQueue.Enqueue(BattleState.CHAR3TURN);

        }

        DeactivateAttackButtons();
        DeactivateSupportButtons();
        DeactivateTargetSprite();

    }

    public void SkillTargetEnemyButton(int enemyTarget)
    {
        if (state == BattleState.CHAR1TURN || currentState == BattleState.CHAR1TURN)
        {
            char1AttackTarget = enemyTarget;
            char1SkillChoice = lastSkillChoice;
            char1Attacking = true;
            char1SkillAttack = true;
        }
        if (state == BattleState.CHAR2TURN || currentState == BattleState.CHAR2TURN)
        {
            char2AttackTarget = enemyTarget;
            char2Attacking = true;
            char2SkillChoice = lastSkillChoice;
            char2SkillAttack = true;

        }
        if (state == BattleState.CHAR3TURN || currentState == BattleState.CHAR3TURN)
        {
            char3AttackTarget = enemyTarget;
            char3Attacking = true;
            char3SkillChoice = lastSkillChoice;
            char3SkillAttack = true;
        }

        if (!Engine.e.battleModeActive)
        {
            if (enemies[enemyTarget].gameObject.GetComponent<Enemy>().health > 0)
            {
                if (!charMoving && !dropExists && !enemyMoving
                && state != BattleState.ENEMY1TURN && state != BattleState.ENEMY2TURN
                && state != BattleState.ENEMY3TURN && state != BattleState.ENEMY4TURN)
                {
                    SetPhysicalAttack();
                    StartCoroutine(CharAttack(enemyTarget));
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            if (state == BattleState.CHAR1TURN || currentState == BattleState.CHAR1TURN)
            {
                battleQueue.Enqueue(BattleState.CHAR1TURN);
            }
            if (state == BattleState.CHAR2TURN || currentState == BattleState.CHAR2TURN)
            {
                battleQueue.Enqueue(BattleState.CHAR2TURN);

            }
            if (state == BattleState.CHAR3TURN || currentState == BattleState.CHAR3TURN)
            {
                battleQueue.Enqueue(BattleState.CHAR3TURN);
            }

            DeactivateAttackButtons();
            DeactivateTargetSprite();

        }
    }
    public void DisplayAvailableCharBattle()
    {
        if (state == BattleState.CHAR1TURN)
        {
            for (int i = 0; i < availableChar1Buttons.Length; i++)
            {
                availableChar1Buttons[i].SetActive(true);
            }
            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().characterName == "Grieve" ||
                Engine.e.activeParty.activeParty[1].GetComponent<Character>().characterName == "Grieve" ||
                Engine.e.activeParty.activeParty[2].GetComponent<Character>().characterName == "Grieve")
            {
                availableChar1Buttons[0].SetActive(false);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(GetComponent<BattleMenuControllerNav>().char1AvailSwitchGrieve);
            }

            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().characterName == "Mac" ||
           Engine.e.activeParty.activeParty[1].GetComponent<Character>().characterName == "Mac" ||
           Engine.e.activeParty.activeParty[2].GetComponent<Character>().characterName == "Mac")
            {
                availableChar1Buttons[1].SetActive(false);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(GetComponent<BattleMenuControllerNav>().char1AvailSwitchMac);
            }

            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().characterName == "Field" ||
          Engine.e.activeParty.activeParty[1].GetComponent<Character>().characterName == "Field" ||
          Engine.e.activeParty.activeParty[2].GetComponent<Character>().characterName == "Field")
            {
                availableChar1Buttons[2].SetActive(false);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(GetComponent<BattleMenuControllerNav>().char1AvailSwitchField);
            }

            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().characterName == "Riggs" ||
          Engine.e.activeParty.activeParty[1].GetComponent<Character>().characterName == "Riggs" ||
          Engine.e.activeParty.activeParty[2].GetComponent<Character>().characterName == "Riggs")
            {
                availableChar1Buttons[3].SetActive(false);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(GetComponent<BattleMenuControllerNav>().char1AvailSwitchRiggs);
            }
        }

        if (state == BattleState.CHAR2TURN)
        {
            for (int i = 0; i < availableChar2Buttons.Length; i++)
            {
                availableChar2Buttons[i].SetActive(true);
            }
            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().characterName == "Grieve" ||
                Engine.e.activeParty.activeParty[1].GetComponent<Character>().characterName == "Grieve" ||
                Engine.e.activeParty.activeParty[2].GetComponent<Character>().characterName == "Grieve")
            {
                availableChar2Buttons[0].SetActive(false);
            }

            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().characterName == "Mac" ||
           Engine.e.activeParty.activeParty[1].GetComponent<Character>().characterName == "Mac" ||
           Engine.e.activeParty.activeParty[2].GetComponent<Character>().characterName == "Mac")
            {
                availableChar2Buttons[1].SetActive(false);
            }

            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().characterName == "Field" ||
          Engine.e.activeParty.activeParty[1].GetComponent<Character>().characterName == "Field" ||
          Engine.e.activeParty.activeParty[2].GetComponent<Character>().characterName == "Field")
            {
                availableChar2Buttons[2].SetActive(false);
            }

            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().characterName == "Riggs" ||
          Engine.e.activeParty.activeParty[1].GetComponent<Character>().characterName == "Riggs" ||
          Engine.e.activeParty.activeParty[2].GetComponent<Character>().characterName == "Riggs")
            {
                availableChar2Buttons[3].SetActive(false);
            }
        }

        if (state == BattleState.CHAR3TURN)
        {
            for (int i = 0; i < availableChar3Buttons.Length; i++)
            {
                availableChar3Buttons[i].SetActive(true);
            }

            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().characterName == "Grieve" ||
                Engine.e.activeParty.activeParty[1].GetComponent<Character>().characterName == "Grieve" ||
                Engine.e.activeParty.activeParty[2].GetComponent<Character>().characterName == "Grieve")
            {
                availableChar3Buttons[0].SetActive(false);
            }

            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().characterName == "Mac" ||
           Engine.e.activeParty.activeParty[1].GetComponent<Character>().characterName == "Mac" ||
           Engine.e.activeParty.activeParty[2].GetComponent<Character>().characterName == "Mac")
            {
                availableChar3Buttons[1].SetActive(false);
            }

            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().characterName == "Field" ||
          Engine.e.activeParty.activeParty[1].GetComponent<Character>().characterName == "Field" ||
          Engine.e.activeParty.activeParty[2].GetComponent<Character>().characterName == "Field")
            {
                availableChar3Buttons[2].SetActive(false);
            }

            if (Engine.e.activeParty.activeParty[0].GetComponent<Character>().characterName == "Riggs" ||
          Engine.e.activeParty.activeParty[1].GetComponent<Character>().characterName == "Riggs" ||
          Engine.e.activeParty.activeParty[2].GetComponent<Character>().characterName == "Riggs")
            {
                availableChar3Buttons[3].SetActive(false);
            }
        }
    }


    public void SwitchCharButton(int index)
    {
        StartCoroutine(CharSwitch(index));
    }


    public void SkillSupportAlly1Button()
    {
        Engine.e.charBeingTargeted = 0;
    }

    public void SkillSupportAlly2Button()
    {
        Engine.e.charBeingTargeted = 1;
    }

    public void SkillSupportAlly3Button()
    {
        Engine.e.charBeingTargeted = 2;
    }

    public void ActivateChar1MenuButtons()
    {
        if (!char1MenuButtonsActive)
        {
            for (int i = 0; i < char1MenuButtons.Length; i++)
            {
                char1MenuButtons[i].SetActive(true);
                if (battleSwitchButtons == false)
                {
                    char1MenuButtons[2].SetActive(false);
                }
            }
        }
    }
    public void ActivateChar2MenuButtons()
    {
        if (!char2MenuButtonsActive)
        {
            for (int i = 0; i < char2MenuButtons.Length; i++)
            {
                char2MenuButtons[i].SetActive(true);
                if (battleSwitchButtons == false)
                {
                    char2MenuButtons[2].SetActive(false);
                }
            }
        }
    }

    public void ActivateChar3MenuButtons()
    {
        if (!char3MenuButtonsActive)
        {
            for (int i = 0; i < char3MenuButtons.Length; i++)
            {
                char3MenuButtons[i].SetActive(true);
                if (battleSwitchButtons == false)
                {
                    char3MenuButtons[2].SetActive(false);
                }
            }
        }
    }

    public void DeactivateChar1MenuButtons()
    {
        for (int i = 0; i < char1MenuButtons.Length; i++)
        {
            char1MenuButtons[i].SetActive(false);
        }
    }
    public void DeactivateChar2MenuButtons()
    {
        for (int i = 0; i < char2MenuButtons.Length; i++)
        {
            char2MenuButtons[i].SetActive(false);
        }
    }
    public void DeactivateChar3MenuButtons()
    {
        for (int i = 0; i < char3MenuButtons.Length; i++)
        {
            char3MenuButtons[i].SetActive(false);
        }
    }
    public void DeactivateChar1SwitchButtons()
    {
        for (int i = 0; i < availableChar1Buttons.Length; i++)
        {
            availableChar1Buttons[i].SetActive(false);
        }
    }
    public void DeactivateChar2SwitchButtons()
    {
        for (int i = 0; i < availableChar2Buttons.Length; i++)
        {
            availableChar2Buttons[i].SetActive(false);
        }
    }
    public void DeactivateChar3SwitchButtons()
    {
        for (int i = 0; i < availableChar3Buttons.Length; i++)
        {
            availableChar3Buttons[i].SetActive(false);
        }
    }

    public void ActivateAttackButtons()
    {

        //  if (!charMoving && !dropExists && !enemyMoving)
        //{
        inBattleMenu = true;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                targetButtons[i].SetActive(true);
            }
        }


        GetComponent<BattleMenuControllerNav>().OpenAttackFirstEnemy();

        DeactivateDropsUI();
        DeactivateSkillsUI();
        DeactivateChar1MenuButtons();
        DeactivateChar2MenuButtons();
        DeactivateChar3MenuButtons();

        if (state == BattleState.CHAR1TURN)
        {
            returnToPanelButton[0].SetActive(true);
        }
        if (state == BattleState.CHAR2TURN)
        {
            returnToPanelButton[1].SetActive(true);
        }
        if (state == BattleState.CHAR3TURN)
        {
            returnToPanelButton[2].SetActive(true);
        }
        // }
        // else
        // {
        //     return;
        // }
    }

    public void ActivateSkillTargetButtons()
    {
        if (lastSkillChoice.dps)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                    skillEnemyTargetButtons[i].SetActive(true);

            }
        }

        if (lastSkillChoice.targetSupport)
        {
            {
                for (int i = 0; i < activeParty.activeParty.Length; i++)
                {
                    if (activeParty.activeParty[i] != null)
                        skillAllyTargetButtons[i].SetActive(true);
                }
            }
        }

        GetComponent<BattleMenuControllerNav>().OpenSkillTarget();

        if (lastSkillChoice.selfSupport)

        {
            CharSkills(lastSkillChoice);

        }


        if (state == BattleState.CHAR1TURN)
        {
            DeactivateDropsUI();
            DeactivateChar1MenuButtons();
            if (!lastSkillChoice.selfSupport)
            {
                returnToPanelButton[0].SetActive(true);
            }
        }
        if (state == BattleState.CHAR2TURN)
        {
            DeactivateDropsUI();
            DeactivateChar2MenuButtons();
            if (!lastSkillChoice.selfSupport)
            {
                returnToPanelButton[1].SetActive(true);
            }
        }
        if (state == BattleState.CHAR3TURN)
        {
            DeactivateDropsUI();
            DeactivateChar3MenuButtons();
            if (!lastSkillChoice.selfSupport)
            {
                returnToPanelButton[2].SetActive(true);
            }
        }
    }

    public void DeactivateSkillTargetButtons()
    {
        if (lastSkillChoice != null)
        {
            for (int i = 0; i < skillEnemyTargetButtons.Length; i++)
            {
                if (lastSkillChoice.dps)
                {
                    if (enemies[i] != null)
                    {
                        skillEnemyTargetButtons[i].SetActive(false);
                    }
                }
            }

            for (int i = 0; i < skillAllyTargetButtons.Length; i++)
            {
                if (lastSkillChoice.targetSupport)
                {
                    if (skillEnemyTargetButtons != null)
                    {
                        skillAllyTargetButtons[i].SetActive(false);
                    }
                }

                if (lastSkillChoice.selfSupport)
                {
                    if (activeParty.activeParty[i] != null)
                    {
                        returnToPanelButton[i].SetActive(false);
                    }
                }
            }
        }
    }
    public void ActivateAllyTargetSprite(int index)
    {
        if (index == 0)
        {
            Vector3 position = new Vector3(Engine.e.activeParty.transform.position.x - 0.5f, Engine.e.activeParty.transform.position.y, Engine.e.activeParty.transform.position.z);
            spriteTarget.transform.position = position;
        }
        if (index == 1)
        {
            Vector3 position = new Vector3(Engine.e.activePartyMember2.transform.position.x - 0.5f, Engine.e.activePartyMember2.transform.position.y, Engine.e.activePartyMember2.transform.position.z);
            spriteTarget.transform.position = position;
        }
        if (index == 2)
        {
            Vector3 position = new Vector3(Engine.e.activePartyMember3.transform.position.x - 0.5f, Engine.e.activePartyMember3.transform.position.y, Engine.e.activePartyMember3.transform.position.z);
            spriteTarget.transform.position = position;
        }

        spriteTarget.SetActive(true);

    }
    public void ActivateEnemyTargetSprite(int index)
    {
        Vector3 position = new Vector3(enemies[index].transform.position.x - 0.5f, enemies[index].transform.position.y, enemies[index].transform.position.z);
        spriteTarget.transform.position = position;
        spriteTarget.SetActive(true);

    }

    public void DeactivateTargetSprite()
    {
        spriteTarget.SetActive(false);
    }


    public void RestartTurn()
    {
        if (state == BattleState.CHAR1TURN)
        {
            Char1Turn();
        }
        if (state == BattleState.CHAR2TURN)
        {
            Char2Turn();
        }
        if (state == BattleState.CHAR3TURN)
        {
            Char3Turn();
        }
    }

    public void ReturnToCharPanel()
    {
        inBattleMenu = false;
        DeactivateAttackButtons();
        DeactivateBattleItems();
        DeactivateSkillTargetButtons();
        physicalAttack = false;
        dropAttack = false;
        skillPhysicalAttack = false;
        skillRangedAttack = false;
        skillTargetSupport = false;
        skillSelfSupport = false;
        usingItem = false;

        if (char1ATB >= 100)
        {
            ActivateChar1MenuButtons();
        }
        if (char2ATB >= 100)
        {
            ActivateChar2MenuButtons();
        }
        if (char3ATB >= 100)
        {
            ActivateChar3MenuButtons();
        }

        if (state == BattleState.CHAR1TURN)
        {
            Char1Turn();
        }
        if (state == BattleState.CHAR2TURN)
        {
            Char2Turn();
        }
        if (state == BattleState.CHAR3TURN)
        {
            Char3Turn();
        }
    }

    public void DeactivateAttackButtons()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                targetButtons[i].SetActive(false);
            }
        }

        for (int i = 0; i < Engine.e.activeParty.activeParty.Length; i++)
        {
            if (Engine.e.activeParty.activeParty[i] != null)
            {
                returnToPanelButton[i].SetActive(false);
            }
        }
    }

    public void DisplayBattleItems()
    {
        battleItemMenu.SetActive(true);
        Engine.e.partyInventoryReference.OpenInventoryMenu();
        if (state == BattleState.CHAR1TURN)
        {
            DeactivateChar1MenuButtons();
        }
        if (state == BattleState.CHAR2TURN)
        {
            DeactivateChar2MenuButtons();
        }
        if (state == BattleState.CHAR3TURN)
        {
            DeactivateChar3MenuButtons();
        }
    }

    public void DeactivateBattleItems()
    {
        battleItemMenu.SetActive(false);
        usingItem = false;
        inBattleMenu = false;
        DeactivateSupportButtons();

        if (state == BattleState.CHAR1TURN)
        {
            ActivateChar1MenuButtons();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(char1MenuButtons[0]);
        }
        if (state == BattleState.CHAR2TURN)
        {
            ActivateChar2MenuButtons();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(char2MenuButtons[0]);
        }
        if (state == BattleState.CHAR3TURN)
        {
            ActivateChar3MenuButtons();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(char3MenuButtons[0]);
        }
    }

    public void ActivateSupportButtons()
    {
        Engine.e.enemyPanel.SetActive(true);

        dropsListReference.SetActive(false);
        Engine.e.char1BattlePanel.SetActive(true);
        if (Engine.e.activeParty.activeParty[1] != null)
        {
            Engine.e.char2BattlePanel.SetActive(true);
        }
        if (Engine.e.activeParty.activeParty[2] != null)
        {
            Engine.e.char3BattlePanel.SetActive(true);
        }

        for (int i = 0; i < activeParty.activeParty.Length; i++)
        {
            if (activeParty.activeParty[i] != null)
                allyTargetButtons[i].SetActive(true);
        }
        GetComponent<BattleMenuControllerNav>().OpenDropSupportTarget();

    }
    public void DeactivateSupportButtons()
    {

        for (int i = 0; i < activeParty.activeParty.Length; i++)
        {
            if (activeParty.activeParty[i] != null)
                allyTargetButtons[i].SetActive(false);
        }

    }

    public void ActivateEnemyUI()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                enemyUI[i].SetActive(false);
            }
        }
    }
    public void DeactivateEnemyUI()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyUI[i].SetActive(true);
        }
    }

    public void ActivateCharSkillsUI()
    {
        int index = 0;

        if (state == BattleState.CHAR1TURN)
        {
            index = 0;
        }
        if (state == BattleState.CHAR2TURN)
        {
            index = 1;
        }
        if (state == BattleState.CHAR3TURN)
        {
            index = 2;
        }

        if (Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Character>().characterName == "Grieve")
        {
            Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Grieve>().ActivateGrieveSkillsUI();
        }
        if (Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Character>().characterName == "Mac")
        {
            Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Mac>().ActivateMacSkillsUI();
        }
        if (Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Character>().characterName == "Field")
        {
            Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Field>().ActivateFieldSkillsUI();
        }
        if (Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Character>().characterName == "Riggs")
        {
            Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Riggs>().ActivateRiggsSkillsUI();
        }
    }

    public void DeactivateSkillsUI()
    {
        Engine.e.char1BattlePanel.SetActive(true);

        if (Engine.e.activeParty.activeParty[1] != null)
        {
            {
                Engine.e.char2BattlePanel.SetActive(true);
            }
        }
        if (Engine.e.activeParty.activeParty[2] != null)
        {
            Engine.e.char3BattlePanel.SetActive(true);
        }
        skillListReference.SetActive(false);
        for (int i = 0; i < Engine.e.gameSkills.Length; i++)
        {
            skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "-";
        }

    }

    public void ActivateCharDropsUI()
    {
        int index = 0;

        if (state == BattleState.CHAR1TURN)
        {
            index = 0;
        }
        if (state == BattleState.CHAR2TURN)
        {
            index = 1;
        }
        if (state == BattleState.CHAR3TURN)
        {
            index = 2;
        }

        if (Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Character>().characterName == "Grieve")
        {
            Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Grieve>().ActivateGrieveDropsUI();
        }
        if (Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Character>().characterName == "Mac")
        {
            Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Mac>().ActivateMacDropsUI();
        }
        if (Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Character>().characterName == "Field")
        {
            Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Field>().ActivateFieldDropsUI();
        }
        if (Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Character>().characterName == "Riggs")
        {
            Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Riggs>().ActivateRiggsDropsUI();
        }
    }

    public void DeactivateDropsUI()
    {
        dropsListReference.SetActive(false);

        for (int i = 0; i < dropsButtons.Length; i++)
        {
            dropsButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "-";
        }
    }


    public void DropChoice(Drops dropChoice)
    {
        int index = 0;

        if (state == BattleState.CHAR1TURN)
        {
            index = 0;
        }
        if (state == BattleState.CHAR2TURN)
        {
            index = 1;
        }
        if (state == BattleState.CHAR3TURN)
        {
            index = 2;
        }


        if (dropChoice.dropType == "Fire" && Engine.e.activeParty.activeParty[index].GetComponent<Character>().fireDrops[dropChoice.dropIndex] != null
        || dropChoice.dropType == "Ice" && Engine.e.activeParty.activeParty[index].GetComponent<Character>().iceDrops[dropChoice.dropIndex] != null
        || dropChoice.dropType == "Lightning" && Engine.e.activeParty.activeParty[index].GetComponent<Character>().lightningDrops[dropChoice.dropIndex] != null
        || dropChoice.dropType == "Water" && Engine.e.activeParty.activeParty[index].GetComponent<Character>().waterDrops[dropChoice.dropIndex] != null
        || dropChoice.dropType == "Shadow" && Engine.e.activeParty.activeParty[index].GetComponent<Character>().shadowDrops[dropChoice.dropIndex] != null
        || dropChoice.dropType == "Holy" && Engine.e.activeParty.activeParty[index].GetComponent<Character>().holyDrops[dropChoice.dropIndex] != null)
        {
            if (state != BattleState.ENEMY1TURN && state != BattleState.ENEMY2TURN
            && state != BattleState.ENEMY3TURN && state != BattleState.ENEMY4TURN)
            {
                if (Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Character>().currentMana >= dropChoice.dropCost)
                {
                    Engine.e.enemyPanel.SetActive(true);

                    Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Character>().UseDrop(dropChoice);
                    lastDropChoice = dropChoice;

                    if (index == 0)
                    {
                        DeactivateChar1MenuButtons();
                    }
                    if (index == 1)
                    {
                        DeactivateChar2MenuButtons();
                    }
                    if (index == 2)
                    {
                        DeactivateChar3MenuButtons();
                    }
                    DeactivateDropsUI();
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }

    public void SkillChoice(int tier)
    {
        int index = 0;

        if (state == BattleState.CHAR1TURN)
        {
            index = 0;
        }
        if (state == BattleState.CHAR2TURN)
        {
            index = 1;
        }
        if (state == BattleState.CHAR3TURN)
        {
            index = 2;
        }


        if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().skills[tier] != null)
        {
            if (state != BattleState.ENEMY1TURN && state != BattleState.ENEMY2TURN
            && state != BattleState.ENEMY3TURN && state != BattleState.ENEMY4TURN)

            {
                if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().currentEnergy >= Engine.e.gameSkills[tier].skillCost)
                {

                    lastSkillChoice = Engine.e.gameSkills[tier];
                    skillTierChoice = tier;

                    if (index == 0)
                    {
                        DeactivateChar1MenuButtons();
                        char1SkillChoice = lastSkillChoice;
                    }
                    if (index == 1)
                    {
                        DeactivateChar2MenuButtons();
                        char2SkillChoice = lastSkillChoice;

                    }
                    if (index == 2)
                    {
                        DeactivateChar3MenuButtons();
                        char3SkillChoice = lastSkillChoice;

                    }


                    DeactivateDropsUI();
                    DeactivateSkillsUI();
                    ActivateSkillTargetButtons();

                }
                else
                    return;

            }
            else
            {
                return;

            }
        }
        else
        {
            return;
        }
    }

    // For non-ATB battlesystem
    public IEnumerator CheckNext()
    {

        yield return new WaitForSeconds(0.1f);
        attackingTeam = false;
        charUsingSkill = false;
        GameObject character = null;

        if (state == BattleState.CHAR1TURN)
        {
            currentIndex = 0;
            nextIndex = 1;
            partyCheckNext = true;
            character = Engine.e.activeParty.gameObject;
        }

        if (state == BattleState.CHAR2TURN)
        {
            currentIndex = 1;
            nextIndex = 2;
            partyCheckNext = true;
            character = Engine.e.activePartyMember2.gameObject;
        }

        if (state == BattleState.CHAR3TURN)
        {
            currentIndex = 2;
            nextIndex = 0;
            partyCheckNext = true;
            character = Engine.e.activePartyMember3.gameObject;
        }
        if (state == BattleState.ENEMY1TURN)
        {
            currentIndex = 0;
            nextIndex = 1;
            partyCheckNext = false;

        }
        if (state == BattleState.ENEMY2TURN)
        {
            currentIndex = 1;
            nextIndex = 2;
            partyCheckNext = false;

        }
        if (state == BattleState.ENEMY3TURN)
        {
            currentIndex = 2;
            nextIndex = 3;
            partyCheckNext = false;

        }
        if (state == BattleState.ENEMY4TURN)
        {
            currentIndex = 3;
            nextIndex = 0;
            partyCheckNext = false;

        }

        if (!isDead)
        {
            if (partyCheckNext)
            {
                if (activeParty.activeParty[currentIndex].GetComponent<Character>().isConfused)
                {
                    activeParty.activeParty[currentIndex].GetComponent<Character>().confuseTimer++;
                    if (activeParty.activeParty[currentIndex].GetComponent<Character>().confuseTimer == 3)
                    {
                        activeParty.activeParty[currentIndex].GetComponent<Character>().isConfused = false;
                        activeParty.activeParty[currentIndex].GetComponent<Character>().confuseTimer = 0;
                    }
                }
                if (activeParty.activeParty[currentIndex].GetComponent<Character>().isPoisoned)
                {
                    GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, character.transform.position, Quaternion.identity);

                    isDead = Engine.e.TakePoisonDamage(currentIndex, activeParty.activeParty[currentIndex].GetComponent<Character>().poisonDmg);

                    dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = activeParty.activeParty[currentIndex].GetComponent<Character>().poisonDmg.ToString();
                    Destroy(dmgPopup, 1f);
                }

                if (activeParty.activeParty[currentIndex].GetComponent<Character>().deathInflicted)
                {
                    isDead = activeParty.activeParty[currentIndex].GetComponent<Character>().TakeDeathDamage(currentIndex);
                }
            }
            else
            {
                if (enemies[currentIndex].GetComponent<Enemy>().isConfused)
                {
                    enemies[currentIndex].GetComponent<Enemy>().confuseTimer++;
                    if (enemies[currentIndex].GetComponent<Enemy>().confuseTimer == 3)
                    {
                        enemies[currentIndex].GetComponent<Enemy>().isConfused = false;
                        enemies[currentIndex].GetComponent<Enemy>().confuseTimer = 0;
                    }
                }

                if (enemies[currentIndex].GetComponent<Enemy>().isPoisoned)
                {
                    enemies[currentIndex].GetComponent<Enemy>().TakePoisonDamage(currentIndex, enemies[currentIndex].GetComponent<Enemy>().poisonDmg);


                }

                if (enemies[currentIndex].GetComponent<Enemy>().deathInflicted)
                {
                    enemies[currentIndex].GetComponent<Enemy>().TakeDeathDamage();
                }
                if (enemies[currentIndex].gameObject.GetComponent<Enemy>().health <= 0)
                {
                    enemies[currentIndex].gameObject.GetComponent<Enemy>().health = 0;

                    isDead = EnemyGroup.enemyGroup.CheckEndBattle();

                    if (isDead)
                    {
                        state = BattleState.LEVELUPCHECK;
                        yield return new WaitForSeconds(2f);
                        StartCoroutine(LevelUpCheck());
                    }
                }
            }
        }

        if (partyCheckNext)
        {
            if (!isDead)
            {
                if (currentIndex != 2)
                {
                    if (Engine.e.activeParty.activeParty[nextIndex] != null)
                    {
                        if (Engine.e.activeParty.activeParty[nextIndex].gameObject.GetComponent<Character>().currentHealth > 0)
                        {
                            if (!Engine.e.activeParty.activeParty[nextIndex].gameObject.GetComponent<Character>().isAsleep)
                            {
                                if (currentIndex == 0)
                                {
                                    state = BattleState.CHAR2TURN;
                                    Char2Turn();
                                }
                                if (currentIndex == 1)
                                {
                                    state = BattleState.CHAR3TURN;
                                    Char3Turn();
                                }
                                partyCheckNext = false;
                            }
                            else
                            {
                                if (currentIndex == 0)
                                {
                                    int newIndex = Engine.e.GetNextRemainingPartyMember();

                                    if (activeParty.activeParty[newIndex] != null)
                                    {
                                        if (newIndex == 1)
                                        {
                                            state = BattleState.CHAR2TURN;
                                            Char2Turn();
                                        }
                                        if (newIndex == 2)
                                        {
                                            state = BattleState.CHAR3TURN;
                                            Char3Turn();
                                        }

                                        if (newIndex == 0)
                                        {
                                            int newEnemyIndex = enemyGroup.GetNextRemainingEnemy();

                                            if (newEnemyIndex == 0)
                                            {
                                                state = BattleState.ENEMY1TURN;
                                                Enemy1Turn();
                                            }
                                            if (newEnemyIndex == 1)
                                            {
                                                state = BattleState.ENEMY2TURN;
                                                Enemy2Turn();
                                            }
                                            if (newEnemyIndex == 2)
                                            {
                                                state = BattleState.ENEMY3TURN;
                                                Enemy3Turn();
                                            }
                                            if (newEnemyIndex == 3)
                                            {
                                                state = BattleState.ENEMY4TURN;
                                                Enemy4Turn();
                                            }
                                        }
                                    }
                                }

                                if (currentIndex == 1)
                                {
                                    if (!Engine.e.activeParty.activeParty[2].GetComponent<Character>().isAsleep)
                                    {
                                        state = BattleState.CHAR3TURN;
                                        Char3Turn();
                                    }
                                    else
                                    {
                                        int newIndex = enemyGroup.GetNextRemainingEnemy();

                                        if (newIndex == 0)
                                        {
                                            state = BattleState.ENEMY1TURN;
                                            Enemy1Turn();
                                        }
                                        if (newIndex == 1)
                                        {
                                            state = BattleState.ENEMY2TURN;
                                            Enemy2Turn();
                                        }
                                        if (newIndex == 2)
                                        {
                                            state = BattleState.ENEMY3TURN;
                                            Enemy3Turn();
                                        }
                                        if (newIndex == 3)
                                        {
                                            state = BattleState.ENEMY4TURN;
                                            Enemy4Turn();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            int newIndex = Engine.e.GetNextRemainingPartyMember();

                            if (newIndex == 0)
                            {
                                newIndex = enemyGroup.GetNextRemainingEnemy();

                                if (newIndex == 0)
                                {
                                    state = BattleState.ENEMY1TURN;
                                    Enemy1Turn();
                                }
                                if (newIndex == 1)
                                {
                                    state = BattleState.ENEMY2TURN;
                                    Enemy2Turn();
                                }
                                if (newIndex == 2)
                                {
                                    state = BattleState.ENEMY3TURN;
                                    Enemy3Turn();
                                }
                                if (newIndex == 3)
                                {
                                    state = BattleState.ENEMY4TURN;
                                    Enemy4Turn();
                                }
                            }

                            if (newIndex == 1)
                            {
                                state = BattleState.CHAR2TURN;
                                Char2Turn();
                            }
                            if (newIndex == 2)
                            {
                                state = BattleState.CHAR3TURN;
                                Char3Turn();
                            }
                        }
                        partyCheckNext = false;
                    }
                    else
                    {
                        int newIndex = enemyGroup.GetNextRemainingEnemy();

                        if (newIndex == 0)
                        {
                            state = BattleState.ENEMY1TURN;
                            Enemy1Turn();
                        }
                        if (newIndex == 1)
                        {
                            state = BattleState.ENEMY2TURN;
                            Enemy2Turn();
                        }

                        if (newIndex == 2)
                        {

                            state = BattleState.ENEMY3TURN;
                            Enemy3Turn();
                        }

                        if (newIndex == 3)
                        {

                            state = BattleState.ENEMY4TURN;
                            Enemy4Turn();
                        }
                    }
                    partyCheckNext = false;
                }

                if (currentIndex == 2)
                {
                    if (enemies[nextIndex].gameObject.GetComponent<Enemy>().health > 0 && !enemies[nextIndex].GetComponent<Enemy>().isAsleep)
                    {
                        state = BattleState.ENEMY1TURN;
                        Enemy1Turn();
                        partyCheckNext = false;
                    }
                    else
                    {
                        int newIndex = enemyGroup.GetNextRemainingEnemy();

                        if (newIndex == 1)
                        {
                            state = BattleState.ENEMY2TURN;
                            Enemy2Turn();
                        }

                        if (newIndex == 2)
                        {

                            state = BattleState.ENEMY3TURN;
                            Enemy3Turn();
                        }

                        if (newIndex == 3)
                        {

                            state = BattleState.ENEMY4TURN;
                            Enemy4Turn();
                        }
                    }
                    partyCheckNext = false;
                }
            }
            else

            {
                state = BattleState.LOST;
                StartCoroutine(EndBattle());
            }
        }
        else
        {
            if (!isDead)
            {
                if (currentIndex != 3)
                {
                    if (enemies[nextIndex] != null)
                    {
                        if (enemies[nextIndex].gameObject.GetComponent<Enemy>().health > 0)
                        {
                            if (!enemies[nextIndex].gameObject.GetComponent<Enemy>().isAsleep)
                            {
                                if (currentIndex == 0)
                                {
                                    state = BattleState.ENEMY2TURN;
                                    Enemy2Turn();
                                }
                                if (currentIndex == 1)
                                {
                                    state = BattleState.ENEMY3TURN;
                                    Enemy3Turn();
                                }
                                if (currentIndex == 2)
                                {
                                    state = BattleState.ENEMY4TURN;
                                    Enemy4Turn();
                                }
                            }
                            else
                            {
                                int newIndex = enemyGroup.GetNextRemainingEnemy();
                                Debug.Log("New Index: " + newIndex);


                                if (currentIndex == 0)
                                {
                                    if (newIndex == 0)
                                    {
                                        int newCharacterIndex = Engine.e.GetNextRemainingPartyMember();

                                        if (newCharacterIndex == 0)
                                        {
                                            state = BattleState.CHAR1TURN;
                                            Char1Turn();
                                        }

                                        if (newCharacterIndex == 1)
                                        {

                                            state = BattleState.CHAR2TURN;
                                            Char2Turn();
                                        }

                                        if (newCharacterIndex == 2)
                                        {
                                            state = BattleState.CHAR3TURN;
                                            Char3Turn();
                                        }
                                    }

                                    if (newIndex == 1)
                                    {
                                        state = BattleState.ENEMY2TURN;
                                        Enemy2Turn();
                                    }
                                    if (newIndex == 2)
                                    {
                                        state = BattleState.ENEMY3TURN;
                                        Enemy3Turn();
                                    }
                                    if (newIndex == 3)
                                    {
                                        state = BattleState.ENEMY4TURN;
                                        Enemy4Turn();
                                    }
                                }

                                if (currentIndex == 1)
                                {

                                    if (newIndex == 0 || newIndex == 1)
                                    {
                                        int newCharacterIndex = Engine.e.GetNextRemainingPartyMember();

                                        if (newCharacterIndex == 0)
                                        {
                                            state = BattleState.CHAR1TURN;
                                            Char1Turn();
                                        }

                                        if (newCharacterIndex == 1)
                                        {

                                            state = BattleState.CHAR2TURN;
                                            Char2Turn();
                                        }

                                        if (newCharacterIndex == 2)
                                        {
                                            state = BattleState.CHAR3TURN;
                                            Char3Turn();
                                        }
                                    }

                                    if (newIndex == 2)
                                    {
                                        state = BattleState.ENEMY3TURN;
                                        Enemy3Turn();
                                    }
                                    if (newIndex == 3)
                                    {
                                        state = BattleState.ENEMY4TURN;
                                        Enemy4Turn();
                                    }
                                }

                                if (currentIndex == 2)
                                {
                                    if (newIndex == 0 || newIndex == 1 || newIndex == 2)
                                    {
                                        int newCharacterIndex = Engine.e.GetNextRemainingPartyMember();

                                        if (newCharacterIndex == 0)
                                        {
                                            state = BattleState.CHAR1TURN;
                                            Char1Turn();
                                        }

                                        if (newCharacterIndex == 1)
                                        {

                                            state = BattleState.CHAR2TURN;
                                            Char2Turn();
                                        }

                                        if (newCharacterIndex == 2)
                                        {
                                            state = BattleState.CHAR3TURN;
                                            Char3Turn();
                                        }
                                    }
                                    if (newIndex == 3)
                                    {
                                        state = BattleState.ENEMY4TURN;
                                        Enemy4Turn();
                                    }
                                }

                                if (currentIndex == 3)
                                {

                                    int newCharacterIndex = Engine.e.GetNextRemainingPartyMember();

                                    if (newCharacterIndex == 0)
                                    {
                                        state = BattleState.CHAR1TURN;
                                        Char1Turn();
                                    }

                                    if (newCharacterIndex == 1)
                                    {

                                        state = BattleState.CHAR2TURN;
                                        Char2Turn();
                                    }

                                    if (newCharacterIndex == 2)
                                    {
                                        state = BattleState.CHAR3TURN;
                                        Char3Turn();
                                    }
                                }

                            }
                        }
                        else
                        {
                            int newIndex = enemyGroup.GetNextRemainingEnemy();
                            Debug.Log("New Index: " + newIndex);


                            if (currentIndex == 0)
                            {
                                if (newIndex == 0)
                                {
                                    int newCharacterIndex = Engine.e.GetNextRemainingPartyMember();

                                    if (newCharacterIndex == 0)
                                    {
                                        state = BattleState.CHAR1TURN;
                                        Char1Turn();
                                    }

                                    if (newCharacterIndex == 1)
                                    {

                                        state = BattleState.CHAR2TURN;
                                        Char2Turn();
                                    }

                                    if (newCharacterIndex == 2)
                                    {
                                        state = BattleState.CHAR3TURN;
                                        Char3Turn();
                                    }
                                }

                                if (newIndex == 1)
                                {
                                    state = BattleState.ENEMY2TURN;
                                    Enemy2Turn();
                                }
                                if (newIndex == 2)
                                {
                                    state = BattleState.ENEMY3TURN;
                                    Enemy3Turn();
                                }
                                if (newIndex == 3)
                                {
                                    state = BattleState.ENEMY4TURN;
                                    Enemy4Turn();
                                }
                            }

                            if (currentIndex == 1)
                            {

                                if (newIndex == 0 || newIndex == 1)
                                {
                                    int newCharacterIndex = Engine.e.GetNextRemainingPartyMember();

                                    if (newCharacterIndex == 0)
                                    {
                                        state = BattleState.CHAR1TURN;
                                        Char1Turn();
                                    }

                                    if (newCharacterIndex == 1)
                                    {

                                        state = BattleState.CHAR2TURN;
                                        Char2Turn();
                                    }

                                    if (newCharacterIndex == 2)
                                    {
                                        state = BattleState.CHAR3TURN;
                                        Char3Turn();
                                    }
                                }

                                if (newIndex == 2)
                                {
                                    state = BattleState.ENEMY3TURN;
                                    Enemy3Turn();
                                }
                                if (newIndex == 3)
                                {
                                    state = BattleState.ENEMY4TURN;
                                    Enemy4Turn();
                                }
                            }

                            if (currentIndex == 2)
                            {
                                if (newIndex == 0 || newIndex == 1 || newIndex == 2)
                                {
                                    int newCharacterIndex = Engine.e.GetNextRemainingPartyMember();

                                    if (newCharacterIndex == 0)
                                    {
                                        state = BattleState.CHAR1TURN;
                                        Char1Turn();
                                    }

                                    if (newCharacterIndex == 1)
                                    {

                                        state = BattleState.CHAR2TURN;
                                        Char2Turn();
                                    }

                                    if (newCharacterIndex == 2)
                                    {
                                        state = BattleState.CHAR3TURN;
                                        Char3Turn();
                                    }
                                }
                                if (newIndex == 3)
                                {
                                    state = BattleState.ENEMY4TURN;
                                    Enemy4Turn();
                                }
                            }

                            if (currentIndex == 3)
                            {

                                int newCharacterIndex = Engine.e.GetNextRemainingPartyMember();

                                if (newCharacterIndex == 0)
                                {
                                    state = BattleState.CHAR1TURN;
                                    Char1Turn();
                                }

                                if (newCharacterIndex == 1)
                                {

                                    state = BattleState.CHAR2TURN;
                                    Char2Turn();
                                }

                                if (newCharacterIndex == 2)
                                {
                                    state = BattleState.CHAR3TURN;
                                    Char3Turn();
                                }
                            }
                        }
                    }

                    else
                    {
                        int newIndex = Engine.e.GetNextRemainingPartyMember();

                        Debug.Log("NewIndex: " + newIndex);

                        if (!activeParty.activeParty[newIndex].GetComponent<Character>().isAsleep)
                        {
                            if (newIndex == 0)
                            {
                                state = BattleState.CHAR1TURN;
                                Char1Turn();
                            }
                            if (newIndex == 1)
                            {
                                state = BattleState.CHAR2TURN;
                                Char2Turn();
                            }
                            if (newIndex == 2)
                            {
                                state = BattleState.CHAR3TURN;
                                Char3Turn();
                            }
                        }
                        else
                        {
                            if (currentIndex == 0)
                            {
                                state = BattleState.ENEMY1TURN;
                                Enemy1Turn();
                            }
                            if (currentIndex == 1)
                            {
                                state = BattleState.ENEMY2TURN;
                                Enemy2Turn();
                            }
                            if (currentIndex == 2)
                            {
                                state = BattleState.ENEMY3TURN;
                                Enemy3Turn();
                            }
                            if (currentIndex == 3)
                            {
                                state = BattleState.ENEMY4TURN;
                                Enemy4Turn();
                            }
                        }
                    }
                    partyCheckNext = false;
                }

                if (currentIndex == 3)
                {
                    if (Engine.e.activeParty.activeParty[nextIndex].GetComponent<Character>().currentHealth > 0 && !Engine.e.activeParty.activeParty[nextIndex].GetComponent<Character>().isAsleep)
                    {
                        state = BattleState.CHAR1TURN;
                        Char1Turn();
                        partyCheckNext = false;
                    }
                    else
                    {
                        int newIndex = Engine.e.GetNextRemainingPartyMember();

                        if (newIndex == 1)
                        {
                            state = BattleState.CHAR2TURN;
                            Char2Turn();
                        }

                        if (newIndex == 2)
                        {

                            state = BattleState.CHAR3TURN;
                            Char3Turn();
                        }
                    }
                    partyCheckNext = false;
                }
            }
        }
    }

    public void SetPhysicalAttack()
    {
        if (!charMoving)
        {
            physicalAttack = true;
        }
        else
        {
            return;
        }
    }
    public void SetDropAttack(bool setter)
    {
        dropAttack = setter;

    }

    public void ResetPartyMemberStats()
    {

        Engine.e.activeParty.GetComponent<SpriteRenderer>().color = Color.white;
        Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().color = Color.white;
        Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().color = Color.white;

        char1ATBGuage.value = 0;
        char2ATBGuage.value = 0;
        char3ATBGuage.value = 0;

        enemy1ATBGuage.value = 0;
        enemy2ATBGuage.value = 0;
        enemy3ATBGuage.value = 0;
        enemy4ATBGuage.value = 0;

        char1ATB = 0;
        char2ATB = 0;
        char3ATB = 0;
        enemy1ATB = 0;
        enemy2ATB = 0;
        enemy3ATB = 0;
        enemy4ATB = 0;

        if (grieveStatBoost)
            if (Engine.e.party[0] != null)
            {
                Engine.e.party[0].GetComponent<Grieve>().physicalDamage -= grievePhysicalBoost;
                grievePhysicalBoost = 0;
                Engine.e.party[0].GetComponent<Grieve>().currentHealth -= grieveHealthBoost;
                grieveHealthBoost = 0;
                Engine.e.party[0].GetComponent<Grieve>().currentMana -= grieveManaBoost;
                grieveManaBoost = 0;
                Engine.e.party[0].GetComponent<Grieve>().currentEnergy -= grieveEnergyBoost;
                grieveEnergyBoost = 0;
                Engine.e.party[0].GetComponent<Grieve>().dodgeChance -= grieveDodgeBoost;
                grieveDodgeBoost = 0;
            }

        if (macStatBoost)
            if (Engine.e.party[1] != null)
            {
                Engine.e.party[1].GetComponent<Mac>().physicalDamage -= macPhysicalBoost;
                macPhysicalBoost = 0;
                Engine.e.party[1].GetComponent<Mac>().currentHealth -= macHealthBoost;
                macHealthBoost = 0;
                Engine.e.party[1].GetComponent<Mac>().currentMana -= macManaBoost;
                macManaBoost = 0;
                Engine.e.party[1].GetComponent<Mac>().currentEnergy -= macEnergyBoost;
                macEnergyBoost = 0;
                Engine.e.party[1].GetComponent<Mac>().dodgeChance -= macDodgeBoost;
                macDodgeBoost = 0;
            }

        if (fieldStatBoost)
            if (Engine.e.party[2] != null)
            {
                Engine.e.party[2].GetComponent<Field>().physicalDamage -= fieldPhysicalBoost;
                fieldPhysicalBoost = 0;
                Engine.e.party[2].GetComponent<Field>().currentHealth -= fieldHealthBoost;
                fieldHealthBoost = 0;
                Engine.e.party[2].GetComponent<Field>().currentMana -= fieldManaBoost;
                fieldManaBoost = 0;
                Engine.e.party[2].GetComponent<Field>().currentEnergy -= fieldEnergyBoost;
                fieldEnergyBoost = 0;
                Engine.e.party[2].GetComponent<Field>().dodgeChance -= fieldDodgeBoost;
                fieldDodgeBoost = 0;
            }

        if (riggsStatBoost)
            if (Engine.e.party[3] != null)
            {
                Engine.e.party[3].GetComponent<Riggs>().physicalDamage -= riggsPhysicalBoost;
                riggsPhysicalBoost = 0;
                Engine.e.party[3].GetComponent<Riggs>().currentHealth -= riggsHealthBoost;
                riggsHealthBoost = 0;
                Engine.e.party[3].GetComponent<Riggs>().currentMana -= riggsManaBoost;
                riggsManaBoost = 0;
                Engine.e.party[3].GetComponent<Riggs>().currentEnergy -= riggsEnergyBoost;
                riggsEnergyBoost = 0;
                Engine.e.party[3].GetComponent<Riggs>().dodgeChance -= riggsDodgeBoost;
                riggsDodgeBoost = 0;
            }

        for (int i = 0; i < Engine.e.party.Length; i++)
        {
            if (Engine.e.party[i] != null)
            {
                Engine.e.party[i].GetComponent<Character>().isPoisoned = false;
                Engine.e.party[i].GetComponent<Character>().isAsleep = false;
                Engine.e.party[i].GetComponent<Character>().isConfused = false;
                Engine.e.party[i].GetComponent<Character>().deathInflicted = false;
                Engine.e.party[i].GetComponent<Character>().poisonDmg = 0;
                Engine.e.party[i].GetComponent<Character>().sleepTimer = 0;
                Engine.e.party[i].GetComponent<Character>().confuseTimer = 0;
                Engine.e.party[i].GetComponent<Character>().deathTimer = 3;
                Destroy(Engine.e.party[i].GetComponent<Character>().deathTimerPopup);
            }
        }
    }

    private void ChangeCharState()
    {
        if (!inBattleMenu)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (state == BattleState.CHAR1TURN)
                {
                    if (activeParty.activeParty[2] != null)
                    {
                        if (char2Ready && char3Ready)
                        {
                            DeactivateSkillsUI();
                            DeactivateDropsUI();
                            Char2Turn();
                        }
                        else
                        {
                            if (!char2Ready && char3Ready)
                            {
                                DeactivateSkillsUI();
                                DeactivateDropsUI();
                                Char3Turn();
                            }
                        }
                    }
                    else
                    {
                        if (char2Ready)
                        {
                            DeactivateSkillsUI();
                            DeactivateDropsUI();
                            Char2Turn();
                        }
                    }
                }
                else
                {

                    if (state == BattleState.CHAR2TURN)
                    {
                        if (activeParty.activeParty[2] != null)
                        {
                            if (char3Ready)
                            {
                                DeactivateSkillsUI();
                                DeactivateDropsUI();
                                Char3Turn();
                            }
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                if (activeParty.activeParty[2] != null)
                {
                    if (state == BattleState.CHAR3TURN)
                    {
                        if (char2Ready && char1Ready)
                        {
                            DeactivateSkillsUI();
                            DeactivateDropsUI();
                            Char2Turn();
                        }
                        else
                        {
                            if (!char2Ready && char1Ready)
                            {
                                DeactivateSkillsUI();
                                DeactivateDropsUI();
                                Char1Turn();
                            }
                        }
                    }
                    else
                    {
                        if (char1Ready)
                        {
                            DeactivateSkillsUI();
                            DeactivateDropsUI();
                            Char1Turn();
                        }
                    }
                }
                else
                {

                    if (state == BattleState.CHAR2TURN)
                    {

                        if (char1Ready)
                        {
                            DeactivateSkillsUI();
                            DeactivateDropsUI();
                            Char1Turn();
                        }
                    }
                }
            }
        }
    }


    public void SetInBattleMenuTrue()
    {
        inBattleMenu = true;
    }

    public void SetInBattleMenuFalse()
    {
        inBattleMenu = false;
    }

    public void DisableBattleMenus()
    {
        Engine.e.enemyPanel.SetActive(true);
        inBattleMenu = false;

        DeactivateTargetSprite();
        DeactivateDropsUI();
        DeactivateSkillsUI();
        DeactivateBattleItems();
        DeactivateSupportButtons();
        DeactivateSkillTargetButtons();
        DeactivateAttackButtons();

    }

    private void Update()
    {
        ChangeCharState();

        if (Engine.e.battleModeActive)
        {
            if (isDead)
            {
                if (activeParty.activeParty[2] != null)
                {
                    if (activeParty.activeParty[0].GetComponent<Character>().currentHealth <= 0 && activeParty.activeParty[1].GetComponent<Character>().currentHealth <= 0
                    && activeParty.activeParty[2].GetComponent<Character>().currentHealth <= 0)
                    {
                        state = BattleState.LOST;
                    }
                    else
                    {
                        state = BattleState.LEVELUPCHECK;
                    }

                }
                else
                {
                    if (activeParty.activeParty[1] != null)
                    {
                        if (activeParty.activeParty[0].GetComponent<Character>().currentHealth <= 0 && activeParty.activeParty[1].GetComponent<Character>().currentHealth <= 0)
                        {
                            state = BattleState.LOST;
                        }
                        else
                        {
                            state = BattleState.LEVELUPCHECK;
                        }
                    }
                    else
                    {
                        if (activeParty.activeParty[0].GetComponent<Character>().currentHealth <= 0)
                        {
                            state = BattleState.LOST;
                        }
                        else
                        {
                            state = BattleState.LEVELUPCHECK;
                        }
                    }
                }
            }

            EndBattle();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (BattleState state in battleQueue)
            {
                Debug.Log(state);
            }
        }

        if (currentInQueue != BattleState.ENEMY1TURN && currentInQueue != BattleState.ENEMY2TURN
        && currentInQueue != BattleState.ENEMY3TURN && currentInQueue != BattleState.ENEMY4TURN
        && currentInQueue != BattleState.CONFCHAR1 && currentInQueue != BattleState.CONFCHAR2
        && currentInQueue != BattleState.CONFCHAR3)
        {

            currentState = state;
        }

        // Do Something Like This (to make sure stats are up-to-date)
        /*
        Engine.e.battleSystem.hud.displayHealth[0].text = Engine.e.activeParty.activeParty[0].GetComponent<Character>().currentHealth.ToString();
        Engine.e.battleSystem.hud.displayMana[0].text = Engine.e.activeParty.activeParty[0].GetComponent<Character>().currentMana.ToString();
        Engine.e.battleSystem.hud.displayEnergy[0].text = Engine.e.activeParty.activeParty[0].GetComponent<Character>().currentEnergy.ToString();

        if (Engine.e.party[1] != null)
        {
            Engine.e.battleSystem.hud.displayHealth[1].text = Engine.e.activeParty.activeParty[1].GetComponent<Character>().currentHealth.ToString();
            Engine.e.battleSystem.hud.displayMana[1].text = Engine.e.activeParty.activeParty[1].GetComponent<Character>().currentMana.ToString();
            Engine.e.battleSystem.hud.displayEnergy[1].text = Engine.e.activeParty.activeParty[1].GetComponent<Character>().currentEnergy.ToString();
        }
        if (Engine.e.party[2] != null)
        {
            Engine.e.battleSystem.hud.displayHealth[2].text = Engine.e.activeParty.activeParty[2].GetComponent<Character>().currentHealth.ToString();
            Engine.e.battleSystem.hud.displayMana[2].text = Engine.e.activeParty.activeParty[2].GetComponent<Character>().currentMana.ToString();
            Engine.e.battleSystem.hud.displayEnergy[2].text = Engine.e.activeParty.activeParty[2].GetComponent<Character>().currentEnergy.ToString();
        }*/

        if (!isDead)
        {
            if (!Engine.e.battleModeActive)
            {
                if (state == BattleState.ATBCHECK)
                {
                    dialogueText.text = string.Empty;
                    if (activeParty.activeParty[0].GetComponent<Character>().currentHealth > 0)
                    {
                        if (char1ATB < ATBReady)
                        {
                            //char1ATB += (activeParty.activeParty[0].GetComponent<Character>().haste * 10) / 50;
                            char1ATB += activeParty.activeParty[0].GetComponent<Character>().haste * Time.deltaTime;
                            char1ATBGuage.value = char1ATB;
                        }
                        else
                        {
                            state = BattleState.CHAR1TURN;
                            Char1Turn();
                        }
                    }

                    if (activeParty.activeParty[1] != null)
                    {
                        if (activeParty.activeParty[1].GetComponent<Character>().currentHealth > 0)
                        {
                            if (char2ATB < ATBReady)
                            {
                                //char2ATB += (activeParty.activeParty[1].GetComponent<Character>().haste * 10) / 50;

                                char2ATB += activeParty.activeParty[1].GetComponent<Character>().haste * Time.deltaTime;
                                char2ATBGuage.value = char2ATB;
                            }
                            else
                            {
                                state = BattleState.CHAR2TURN;
                                Char2Turn();
                            }
                        }
                    }

                    if (activeParty.activeParty[2] != null)
                    {
                        if (activeParty.activeParty[2].GetComponent<Character>().currentHealth > 0)
                        {
                            if (char3ATB < ATBReady)
                            {
                                char3ATB += activeParty.activeParty[2].GetComponent<Character>().haste * Time.deltaTime;
                                char3ATBGuage.value = char3ATB;
                            }
                            else
                            {
                                state = BattleState.CHAR3TURN;
                                Char3Turn();
                            }
                        }
                    }

                    if (enemies[0].GetComponent<Enemy>().health > 0)
                    {
                        if (enemy1ATB < ATBReady)
                        {
                            enemy1ATB += (enemies[0].GetComponent<Enemy>().haste * Time.deltaTime);
                            enemy1ATBGuage.value = enemy1ATB;
                        }
                        else
                        {
                            Enemy1Turn();
                        }
                    }

                    if (enemies[1] != null)
                    {
                        if (enemies[1].GetComponent<Enemy>().health > 0)
                        {
                            if (enemy2ATB < ATBReady)
                            {
                                enemy2ATB += (enemies[1].GetComponent<Enemy>().haste * Time.deltaTime);
                                enemy2ATBGuage.value = enemy2ATB;

                            }
                            else
                            {
                                Enemy2Turn();
                            }
                        }
                    }

                    if (enemies[2] != null)
                    {
                        if (enemies[2].GetComponent<Enemy>().health > 0)
                        {
                            if (enemy3ATB < ATBReady)
                            {
                                enemy3ATB += (enemies[2].GetComponent<Enemy>().haste * Time.deltaTime);
                                enemy3ATBGuage.value = enemy3ATB;
                            }
                            else
                            {
                                Enemy3Turn();
                            }
                        }
                    }

                    if (enemies[3] != null)
                    {
                        if (enemies[3].GetComponent<Enemy>().health > 0)
                        {
                            if (enemy4ATB < ATBReady)
                            {
                                enemy4ATB += (enemies[3].GetComponent<Enemy>().haste * Time.deltaTime);
                                enemy4ATBGuage.value = enemy4ATB;
                            }
                            else
                            {
                                Enemy4Turn();
                            }
                        }
                    }
                }
            }
            else
            {

                HandleQueue();



            }
        }


        if (Engine.e.partyInventoryReference.battleScreenInventorySet)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                PressDown();
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                ReleaseDown();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                PressUp();
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                ReleaseUp();
            }
            if (pressDown && !pressUp)
            {
                vertMove = 1;
            }
            if (!pressDown && pressUp)
            {
                vertMove = -1;
            }

            HandleInventory();
        }
    }

    void HandleInventory()
    {
        // "Battle Items Menu" Inventory
        if (Engine.e.partyInventoryReference.battleScreenInventorySet)
        {
            if (vertMove > 0 && pressDown)
            {
                pressDown = false;
                if (inventoryPointerIndex < Engine.e.battleSystem.battleItems.Length)
                {
                    inventoryPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(battleItems[inventoryPointerIndex].gameObject);

                    if (inventoryPointerIndex > 2 && inventoryPointerIndex < Engine.e.battleSystem.battleItems.Length)
                    {
                        Engine.e.partyInventoryReference.battleItemsRectTransform.offsetMax -= new Vector2(0, -30);
                    }
                }
            }

            if (vertMove < 0 && pressUp)
            {
                pressUp = false;
                if (inventoryPointerIndex > 0)
                {
                    inventoryPointerIndex += vertMove;

                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(battleItems[inventoryPointerIndex].gameObject);


                    if (inventoryPointerIndex >= 2 && inventoryPointerIndex > 0)
                    {
                        Engine.e.partyInventoryReference.battleItemsRectTransform.offsetMax -= new Vector2(0, 30);
                    }
                }
            }
        }
    }

    void PressDown()
    {
        pressDown = true;
        vertMove = 1;
    }
    void ReleaseDown()
    {
        pressDown = false;
        vertMove = 0;
    }
    void PressUp()
    {
        pressUp = true;
        vertMove = -1;
    }
    void ReleaseUp()
    {
        pressUp = false;
        vertMove = 0;
    }

    private void FixedUpdate()
    {

        if (charMoving == true)
        {

            if (currentInQueue == BattleState.CHAR1TURN || currentInQueue == BattleState.CONFCHAR1)
            {
                currentMoveIndex = 0;
            }
            if (currentInQueue == BattleState.CHAR2TURN || currentInQueue == BattleState.CONFCHAR2)
            {
                currentMoveIndex = 1;

            }
            if (currentInQueue == BattleState.CHAR3TURN || currentInQueue == BattleState.CONFCHAR3)
            {
                currentMoveIndex = 2;

            }

            if (!Engine.e.activeParty.activeParty[currentMoveIndex].GetComponent<Character>().isConfused)
            {
                StartCoroutine(AttackMoveToTargetChar());
            }
            else
            {
                if (!attackingTeam)
                {
                    StartCoroutine(AttackMoveToTargetChar());
                }
                else
                {
                    StartCoroutine(ConfuseAttackMoveToTargetChar());

                }
            }
        }

        if (charAttackDrop == true)
        {
            StartCoroutine(CharDropAttack());
        }

        if (enemyMoving == true)
        {

            if (currentInQueue == BattleState.ENEMY1TURN)
            {
                currentMoveIndex = 0;
            }
            if (currentInQueue == BattleState.ENEMY2TURN)
            {
                currentMoveIndex = 1;

            }
            if (currentInQueue == BattleState.ENEMY3TURN)
            {
                currentMoveIndex = 2;
            }
            if (currentInQueue == BattleState.ENEMY4TURN)
            {
                currentMoveIndex = 3;
            }

            if (!enemies[currentMoveIndex].GetComponent<Enemy>().isConfused)
            {
                StartCoroutine(AttackMoveToTargetEnemy());
            }
            else
            {
                if (!attackingTeam)
                {
                    StartCoroutine(AttackMoveToTargetEnemy());
                }
                else
                {
                    StartCoroutine(ConfuseAttackMoveToTargetEnemy());

                }
            }
        }

        if (enemyAttackDrop == true)
        {
            StartCoroutine(EnemyDropAttack());
        }
    }
}