using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
public class Engine : MonoBehaviour
{

    // General Info
    public GameObject[] party;
    public ActiveParty activeParty;
    public Character[] playableCharacters;
    public Item[] charEquippedWeaponRight, charEquippedWeaponLeft, charEquippedChestArmor, charEquippedLegArmor, charEquippedAccessory1, charEquippedAccessory2;
    public int partyMoney;
    public float timeOfDay;
    public string currentScene;
    public bool inBattle = false;
    public bool indoors = false;
    public bool inTown = false;
    public bool inWorldMap = false;
    public bool inRange = false;
    public bool battleModeActive = true;
    public bool aboveLayer = false;
    public bool autoSaveReady, recentAutoSave = false;
    // Stat Curves
    [SerializeField]
    AnimationCurve healthCurve, manaCurve, energyCurve, strengthCurve, intelligenceCurve;

    // Drops
    public Drops[] gameDrops;

    // Skills    
    public Skills[] gameSkills;


    // Inventory / Weapons / Armor / Drops
    // "Game Inventory" is the most important. The other lists are mainly for visual reference.
    public List<Item> gameInventory;
    [SerializeField]
    List<Item> gameWeapons, gameChestArmor, gameFireDrops, gameIceDrops,
    gameLightningDrops, gameWaterDrops, gameShadowDrops, gameHolyDrops;

    // Quests
    public Quest[] gameQuests;

    // Misc References
    public int characterBeingTargeted, charBeingTargeted;
    public Item itemToBeUsed;
    public List<int> remainingPartyMembers;
    public static Engine e;
    public bool gameStart, loading, onRamp, ableToSave, arrangePartyButtonActive, char1LevelUp, char2LevelUp, char3LevelUp;
    public BattleSystem battleSystem;
    int nextRemainingCharacterIndex, randomPartyMemberIndex;
    public CinemachineVirtualCamera mainVirtualCamera;
    public Camera mainCamera;
    public GameObject activateArrangePartyButton,
    activePartyMember2, activePartyMember3, itemMenuCharFirst;
    public TextMeshProUGUI[] char1LevelUpPanelReference, char2LevelUpPanelReference, char3LevelUpPanelReference;
    public TextMeshProUGUI enemyLootReferenceG, enemyLootReferenceExp, battleHelp;
    Vector3 startingPos;
    public System.Random randomIndex;
    public CharacterClass charClassReference;
    public string[] charClasses;

    // Shopping
    public bool selling = false;
    public TextMeshProUGUI storeDialogueReference, helpText;
    public GameObject[] shopSellButtons;
    public Merchant currentMerchant;
    public TavernBarkeeperDialogue currentTavern;
    public GameObject[] itemConfirmUseButtons, itemDropConfirmUseButtons;
    public GameObject interactionPopup;
    public bool loadTimer = false;
    public GameObject weatherRain;

    // Menu References
    public PartyInventory partyInventoryReference;
    public PauseMenu pauseMenuReference;
    public AdventureLog adventureLogReference;
    public EquipDisplay equipMenuReference;
    public FileMenu fileMenuReference;
    public Status statusMenuReference;
    public BattleMenu battleMenu;
    public Grid gridReference;
    public GameObject mainMenu, battleSystemMenu;
    public GameObject inventoryMenuReference;
    public GameObject[] pauseMenuCharacterPanels, itemMenuPanels;
    public GameObject canvasReference;
    public GameObject[] charAbilityButtons, charSkillTierButtons;
    public bool saveExists;

    [SerializeField]
    TextMeshProUGUI[] inventoryMenuPartyNameStatsReference, inventoryMenuPartyHPStatsReference, inventoryMenuPartyMPStatsReference, inventoryMenuPartyENRStatsReference;
    [SerializeField]
    GameObject miniMap;
    float timer = 60;
    //public float rainTimer = 0f, rainChance, rainOff;
    //public bool startTimer = true, stopTimer = false, weatherRainOn = false;
    // Music
    //public AudioSource battleMusic;

    void Awake()
    {
        gameStart = false;
        e = this;
        SaveSystem.CheckFilesForDisplay();
    }

    // Establishes a New Game. Clears multiple variables to their default states for a fresh start.
    public void NewGame()
    {
        ClearGameInventoryHeld();
        ClearGameQuests();
        SetDropIndexes();
        SetSkillIndexes();
        gridReference.SetupGrid();

        SetClasses();
        SetParty();
        //abilityScreenReference.ClearNodeUnlocked();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        activeParty.SetActiveParty();
        //SceneManager.LoadSceneAsync("OpeningScene", LoadSceneMode.Additive);
        //SceneManager.LoadSceneAsync("MariaWest", LoadSceneMode.Additive);
        loadTimer = true;
        SceneManager.LoadSceneAsync("GrieveNameInput", LoadSceneMode.Additive);

        activeParty.SetLeaderSprite();
        timeOfDay = 750f;
        recentAutoSave = true;
        arrangePartyButtonActive = false;
        mainVirtualCamera.GetComponent<CinemachineVirtualCamera>().Priority = 10;
        partyMoney = 100;
        aboveLayer = false;
        inWorldMap = false;
        battleModeActive = true;
        battleSystem.enemyGroup = null;
        onRamp = false;
        activeParty.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Layer1";
        activePartyMember2.GetComponent<SpriteRenderer>().sortingLayerName = "Layer1";
        activePartyMember3.GetComponent<SpriteRenderer>().sortingLayerName = "Layer1";

    }

    // Sets default values for various game components relating to the playable characters.       
    void SetParty()
    {
        party = new GameObject[playableCharacters.Length];

        adventureLogReference.questLog = new Quest[adventureLogReference.questSlots.Length];
        adventureLogReference.completedQuestLog = new Quest[adventureLogReference.completedQuestSlots.Length];

        partyInventoryReference.partyInventory = new Item[partyInventoryReference.itemInventorySlots.Length];

        partyInventoryReference.weapons = new Weapon[partyInventoryReference.weaponInventorySlots.Length];
        partyInventoryReference.chestArmor = new ChestArmor[partyInventoryReference.chestArmorInventorySlots.Length];
        //partyInventoryReference.chestArmor = new LegArmor[partyInventoryReference.legArmorInventorySlots.Length];
        partyInventoryReference.accessories = new Accessory[partyInventoryReference.accessoryInventorySlots.Length];

        SetInventoryArrayPositions();
        ResetCharClasses();

        charEquippedWeaponRight = new Item[playableCharacters.Length];
        charEquippedWeaponLeft = new Item[playableCharacters.Length];
        charEquippedChestArmor = new Item[playableCharacters.Length];
        charEquippedLegArmor = new Item[playableCharacters.Length];
        charEquippedAccessory1 = new Item[playableCharacters.Length];
        charEquippedAccessory2 = new Item[playableCharacters.Length];

        partyMoney = 0;

        // Grieve
        playableCharacters[0].characterName = "Grieve";
        playableCharacters[0].characterClass[0] = true;
        //playableCharacters[0].currentClass = "Soldier";
        //playableCharacters[0].classEXP[0] = 1f;
        //playableCharacters[0].classUnlocked[0] = true;

        playableCharacters[0].lvl = 1;
        playableCharacters[0].healthOffset = 0f;
        playableCharacters[0].manaOffset = -30f;
        playableCharacters[0].energyOffset = 0f;
        playableCharacters[0].strengthOffset = 0f;
        playableCharacters[0].intelligenceOffset = -10f;

        playableCharacters[0].maxHealth = Mathf.Round(healthCurve.Evaluate(playableCharacters[0].lvl) + (healthCurve.Evaluate(playableCharacters[0].lvl) * (playableCharacters[0].healthOffset / 100)));
        playableCharacters[0].currentHealth = playableCharacters[0].maxHealth;
        playableCharacters[0].maxMana = Mathf.Round(manaCurve.Evaluate(playableCharacters[0].lvl) + (manaCurve.Evaluate(playableCharacters[0].lvl) * (playableCharacters[0].manaOffset / 100)));
        playableCharacters[0].currentMana = playableCharacters[0].maxMana;
        playableCharacters[0].maxEnergy = Mathf.Round(energyCurve.Evaluate(playableCharacters[0].lvl) + (energyCurve.Evaluate(playableCharacters[0].lvl) * (playableCharacters[0].energyOffset / 100)));
        playableCharacters[0].currentEnergy = playableCharacters[0].maxEnergy;
        playableCharacters[0].haste = 56;
        playableCharacters[0].critChance = 15;
        playableCharacters[0].strength = Mathf.Round(strengthCurve.Evaluate(playableCharacters[0].lvl) + +(strengthCurve.Evaluate(playableCharacters[0].lvl) * (playableCharacters[0].strengthOffset / 100)));
        playableCharacters[0].intelligence = Mathf.Round(intelligenceCurve.Evaluate(playableCharacters[0].lvl) + +(intelligenceCurve.Evaluate(playableCharacters[0].lvl) * (playableCharacters[0].intelligenceOffset / 100)));
        playableCharacters[0].experiencePoints = 0;
        playableCharacters[0].levelUpReq = 100;
        playableCharacters[0].isInParty = true;
        playableCharacters[0].isInActiveParty = true;
        playableCharacters[0].isLeader = true;
        playableCharacters[0].dodgeChance = 10f;
        playableCharacters[0].dropCostReduction = 0f;
        playableCharacters[0].skillCostReduction = 0f;
        playableCharacters[0].healthCapped = true;
        playableCharacters[0].manaCapped = true;
        playableCharacters[0].energyCapped = true;

        playableCharacters[0].activePartyIndex = 0;
        playableCharacters[0].partyIndex = 0;

        playableCharacters[0].physicalDefense = 2;
        playableCharacters[0].physicalDefense += gameChestArmor[0].GetComponent<ChestArmor>().physicalArmor;
        playableCharacters[0].fireDefense = 1f;
        playableCharacters[0].waterDefense = 1f;
        playableCharacters[0].lightningDefense = 3f;
        playableCharacters[0].shadowDefense = 3f;
        playableCharacters[0].holyDefense = 1f;
        playableCharacters[0].iceDefense = 1f;
        playableCharacters[0].poisonDefense = 1f;
        playableCharacters[0].sleepDefense = 1f;
        playableCharacters[0].confuseDefense = 1f;

        playableCharacters[0].canDualWield = false;
        playableCharacters[0].canUse2HWeapon = false;

        playableCharacters[0].canUseFireDrops = true;
        playableCharacters[0].canUseHolyDrops = true;
        playableCharacters[0].canUseWaterDrops = true;
        playableCharacters[0].canUseLightningDrops = true;
        playableCharacters[0].canUseShadowDrops = true;
        playableCharacters[0].canUseIceDrops = true;


        //playableCharacters[0].activePartyGO = activeParty.gameObject;

        /* playableCharacters[0].fireDrops = new Drops[10];
         playableCharacters[0].holyDrops = new Drops[10];
         playableCharacters[0].waterDrops = new Drops[10];
         playableCharacters[0].lightningDrops = new Drops[10];
         playableCharacters[0].shadowDrops = new Drops[10];
         playableCharacters[0].iceDrops = new Drops[10];




         playableCharacters[0].fireDrops[0] = fireDrops[0];
         playableCharacters[0].fireDrops[1] = fireDrops[1];
         playableCharacters[0].holyDrops[0] = holyDrops[0];
         playableCharacters[0].holyDrops[1] = holyDrops[1];
         playableCharacters[0].holyDrops[2] = holyDrops[2];
         playableCharacters[0].holyDrops[3] = holyDrops[3];
         playableCharacters[0].waterDrops[0] = waterDrops[0];
         playableCharacters[0].lightningDrops[0] = lightningDrops[0];
         playableCharacters[0].shadowDrops[0] = shadowDrops[0];
         playableCharacters[0].shadowDrops[1] = shadowDrops[1];
         playableCharacters[0].shadowDrops[2] = shadowDrops[2];
         playableCharacters[0].shadowDrops[3] = shadowDrops[3];
         playableCharacters[0].shadowDrops[4] = shadowDrops[4];
         playableCharacters[0].iceDrops[0] = iceDrops[0];
 */
        // fireDrops[0].isKnown = true;


        playableCharacters[0].drops = new Drops[gameDrops.Length];

        playableCharacters[0].drops[0] = gameDrops[0];
        playableCharacters[0].drops[1] = gameDrops[1];
        playableCharacters[0].drops[10] = gameDrops[10];
        gameDrops[10].isKnown = true;
        //gameDrops[1].isKnown = true;
        //playableCharacters[0].drops[4] = gameDrops[4];
        /*playableCharacters[0].drops[5] = gameDrops[5];
        playableCharacters[0].drops[10] = gameDrops[10];
        playableCharacters[0].drops[15] = gameDrops[15];
        playableCharacters[0].drops[20] = gameDrops[20];
        playableCharacters[0].drops[21] = gameDrops[21];
        playableCharacters[0].drops[22] = gameDrops[22];
        playableCharacters[0].drops[23] = gameDrops[23];
        playableCharacters[0].drops[24] = gameDrops[24];
        playableCharacters[0].drops[25] = gameDrops[25];
        playableCharacters[0].drops[26] = gameDrops[26];
        playableCharacters[0].drops[27] = gameDrops[27];
        playableCharacters[0].drops[28] = gameDrops[28];
        playableCharacters[0].drops[29] = gameDrops[29];
*/

        playableCharacters[0].fireDropsLevel = 1f;
        playableCharacters[0].holyDropsLevel = 2f;
        playableCharacters[0].waterDropsLevel = 1f;
        playableCharacters[0].lightningDropsLevel = 5f;
        playableCharacters[0].shadowDropsLevel = 1f;
        playableCharacters[0].iceDropsLevel = 1f;


        playableCharacters[0].fireDropsExperience = 0f;
        playableCharacters[0].waterDropsExperience = 0f;
        playableCharacters[0].lightningDropsExperience = 0f;
        playableCharacters[0].shadowDropsExperience = 0f;
        playableCharacters[0].iceDropsExperience = 0f;
        playableCharacters[0].holyDropsExperience = 0f;


        playableCharacters[0].fireDropsLvlReq = 50f;
        playableCharacters[0].waterDropsLvlReq = 50f;
        playableCharacters[0].lightningDropsLvlReq = 50f;
        playableCharacters[0].shadowDropsLvlReq = 50f;
        playableCharacters[0].iceDropsLvlReq = 50f;
        playableCharacters[0].holyDropsLvlReq = 50f;


        playableCharacters[0].fireDropAttackBonus = 0f;
        playableCharacters[0].waterDropAttackBonus = 0f;
        playableCharacters[0].lightningDropAttackBonus = 0f;
        playableCharacters[0].shadowDropAttackBonus = 0f;
        playableCharacters[0].iceDropAttackBonus = 0f;

        playableCharacters[0].firePhysicalAttackBonus = 0f;
        playableCharacters[0].waterPhysicalAttackBonus = 0f;
        playableCharacters[0].lightningPhysicalAttackBonus = 0f;
        playableCharacters[0].shadowPhysicalAttackBonus = 0f;
        playableCharacters[0].icePhysicalAttackBonus = 0f;



        playableCharacters[0].skills = new Skills[30];
        playableCharacters[0].skillIndex = 0;
        playableCharacters[0].skillTotal = 1;
        playableCharacters[0].skillScale = 1f;
        playableCharacters[0].availableSkillPoints = 0;
        playableCharacters[0].skills[0] = gameSkills[0];
        gameSkills[0].isKnown = true;

        playableCharacters[0].stealChance = 60f;


        // Negative Status Effects
        playableCharacters[0].isPoisoned = false;
        playableCharacters[0].isAsleep = false;
        playableCharacters[0].isConfused = false;
        playableCharacters[0].deathInflicted = false;
        playableCharacters[0].inflicted = false;
        playableCharacters[0].miterInflicted = false;
        playableCharacters[0].haltInflicted = false;

        // Beneficial Status Effects
        playableCharacters[0].isProtected = false;
        playableCharacters[0].isEncompassed = false;
        playableCharacters[0].isHastened = false;


        playableCharacters[0].poisonDmg = 0f;
        playableCharacters[0].sleepTimer = 0;
        playableCharacters[0].confuseTimer = 0;
        playableCharacters[0].deathTimer = 3;


        playableCharacters[0].GetComponent<SpriteRenderer>().color = Color.white;



        // Mac
        playableCharacters[1].characterName = "Mac";
        playableCharacters[1].currentClass = "Mage";
        playableCharacters[1].characterClass[3] = true;
        playableCharacters[1].classEXP[3] = 1f;
        playableCharacters[1].classUnlocked[3] = true;

        playableCharacters[1].lvl = 1;
        playableCharacters[1].healthOffset = -20f;
        playableCharacters[1].manaOffset = 20f;
        playableCharacters[1].energyOffset = 0f;
        playableCharacters[1].strengthOffset = -10f;
        playableCharacters[1].intelligenceOffset = 15f;

        playableCharacters[1].maxHealth = Mathf.Round(healthCurve.Evaluate(playableCharacters[1].lvl) + (healthCurve.Evaluate(playableCharacters[1].lvl) * (playableCharacters[1].healthOffset / 100)));
        playableCharacters[1].currentHealth = playableCharacters[1].maxHealth;
        playableCharacters[1].maxMana = Mathf.Round(manaCurve.Evaluate(playableCharacters[1].lvl) + (manaCurve.Evaluate(playableCharacters[1].lvl) * (playableCharacters[1].manaOffset / 100)));
        playableCharacters[1].currentMana = playableCharacters[1].maxMana;
        playableCharacters[1].maxEnergy = Mathf.Round(energyCurve.Evaluate(playableCharacters[1].lvl) + (energyCurve.Evaluate(playableCharacters[1].lvl) * (playableCharacters[1].energyOffset / 100)));
        playableCharacters[1].currentEnergy = playableCharacters[1].maxEnergy;
        playableCharacters[1].haste = 46;
        playableCharacters[1].critChance = 10;
        playableCharacters[1].strength = Mathf.Round(strengthCurve.Evaluate(playableCharacters[1].lvl) + +(strengthCurve.Evaluate(playableCharacters[1].lvl) * (playableCharacters[1].strengthOffset / 100)));
        playableCharacters[1].intelligence = Mathf.Round(intelligenceCurve.Evaluate(playableCharacters[1].lvl) + +(intelligenceCurve.Evaluate(playableCharacters[1].lvl) * (playableCharacters[1].intelligenceOffset / 100)));
        playableCharacters[1].experiencePoints = 0;
        playableCharacters[1].levelUpReq = 100;
        playableCharacters[1].isInParty = true;
        playableCharacters[1].isInActiveParty = true;
        playableCharacters[1].isLeader = false;
        playableCharacters[1].dropCostReduction = 0f;
        playableCharacters[1].skillCostReduction = 0f;
        playableCharacters[1].healthCapped = true;
        playableCharacters[1].manaCapped = true;
        playableCharacters[1].energyCapped = true;

        playableCharacters[1].activePartyIndex = 1;
        playableCharacters[1].partyIndex = 1;

        playableCharacters[1].dodgeChance = 10f;

        playableCharacters[1].physicalDefense = 1;
        playableCharacters[1].physicalDefense += gameChestArmor[0].GetComponent<ChestArmor>().physicalArmor;
        playableCharacters[1].fireDefense = 2f;
        playableCharacters[1].waterDefense = 2f;
        playableCharacters[1].lightningDefense = 2f;
        playableCharacters[1].shadowDefense = 1f;
        playableCharacters[1].holyDefense = 1f;
        playableCharacters[1].iceDefense = 1f;
        playableCharacters[1].poisonDefense = 1f;
        playableCharacters[1].sleepDefense = 1f;
        playableCharacters[1].confuseDefense = 1f;

        playableCharacters[1].canDualWield = false;
        playableCharacters[1].canUse2HWeapon = true;

        playableCharacters[1].canUseFireDrops = false;
        playableCharacters[1].canUseHolyDrops = false;
        playableCharacters[1].canUseWaterDrops = true;
        playableCharacters[1].canUseLightningDrops = true;
        playableCharacters[1].canUseShadowDrops = false;
        playableCharacters[1].canUseIceDrops = false;

        playableCharacters[1].drops = new Drops[gameDrops.Length];

        //playableCharacters[1].drops[10] = gameDrops[10];
        playableCharacters[1].drops[15] = gameDrops[15];
        playableCharacters[1].drops[16] = gameDrops[16];

        /* playableCharacters[1].fireDrops = new Drops[10];
         playableCharacters[1].holyDrops = new Drops[10];
         playableCharacters[1].waterDrops = new Drops[10];
         playableCharacters[1].lightningDrops = new Drops[10];
         playableCharacters[1].shadowDrops = new Drops[10];
         playableCharacters[1].iceDrops = new Drops[10];




         playableCharacters[1].waterDrops[0] = waterDrops[0];
         playableCharacters[1].lightningDrops[0] = lightningDrops[0];
 */
        playableCharacters[1].fireDropsLevel = 1f;
        playableCharacters[1].holyDropsLevel = 1f;
        playableCharacters[1].waterDropsLevel = 5f;
        playableCharacters[1].lightningDropsLevel = 2f;
        playableCharacters[1].shadowDropsLevel = 1.0f;
        playableCharacters[1].iceDropsLevel = 1.0f;


        playableCharacters[1].fireDropsExperience = 0f;
        playableCharacters[1].waterDropsExperience = 0f;
        playableCharacters[1].lightningDropsExperience = 0f;
        playableCharacters[1].shadowDropsExperience = 0f;
        playableCharacters[1].iceDropsExperience = 0f;
        playableCharacters[1].holyDropsExperience = 0f;


        playableCharacters[1].fireDropsLvlReq = 50f;
        playableCharacters[1].waterDropsLvlReq = 50f;
        playableCharacters[1].lightningDropsLvlReq = 50f;
        playableCharacters[1].shadowDropsLvlReq = 50f;
        playableCharacters[1].iceDropsLvlReq = 50f;
        playableCharacters[1].holyDropsLvlReq = 50f;


        playableCharacters[1].fireDropAttackBonus = 0f;
        playableCharacters[1].waterDropAttackBonus = 0f;
        playableCharacters[1].lightningDropAttackBonus = 0f;
        playableCharacters[1].shadowDropAttackBonus = 0f;
        playableCharacters[1].iceDropAttackBonus = 0f;

        playableCharacters[1].firePhysicalAttackBonus = 0f;
        playableCharacters[1].waterPhysicalAttackBonus = 0f;
        playableCharacters[1].lightningPhysicalAttackBonus = 0f;
        playableCharacters[1].shadowPhysicalAttackBonus = 0f;
        playableCharacters[1].icePhysicalAttackBonus = 0f;

        playableCharacters[1].skills = new Skills[30];
        playableCharacters[1].skillIndex = 5;
        playableCharacters[1].skillTotal = 1;
        playableCharacters[1].availableSkillPoints = 0;
        playableCharacters[1].skillScale = 1f;

        playableCharacters[1].skills[5] = gameSkills[5];

        playableCharacters[1].isPoisoned = false;
        playableCharacters[1].isAsleep = false;
        playableCharacters[1].isConfused = false;
        playableCharacters[1].deathInflicted = false;
        playableCharacters[1].inflicted = false;
        playableCharacters[1].miterInflicted = false;
        playableCharacters[1].haltInflicted = false;

        playableCharacters[1].isProtected = false;
        playableCharacters[1].isEncompassed = false;
        playableCharacters[1].isHastened = false;

        playableCharacters[1].stealChance = 60f;

        playableCharacters[1].poisonDmg = 0f;
        playableCharacters[1].sleepTimer = 0;
        playableCharacters[1].confuseTimer = 0;
        playableCharacters[1].deathTimer = 3;

        playableCharacters[1].GetComponent<SpriteRenderer>().color = Color.white;


        //Field
        playableCharacters[2].characterName = "Field";
        playableCharacters[2].currentClass = "Thief";
        playableCharacters[2].characterClass[2] = true;
        playableCharacters[2].classEXP[2] = 1f;
        playableCharacters[2].classUnlocked[2] = true;

        playableCharacters[2].lvl = 3;
        playableCharacters[2].healthOffset = -20f;
        playableCharacters[2].manaOffset = 0f;
        playableCharacters[2].energyOffset = 20f;
        playableCharacters[2].strengthOffset = -15f;
        playableCharacters[2].intelligenceOffset = 0f;

        playableCharacters[2].maxHealth = 80f;
        playableCharacters[2].currentHealth = playableCharacters[2].maxHealth;
        playableCharacters[2].maxMana = 20f;
        playableCharacters[2].currentMana = playableCharacters[2].maxMana;
        playableCharacters[2].maxEnergy = 50f;
        playableCharacters[2].currentEnergy = playableCharacters[2].maxEnergy;
        playableCharacters[2].haste = 60;
        playableCharacters[2].critChance = 10;
        playableCharacters[2].strength = Mathf.Round(strengthCurve.Evaluate(playableCharacters[2].lvl) + +(strengthCurve.Evaluate(playableCharacters[2].lvl) * (playableCharacters[2].strengthOffset / 100)));
        playableCharacters[2].intelligence = Mathf.Round(intelligenceCurve.Evaluate(playableCharacters[2].lvl) + +(intelligenceCurve.Evaluate(playableCharacters[2].lvl) * (playableCharacters[2].intelligenceOffset / 100)));
        playableCharacters[2].experiencePoints = 0;
        playableCharacters[2].levelUpReq = 100;
        playableCharacters[2].experiencePoints = 0;
        playableCharacters[2].levelUpReq = 100;
        playableCharacters[2].isInParty = true;
        playableCharacters[2].isInActiveParty = true;
        playableCharacters[2].isLeader = false;
        playableCharacters[2].dodgeChance = 15f;
        playableCharacters[2].dropCostReduction = 0f;
        playableCharacters[2].skillCostReduction = 0f;
        playableCharacters[2].healthCapped = true;
        playableCharacters[2].manaCapped = true;
        playableCharacters[2].energyCapped = true;

        playableCharacters[2].activePartyIndex = 2;
        playableCharacters[2].partyIndex = 2;

        playableCharacters[2].physicalDefense = 1f;
        playableCharacters[2].physicalDefense += gameChestArmor[0].GetComponent<ChestArmor>().physicalArmor;
        playableCharacters[2].fireDefense = 1f;
        playableCharacters[2].waterDefense = 1f;
        playableCharacters[2].lightningDefense = 1f;
        playableCharacters[2].shadowDefense = 3f;
        playableCharacters[2].holyDefense = 1f;
        playableCharacters[2].iceDefense = 1f;
        playableCharacters[2].poisonDefense = 2f;
        playableCharacters[2].sleepDefense = 1f;
        playableCharacters[2].confuseDefense = 1f;

        playableCharacters[2].canUse2HWeapon = false;
        playableCharacters[2].canDualWield = true;

        playableCharacters[2].canUseFireDrops = false;
        playableCharacters[2].canUseHolyDrops = false;
        playableCharacters[2].canUseWaterDrops = false;
        playableCharacters[2].canUseLightningDrops = false;
        playableCharacters[2].canUseShadowDrops = true;
        playableCharacters[2].canUseIceDrops = false;


        playableCharacters[2].drops = new Drops[gameDrops.Length];

        playableCharacters[2].drops[20] = gameDrops[20];

        /* playableCharacters[2].fireDrops = new Drops[10];
         playableCharacters[2].holyDrops = new Drops[10];
         playableCharacters[2].waterDrops = new Drops[10];
         playableCharacters[2].lightningDrops = new Drops[10];
         playableCharacters[2].shadowDrops = new Drops[10];
         playableCharacters[2].iceDrops = new Drops[10];


         playableCharacters[2].shadowDrops[0] = shadowDrops[0];
         playableCharacters[2].shadowDrops[1] = shadowDrops[1];
 */

        playableCharacters[2].fireDropsLevel = 1f;
        playableCharacters[2].holyDropsLevel = 1f;
        playableCharacters[2].waterDropsLevel = 1f;
        playableCharacters[2].lightningDropsLevel = 1f;
        playableCharacters[2].shadowDropsLevel = 2f;
        playableCharacters[2].iceDropsLevel = 1f;


        playableCharacters[2].fireDropsExperience = 0f;
        playableCharacters[2].waterDropsExperience = 0f;
        playableCharacters[2].lightningDropsExperience = 0f;
        playableCharacters[2].shadowDropsExperience = 0f;
        playableCharacters[2].iceDropsExperience = 0f;
        playableCharacters[2].holyDropsExperience = 0f;


        playableCharacters[2].fireDropsLvlReq = 50f;
        playableCharacters[2].waterDropsLvlReq = 50f;
        playableCharacters[2].lightningDropsLvlReq = 50f;
        playableCharacters[2].shadowDropsLvlReq = 50f;
        playableCharacters[2].iceDropsLvlReq = 50f;
        playableCharacters[2].holyDropsLvlReq = 50f;


        playableCharacters[2].fireDropAttackBonus = 0f;
        playableCharacters[2].waterDropAttackBonus = 0f;
        playableCharacters[2].lightningDropAttackBonus = 0f;
        playableCharacters[2].shadowDropAttackBonus = 0f;
        playableCharacters[2].iceDropAttackBonus = 0f;

        playableCharacters[2].firePhysicalAttackBonus = 0f;
        playableCharacters[2].waterPhysicalAttackBonus = 0f;
        playableCharacters[2].lightningPhysicalAttackBonus = 0f;
        playableCharacters[2].shadowPhysicalAttackBonus = 0f;
        playableCharacters[2].icePhysicalAttackBonus = 0f;

        playableCharacters[2].skills = new Skills[30];
        playableCharacters[2].skillIndex = 10;
        playableCharacters[2].skillTotal = 1;
        playableCharacters[2].skillScale = 1f;
        playableCharacters[2].availableSkillPoints = 0;
        playableCharacters[2].skills[10] = gameSkills[10];

        playableCharacters[2].isPoisoned = false;
        playableCharacters[2].isAsleep = false;
        playableCharacters[2].isConfused = false;
        playableCharacters[2].deathInflicted = false;
        playableCharacters[2].inflicted = false;
        playableCharacters[2].miterInflicted = false;
        playableCharacters[2].haltInflicted = false;

        playableCharacters[2].isProtected = false;
        playableCharacters[2].isEncompassed = false;
        playableCharacters[2].isHastened = false;

        playableCharacters[2].stealChance = 75f;

        playableCharacters[2].poisonDmg = 0f;
        playableCharacters[2].sleepTimer = 0;
        playableCharacters[2].confuseTimer = 0;
        playableCharacters[2].deathTimer = 3;

        playableCharacters[2].GetComponent<SpriteRenderer>().color = Color.white;


        //Riggs
        playableCharacters[3].characterName = "Riggs";
        playableCharacters[3].currentClass = "Rōnin";
        playableCharacters[3].characterClass[5] = true;
        playableCharacters[3].classEXP[5] = 1f;
        playableCharacters[3].classUnlocked[5] = true;


        playableCharacters[3].lvl = 5;
        playableCharacters[3].healthOffset = 30f;
        playableCharacters[3].manaOffset = 0f;
        playableCharacters[3].energyOffset = -20f;
        playableCharacters[3].strengthOffset = 20f;
        playableCharacters[3].intelligenceOffset = -15f;

        playableCharacters[3].maxHealth = 200f;
        playableCharacters[3].currentHealth = playableCharacters[3].maxHealth;
        playableCharacters[3].maxMana = 50f;
        playableCharacters[3].currentMana = playableCharacters[3].maxMana;
        playableCharacters[3].maxEnergy = 20f;
        playableCharacters[3].haste = 42;
        playableCharacters[3].critChance = 10;
        playableCharacters[3].strength = Mathf.Round(strengthCurve.Evaluate(playableCharacters[3].lvl) + +(strengthCurve.Evaluate(playableCharacters[3].lvl) * (playableCharacters[3].strengthOffset / 100)));
        playableCharacters[3].intelligence = Mathf.Round(intelligenceCurve.Evaluate(playableCharacters[3].lvl) + +(intelligenceCurve.Evaluate(playableCharacters[3].lvl) * (playableCharacters[3].intelligenceOffset / 100)));
        playableCharacters[3].experiencePoints = 0;
        playableCharacters[3].experiencePoints = 0;
        playableCharacters[3].levelUpReq = 100;
        playableCharacters[3].isInParty = false;
        playableCharacters[3].isInActiveParty = false;
        playableCharacters[3].isLeader = false;
        playableCharacters[3].dodgeChance = 12f;
        playableCharacters[3].dropCostReduction = 0f;
        playableCharacters[3].skillCostReduction = 0f;
        playableCharacters[3].healthCapped = true;
        playableCharacters[3].manaCapped = true;
        playableCharacters[3].energyCapped = true;

        playableCharacters[3].activePartyIndex = -1;
        playableCharacters[3].partyIndex = 3;

        playableCharacters[3].physicalDefense = 5;
        playableCharacters[3].physicalDefense += gameChestArmor[1].GetComponent<ChestArmor>().physicalArmor;
        playableCharacters[3].fireDefense = 2f;
        playableCharacters[3].waterDefense = 2f;
        playableCharacters[3].lightningDefense = 2f;
        playableCharacters[3].shadowDefense = 2f;
        playableCharacters[3].holyDefense = 2f;
        playableCharacters[3].iceDefense = 2f;
        playableCharacters[3].poisonDefense = 1f;
        playableCharacters[3].sleepDefense = 1f;
        playableCharacters[3].confuseDefense = 1f;

        playableCharacters[3].canUse2HWeapon = true;
        playableCharacters[3].canDualWield = false;

        playableCharacters[3].canUseFireDrops = false;
        playableCharacters[3].canUseHolyDrops = true;
        playableCharacters[3].canUseWaterDrops = false;
        playableCharacters[3].canUseLightningDrops = false;
        playableCharacters[3].canUseShadowDrops = true;
        playableCharacters[3].canUseIceDrops = false;

        playableCharacters[3].drops = new Drops[gameDrops.Length];
        playableCharacters[3].drops[25] = gameDrops[25];


        /*    playableCharacters[3].fireDrops = new Drops[10];
            playableCharacters[3].holyDrops = new Drops[10];
            playableCharacters[3].waterDrops = new Drops[10];
            playableCharacters[3].lightningDrops = new Drops[10];
            playableCharacters[3].shadowDrops = new Drops[10];
            playableCharacters[3].iceDrops = new Drops[10];



            playableCharacters[3].holyDrops[0] = holyDrops[0];
            playableCharacters[3].shadowDrops[0] = shadowDrops[0];
    */


        playableCharacters[3].fireDropsLevel = 1f;
        playableCharacters[3].holyDropsLevel = 3f;
        playableCharacters[3].waterDropsLevel = 1f;
        playableCharacters[3].lightningDropsLevel = 1f;
        playableCharacters[3].shadowDropsLevel = 2f;
        playableCharacters[3].iceDropsLevel = 1f;

        playableCharacters[3].fireDropsExperience = 0f;
        playableCharacters[3].waterDropsExperience = 0f;
        playableCharacters[3].lightningDropsExperience = 0f;
        playableCharacters[3].shadowDropsExperience = 0f;
        playableCharacters[3].iceDropsExperience = 0f;
        playableCharacters[3].holyDropsExperience = 0f;

        playableCharacters[3].fireDropsLvlReq = 50f;
        playableCharacters[3].waterDropsLvlReq = 50f;
        playableCharacters[3].lightningDropsLvlReq = 50f;
        playableCharacters[3].shadowDropsLvlReq = 50f;
        playableCharacters[3].iceDropsLvlReq = 50f;
        playableCharacters[3].holyDropsLvlReq = 50f;

        playableCharacters[3].fireDropAttackBonus = 0f;
        playableCharacters[3].waterDropAttackBonus = 0f;
        playableCharacters[3].lightningDropAttackBonus = 0f;
        playableCharacters[3].shadowDropAttackBonus = 0f;
        playableCharacters[3].iceDropAttackBonus = 0f;

        playableCharacters[3].firePhysicalAttackBonus = 0f;
        playableCharacters[3].waterPhysicalAttackBonus = 0f;
        playableCharacters[3].lightningPhysicalAttackBonus = 0f;
        playableCharacters[3].shadowPhysicalAttackBonus = 0f;
        playableCharacters[3].icePhysicalAttackBonus = 0f;

        playableCharacters[3].skills = new Skills[30];
        playableCharacters[3].skillIndex = 15;
        playableCharacters[3].skillTotal = 1;
        playableCharacters[3].skillScale = 1f;
        playableCharacters[3].availableSkillPoints = 0;
        playableCharacters[3].skills[15] = gameSkills[15];

        playableCharacters[3].isPoisoned = false;
        playableCharacters[3].isAsleep = false;
        playableCharacters[3].isConfused = false;
        playableCharacters[3].deathInflicted = false;
        playableCharacters[3].inflicted = false;
        playableCharacters[3].miterInflicted = false;
        playableCharacters[3].haltInflicted = false;

        playableCharacters[3].isProtected = false;
        playableCharacters[3].isEncompassed = false;
        playableCharacters[3].isHastened = false;

        playableCharacters[3].stealChance = 50f;

        playableCharacters[3].poisonDmg = 0f;
        playableCharacters[3].sleepTimer = 0;
        playableCharacters[3].confuseTimer = 0;
        playableCharacters[3].deathTimer = 3;

        playableCharacters[3].GetComponent<SpriteRenderer>().color = Color.white;


        party[0] = playableCharacters[0].GetComponent<Character>().gameObject;

        AddCharacterToParty("Mac");

        //Beginning Weapons
        playableCharacters[0].GetComponent<Grieve>().EquipGrieveWeaponRightOnLoad(gameWeapons[1].GetComponent<Weapon>());
        playableCharacters[1].GetComponent<Mac>().EquipMacWeaponRightOnLoad(gameWeapons[3].GetComponent<Weapon>());
        playableCharacters[2].GetComponent<Field>().EquipFieldWeaponRightOnLoad(gameWeapons[4].GetComponent<Weapon>());
        playableCharacters[3].GetComponent<Riggs>().EquipRiggsWeaponRightOnLoad(gameWeapons[6].GetComponent<Weapon>());
        // partyInventoryReference.AddItemToInventory(gameWeapons[0].GetComponent<Weapon>());
        //partyInventoryReference.AddItemToInventory(gameWeapons[3].GetComponent<Weapon>()); // Depends If Mac Is In Party From Start

        //Beginning Armor
        playableCharacters[0].GetComponent<Grieve>().EquipGrieveChestArmorOnLoad(gameChestArmor[0].GetComponent<ChestArmor>());
        playableCharacters[1].GetComponent<Mac>().EquipMacChestArmorOnLoad(gameChestArmor[0].GetComponent<ChestArmor>());
        playableCharacters[2].GetComponent<Field>().EquipFieldChestArmorOnLoad(gameChestArmor[0].GetComponent<ChestArmor>());
        playableCharacters[3].GetComponent<Riggs>().EquipRiggsChestArmorOnLoad(gameChestArmor[1].GetComponent<ChestArmor>());
        //partyInventoryReference.AddItemToInventory(gameArmor[0].GetComponent<ChestArmor>());
        //partyInventoryReference.AddItemToInventory(gameArmor[0].GetComponent<ChestArmor>()); // Depends If Mac Is In Party From Start

        // Beginning Accessories 
        playableCharacters[0].GetComponent<Grieve>().accessory1 = null;
        playableCharacters[0].GetComponent<Grieve>().accessory2 = null;
        playableCharacters[0].GetComponent<Grieve>().weaponLeft = null;
        playableCharacters[1].GetComponent<Mac>().accessory1 = null;
        playableCharacters[1].GetComponent<Mac>().accessory2 = null;
        playableCharacters[1].GetComponent<Mac>().weaponLeft = null;
        // party[1] = playableCharacters[1].GetComponent<Character>().gameObject;

    }

    public void AddCharacterToParty(string _name)
    {
        for (int i = 0; i < party.Length; i++)
        {
            if (playableCharacters[i].GetComponent<Character>().characterName == _name && party[i] == null)
            {
                party[i] = playableCharacters[i].GetComponent<Character>().gameObject;
                party[i].GetComponent<Character>().isInParty = true;
                ActivatePauseMenuCharacterPanels();
            }
            if (party[1] != null)
            {
                activePartyMember2.GetComponent<SpriteRenderer>().sprite = party[1].GetComponent<SpriteRenderer>().sprite;
                activePartyMember2.SetActive(true);
                //                gameDrops[15].isKnown = true;
                gameSkills[5].isKnown = true;
                playableCharacters[1].activePartyIndex = 1;
                gridReference.classPaths[3].SetActive(true);

            }
            if (party[2] != null)
            {
                ActivateArrangePartyButton();
                gridReference.classPaths[2].SetActive(true);

                if (playableCharacters[0].lvl > 1)
                {
                    playableCharacters[2].lvl = playableCharacters[0].lvl - 1;
                }
                else
                {
                    playableCharacters[2].lvl = playableCharacters[0].lvl;
                }

                playableCharacters[2].healthOffset = -20f;
                playableCharacters[2].manaOffset = 0f;
                playableCharacters[2].energyOffset = 20f;
                playableCharacters[2].maxHealth = Mathf.Round(healthCurve.Evaluate(playableCharacters[2].lvl) + (healthCurve.Evaluate(playableCharacters[2].lvl) * (playableCharacters[2].healthOffset / 100)));
                playableCharacters[2].currentHealth = playableCharacters[2].maxHealth;
                playableCharacters[2].maxMana = Mathf.Round(manaCurve.Evaluate(playableCharacters[2].lvl) + (manaCurve.Evaluate(playableCharacters[2].lvl) * (playableCharacters[2].manaOffset / 100)));
                playableCharacters[2].currentMana = playableCharacters[2].maxMana;
                playableCharacters[2].maxEnergy = Mathf.Round(energyCurve.Evaluate(playableCharacters[2].lvl) + (energyCurve.Evaluate(playableCharacters[2].lvl) * (playableCharacters[2].energyOffset / 100)));
                playableCharacters[2].currentEnergy = playableCharacters[2].maxEnergy;
                playableCharacters[2].strength = Mathf.Round(strengthCurve.Evaluate(playableCharacters[2].lvl) + +(strengthCurve.Evaluate(playableCharacters[2].lvl) * (playableCharacters[2].strengthOffset / 100)));
                playableCharacters[2].maxHealthBase = playableCharacters[2].maxHealth;
                playableCharacters[2].maxManaBase = playableCharacters[2].maxMana;
                playableCharacters[2].maxEnergyBase = playableCharacters[2].maxEnergy;
                playableCharacters[2].strength = Mathf.Round(strengthCurve.Evaluate(playableCharacters[2].lvl) + +(strengthCurve.Evaluate(playableCharacters[2].lvl) * (playableCharacters[2].strengthOffset / 100)));
                playableCharacters[2].intelligence = Mathf.Round(intelligenceCurve.Evaluate(playableCharacters[2].lvl) + +(intelligenceCurve.Evaluate(playableCharacters[2].lvl) * (playableCharacters[2].intelligenceOffset / 100)));
                arrangePartyButtonActive = true;
                activePartyMember3.GetComponent<SpriteRenderer>().sprite = party[2].GetComponent<SpriteRenderer>().sprite;
                activePartyMember3.SetActive(true);
                activeParty.activeParty[2] = party[2].GetComponent<Character>().gameObject;
                //      charAbilityButtons[2].SetActive(true);
                //     charSkillTierButtons[2].SetActive(true);
                //gameDrops[20].isKnown = true;
                gameSkills[10].isKnown = true;
                playableCharacters[2].activePartyIndex = 2;
                gridReference.classPaths[2].SetActive(true);

                activeParty.SetActivePartyIndexes();

            }
            if (party[3] != null)
            {
                battleSystem.battleSwitchButtons = true;
                gridReference.classPaths[3].SetActive(true);

                if (playableCharacters[0].lvl < 99)
                {
                    playableCharacters[3].lvl = playableCharacters[0].lvl + 1;
                }
                else
                {
                    playableCharacters[3].lvl = playableCharacters[0].lvl;
                }

                playableCharacters[3].healthOffset = 30f;
                playableCharacters[3].manaOffset = 0f;
                playableCharacters[3].energyOffset = -20f;
                playableCharacters[3].maxHealth = Mathf.Round(healthCurve.Evaluate(playableCharacters[3].lvl) + (healthCurve.Evaluate(playableCharacters[3].lvl) * (playableCharacters[3].healthOffset / 100)));
                playableCharacters[3].currentHealth = playableCharacters[3].maxHealth;
                playableCharacters[3].maxMana = Mathf.Round(manaCurve.Evaluate(playableCharacters[3].lvl) + (manaCurve.Evaluate(playableCharacters[3].lvl) * (playableCharacters[3].manaOffset / 100)));
                playableCharacters[3].currentMana = playableCharacters[3].maxMana;
                playableCharacters[3].maxEnergy = Mathf.Round(energyCurve.Evaluate(playableCharacters[3].lvl) + (energyCurve.Evaluate(playableCharacters[3].lvl) * (playableCharacters[3].energyOffset / 100)));
                playableCharacters[3].currentEnergy = playableCharacters[3].maxEnergy;
                playableCharacters[3].strength = Mathf.Round(strengthCurve.Evaluate(playableCharacters[3].lvl) + +(strengthCurve.Evaluate(playableCharacters[3].lvl) * (playableCharacters[3].strengthOffset / 100)));
                playableCharacters[3].maxHealthBase = playableCharacters[3].maxHealth;
                playableCharacters[3].maxManaBase = playableCharacters[3].maxMana;
                playableCharacters[3].maxEnergyBase = playableCharacters[3].maxEnergy;
                playableCharacters[3].strength = Mathf.Round(strengthCurve.Evaluate(playableCharacters[3].lvl) + +(strengthCurve.Evaluate(playableCharacters[3].lvl) * (playableCharacters[3].strengthOffset / 100)));
                playableCharacters[3].intelligence = Mathf.Round(intelligenceCurve.Evaluate(playableCharacters[3].lvl) + +(intelligenceCurve.Evaluate(playableCharacters[3].lvl) * (playableCharacters[3].intelligenceOffset / 100)));
                //     charAbilityButtons[3].SetActive(true);
                //     charSkillTierButtons[3].SetActive(true);
                //         gameDrops[25].isKnown = true;
                gameSkills[15].isKnown = true;
                playableCharacters[3].activePartyIndex = -1;
                gridReference.classPaths[5].SetActive(true);

            }
        }
    }

    // Handles Experience Point gain, as well as character Level Ups.
    public void GiveExperiencePoints(float xp)
    {
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i] != null)
            {
                if (party[i].GetComponent<Character>().lvl < 99)
                {
                    if (party[i].GetComponent<Character>().isInActiveParty == true)
                    {
                        party[i].GetComponent<Character>().experiencePoints += xp;
                    }
                    else
                    {
                        party[i].GetComponent<Character>().experiencePoints += Mathf.Round((xp / 1.5f));
                    }

                    // Level Up
                    if (party[i].GetComponent<Character>().experiencePoints >= party[i].GetComponent<Character>().levelUpReq)
                    {
                        party[i].GetComponent<Character>().lvl = party[i].GetComponent<Character>().lvl + 1;

                        // Health
                        //float gainedHealth = Mathf.Round((party[i].GetComponent<Character>().maxHealth * 3 / 2) - party[i].GetComponent<Character>().maxHealth);
                        float newHealthCheck = Mathf.Round(healthCurve.Evaluate(party[i].GetComponent<Character>().lvl) * (party[i].GetComponent<Character>().healthOffset / 100));
                        float newHealth = Mathf.Round(healthCurve.Evaluate(party[i].GetComponent<Character>().lvl) + newHealthCheck);
                        float gainedHealth = Mathf.Round(newHealth - party[i].GetComponent<Character>().maxHealth);

                        // Mana
                        float newManaCheck = Mathf.Round(manaCurve.Evaluate(party[i].GetComponent<Character>().lvl) * (party[i].GetComponent<Character>().manaOffset / 100));
                        float newMana = Mathf.Round(manaCurve.Evaluate(party[i].GetComponent<Character>().lvl) + newManaCheck);
                        float gainedMana = Mathf.Round(newMana - party[i].GetComponent<Character>().maxMana);

                        // Energy
                        float newEnergyCheck = Mathf.Round(energyCurve.Evaluate(party[i].GetComponent<Character>().lvl) * (party[i].GetComponent<Character>().energyOffset / 100));
                        float newEnergy = Mathf.Round(energyCurve.Evaluate(party[i].GetComponent<Character>().lvl) + newEnergyCheck);
                        float gainedEnergy = Mathf.Round(newEnergy - party[i].GetComponent<Character>().maxEnergy);


                        float newExperiencePointsTotal = Mathf.Round((party[i].GetComponent<Character>().experiencePoints - party[i].GetComponent<Character>().levelUpReq));

                        if (newHealth > 9999f)
                        {
                            if (party[i].GetComponent<Character>().healthCapped)
                            {
                                party[i].GetComponent<Character>().maxHealth = 9999f;
                            }
                            else
                            {
                                party[i].GetComponent<Character>().maxHealth = newHealth;
                            }
                        }
                        else
                        {
                            party[i].GetComponent<Character>().maxHealth = newHealth;
                        }

                        if (newMana > 999f)
                        {
                            if (party[i].GetComponent<Character>().manaCapped)
                            {
                                party[i].GetComponent<Character>().maxMana = 999f;
                            }
                            else
                            {
                                party[i].GetComponent<Character>().maxMana = newMana;
                            }
                        }
                        else
                        {
                            party[i].GetComponent<Character>().maxMana = newMana;
                        }

                        if (newEnergy > 999f)
                        {
                            if (party[i].GetComponent<Character>().energyCapped)
                            {
                                party[i].GetComponent<Character>().maxEnergy = 999f;
                            }
                            else
                            {
                                party[i].GetComponent<Character>().maxEnergy = newEnergy;
                            }
                        }
                        else
                        {
                            party[i].GetComponent<Character>().maxEnergy = newEnergy;

                        }


                        //                       party[i].GetComponent<Character>().maxHealth = Mathf.Round(party[i].GetComponent<Character>().maxHealth * 3 / 2);
                        party[i].GetComponent<Character>().currentHealth = party[i].GetComponent<Character>().maxHealth;
                        party[i].GetComponent<Character>().currentMana = party[i].GetComponent<Character>().maxMana;
                        party[i].GetComponent<Character>().currentEnergy = party[i].GetComponent<Character>().maxEnergy;

                        float newStrengthCheck = Mathf.Round(strengthCurve.Evaluate(party[i].GetComponent<Character>().lvl) * (party[i].GetComponent<Character>().strengthOffset / 100));
                        float newStrength = Mathf.Round(strengthCurve.Evaluate(party[i].GetComponent<Character>().lvl) + newStrengthCheck);
                        float gainedStrength = Mathf.Round(newStrength - party[i].GetComponent<Character>().strength);

                        party[i].GetComponent<Character>().strength = newStrength;
                        party[i].GetComponent<Character>().physicalDefense = party[i].GetComponent<Character>().physicalDefense + 0.5f;

                        float newIntelligenceCheck = Mathf.Round(intelligenceCurve.Evaluate(party[i].GetComponent<Character>().lvl) * (party[i].GetComponent<Character>().intelligenceOffset / 100));
                        float newIntelligence = Mathf.Round(intelligenceCurve.Evaluate(party[i].GetComponent<Character>().lvl) + newIntelligenceCheck);
                        float gainedIntelligence = Mathf.Round(newIntelligence - party[i].GetComponent<Character>().intelligence);

                        party[i].GetComponent<Character>().intelligence = newIntelligence;

                        party[i].GetComponent<Character>().experiencePoints = newExperiencePointsTotal;
                        party[i].GetComponent<Character>().levelUpReq = Mathf.Round(party[i].GetComponent<Character>().levelUpReq * 6 / 2);

                        party[i].GetComponent<Character>().sleepDefense = 0.5f;
                        party[i].GetComponent<Character>().confuseDefense += 0.5f;



                        party[i].GetComponent<Character>().skillScale += 1f;

                        /*if (party[i].GetComponent<Character>().lvl <= 20)
                        {
                            if (party[i].GetComponent<Character>().lvl % 5 == 0)
                            {
                                party[i].GetComponent<Character>().skillIndex++;
                                party[i].GetComponent<Character>().skillTotal++;

                                switch (party[i].GetComponent<Character>().characterName)
                                {
                                    case "Grieve":
                                        party[i].GetComponent<Grieve>().skills[party[i].GetComponent<Grieve>().skillIndex] = gameSkills[party[i].GetComponent<Grieve>().skillIndex];
                                        break;

                                    case "Mac":
                                        party[i].GetComponent<Mac>().skills[party[i].GetComponent<Mac>().skillIndex] = gameSkills[party[i].GetComponent<Mac>().skillIndex];
                                        break;

                                    case "Field":
                                        party[i].GetComponent<Field>().skills[party[i].GetComponent<Field>().skillIndex] = gameSkills[party[i].GetComponent<Field>().skillIndex];
                                        break;

                                    case "Riggs":
                                        party[i].GetComponent<Riggs>().skills[party[i].GetComponent<Riggs>().skillIndex] = gameSkills[party[i].GetComponent<Riggs>().skillIndex];
                                        break;
                                }
                            }
                        }
                        else
                        {
                            if (party[i].GetComponent<Character>().lvl % 3 == 0)
                            {
                                party[i].GetComponent<Character>().availableSkillPoints++;
                            }
                        }*/

                        if (party[i] == activeParty.activeParty[0])
                        {
                            char1LevelUp = true;
                            char1LevelUpPanelReference[0].text = activeParty.activeParty[0].GetComponent<Character>().characterName;
                            char1LevelUpPanelReference[1].text = "HP + " + gainedHealth.ToString();
                            char1LevelUpPanelReference[2].text = "MP + " + gainedMana.ToString();
                            char1LevelUpPanelReference[3].text = "ENR + " + gainedEnergy.ToString();
                            char1LevelUpPanelReference[4].text = "Physical Damage + " + gainedStrength.ToString();
                            char1LevelUpPanelReference[5].text = "Physical Defense + 0.5%";

                            /* if (activeParty.activeParty[0].GetComponent<Character>().lvl <= 20)
                             {
                                 if (gameSkills[activeParty.activeParty[0].GetComponent<Character>().skillIndex] != null)
                                 {
                                     if (activeParty.activeParty[0].GetComponent<Character>().lvl % 5 == 0)
                                     {
                                         char1LevelUpPanelReference[6].text = gameSkills[activeParty.activeParty[0].GetComponent<Character>().skillIndex].skillName + " learned!";
                                     }
                                     else
                                     {
                                         char1LevelUpPanelReference[6].text = string.Empty;
                                     }
                                 }
                             }
                             else
                             {
                                 if (activeParty.activeParty[0].GetComponent<Character>().lvl % 3 == 0)
                                 {
                                     char1LevelUpPanelReference[6].text = "Skill Point earned!";
                                 }
                             }
                         }*/

                            if (party[i] == activeParty.activeParty[1] && activeParty.activeParty[1] != null)
                            {
                                char2LevelUp = true;
                                char2LevelUpPanelReference[0].text = activeParty.activeParty[1].GetComponent<Character>().characterName;
                                char2LevelUpPanelReference[1].text = "HP + " + gainedHealth.ToString();
                                char2LevelUpPanelReference[2].text = "MP + " + gainedMana.ToString();
                                char2LevelUpPanelReference[3].text = "ENR + " + gainedEnergy.ToString();
                                char2LevelUpPanelReference[4].text = "Physical Damage + " + gainedStrength.ToString();
                                char2LevelUpPanelReference[5].text = "Physical Defense + 0.5%";

                                /* if (activeParty.activeParty[1].GetComponent<Character>().lvl <= 25)
                                 {
                                     if (gameSkills[activeParty.activeParty[1].GetComponent<Character>().skillIndex] != null)
                                     {
                                         if (activeParty.activeParty[1].GetComponent<Character>().lvl % 5 == 0)
                                         {
                                             char2LevelUpPanelReference[6].text = gameSkills[activeParty.activeParty[1].GetComponent<Character>().skillIndex].skillName + " learned!";
                                         }
                                         else
                                         {
                                             char2LevelUpPanelReference[6].text = string.Empty;
                                         }
                                     }
                                 }
                                 else
                                 {

                                     if (activeParty.activeParty[1].GetComponent<Character>().lvl % 3 == 0)
                                     {
                                         char2LevelUpPanelReference[6].text = "Skill Point earned!";
                                     }
                                 }
                             }*/

                                if (party[i] == activeParty.activeParty[2] && activeParty.activeParty[2] != null)
                                {
                                    char3LevelUp = true;
                                    char3LevelUpPanelReference[0].text = activeParty.activeParty[2].GetComponent<Character>().characterName;
                                    char3LevelUpPanelReference[1].text = "HP + " + gainedHealth.ToString();
                                    char3LevelUpPanelReference[2].text = "MP + " + gainedMana.ToString();
                                    char3LevelUpPanelReference[3].text = "ENR + " + gainedEnergy.ToString(); ;
                                    char3LevelUpPanelReference[4].text = "Physical Damage + " + gainedStrength.ToString();
                                    char3LevelUpPanelReference[5].text = "Physical Defense + 0.5%";

                                    if (activeParty.activeParty[2].GetComponent<Character>().lvl <= 25)
                                    {
                                        if (activeParty.activeParty[2].GetComponent<Character>().lvl % 5 == 0)
                                        {
                                            char3LevelUpPanelReference[6].text = gameSkills[activeParty.activeParty[2].GetComponent<Character>().skillIndex].skillName + " learned!";
                                        }
                                        else
                                        {
                                            char3LevelUpPanelReference[6].text = string.Empty;
                                        }
                                    }
                                    else
                                    {
                                        if (activeParty.activeParty[2].GetComponent<Character>().lvl % 3 == 0)
                                        {
                                            char3LevelUpPanelReference[6].text = "Skill Point earned!";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    // Handles Experience Point gain for quests, as well as character Level Ups.
    public void GiveQuestExperiencePoints(float xp)
    {
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i] != null)
            {
                if (party[i].GetComponent<Character>().lvl < 99)
                {

                    party[i].GetComponent<Character>().experiencePoints += xp;

                    // Level Up
                    if (party[i].GetComponent<Character>().experiencePoints >= party[i].GetComponent<Character>().levelUpReq)
                    {
                        party[i].GetComponent<Character>().lvl = party[i].GetComponent<Character>().lvl + 1;

                        // Health
                        //float gainedHealth = Mathf.Round((party[i].GetComponent<Character>().maxHealth * 3 / 2) - party[i].GetComponent<Character>().maxHealth);
                        float newHealthCheck = Mathf.Round(healthCurve.Evaluate(party[i].GetComponent<Character>().lvl) * (party[i].GetComponent<Character>().healthOffset / 100));
                        float newHealth = Mathf.Round(healthCurve.Evaluate(party[i].GetComponent<Character>().lvl) + newHealthCheck);
                        float gainedHealth = Mathf.Round(newHealth - party[i].GetComponent<Character>().maxHealth);

                        // Mana
                        float newManaCheck = Mathf.Round(manaCurve.Evaluate(party[i].GetComponent<Character>().lvl) * (party[i].GetComponent<Character>().manaOffset / 100));
                        float newMana = Mathf.Round(manaCurve.Evaluate(party[i].GetComponent<Character>().lvl) + newManaCheck);
                        float gainedMana = Mathf.Round(newMana - party[i].GetComponent<Character>().maxMana);

                        // Energy
                        float newEnergyCheck = Mathf.Round(energyCurve.Evaluate(party[i].GetComponent<Character>().lvl) * (party[i].GetComponent<Character>().energyOffset / 100));
                        float newEnergy = Mathf.Round(energyCurve.Evaluate(party[i].GetComponent<Character>().lvl) + newEnergyCheck);
                        float gainedEnergy = Mathf.Round(newEnergy - party[i].GetComponent<Character>().maxEnergy);


                        float newExperiencePointsTotal = Mathf.Round((party[i].GetComponent<Character>().experiencePoints - party[i].GetComponent<Character>().levelUpReq));

                        if (newHealth > 9999f)
                        {
                            if (party[i].GetComponent<Character>().healthCapped)
                            {
                                party[i].GetComponent<Character>().maxHealth = 9999f;
                            }
                            else
                            {
                                party[i].GetComponent<Character>().maxHealth = newHealth;
                            }
                        }
                        else
                        {
                            party[i].GetComponent<Character>().maxHealth = newHealth;
                        }

                        if (newMana > 999f)
                        {
                            if (party[i].GetComponent<Character>().manaCapped)
                            {
                                party[i].GetComponent<Character>().maxMana = 999f;
                            }
                            else
                            {
                                party[i].GetComponent<Character>().maxMana = newMana;
                            }
                        }
                        else
                        {
                            party[i].GetComponent<Character>().maxMana = newMana;
                        }

                        if (newEnergy > 999f)
                        {
                            if (party[i].GetComponent<Character>().energyCapped)
                            {
                                party[i].GetComponent<Character>().maxEnergy = 999f;
                            }
                            else
                            {
                                party[i].GetComponent<Character>().maxEnergy = newEnergy;
                            }
                        }
                        else
                        {
                            party[i].GetComponent<Character>().maxEnergy = newEnergy;

                        }


                        //                       party[i].GetComponent<Character>().maxHealth = Mathf.Round(party[i].GetComponent<Character>().maxHealth * 3 / 2);
                        party[i].GetComponent<Character>().currentHealth = party[i].GetComponent<Character>().maxHealth;
                        party[i].GetComponent<Character>().currentMana = party[i].GetComponent<Character>().maxMana;
                        party[i].GetComponent<Character>().currentEnergy = party[i].GetComponent<Character>().maxEnergy;

                        float newStrengthCheck = Mathf.Round(strengthCurve.Evaluate(party[i].GetComponent<Character>().lvl) * (party[i].GetComponent<Character>().strengthOffset / 100));
                        float newStrength = Mathf.Round(strengthCurve.Evaluate(party[i].GetComponent<Character>().lvl) + newStrengthCheck);
                        float gainedStrength = Mathf.Round(newStrength - party[i].GetComponent<Character>().strength);

                        party[i].GetComponent<Character>().strength = newStrength;
                        party[i].GetComponent<Character>().physicalDefense = party[i].GetComponent<Character>().physicalDefense + 0.5f;

                        party[i].GetComponent<Character>().experiencePoints = newExperiencePointsTotal;
                        party[i].GetComponent<Character>().levelUpReq = Mathf.Round(party[i].GetComponent<Character>().levelUpReq * 6 / 2);

                        party[i].GetComponent<Character>().sleepDefense = 0.5f;
                        party[i].GetComponent<Character>().confuseDefense += 0.5f;



                        party[i].GetComponent<Character>().skillScale += 1f;

                    }
                }
            }
        }
    }

    // Out of battle
    public void UseItem(int _target)
    {

        if (!itemToBeUsed.isDrop)
        {
            // Consumables
            switch (itemToBeUsed.itemName)
            {

                case "Health Potion":

                    if (party[_target].GetComponent<Character>().currentHealth == party[_target].GetComponent<Character>().maxHealth)
                    {
                        return;
                    }
                    else
                    {
                        party[_target].GetComponent<Character>().currentHealth += itemToBeUsed.itemPower;

                        if (party[_target].GetComponent<Character>().currentHealth > party[_target].GetComponent<Character>().maxHealth)
                        {
                            party[_target].GetComponent<Character>().currentHealth = party[_target].GetComponent<Character>().maxHealth;
                        }

                        ItemDisplayCharacterStats(_target);
                        partyInventoryReference.SubtractItemFromInventory(itemToBeUsed);
                    }
                    break;

                case "Mana Potion":

                    if (party[_target].GetComponent<Character>().currentMana == party[_target].GetComponent<Character>().maxMana)
                    {
                        return;
                    }
                    else
                    {

                        party[_target].GetComponent<Character>().currentMana += itemToBeUsed.itemPower;

                        if (party[_target].GetComponent<Character>().currentMana > party[_target].GetComponent<Character>().maxMana)
                        {
                            party[_target].GetComponent<Character>().currentMana = party[_target].GetComponent<Character>().maxMana;
                        }
                        ItemDisplayCharacterStats(_target);
                        partyInventoryReference.SubtractItemFromInventory(itemToBeUsed);
                    }
                    break;

                case "Antidote":

                    if (!party[_target].GetComponent<Character>().isPoisoned)
                    {
                        return;
                    }
                    else
                    {

                        party[_target].GetComponent<Character>().isPoisoned = false;
                        party[_target].GetComponent<Character>().poisonDmg = 0;
                        ItemDisplayCharacterStats(_target);
                        partyInventoryReference.SubtractItemFromInventory(itemToBeUsed);
                    }
                    break;
            }

            itemToBeUsed = null;
            partyInventoryReference.OpenInventoryMenu();
        }
        else
        {
            // Drops
            switch (itemToBeUsed.itemName)
            {
                case "Fire Blast":
                    if (party[_target].GetComponent<Character>().KnowsDrop(Engine.e.itemToBeUsed.drop))
                    {
                        return;
                    }
                    else
                    {
                        Engine.e.party[_target].GetComponent<Character>().TeachDrop(Engine.e.gameDrops[itemToBeUsed.drop.dropIndex]);
                        Engine.e.gridReference.ActivateDropNode(_target, Engine.e.gameDrops[itemToBeUsed.drop.dropIndex]);
                        break;
                    }
            }
            characterBeingTargeted = _target;
            partyInventoryReference.ConfirmDropCheck();
        }
    }

    // Teaches Drop to character (via inventory item)
    public void UseItemDrop()
    {
        party[characterBeingTargeted].GetComponent<Character>().TeachDrop(itemToBeUsed.drop);
        gridReference.ActivateDropNode(characterBeingTargeted, itemToBeUsed.drop);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(partyInventoryReference.itemInventorySlots[partyInventoryReference.inventoryPointerIndex].gameObject);

        partyInventoryReference.SubtractItemFromInventory(itemToBeUsed);
        partyInventoryReference.confirmDropCheck.SetActive(false);
        itemToBeUsed = null;

    }

    // Function for "consuming" an item, in battle.
    public void UseItemInBattle(int _target)
    {
        Item _itemToBeUsed = null;

        GameObject spawnGOLoc = null;
        GameObject targetGOLoc = null;

        if (battleSystem.currentInQueue == BattleState.CHAR1TURN)
        {
            _itemToBeUsed = battleSystem.char1ItemToBeUsed;
            spawnGOLoc = activeParty.gameObject;
        }
        if (battleSystem.currentInQueue == BattleState.CHAR2TURN)
        {
            _itemToBeUsed = battleSystem.char2ItemToBeUsed;
            spawnGOLoc = activePartyMember2;

        }
        if (battleSystem.currentInQueue == BattleState.CHAR3TURN)
        {
            _itemToBeUsed = battleSystem.char3ItemToBeUsed;
            spawnGOLoc = activePartyMember3;

        }

        if (_target == 0)
        {
            targetGOLoc = activeParty.gameObject;
        }
        if (_target == 1)
        {
            targetGOLoc = activePartyMember2;
        }
        if (_target == 2)
        {
            targetGOLoc = activePartyMember3;
        }

        // Consumables
        switch (_itemToBeUsed.itemName)
        {
            case "Health Potion":


                battleSystem.HandleItemAnim(spawnGOLoc, targetGOLoc, _itemToBeUsed);

                battleSystem.SetDamagePopupTextOne(targetGOLoc.transform.position, _itemToBeUsed.itemPower.ToString(), Color.green);

                if (activeParty.activeParty[_target].GetComponent<Character>().currentHealth > 0)
                {
                    activeParty.activeParty[_target].GetComponent<Character>().currentHealth += _itemToBeUsed.itemPower;
                }

                if (activeParty.activeParty[_target].GetComponent<Character>().currentHealth > activeParty.activeParty[_target].GetComponent<Character>().maxHealth)
                {
                    activeParty.activeParty[_target].GetComponent<Character>().currentHealth = activeParty.activeParty[_target].GetComponent<Character>().maxHealth;

                }

                ItemDisplayCharacterStats(_target);
                break;

            case "Mana Potion":


                battleSystem.HandleItemAnim(spawnGOLoc, targetGOLoc, _itemToBeUsed);
                battleSystem.SetDamagePopupTextOne(targetGOLoc.transform.position, _itemToBeUsed.itemPower.ToString(), Color.blue);

                activeParty.activeParty[_target].GetComponent<Character>().currentMana += _itemToBeUsed.itemPower;

                if (activeParty.activeParty[_target].GetComponent<Character>().currentMana > activeParty.activeParty[_target].GetComponent<Character>().maxMana)
                {
                    activeParty.activeParty[_target].GetComponent<Character>().currentMana = activeParty.activeParty[_target].GetComponent<Character>().maxMana;
                }

                ItemDisplayCharacterStats(_target);
                break;


            case "Ashes":

                if (activeParty.activeParty[_target].GetComponent<Character>().currentHealth == 0)
                {
                    activeParty.activeParty[_target].GetComponent<Character>().currentHealth += _itemToBeUsed.itemPower;
                }
                if (activeParty.activeParty[_target].GetComponent<Character>().currentHealth > activeParty.activeParty[_target].GetComponent<Character>().maxHealth)
                {
                    activeParty.activeParty[_target].GetComponent<Character>().currentHealth = activeParty.activeParty[_target].GetComponent<Character>().maxHealth;
                }
                ItemDisplayCharacterStats(_target);
                break;

            case "Antidote":


                battleSystem.HandleItemAnim(spawnGOLoc, targetGOLoc, _itemToBeUsed);
                battleSystem.SetDamagePopupTextOne(targetGOLoc.transform.position, "Cured!", Color.green);

                activeParty.activeParty[_target].GetComponent<Character>().isPoisoned = false;
                activeParty.activeParty[_target].GetComponent<Character>().inflicted = false;

                ItemDisplayCharacterStats(_target);

                break;
                /*case "Elixir":


                    battleSystem.HandleItemAnim(spawnGOLoc, targetGOLoc, _itemToBeUsed);


                    battleSystem.SetDamagePopupTextAllTeam(_itemToBeUsed.itemPower.ToString(), Color.green);

                    for (int i = 0; i < activeParty.activeParty.Length; i++)
                    {
                        if (activeParty.activeParty[i] != null && activeParty.activeParty[i].GetComponent<Character>().currentHealth > 0)
                        {
                            activeParty.activeParty[i].GetComponent<Character>().currentHealth += _itemToBeUsed.itemPower;

                            if (activeParty.activeParty[i].GetComponent<Character>().currentHealth > activeParty.activeParty[i].GetComponent<Character>().maxHealth)
                            {
                                activeParty.activeParty[i].GetComponent<Character>().currentHealth = activeParty.activeParty[i].GetComponent<Character>().maxHealth;
                            }
                        }
                    }

                    ItemDisplayCharacterStats(_target);
                    partyInventoryReference.SubtractItemFromInventory(_itemToBeUsed);
                    break;
                */
        }

        if (!_itemToBeUsed.targetAll)
        {
            partyInventoryReference.SubtractItemFromInventory(_itemToBeUsed);
        }

        itemToBeUsed = null;
    }

    // Dismisses "character target" buttons, outside of combat.
    public void ItemDismissCharacterUseButtons()
    {
        for (int i = 0; i < itemMenuPanels.Length; i++)
        {
            if (party[i] != null)
            {
                itemConfirmUseButtons[i].SetActive(false);
                itemDropConfirmUseButtons[i].SetActive(false);
            }
        }
    }

    // Calls the Coroutine for unloading the previous scene. Used on scene transitions.
    public void UnloadScene(string scene)
    {
        StartCoroutine(Unload(scene));
    }

    // Unloads the previous scene. 
    IEnumerator Unload(string scene)
    {
        yield return null;
        SceneManager.UnloadSceneAsync(scene);
    }

    public void SetClasses()
    {
        charClasses = new string[12];

        for (int i = 0; i < playableCharacters.Length; i++)
        {
            playableCharacters[i].classUnlocked = new bool[charClasses.Length];
        }

        charClasses[0] = "Soldier";
        charClasses[1] = "Shaman";
        charClasses[2] = "Thief";
        charClasses[3] = "Mage";
        charClasses[4] = "Assassin";
        charClasses[5] = "Ronin";

        charClasses[6] = "Monk";
        charClasses[7] = "Watcher";
        charClasses[8] = "Quickpocket";
        charClasses[9] = "Evoker";
        charClasses[10] = "Shinobi";
        charClasses[11] = "Bushi";

    }

    // Establishes a new Save File by storing the data found in the GameData class.
    public void SaveGame(int saveSlot)
    {
        if (ableToSave)
        {
            SaveSystem.SaveGame(this, saveSlot);

        }
        else
        {
            Debug.Log("Not at save point.");

        }
    }

    // Sets values based on an existing Save File.    
    public void LoadGame(int saveSlot)
    {
        GameData gameData = SaveSystem.LoadGame(saveSlot);
        loading = true;
        recentAutoSave = true;
        fileMenuReference.loading = false;
        fileMenuReference.saveMenuSet = false;
        fileMenuReference.gameObject.SetActive(false);
        SetInventoryArrayPositions();
        party = new GameObject[playableCharacters.Length];
        activeParty.activeParty = new GameObject[3];

        partyInventoryReference.partyInventory = new Item[partyInventoryReference.itemInventorySlots.Length];

        partyInventoryReference.weapons = new Weapon[partyInventoryReference.weaponInventorySlots.Length];
        partyInventoryReference.chestArmor = new ChestArmor[partyInventoryReference.chestArmorInventorySlots.Length];
        partyInventoryReference.accessories = new Accessory[partyInventoryReference.accessoryInventorySlots.Length];

        charEquippedWeaponRight = new Item[playableCharacters.Length];
        charEquippedWeaponLeft = new Item[playableCharacters.Length];
        charEquippedChestArmor = new Item[playableCharacters.Length];
        charEquippedAccessory1 = new Item[playableCharacters.Length];
        charEquippedAccessory2 = new Item[playableCharacters.Length];

        adventureLogReference.questLog = new Quest[adventureLogReference.questSlots.Length];
        adventureLogReference.completedQuestLog = new Quest[adventureLogReference.completedQuestSlots.Length];


        for (int i = 0; i < gameData.partySize; i++)
        {
            if (party[i] == null)
            {
                party[i] = playableCharacters[i].gameObject;

                party[i].GetComponent<Character>().characterName = gameData.charNames[i];
                party[i].GetComponent<Character>().maxHealth = gameData.charMaxHealth[i];
                party[i].GetComponent<Character>().currentHealth = gameData.charMaxHealth[i];
                party[i].GetComponent<Character>().maxMana = gameData.charMaxMana[i];
                party[i].GetComponent<Character>().currentMana = gameData.charMaxMana[i];
                party[i].GetComponent<Character>().lvl = gameData.charLvl[i];

                party[i].GetComponent<Character>().skillScale = gameData.charSkillScale[i];
                party[i].GetComponent<Character>().skillIndex = gameData.charSkillIndex[i];
                party[i].GetComponent<Character>().availableSkillPoints = gameData.charAvailableSkillPoints[i];
                party[i].GetComponent<Character>().stealChance = gameData.charStealChance[i];
                party[i].GetComponent<Character>().haste = gameData.charHaste[i];


                party[i].GetComponent<Character>().fireDropsLevel = gameData.charFireDropLevel[i];
                party[i].GetComponent<Character>().waterDropsLevel = gameData.charWaterDropLevel[i];
                party[i].GetComponent<Character>().lightningDropsLevel = gameData.charLightningDropLevel[i];
                party[i].GetComponent<Character>().shadowDropsLevel = gameData.charShadowDropLevel[i];
                party[i].GetComponent<Character>().iceDropsLevel = gameData.charIceDropLevel[i];
                party[i].GetComponent<Character>().holyDropsLevel = gameData.charHolyDropLevel[i];

                party[i].GetComponent<Character>().fireDropsExperience = gameData.charFireDropExperience[i];
                party[i].GetComponent<Character>().waterDropsExperience = gameData.charWaterDropExperience[i];
                party[i].GetComponent<Character>().lightningDropsExperience = gameData.charLightningDropExperience[i];
                party[i].GetComponent<Character>().shadowDropsExperience = gameData.charShadowDropExperience[i];
                party[i].GetComponent<Character>().iceDropsExperience = gameData.charIceDropExperience[i];
                party[i].GetComponent<Character>().holyDropsExperience = gameData.charHolyDropExperience[i];

                party[i].GetComponent<Character>().fireDropsLvlReq = gameData.charFireDropLvlReq[i];
                party[i].GetComponent<Character>().waterDropsLvlReq = gameData.charWaterDropLvlReq[i];
                party[i].GetComponent<Character>().lightningDropsLvlReq = gameData.charLightningDropLvlReq[i];
                party[i].GetComponent<Character>().shadowDropsLvlReq = gameData.charShadowDropLvlReq[i];
                party[i].GetComponent<Character>().iceDropsLvlReq = gameData.charIceDropLvlReq[i];
                party[i].GetComponent<Character>().holyDropsLvlReq = gameData.charHolyDropLvlReq[i];

                party[i].GetComponent<Character>().fireDropAttackBonus = gameData.charFireDropAttackBonus[i];
                party[i].GetComponent<Character>().waterDropAttackBonus = gameData.charWaterDropAttackBonus[i];
                party[i].GetComponent<Character>().lightningDropAttackBonus = gameData.charLightningDropAttackBonus[i];
                party[i].GetComponent<Character>().shadowDropAttackBonus = gameData.charShadowDropAttackBonus[i];
                party[i].GetComponent<Character>().iceDropAttackBonus = gameData.charIceDropAttackBonus[i];

                party[i].GetComponent<Character>().firePhysicalAttackBonus = gameData.charFirePhysicalAttackBonus[i];
                party[i].GetComponent<Character>().waterPhysicalAttackBonus = gameData.charWaterPhysicalAttackBonus[i];
                party[i].GetComponent<Character>().lightningPhysicalAttackBonus = gameData.charLightningPhysicalAttackBonus[i];
                party[i].GetComponent<Character>().shadowPhysicalAttackBonus = gameData.charShadowPhysicalAttackBonus[i];
                party[i].GetComponent<Character>().icePhysicalAttackBonus = gameData.charIcePhysicalAttackBonus[i];

                party[i].GetComponent<Character>().strength = gameData.charPhysicalDamage[i];
                party[i].GetComponent<Character>().experiencePoints = gameData.charXP[i];
                party[i].GetComponent<Character>().levelUpReq = gameData.charLvlUpReq[i];
                party[i].GetComponent<Character>().isInParty = gameData.charInParty[i];

                party[i].GetComponent<Character>().canDualWield = gameData.canDualWield[i];
                party[i].GetComponent<Character>().canUse2HWeapon = gameData.canUse2HWeapon[i];

                party[i].GetComponent<Character>().dodgeChance = gameData.charDodgeChance[i];
                party[i].GetComponent<Character>().physicalDefense = gameData.charPhysicalDefense[i];
                party[i].GetComponent<Character>().fireDefense = gameData.charFireDefense[i];
                party[i].GetComponent<Character>().waterDefense = gameData.charWaterDefense[i];
                party[i].GetComponent<Character>().lightningDefense = gameData.charLightningDefense[i];
                party[i].GetComponent<Character>().shadowDefense = gameData.charShadowDefense[i];
                party[i].GetComponent<Character>().iceDefense = gameData.charIceDefense[i];
                party[i].GetComponent<Character>().poisonDefense = gameData.charPoisonDefense[i];
                party[i].GetComponent<Character>().sleepDefense = gameData.charSleepDefense[i];
                party[i].GetComponent<Character>().confuseDefense = gameData.charConfuseDefense[i];


                party[i].GetComponent<Character>().drops = new Drops[gameDrops.Length];
                party[i].GetComponent<Character>().skills = new Skills[gameSkills.Length];

                gridReference.classPaths[i].SetActive(true);
                //  party[i].GetComponent<Character>().holyDrops = new Drops[holyDrops.Length];
                // party[i].GetComponent<Character>().waterDrops = new Drops[waterDrops.Length];
                //  party[i].GetComponent<Character>().lightningDrops = new Drops[lightningDrops.Length];
                //  party[i].GetComponent<Character>().shadowDrops = new Drops[shadowDrops.Length];
                //  party[i].GetComponent<Character>().iceDrops = new Drops[iceDrops.Length];



                // Drops
                for (int _drops = 0; _drops < gameDrops.Length; _drops++)
                {
                    if (gameData.charDrops[i, _drops] != null)
                    {
                        if (gameData.charDrops[i, _drops] == gameDrops[_drops].dropName)
                        {
                            party[i].GetComponent<Character>().drops[_drops] = gameDrops[_drops];

                            gameDrops[_drops].isKnown = true;
                        }
                    }
                }

                // Skills
                for (int _skills = 0; _skills < gameSkills.Length; _skills++)
                {
                    if (gameData.charSkills[i, _skills] != null)
                    {
                        if (gameData.charSkills[i, _skills] == gameSkills[_skills].skillName)
                        {
                            party[i].GetComponent<Character>().skills[_skills] = gameSkills[_skills];

                            gameSkills[_skills].isKnown = true;
                        }
                    }
                }

                if (gameData.activeParty[0] == party[i].GetComponent<Character>().characterName)
                {
                    activeParty.activeParty[0] = party[i].gameObject;
                }
                if (gameData.activeParty[1] != null && gameData.activeParty[1] == party[i].GetComponent<Character>().characterName)
                {
                    activeParty.activeParty[1] = party[i].gameObject;
                }
                if (gameData.activeParty[2] != null && gameData.activeParty[2] == party[i].GetComponent<Character>().characterName)
                {
                    activeParty.activeParty[2] = party[i].gameObject;
                }
            }

        }

        for (int i = 0; i < gameData.partyInvNames.Length; i++)
        {
            if (gameData.partyInvNames[i] != null)
            {
                // Items
                for (int k = 0; k < gameInventory.Count; k++)
                {
                    if (gameInventory[k] != null)
                    {
                        if (gameInventory[k].itemName == gameData.partyInvNames[i])
                        {
                            partyInventoryReference.AddItemToInventory(gameInventory[k]);
                        }

                        // Weapon Equip
                        // if (gameInventory[k].itemName == gameData.grieveWeaponEquip)
                        {
                            //  playableCharacters[0].GetComponent<Grieve>().EquipGrieveWeaponOnLoad(gameInventory[k].GetComponent<GrieveWeapons>());
                        }
                        if (party[1] != null)
                        {
                            //     if (gameInventory[k].itemName == gameData.macWeaponEquip)
                            {
                                //      playableCharacters[1].GetComponent<Mac>().EquipMacWeaponOnLoad(gameInventory[k].GetComponent<MacWeapons>());
                            }
                        }
                        if (party[2] != null)
                        {
                            //    if (gameInventory[k].itemName == gameData.fieldWeaponEquip)
                            {
                                //        playableCharacters[2].GetComponent<Field>().EquipFieldWeaponOnLoad(gameInventory[k].GetComponent<FieldWeapons>());
                            }
                        }
                        if (party[3] != null)
                        {
                            //   if (gameInventory[k].itemName == gameData.riggsWeaponEquip)
                            {
                                //       playableCharacters[3].GetComponent<Riggs>().EquipRiggsWeaponOnLoad(gameInventory[k].GetComponent<RiggsWeapons>());
                            }
                        }

                        // Chest Armor Equip
                        if (gameInventory[k].itemName == gameData.charChestArmorEquip[0])
                        {
                            playableCharacters[0].GetComponent<Grieve>().EquipGrieveChestArmorOnLoad(gameInventory[k].GetComponent<ChestArmor>());
                        }
                        if (party[1] != null)
                        {
                            if (gameInventory[k].itemName == gameData.charChestArmorEquip[1])
                            {
                                playableCharacters[1].GetComponent<Mac>().EquipMacChestArmorOnLoad(gameInventory[k].GetComponent<ChestArmor>());
                            }
                        }
                        if (party[2] != null)
                        {
                            if (gameInventory[k].itemName == gameData.charChestArmorEquip[2])
                            {
                                playableCharacters[2].GetComponent<Field>().EquipFieldChestArmorOnLoad(gameInventory[k].GetComponent<ChestArmor>());
                            }
                        }
                        if (party[3] != null)
                        {
                            if (gameInventory[k].itemName == gameData.charChestArmorEquip[3])
                            {
                                playableCharacters[3].GetComponent<Riggs>().EquipRiggsChestArmorOnLoad(gameInventory[k].GetComponent<ChestArmor>());
                            }
                        }

                        // Accessory Equip
                        if (gameInventory[k].itemName == gameData.charAccessory1Equip[0])
                        {
                            playableCharacters[0].GetComponent<Grieve>().EquipGrieveAccessory1OnLoad(gameInventory[k].GetComponent<Accessory>());
                        }
                        if (gameInventory[k].itemName == gameData.charAccessory2Equip[0])
                        {
                            playableCharacters[0].GetComponent<Grieve>().EquipGrieveAccessory2OnLoad(gameInventory[k].GetComponent<Accessory>());
                        }

                        if (party[1] != null)
                        {
                            if (gameInventory[k].itemName == gameData.charAccessory1Equip[1])
                            {
                                playableCharacters[1].GetComponent<Mac>().EquipMacAccessory1OnLoad(gameInventory[k].GetComponent<Accessory>());
                            }
                            if (gameInventory[k].itemName == gameData.charAccessory2Equip[1])
                            {
                                playableCharacters[1].GetComponent<Mac>().EquipMacAccessory2OnLoad(gameInventory[k].GetComponent<Accessory>());
                            }
                        }
                        if (party[2] != null)
                        {
                            if (gameInventory[k].itemName == gameData.charAccessory1Equip[2])
                            {
                                playableCharacters[2].GetComponent<Field>().EquipFieldAccessory1OnLoad(gameInventory[k].GetComponent<Accessory>());
                            }
                            if (gameInventory[k].itemName == gameData.charAccessory2Equip[2])
                            {
                                playableCharacters[2].GetComponent<Field>().EquipFieldAccessory2OnLoad(gameInventory[k].GetComponent<Accessory>());
                            }
                        }
                        if (party[3] != null)
                        {
                            if (gameInventory[k].itemName == gameData.charAccessory1Equip[3])
                            {
                                playableCharacters[3].GetComponent<Riggs>().EquipRiggsAccessory1OnLoad(gameInventory[k].GetComponent<Accessory>());
                            }
                            if (gameInventory[k].itemName == gameData.charAccessory2Equip[3])
                            {
                                playableCharacters[3].GetComponent<Riggs>().EquipRiggsAccessory2OnLoad(gameInventory[k].GetComponent<Accessory>());
                            }
                        }
                    }
                }
            }
        }

        // Node Information

        gridReference.grievePosition = gameData.charNodePositions[0];
        gridReference.macPosition = gameData.charNodePositions[1];
        gridReference.fieldPosition = gameData.charNodePositions[2];
        gridReference.riggsPosition = gameData.charNodePositions[3];

        gridReference.ClearGrid(); // Consider removing and manually changing lines to gray by default

        for (int i = 0; i < gridReference.nodes.Length; i++)
        {

            if (gameData.grieveNodes[i] == true)
            {
                gridReference.nodes[i].grieveUnlocked = true;
            }
            if (gameData.macNodes[i] == true)
            {
                gridReference.nodes[i].macUnlocked = true;
            }
            if (gameData.fieldNodes[i] == true)
            {
                gridReference.nodes[i].fieldUnlocked = true;
            }
            if (gameData.riggsNodes[i] == true)
            {
                gridReference.nodes[i].riggsUnlocked = true;
            }
        }

        // Quests
        for (int i = 0; i < gameQuests.Length; i++)
        {
            if (gameData.partyQuests[i] != null)
            {
                if (gameData.partyQuests[i] == gameQuests[i].questName)
                {
                    adventureLogReference.AddQuestToAdventureLog(gameQuests[i]);

                    for (int k = 0; k < gameQuests[i].objectiveCount.Length; k++)
                    {
                        gameQuests[i].objectiveCount[k] = gameData.partyQuestObjectiveCount[i, k];
                    }

                    gameQuests[i].turnedIn = gameData.questTurnedIn[i];
                }
            }

            if (gameData.completedQuests[i] != null)
            {
                if (gameData.completedQuests[i] == gameQuests[i].questName)
                {
                    adventureLogReference.AddQuestToCompleteQuestLog(gameQuests[i]);

                }
            }
        }


        // World Position
        Vector3 partyPosition;
        partyPosition.x = gameData.partyPosition[0];
        partyPosition.y = gameData.partyPosition[1];
        partyPosition.z = gameData.partyPosition[2];

        if (gameData.time <= 50)
        {
            timeOfDay = 51;
        }
        else
        {
            timeOfDay = gameData.time;
        }
        partyMoney = gameData.partyMoney;
        activeParty.gameObject.GetComponent<SpriteRenderer>().sprite = activeParty.activeParty[0].gameObject.GetComponent<SpriteRenderer>().sprite;
        activeParty.gameObject.transform.position = partyPosition;
        activePartyMember2.gameObject.transform.position = partyPosition;
        inWorldMap = gameData.inWorldMap;
        if (activeParty.activeParty[1] != null)
        {
            activePartyMember2.gameObject.GetComponent<SpriteRenderer>().sprite = activeParty.activeParty[1].gameObject.GetComponent<SpriteRenderer>().sprite;
            activePartyMember2.gameObject.SetActive(true);
            activePartyMember2.gameObject.transform.position = partyPosition;
        }

        if (activeParty.activeParty[2] != null)
        {
            activePartyMember3.gameObject.GetComponent<SpriteRenderer>().sprite = activeParty.activeParty[2].gameObject.GetComponent<SpriteRenderer>().sprite;
            activePartyMember3.gameObject.SetActive(true);
            activePartyMember3.gameObject.transform.position = partyPosition;
        }

        if (party[1] != null)
        {
            activePartyMember2.GetComponent<SpriteRenderer>().sprite = party[1].GetComponent<SpriteRenderer>().sprite;
            activePartyMember2.SetActive(true);
            charAbilityButtons[1].SetActive(true);
            //            charSkillTierButtons[1].SetActive(true);

        }
        if (party[2] != null)
        {
            ActivateArrangePartyButton();
            arrangePartyButtonActive = true;
            activePartyMember3.GetComponent<SpriteRenderer>().sprite = party[2].GetComponent<SpriteRenderer>().sprite;
            activePartyMember3.SetActive(true);
            activeParty.activeParty[2] = party[2].GetComponent<Character>().gameObject;
            charAbilityButtons[2].SetActive(true);
            activeParty.SetActivePartyIndexes();
            //        charSkillTierButtons[2].SetActive(true);

        }
        if (party[3] != null)
        {
            battleSystem.battleSwitchButtons = true;
            charAbilityButtons[3].SetActive(true);

        }

        aboveLayer = gameData.aboveLayer;
        activeParty.GetComponent<SpriteRenderer>().sortingLayerName = gameData.whichLayer;
        if (party[1] != null)
        {
            activePartyMember2.GetComponent<SpriteRenderer>().sortingLayerName = activeParty.GetComponent<SpriteRenderer>().sortingLayerName;
        }
        if (party[2] != null)
        {
            activePartyMember3.GetComponent<SpriteRenderer>().sortingLayerName = activeParty.GetComponent<SpriteRenderer>().sortingLayerName;
        }
        ActivatePauseMenuCharacterPanels();
        arrangePartyButtonActive = gameData.arrangePartyButtonActive;

        if (arrangePartyButtonActive == true)
        {
            ActivateArrangePartyButton();
        }
        currentScene = gameData.scene;

        SceneManager.LoadSceneAsync(gameData.scene, LoadSceneMode.Additive);

        battleModeActive = gameData.battleModeActive;
        Engine.e.canvasReference.GetComponent<PauseMenu>().partyLocationDisplay.text = gameData.scene;

        if (gameData.scene == "WorldMap")
        {
            if (Engine.e.mainVirtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize == 6.5f)
            {
                Engine.e.mainVirtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 10f;
            }

            Engine.e.activeParty.gameObject.transform.localScale = new Vector3(0.65f, 0.65f, 1f);
            Engine.e.activeParty.GetComponent<PlayerController>().speed = 3.5f;

            if (Engine.e.activeParty.activeParty[1] != null)
            {
                Engine.e.activePartyMember2.GetComponent<APFollow>().speed = 3.5f;
                Engine.e.activePartyMember2.GetComponent<APFollow>().distance = 1.0f;
                Engine.e.activePartyMember2.transform.localScale = new Vector3(0.65f, 0.65f, 1f);

            }
            if (Engine.e.activeParty.activeParty[2] != null)
            {
                Engine.e.activePartyMember3.GetComponent<APFollow>().speed = 3.5f;
                Engine.e.activePartyMember3.GetComponent<APFollow>().distance = 1.0f;
                Engine.e.activePartyMember3.transform.localScale = new Vector3(0.65f, 0.65f, 1f);

            }

            ableToSave = true;
        }
        else
        {
            if (Engine.e.mainVirtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize == 10f)
            {
                Engine.e.mainVirtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 6.5f;
            }

            Engine.e.activeParty.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1f);
            Engine.e.activeParty.GetComponent<PlayerController>().speed = 4.5f;
            if (Engine.e.activeParty.activeParty[1] != null)
            {
                Engine.e.activePartyMember2.GetComponent<APFollow>().speed = 4.5f;
                Engine.e.activePartyMember2.GetComponent<APFollow>().distance = 1.25f;
                Engine.e.activePartyMember2.transform.localScale = new Vector3(1.0f, 1.0f, 1f);

            }
            if (Engine.e.activeParty.activeParty[2] != null)
            {
                Engine.e.activePartyMember3.GetComponent<APFollow>().speed = 4.5f;
                Engine.e.activePartyMember3.GetComponent<APFollow>().distance = 1.25f;
                Engine.e.activePartyMember3.transform.localScale = new Vector3(1.0f, 1.0f, 1f);

            }
        }
        gameStart = true;
    }

    // Establishes a battle by communicating with the BattleSystem class.
    public void BeginBattle()
    {

        inBattle = true;
        battleMenu.battleMenuUI.SetActive(true);
        storeDialogueReference.gameObject.SetActive(false);
        StartCoroutine(battleSystem.SetupBattle());
        //battleMusic.Play();

    }



    // Clears the "Amount Held" value of any item that is stackable. Used mainly for NewGame().
    void ClearGameInventoryHeld()
    {
        for (int i = 0; i < gameInventory.Count; i++)
        {
            if (gameInventory[i] != null)
            {
                gameInventory[i].numberHeld = 0;
            }
        }

        partyInventoryReference.grieveWeaponTotal = 0;
        partyInventoryReference.macWeaponTotal = 0;
        partyInventoryReference.fieldWeaponTotal = 0;
        partyInventoryReference.riggsWeaponTotal = 0;
        partyInventoryReference.chestArmorTotal = 0;

    }
    void SetDropIndexes()
    {
        battleSystem.lastDropChoice = null;

        for (int i = 0; i < gameDrops.Length; i++)
        {
            if (gameDrops[i] != null)
            {
                gameDrops[i].dropIndex = i;
                gameDrops[i].isKnown = false;
            }
        }
    }

    void SetSkillIndexes()
    {
        battleSystem.lastSkillChoice = null;

        for (int i = 0; i < gameSkills.Length; i++)
        {
            if (gameSkills[i] != null)
            {
                gameSkills[i].skillIndex = i;
                gameSkills[i].isKnown = false;
            }
        }
    }

    void ClearGameQuests()
    {
        for (int i = 0; i < gameQuests.Length; i++)
        {
            if (gameQuests[i] != null)
            {
                gameQuests[i].ClearQuest();
            }
        }
    }

    public void ActivatePauseMenuCharacterPanels()
    {
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i] != null)
            {
                pauseMenuCharacterPanels[i].SetActive(true);
                itemMenuPanels[i].SetActive(true);

            }
        }
    }

    public void ActivateArrangePartyButton()
    {
        activateArrangePartyButton.SetActive(true);
    }



    public void ConfirmSell()
    {
        shopSellButtons[0].SetActive(false);
        shopSellButtons[1].SetActive(false);

    }

    public void DenySell()
    {
        storeDialogueReference.text = string.Empty;
        storeDialogueReference.text = "Anything else?";

        shopSellButtons[0].SetActive(false);
        shopSellButtons[1].SetActive(false);

        currentMerchant.OpenInventoryMenu();

    }
    public void ItemDisplayCharacterStats(int index)
    {
        if (index == 0)
        {
            DisplayGrieveInventoryStats();
        }
        if (index == 1)
        {
            DisplayMacInventoryStats();
        }
        if (index == 2)
        {
            DisplayFieldInventoryStats();
        }
        if (index == 3)
        {
            DisplayRiggsInventoryStats();
        }
    }

    public void DisplayGrieveInventoryStats()
    {
        inventoryMenuPartyNameStatsReference[0].text = string.Empty;
        inventoryMenuPartyNameStatsReference[0].text += party[0].GetComponent<Character>().characterName;

        inventoryMenuPartyHPStatsReference[0].text = string.Empty;
        inventoryMenuPartyHPStatsReference[0].text += "HP: " + party[0].GetComponent<Character>().currentHealth + " / " + party[0].GetComponent<Character>().maxHealth;

        inventoryMenuPartyMPStatsReference[0].text = string.Empty;
        inventoryMenuPartyMPStatsReference[0].text += "MP: " + party[0].GetComponent<Character>().currentMana + " / " + party[0].GetComponent<Character>().maxMana;

        inventoryMenuPartyENRStatsReference[0].text = string.Empty;
        inventoryMenuPartyENRStatsReference[0].text += "ENR: " + party[0].GetComponent<Character>().currentEnergy + " / " + party[0].GetComponent<Character>().maxEnergy;
    }


    public void DisplayMacInventoryStats()
    {
        if (party[1] != null)
        {
            inventoryMenuPartyNameStatsReference[1].text = string.Empty;
            inventoryMenuPartyNameStatsReference[1].text += party[1].GetComponent<Character>().characterName;

            inventoryMenuPartyHPStatsReference[1].text = string.Empty;
            inventoryMenuPartyHPStatsReference[1].text += "HP: " + party[1].GetComponent<Character>().currentHealth + " / " + party[1].GetComponent<Character>().maxHealth;

            inventoryMenuPartyMPStatsReference[1].text = string.Empty;
            inventoryMenuPartyMPStatsReference[1].text += "MP: " + party[1].GetComponent<Character>().currentMana + " / " + party[1].GetComponent<Character>().maxMana;

            inventoryMenuPartyENRStatsReference[1].text = string.Empty;
            inventoryMenuPartyENRStatsReference[1].text += "ENR: " + party[1].GetComponent<Character>().currentEnergy + " / " + party[1].GetComponent<Character>().maxEnergy;
        }
    }

    public void DisplayFieldInventoryStats()
    {
        if (party[2] != null)
        {
            inventoryMenuPartyNameStatsReference[2].text = string.Empty;
            inventoryMenuPartyNameStatsReference[2].text += party[2].GetComponent<Character>().characterName;

            inventoryMenuPartyHPStatsReference[2].text = string.Empty;
            inventoryMenuPartyHPStatsReference[2].text += "HP: " + party[2].GetComponent<Character>().currentHealth + " / " + party[2].GetComponent<Character>().maxHealth;

            inventoryMenuPartyMPStatsReference[2].text = string.Empty;
            inventoryMenuPartyMPStatsReference[2].text += "MP: " + party[2].GetComponent<Character>().currentMana + " / " + party[2].GetComponent<Character>().maxMana;

            inventoryMenuPartyENRStatsReference[2].text = string.Empty;
            inventoryMenuPartyENRStatsReference[2].text += "ENR: " + party[2].GetComponent<Character>().currentEnergy + " / " + party[2].GetComponent<Character>().maxEnergy;
        }
    }

    public void DisplayRiggsInventoryStats()
    {
        if (party[3] != null)
        {
            inventoryMenuPartyNameStatsReference[3].text = string.Empty;
            inventoryMenuPartyNameStatsReference[3].text += party[3].GetComponent<Character>().characterName;

            inventoryMenuPartyHPStatsReference[3].text = string.Empty;
            inventoryMenuPartyHPStatsReference[3].text += "HP: " + party[3].GetComponent<Character>().currentHealth + " / " + party[3].GetComponent<Character>().maxHealth;

            inventoryMenuPartyMPStatsReference[3].text = string.Empty;
            inventoryMenuPartyMPStatsReference[3].text += "MP: " + party[3].GetComponent<Character>().currentMana + " / " + party[3].GetComponent<Character>().maxMana;

            inventoryMenuPartyENRStatsReference[3].text = string.Empty;
            inventoryMenuPartyENRStatsReference[3].text += "ENR: " + party[3].GetComponent<Character>().currentEnergy + " / " + party[3].GetComponent<Character>().maxEnergy;
        }
    }

    public int GetRandomRemainingPartyMember()
    {
        remainingPartyMembers = new List<int>();

        for (int i = 0; i < activeParty.activeParty.Length; i++)
        {
            if (activeParty.activeParty[i] != null)
            {
                if (activeParty.activeParty[i].GetComponent<Character>().currentHealth > 0)
                {
                    remainingPartyMembers.Add(i);
                }
            }
        }

        randomIndex = new System.Random();
        randomPartyMemberIndex = randomIndex.Next(remainingPartyMembers.Count);
        return remainingPartyMembers[randomPartyMemberIndex];
    }

    public void SetInventoryArrayPositions()
    {

        for (int i = 0; i < partyInventoryReference.itemInventorySlots.Length; i++)
        {
            partyInventoryReference.itemInventorySlots[i].index = i;
        }

        for (int i = 0; i < partyInventoryReference.weaponInventorySlots.Length; i++)
        {
            partyInventoryReference.weaponInventorySlots[i].index = i;
        }

        for (int i = 0; i < adventureLogReference.questSlots.Length; i++)
        {
            adventureLogReference.questSlots[i].index = i;
        }
    }

    public void ResetCharClasses()
    {
        for (int i = 0; i < playableCharacters.Length; i++)
        {
            playableCharacters[i].currentClass = string.Empty;

            for (int k = 0; k < charClasses.Length; k++)
            {
                Debug.Log(i + " " + k);
                playableCharacters[i].characterClass[k] = false;
                playableCharacters[i].classEXP[k] = 0f;
            }
        }
    }
    void ClearHelpText()
    {
        helpText.text = string.Empty;
    }

    // Displays the correct "back button" of various menu related screens.
    // How selling was intrigrated, this is sometimes necessary for UI navigation. 
    public void AppropriateBackButton()
    {
        if (selling)
        {
            selling = false;

            inventoryMenuReference.SetActive(false);
            currentMerchant.OpenDialogueMenu();
        }
        else
        {

            canvasReference.GetComponent<PauseMenu>().OpenPauseMenu();
            canvasReference.GetComponent<PauseMenu>().DisplayGrievePartyText();
            canvasReference.GetComponent<PauseMenu>().DisplayMacPartyText();
            canvasReference.GetComponent<PauseMenu>().DisplayFieldPartyText();
            canvasReference.GetComponent<PauseMenu>().DisplayRiggsPartyText();


        }
    }

    // Calls Update() every frame.
    void Update()
    {

        if (!inBattle)
        {
            mainCamera.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = 0;
        }
        else
        {
            mainCamera.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = 2;
        }

        if (gameStart && !pauseMenuReference.isPaused && !inTown)
        {
            if (timeOfDay > 650 || timeOfDay < 400)
            {
                //timeOfDay += Time.deltaTime / 2;
                timeOfDay += Time.deltaTime * 2;

            }
            else
            {
                timeOfDay += Time.deltaTime;
            }
        }

        if (autoSaveReady && ableToSave && !inBattle && !recentAutoSave)
        {
            //  autoSaveReady = false;
            //  recentAutoSave = true;
            //  SaveGame(3);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            adventureLogReference.AddQuestToAdventureLog(gameQuests[0]);

            adventureLogReference.AddQuestToAdventureLog(gameQuests[1]);
            //GameManager.gameManager.activeParty.activeParty[0].GetComponent<Grieve>().weapon.GetComponent<GrieveWeapons>().fireAttack += 10;
            partyInventoryReference.AddItemToInventory(gameInventory[0]);
            partyInventoryReference.AddItemToInventory(gameInventory[1]);
            partyInventoryReference.AddItemToInventory(gameInventory[3]);
            partyInventoryReference.AddItemToInventory(gameInventory[28]);
            partyInventoryReference.AddItemToInventory(gameInventory[17]);
            partyInventoryReference.AddItemToInventory(gameInventory[17]);
            partyInventoryReference.AddItemToInventory(gameInventory[17]);
            partyInventoryReference.AddItemToInventory(gameInventory[17]);
            partyInventoryReference.AddItemToInventory(gameInventory[29]);
            partyInventoryReference.AddItemToInventory(gameInventory[33]);
            partyInventoryReference.AddItemToInventory(gameInventory[34]);
            partyInventoryReference.AddItemToInventory(gameInventory[35]);

            // partyInventoryReference.AddItemToInventory(gameInventory[2]);
            //partyInventoryReference.AddItemToInventory(gameInventory[3]);
            //partyInventoryReference.AddItemToInventory(gameGrieveWeapons[1].GetComponent<GrieveWeapons>());
            partyInventoryReference.AddItemToInventory(gameWeapons[9].GetComponent<Weapon>());
            partyInventoryReference.AddItemToInventory(gameChestArmor[2].GetComponent<ChestArmor>());
            partyInventoryReference.AddItemToInventory(gameShadowDrops[2]);

            //partyInventoryReference.AddMacWeaponToInventory(macGameWeapons[2].GetComponent<MacWeapons>());
            //partyInventoryReference.AddFieldWeaponToInventory(fieldGameWeapons[2].GetComponent<FieldWeapons>());

            //partyInventoryReference.AddChestArmorToInventory(gameArmor[0].GetComponent<ChestArmor>());
            // partyInventoryReference.AddChestArmorToInventory(gameArmor[2].GetComponent<ChestArmor>());

            //partyInventoryReference.AddItemToInventory(gameFireDrops[0]);

        }

        if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown("joystick button 2"))
        {
            //SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SaveGame(0);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {

        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!inBattle)
            {
                if (miniMap.activeInHierarchy)
                {
                    miniMap.SetActive(false);
                }
                else
                {
                    miniMap.SetActive(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            AddCharacterToParty("Mac");
            AddCharacterToParty("Field");
            AddCharacterToParty("Riggs");

            activeParty.SetActiveParty();
            // gridReference.tier2Path.SetActive(true);
        }

        if (inWorldMap)
        {
            if (currentScene != "WorldMap")
            {
                currentScene = "WorldMap";
            }
        }
    }
    void FixedUpdate()
    {

        // Debug.Log(timer);

        // timer -= Time.deltaTime;
    }
}



