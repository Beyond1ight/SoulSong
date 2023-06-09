using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Cinemachine;
using UnityEngine.SceneManagement;

public enum BattleState { START, CHAR1TURN, CHAR2TURN, CHAR3TURN, ENEMY1TURN, ENEMY2TURN, ENEMY3TURN, ENEMY4TURN, CONFCHAR1, CONFCHAR2, CONFCHAR3, ATBCHECK, QUEUECHECK, WON, LOST, LEVELUP, LEVELUPCHECK }
public enum AnimState { NONE, ATTACKANIM, ITEMANIM, DROPANIM, SKILLANIM, SWITCHANIM }
public class BattleSystem : MonoBehaviour
{

    // General Info
    public BattleState state, currentState, currentInQueue;
    public AnimState animState;
    public Queue<BattleState> battleQueue;
    public ActiveParty activeParty;
    public Enemy[] enemies;
    public EnemyGroup enemyGroup;
    public bool hpRestore, mpRestore;
    public bool physicalAttack = false;
    public bool skillPhysicalAttack = false, skillRangedAttack = false, skillTargetSupport = false, skillSelfSupport = false;
    public bool dropAttack, dropSupport = false;
    public bool usingItem = false;
    public float damageTotal;
    public float skillBoostTotal;
    public int char1Target, char2Target, char3Target;
    public bool char1Attacking, char1Supporting, char2Attacking, char2Supporting, char3Attacking, char3Supporting;
    public bool char1UsingItem, char2UsingItem, char3UsingItem;
    public bool char1DropAttack, char1SkillAttack, char2DropAttack, char2SkillAttack, char3DropAttack, char3SkillAttack,
    char1DropSupport, char2DropSupport, char3DropSupport;
    public bool char1TargetingTeam, char1TargetingEnemy, char2TargetingTeam, char2TargetingEnemy, char3TargetingTeam, char3TargetingEnemy;
    public Drops char1DropChoice, char2DropChoice, char3DropChoice;
    public Skills char1SkillChoice, char2SkillChoice, char3SkillChoice;
    public bool char1PhysicalAttack, char1SkillPhysicalAttack, char1SkillRangedAttack, char1SkillSelfSupport, char1SkillTargetSupport, char2PhysicalAttack, char2SkillPhysicalAttack, char2SkillRangedAttack,
    char2SkillSelfSupport, char2SkillTargetSupport, char3PhysicalAttack, char3SkillPhysicalAttack, char3SkillRangedAttack, char3SkillSelfSupport, char3SkillTargetSupport, charSkillSwitch, charSkillSwitchCheck,
    char1SkillSwitch, char2SkillSwitch, char3SkillSwitch;
    public bool char1ConfusedReady, char2ConfusedReady, char3ConfusedReady;
    public int enemy1AttackTarget, enemy2AttackTarget, enemy3AttackTarget, enemy4AttackTarget;
    public bool enemy1Ready, enemy2Ready, enemy3Ready, enemy4Ready;
    public int previousTargetReference;
    public bool char1Switching, char2Switching, char3Switching;
    public int char1SwitchToIndex, char2SwitchToIndex, char3SwitchToIndex;
    public Item char1ItemToBeUsed, char2ItemToBeUsed, char3ItemToBeUsed;
    public bool isDead;
    public float enemyGroupExperiencePoints;
    public bool animExists = false;
    public bool enemyMoving = false, charMoving = false;
    public Vector3 leaderPos, activeParty2Pos, activeParty3Pos;
    public GameObject char1BattlePanel, char1LevelUpPanel, char2BattlePanel, char2LevelUpPanel, char3BattlePanel, char3LevelUpPanel,
    enemyLootReference, enemyLootPanelReference, enemyPanel, enemyLootCountReference, char1Context, char2Context, char3Context, availableCharSwitchButtons;
    bool charAtBattlePos = true;
    bool charAttacking = false;
    bool enemyAtBattlePos = true;
    public bool enemyAttacking = false;
    public bool charAttackDrop = false, enemyAttackDrop = false;
    bool confuseAttack = false;
    public Drops lastDropChoice;
    public Skills lastSkillChoice;
    public bool charUsingSkill;
    public bool attackingTeam = false;
    public float char1ATB, char2ATB, char3ATB, enemy1ATB, enemy2ATB, enemy3ATB, enemy4ATB, ATBReady;
    public Slider char1ATBGuage, char2ATBGuage, char3ATBGuage, enemy1ATBGuage, enemy2ATBGuage, enemy3ATBGuage, enemy4ATBGuage;

    // UI References
    public bool inBattleMenu = false;

    public GameObject char1UI;
    public GameObject char2UI;
    public GameObject char3UI;

    public GameObject[] enemyUI;
    public GameObject[] allyTargetButtons, enemyTargetButtons;
    public TextMeshProUGUI dialogueText;

    public GameObject[] returnToPanelButton;
    public GameObject battleItemMenu;
    public BattleItemSlot[] battleItems;
    public GameObject[] char1MenuButtons;
    public GameObject[] char2MenuButtons;
    public GameObject[] char3MenuButtons;

    // Skills
    public GameObject[] skillButtons;

    // Drops
    public GameObject[] dropsButtons;

    public GameObject[] switchButtons;
    public GameObject skillListReference;
    public GameObject dropsListReference;


    // Misc Info/References
    public string[] charIndexName;
    public BattleAnimations battleAnimationsReference;
    int currentIndex;
    int nextIndex;
    public bool dodgedAttack = false;
    public bool battleSwitchButtons = false;
    public bool dontDisplayDmgPopup = false;
    public BattleHUD hud;
    int currentMoveIndex;
    public int enemyDropChoice;
    public GameObject[] damagePopup, targetSprite;
    public GameObject levelUpPopup, deathTimerPopup;
    public Material originalMaterial;
    public Material damageFlash;
    public bool failedItemUse = false;
    int skillTierChoice;
    bool checkingForRandomDrop = false;
    int randomDropIndex;
    public bool partyCheckNext, targetingTeam, targetingEnemy = false;
    List<Drops> charRandomDropList;
    public bool char1Ready = false, char2Ready = false, char3Ready = false;
    bool targetCheck = false;
    public GameObject characterDropTarget;
    bool confuseTargetCheck = false;
    bool char1MenuButtonsActive = false, char2MenuButtonsActive = false, char3MenuButtonsActive = false;
    bool pressUp, pressDown;
    public int inventoryPointerIndex = 0, vertMove = 0;
    public TextMeshProUGUI battleHelpReference;
    public GameObject[] currentAnimation;
    bool partyTurn = false;
    [SerializeField]
    public bool dmgText1Active, dmgText2Active, dmgText3Active, dmgText4Active, dmgText5Active, dmgText6Active, dmgText7Active, settingTarget;

    [SerializeField]
    float battleBodyTotal, menuTargetIndex;
    [SerializeField]
    float[] damageTextTimer;
    public float animationTimer = 0f;
    public bool displayDamageText, targetAll = false; // "targetAll" refers to all (sprites) on one side for targeting, i.e. all enemies or all active party members

    public IEnumerator SetupBattle()
    {
        Engine.e.activeParty.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Engine.e.activePartyMember2.GetComponent<BoxCollider2D>().enabled = false;
        Engine.e.activePartyMember3.GetComponent<BoxCollider2D>().enabled = false;

        charAttacking = false;
        enemyAttacking = false;
        enemyAtBattlePos = true;
        damageTotal = 0;
        isDead = false;
        enemyGroup.groupInBattle = true;
        confuseAttack = false;
        ATBReady = 100;
        char1ItemToBeUsed = null;
        char2ItemToBeUsed = null;
        char3ItemToBeUsed = null;
        settingTarget = false;
        targetingTeam = false;
        targetingEnemy = false;

        char1ATB += 80;
        char2ATB = 0;
        char3ATB = 0;
        enemy1ATB = 0;
        enemy2ATB = 0;
        enemy3ATB = 0;
        enemy4ATB = 0;

        lastDropChoice = null;
        lastSkillChoice = null;

        for (int i = 0; i < Engine.e.party.Length; i++)
        {
            if (Engine.e.party[i] != null)
            {
                Engine.e.party[i].GetComponent<Character>().maxHealthBase = Engine.e.party[i].GetComponent<Character>().maxHealth;
                Engine.e.party[i].GetComponent<Character>().maxManaBase = Engine.e.party[i].GetComponent<Character>().maxMana;
                Engine.e.party[i].GetComponent<Character>().maxEnergyBase = Engine.e.party[i].GetComponent<Character>().maxEnergy;
                Engine.e.party[i].GetComponent<Character>().strengthBase = Engine.e.party[i].GetComponent<Character>().strength;
                Engine.e.party[i].GetComponent<Character>().intelligenceBase = Engine.e.party[i].GetComponent<Character>().intelligence;
                Engine.e.party[i].GetComponent<Character>().dodgeChanceBase = Engine.e.party[i].GetComponent<Character>().dodgeChance;
                Engine.e.party[i].GetComponent<Character>().critChanceBase = Engine.e.party[i].GetComponent<Character>().critChance;
                Engine.e.party[i].GetComponent<Character>().hasteBase = Engine.e.party[i].GetComponent<Character>().haste;

                Engine.e.party[i].GetComponent<Character>().firePhysicalAttackBonusBase = Engine.e.party[i].GetComponent<Character>().firePhysicalAttackBonus;
                Engine.e.party[i].GetComponent<Character>().icePhysicalAttackBonusBase = Engine.e.party[i].GetComponent<Character>().icePhysicalAttackBonus;
                Engine.e.party[i].GetComponent<Character>().lightningPhysicalAttackBonusBase = Engine.e.party[i].GetComponent<Character>().lightningPhysicalAttackBonus;
                Engine.e.party[i].GetComponent<Character>().waterPhysicalAttackBonusBase = Engine.e.party[i].GetComponent<Character>().waterPhysicalAttackBonus;
                Engine.e.party[i].GetComponent<Character>().shadowPhysicalAttackBonusBase = Engine.e.party[i].GetComponent<Character>().shadowPhysicalAttackBonus;
                Engine.e.party[i].GetComponent<Character>().holyPhysicalAttackBonusBase = Engine.e.party[i].GetComponent<Character>().holyPhysicalAttackBonus;

                Engine.e.party[i].GetComponent<Character>().fireDefenseBase = Engine.e.party[i].GetComponent<Character>().fireDefense;
                Engine.e.party[i].GetComponent<Character>().iceDefenseBase = Engine.e.party[i].GetComponent<Character>().iceDefense;
                Engine.e.party[i].GetComponent<Character>().lightningDefenseBase = Engine.e.party[i].GetComponent<Character>().lightningDefense;
                Engine.e.party[i].GetComponent<Character>().waterDefenseBase = Engine.e.party[i].GetComponent<Character>().waterDefense;
                Engine.e.party[i].GetComponent<Character>().shadowDefenseBase = Engine.e.party[i].GetComponent<Character>().shadowDefense;
                Engine.e.party[i].GetComponent<Character>().holyDefenseBase = Engine.e.party[i].GetComponent<Character>().holyDefense;
                Engine.e.party[i].GetComponent<Character>().sleepDefenseBase = Engine.e.party[i].GetComponent<Character>().sleepDefense;
                Engine.e.party[i].GetComponent<Character>().poisonDefenseBase = Engine.e.party[i].GetComponent<Character>().poisonDefense;
                Engine.e.party[i].GetComponent<Character>().confuseDefenseBase = Engine.e.party[i].GetComponent<Character>().confuseDefense;
                Engine.e.party[i].GetComponent<Character>().deathDefenseBase = Engine.e.party[i].GetComponent<Character>().deathDefense;

                Engine.e.party[i].GetComponent<Character>().inSwitchQueue = false;
            }
        }

        for (int i = 0; i < activeParty.activeParty.Length; i++)
        {
            if (activeParty.activeParty[i] != null)
            {
                battleBodyTotal++;
            }
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                battleBodyTotal++;
            }
        }

        for (int i = 0; i < currentAnimation.Length; i++)
        {
            currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController = null;
        }

        damageTextTimer = new float[8];

        for (int i = 0; i < damageTextTimer.Length; i++)
        {
            damageTextTimer[i] = 0.75f;
        }

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

        if (!isDead && state != BattleState.WON && state != BattleState.LOST && state != BattleState.LEVELUP && state != BattleState.LEVELUPCHECK)
        {
            if (state == currentInQueue && state != BattleState.ATBCHECK)
            {
                state = BattleState.ATBCHECK; 
            }

            if (!battleQueue.Contains(BattleState.CHAR1TURN) && !battleQueue.Contains(BattleState.CONFCHAR1))
            {
                if (activeParty.activeParty[0].GetComponent<Character>().currentHealth > 0
                && !activeParty.activeParty[0].GetComponent<Character>().isAsleep)
                {
                    if (char1ATB < ATBReady)
                    {
                        //char1ATB += (activeParty.activeParty[0].GetComponent<Character>().haste * 10) / 50;
                        if (Engine.e.battleModeActive || !Engine.e.battleModeActive && !char2Ready && !char3Ready && !enemy1Ready && !enemy2Ready && !enemy3Ready && !enemy4Ready)
                        {
                            char1ATB += activeParty.activeParty[0].GetComponent<Character>().haste * 10 / 25 * Time.deltaTime;
                            char1ATBGuage.value = char1ATB;
                        }
                    }
                    else
                    {
                        char1ATB = ATBReady;
                        char1ATBGuage.value = char1ATB;

                        if (!activeParty.activeParty[0].GetComponent<Character>().isConfused)
                        {
                            if (state == BattleState.ATBCHECK)
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

                            if (Engine.e.battleModeActive || !Engine.e.battleModeActive && !char1Ready && !char3Ready && !enemy1Ready && !enemy2Ready && !enemy3Ready && !enemy4Ready)
                            {
                                char2ATB += activeParty.activeParty[1].GetComponent<Character>().haste * 10 / 25 * Time.deltaTime;
                                char2ATBGuage.value = char2ATB;
                            }
                        }
                        else
                        {
                            char2ATB = ATBReady;
                            char2ATBGuage.value = char2ATB;

                            if (!activeParty.activeParty[1].GetComponent<Character>().isConfused)
                            {
                                if (state == BattleState.ATBCHECK)
                                //&& !enemyMoving && !enemyAttackDrop && !confuseTargetCheck && !confuseAttack)
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
                            if (Engine.e.battleModeActive || !Engine.e.battleModeActive && !char1Ready && !char2Ready && !enemy1Ready && !enemy2Ready && !enemy3Ready && !enemy4Ready)
                            {

                                char3ATB += activeParty.activeParty[2].GetComponent<Character>().haste * 10 / 25 * Time.deltaTime;
                                char3ATBGuage.value = char3ATB;
                            }
                        }
                        else
                        {
                            char3ATB = ATBReady;
                            char3ATBGuage.value = char3ATB;

                            if (!activeParty.activeParty[2].GetComponent<Character>().isConfused)
                            {
                                if (state == BattleState.ATBCHECK)
                                //&& !enemyMoving && !enemyAttackDrop && !confuseTargetCheck && !confuseAttack)
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
                if (enemies[0].GetComponent<Enemy>().currentHealth > 0 && !enemies[0].GetComponent<Enemy>().isAsleep)
                {
                    if (enemy1ATB < ATBReady)
                    {
                        if (Engine.e.battleModeActive || !Engine.e.battleModeActive && !char1Ready && !char2Ready && !char3Ready && !enemy2Ready && !enemy3Ready && !enemy4Ready)
                        {
                            float randomVariation = Random.Range(0.65f, 1f);
                            enemy1ATB += ((enemies[0].GetComponent<Enemy>().haste * randomVariation) * 10 / 25 * Time.deltaTime);
                            enemy1ATBGuage.value = enemy1ATB;
                        }
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
                        if (enemies[1].GetComponent<Enemy>().currentHealth > 0 && !enemies[1].GetComponent<Enemy>().isAsleep)
                        {
                            if (enemy2ATB < ATBReady)
                            {
                                if (Engine.e.battleModeActive || !Engine.e.battleModeActive && !char1Ready && !char2Ready && !char3Ready && !enemy1Ready && !enemy3Ready && !enemy4Ready)
                                {
                                    float randomVariation = Random.Range(0.65f, 1f);
                                    enemy2ATB += ((enemies[1].GetComponent<Enemy>().haste * randomVariation) * 10 / 25 * Time.deltaTime);
                                    enemy2ATBGuage.value = enemy2ATB;
                                }
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
                        if (enemies[2].GetComponent<Enemy>().currentHealth > 0 && !enemies[2].GetComponent<Enemy>().isAsleep)
                        {
                            if (enemy3ATB < ATBReady)
                            {
                                if (Engine.e.battleModeActive || !Engine.e.battleModeActive && !char1Ready && !char2Ready && !char3Ready && !enemy1Ready && !enemy2Ready && !enemy4Ready)
                                {
                                    float randomVariation = Random.Range(0.65f, 1f);
                                    enemy3ATB += ((enemies[2].GetComponent<Enemy>().haste * randomVariation) * 10 / 25 * Time.deltaTime);
                                    enemy3ATBGuage.value = enemy3ATB;
                                }
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
                        if (enemies[3].GetComponent<Enemy>().currentHealth > 0 && !enemies[3].GetComponent<Enemy>().isAsleep)
                        {
                            if (enemy4ATB < ATBReady)
                            {
                                if (Engine.e.battleModeActive || !Engine.e.battleModeActive && !char1Ready && !char2Ready && !char3Ready && !enemy1Ready && !enemy2Ready && !enemy3Ready)
                                {
                                    float randomVariation = Random.Range(0.65f, 1f);
                                    enemy4ATB += ((enemies[3].GetComponent<Enemy>().haste * randomVariation) * 10 / 25 * Time.deltaTime);
                                    enemy4ATBGuage.value = enemy4ATB;
                                }
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
                    if (activeParty.activeParty[0].GetComponent<Character>().currentHealth > 0 && !activeParty.activeParty[0].GetComponent<Character>().isAsleep)
                    {
                        partyTurn = true;

                        targetingEnemy = char1TargetingEnemy;
                        targetingTeam = char1TargetingTeam;

                        if (char1Attacking)
                        {

                            char1Attacking = false;
                            char1Ready = false;

                            physicalAttack = char1PhysicalAttack;
                            char1PhysicalAttack = false;

                            dropAttack = char1DropAttack;
                            char1DropAttack = false;

                            skillPhysicalAttack = char1SkillPhysicalAttack;
                            char1SkillPhysicalAttack = false;

                            skillRangedAttack = char1SkillRangedAttack;
                            char1SkillRangedAttack = false;


                            StartCoroutine(CharAttack(char1Target));


                        }

                        if (char1Supporting)
                        {
                            char1Supporting = false;

                            usingItem = char1UsingItem;
                            char1UsingItem = false;

                            dropSupport = char1DropSupport;
                            char1DropSupport = false;

                            skillSelfSupport = char1SkillSelfSupport;
                            char1SkillSelfSupport = false;

                            skillTargetSupport = char1SkillTargetSupport;
                            char1SkillTargetSupport = false;

                            StartCoroutine(CharSupport(char1Target));

                        }

                        if (char1Switching)
                        {
                            char1Switching = false;
                            StartCoroutine(CharSwitch(char1SwitchToIndex));
                        }
                    }
                    else
                    {
                        battleQueue.Dequeue();
                        currentInQueue = BattleState.QUEUECHECK;

                    }
                }

                if (currentInQueue == BattleState.CHAR2TURN)
                {
                    if (activeParty.activeParty[1].GetComponent<Character>().currentHealth > 0 && !activeParty.activeParty[1].GetComponent<Character>().isAsleep)
                    {
                        partyTurn = true;

                        targetingEnemy = char2TargetingEnemy;
                        targetingTeam = char2TargetingTeam;

                        if (char2Attacking)
                        {

                            char2Attacking = false;
                            char2Ready = false;

                            physicalAttack = char2PhysicalAttack;
                            char2PhysicalAttack = false;

                            dropAttack = char2DropAttack;
                            char2DropAttack = false;

                            skillPhysicalAttack = char2SkillPhysicalAttack;
                            char2SkillPhysicalAttack = false;

                            skillRangedAttack = char2SkillRangedAttack;
                            char2SkillRangedAttack = false;

                            StartCoroutine(CharAttack(char2Target));

                        }

                        if (char2Supporting)
                        {
                            char2Supporting = false;

                            usingItem = char2UsingItem;
                            char2UsingItem = false;

                            dropSupport = char2DropSupport;
                            char2DropSupport = false;

                            skillSelfSupport = char2SkillSelfSupport;
                            char2SkillSelfSupport = false;

                            skillTargetSupport = char2SkillTargetSupport;
                            char2SkillTargetSupport = false;

                            StartCoroutine(CharSupport(char2Target));

                        }

                        if (char2Switching)
                        {
                            char2Switching = false;
                            StartCoroutine(CharSwitch(char2SwitchToIndex));
                        }
                    }
                    else
                    {
                        battleQueue.Dequeue();
                        currentInQueue = BattleState.QUEUECHECK;

                    }
                }

                if (currentInQueue == BattleState.CHAR3TURN)
                {
                    if (activeParty.activeParty[2].GetComponent<Character>().currentHealth > 0 && !activeParty.activeParty[2].GetComponent<Character>().isAsleep)
                    {
                        partyTurn = true;

                        targetingEnemy = char3TargetingEnemy;
                        targetingTeam = char3TargetingTeam;

                        if (char3Attacking)
                        {

                            char3Attacking = false;
                            char3Ready = false;

                            physicalAttack = char3PhysicalAttack;
                            char3PhysicalAttack = false;

                            dropAttack = char3DropAttack;
                            char3DropAttack = false;

                            skillPhysicalAttack = char3SkillPhysicalAttack;
                            char3SkillPhysicalAttack = false;

                            skillRangedAttack = char3SkillRangedAttack;
                            char3SkillRangedAttack = false;

                            StartCoroutine(CharAttack(char3Target));
                        }

                        if (char3Supporting)
                        {
                            char3Supporting = false;

                            usingItem = char3UsingItem;
                            char3UsingItem = false;

                            dropSupport = char3DropSupport;
                            char3DropSupport = false;

                            skillSelfSupport = char3SkillSelfSupport;
                            char3SkillSelfSupport = false;

                            skillTargetSupport = char3SkillTargetSupport;
                            char3SkillTargetSupport = false;

                            StartCoroutine(CharSupport(char3Target));

                        }

                        if (char3Switching)
                        {
                            char3Switching = false;
                            StartCoroutine(CharSwitch(char3SwitchToIndex));
                        }
                    }
                    else
                    {
                        battleQueue.Dequeue();
                        currentInQueue = BattleState.QUEUECHECK;

                    }
                }

                if (currentInQueue == BattleState.CONFCHAR1 && char1Ready)
                {
                    if (activeParty.activeParty[0].GetComponent<Character>().currentHealth > 0 && !activeParty.activeParty[0].GetComponent<Character>().isAsleep)
                    {
                        partyTurn = true;

                        Char1ConfusedTurn();
                    }
                    else
                    {
                        battleQueue.Dequeue();
                        currentInQueue = BattleState.QUEUECHECK;

                    }
                }

                if (currentInQueue == BattleState.CONFCHAR2 && char2Ready)
                {
                    if (activeParty.activeParty[1].GetComponent<Character>().currentHealth > 0 && !activeParty.activeParty[1].GetComponent<Character>().isAsleep)
                    {
                        partyTurn = true;

                        Char2ConfusedTurn();
                    }
                    else
                    {
                        battleQueue.Dequeue();
                        currentInQueue = BattleState.QUEUECHECK;

                    }
                }

                if (currentInQueue == BattleState.CONFCHAR3 && char3Ready)
                {
                    if (activeParty.activeParty[2].GetComponent<Character>().currentHealth > 0 && !activeParty.activeParty[2].GetComponent<Character>().isAsleep)
                    {
                        partyTurn = true;

                        Char3ConfusedTurn();
                    }
                    else
                    {
                        battleQueue.Dequeue();
                        currentInQueue = BattleState.QUEUECHECK;

                    }
                }

                if (currentInQueue == BattleState.ENEMY1TURN && enemy1Ready)
                {
                    if (enemies[0].GetComponent<Enemy>().currentHealth > 0 && !enemies[0].GetComponent<Enemy>().isAsleep)
                    {

                        partyTurn = false;

                        Enemy1Turn();
                    }
                    else
                    {
                        battleQueue.Dequeue();
                        currentInQueue = BattleState.QUEUECHECK;

                    }
                    enemy1Ready = false;
                }
                if (currentInQueue == BattleState.ENEMY2TURN && enemy2Ready)
                {
                    if (enemies[1].GetComponent<Enemy>().currentHealth > 0 && !enemies[1].GetComponent<Enemy>().isAsleep)
                    {
                        partyTurn = false;

                        Enemy2Turn();
                    }
                    else
                    {
                        battleQueue.Dequeue();
                        currentInQueue = BattleState.QUEUECHECK;

                    }
                    enemy2Ready = false;
                }
                if (currentInQueue == BattleState.ENEMY3TURN && enemy3Ready)
                {
                    if (enemies[2].GetComponent<Enemy>().currentHealth > 0 && !enemies[2].GetComponent<Enemy>().isAsleep)
                    {
                        partyTurn = false;

                        Enemy3Turn();
                    }
                    else
                    {
                        battleQueue.Dequeue();
                        currentInQueue = BattleState.QUEUECHECK;

                    }
                    enemy3Ready = false;
                }
                if (currentInQueue == BattleState.ENEMY4TURN && enemy4Ready)
                {
                    if (enemies[3].GetComponent<Enemy>().currentHealth > 0 && !enemies[3].GetComponent<Enemy>().isAsleep)
                    {
                        partyTurn = false;

                        Enemy4Turn();
                    }
                    else
                    {
                        battleQueue.Dequeue();
                        currentInQueue = BattleState.QUEUECHECK;

                    }
                    enemy4Ready = false;
                }
            }
        }
    }

    public IEnumerator CharAttack(int _target)
    {

        //DeactivateTargetButtons();
        inBattleMenu = false;
        int index = 0;
        int target = 0;
        GameObject characterAttacking = null;
        GameObject targetGOLoc = null;
        Character _characterAttacking = null;

        if (currentInQueue == BattleState.CHAR1TURN)
        {
            index = 0;
            characterAttacking = Engine.e.activeParty.gameObject;
            _characterAttacking = activeParty.activeParty[0].GetComponent<Character>();
        }
        if (currentInQueue == BattleState.CHAR2TURN)
        {
            index = 1;
            characterAttacking = Engine.e.activePartyMember2;
            _characterAttacking = activeParty.activeParty[1].GetComponent<Character>();
        }
        if (currentInQueue == BattleState.CHAR3TURN)
        {
            index = 2;
            characterAttacking = Engine.e.activePartyMember3;
            _characterAttacking = activeParty.activeParty[2].GetComponent<Character>();
        }

        if (!targetingTeam)
        {
            targetGOLoc = enemies[_target].gameObject;

            if (enemies[_target].GetComponent<Enemy>().currentHealth <= 0)
            {
                target = enemyGroup.GetRandomRemainingEnemy();

                if (index == 0)
                {
                    char1Target = target;
                }
                if (index == 1)
                {
                    char2Target = target;
                }
                if (index == 2)
                {
                    char3Target = target;
                }
            }
            else
            {
                target = _target;
            }

            previousTargetReference = target;

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

                skillPhysicalAttack = false;

                Skills _skillChoice = null;

                if (index == 0)
                {
                    _skillChoice = char1SkillChoice;
                }
                if (index == 1)
                {
                    _skillChoice = char2SkillChoice;
                }
                if (index == 2)
                {
                    _skillChoice = char3SkillChoice;
                }

                float skillModifier;
                skillBoostTotal = 0f;


                skillModifier = ((_characterAttacking.skillScale) / 2);

                switch (_skillChoice.skillIndex)
                {
                    case 0: // Foebreak
                        damageTotal = _characterAttacking.strength * 2;
                        break;

                    case 1: // Berserk
                        damageTotal = Mathf.Round(_characterAttacking.strength + skillModifier / 2);
                        break;
                    case 10: // Steal
                        dontDisplayDmgPopup = true;
                        damageTotal = 0;
                        break;
                    case 11: // Daze
                        dontDisplayDmgPopup = true;
                        damageTotal = 0;
                        break;
                    case 13: // Envenom
                        damageTotal = Mathf.Round(_skillChoice.skillPower + (_skillChoice.skillPower * _characterAttacking.shadowDropsLevel / 20) * skillModifier);
                        break;
                    case 14: // Assassinate
                        damageTotal = 99999;
                        break;

                }


                _characterAttacking.currentEnergy -= Mathf.Round((_skillChoice.skillCost
                - _skillChoice.skillCost * _characterAttacking.skillCostReduction / 100) + 0.45f);


                charUsingSkill = true;
                charMoving = true;
                targetCheck = true;
                charAttacking = true;

                // Check "AttackMoveToTargetChar()" for more details on each Skill

            }

            if (skillRangedAttack == true)
            {
                skillRangedAttack = false;

                Skills _skillChoice = null;

                if (index == 0)
                {
                    _skillChoice = char1SkillChoice;
                }
                if (index == 1)
                {
                    _skillChoice = char2SkillChoice;
                }
                if (index == 2)
                {
                    _skillChoice = char3SkillChoice;
                }

                float skillModifier;
                skillBoostTotal = 0f;


                skillModifier = ((_characterAttacking.skillScale) / 2);

                switch (_skillChoice.skillIndex)
                {

                    case 5:
                        mpRestore = true;
                        _characterAttacking.currentMana += (_skillChoice.skillPointReturn + (_skillChoice.skillPointReturn * skillModifier));
                        skillBoostTotal = (_skillChoice.skillPointReturn + (_skillChoice.skillPointReturn * skillModifier));
                        //Instantiate(siphonAnim, enemies[target].transform.position, Quaternion.identity);

                        if (_characterAttacking.currentMana > _characterAttacking.maxMana)
                        {
                            _characterAttacking.currentMana = _characterAttacking.maxMana;
                        }
                        damageTotal = skillBoostTotal;
                        break;
                    case 16:
                        //skillRangedAttack = true;
                        damageTotal = Mathf.Round(_skillChoice.skillPower + (_skillChoice.skillPower * _characterAttacking.holyDropsLevel / 20) * skillModifier);
                        //Instantiate(lightningDropAnim, characterGameObject.transform.position, Quaternion.identity);
                        break;

                }


                _characterAttacking.currentEnergy -= Mathf.Round((_skillChoice.skillCost
                - _skillChoice.skillCost * _characterAttacking.skillCostReduction / 100) + 0.45f);

                targetCheck = true;
                charMoving = false;
                charAttackDrop = true;
                skillRangedAttack = false;
                enemies[_target].GetComponent<Enemy>().TakeSkillDamage(damageTotal, _target);
                //hud.displayMana[index].text = activeParty.activeParty[index].gameObject.GetComponent<Character>().currentMana.ToString();

                if (enemies[_target].gameObject.GetComponent<Enemy>().currentHealth <= 0)
                {
                    enemies[_target].gameObject.GetComponent<Enemy>().currentHealth = 0;

                    isDead = EnemyGroup.enemyGroup.CheckEndBattle();
                    yield return new WaitForSeconds(0.1f);
                }

            }

            if (dropAttack == true)
            {
                Drops dropChoice = null;

                if (index == 0)
                {
                    dropChoice = char1DropChoice;
                }
                if (index == 1)
                {
                    dropChoice = char2DropChoice;
                }
                if (index == 2)
                {
                    dropChoice = char3DropChoice;
                }

                lastDropChoice = dropChoice;

                charMoving = false;
                targetCheck = true;

                charAttackDrop = true;

                HandleDropAnim(characterAttacking, targetGOLoc, dropChoice);

                if (dropChoice.targetAll)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        if (enemies[i] != null && enemies[i].currentHealth > 0)
                        {
                            enemies[i].GetComponent<Enemy>().DropEffect(dropChoice);
                        }
                    }
                }
                else
                {
                    enemies[_target].GetComponent<Enemy>().DropEffect(dropChoice);
                }

                activeParty.activeParty[index].gameObject.GetComponent<Character>().currentMana -= Mathf.Round(dropChoice.dropCost
                    - (dropChoice.dropCost * activeParty.activeParty[index].GetComponent<Character>().dropCostReduction / 100) + 0.45f);

                dropAttack = false;

                if (enemies[_target].gameObject.GetComponent<Enemy>().currentHealth <= 0)
                {
                    enemies[_target].gameObject.GetComponent<Enemy>().currentHealth = 0;

                    isDead = EnemyGroup.enemyGroup.CheckEndBattle();
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        else // Attacking Team
        {
            if (_target == 0)
            {
                targetGOLoc = Engine.e.activeParty.gameObject;
            }
            if (_target == 1)
            {
                targetGOLoc = Engine.e.activePartyMember2;
            }
            if (_target == 2)
            {
                targetGOLoc = Engine.e.activePartyMember3;
            }

            if (activeParty.activeParty[_target].GetComponent<Character>().currentHealth <= 0)
            {
                target = Engine.e.GetRandomRemainingPartyMember();
            }
            else
            {
                target = _target;
            }

            previousTargetReference = target;

            dialogueText.text = string.Empty;

            if (physicalAttack == true)
            {
                charMoving = true;
                targetCheck = true;
                physicalAttack = false;
                charAttacking = true;
            }

            if (dropAttack == true)
            {
                dropAttack = false;

                Drops dropChoice = null;

                if (index == 0)
                {
                    dropChoice = char1DropChoice;
                }
                if (index == 1)
                {
                    dropChoice = char2DropChoice;
                }
                if (index == 2)
                {
                    dropChoice = char3DropChoice;
                }

                charMoving = false;
                targetCheck = true;

                charAttackDrop = true;

                HandleDropAnim(characterAttacking, targetGOLoc, dropChoice);

                if (dropChoice.targetAll)
                {
                    for (int i = 0; i < activeParty.activeParty.Length; i++)
                    {
                        if (activeParty.activeParty[i] != null && activeParty.activeParty[i].GetComponent<Character>().currentHealth > 0)
                        {
                            activeParty.activeParty[i].GetComponent<Character>().DropEffect(dropChoice);
                        }
                    }
                }
                else
                {
                    activeParty.activeParty[_target].GetComponent<Character>().DropEffect(dropChoice);
                }

                _characterAttacking.currentMana -= Mathf.Round(dropChoice.dropCost
                - (dropChoice.dropCost * _characterAttacking.dropCostReduction / 100) + 0.45f);
                //hud.displayMana[index].text = activeParty.activeParty[index].gameObject.GetComponent<Character>().currentMana.ToString();

            }
        }
    }

    public IEnumerator AttackMoveToTargetChar()
    {
        int targetEnemy = 0;
        int index = 0;
        GameObject characterAttacking = null;
        Character characterAttackIndex = null;
        GameObject characterTarget = null;
        Enemy enemy = null;

        if (currentInQueue == BattleState.CHAR1TURN || currentInQueue == BattleState.CONFCHAR1)
        {
            index = 0;
            targetEnemy = char1Target;
            characterAttacking = Engine.e.activeParty.gameObject;
            characterAttackIndex = Engine.e.activeParty.activeParty[0].GetComponent<Character>();

            if (!targetingTeam)
            {
                enemy = enemies[targetEnemy].GetComponent<Enemy>();
            }
            else
            {
                if (char1Target == 0)
                {
                    characterTarget = Engine.e.activeParty.gameObject;
                }
                if (char1Target == 1)
                {
                    characterTarget = Engine.e.activePartyMember2;
                }
                if (char1Target == 2)
                {
                    characterTarget = Engine.e.activePartyMember3;
                }
            }
        }
        if (currentInQueue == BattleState.CHAR2TURN || currentInQueue == BattleState.CONFCHAR2)
        {
            index = 1;
            targetEnemy = char2Target;
            characterAttacking = Engine.e.activePartyMember2;
            characterAttackIndex = Engine.e.activeParty.activeParty[1].GetComponent<Character>();

            if (!attackingTeam)
            {
                enemy = enemies[targetEnemy].GetComponent<Enemy>();
            }
            else
            {
                if (char2Target == 0)
                {
                    characterTarget = Engine.e.activeParty.gameObject;
                }
                if (char2Target == 1)
                {
                    characterTarget = Engine.e.activePartyMember2;
                }
                if (char2Target == 2)
                {
                    characterTarget = Engine.e.activePartyMember3;
                }
            }
        }

        if (currentInQueue == BattleState.CHAR3TURN || currentInQueue == BattleState.CONFCHAR3)
        {
            index = 2;
            targetEnemy = char3Target;
            characterAttacking = Engine.e.activePartyMember3;
            characterAttackIndex = Engine.e.activeParty.activeParty[2].GetComponent<Character>();

            if (!targetingTeam)
            {
                enemy = enemies[targetEnemy].GetComponent<Enemy>();
            }
            else
            {
                if (char3Target == 0)
                {
                    characterTarget = Engine.e.activeParty.gameObject;
                }
                if (char3Target == 1)
                {
                    characterTarget = Engine.e.activePartyMember2;
                }
                if (char3Target == 2)
                {
                    characterTarget = Engine.e.activePartyMember3;
                }
            }
        }

        if (targetCheck)
        {
            targetCheck = false;


            if (Engine.e.battleModeActive && !activeParty.activeParty[index].GetComponent<Character>().isConfused)
            {
                //ActiveCheckNext();
                //state = BattleState.ATBCHECK;

            }
        }

        if (charAttacking && charAtBattlePos)
        {
            switch (index)
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

            if (!targetingTeam)
            {
                Vector3 targetPos = Vector3.MoveTowards(characterAttacking.GetComponent<Rigidbody2D>().transform.position, enemies[targetEnemy].transform.position, 8f * Time.deltaTime);

                characterAttacking.GetComponent<Rigidbody2D>().MovePosition(targetPos);

                if (Vector3.Distance(characterAttacking.transform.position, enemies[targetEnemy].transform.position) < 1)
                {

                    charAttacking = false;
                    charAtBattlePos = false;

                    if (!charUsingSkill)
                    {
                        if (characterAttackIndex.GetComponent<Character>().weaponRight.GetComponent<Weapon>() != null)
                        {
                            HandleMeleeAttackAnim(characterAttacking, enemies[targetEnemy].gameObject, characterAttackIndex.GetComponent<Character>().weaponRight.GetComponent<Weapon>());
                        }

                        enemy.TakePhysicalDamage(targetEnemy, characterAttackIndex.strength, characterAttackIndex.hitChance);

                    }
                    else
                    {
                        Skills _skillChoice = null;

                        if (index == 0)
                        {
                            _skillChoice = char1SkillChoice;
                        }
                        if (index == 1)
                        {
                            _skillChoice = char2SkillChoice;
                        }
                        if (index == 2)
                        {
                            _skillChoice = char3SkillChoice;
                        }

                        switch (_skillChoice.skillIndex)
                        {
                            case 0: // Foebreak
                                enemy.TakeSkillDamage(damageTotal, targetEnemy);
                                break;
                            case 1: // Berserk
                                if (enemies[0].currentHealth > 0)
                                {
                                    enemies[0].TakeSkillDamage(damageTotal, 0);
                                    SpriteDamageFlash(0);
                                }
                                if (enemies[1] != null && enemies[1].currentHealth > 0)
                                {
                                    enemies[1].TakeSkillDamage(damageTotal, 1);
                                    SpriteDamageFlash(1);

                                }
                                if (enemies[2] != null && enemies[2].currentHealth > 0)
                                {
                                    enemies[2].TakeSkillDamage(damageTotal, 2);
                                    SpriteDamageFlash(2);

                                }
                                if (enemies[3] != null && enemies[3].currentHealth > 0)
                                {
                                    enemies[3].TakeSkillDamage(damageTotal, 3);
                                    SpriteDamageFlash(3);

                                }

                                //SetDamagePopupTextAllEnemies(damageTotal.ToString(), Color.white);

                                break;
                            case 10: // Steal
                                enemy.StealAttempt(targetEnemy, characterAttackIndex.stealChance);
                                break;
                            case 11: // Daze
                                float hitChanceReduction = 10 + Mathf.Round(characterAttackIndex.skillScale * 10 / 25);
                                enemy.hitChance -= hitChanceReduction;
                                break;
                            case 13: // Envenom
                                enemy.TakeSkillDamage(damageTotal, targetEnemy);
                                enemy.InflictBio(index, 10);
                                break;
                            case 14: // Assassinate
                                enemy.InflictDeath();
                                break;
                        }
                    }



                    if (dodgedAttack == true)
                    {
                        dodgedAttack = false;
                    }
                    else
                    {
                        SpriteDamageFlash(targetEnemy);
                    }
                }
            }
            else
            {
                Vector3 targetPos = Vector3.MoveTowards(characterAttacking.GetComponent<Rigidbody2D>().transform.position, characterTarget.transform.position, 8f * Time.deltaTime);

                characterAttacking.GetComponent<Rigidbody2D>().MovePosition(targetPos);

                if (Vector3.Distance(characterAttacking.transform.position, characterTarget.transform.position) < 1)
                {

                    charAttacking = false;
                    charAtBattlePos = false;

                    activeParty.activeParty[targetEnemy].GetComponent<Character>().TakePhysicalDamage(targetEnemy, characterAttackIndex.strength);



                    if (dodgedAttack == true)
                    {
                        dodgedAttack = false;
                    }
                    else
                    {
                        SpriteDamageFlash(targetEnemy);
                    }
                }
            }
        }

        if (!charAttacking && !charAtBattlePos)
        {
            enemyGroup.moveToPosition = true;

            if (index == 0)
            {
                if (characterAttacking.transform.position == leaderPos)
                {
                    charAtBattlePos = true;
                }
            }
            if (index == 1)
            {
                if (characterAttacking.transform.position == activeParty2Pos)
                {
                    charAtBattlePos = true;
                }
            }
            if (index == 2)
            {
                if (characterAttacking.transform.position == activeParty3Pos)
                {
                    charAtBattlePos = true;
                }
            }

            if (charAtBattlePos && !charAttacking)
            {
                if (!attackingTeam)
                {
                    if (enemies[targetEnemy].gameObject.GetComponent<Enemy>().currentHealth <= 0)
                    {
                        enemies[targetEnemy].gameObject.GetComponent<Enemy>().currentHealth = 0;

                        isDead = EnemyGroup.enemyGroup.CheckEndBattle();

                        if (isDead)
                        {
                            state = BattleState.LEVELUPCHECK;
                            yield return new WaitForSeconds(2f);
                            StartCoroutine(LevelUpCheck());
                        }
                    }
                }
                else
                {
                    attackingTeam = false;
                }

                animExists = false;
                HandleAnimation();
                //PauseTransition();
                //state = BattleState.ATBCHECK;
                //currentInQueue = nextInQueue;


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

    public void SpriteDamageFlash(int _target)
    {
        if (currentInQueue == BattleState.CHAR1TURN || currentInQueue == BattleState.CONFCHAR1
        || currentInQueue == BattleState.CHAR2TURN || currentInQueue == BattleState.CONFCHAR2
        || currentInQueue == BattleState.CHAR3TURN || currentInQueue == BattleState.CONFCHAR3)
        {
            if (!targetingTeam)
            {
                if (lastDropChoice != null && !lastDropChoice.targetAll || lastSkillChoice != null && !lastSkillChoice.targetAll
                || lastDropChoice == null && lastSkillChoice == null)
                {
                    enemies[_target].GetComponentInChildren<SpriteRenderer>().material = damageFlash;
                }
                else
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        if (enemies[i].currentHealth > 0)
                        {
                            enemies[i].GetComponentInChildren<SpriteRenderer>().material = damageFlash;
                        }
                    }
                }
            }
            else
            {
                if (lastDropChoice != null && !lastDropChoice.targetAll || lastSkillChoice != null && !lastSkillChoice.targetAll
                || lastSkillChoice == null && lastDropChoice == null)
                {
                    if (_target == 0)
                    {
                        Engine.e.activeParty.gameObject.GetComponent<SpriteRenderer>().material = damageFlash;
                    }
                    if (_target == 1)
                    {
                        Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().material = damageFlash;
                    }
                    if (_target == 2)
                    {
                        Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().material = damageFlash;
                    }
                }
                else
                {
                    if (activeParty.activeParty[0].GetComponent<Character>().currentHealth > 0)
                    {
                        Engine.e.activeParty.gameObject.GetComponent<SpriteRenderer>().material = damageFlash;
                    }
                    if (activeParty.activeParty[1] != null && activeParty.activeParty[1].GetComponent<Character>().currentHealth > 0)
                    {
                        Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().material = damageFlash;
                    }
                    if (activeParty.activeParty[2] != null && activeParty.activeParty[2].GetComponent<Character>().currentHealth > 0)
                    {
                        Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().material = damageFlash;
                    }
                }
            }
        }
        else
        {
            if (!targetingTeam)
            {
                if (lastDropChoice != null && !lastDropChoice.targetAll || lastSkillChoice != null && !lastSkillChoice.targetAll
                || lastSkillChoice == null && lastDropChoice == null)
                {
                    if (_target == 0)
                    {
                        Engine.e.activeParty.gameObject.GetComponent<SpriteRenderer>().material = damageFlash;
                    }
                    if (_target == 1)
                    {
                        Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().material = damageFlash;
                    }
                    if (_target == 2)
                    {
                        Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().material = damageFlash;
                    }
                }
                else
                {
                    if (activeParty.activeParty[0].GetComponent<Character>().currentHealth > 0)
                    {
                        Engine.e.activeParty.gameObject.GetComponent<SpriteRenderer>().material = damageFlash;
                    }
                    if (activeParty.activeParty[1] != null && activeParty.activeParty[1].GetComponent<Character>().currentHealth > 0)
                    {
                        Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().material = damageFlash;
                    }
                    if (activeParty.activeParty[2] != null && activeParty.activeParty[2].GetComponent<Character>().currentHealth > 0)
                    {
                        Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().material = damageFlash;
                    }
                }
            }
            else
            {
                if (lastDropChoice != null && !lastDropChoice.targetAll || lastSkillChoice != null && !lastSkillChoice.targetAll
                || lastDropChoice == null && lastSkillChoice == null)
                {
                    enemies[_target].GetComponentInChildren<SpriteRenderer>().material = damageFlash;
                }
                else
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        if (enemies[i].currentHealth > 0)
                        {
                            enemies[i].GetComponentInChildren<SpriteRenderer>().material = damageFlash;
                        }
                    }
                }
            }
        }
    }

    void HandleAnimation()
    {
        if (state == currentInQueue)
        {
            state = BattleState.ATBCHECK;
        }

        if (animExists)
        {
            if (animationTimer <= 0)
            {
                charAttackDrop = false;
                animExists = false;
                for (int i = 0; i < currentAnimation.Length; i++)
                {
                    if (currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController != null)
                    {
                        currentAnimation[i].gameObject.SetActive(false);
                        currentAnimation[i].GetComponent<Animator>().runtimeAnimatorController = null;
                    }
                }
                animState = AnimState.NONE;

                displayDamageText = true;
            }
        }
        else // For strange scenarios 
        {
            displayDamageText = true;
            //EndTurn();
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
        GameObject _characterLoc = null;
        int target = 0;
        Character _characterAttacking = null;
        GameObject _charTargetLoc = null;

        if (currentInQueue == BattleState.CONFCHAR1)
        {
            index = 0;
            _characterLoc = Engine.e.activeParty.gameObject;
            target = char1Target;
            _characterAttacking = activeParty.activeParty[0].GetComponent<Character>();
        }
        if (currentInQueue == BattleState.CONFCHAR2)
        {
            index = 1;
            _characterLoc = Engine.e.activePartyMember2;
            target = char2Target;
            _characterAttacking = activeParty.activeParty[1].GetComponent<Character>();

        }
        if (currentInQueue == BattleState.CONFCHAR3)
        {
            index = 2;
            _characterLoc = Engine.e.activePartyMember3;
            target = char3Target;
            _characterAttacking = activeParty.activeParty[2].GetComponent<Character>();

        }

        yield return new WaitForSeconds(0.3f);


        Debug.Log("entered CharConfuseAttack");

        int choiceAttack = Random.Range(0, 100);
        Debug.Log(choiceAttack);
        if (choiceAttack < 70)
        {
            charMoving = true;
            physicalAttack = true;
            charAttacking = true;
            targetCheck = true;
            //animExists = true;

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
                    if (attackingTeam)
                    {
                        physicalAttack = false;
                        activeParty.activeParty[target].GetComponent<Character>().DropEffect(lastDropChoice);

                        if (target == 0)
                        {
                            _charTargetLoc = Engine.e.activeParty.gameObject;
                        }
                        if (target == 1)
                        {
                            _charTargetLoc = Engine.e.activePartyMember2.gameObject;
                        }
                        if (target == 2)
                        {
                            _charTargetLoc = Engine.e.activePartyMember3.gameObject;
                        }

                        HandleDropAnim(_characterLoc, _charTargetLoc, lastDropChoice);

                    }
                    else
                    {

                        enemies[target].gameObject.GetComponent<Enemy>().DropEffect(lastDropChoice);
                        HandleDropAnim(_characterLoc, enemies[target].gameObject, lastDropChoice);

                        if (enemies[target].gameObject.GetComponent<Enemy>().currentHealth <= 0)
                        {
                            enemies[target].gameObject.GetComponent<Enemy>().currentHealth = 0;

                            isDead = EnemyGroup.enemyGroup.CheckEndBattle();

                            //state = BattleState.ATBCHECK;
                            yield return new WaitForSeconds(0.1f);

                        }
                    }
                }

                confuseAttack = false;
                Debug.Log("Cost: " + lastDropChoice.dropCost);
                Debug.Log("Drop Name: " + lastDropChoice.dropName);

                activeParty.activeParty[index].GetComponent<Character>().currentMana -= Mathf.Round(lastDropChoice.dropCost
            - (lastDropChoice.dropCost * activeParty.activeParty[index].GetComponent<Character>().dropCostReduction / 100) + 0.45f);

                //                animExists = true;
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

    public IEnumerator CharSupport(int _target)
    {

        int index = 0;
        GameObject _characterLoc = null;
        GameObject _targetGOLoc = null;

        previousTargetReference = _target;

        inBattleMenu = false;

        if (currentInQueue == BattleState.CHAR1TURN)
        {
            index = 0;
            _characterLoc = Engine.e.activeParty.gameObject;
        }
        if (currentInQueue == BattleState.CHAR2TURN)
        {
            index = 1;
            _characterLoc = Engine.e.activePartyMember2;

        }
        if (currentInQueue == BattleState.CHAR3TURN)
        {
            index = 2;
            _characterLoc = Engine.e.activePartyMember3;
        }

        if (targetingTeam) // Most of the time this should be the case
        {
            if (_target == 0)
            {
                _targetGOLoc = Engine.e.activeParty.gameObject;
            }
            if (_target == 1)
            {
                _targetGOLoc = Engine.e.activePartyMember2;
            }
            if (_target == 2)
            {
                _targetGOLoc = Engine.e.activePartyMember3;
            }

            // Supporting target via an Item
            if (usingItem)
            {
                usingItem = false;

                Item _itemToBeUsed = null;

                if (index == 0)
                {
                    _itemToBeUsed = char1ItemToBeUsed;
                }
                if (index == 1)
                {
                    _itemToBeUsed = char2ItemToBeUsed;
                }
                if (index == 2)
                {
                    _itemToBeUsed = char3ItemToBeUsed;
                }

                if (!_itemToBeUsed.targetAll)
                {
                    Engine.e.UseItemInBattle(_target);
                }
                else
                {
                    for (int i = 0; i < activeParty.activeParty.Length; i++)
                    {
                        if (activeParty.activeParty[i] != null)
                        {
                            Engine.e.UseItemInBattle(i);
                        }
                    }
                    Engine.e.partyInventoryReference.SubtractItemFromInventory(_itemToBeUsed);
                }
            }

            // Supporting target via Drop
            if (dropSupport)
            {
                Drops _dropChoice = null;

                dropSupport = false;

                if (index == 0)
                {
                    _dropChoice = char1DropChoice;
                }
                if (index == 1)
                {
                    _dropChoice = char2DropChoice;

                }
                if (index == 2)
                {
                    _dropChoice = char3DropChoice;

                }

                lastDropChoice = _dropChoice;


                HandleDropAnim(_characterLoc, _targetGOLoc, _dropChoice);

                if (_dropChoice.targetAll)
                {

                }
                activeParty.activeParty[_target].GetComponent<Character>().DropEffect(_dropChoice);

                activeParty.activeParty[index].gameObject.GetComponent<Character>().currentMana -= Mathf.Round(_dropChoice.dropCost
                                - (_dropChoice.dropCost * activeParty.activeParty[index].GetComponent<Character>().dropCostReduction / 100) + 0.45f);
                dropAttack = false;

            }

            // Supporting self via Skill
            if (skillSelfSupport)
            {
                Skills _skillChoice = null;
                Character _selfTarget = activeParty.activeParty[index].GetComponent<Character>();
                skillSelfSupport = false;

                if (index == 0)
                {
                    _skillChoice = char1SkillChoice;
                }
                if (index == 1)
                {
                    _skillChoice = char2SkillChoice;

                }
                if (index == 2)
                {
                    _skillChoice = char3SkillChoice;

                }

                float skillModifier;
                skillBoostTotal = 0f;


                skillModifier = ((_selfTarget.skillScale) / 2);

                if (_selfTarget.currentEnergy >= _skillChoice.skillCost)
                {
                    {

                        switch (_skillChoice.skillIndex)
                        {
                            case 15:

                                _selfTarget.currentEnergy -= _skillChoice.skillCost;

                                _selfTarget.physicalDefense += Mathf.Round(_selfTarget.physicalDefense * 0.1f);
                                break;
                            case 16:

                                float healAmount = Mathf.Round(skillModifier + (_skillChoice.skillPower * _selfTarget.holyDropsLevel / 10));

                                activeParty.activeParty[0].GetComponent<Character>().currentHealth += healAmount;

                                SetDamagePopupTextAllTeam(healAmount.ToString(), Color.green);

                                break;
                        }
                    }
                }
            }
            // Supporting target via Skill
            if (skillTargetSupport)
            {
                Skills _skillChoice = null;

                if (index == 0)
                {
                    _skillChoice = char1SkillChoice;
                }
                if (index == 1)
                {
                    _skillChoice = char2SkillChoice;

                }
                if (index == 2)
                {
                    _skillChoice = char3SkillChoice;

                }

                switch (_skillChoice.skillIndex)
                {
                    case 25:

                        activeParty.activeParty[index].GetComponent<Character>().currentEnergy -= _skillChoice.skillCost;

                        if (index == 0)
                        {
                            if (char1SkillSwitch)
                            {
                                char1SkillSwitch = false;
                                charSkillSwitch = true;
                                StartCoroutine(CharSwitch(_target));
                            }
                            else
                            {
                                if (_target == 1)
                                {
                                    char2ATB = ATBReady;
                                    char2ATBGuage.value = char2ATB;
                                    char2Ready = true;
                                    ActivateChar2MenuButtons();
                                }

                                if (_target == 2)
                                {
                                    char3ATB = ATBReady;
                                    char3ATBGuage.value = char2ATB;
                                    char3Ready = true;
                                    ActivateChar3MenuButtons();
                                }
                            }
                        }

                        if (index == 1)
                        {
                            if (char2SkillSwitch)
                            {
                                char2SkillSwitch = false;
                                charSkillSwitch = true;
                                StartCoroutine(CharSwitch(_target));
                            }
                            else
                            {
                                if (_target == 0)
                                {
                                    char1ATB = ATBReady;
                                    char1ATBGuage.value = char1ATB;
                                    char1Ready = true;
                                    ActivateChar1MenuButtons();
                                }

                                if (_target == 2)
                                {
                                    char3ATB = ATBReady;
                                    char3ATBGuage.value = char3ATB;
                                    char3Ready = true;
                                    ActivateChar3MenuButtons();
                                }
                            }
                        }

                        if (index == 2)
                        {
                            if (char3SkillSwitch)
                            {
                                char3SkillSwitch = false;
                                charSkillSwitch = true;
                                StartCoroutine(CharSwitch(_target));
                            }
                            else
                            {
                                if (_target == 0)
                                {
                                    char1ATB = ATBReady;
                                    char1ATBGuage.value = char1ATB;
                                    char1Ready = true;
                                    ActivateChar1MenuButtons();
                                }

                                if (_target == 1)
                                {
                                    char2ATB = ATBReady;
                                    char2ATBGuage.value = char2ATB;
                                    char2Ready = true;
                                    ActivateChar2MenuButtons();
                                }
                            }
                        }
                        break;
                }
            }
        }

        else // Targeting enemy
        {

            _targetGOLoc = enemies[_target].gameObject;

            if (usingItem)
            {
                usingItem = false;

                Item _itemToBeUsed = null;

                if (index == 0)
                {
                    _itemToBeUsed = char1ItemToBeUsed;
                }
                if (index == 1)
                {
                    _itemToBeUsed = char2ItemToBeUsed;
                }
                if (index == 2)
                {
                    _itemToBeUsed = char3ItemToBeUsed;
                }

                if (!_itemToBeUsed.targetAll)
                {
                    enemies[_target].UseItem(_itemToBeUsed);
                }
                else
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        if (enemies[i] != null && enemies[i].currentHealth > 0)
                        {
                            enemies[i].UseItem(_itemToBeUsed);

                        }
                    }

                    Engine.e.partyInventoryReference.SubtractItemFromInventory(_itemToBeUsed);
                }
            }

            if (dropSupport)
            {
                Drops _dropChoice = null;

                dropSupport = false;

                if (index == 0)
                {
                    _dropChoice = char1DropChoice;
                }
                if (index == 1)
                {
                    _dropChoice = char2DropChoice;

                }
                if (index == 2)
                {
                    _dropChoice = char3DropChoice;

                }

                HandleDropAnim(_characterLoc, _targetGOLoc, _dropChoice);


                enemies[_target].DropEffect(_dropChoice);

                activeParty.activeParty[index].gameObject.GetComponent<Character>().currentMana -= Mathf.Round(_dropChoice.dropCost
                - (_dropChoice.dropCost * activeParty.activeParty[index].GetComponent<Character>().dropCostReduction / 100) + 0.45f);
                dropAttack = false;

                if (enemies[_target].gameObject.GetComponent<Enemy>().currentHealth <= 0)
                {
                    enemies[_target].gameObject.GetComponent<Enemy>().currentHealth = 0;

                    isDead = EnemyGroup.enemyGroup.CheckEndBattle();
                    yield return new WaitForSeconds(0.1f);


                }
            }
        }


        //EndTurn();

        yield return new WaitForSeconds(1f);
        if (!Engine.e.battleModeActive)
        {
            state = BattleState.ATBCHECK;
        }
    }


    public IEnumerator CharSwitch(int index)
    {
        DeactivateSupportButtons();
        DeactivateDropsUI();
        DeactivateSkillsUI();
        DeactivateCharSwitchButtons();

        if (!charSkillSwitch)
        {
            if (currentInQueue == BattleState.CHAR1TURN)
            {
                char1ATB = 0;
                char1ATBGuage.value = char1ATB;
                char1Ready = false;
                activeParty.activeParty[0].GetComponent<Character>().activePartyIndex = -1;

                Engine.e.activeParty.InstantiateBattleLeader(index);

                Engine.e.activeParty.activeParty[0].GetComponent<Character>().inSwitchQueue = false;

                hud.displayNames[0].text = activeParty.activeParty[0].gameObject.GetComponent<Character>().characterName;
                hud.displayHealth[0].text = activeParty.activeParty[0].gameObject.GetComponent<Character>().currentHealth.ToString();
                hud.displayMana[0].text = activeParty.activeParty[0].gameObject.GetComponent<Character>().currentMana.ToString();

            }
            if (currentInQueue == BattleState.CHAR2TURN)
            {
                char2ATB = 0;
                char2ATBGuage.value = char2ATB;
                char2Ready = false;
                activeParty.activeParty[1].GetComponent<Character>().activePartyIndex = -1;
                Engine.e.activeParty.InstantiateBattleActiveParty2(index);
                Engine.e.activeParty.activeParty[1].GetComponent<Character>().inSwitchQueue = false;

                hud.displayNames[1].text = activeParty.activeParty[1].gameObject.GetComponent<Character>().characterName;
                hud.displayHealth[1].text = activeParty.activeParty[1].gameObject.GetComponent<Character>().currentHealth.ToString();
                hud.displayMana[1].text = activeParty.activeParty[1].gameObject.GetComponent<Character>().currentMana.ToString();

            }
            if (currentInQueue == BattleState.CHAR3TURN)
            {
                char3ATB = 0;
                char3ATBGuage.value = char3ATB;
                char3Ready = false;
                activeParty.activeParty[2].GetComponent<Character>().activePartyIndex = -1;
                Engine.e.activeParty.InstantiateBattleActiveParty3(index);
                Engine.e.activeParty.activeParty[2].GetComponent<Character>().inSwitchQueue = false;

                hud.displayNames[2].text = activeParty.activeParty[2].gameObject.GetComponent<Character>().characterName;
                hud.displayHealth[2].text = activeParty.activeParty[2].gameObject.GetComponent<Character>().currentHealth.ToString();
                hud.displayMana[2].text = activeParty.activeParty[2].gameObject.GetComponent<Character>().currentMana.ToString();
            }

            dialogueText.text = string.Empty;

            yield return new WaitForSeconds(1f);

            if (!Engine.e.battleModeActive)
            {
                //StartCoroutine(CheckNext());
            }
            else
            {

                battleQueue.Dequeue();

                state = BattleState.ATBCHECK;
                currentInQueue = BattleState.QUEUECHECK;
            }
        }
        else
        {
            if (currentInQueue == BattleState.CHAR1TURN)
            {
                activeParty.activeParty[0].GetComponent<Character>().activePartyIndex = -1;

                Engine.e.activeParty.InstantiateBattleLeader(index);
                Engine.e.activeParty.activeParty[0].GetComponent<Character>().inSwitchQueue = false;

                char1ATB = ATBReady;
                char1ATBGuage.value = char1ATB;
                char1Ready = true;

                hud.displayNames[0].text = activeParty.activeParty[0].gameObject.GetComponent<Character>().characterName;
                hud.displayHealth[0].text = activeParty.activeParty[0].gameObject.GetComponent<Character>().currentHealth.ToString();
                hud.displayMana[0].text = activeParty.activeParty[0].gameObject.GetComponent<Character>().currentMana.ToString();

            }
            if (currentInQueue == BattleState.CHAR2TURN)
            {
                activeParty.activeParty[1].GetComponent<Character>().activePartyIndex = -1;

                Engine.e.activeParty.InstantiateBattleActiveParty2(index);

                Engine.e.activeParty.activeParty[1].GetComponent<Character>().inSwitchQueue = false;

                char2ATB = ATBReady;
                char2ATBGuage.value = char2ATB;
                char2Ready = true;

                hud.displayNames[1].text = activeParty.activeParty[1].gameObject.GetComponent<Character>().characterName;
                hud.displayHealth[1].text = activeParty.activeParty[1].gameObject.GetComponent<Character>().currentHealth.ToString();
                hud.displayMana[1].text = activeParty.activeParty[1].gameObject.GetComponent<Character>().currentMana.ToString();

            }
            if (currentInQueue == BattleState.CHAR3TURN)
            {
                activeParty.activeParty[2].GetComponent<Character>().activePartyIndex = -1;
                Engine.e.activeParty.InstantiateBattleActiveParty3(index);
                Engine.e.activeParty.activeParty[2].GetComponent<Character>().inSwitchQueue = false;

                char3ATB = ATBReady;
                char3ATBGuage.value = char3ATB;
                char3Ready = true;

                hud.displayNames[2].text = activeParty.activeParty[2].gameObject.GetComponent<Character>().characterName;
                hud.displayHealth[2].text = activeParty.activeParty[2].gameObject.GetComponent<Character>().currentHealth.ToString();
                hud.displayMana[2].text = activeParty.activeParty[2].gameObject.GetComponent<Character>().currentMana.ToString();
            }
        }
    }

    public IEnumerator ConfuseAttackMoveToTargetChar()
    {

        int index = 0;
        GameObject character = null;
        GameObject targetCharacter = null;
        int target = 0;

        if (currentInQueue == BattleState.CHAR1TURN || currentInQueue == BattleState.CONFCHAR1)
        {
            index = 0;
            character = Engine.e.activeParty.gameObject;
            target = char1Target;
        }
        if (currentInQueue == BattleState.CHAR2TURN || currentInQueue == BattleState.CONFCHAR2)
        {
            index = 1;
            character = Engine.e.activePartyMember2;
            target = char2Target;
        }
        if (currentInQueue == BattleState.CHAR3TURN || currentInQueue == BattleState.CONFCHAR3)
        {
            index = 2;
            character = Engine.e.activePartyMember3;
            target = char3Target;
        }

        if (target == 0)
        {
            targetCharacter = Engine.e.activeParty.gameObject;
        }
        if (target == 1)
        {
            targetCharacter = Engine.e.activePartyMember2.gameObject;
        }
        if (target == 2)
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

                isDead = Engine.e.activeParty.activeParty[target].GetComponent<Character>().TakePhysicalDamage(target, damageTotal);

                animExists = true;
                if (dodgedAttack == true)
                {
                    dodgedAttack = false;

                }
                else
                {
                    targetCharacter.GetComponent<SpriteRenderer>().material = damageFlash;

                    yield return new WaitForSeconds(0.2f);
                    targetCharacter.GetComponent<SpriteRenderer>().material = originalMaterial;
                }

            }
        }

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
                // EndTurn();
                animExists = false;

            }

            yield return new WaitForSeconds(1f);

            // StartCoroutine(CheckNext());
            if (!Engine.e.battleModeActive)
            {
                state = BattleState.ATBCHECK;
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

        for (int i = 0; i < activeParty.activeParty[index].GetComponent<Character>().drops.Length; i++)
        {
            if (activeParty.activeParty[index].GetComponent<Character>().drops[i] != null)
            {
                charRandomDropList.Add(activeParty.activeParty[index].GetComponent<Character>().drops[i]);
            }
        }

        int randomDropIndex = Random.Range(0, charRandomDropList.Count);

        lastDropChoice = charRandomDropList[randomDropIndex];
    }

    IEnumerator EnemyAttack()
    {
        int index = 0;
        int randTarget = 0;

        if (currentInQueue == BattleState.ENEMY1TURN)
        {
            index = 0;
            enemy1AttackTarget = Engine.e.GetRandomRemainingPartyMember();
            randTarget = enemy1AttackTarget;
        }
        if (currentInQueue == BattleState.ENEMY2TURN)
        {
            index = 1;
            enemy2AttackTarget = Engine.e.GetRandomRemainingPartyMember();
            randTarget = enemy2AttackTarget;
        }
        if (currentInQueue == BattleState.ENEMY3TURN)
        {
            index = 2;
            enemy3AttackTarget = Engine.e.GetRandomRemainingPartyMember();
            randTarget = enemy3AttackTarget;
        }
        if (currentInQueue == BattleState.ENEMY4TURN)
        {
            index = 3;
            enemy4AttackTarget = Engine.e.GetRandomRemainingPartyMember();
            randTarget = enemy4AttackTarget;
        }


        previousTargetReference = randTarget;

        Debug.Log(randTarget);

        yield return new WaitForSeconds(1f);

        enemies[index].GetComponent<Enemy>().BattleLogic(enemies[index], randTarget);

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

                if (dodgedAttack)
                {
                    dodgedAttack = false;
                }
                else
                {
                    SpriteDamageFlash(enemyTarget);
                }
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
                enemyMoving = false;

                //EndTurn();
                animExists = false;
                HandleAnimation();
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
        int randTarget = 0;

        if (currentInQueue == BattleState.ENEMY1TURN)
        {
            index = 0;
            randTarget = enemy1AttackTarget;
        }
        if (currentInQueue == BattleState.ENEMY2TURN)
        {
            index = 1;
            randTarget = enemy2AttackTarget;

        }
        if (currentInQueue == BattleState.ENEMY3TURN)
        {
            index = 2;
            randTarget = enemy3AttackTarget;

        }
        if (currentInQueue == BattleState.ENEMY4TURN)
        {
            index = 3;
            randTarget = enemy4AttackTarget;

        }


        if (enemyAttacking && enemyAtBattlePos)
        {
            switch (index)
            {
                case 0:
                    enemy1ATB = 0;
                    enemy1ATBGuage.value = enemy1ATB;
                    enemy1Ready = false;
                    break;
                case 1:
                    enemy2ATB = 0;
                    enemy2ATBGuage.value = enemy2ATB;
                    enemy2Ready = false;
                    break;
                case 2:
                    enemy3ATB = 0;
                    enemy3ATBGuage.value = enemy3ATB;
                    enemy3Ready = false;
                    break;
                case 3:
                    enemy4ATB = 0;
                    enemy4ATBGuage.value = enemy4ATB;
                    enemy4Ready = false;
                    break;
            }

            Vector3 targetPos = Vector3.MoveTowards(enemies[index].gameObject.transform.position, enemies[randTarget].transform.position, 6 * Time.deltaTime);

            enemies[index].GetComponent<Rigidbody2D>().MovePosition(targetPos);

            if (Vector3.Distance(enemies[index].transform.position, enemies[randTarget].transform.position) < 1)
            {
                enemyAttacking = false;
                enemyAtBattlePos = false;

                enemies[randTarget].GetComponent<Enemy>().TakePhysicalDamage(randTarget, damageTotal, enemies[index].GetComponent<Enemy>().hitChance);
                if (dodgedAttack)
                {
                    dodgedAttack = false;
                }
                else
                {
                    SpriteDamageFlash(randTarget);

                }
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
                //EndTurn();
                animExists = false;
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

    IEnumerator EnemyConfuseAttack()
    {
        int index = 0;
        int randTarget = 0;

        if (currentInQueue == BattleState.ENEMY1TURN)
        {
            index = 0;
            randTarget = enemy1AttackTarget;
        }
        if (currentInQueue == BattleState.ENEMY2TURN)
        {
            index = 1;
            randTarget = enemy2AttackTarget;

        }
        if (currentInQueue == BattleState.ENEMY3TURN)
        {
            index = 2;
            randTarget = enemy3AttackTarget;

        }
        if (currentInQueue == BattleState.ENEMY4TURN)
        {
            index = 3;
            randTarget = enemy4AttackTarget;

        }

        yield return new WaitForSeconds(0.3f);


        if (enemies[index].GetComponent<Enemy>().drops[0] != null)
        {
            int choiceAttack = Random.Range(0, 100);

            if (enemies[index].GetComponent<Enemy>().choiceAttack < choiceAttack)
            {
                enemyMoving = true;
                enemyAttacking = true;

                if (!attackingTeam)
                {
                    isDead = activeParty.activeParty[randTarget].GetComponent<Character>().TakePhysicalDamage(randTarget, enemies[index].gameObject.GetComponent<Enemy>().strength);
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
                int _enemyDropChoice = Random.Range(0, enemies[index].GetComponent<Enemy>().drops.Length);

                if (enemies[index].GetComponent<Enemy>().currentMana >= enemies[index].GetComponent<Enemy>().drops[_enemyDropChoice].dropCost)
                {
                    physicalAttack = false;
                    confuseAttack = false;

                    enemyAttackDrop = true;

                    lastDropChoice = enemies[index].gameObject.GetComponent<Enemy>().drops[_enemyDropChoice];


                    if (!attackingTeam)
                    {
                        //                      HandleDropAnim(enemies[index].gameObject, enemies[index].GetComponent<Enemy>().drops[_enemyDropChoice]);
                        activeParty.activeParty[randTarget].GetComponent<Character>().DropEffect(enemies[index].GetComponent<Enemy>().drops[_enemyDropChoice]);
                    }
                    else
                    {

                        enemies[randTarget].DropEffect(enemies[index].drops[_enemyDropChoice]);
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
                    enemies[index].GetComponent<Enemy>().currentMana -= enemies[index].GetComponent<Enemy>().drops[enemyDropChoice].dropCost;

                    if (enemies[index].GetComponent<EnemyMovement>() != null)
                    {
                        enemies[index].GetComponent<EnemyMovement>().enabled = true;
                    }
                }
                else
                {
                    enemyMoving = true;
                    enemyAttacking = true;

                    if (attackingTeam)
                    {
                        activeParty.activeParty[randTarget].GetComponent<Character>().TakePhysicalDamage(randTarget, enemies[index].gameObject.GetComponent<Enemy>().strength);
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

            if (!attackingTeam)
            {
                activeParty.activeParty[randTarget].GetComponent<Character>().TakePhysicalDamage(randTarget, enemies[index].gameObject.GetComponent<Enemy>().strength);
            }
            else
            {
                enemies[randTarget].GetComponent<Enemy>().TakePhysicalDamage(randTarget, enemies[index].GetComponent<Enemy>().damageTotal, enemies[index].GetComponent<Enemy>().hitChance);
            }
        }
    }

    IEnumerator LevelUpCheck()
    {
        Debug.Log("Entered LevelUpCheck");
        if (state == BattleState.LEVELUPCHECK)
        {
            DeactivateEnemyUI();
            //DeactivateChar1MenuButtons();
            DeactivateDropsUI();
            DeactivateSkillsUI();
            DeactivateCharSwitchButtons();
            ResetPartyMemberStats();

            if (activeParty.activeParty[1] != null)
            {
                //DeactivateChar2MenuButtons();
                DeactivateDropsUI();
                DeactivateSkillsUI();
            }

            if (activeParty.activeParty[2] != null)
            {
                //DeactivateChar3MenuButtons();
                DeactivateDropsUI();
                DeactivateSkillsUI();
            }

            Engine.e.battleSystem.enemyPanel.SetActive(false);
            Engine.e.adventureLogReference.QuestManager(enemies);


            yield return new WaitForSeconds(1f);
            enemyGroup.GroupItemDrops();

            Engine.e.battleSystem.enemyLootPanelReference.SetActive(true);
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
            Engine.e.activePartyMember2.GetComponent<BoxCollider2D>().enabled = true;
            Engine.e.activePartyMember3.GetComponent<BoxCollider2D>().enabled = true;

            enemyGroup.battleCamera.gameObject.SetActive(false);
            Engine.e.char1LevelUp = false;
            Engine.e.char2LevelUp = false;
            Engine.e.char3LevelUp = false;

            enemyGroup.DestroyGroup();

            isDead = false;

            Engine.e.battleMenu.battleMenuUI.SetActive(false);
            Engine.e.battleSystem.enemyLootPanelReference.SetActive(false);
            Engine.e.battleSystem.enemyPanel.SetActive(true);
            Engine.e.battleSystem.enemyLootReference.GetComponent<TMP_Text>().text = string.Empty;
            Engine.e.inBattle = false;
            Engine.e.storeDialogueReference.gameObject.SetActive(true);

            for (int i = 0; i < damagePopup.Length; i++)
            {
                damagePopup[i].SetActive(false);
            }
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
                Engine.e.mainVirtualCamera.GetComponent<CinemachineVirtualCamera>().Priority = 10;

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
                yield return new WaitForSeconds(2f);
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
                //char1BattlePanel.SetActive(false);
                char1LevelUpPanel.SetActive(true);
            }
            if (Engine.e.char2LevelUp == true)
            {
                // char1BattlePanel.SetActive(false);
                char2LevelUpPanel.SetActive(true);
            }
            if (Engine.e.char3LevelUp == true)
            {
                // char1BattlePanel.SetActive(false);
                char3LevelUpPanel.SetActive(true);
            }
        }

        dialogueText.text = string.Empty;

        yield return new WaitForSeconds(7f);

        char1LevelUpPanel.SetActive(false);
        char2LevelUpPanel.SetActive(false);
        char3LevelUpPanel.SetActive(false);

        char1BattlePanel.SetActive(true);

        if (activeParty.activeParty[1] != null)
        {
            char2BattlePanel.SetActive(true);
        }
        if (activeParty.activeParty[2] != null)
        {
            char3BattlePanel.SetActive(true);
        }

        state = BattleState.WON;
        StartCoroutine(EndBattle());
    }

    public void Char1Turn()
    {

        state = BattleState.CHAR1TURN;


        //    dialogueText.text = Engine.e.activeParty.activeParty[0].gameObject.GetComponent<Character>().characterName;


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

        if (!activeParty.activeParty[0].GetComponent<Character>().haltInflicted)
        {
            EventSystem.current.SetSelectedGameObject(char1MenuButtons[0]);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(char1MenuButtons[1]);
        }

    }

    public void Char1ConfusedTurn()
    {
        char1Ready = false;

        //dialogueText.text = Engine.e.activeParty.activeParty[0].gameObject.GetComponent<Character>().characterName;
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


        //   dialogueText.text = Engine.e.activeParty.activeParty[1].gameObject.GetComponent<Character>().characterName;


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

        if (!activeParty.activeParty[1].GetComponent<Character>().haltInflicted)
        {
            EventSystem.current.SetSelectedGameObject(char2MenuButtons[0]);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(char2MenuButtons[1]);
        }

    }
    public void Char2ConfusedTurn()
    {
        char2Ready = false;


        //dialogueText.text = Engine.e.activeParty.activeParty[1].gameObject.GetComponent<Character>().characterName;

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


        // dialogueText.text = Engine.e.activeParty.activeParty[2].gameObject.GetComponent<Character>().characterName;


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

        if (!activeParty.activeParty[2].GetComponent<Character>().haltInflicted)
        {
            EventSystem.current.SetSelectedGameObject(char3MenuButtons[0]);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(char3MenuButtons[1]);
        }

    }

    public void Char3ConfusedTurn()
    {
        char3Ready = false;


        //dialogueText.text = Engine.e.activeParty.activeParty[2].gameObject.GetComponent<Character>().characterName;

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
        //dialogueText.text = string.Empty;
        // dialogueText.text = enemies[0].GetComponent<Enemy>().enemyName;

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

        //dialogueText.text = string.Empty;
        // dialogueText.text = enemies[1].GetComponent<Enemy>().enemyName;
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

        //  dialogueText.text = string.Empty;
        // dialogueText.text = enemies[2].GetComponent<Enemy>().enemyName;
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

        // dialogueText.text = string.Empty;
        // dialogueText.text = enemies[3].GetComponent<Enemy>().enemyName;
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

            int teamTargetChoice = Random.Range(0, 2);
            int index = 0;
            bool partyAttacking = false;
            int randTarget = 0;

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

            if (partyAttacking) // Team is attacking
            {
                if (teamTargetChoice == 0) // Team is attacking themselves
                {
                    if (currentInQueue == BattleState.CONFCHAR1)
                    {
                        char1Target = Engine.e.GetRandomRemainingPartyMember();
                        randTarget = char1Target;
                    }
                    if (currentInQueue == BattleState.CONFCHAR2)
                    {
                        char2Target = Engine.e.GetRandomRemainingPartyMember();
                        randTarget = char2Target;
                    }
                    if (currentInQueue == BattleState.CONFCHAR3)
                    {
                        char3Target = Engine.e.GetRandomRemainingPartyMember();
                        randTarget = char3Target;
                    }

                    activeParty.activeParty[randTarget].GetComponent<Character>().TakePhysicalDamage(randTarget, Engine.e.activeParty.activeParty[index].GetComponent<Character>().strength);
                    attackingTeam = true;

                }
                else
                {
                    if (currentInQueue == BattleState.CONFCHAR1)
                    {
                        char1Target = enemyGroup.GetRandomRemainingEnemy();
                        randTarget = char1Target;
                    }
                    if (currentInQueue == BattleState.CONFCHAR2)
                    {
                        char2Target = enemyGroup.GetRandomRemainingEnemy();
                        randTarget = char2Target;
                    }
                    if (currentInQueue == BattleState.CONFCHAR3)
                    {
                        char3Target = enemyGroup.GetRandomRemainingEnemy();
                        randTarget = char3Target;
                    }
                }

                Debug.Log("Team Choice: " + teamTargetChoice + " Index: " + randTarget);
                StartCoroutine(CharConfuseAttack());
            }   // Enemy is attacking
            else
            {

                if (teamTargetChoice == 0) // Enemy is attacking Team
                {
                    if (currentInQueue == BattleState.ENEMY1TURN)
                    {
                        enemy1AttackTarget = Engine.e.GetRandomRemainingPartyMember();
                        randTarget = enemy1AttackTarget;
                    }
                    if (currentInQueue == BattleState.ENEMY2TURN)
                    {
                        enemy2AttackTarget = Engine.e.GetRandomRemainingPartyMember();
                        randTarget = enemy2AttackTarget;

                    }
                    if (currentInQueue == BattleState.ENEMY3TURN)
                    {
                        enemy3AttackTarget = Engine.e.GetRandomRemainingPartyMember();
                        randTarget = enemy3AttackTarget;

                    }
                    if (currentInQueue == BattleState.ENEMY4TURN)
                    {
                        enemy4AttackTarget = Engine.e.GetRandomRemainingPartyMember();
                        randTarget = enemy4AttackTarget;
                    }

                    activeParty.activeParty[randTarget].GetComponent<Character>().TakePhysicalDamage(randTarget, enemies[index].GetComponent<Enemy>().strength);
                }   // Enemy is attacking themeselves
                else
                {
                    if (currentInQueue == BattleState.ENEMY1TURN)
                    {
                        enemy1AttackTarget = enemyGroup.GetRandomRemainingEnemy();
                        randTarget = enemy1AttackTarget;
                    }
                    if (currentInQueue == BattleState.ENEMY2TURN)
                    {
                        enemy2AttackTarget = enemyGroup.GetRandomRemainingEnemy();
                        randTarget = enemy2AttackTarget;

                    }
                    if (currentInQueue == BattleState.ENEMY3TURN)
                    {
                        enemy3AttackTarget = enemyGroup.GetRandomRemainingEnemy();
                        randTarget = enemy3AttackTarget;

                    }
                    if (currentInQueue == BattleState.ENEMY4TURN)
                    {
                        enemy4AttackTarget = enemyGroup.GetRandomRemainingEnemy();
                        randTarget = enemy4AttackTarget;

                    }
                    attackingTeam = true;

                }
                Debug.Log("Team Choice: " + teamTargetChoice + " Index: " + randTarget);

                StartCoroutine(EnemyConfuseAttack());
            }
        }
    }

    public void TargetTeam()
    {
        if (state == BattleState.CHAR1TURN)
        {
            if (!char1TargetingTeam)
            {
                char1TargetingTeam = true;
                char1TargetingEnemy = false;
            }
        }

        if (state == BattleState.CHAR2TURN)
        {
            if (!char2TargetingTeam)
            {
                char2TargetingTeam = true;
                char2TargetingEnemy = false;
            }
        }

        if (state == BattleState.CHAR3TURN)
        {
            if (!char3TargetingTeam)
            {
                char3TargetingTeam = true;
                char3TargetingEnemy = false;
            }
        }
    }

    public void TargetEnemy()
    {
        if (state == BattleState.CHAR1TURN)
        {
            if (!char1TargetingEnemy)
            {
                char1TargetingEnemy = true;
                char1TargetingTeam = false;
            }
        }

        if (state == BattleState.CHAR2TURN)
        {
            if (!char2TargetingEnemy)
            {
                char2TargetingEnemy = true;
                char2TargetingTeam = false;
            }
        }

        if (state == BattleState.CHAR3TURN)
        {
            if (!char3TargetingEnemy)
            {
                char3TargetingEnemy = true;
                char3TargetingTeam = false;
            }
        }
    }

    public void ClearTarget()
    {
        targetingTeam = false;
        targetingEnemy = false;
    }

    // To keep track of where we are targeting (for UI navigation)
    public void SetMenuTargetIndex(float index)
    {
        menuTargetIndex = index;
    }

    public void SetTarget(int _target)
    {
        if (state == BattleState.CHAR1TURN)
        {
            if (char1PhysicalAttack)
            {
                char1Attacking = true;
            }

            char1Target = _target;
            char1Ready = false;

            battleQueue.Enqueue(BattleState.CHAR1TURN);

            if (activeParty.activeParty[1] != null)
            {
                if (char2Ready)
                {
                    ActivateChar2MenuButtons();
                }
            }

            if (activeParty.activeParty[2] != null)
            {
                if (char3Ready)
                {
                    ActivateChar3MenuButtons();
                }
            }

        }

        if (state == BattleState.CHAR2TURN)
        {
            if (char2PhysicalAttack)
            {
                char2Attacking = true;
            }

            char2Target = _target;
            char2Ready = false;

            battleQueue.Enqueue(BattleState.CHAR2TURN);


            if (char1Ready)
            {
                ActivateChar1MenuButtons();
            }


            if (activeParty.activeParty[2] != null)
            {
                if (char3Ready)
                {
                    ActivateChar3MenuButtons();
                }
            }
        }

        if (state == BattleState.CHAR3TURN)
        {
            if (char3PhysicalAttack)
            {
                char3Attacking = true;
            }

            char3Target = _target;
            char3Ready = false;

            DeactivateChar3MenuButtons();
            battleQueue.Enqueue(BattleState.CHAR3TURN);

            if (char1Ready)
            {
                ActivateChar1MenuButtons();
            }

            if (char2Ready)
            {
                ActivateChar2MenuButtons();
            }


        }

        DeactivateTargetButtons();
        DeactivateTargetSprite();
        state = BattleState.ATBCHECK;

    }

    public void ActivateAvailableCharSwitchButtons()
    {
        availableCharSwitchButtons.SetActive(true);

        DeactivateChar1MenuButtons();
        DeactivateChar2MenuButtons();
        DeactivateChar3MenuButtons();

        for (int i = 0; i < switchButtons.Length; i++)
        {
            switchButtons[i].GetComponent<CharacterSwitchUIHolder>().character = null;
            switchButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        }

        for (int i = 0; i < Engine.e.party.Length; i++)
        {
            if (Engine.e.party[i] != null && Engine.e.party[i].GetComponent<Character>().activePartyIndex == -1
            && !Engine.e.party[i].GetComponent<Character>().inSwitchQueue)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (switchButtons[k].GetComponent<CharacterSwitchUIHolder>().character == null)
                    {
                        switchButtons[k].GetComponent<CharacterSwitchUIHolder>().character = Engine.e.party[i].GetComponent<Character>();
                        Engine.e.party[i].GetComponent<Character>().inSwitchQueue = true;
                        switchButtons[k].GetComponentInChildren<TextMeshProUGUI>().text = Engine.e.party[i].GetComponent<Character>().characterName;
                        switchButtons[k].SetActive(true);
                        break;
                    }
                }
            }
        }

        if (!char1SkillSwitch && !char2SkillSwitch && !char3SkillSwitch)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(switchButtons[0]);
        }
    }

    public void ActivateChar1MenuButtons()
    {
        if (!isDead)
        {
            if (!char1MenuButtonsActive)
            {
                for (int i = 0; i < char1MenuButtons.Length; i++)
                {
                    char1MenuButtons[i].SetActive(true);

                    if (battleSwitchButtons == false)
                    {
                        char1MenuButtons[4].SetActive(false);
                    }
                    if (activeParty.activeParty[0].GetComponent<Character>().miterInflicted)
                    {
                        char1MenuButtons[3].SetActive(false);

                    }
                    if (activeParty.activeParty[0].GetComponent<Character>().haltInflicted)
                    {
                        char1MenuButtons[0].SetActive(false);
                        char1MenuButtons[4].SetActive(false);
                    }
                }
            }
        }
    }
    public void ActivateChar2MenuButtons()
    {
        if (!isDead)
        {
            if (!char2MenuButtonsActive)
            {
                for (int i = 0; i < char2MenuButtons.Length; i++)
                {
                    char2MenuButtons[i].SetActive(true);
                    if (battleSwitchButtons == false)
                    {
                        char2MenuButtons[4].SetActive(false);
                    }
                    if (activeParty.activeParty[1].GetComponent<Character>().miterInflicted)
                    {
                        char2MenuButtons[3].SetActive(false);

                    }
                    if (activeParty.activeParty[1].GetComponent<Character>().haltInflicted)
                    {
                        char2MenuButtons[0].SetActive(false);
                        char2MenuButtons[4].SetActive(false);
                    }
                }
            }
        }
    }

    public void ActivateChar3MenuButtons()
    {
        if (!isDead)
        {
            if (!char3MenuButtonsActive)
            {
                for (int i = 0; i < char3MenuButtons.Length; i++)
                {
                    char3MenuButtons[i].SetActive(true);
                    if (battleSwitchButtons == false)
                    {
                        char3MenuButtons[4].SetActive(false);
                    }
                    if (activeParty.activeParty[2].GetComponent<Character>().miterInflicted)
                    {
                        char3MenuButtons[3].SetActive(false);
                    }
                    if (activeParty.activeParty[2].GetComponent<Character>().haltInflicted)
                    {
                        char3MenuButtons[0].SetActive(false);
                        char3MenuButtons[4].SetActive(false);
                    }
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
    public void DeactivateCharSwitchButtons()
    {

    }

    public void SetPhysicalAttack()
    {
        if (state == BattleState.CHAR1TURN)
        {
            char1PhysicalAttack = true;
        }
        if (state == BattleState.CHAR2TURN)
        {
            char2PhysicalAttack = true;
        }
        if (state == BattleState.CHAR3TURN)
        {
            char3PhysicalAttack = true;
        }
    }

    public void ActivateTargetButtons()
    {

        inBattleMenu = true;
        DeactivateChar1MenuButtons();
        DeactivateChar2MenuButtons();
        DeactivateChar3MenuButtons();

        if (char1PhysicalAttack || char2PhysicalAttack || char3PhysicalAttack
                   || char1DropAttack || char2DropAttack || char3DropAttack)
        {
            for (int i = 0; i < allyTargetButtons.Length; i++)
            {
                if (activeParty.activeParty[i] != null)
                {

                    if (activeParty.activeParty[i].GetComponent<Character>().currentHealth > 0)
                    {
                        allyTargetButtons[i].SetActive(true);
                    }
                }
            }


            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                {
                    if (enemies[i].currentHealth > 0)
                    {
                        enemyTargetButtons[i].SetActive(true);
                    }
                }
            }

            GetComponent<BattleMenuControllerNav>().OpenAttackFirstEnemy();

        }

        if (char1Supporting || char2Supporting || char3Supporting)
        {
            if (!char1SkillSelfSupport && !char2SkillSelfSupport && !char3SkillSelfSupport)
            {
                if (!targetAll)
                {
                    for (int i = 0; i < allyTargetButtons.Length; i++)
                    {
                        if (activeParty.activeParty[i] != null)
                        {
                            allyTargetButtons[i].SetActive(true);
                        }
                    }

                    for (int i = 0; i < enemies.Length; i++)
                    {
                        if (enemies[i] != null)
                        {
                            if (enemies[i].currentHealth > 0)
                            {
                                enemyTargetButtons[i].SetActive(true);
                            }
                        }
                    }

                    if (state == BattleState.CHAR1TURN)
                    {
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(allyTargetButtons[0]);
                    }
                    if (state == BattleState.CHAR2TURN)
                    {
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(allyTargetButtons[1]);
                    }
                    if (state == BattleState.CHAR3TURN)
                    {
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(allyTargetButtons[2]);
                    }
                }
                else
                {
                    if (state == BattleState.CHAR1TURN)
                    {
                        allyTargetButtons[0].SetActive(true);
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(allyTargetButtons[0]);
                    }
                    if (state == BattleState.CHAR2TURN)
                    {
                        allyTargetButtons[1].SetActive(true);
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(allyTargetButtons[1]);
                    }
                    if (state == BattleState.CHAR3TURN)
                    {
                        allyTargetButtons[2].SetActive(true);
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(allyTargetButtons[2]);
                    }

                    for (int i = 0; i < enemies.Length; i++)
                    {
                        if (enemies[i] != null)
                        {
                            if (enemies[i].currentHealth > 0)
                            {
                                enemyTargetButtons[i].SetActive(true);
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                if (state == BattleState.CHAR1TURN)
                {
                    allyTargetButtons[0].SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(allyTargetButtons[0]);
                }
                if (state == BattleState.CHAR2TURN)
                {
                    allyTargetButtons[1].SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(allyTargetButtons[1]);
                }
                if (state == BattleState.CHAR3TURN)
                {
                    allyTargetButtons[2].SetActive(true);
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(allyTargetButtons[2]);
                }
            }
        }

        if (char1SkillAttack || char2SkillAttack || char3SkillAttack)
        {
            if (!targetAll)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i] != null)
                    {
                        if (enemies[i].currentHealth > 0)
                        {
                            enemyTargetButtons[i].SetActive(true);
                        }
                    }
                }
                GetComponent<BattleMenuControllerNav>().OpenAttackFirstEnemy();
            }
            else
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i] != null)
                    {
                        if (enemies[i].currentHealth > 0)
                        {
                            enemyTargetButtons[i].SetActive(true);
                            EventSystem.current.SetSelectedGameObject(null);
                            EventSystem.current.SetSelectedGameObject(enemyTargetButtons[i]);
                            break;
                        }
                    }
                }
            }
        }

        if (charSkillSwitchCheck)
        {
            charSkillSwitchCheck = false;
            for (int i = 0; i < allyTargetButtons.Length; i++)
            {
                if (activeParty.activeParty[i].GetComponent<Character>().currentHealth > 0 &&
                !activeParty.activeParty[i].GetComponent<Character>().isAsleep
                && !activeParty.activeParty[i].GetComponent<Character>().isConfused)

                {
                    allyTargetButtons[i].SetActive(true);
                }

                if (state == BattleState.CHAR1TURN)
                {
                    allyTargetButtons[0].SetActive(false);
                }
                if (state == BattleState.CHAR2TURN)
                {
                    allyTargetButtons[1].SetActive(false);
                }
                if (state == BattleState.CHAR3TURN)
                {
                    allyTargetButtons[2].SetActive(false);
                }
            }

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(switchButtons[0]);
        }

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
    }

    public void ActivateAllyTargetSprite(int index)
    {
        if (!targetAll)
        {
            if (index == 0)
            {
                Vector3 position = new Vector3(Engine.e.activeParty.transform.position.x - 0.5f, Engine.e.activeParty.transform.position.y, Engine.e.activeParty.transform.position.z);
                targetSprite[0].transform.position = position;
            }
            if (index == 1)
            {
                Vector3 position = new Vector3(Engine.e.activePartyMember2.transform.position.x - 0.5f, Engine.e.activePartyMember2.transform.position.y, Engine.e.activePartyMember2.transform.position.z);
                targetSprite[1].transform.position = position;
            }
            if (index == 2)
            {
                Vector3 position = new Vector3(Engine.e.activePartyMember3.transform.position.x - 0.5f, Engine.e.activePartyMember3.transform.position.y, Engine.e.activePartyMember3.transform.position.z);
                targetSprite[2].transform.position = position;
            }

            targetSprite[index].SetActive(true);
        }
        else
        {
            ActivateTargetSpriteTeam();
        }
    }

    public void ActivateTargetSpriteTeam()
    {

        Vector3 position1 = new Vector3(Engine.e.activeParty.transform.position.x - 0.5f, Engine.e.activeParty.transform.position.y, Engine.e.activeParty.transform.position.z);
        targetSprite[0].transform.position = position1;

        Vector3 position2 = new Vector3(Engine.e.activePartyMember2.transform.position.x - 0.5f, Engine.e.activePartyMember2.transform.position.y, Engine.e.activePartyMember2.transform.position.z);
        targetSprite[1].transform.position = position2;

        Vector3 position3 = new Vector3(Engine.e.activePartyMember3.transform.position.x - 0.5f, Engine.e.activePartyMember3.transform.position.y, Engine.e.activePartyMember3.transform.position.z);
        targetSprite[2].transform.position = position3;

        if (activeParty.activeParty[0].GetComponent<Character>().currentHealth > 0)
        {
            targetSprite[0].SetActive(true);
        }
        if (activeParty.activeParty[1] != null && activeParty.activeParty[1].GetComponent<Character>().currentHealth > 0)
        {
            targetSprite[1].SetActive(true);
        }
        if (activeParty.activeParty[2] != null && activeParty.activeParty[2].GetComponent<Character>().currentHealth > 0)
        {
            targetSprite[2].SetActive(true);
        }
    }

    public void ActivateEnemyTargetSprite(int index)
    {
        if (!targetAll)
        {
            Vector3 position = new Vector3(enemies[index].transform.position.x - 0.5f, enemies[index].transform.position.y, enemies[index].transform.position.z);
            targetSprite[3].transform.position = position;
            targetSprite[3].SetActive(true);
        }
        else
        {
            ActivateTargetSpriteEnemiesAll();
        }
    }

    public void ActivateTargetSpriteEnemiesAll()
    {

        Vector3 position1 = new Vector3(enemies[0].transform.position.x - 0.5f, enemies[0].transform.transform.position.y, enemies[0].transform.transform.position.z);
        targetSprite[3].transform.position = position1;

        if (enemies[1] != null)
        {
            Vector3 position2 = new Vector3(enemies[1].transform.position.x - 0.5f, enemies[1].transform.transform.position.y, enemies[1].transform.transform.position.z);
            targetSprite[4].transform.position = position2;
        }

        if (enemies[2] != null)
        {
            Vector3 position3 = new Vector3(enemies[2].transform.position.x - 0.5f, enemies[2].transform.transform.position.y, enemies[2].transform.transform.position.z);
            targetSprite[5].transform.position = position3;
        }

        if (enemies[3] != null)
        {
            Vector3 position4 = new Vector3(enemies[3].transform.position.x - 0.5f, enemies[3].transform.transform.position.y, enemies[3].transform.transform.position.z);
            targetSprite[6].transform.position = position4;
        }

        if (enemies[0].currentHealth > 0)
        {
            targetSprite[3].SetActive(true);
        }
        if (enemies[1] != null && enemies[1].currentHealth > 0)
        {
            targetSprite[4].SetActive(true);
        }
        if (enemies[2] != null && enemies[2].currentHealth > 0)
        {
            targetSprite[5].SetActive(true);
        }
        if (enemies[3] != null && enemies[3].currentHealth > 0)
        {
            targetSprite[6].SetActive(true);
        }
    }

    public void DeactivateTargetSprite()
    {
        for (int i = 0; i < targetSprite.Length; i++)
        {
            targetSprite[i].SetActive(false);
        }
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
        DeactivateTargetButtons();
        DeactivateBattleItems();

        if (state == BattleState.CHAR1TURN)
        {
            char1PhysicalAttack = false;
            char1DropSupport = false;
            char1DropAttack = false;
            char1UsingItem = false;
            char1TargetingEnemy = false;
            char1TargetingTeam = false;
            char1SkillPhysicalAttack = false;
            char1SkillRangedAttack = false;
            char1SkillSelfSupport = false;
            char1SkillTargetSupport = false;
            char1SkillAttack = false;
            char1Attacking = false;
            char1Supporting = false;
            char1Switching = false;
            //char1DropChoice = null;
            //char1SkillChoice = null;
        }

        if (state == BattleState.CHAR2TURN)
        {
            char2PhysicalAttack = false;
            char2DropSupport = false;
            char2DropAttack = false;
            char2UsingItem = false;
            char2TargetingEnemy = false;
            char2TargetingTeam = false;
            char2SkillPhysicalAttack = false;
            char2SkillRangedAttack = false;
            char2SkillSelfSupport = false;
            char2SkillTargetSupport = false;
            char2SkillAttack = false;
            char2Attacking = false;
            char2Supporting = false;
            char2Switching = false;
            //char2DropChoice = null;
            //char2SkillChoice = null;
        }

        if (state == BattleState.CHAR3TURN)
        {
            char3PhysicalAttack = false;
            char3DropSupport = false;
            char3DropAttack = false;
            char3UsingItem = false;
            char3TargetingEnemy = false;
            char3TargetingTeam = false;
            char3SkillPhysicalAttack = false;
            char3SkillRangedAttack = false;
            char3SkillSelfSupport = false;
            char3SkillTargetSupport = false;
            char3SkillAttack = false;
            char3Attacking = false;
            char3Supporting = false;
            char3Switching = false;
            //char3DropChoice = null;
            //char3SkillChoice = null;
        }

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

    public void DeactivateTargetButtons()
    {
        targetAll = false;

        for (int i = 0; i < Engine.e.activeParty.activeParty.Length; i++)
        {
            if (Engine.e.activeParty.activeParty[i] != null)
            {
                returnToPanelButton[i].SetActive(false);
                allyTargetButtons[i].SetActive(false);
            }
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemyTargetButtons[i].SetActive(false);
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

            if (activeParty.activeParty[1] != null)
            {
                if (char2Ready)
                {
                    DeactivateChar2MenuButtons();
                }
            }

            if (activeParty.activeParty[2] != null)
            {
                if (char3Ready)
                {
                    DeactivateChar3MenuButtons();
                }
            }


        }
        if (state == BattleState.CHAR2TURN)
        {
            DeactivateChar2MenuButtons();


            if (char1Ready)
            {
                DeactivateChar1MenuButtons();
            }


            if (activeParty.activeParty[2] != null)
            {
                if (char3Ready)
                {
                    DeactivateChar3MenuButtons();
                }
            }
        }
        if (state == BattleState.CHAR3TURN)
        {
            DeactivateChar3MenuButtons();

            if (char1Ready)
            {
                DeactivateChar1MenuButtons();
            }



            if (char2Ready)
            {
                DeactivateChar2MenuButtons();

            }
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

            if (activeParty.activeParty[1] != null)
            {
                if (char2Ready)
                {
                    ActivateChar2MenuButtons();
                }
            }
            if (activeParty.activeParty[2] != null)
            {
                if (char3Ready)
                {
                    ActivateChar3MenuButtons();
                }
            }
        }
        if (state == BattleState.CHAR2TURN)
        {
            ActivateChar2MenuButtons();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(char2MenuButtons[0]);


            if (char1Ready)
            {
                ActivateChar1MenuButtons();

            }
            if (activeParty.activeParty[2] != null)
            {
                if (char3Ready)
                {
                    ActivateChar3MenuButtons();
                }
            }
        }
        if (state == BattleState.CHAR3TURN)
        {
            ActivateChar3MenuButtons();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(char3MenuButtons[0]);


            if (char1Ready)
            {
                ActivateChar1MenuButtons();
            }


            if (char2Ready)
            {
                ActivateChar2MenuButtons();

            }
        }

    }

    public void ActivateSupportButtons()
    {
        Engine.e.battleSystem.enemyPanel.SetActive(true);

        dropsListReference.SetActive(false);
        char1BattlePanel.SetActive(true);
        DeactivateChar1MenuButtons();
        if (Engine.e.activeParty.activeParty[1] != null)
        {
            char2BattlePanel.SetActive(true);
            DeactivateChar2MenuButtons();

        }
        if (Engine.e.activeParty.activeParty[2] != null)
        {
            char3BattlePanel.SetActive(true);
            DeactivateChar3MenuButtons();

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
        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (skillButtons[i].GetComponent<BattleSkillsUIHolder>().skill != null)
            {
                skillButtons[i].GetComponent<BattleSkillsUIHolder>().SetSkillText();
            }
        }
    }

    public void DeactivateSkillsUI()
    {
        char1BattlePanel.SetActive(true);

        if (Engine.e.activeParty.activeParty[1] != null)
        {
            {
                char2BattlePanel.SetActive(true);
            }
        }
        if (Engine.e.activeParty.activeParty[2] != null)
        {
            char3BattlePanel.SetActive(true);
        }
        skillListReference.SetActive(false);
        for (int i = 0; i < Engine.e.gameSkills.Length; i++)
        {
            if (Engine.e.gameSkills[i] != null)
            {
                skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "-";
            }
        }

    }

    public void ActivateCharDropsUI()
    {
        for (int i = 0; i < dropsButtons.Length; i++)
        {
            if (dropsButtons[i].GetComponent<BattleDropsUIHolder>().drop != null)
            {
                dropsButtons[i].GetComponent<BattleDropsUIHolder>().SetDropText();
            }
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


        if (Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Character>().currentMana >= dropChoice.dropCost)
        {
            lastDropChoice = dropChoice;
            Engine.e.battleSystem.enemyPanel.SetActive(true);

            Engine.e.activeParty.activeParty[index].gameObject.GetComponent<Character>().UseDrop(dropChoice);

            if (index == 0)
            {
                char1DropChoice = dropChoice;
                DeactivateChar1MenuButtons();
            }
            if (index == 1)
            {
                char2DropChoice = dropChoice;
                DeactivateChar2MenuButtons();
            }
            if (index == 2)
            {
                char3DropChoice = dropChoice;
                DeactivateChar3MenuButtons();
            }
            DeactivateDropsUI();

        }
        // }

        else
        {
            return;
        }

    }

    public void SkillChoice(Skills skillChoice)
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

        if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().haltInflicted && skillChoice.physicalDps)
        {
            return;
        }
        else
        {
            if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().currentEnergy >= skillChoice.skillCost)
            {
                lastSkillChoice = skillChoice;

                Engine.e.battleSystem.enemyPanel.SetActive(true);

                Engine.e.activeParty.activeParty[index].GetComponent<Character>().UseSkill(skillChoice);

                if (index == 0)
                {
                    char1SkillChoice = skillChoice;
                    DeactivateChar1MenuButtons();
                }
                if (index == 1)
                {
                    char2SkillChoice = skillChoice;
                    DeactivateChar2MenuButtons();
                }
                if (index == 2)
                {
                    char3SkillChoice = skillChoice;
                    DeactivateChar3MenuButtons();
                }

                DeactivateSkillsUI();
            }
            else
            {
                return;
            }

        }
    }

    // General check for enemies, mainly regarding poison and death damage (if they die, should the battle end)
    public bool CheckIsDeadEnemy()
    {
        if (enemies[3] != null)
        {
            if (enemies[3].GetComponent<Enemy>().currentHealth == 0 && enemies[2].GetComponent<Enemy>().currentHealth == 0
            && enemies[1].GetComponent<Enemy>().currentHealth == 0 && enemies[0].GetComponent<Enemy>().currentHealth == 0)
            {
                return true;
            }
        }

        if (enemies[3] == null)
        {
            if (enemies[2] != null)
            {
                if (enemies[2].GetComponent<Enemy>().currentHealth == 0 && enemies[1].GetComponent<Enemy>().currentHealth == 0
                && enemies[0].GetComponent<Enemy>().currentHealth == 0)
                {
                    return true;
                }
            }
        }

        if (enemies[3] == null && enemies[2] == null)
        {
            if (enemies[1] != null)
            {
                if (enemies[1].GetComponent<Enemy>().currentHealth == 0 && enemies[0].GetComponent<Enemy>().currentHealth == 0)
                {
                    return true;
                }
            }
        }
        if (enemies[3] == null && enemies[2] == null && enemies[1] == null)
        {

            if (enemies[0].GetComponent<Enemy>().currentHealth == 0)
            {
                return true;
            }
        }
        return false;
    }


    public void EndTurn()
    {
        charMoving = false;
        targetCheck = false;
        attackingTeam = false;
        lastDropChoice = null;  // Test this
        lastSkillChoice = null; // Test this
                                //targetAll = false;
        GameObject _characterAttacking = null;
        int index = 0;

        if (partyTurn)
        {
            isDead = CheckIsDeadEnemy();

            partyTurn = false;

            if (currentInQueue == BattleState.CHAR1TURN || currentInQueue == BattleState.CONFCHAR1)
            {
                index = 0;

                char1PhysicalAttack = false;
                char1DropSupport = false;
                char1DropAttack = false;
                char1UsingItem = false;
                char1TargetingEnemy = false;
                char1TargetingTeam = false;
                char1SkillPhysicalAttack = false;
                char1SkillRangedAttack = false;
                char1SkillSelfSupport = false;
                char1SkillTargetSupport = false;
                char1SkillAttack = false;
                char1Attacking = false;
                char1Supporting = false;
                char1Ready = false;
                char1ConfusedReady = false;
                char1Switching = false;
                char1ItemToBeUsed = null;

                confuseAttack = false;

                physicalAttack = false;
                dropAttack = false;
                dropSupport = false;
                skillPhysicalAttack = false;
                skillRangedAttack = false;
                skillSelfSupport = false;
                skillTargetSupport = false;
                charUsingSkill = false;


                //char1DropChoice = null;
                //char1SkillChoice = null;

                if (!charSkillSwitch)
                {
                    char1ATB = 0;
                    char1ATBGuage.value = char1ATB;
                }

                charSkillSwitch = false;

                _characterAttacking = Engine.e.activeParty.gameObject;
            }

            if (currentInQueue == BattleState.CHAR2TURN || currentInQueue == BattleState.CONFCHAR2)
            {
                index = 1;

                char2PhysicalAttack = false;
                char2DropSupport = false;
                char2DropAttack = false;
                char2UsingItem = false;
                char2TargetingEnemy = false;
                char2TargetingTeam = false;
                char2SkillPhysicalAttack = false;
                char2SkillRangedAttack = false;
                char2SkillSelfSupport = false;
                char2SkillTargetSupport = false;
                char2SkillAttack = false;
                char2Attacking = false;
                char2Supporting = false;
                char2Ready = false;
                char2ConfusedReady = false;
                char2Switching = false;
                char2ItemToBeUsed = null;

                physicalAttack = false;
                dropAttack = false;
                dropSupport = false;
                skillPhysicalAttack = false;
                skillRangedAttack = false;
                skillSelfSupport = false;
                skillTargetSupport = false;
                charUsingSkill = false;




                if (!charSkillSwitch)
                {
                    char2ATB = 0;
                    char2ATBGuage.value = char1ATB;
                }

                charSkillSwitch = false;

                _characterAttacking = Engine.e.activePartyMember2;

            }

            if (currentInQueue == BattleState.CHAR3TURN || currentInQueue == BattleState.CONFCHAR3)
            {
                index = 2;

                char3PhysicalAttack = false;
                char3DropSupport = false;
                char3DropAttack = false;
                char3UsingItem = false;
                char3TargetingEnemy = false;
                char3TargetingTeam = false;
                char3SkillPhysicalAttack = false;
                char3SkillRangedAttack = false;
                char3SkillSelfSupport = false;
                char3SkillTargetSupport = false;
                char3SkillAttack = false;
                char3Attacking = false;
                char3Supporting = false;
                char3Ready = false;
                char3ConfusedReady = false;
                char3Switching = false;
                char3ItemToBeUsed = null;

                physicalAttack = false;
                dropAttack = false;
                dropSupport = false;
                skillPhysicalAttack = false;
                skillRangedAttack = false;
                skillSelfSupport = false;
                skillTargetSupport = false;
                charUsingSkill = false;


                if (!charSkillSwitch)
                {
                    char3ATB = 0;
                    char3ATBGuage.value = char1ATB;
                }

                charSkillSwitch = false;

                _characterAttacking = Engine.e.activePartyMember3;

            }

            if (activeParty.activeParty[index].GetComponent<Character>().isPoisoned)
            {
                isDead = activeParty.activeParty[index].GetComponent<Character>().TakePoisonDamage(activeParty.activeParty[index].GetComponent<Character>().poisonDmg);
            }

            if (activeParty.activeParty[index].GetComponent<Character>().deathInflicted)
            {
                isDead = activeParty.activeParty[index].GetComponent<Character>().TakeDeathDamage();
            }

            if (Engine.e.activeParty.activeParty[index].GetComponent<Character>().currentHealth <= 0)
            {
                Engine.e.activeParty.activeParty[index].GetComponent<Character>().currentHealth = 0;

                if (isDead)
                {
                    state = BattleState.LOST;
                    StartCoroutine(EndBattle());
                }
            }

            if (activeParty.activeParty[index].GetComponent<Character>().isConfused)
            {
                confuseAttack = false;
                Debug.Log("Adding confuse timer");
                activeParty.activeParty[index].GetComponent<Character>().confuseTimer++;
                if (activeParty.activeParty[index].GetComponent<Character>().confuseTimer == 3)
                {
                    activeParty.activeParty[index].GetComponent<Character>().isConfused = false;
                    activeParty.activeParty[index].GetComponent<Character>().inflicted = false;
                    activeParty.activeParty[index].GetComponent<Character>().confuseTimer = 0;
                }
            }
        }
        else
        {
            if (currentInQueue == BattleState.ENEMY1TURN)
            {
                index = 0;

                enemy1Ready = false;
                enemy1ATB = 0;
                enemy1ATBGuage.value = enemy1ATB;
            }
            if (currentInQueue == BattleState.ENEMY2TURN)
            {
                index = 1;

                enemy2Ready = false;
                enemy2ATB = 0;
                enemy2ATBGuage.value = enemy2ATB;
            }
            if (currentInQueue == BattleState.ENEMY3TURN)
            {
                index = 2;

                enemy3Ready = false;
                enemy3ATB = 0;
                enemy3ATBGuage.value = enemy3ATB;
            }
            if (currentInQueue == BattleState.ENEMY4TURN)
            {
                index = 3;

                enemy4Ready = false;
                enemy4ATB = 0;
                enemy4ATBGuage.value = enemy4ATB;
            }

            if (enemies[index].GetComponent<Enemy>().isConfused)
            {
                enemies[index].GetComponent<Enemy>().confuseTimer++;
                if (enemies[index].GetComponent<Enemy>().confuseTimer == 3)
                {
                    enemies[index].GetComponent<Enemy>().isConfused = false;
                    enemies[index].GetComponent<Enemy>().inflicted = false;
                    enemies[index].GetComponent<Enemy>().confuseTimer = 0;
                }
            }

            if (enemies[index].GetComponent<Enemy>().isPoisoned)
            {
                enemies[index].GetComponent<Enemy>().TakePoisonDamage(index, enemies[index].GetComponent<Enemy>().poisonDmg);
                isDead = CheckIsDeadEnemy();

            }

            if (enemies[index].GetComponent<Enemy>().deathInflicted)
            {
                enemies[index].GetComponent<Enemy>().TakeDeathDamage();
                isDead = CheckIsDeadEnemy();

            }
            if (enemies[index].GetComponent<EnemyMovement>() != null)
            {
                enemies[index].GetComponent<EnemyMovement>().enabled = true;
            }

            //CheckIsDeadTeam
        }

        targetingTeam = false;
        targetingEnemy = false;
        /* for (int i = 0; i < damagePopup.Length; i++)
         {
             damagePopup[i].SetActive(false);
         }
    */
        if (battleQueue.Count > 0)
            battleQueue.Dequeue();

        if (battleQueue.Count > 0)
        {
            currentInQueue = battleQueue.Peek();
        }
        else
        {
            currentInQueue = BattleState.ATBCHECK;
        }

        // state = BattleState.ATBCHECK;
    }

    public void ResetPartyMemberStats()
    {

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

        char1Attacking = false;
        char1ConfusedReady = false;
        char1DropAttack = false;
        char1PhysicalAttack = false;
        char1Ready = false;
        char1SkillAttack = false;
        char1SkillPhysicalAttack = false;
        char1SkillRangedAttack = false;
        char1Supporting = false;
        char1UsingItem = false;
        char1Switching = false;
        char1SkillSwitch = false;

        char2Attacking = false;
        char2ConfusedReady = false;
        char2DropAttack = false;
        char2PhysicalAttack = false;
        char2Ready = false;
        char2SkillAttack = false;
        char2SkillPhysicalAttack = false;
        char2SkillRangedAttack = false;
        char2Supporting = false;
        char2UsingItem = false;
        char2Switching = false;
        char2SkillSwitch = false;

        char3Attacking = false;
        char3ConfusedReady = false;
        char3DropAttack = false;
        char3PhysicalAttack = false;
        char3Ready = false;
        char3SkillAttack = false;
        char3SkillPhysicalAttack = false;
        char3SkillRangedAttack = false;
        char3Supporting = false;
        char3UsingItem = false;
        char3Switching = false;
        char3SkillSwitch = false;

        charSkillSwitchCheck = false;

        Engine.e.activeParty.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().color = Color.white;
        Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().color = Color.white;


        for (int i = 0; i < Engine.e.party.Length; i++)
        {
            if (Engine.e.party[i] != null)
            {
                //Engine.e.party[i].GetComponent<Character>().isPoisoned = false;
                //Engine.e.party[i].GetComponent<Character>().isAsleep = false;
                //Engine.e.party[i].GetComponent<Character>().isConfused = false;
                //Engine.e.party[i].GetComponent<Character>().miterInflicted = false;
                //Engine.e.party[i].GetComponent<Character>().haltInflicted = false;

                // Engine.e.party[i].GetComponent<Character>().inflicted = false;
                //Engine.e.party[i].GetComponent<Character>().poisonDmg = 0;
                //Engine.e.party[i].GetComponent<Character>().sleepTimer = 0;
                //Engine.e.party[i].GetComponent<Character>().confuseTimer = 0;

                // Negative Status Effects
                Engine.e.party[i].GetComponent<Character>().deathInflicted = false;
                Engine.e.party[i].GetComponent<Character>().deathTimer = 3;

                // Beneficial Status Effects
                Engine.e.party[i].GetComponent<Character>().isProtected = false;
                Engine.e.party[i].GetComponent<Character>().isEncompassed = false;
                Engine.e.party[i].GetComponent<Character>().isHastened = false;

                Engine.e.party[i].GetComponent<SpriteRenderer>().color = Color.white;

                Engine.e.party[i].GetComponent<Character>().maxHealth = Engine.e.party[i].GetComponent<Character>().maxHealthBase;
                Engine.e.party[i].GetComponent<Character>().maxMana = Engine.e.party[i].GetComponent<Character>().maxManaBase;
                Engine.e.party[i].GetComponent<Character>().maxEnergy = Engine.e.party[i].GetComponent<Character>().maxEnergyBase;
                Engine.e.party[i].GetComponent<Character>().strength = Engine.e.party[i].GetComponent<Character>().strengthBase;
                Engine.e.party[i].GetComponent<Character>().intelligence = Engine.e.party[i].GetComponent<Character>().intelligenceBase;
                Engine.e.party[i].GetComponent<Character>().dodgeChance = Engine.e.party[i].GetComponent<Character>().dodgeChanceBase;
                Engine.e.party[i].GetComponent<Character>().critChance = Engine.e.party[i].GetComponent<Character>().critChanceBase;
                Engine.e.party[i].GetComponent<Character>().haste = Engine.e.party[i].GetComponent<Character>().hasteBase;
                Engine.e.party[i].GetComponent<Character>().firePhysicalAttackBonus = Engine.e.party[i].GetComponent<Character>().firePhysicalAttackBonusBase;
                Engine.e.party[i].GetComponent<Character>().icePhysicalAttackBonus = Engine.e.party[i].GetComponent<Character>().icePhysicalAttackBonusBase;
                Engine.e.party[i].GetComponent<Character>().lightningPhysicalAttackBonus = Engine.e.party[i].GetComponent<Character>().lightningPhysicalAttackBonusBase;
                Engine.e.party[i].GetComponent<Character>().waterPhysicalAttackBonus = Engine.e.party[i].GetComponent<Character>().waterPhysicalAttackBonusBase;
                Engine.e.party[i].GetComponent<Character>().shadowPhysicalAttackBonus = Engine.e.party[i].GetComponent<Character>().shadowPhysicalAttackBonusBase;
                Engine.e.party[i].GetComponent<Character>().holyPhysicalAttackBonus = Engine.e.party[i].GetComponent<Character>().holyPhysicalAttackBonusBase;
                Engine.e.party[i].GetComponent<Character>().fireDefense = Engine.e.party[i].GetComponent<Character>().fireDefenseBase;
                Engine.e.party[i].GetComponent<Character>().iceDefense = Engine.e.party[i].GetComponent<Character>().iceDefenseBase;
                Engine.e.party[i].GetComponent<Character>().lightningDefense = Engine.e.party[i].GetComponent<Character>().lightningDefenseBase;
                Engine.e.party[i].GetComponent<Character>().waterDefense = Engine.e.party[i].GetComponent<Character>().waterDefenseBase;
                Engine.e.party[i].GetComponent<Character>().shadowDefense = Engine.e.party[i].GetComponent<Character>().shadowDefenseBase;
                Engine.e.party[i].GetComponent<Character>().holyDefense = Engine.e.party[i].GetComponent<Character>().holyDefenseBase;
                Engine.e.party[i].GetComponent<Character>().sleepDefense = Engine.e.party[i].GetComponent<Character>().sleepDefenseBase;
                Engine.e.party[i].GetComponent<Character>().poisonDefense = Engine.e.party[i].GetComponent<Character>().poisonDefenseBase;
                Engine.e.party[i].GetComponent<Character>().confuseDefense = Engine.e.party[i].GetComponent<Character>().confuseDefenseBase;
                Engine.e.party[i].GetComponent<Character>().deathDefense = Engine.e.party[i].GetComponent<Character>().deathDefenseBase;

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
                        if (char2Ready)
                        {
                            DeactivateSkillsUI();
                            DeactivateDropsUI();
                            Char2Turn();
                        }
                        else
                        {
                            if (char3Ready)
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
                        if (char2Ready)
                        {
                            DeactivateSkillsUI();
                            DeactivateDropsUI();
                            Char2Turn();
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
        Engine.e.battleSystem.enemyPanel.SetActive(true);
        inBattleMenu = false;

        DeactivateTargetSprite();
        DeactivateDropsUI();
        DeactivateSkillsUI();
        DeactivateBattleItems();
        DeactivateSupportButtons();
        DeactivateTargetButtons();

    }

    private void Update()
    {
        ChangeCharState();
        allyTargetButtons[0].transform.position = Engine.e.activeParty.transform.position;
        allyTargetButtons[1].transform.position = Engine.e.activePartyMember2.transform.position;
        allyTargetButtons[2].transform.position = Engine.e.activePartyMember3.transform.position;

        if (enemies[0] != null && enemies[0].GetComponent<Enemy>().currentHealth > 0)
        {
            enemyTargetButtons[0].transform.position = enemies[0].transform.position;
        }
        if (enemies[1] != null && enemies[1].GetComponent<Enemy>().currentHealth > 0)
        {
            enemyTargetButtons[1].transform.position = enemies[1].transform.position;
        }
        if (enemies[2] != null && enemies[2].GetComponent<Enemy>().currentHealth > 0)
        {
            enemyTargetButtons[2].transform.position = enemies[2].transform.position;
        }
        if (enemies[3] != null && enemies[3].GetComponent<Enemy>().currentHealth > 0)
        {
            enemyTargetButtons[3].transform.position = enemies[3].transform.position;
        }

        if (state == BattleState.CHAR1TURN)
        {
            dialogueText.text = activeParty.activeParty[0].GetComponent<Character>().characterName;
        }
        if (state == BattleState.CHAR2TURN)
        {
            dialogueText.text = activeParty.activeParty[1].GetComponent<Character>().characterName;
        }
        if (state == BattleState.CHAR3TURN)
        {
            dialogueText.text = activeParty.activeParty[2].GetComponent<Character>().characterName;
        }

        if (state == BattleState.ATBCHECK)
        {
            // if (currentInQueue == BattleState.QUEUECHECK || currentInQueue == BattleState.ATBCHECK)
            //  {
            dialogueText.text = string.Empty;
            // }
            if (currentInQueue == BattleState.CHAR1TURN || currentInQueue == BattleState.CONFCHAR1)
            {
                dialogueText.text = activeParty.activeParty[0].GetComponent<Character>().characterName;
            }
            if (currentInQueue == BattleState.CHAR2TURN || currentInQueue == BattleState.CONFCHAR2)
            {
                dialogueText.text = activeParty.activeParty[1].GetComponent<Character>().characterName;
            }
            if (currentInQueue == BattleState.CHAR3TURN || currentInQueue == BattleState.CONFCHAR3)
            {
                dialogueText.text = activeParty.activeParty[2].GetComponent<Character>().characterName;
            }

            if (currentInQueue == BattleState.ENEMY1TURN)
            {
                dialogueText.text = enemies[0].GetComponent<Enemy>().name;
            }
            if (currentInQueue == BattleState.ENEMY2TURN)
            {
                dialogueText.text = enemies[1].GetComponent<Enemy>().name;
            }
            if (currentInQueue == BattleState.ENEMY3TURN)
            {
                dialogueText.text = enemies[2].GetComponent<Enemy>().name;
            }
            if (currentInQueue == BattleState.ENEMY4TURN)
            {
                dialogueText.text = enemies[3].GetComponent<Enemy>().name;
            }
        }

        if (Engine.e.battleModeActive)
        {
            if (animExists)
            {
                HandleAnimation();
            }

            if (!animExists && displayDamageText)
            {
                DisplayDamageText();
            }

            if (isDead && state != BattleState.LEVELUPCHECK && state != BattleState.LEVELUP && state != BattleState.WON && state != BattleState.LOST)
            {
                DeactivateTargetSprite();
                DeactivateChar1MenuButtons();

                if (activeParty.activeParty[1] != null)
                {
                    DeactivateChar2MenuButtons();
                }
                if (activeParty.activeParty[2] != null)
                {
                    DeactivateChar3MenuButtons();
                }


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

                if (state == BattleState.LEVELUPCHECK)
                {
                    StartCoroutine(LevelUpCheck());
                }
                if (state == BattleState.LOST)
                {
                    StartCoroutine(EndBattle());
                }

            }
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

        // if ()


        if (activeParty.activeParty[0].GetComponent<Character>().isAsleep || activeParty.activeParty[0].GetComponent<Character>().isConfused)
        {
            if (!activeParty.activeParty[0].GetComponent<Character>().inflicted)
            {
                activeParty.activeParty[0].GetComponent<Character>().inflicted = true;
            }

            if (activeParty.activeParty[0].GetComponent<SpriteRenderer>().color == Color.white && !animExists)
            {
                activeParty.activeParty[0].GetComponent<SpriteRenderer>().color = Color.grey;

            }

            if (char1Ready)
            {
                DeactivateChar1MenuButtons();
            }
        }

        if (activeParty.activeParty[0].GetComponent<Character>().isPoisoned)
        {
            if (!activeParty.activeParty[0].GetComponent<Character>().inflicted)
            {
                activeParty.activeParty[0].GetComponent<Character>().inflicted = true;
            }

            if (activeParty.activeParty[0].GetComponent<SpriteRenderer>().color == Color.white && !animExists)
            {
                activeParty.activeParty[0].GetComponent<SpriteRenderer>().color = Color.green;
            }
        }

        if (!activeParty.activeParty[0].GetComponent<Character>().inflicted)
        {
            if (activeParty.activeParty[0].GetComponent<SpriteRenderer>().color != Color.white && !animExists && currentAnimation[0].GetComponent<Animator>() != Engine.e.gameInventory[3].GetComponent<Animator>())
            {
                activeParty.activeParty[0].GetComponent<SpriteRenderer>().color = Color.white;
            }
            else
            {
                activeParty.activeParty[0].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        if (activeParty.activeParty[1] != null)
        {

            if (activeParty.activeParty[1].GetComponent<Character>().isAsleep || activeParty.activeParty[1].GetComponent<Character>().isConfused)
            {
                if (!activeParty.activeParty[1].GetComponent<Character>().inflicted)
                {
                    activeParty.activeParty[1].GetComponent<Character>().inflicted = true;
                }

                if (activeParty.activeParty[1].GetComponent<SpriteRenderer>().color == Color.white && !animExists)
                {
                    activeParty.activeParty[1].GetComponent<SpriteRenderer>().color = Color.grey;
                }
            }

            if (activeParty.activeParty[1].GetComponent<Character>().isPoisoned)
            {
                if (!activeParty.activeParty[1].GetComponent<Character>().inflicted)
                {
                    activeParty.activeParty[1].GetComponent<Character>().inflicted = true;
                }

                if (activeParty.activeParty[1].GetComponent<SpriteRenderer>().color == Color.white && !animExists)
                {
                    activeParty.activeParty[1].GetComponent<SpriteRenderer>().color = Color.green;
                }
            }

            if (!activeParty.activeParty[1].GetComponent<Character>().inflicted)
            {
                if (activeParty.activeParty[1].GetComponent<SpriteRenderer>().color != Color.white && !animExists && currentAnimation[0].GetComponent<Animator>() != Engine.e.gameInventory[3].GetComponent<Animator>())
                {
                    activeParty.activeParty[1].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }


        if (activeParty.activeParty[2] != null)
        {

            if (activeParty.activeParty[2].GetComponent<Character>().isAsleep || activeParty.activeParty[2].GetComponent<Character>().isConfused)
            {
                if (!activeParty.activeParty[2].GetComponent<Character>().inflicted)
                {
                    activeParty.activeParty[2].GetComponent<Character>().inflicted = true;
                }

                if (activeParty.activeParty[2].GetComponent<SpriteRenderer>().color == Color.white && !animExists)
                {
                    activeParty.activeParty[2].GetComponent<SpriteRenderer>().color = Color.grey;
                }
            }

            if (activeParty.activeParty[2].GetComponent<Character>().isPoisoned)
            {
                if (!activeParty.activeParty[2].GetComponent<Character>().inflicted)
                {
                    activeParty.activeParty[2].GetComponent<Character>().inflicted = true;
                }

                if (activeParty.activeParty[2].GetComponent<SpriteRenderer>().color == Color.white && !animExists)
                {
                    activeParty.activeParty[2].GetComponent<SpriteRenderer>().color = Color.green;
                }
            }

            if (!activeParty.activeParty[2].GetComponent<Character>().inflicted)
            {
                if (activeParty.activeParty[2].GetComponent<SpriteRenderer>().color != Color.white && !animExists && currentAnimation[0].GetComponent<Animator>() != Engine.e.gameInventory[3].GetComponent<Animator>())
                {
                    activeParty.activeParty[2].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }


        Engine.e.activeParty.GetComponent<SpriteRenderer>().color = activeParty.activeParty[0].GetComponent<SpriteRenderer>().color;

        if (activeParty.activeParty[1] != null)
        {
            Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().color = activeParty.activeParty[1].GetComponent<SpriteRenderer>().color;
        }
        if (activeParty.activeParty[2] != null)
        {
            Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().color = activeParty.activeParty[2].GetComponent<SpriteRenderer>().color;
        }

        if (enemies[0].GetComponent<Enemy>().isAsleep || enemies[0].GetComponent<Enemy>().isConfused)
        {
            if (!enemies[0].GetComponent<Enemy>().inflicted)
            {
                enemies[0].GetComponent<Enemy>().inflicted = true;
            }

            if (enemies[0].GetComponentInChildren<SpriteRenderer>().color == Color.white && !animExists)
            {
                enemies[0].GetComponentInChildren<SpriteRenderer>().color = Color.grey;
            }
        }

        if (enemies[0].GetComponent<Enemy>().isPoisoned)
        {
            if (!enemies[0].GetComponent<Enemy>().inflicted)
            {
                enemies[0].GetComponent<Enemy>().inflicted = true;
            }

            if (enemies[0].GetComponentInChildren<SpriteRenderer>().color == Color.white && !animExists)
            {
                enemies[0].GetComponentInChildren<SpriteRenderer>().color = Color.green;
            }
        }

        if (!enemies[0].GetComponent<Enemy>().inflicted)
        {
            if (enemies[0].GetComponentInChildren<SpriteRenderer>().color != Color.white && !animExists)
            {
                enemies[0].GetComponentInChildren<SpriteRenderer>().color = Color.white;
            }
        }

        if (enemies[1] != null)
        {

            if (enemies[1].GetComponent<Enemy>().isAsleep || enemies[1].GetComponent<Enemy>().isConfused)
            {
                if (!enemies[1].GetComponent<Enemy>().inflicted)
                {
                    enemies[1].GetComponent<Enemy>().inflicted = true;
                }

                if (enemies[1].GetComponentInChildren<SpriteRenderer>().color == Color.white && !animExists)
                {
                    enemies[1].GetComponentInChildren<SpriteRenderer>().color = Color.grey;
                }
            }

            if (enemies[1].GetComponent<Enemy>().isPoisoned)
            {
                if (!enemies[1].GetComponent<Enemy>().inflicted)
                {
                    enemies[1].GetComponent<Enemy>().inflicted = true;
                }

                if (enemies[1].GetComponentInChildren<SpriteRenderer>().color == Color.white && !animExists)
                {
                    enemies[1].GetComponentInChildren<SpriteRenderer>().color = Color.green;
                }
            }

            if (!enemies[1].GetComponent<Enemy>().inflicted)
            {
                if (enemies[1].GetComponentInChildren<SpriteRenderer>().color != Color.white && !animExists)
                {
                    enemies[1].GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
            }
        }

        if (enemies[2] != null)
        {

            if (enemies[2].GetComponent<Enemy>().isAsleep || enemies[2].GetComponent<Enemy>().isConfused)
            {
                if (!enemies[2].GetComponent<Enemy>().inflicted)
                {
                    enemies[2].GetComponent<Enemy>().inflicted = true;
                }

                if (enemies[2].GetComponentInChildren<SpriteRenderer>().color == Color.white && !animExists)
                {
                    enemies[2].GetComponentInChildren<SpriteRenderer>().color = Color.grey;
                }
            }

            if (enemies[2].GetComponent<Enemy>().isPoisoned)
            {
                if (!enemies[2].GetComponent<Enemy>().inflicted)
                {
                    enemies[2].GetComponent<Enemy>().inflicted = true;
                }

                if (enemies[2].GetComponentInChildren<SpriteRenderer>().color == Color.white && !animExists)
                {
                    enemies[2].GetComponentInChildren<SpriteRenderer>().color = Color.green;
                }
            }

            if (!enemies[2].GetComponent<Enemy>().inflicted)
            {
                if (enemies[2].GetComponentInChildren<SpriteRenderer>().color != Color.white && !animExists)
                {
                    enemies[2].GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
            }
        }

        if (enemies[3] != null)
        {


            if (enemies[3].GetComponent<Enemy>().isAsleep || enemies[3].GetComponent<Enemy>().isConfused)
            {
                if (!enemies[3].GetComponent<Enemy>().inflicted)
                {
                    enemies[3].GetComponent<Enemy>().inflicted = true;
                }

                if (enemies[3].GetComponentInChildren<SpriteRenderer>().color == Color.white && !animExists)
                {
                    enemies[3].GetComponentInChildren<SpriteRenderer>().color = Color.grey;
                }
            }

            if (enemies[3].GetComponent<Enemy>().isPoisoned)
            {
                if (!enemies[3].GetComponent<Enemy>().inflicted)
                {
                    enemies[3].GetComponent<Enemy>().inflicted = true;
                }

                if (enemies[3].GetComponentInChildren<SpriteRenderer>().color == Color.white && !animExists)
                {
                    enemies[3].GetComponentInChildren<SpriteRenderer>().color = Color.green;
                }
            }

            if (!enemies[3].GetComponent<Enemy>().inflicted)
            {
                if (enemies[3].GetComponentInChildren<SpriteRenderer>().color != Color.white && !animExists)
                {
                    enemies[3].GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
            }
        }

        if (!isDead)
        {

            HandleQueue();
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

    public void HandleMeleeAttackAnim(GameObject _spawnLoc, GameObject _targetLoc, Weapon weapon)
    {
        battleAnimationsReference.StartMeleeAttackAnimation(_spawnLoc, _targetLoc, weapon);
    }

    public void HandleItemAnim(GameObject _spawnLoc, GameObject _targetLoc, Item _item)
    {
        if (_item.targetAll)
        {
            if (targetingTeam)
            {
                battleAnimationsReference.StartItemAnimationAllTeam(_spawnLoc, _item);
            }
            if (targetingEnemy)
            {
                battleAnimationsReference.StartItemAnimationAllEnemies(_spawnLoc, _item);
            }
        }
        else
        {
            battleAnimationsReference.StartItemAnimation(_spawnLoc, _targetLoc, _item);
        }
    }

    public void HandleDropAnim(GameObject _spawnLoc, GameObject _targetLoc, Drops _drop)
    {
        if (_drop.targetAll)
        {
            if (targetingTeam)
            {
                battleAnimationsReference.StartDropAnimationAllTeam(_spawnLoc, _drop);
            }
            if (targetingEnemy)
            {
                battleAnimationsReference.StartDropAnimationAllEnemies(_spawnLoc, _drop);
            }
        }
        else
        {
            battleAnimationsReference.StartDropAnimation(_spawnLoc, _targetLoc, _drop);
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

    public IEnumerator DisplayDamageText()
    {
        displayDamageText = false;

        if (dmgText1Active == true)
        {
            dmgText1Active = false;
            damagePopup[0].SetActive(true);
        }
        if (dmgText2Active == true)
        {
            dmgText2Active = false;
            damagePopup[1].SetActive(true);
        }
        if (dmgText3Active == true)
        {
            dmgText3Active = false;
            damagePopup[2].SetActive(true);
        }
        if (dmgText4Active == true)
        {
            dmgText4Active = false;
            damagePopup[3].SetActive(true);
        }
        if (dmgText5Active == true)
        {
            dmgText5Active = false;
            damagePopup[4].SetActive(true);
        }
        if (dmgText6Active == true)
        {
            dmgText6Active = false;
            damagePopup[5].SetActive(true);
        }
        if (dmgText7Active == true)
        {
            dmgText7Active = false;
            damagePopup[6].SetActive(true);
        }

        if (Engine.e.activeParty.gameObject.GetComponent<SpriteRenderer>().material != originalMaterial)
        {
            Engine.e.activeParty.gameObject.GetComponent<SpriteRenderer>().material = originalMaterial;
        }

        if (Engine.e.battleSystem.hud.displayHealth[0].text != Engine.e.activeParty.activeParty[0].GetComponent<Character>().currentHealth.ToString())
        {
            Engine.e.battleSystem.hud.displayHealth[0].text = Engine.e.activeParty.activeParty[0].GetComponent<Character>().currentHealth.ToString();
        }

        if (Engine.e.battleSystem.hud.displayMaxHealth[0].text != Engine.e.activeParty.activeParty[0].GetComponent<Character>().maxHealth.ToString())
        {
            Engine.e.battleSystem.hud.displayMaxHealth[0].text = Engine.e.activeParty.activeParty[0].GetComponent<Character>().maxHealth.ToString();
        }

        if (Engine.e.battleSystem.hud.displayMana[0].text != Engine.e.activeParty.activeParty[0].GetComponent<Character>().currentMana.ToString())
        {
            Engine.e.battleSystem.hud.displayMana[0].text = Engine.e.activeParty.activeParty[0].GetComponent<Character>().currentMana.ToString();
        }
        if (Engine.e.battleSystem.hud.displayMaxMana[0].text != Engine.e.activeParty.activeParty[0].GetComponent<Character>().maxMana.ToString())
        {
            Engine.e.battleSystem.hud.displayMaxMana[0].text = Engine.e.activeParty.activeParty[0].GetComponent<Character>().maxMana.ToString();
        }

        if (Engine.e.battleSystem.hud.displayEnergy[0].text != Engine.e.activeParty.activeParty[0].GetComponent<Character>().currentEnergy.ToString())
        {
            Engine.e.battleSystem.hud.displayEnergy[0].text = Engine.e.activeParty.activeParty[0].GetComponent<Character>().currentEnergy.ToString();
        }
        if (Engine.e.battleSystem.hud.displayMaxEnergy[0].text != Engine.e.activeParty.activeParty[0].GetComponent<Character>().maxEnergy.ToString())
        {
            Engine.e.battleSystem.hud.displayMaxEnergy[0].text = Engine.e.activeParty.activeParty[0].GetComponent<Character>().maxEnergy.ToString();
        }

        if (Engine.e.party[1] != null)
        {
            if (Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().material != originalMaterial)
            {
                Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().material = originalMaterial;
            }

            if (Engine.e.battleSystem.hud.displayHealth[1].text != Engine.e.activeParty.activeParty[1].GetComponent<Character>().currentHealth.ToString())
            {
                Engine.e.battleSystem.hud.displayHealth[1].text = Engine.e.activeParty.activeParty[1].GetComponent<Character>().currentHealth.ToString();
            }
            if (Engine.e.battleSystem.hud.displayMaxHealth[1].text != Engine.e.activeParty.activeParty[1].GetComponent<Character>().maxHealth.ToString())
            {
                Engine.e.battleSystem.hud.displayMaxHealth[1].text = Engine.e.activeParty.activeParty[1].GetComponent<Character>().maxHealth.ToString();
            }


            if (Engine.e.battleSystem.hud.displayMana[1].text != Engine.e.activeParty.activeParty[1].GetComponent<Character>().currentMana.ToString())
            {
                Engine.e.battleSystem.hud.displayMana[1].text = Engine.e.activeParty.activeParty[1].GetComponent<Character>().currentMana.ToString();
            }
            if (Engine.e.battleSystem.hud.displayMaxMana[1].text != Engine.e.activeParty.activeParty[1].GetComponent<Character>().maxMana.ToString())
            {
                Engine.e.battleSystem.hud.displayMaxMana[1].text = Engine.e.activeParty.activeParty[1].GetComponent<Character>().maxMana.ToString();
            }

            if (Engine.e.battleSystem.hud.displayEnergy[1].text != Engine.e.activeParty.activeParty[1].GetComponent<Character>().currentEnergy.ToString())
            {
                Engine.e.battleSystem.hud.displayEnergy[1].text = Engine.e.activeParty.activeParty[1].GetComponent<Character>().currentEnergy.ToString();
            }
            if (Engine.e.battleSystem.hud.displayMaxEnergy[1].text != Engine.e.activeParty.activeParty[1].GetComponent<Character>().maxEnergy.ToString())
            {
                Engine.e.battleSystem.hud.displayMaxEnergy[1].text = Engine.e.activeParty.activeParty[1].GetComponent<Character>().maxEnergy.ToString();
            }
        }

        if (Engine.e.party[2] != null)
        {
            if (Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().material != originalMaterial)
            {
                Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().material = originalMaterial;
            }

            if (Engine.e.battleSystem.hud.displayHealth[2].text != Engine.e.activeParty.activeParty[2].GetComponent<Character>().currentHealth.ToString())
            {
                Engine.e.battleSystem.hud.displayHealth[2].text = Engine.e.activeParty.activeParty[2].GetComponent<Character>().currentHealth.ToString();
                Engine.e.activeParty.gameObject.GetComponent<SpriteRenderer>().material = originalMaterial;
            }
            if (Engine.e.battleSystem.hud.displayMaxHealth[2].text != Engine.e.activeParty.activeParty[2].GetComponent<Character>().maxHealth.ToString())
            {
                Engine.e.battleSystem.hud.displayMaxHealth[2].text = Engine.e.activeParty.activeParty[2].GetComponent<Character>().maxHealth.ToString();
            }

            if (Engine.e.battleSystem.hud.displayMana[2].text != Engine.e.activeParty.activeParty[2].GetComponent<Character>().currentHealth.ToString())
            {
                Engine.e.battleSystem.hud.displayMana[2].text = Engine.e.activeParty.activeParty[2].GetComponent<Character>().currentMana.ToString();
            }
            if (Engine.e.battleSystem.hud.displayMaxMana[2].text != Engine.e.activeParty.activeParty[2].GetComponent<Character>().maxMana.ToString())
            {
                Engine.e.battleSystem.hud.displayMaxMana[2].text = Engine.e.activeParty.activeParty[2].GetComponent<Character>().maxMana.ToString();
            }

            if (Engine.e.battleSystem.hud.displayEnergy[2].text != Engine.e.activeParty.activeParty[2].GetComponent<Character>().currentHealth.ToString())
            {
                Engine.e.battleSystem.hud.displayEnergy[2].text = Engine.e.activeParty.activeParty[2].GetComponent<Character>().currentEnergy.ToString();
            }
            if (Engine.e.battleSystem.hud.displayMaxEnergy[2].text != Engine.e.activeParty.activeParty[2].GetComponent<Character>().maxEnergy.ToString())
            {
                Engine.e.battleSystem.hud.displayMaxEnergy[2].text = Engine.e.activeParty.activeParty[2].GetComponent<Character>().maxEnergy.ToString();
            }
        }

        if (enemies[0].GetComponentInChildren<SpriteRenderer>().material != originalMaterial)
        {
            enemies[0].GetComponentInChildren<SpriteRenderer>().material = originalMaterial;
        }


        if (Engine.e.battleSystem.hud.displayEnemyHealth[0].text != enemies[0].GetComponent<Enemy>().currentHealth.ToString())
        {
            Engine.e.battleSystem.hud.displayEnemyHealth[0].text = enemies[0].GetComponent<Enemy>().currentHealth.ToString();
        }

        if (enemies[1] != null)
        {
            if (Engine.e.battleSystem.hud.displayEnemyHealth[1].text != enemies[1].GetComponent<Enemy>().currentHealth.ToString())
            {
                Engine.e.battleSystem.hud.displayEnemyHealth[1].text = enemies[1].GetComponent<Enemy>().currentHealth.ToString();
            }
            if (enemies[1].GetComponentInChildren<SpriteRenderer>().material != originalMaterial)
            {
                enemies[1].GetComponentInChildren<SpriteRenderer>().material = originalMaterial;
            }
        }

        if (enemies[2] != null)
        {
            if (Engine.e.battleSystem.hud.displayEnemyHealth[2].text != enemies[2].GetComponent<Enemy>().currentHealth.ToString())
            {
                Engine.e.battleSystem.hud.displayEnemyHealth[2].text = enemies[2].GetComponent<Enemy>().currentHealth.ToString();
            }
            if (enemies[2].GetComponentInChildren<SpriteRenderer>().material != originalMaterial)
            {
                enemies[2].GetComponentInChildren<SpriteRenderer>().material = originalMaterial;
            }
        }

        if (enemies[3] != null)
        {
            if (Engine.e.battleSystem.hud.displayEnemyHealth[3].text != enemies[3].GetComponent<Enemy>().currentHealth.ToString())
            {
                Engine.e.battleSystem.hud.displayEnemyHealth[3].text = enemies[3].GetComponent<Enemy>().currentHealth.ToString();
            }
            if (enemies[3].GetComponentInChildren<SpriteRenderer>().material != originalMaterial)
            {
                enemies[3].GetComponentInChildren<SpriteRenderer>().material = originalMaterial;
            }
        }

        yield return new WaitForSeconds(0.75f);

        for (int i = 0; i < damagePopup.Length; i++)
        {
            damagePopup[i].SetActive(false);
        }

        EndTurn();
    }

    public void SetDamagePopupTextOne(Vector3 _pos, string _textDisplayed, Color _color)
    {


        damagePopup[0].transform.position = _pos;
        damagePopup[0].transform.GetChild(0).GetComponent<TextMeshPro>().color = _color;
        damagePopup[0].transform.GetChild(0).GetComponent<TextMeshPro>().text = _textDisplayed;



        dmgText1Active = true;
    }

    public void SetDamagePopupTextAllTeam(string _textDisplayed, Color _color)
    {
        damagePopup[0].transform.position = Engine.e.activeParty.gameObject.transform.position;
        damagePopup[1].transform.position = Engine.e.activePartyMember2.transform.position;
        damagePopup[2].transform.position = Engine.e.activePartyMember3.transform.position;

        damagePopup[0].transform.GetChild(0).GetComponent<TextMeshPro>().color = _color;
        damagePopup[1].transform.GetChild(0).GetComponent<TextMeshPro>().color = _color;
        damagePopup[2].transform.GetChild(0).GetComponent<TextMeshPro>().color = _color;

        damagePopup[0].transform.GetChild(0).GetComponent<TextMeshPro>().text = _textDisplayed;
        damagePopup[1].transform.GetChild(0).GetComponent<TextMeshPro>().text = _textDisplayed;
        damagePopup[2].transform.GetChild(0).GetComponent<TextMeshPro>().text = _textDisplayed;

        if (activeParty.activeParty[0].GetComponent<Character>().currentHealth > 0)
        {
            dmgText1Active = true;
        }

        if (activeParty.activeParty[1] != null && activeParty.activeParty[1].GetComponent<Character>().currentHealth > 0)
        {
            dmgText2Active = true;
        }
        if (activeParty.activeParty[2] != null && activeParty.activeParty[2].GetComponent<Character>().currentHealth > 0)
        {
            dmgText3Active = true;
        }
    }

    private void FixedUpdate()
    {
        if (animExists)
        {
            animationTimer -= Time.deltaTime;
        }

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

        if (displayDamageText)
        {
            StartCoroutine(DisplayDamageText());
        }
    }
}