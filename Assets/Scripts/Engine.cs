using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using UnityEngine.EventSystems;
public class Engine : MonoBehaviour
{

    // General Info
    public GameObject[] party;
    public ActiveParty activeParty;
    public Character[] playableCharacters;
    public Item[] charEquippedWeapons, charEquippedChestArmor;
    public int partyMoney;
    public float timeOfDay;
    public string currentScene;
    public bool inBattle = false;
    public bool indoors = false;
    public bool inWorldMap = false;
    public bool inRange = false;
    public bool battleModeActive = true;

    // Stat Curves
    [SerializeField]
    AnimationCurve healthCurve, manaCurve, energyCurve, strengthCurve;

    // Drops
    public Drops[] fireDrops, iceDrops, lightningDrops, waterDrops, shadowDrops, holyDrops;

    // Skills    
    public Skills[] gameSkills;


    // Inventory / Weapons / Armor / Drops
    // "Game Inventory" is the most important. The other lists are mainly for visual reference.
    public List<Item> gameInventory;
    [SerializeField]
    List<Item> gameGrieveWeapons, gameMacWeapons, gameFieldWeapons, gameRiggsWeapons, gameChestArmor, gameFireDrops, gameIceDrops,
    gameLightningDrops, gameWaterDrops, gameShadowDrops, gameHolyDrops;


    // Misc References
    public int characterBeingTargeted, charBeingTargeted;
    public Item itemToBeUsed;
    public List<int> remainingPartyMembers;
    bool gameStart;
    public static Engine e;
    public bool ableToSave, arrangePartyButtonActive, char1LevelUp, char2LevelUp, char3LevelUp;
    public BattleSystem battleSystem;
    int nextRemainingCharacterIndex, randomPartyMemberIndex;
    public CinemachineVirtualCamera mainCamera;
    public GameObject activateArrangePartyButton,
    activePartyMember2, activePartyMember3, itemMenuCharFirst, dropMenuCharFirst;
    public TextMeshProUGUI[] char1LevelUpPanelReference, char2LevelUpPanelReference, char3LevelUpPanelReference;
    public TextMeshProUGUI enemyLootReferenceG, enemyLootReferenceExp, battleHelp;
    Vector3 startingPos;
    public System.Random randomIndex;

    // Shopping
    public bool selling = false;
    public TextMeshProUGUI storeDialogueReference, helpText;
    public GameObject[] shopSellButtons;
    public Merchant currentMerchant;
    public TavernBarkeeperDialogue currentTavern;
    public GameObject[] itemConfirmUseButtons, itemDropConfirmUseButtons;
    public GameObject interactionPopup;
    public bool loadTimer = false;
    public GameObject deathTimerPopup;
    public GameObject weatherRain;

    // Menu References
    public PartyInventory partyInventoryReference;
    public EquipDisplay equipMenuReference;
    public BattleMenu battleMenu;
    public AbilitiesDisplay abilityScreenReference;
    public GameObject mainMenu, battleSystemMenu;
    public GameObject inventoryMenuReference;
    public GameObject[] pauseMenuCharacterPanels, itemMenuPanels;
    public GameObject canvasReference;
    public GameObject[] charAbilityButtons, charSkillTierButtons;
    [SerializeField]
    TextMeshProUGUI[] inventoryMenuPartyNameStatsReference, inventoryMenuPartyHPStatsReference, inventoryMenuPartyMPStatsReference, inventoryMenuPartyENRStatsReference;

    //public float rainTimer = 0f, rainChance, rainOff;
    //public bool startTimer = true, stopTimer = false, weatherRainOn = false;
    // Music
    //public AudioSource battleMusic;

    void Awake()
    {
        if (!gameStart)
        {
            e = this;
            gameStart = true;
        }
    }

    // Establishes a New Game. Clears multiple variables to their default states for a fresh start.
    public void NewGame()
    {
        ClearGameInventoryHeld();
        SetParty();

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        activeParty.SetActiveParty();
        //SceneManager.LoadSceneAsync("OpeningScene", LoadSceneMode.Additive);
        //SceneManager.LoadSceneAsync("MariaWest", LoadSceneMode.Additive);
        loadTimer = true;
        SceneManager.LoadSceneAsync("GrieveNameInput", LoadSceneMode.Additive);


        activeParty.SetLeaderSprite();
        timeOfDay = 0f;
        arrangePartyButtonActive = false;
        mainCamera.GetComponent<CinemachineVirtualCamera>().Priority = 10;
        partyMoney = 100;
        activeParty.gameObject.transform.position = startingPos;
        inWorldMap = false;
        battleModeActive = true;

    }

    // Sets default values for various game components relating to the playable characters.       
    void SetParty()
    {
        party = new GameObject[playableCharacters.Length];

        partyInventoryReference.partyInventory = new Item[partyInventoryReference.itemInventorySlots.Length];
        partyInventoryReference.grieveWeapons = new GrieveWeapons[partyInventoryReference.grieveWeaponInventorySlots.Length];
        partyInventoryReference.macWeapons = new MacWeapons[partyInventoryReference.macWeaponInventorySlots.Length];
        partyInventoryReference.fieldWeapons = new FieldWeapons[partyInventoryReference.fieldWeaponInventorySlots.Length];
        partyInventoryReference.riggsWeapons = new RiggsWeapons[partyInventoryReference.riggsWeaponInventorySlots.Length];
        partyInventoryReference.chestArmor = new ChestArmor[partyInventoryReference.chestArmorInventorySlots.Length];

        SetInventoryArrayPositions();

        charEquippedWeapons = new Item[playableCharacters.Length];
        charEquippedChestArmor = new Item[playableCharacters.Length];

        partyMoney = 0;

        //Beginning Weapons
        playableCharacters[0].GetComponent<Grieve>().EquipGrieveWeaponOnLoad(gameGrieveWeapons[0].GetComponent<GrieveWeapons>());
        playableCharacters[1].GetComponent<Mac>().EquipMacWeaponOnLoad(gameMacWeapons[0].GetComponent<MacWeapons>());
        playableCharacters[2].GetComponent<Field>().EquipFieldWeaponOnLoad(gameFieldWeapons[0].GetComponent<FieldWeapons>());
        playableCharacters[3].GetComponent<Riggs>().EquipRiggsWeaponOnLoad(gameRiggsWeapons[0].GetComponent<RiggsWeapons>());
        //partyInventoryReference.AddItemToInventory(grieveGameWeapons[0].GetComponent<GrieveWeapons>());
        //partyInventoryReference.AddItemToInventory(macGameWeapons[0].GetComponent<MacWeapons>()); // Depends If Mac Is In Party From Start

        //Beginning Armor
        playableCharacters[0].GetComponent<Grieve>().EquipGrieveChestArmorOnLoad(gameChestArmor[0].GetComponent<ChestArmor>());
        playableCharacters[1].GetComponent<Mac>().EquipMacChestArmorOnLoad(gameChestArmor[0].GetComponent<ChestArmor>());
        playableCharacters[2].GetComponent<Field>().EquipFieldChestArmorOnLoad(gameChestArmor[0].GetComponent<ChestArmor>());
        playableCharacters[3].GetComponent<Riggs>().EquipRiggsChestArmorOnLoad(gameChestArmor[1].GetComponent<ChestArmor>());
        //partyInventoryReference.AddItemToInventory(gameArmor[0].GetComponent<ChestArmor>());
        //partyInventoryReference.AddItemToInventory(gameArmor[0].GetComponent<ChestArmor>()); // Depends If Mac Is In Party From Start


        // Grieve
        playableCharacters[0].characterName = "Grieve";
        playableCharacters[0].lvl = 1;
        playableCharacters[0].healthOffset = 0f;
        playableCharacters[0].manaOffset = -30f;
        playableCharacters[0].energyOffset = 0f;
        playableCharacters[0].strengthOffset = 0f;
        playableCharacters[0].maxHealth = Mathf.Round(healthCurve.Evaluate(playableCharacters[0].lvl) + (healthCurve.Evaluate(playableCharacters[0].lvl) * (playableCharacters[0].healthOffset / 100)));
        playableCharacters[0].currentHealth = playableCharacters[0].maxHealth;
        playableCharacters[0].maxMana = Mathf.Round(manaCurve.Evaluate(playableCharacters[0].lvl) + (manaCurve.Evaluate(playableCharacters[0].lvl) * (playableCharacters[0].manaOffset / 100)));
        playableCharacters[0].currentMana = playableCharacters[0].maxMana;
        playableCharacters[0].maxEnergy = Mathf.Round(energyCurve.Evaluate(playableCharacters[0].lvl) + (energyCurve.Evaluate(playableCharacters[0].lvl) * (playableCharacters[0].energyOffset / 100)));
        playableCharacters[0].currentEnergy = playableCharacters[0].maxEnergy;
        playableCharacters[0].haste = 17;
        playableCharacters[0].baseDamage = 10;
        playableCharacters[0].physicalDamage = Mathf.Round(strengthCurve.Evaluate(playableCharacters[0].lvl) + +(strengthCurve.Evaluate(playableCharacters[0].lvl) * (playableCharacters[0].strengthOffset / 100)));
        playableCharacters[0].experiencePoints = 0;
        playableCharacters[0].levelUpReq = 100;
        playableCharacters[0].isInParty = true;
        playableCharacters[0].isInActiveParty = true;
        playableCharacters[0].isLeader = true;
        playableCharacters[0].canUseMagic = false;
        playableCharacters[0].dodgeChance = 10f;
        playableCharacters[0].dropCostReduction = 0f;
        playableCharacters[0].skillCostReduction = 0f;
        playableCharacters[0].healthCapped = true;
        playableCharacters[0].manaCapped = true;
        playableCharacters[0].energyCapped = true;

        playableCharacters[0].physicalDefense = 2;
        playableCharacters[0].physicalDefense += gameChestArmor[0].GetComponent<ChestArmor>().physicalArmor;
        playableCharacters[0].fireDefense = 1f;
        playableCharacters[0].waterDefense = 1f;
        playableCharacters[0].lightningDefense = 3f;
        playableCharacters[0].shadowDefense = 3f;
        playableCharacters[0].iceDefense = 1f;
        playableCharacters[0].poisonDefense = 1f;
        playableCharacters[0].sleepDefense = 1f;
        playableCharacters[0].confuseDefense = 1f;



        playableCharacters[0].canUseFireDrops = true;
        playableCharacters[0].canUseHolyDrops = true;
        playableCharacters[0].canUseWaterDrops = true;
        playableCharacters[0].canUseLightningDrops = true;
        playableCharacters[0].canUseShadowDrops = true;
        playableCharacters[0].canUseIceDrops = true;



        playableCharacters[0].fireDrops = new Drops[10];
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

        fireDrops[0].isKnown = true;
        fireDrops[1].isKnown = true;
        holyDrops[0].isKnown = true;
        holyDrops[1].isKnown = true;
        holyDrops[2].isKnown = true;
        holyDrops[3].isKnown = true;
        waterDrops[0].isKnown = true;
        lightningDrops[0].isKnown = true;
        shadowDrops[0].isKnown = true;
        shadowDrops[1].isKnown = true;
        shadowDrops[2].isKnown = true;
        shadowDrops[3].isKnown = true;
        shadowDrops[4].isKnown = true;
        iceDrops[0].isKnown = true;

        playableCharacters[0].fireDropsLevel = 1f;
        playableCharacters[0].holyDropsLevel = 2f;
        playableCharacters[0].waterDropsLevel = 1f;
        playableCharacters[0].lightningDropsLevel = 1f;
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

        playableCharacters[0].stealChance = 60f;

        playableCharacters[0].isPoisoned = false;
        playableCharacters[0].isAsleep = false;
        playableCharacters[0].isConfused = false;
        playableCharacters[0].deathInflicted = false;
        playableCharacters[0].inflicted = false;


        playableCharacters[0].poisonDmg = 0f;
        playableCharacters[0].sleepTimer = 0;
        playableCharacters[0].confuseTimer = 0;
        playableCharacters[0].deathTimer = 3;


        playableCharacters[0].GetComponent<SpriteRenderer>().color = Color.white;



        // Mac
        playableCharacters[1].characterName = "Mac";
        playableCharacters[1].lvl = 1;
        playableCharacters[1].healthOffset = -20f;
        playableCharacters[1].manaOffset = 20f;
        playableCharacters[1].energyOffset = 0f;
        playableCharacters[1].strengthOffset = -20f;
        playableCharacters[1].maxHealth = Mathf.Round(healthCurve.Evaluate(playableCharacters[1].lvl) + (healthCurve.Evaluate(playableCharacters[1].lvl) * (playableCharacters[1].healthOffset / 100)));
        playableCharacters[1].currentHealth = playableCharacters[1].maxHealth;
        playableCharacters[1].maxMana = Mathf.Round(manaCurve.Evaluate(playableCharacters[1].lvl) + (manaCurve.Evaluate(playableCharacters[1].lvl) * (playableCharacters[1].manaOffset / 100)));
        playableCharacters[1].currentMana = playableCharacters[1].maxMana;
        playableCharacters[1].maxEnergy = Mathf.Round(energyCurve.Evaluate(playableCharacters[1].lvl) + (energyCurve.Evaluate(playableCharacters[1].lvl) * (playableCharacters[1].energyOffset / 100)));
        playableCharacters[1].currentEnergy = playableCharacters[1].maxEnergy;
        playableCharacters[1].haste = 14;
        playableCharacters[1].baseDamage = 10;
        playableCharacters[1].physicalDamage = Mathf.Round(strengthCurve.Evaluate(playableCharacters[1].lvl) + +(strengthCurve.Evaluate(playableCharacters[1].lvl) * (playableCharacters[1].strengthOffset / 100)));
        playableCharacters[1].experiencePoints = 0;
        playableCharacters[1].levelUpReq = 100;
        playableCharacters[1].isInParty = true;
        playableCharacters[1].isInActiveParty = true;
        playableCharacters[1].isLeader = false;
        playableCharacters[1].canUseMagic = true;
        playableCharacters[1].dropCostReduction = 0f;
        playableCharacters[1].skillCostReduction = 0f;
        playableCharacters[1].healthCapped = true;
        playableCharacters[1].manaCapped = true;
        playableCharacters[1].energyCapped = true;

        playableCharacters[1].dodgeChance = 10f;

        playableCharacters[1].physicalDefense = 1;
        playableCharacters[1].physicalDefense += gameChestArmor[0].GetComponent<ChestArmor>().physicalArmor;
        playableCharacters[1].fireDefense = 2f;
        playableCharacters[1].waterDefense = 2f;
        playableCharacters[1].lightningDefense = 2f;
        playableCharacters[1].shadowDefense = 1f;
        playableCharacters[1].iceDefense = 1f;
        playableCharacters[1].poisonDefense = 1f;
        playableCharacters[1].sleepDefense = 1f;
        playableCharacters[1].confuseDefense = 1f;



        playableCharacters[1].canUseFireDrops = false;
        playableCharacters[1].canUseHolyDrops = false;
        playableCharacters[1].canUseWaterDrops = true;
        playableCharacters[1].canUseLightningDrops = true;
        playableCharacters[1].canUseShadowDrops = false;
        playableCharacters[1].canUseIceDrops = false;




        playableCharacters[1].fireDrops = new Drops[10];
        playableCharacters[1].holyDrops = new Drops[10];
        playableCharacters[1].waterDrops = new Drops[10];
        playableCharacters[1].lightningDrops = new Drops[10];
        playableCharacters[1].shadowDrops = new Drops[10];
        playableCharacters[1].iceDrops = new Drops[10];




        playableCharacters[1].waterDrops[0] = waterDrops[0];
        playableCharacters[1].lightningDrops[0] = lightningDrops[0];

        playableCharacters[1].fireDropsLevel = 1f;
        playableCharacters[1].holyDropsLevel = 1f;
        playableCharacters[1].waterDropsLevel = 2f;
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

        playableCharacters[1].stealChance = 60f;

        playableCharacters[1].poisonDmg = 0f;
        playableCharacters[1].sleepTimer = 0;
        playableCharacters[1].confuseTimer = 0;
        playableCharacters[1].deathTimer = 3;

        playableCharacters[1].GetComponent<SpriteRenderer>().color = Color.white;


        //Field
        playableCharacters[2].characterName = "Field";
        playableCharacters[2].lvl = 3;
        playableCharacters[2].healthOffset = -20f;
        playableCharacters[2].manaOffset = 0f;
        playableCharacters[2].energyOffset = 20f;
        playableCharacters[2].strengthOffset = -10f;
        playableCharacters[2].maxHealth = 80f;
        playableCharacters[2].currentHealth = playableCharacters[2].maxHealth;
        playableCharacters[2].maxMana = 20f;
        playableCharacters[2].currentMana = playableCharacters[2].maxMana;
        playableCharacters[2].maxEnergy = 50f;
        playableCharacters[2].currentEnergy = playableCharacters[2].maxEnergy;
        playableCharacters[2].haste = 19;
        playableCharacters[2].baseDamage = 10;
        playableCharacters[2].physicalDamage = 10;
        playableCharacters[2].experiencePoints = 0;
        playableCharacters[2].levelUpReq = 100;
        playableCharacters[2].isInParty = true;
        playableCharacters[2].isInActiveParty = true;
        playableCharacters[2].isLeader = false;
        playableCharacters[2].canUseMagic = true;
        playableCharacters[2].dodgeChance = 15f;
        playableCharacters[2].dropCostReduction = 0f;
        playableCharacters[2].skillCostReduction = 0f;
        playableCharacters[2].healthCapped = true;
        playableCharacters[2].manaCapped = true;
        playableCharacters[2].energyCapped = true;

        playableCharacters[2].physicalDefense = 1f;
        playableCharacters[2].physicalDefense += gameChestArmor[0].GetComponent<ChestArmor>().physicalArmor;
        playableCharacters[2].fireDefense = 1f;
        playableCharacters[2].waterDefense = 1f;
        playableCharacters[2].lightningDefense = 1f;
        playableCharacters[2].shadowDefense = 3f;
        playableCharacters[2].iceDefense = 1f;
        playableCharacters[2].poisonDefense = 2f;
        playableCharacters[2].sleepDefense = 1f;
        playableCharacters[2].confuseDefense = 1f;



        playableCharacters[2].canUseFireDrops = false;
        playableCharacters[2].canUseHolyDrops = false;
        playableCharacters[2].canUseWaterDrops = false;
        playableCharacters[2].canUseLightningDrops = false;
        playableCharacters[2].canUseShadowDrops = true;
        playableCharacters[2].canUseIceDrops = false;



        playableCharacters[2].fireDrops = new Drops[10];
        playableCharacters[2].holyDrops = new Drops[10];
        playableCharacters[2].waterDrops = new Drops[10];
        playableCharacters[2].lightningDrops = new Drops[10];
        playableCharacters[2].shadowDrops = new Drops[10];
        playableCharacters[2].iceDrops = new Drops[10];


        playableCharacters[2].shadowDrops[0] = shadowDrops[0];
        playableCharacters[2].shadowDrops[1] = shadowDrops[1];


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

        playableCharacters[2].stealChance = 75f;

        playableCharacters[2].poisonDmg = 0f;
        playableCharacters[2].sleepTimer = 0;
        playableCharacters[2].confuseTimer = 0;
        playableCharacters[2].deathTimer = 3;

        playableCharacters[2].GetComponent<SpriteRenderer>().color = Color.white;


        //Riggs
        playableCharacters[3].characterName = "Riggs";
        playableCharacters[3].lvl = 5;
        playableCharacters[3].healthOffset = 30f;
        playableCharacters[3].manaOffset = 0f;
        playableCharacters[3].energyOffset = -20f;
        playableCharacters[3].strengthOffset = 10f;
        playableCharacters[3].maxHealth = 200f;
        playableCharacters[3].currentHealth = playableCharacters[3].maxHealth;
        playableCharacters[3].maxMana = 50f;
        playableCharacters[3].currentMana = playableCharacters[3].maxMana;
        playableCharacters[3].maxEnergy = 20f;
        playableCharacters[3].haste = 15;
        playableCharacters[3].baseDamage = 20;
        playableCharacters[3].physicalDamage = 20;
        playableCharacters[3].experiencePoints = 0;
        playableCharacters[3].levelUpReq = 100;
        playableCharacters[3].isInParty = false;
        playableCharacters[3].isInActiveParty = false;
        playableCharacters[3].isLeader = false;
        playableCharacters[3].canUseMagic = true;
        playableCharacters[3].dodgeChance = 12f;
        playableCharacters[3].dropCostReduction = 0f;
        playableCharacters[3].skillCostReduction = 0f;
        playableCharacters[3].healthCapped = true;
        playableCharacters[3].manaCapped = true;
        playableCharacters[3].energyCapped = true;

        playableCharacters[3].physicalDefense = 5;
        playableCharacters[3].physicalDefense += gameChestArmor[1].GetComponent<ChestArmor>().physicalArmor;
        playableCharacters[3].fireDefense = 2f;
        playableCharacters[3].waterDefense = 2f;
        playableCharacters[3].lightningDefense = 2f;
        playableCharacters[3].shadowDefense = 2f;
        playableCharacters[3].iceDefense = 2f;
        playableCharacters[3].poisonDefense = 1f;
        playableCharacters[3].sleepDefense = 1f;
        playableCharacters[3].confuseDefense = 1f;


        playableCharacters[3].canUseFireDrops = false;
        playableCharacters[3].canUseHolyDrops = true;
        playableCharacters[3].canUseWaterDrops = false;
        playableCharacters[3].canUseLightningDrops = false;
        playableCharacters[3].canUseShadowDrops = true;
        playableCharacters[3].canUseIceDrops = false;



        playableCharacters[3].fireDrops = new Drops[10];
        playableCharacters[3].holyDrops = new Drops[10];
        playableCharacters[3].waterDrops = new Drops[10];
        playableCharacters[3].lightningDrops = new Drops[10];
        playableCharacters[3].shadowDrops = new Drops[10];
        playableCharacters[3].iceDrops = new Drops[10];



        playableCharacters[3].holyDrops[0] = holyDrops[0];
        playableCharacters[3].shadowDrops[0] = shadowDrops[0];



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

        playableCharacters[3].stealChance = 50f;

        playableCharacters[3].poisonDmg = 0f;
        playableCharacters[3].sleepTimer = 0;
        playableCharacters[3].confuseTimer = 0;
        playableCharacters[3].deathTimer = 3;

        playableCharacters[3].GetComponent<SpriteRenderer>().color = Color.white;


        party[0] = playableCharacters[0].GetComponent<Character>().gameObject;
        AddCharacterToParty("Mac");
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
                charAbilityButtons[1].SetActive(true);
                charSkillTierButtons[1].SetActive(true);

            }
            if (party[2] != null)
            {
                ActivateArrangePartyButton();
                playableCharacters[2].lvl = playableCharacters[0].lvl;
                playableCharacters[2].healthOffset = -20f;
                playableCharacters[2].manaOffset = 0f;
                playableCharacters[2].energyOffset = 20f;
                playableCharacters[2].maxHealth = Mathf.Round(healthCurve.Evaluate(playableCharacters[2].lvl) + (healthCurve.Evaluate(playableCharacters[2].lvl) * (playableCharacters[2].healthOffset / 100)));
                playableCharacters[2].currentHealth = playableCharacters[2].maxHealth;
                playableCharacters[2].maxMana = Mathf.Round(manaCurve.Evaluate(playableCharacters[2].lvl) + (manaCurve.Evaluate(playableCharacters[2].lvl) * (playableCharacters[2].manaOffset / 100)));
                playableCharacters[2].currentMana = playableCharacters[2].maxMana;
                playableCharacters[2].maxEnergy = Mathf.Round(energyCurve.Evaluate(playableCharacters[2].lvl) + (energyCurve.Evaluate(playableCharacters[2].lvl) * (playableCharacters[2].energyOffset / 100)));
                playableCharacters[2].currentEnergy = playableCharacters[2].maxEnergy;
                playableCharacters[2].physicalDamage = Mathf.Round(strengthCurve.Evaluate(playableCharacters[2].lvl) + +(strengthCurve.Evaluate(playableCharacters[2].lvl) * (playableCharacters[2].strengthOffset / 100)));
                arrangePartyButtonActive = true;
                activePartyMember3.GetComponent<SpriteRenderer>().sprite = party[2].GetComponent<SpriteRenderer>().sprite;
                activePartyMember3.SetActive(true);
                activeParty.activeParty[2] = party[2].GetComponent<Character>().gameObject;
                charAbilityButtons[2].SetActive(true);
                charSkillTierButtons[2].SetActive(true);

            }
            if (party[3] != null)
            {
                battleSystem.battleSwitchButtons = true;
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
                playableCharacters[3].physicalDamage = Mathf.Round(strengthCurve.Evaluate(playableCharacters[3].lvl) + +(strengthCurve.Evaluate(playableCharacters[3].lvl) * (playableCharacters[3].strengthOffset / 100)));
                charAbilityButtons[3].SetActive(true);
                charSkillTierButtons[3].SetActive(true);

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
                        float gainedStrength = Mathf.Round(newStrength - party[i].GetComponent<Character>().physicalDamage);

                        party[i].GetComponent<Character>().physicalDamage = newStrength;
                        party[i].GetComponent<Character>().physicalDefense = party[i].GetComponent<Character>().physicalDefense + 0.5f;

                        party[i].GetComponent<Character>().experiencePoints = newExperiencePointsTotal;
                        party[i].GetComponent<Character>().levelUpReq = Mathf.Round(party[i].GetComponent<Character>().levelUpReq * 6 / 2);

                        party[i].GetComponent<Character>().sleepDefense = 0.5f;
                        party[i].GetComponent<Character>().confuseDefense += 0.5f;



                        party[i].GetComponent<Character>().skillScale += 1f;

                        if (party[i].GetComponent<Character>().lvl <= 20)
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
                        }

                        if (party[i] == activeParty.activeParty[0])
                        {
                            char1LevelUp = true;
                            char1LevelUpPanelReference[0].text = activeParty.activeParty[0].GetComponent<Character>().characterName;
                            char1LevelUpPanelReference[1].text = "HP + " + gainedHealth.ToString();
                            char1LevelUpPanelReference[2].text = "MP + " + gainedMana.ToString();
                            char1LevelUpPanelReference[3].text = "ENR + " + gainedEnergy.ToString();
                            char1LevelUpPanelReference[4].text = "Physical Damage + " + gainedStrength.ToString();
                            char1LevelUpPanelReference[5].text = "Physical Defense + 0.5%";

                            if (activeParty.activeParty[0].GetComponent<Character>().lvl <= 20)
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
                        }

                        if (party[i] == activeParty.activeParty[1] && activeParty.activeParty[1] != null)
                        {
                            char2LevelUp = true;
                            char2LevelUpPanelReference[0].text = activeParty.activeParty[1].GetComponent<Character>().characterName;
                            char2LevelUpPanelReference[1].text = "HP + " + gainedHealth.ToString();
                            char2LevelUpPanelReference[2].text = "MP + " + gainedMana.ToString();
                            char2LevelUpPanelReference[3].text = "ENR + " + gainedEnergy.ToString();
                            char2LevelUpPanelReference[4].text = "Physical Damage + " + gainedStrength.ToString();
                            char2LevelUpPanelReference[5].text = "Physical Defense + 0.5%";

                            if (activeParty.activeParty[1].GetComponent<Character>().lvl <= 25)
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
                        }

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


    // Function for "consuming" an item, in or out of battle.
    // index refers to the target, not the item.
    public void UseItem(int index)
    {
        bool failedItemUse = false;

        if (inBattle)
        {
            if (battleSystem.currentInQueue == BattleState.CHAR1TURN)
            {
                itemToBeUsed = battleSystem.char1ItemToBeUsed;
            }
            if (battleSystem.currentInQueue == BattleState.CHAR2TURN)
            {
                itemToBeUsed = battleSystem.char2ItemToBeUsed;
            }
            if (battleSystem.currentInQueue == BattleState.CHAR3TURN)
            {
                itemToBeUsed = battleSystem.char3ItemToBeUsed;
            }

            if (itemToBeUsed.numberHeld == 0)
            {
                failedItemUse = true;
            }
        }

        switch (itemToBeUsed.itemName)
        {
            case "Health Potion":

                if (party[index].GetComponent<Character>().currentHealth == party[index].GetComponent<Character>().maxHealth || party[index].GetComponent<Character>().currentHealth == 0)
                {
                    if (inBattle)
                    {
                        failedItemUse = true;
                        partyInventoryReference.OpenInventoryMenu();
                        break;
                    }
                    else
                        break;
                }
                else
                {
                    if (!failedItemUse)
                    {
                        if (inBattle)
                        {
                            if (index == 0)
                            {
                                GameObject healthSprite = Instantiate(gameInventory[itemToBeUsed.itemIndex].GetComponent<Item>().anim, Engine.e.activeParty.transform.position, Quaternion.identity);
                                GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.activeParty.transform.position, Quaternion.identity);
                                dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = itemToBeUsed.itemPower.ToString();
                                dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);
                                Destroy(dmgPopup, 1f);
                                Destroy(healthSprite, 1f);
                            }
                            if (index == 1)
                            {
                                GameObject healthSprite = Instantiate(gameInventory[itemToBeUsed.itemIndex].GetComponent<Item>().anim, Engine.e.activePartyMember2.transform.position, Quaternion.identity);
                                GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.activePartyMember2.transform.position, Quaternion.identity);
                                dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = itemToBeUsed.itemPower.ToString();
                                dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);
                                Destroy(dmgPopup, 1f);
                                Destroy(healthSprite, 1f);

                            }
                            if (index == 2)
                            {
                                GameObject healthSprite = Instantiate(gameInventory[itemToBeUsed.itemIndex].GetComponent<Item>().anim, Engine.e.activePartyMember3.transform.position, Quaternion.identity);
                                GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.activePartyMember3.transform.position, Quaternion.identity);
                                dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = itemToBeUsed.itemPower.ToString();
                                dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);
                                Destroy(dmgPopup, 1f);
                                Destroy(healthSprite, 1f);
                            }
                        }

                        party[index].GetComponent<Character>().currentHealth += itemToBeUsed.itemPower;

                        if (party[index].GetComponent<Character>().currentHealth > party[index].GetComponent<Character>().maxHealth)
                        {
                            party[index].GetComponent<Character>().currentHealth = party[index].GetComponent<Character>().maxHealth;
                        }
                        ItemDisplayCharacterStats(index);
                        partyInventoryReference.SubtractItemFromInventory(itemToBeUsed);
                        break;

                    }
                    break;
                }

            case "Mana Potion":

                if (party[index].GetComponent<Character>().currentMana == party[index].GetComponent<Character>().maxMana)
                {
                    if (inBattle)
                    {
                        failedItemUse = true;
                        partyInventoryReference.OpenInventoryMenu();

                        break;

                    }
                    else
                        break;
                }

                if (!failedItemUse)
                {
                    if (inBattle)
                    {
                        if (index == 0)
                        {
                            GameObject manaSprite = Instantiate(gameInventory[itemToBeUsed.itemIndex].GetComponent<Item>().anim, Engine.e.activeParty.transform.position, Quaternion.identity);
                            GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.activeParty.transform.position, Quaternion.identity);
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = itemToBeUsed.itemPower.ToString();
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 54, 255, 255);
                            Destroy(dmgPopup, 1f);
                            Destroy(manaSprite, 1f);
                        }
                        if (index == 1)
                        {
                            GameObject manaSprite = Instantiate(gameInventory[itemToBeUsed.itemIndex].GetComponent<Item>().anim, Engine.e.activePartyMember2.transform.position, Quaternion.identity);
                            GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.activePartyMember2.transform.position, Quaternion.identity);
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = itemToBeUsed.itemPower.ToString();
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 54, 255, 255);
                            Destroy(dmgPopup, 1f);
                            Destroy(manaSprite, 1f);
                        }
                        if (index == 2)
                        {
                            GameObject manaSprite = Instantiate(gameInventory[itemToBeUsed.itemIndex].GetComponent<Item>().anim, Engine.e.activePartyMember3.transform.position, Quaternion.identity);
                            GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.activePartyMember3.transform.position, Quaternion.identity);
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = itemToBeUsed.itemPower.ToString();
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 54, 255, 255);
                            Destroy(dmgPopup, 1f);
                            Destroy(manaSprite, 1f);
                        }
                    }

                    party[index].GetComponent<Character>().currentMana += itemToBeUsed.itemPower;

                    if (party[index].GetComponent<Character>().currentMana > party[index].GetComponent<Character>().maxMana)
                    {
                        party[index].GetComponent<Character>().currentMana = party[index].GetComponent<Character>().maxMana;
                    }
                    ItemDisplayCharacterStats(index);
                    partyInventoryReference.SubtractItemFromInventory(itemToBeUsed);
                    break;

                }
                break;

            case "Ashes":

                if (party[index].GetComponent<Character>().currentHealth > 0)
                {

                    if (inBattle)
                    {
                        failedItemUse = true;
                        partyInventoryReference.OpenInventoryMenu();
                        break;
                    }
                    else
                        break;
                }

                if (!failedItemUse)
                {
                    party[index].GetComponent<Character>().currentHealth += itemToBeUsed.itemPower;

                    if (party[index].GetComponent<Character>().currentHealth > party[index].GetComponent<Character>().maxHealth)
                    {
                        party[index].GetComponent<Character>().currentHealth = party[index].GetComponent<Character>().maxHealth;
                    }
                    ItemDisplayCharacterStats(index);
                    partyInventoryReference.SubtractItemFromInventory(itemToBeUsed);
                }

                break;

            case "Antidote":

                if (!party[index].GetComponent<Character>().isPoisoned)
                {
                    if (inBattle)
                    {
                        failedItemUse = true;
                        partyInventoryReference.OpenInventoryMenu();

                        break;

                    }
                    else
                        break;
                }

                if (!failedItemUse)
                {
                    if (inBattle)
                    {
                        if (index == 0)
                        {
                            GameObject antidote = Instantiate(gameInventory[itemToBeUsed.itemIndex].GetComponent<Item>().anim, Engine.e.activeParty.transform.position, Quaternion.identity);
                            GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.activeParty.transform.position, Quaternion.identity);
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Cured";
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);
                            Destroy(dmgPopup, 1f);
                            Destroy(antidote, 1f);
                            Engine.e.activeParty.GetComponent<SpriteRenderer>().color = Color.white;

                        }
                        if (index == 1)
                        {
                            GameObject antidote = Instantiate(gameInventory[itemToBeUsed.itemIndex].GetComponent<Item>().anim, Engine.e.activePartyMember2.transform.position, Quaternion.identity);
                            GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.activePartyMember2.transform.position, Quaternion.identity);
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Cured";
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);
                            Destroy(dmgPopup, 1f);
                            Destroy(antidote, 1f);
                            Engine.e.activePartyMember2.GetComponent<SpriteRenderer>().color = Color.white;

                        }
                        if (index == 2)
                        {
                            GameObject antidote = Instantiate(gameInventory[itemToBeUsed.itemIndex].GetComponent<Item>().anim, Engine.e.activePartyMember3.transform.position, Quaternion.identity);
                            GameObject dmgPopup = Instantiate(Engine.e.battleSystem.damagePopup, Engine.e.activePartyMember3.transform.position, Quaternion.identity);
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Cured";
                            dmgPopup.transform.GetChild(0).GetComponent<TextMeshPro>().color = new Color32(0, 229, 69, 255);
                            Destroy(dmgPopup, 1f);
                            Destroy(antidote, 1f);
                            Engine.e.activePartyMember3.GetComponent<SpriteRenderer>().color = Color.white;

                        }
                    }
                    party[index].GetComponent<Character>().isPoisoned = false;
                    party[index].GetComponent<Character>().inflicted = false;

                    ItemDisplayCharacterStats(index);

                }

                break;

        }

        if (!inBattle)
        {
            ItemDismissCharacterUseButtons();
            partyInventoryReference.OpenInventoryMenu();
        }

        if (failedItemUse)
        {
            failedItemUse = false;
        }
        itemToBeUsed = null;

    }

    // Function for "consuming" a Drop item, out of battle. Teaches the targeted character the corresponding Drop.
    public void UseItemDrop()
    {
        int index = characterBeingTargeted;

        switch (itemToBeUsed.itemName)
        {
            // Fire Drops
            case "Fire Blast":

                if (party[index].GetComponent<Character>().fireDrops[0] != null)
                {

                    break;
                }
                else
                {
                    if (Engine.e.party[index].GetComponent<Character>().canUseFireDrops == false)
                    {
                        Engine.e.party[index].GetComponent<Character>().canUseFireDrops = true;
                    }

                    party[index].GetComponent<Character>().fireDrops[0] = fireDrops[0];

                    if (!fireDrops[0].isKnown)
                    {
                        fireDrops[0].isKnown = true;
                    }

                    partyInventoryReference.SubtractItemFromInventory(gameFireDrops[0]);
                    break;
                }

            // Ice Drops
            case "Blizzard":

                if (party[index].GetComponent<Character>().iceDrops[0] != null)
                {

                    break;
                }
                else
                {
                    if (Engine.e.party[index].GetComponent<Character>().canUseIceDrops == false)
                    {
                        Engine.e.party[index].GetComponent<Character>().canUseIceDrops = true;
                    }

                    party[index].GetComponent<Character>().iceDrops[0] = iceDrops[0];

                    if (!iceDrops[0].isKnown)
                    {
                        iceDrops[0].isKnown = true;
                    }

                    partyInventoryReference.SubtractItemFromInventory(gameIceDrops[0]);

                    break;
                }

            // Lightning Drops
            case "Bolt":

                if (party[index].GetComponent<Character>().lightningDrops[0] != null)
                {

                    break;
                }
                else
                {
                    if (Engine.e.party[index].GetComponent<Character>().canUseLightningDrops == false)
                    {
                        Engine.e.party[index].GetComponent<Character>().canUseLightningDrops = true;
                    }

                    party[index].GetComponent<Character>().lightningDrops[0] = lightningDrops[0];

                    if (!lightningDrops[0].isKnown)
                    {
                        lightningDrops[0].isKnown = true;
                    }

                    partyInventoryReference.SubtractItemFromInventory(gameLightningDrops[0]);

                    break;
                }

            // Water Drops    
            case "Bubble":

                if (party[index].GetComponent<Character>().waterDrops[0] != null)
                {

                    break;
                }
                else
                {
                    if (Engine.e.party[index].GetComponent<Character>().canUseWaterDrops == false)
                    {
                        Engine.e.party[index].GetComponent<Character>().canUseWaterDrops = true;
                    }

                    party[index].GetComponent<Character>().waterDrops[0] = waterDrops[0];

                    if (!waterDrops[0].isKnown)
                    {
                        waterDrops[0].isKnown = true;
                    }

                    partyInventoryReference.SubtractItemFromInventory(gameWaterDrops[0]);

                    break;
                }

            // Shadow Drops    
            case "Dark Embrace":

                if (party[index].GetComponent<Character>().shadowDrops[0] != null)
                {

                    break;
                }
                else
                {
                    if (Engine.e.party[index].GetComponent<Character>().canUseShadowDrops == false)
                    {
                        Engine.e.party[index].GetComponent<Character>().canUseShadowDrops = true;
                    }

                    party[index].GetComponent<Character>().shadowDrops[0] = shadowDrops[0];

                    if (!shadowDrops[0].isKnown)
                    {
                        shadowDrops[0].isKnown = true;
                    }

                    partyInventoryReference.SubtractItemFromInventory(gameShadowDrops[0]);

                    break;
                }

            case "Bio":

                if (party[index].GetComponent<Character>().shadowDrops[1] != null)
                {

                    break;
                }
                else
                {
                    if (Engine.e.party[index].GetComponent<Character>().canUseShadowDrops == false)
                    {
                        Engine.e.party[index].GetComponent<Character>().canUseShadowDrops = true;
                    }

                    party[index].GetComponent<Character>().shadowDrops[1] = shadowDrops[1];

                    if (!shadowDrops[1].isKnown)
                    {
                        shadowDrops[1].isKnown = true;
                    }

                    partyInventoryReference.SubtractItemFromInventory(gameShadowDrops[1]);

                    break;
                }

            case "Knockout":

                if (party[index].GetComponent<Character>().shadowDrops[2] != null)
                {

                    break;
                }
                else
                {
                    if (Engine.e.party[index].GetComponent<Character>().canUseShadowDrops == false)
                    {
                        Engine.e.party[index].GetComponent<Character>().canUseShadowDrops = true;
                    }

                    party[index].GetComponent<Character>().shadowDrops[2] = shadowDrops[2];

                    if (!shadowDrops[2].isKnown)
                    {
                        shadowDrops[2].isKnown = true;
                    }

                    partyInventoryReference.SubtractItemFromInventory(gameShadowDrops[2]);

                    break;
                }

            case "Blind":

                if (party[index].GetComponent<Character>().shadowDrops[3] != null)
                {

                    break;
                }
                else
                {
                    if (Engine.e.party[index].GetComponent<Character>().canUseShadowDrops == false)
                    {
                        Engine.e.party[index].GetComponent<Character>().canUseShadowDrops = true;
                    }

                    party[index].GetComponent<Character>().shadowDrops[3] = shadowDrops[3];

                    if (!shadowDrops[3].isKnown)
                    {
                        shadowDrops[3].isKnown = true;
                    }

                    partyInventoryReference.SubtractItemFromInventory(gameShadowDrops[3]);

                    break;
                }
            // Holy Drops    
            case "Holy Light":

                if (party[index].GetComponent<Character>().holyDrops[0] != null)
                {

                    break;
                }
                else
                {
                    if (Engine.e.party[index].GetComponent<Character>().canUseHolyDrops == false)
                    {
                        Engine.e.party[index].GetComponent<Character>().canUseHolyDrops = true;
                    }

                    party[index].GetComponent<Character>().holyDrops[0] = holyDrops[0];

                    if (!holyDrops[0].isKnown)
                    {
                        holyDrops[0].isKnown = true;
                    }

                    partyInventoryReference.SubtractItemFromInventory(gameHolyDrops[0]);

                    break;
                }

            case "Raise":

                if (party[index].GetComponent<Character>().holyDrops[1] != null)
                {

                    break;
                }
                else
                {
                    if (Engine.e.party[index].GetComponent<Character>().canUseHolyDrops == false)
                    {
                        Engine.e.party[index].GetComponent<Character>().canUseHolyDrops = true;
                    }

                    party[index].GetComponent<Character>().holyDrops[1] = holyDrops[1];

                    if (!holyDrops[1].isKnown)
                    {
                        holyDrops[1].isKnown = true;
                    }

                    partyInventoryReference.SubtractItemFromInventory(gameHolyDrops[1]);

                    break;
                }

            case "Repent":

                if (party[index].GetComponent<Character>().holyDrops[2] != null)
                {

                    break;
                }
                else
                {
                    if (Engine.e.party[index].GetComponent<Character>().canUseHolyDrops == false)
                    {
                        Engine.e.party[index].GetComponent<Character>().canUseHolyDrops = true;
                    }

                    party[index].GetComponent<Character>().holyDrops[2] = holyDrops[2];

                    if (!holyDrops[2].isKnown)
                    {
                        holyDrops[2].isKnown = true;
                    }

                    partyInventoryReference.SubtractItemFromInventory(gameHolyDrops[2]);

                    break;
                }

            case "Dispel":

                if (party[index].GetComponent<Character>().holyDrops[3] != null)
                {

                    break;
                }
                else
                {
                    if (Engine.e.party[index].GetComponent<Character>().canUseHolyDrops == false)
                    {
                        Engine.e.party[index].GetComponent<Character>().canUseHolyDrops = true;
                    }

                    party[index].GetComponent<Character>().holyDrops[3] = holyDrops[3];

                    if (!holyDrops[3].isKnown)
                    {
                        holyDrops[3].isKnown = true;
                    }

                    partyInventoryReference.SubtractItemFromInventory(gameHolyDrops[3]);

                    break;
                }
        }

        partyInventoryReference.DismissDropConfirm();
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


    // Handles character healing, outside of battle.
    public void HealCharacter(string _name, int healAmount)
    {
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i].GetComponent<Character>().characterName == _name)
            {
                if (party[i].GetComponent<Character>().currentHealth == party[i].GetComponent<Character>().maxHealth)
                {
                    Debug.Log("Already at full health!");
                    break;
                }
                if (party[i].GetComponent<Character>().currentHealth + healAmount >= party[i].GetComponent<Character>().maxHealth)
                {
                    party[i].GetComponent<Character>().currentHealth = party[i].GetComponent<Character>().maxHealth;
                }
                else
                    party[i].GetComponent<Character>().currentHealth += healAmount;
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

    // Establishes a new Save File by storing the data found in the GameData class.
    public void SaveGame()
    {
        if (ableToSave)
        {
            SaveSystem.SaveGame(this);
            GameObject dmgPopup = Instantiate(battleSystem.damagePopup, activeParty.transform.position, Quaternion.identity);
            dmgPopup.GetComponentInChildren<TMP_Text>().text = "Save Complete!";
            Destroy(dmgPopup, 1f);
        }
        else
        {
            Debug.Log("Not at save point.");

        }
    }

    // Sets values based on an existing Save File.    
    public void LoadGame()
    {
        GameData gameData = SaveSystem.LoadGame();
        SetInventoryArrayPositions();
        party = new GameObject[playableCharacters.Length];
        activeParty.activeParty = new GameObject[3];

        partyInventoryReference.partyInventory = new Item[partyInventoryReference.itemInventorySlots.Length];
        partyInventoryReference.grieveWeapons = new GrieveWeapons[partyInventoryReference.grieveWeaponInventorySlots.Length];
        partyInventoryReference.macWeapons = new MacWeapons[partyInventoryReference.macWeaponInventorySlots.Length];
        partyInventoryReference.fieldWeapons = new FieldWeapons[partyInventoryReference.fieldWeaponInventorySlots.Length];
        partyInventoryReference.riggsWeapons = new RiggsWeapons[partyInventoryReference.riggsWeaponInventorySlots.Length];
        partyInventoryReference.chestArmor = new ChestArmor[partyInventoryReference.chestArmorInventorySlots.Length];

        charEquippedWeapons = new Item[playableCharacters.Length];
        charEquippedChestArmor = new Item[playableCharacters.Length];

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

                party[i].GetComponent<Character>().baseDamage = gameData.charBaseDamage[i];
                party[i].GetComponent<Character>().physicalDamage = gameData.charPhysicalDamage[i];
                party[i].GetComponent<Character>().experiencePoints = gameData.charXP[i];
                party[i].GetComponent<Character>().levelUpReq = gameData.charLvlUpReq[i];
                party[i].GetComponent<Character>().isInParty = gameData.charInParty[i];

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


                party[i].GetComponent<Character>().fireDrops = new Drops[fireDrops.Length];
                party[i].GetComponent<Character>().holyDrops = new Drops[holyDrops.Length];
                party[i].GetComponent<Character>().waterDrops = new Drops[waterDrops.Length];
                party[i].GetComponent<Character>().lightningDrops = new Drops[lightningDrops.Length];
                party[i].GetComponent<Character>().shadowDrops = new Drops[shadowDrops.Length];
                party[i].GetComponent<Character>().iceDrops = new Drops[iceDrops.Length];



                // Fire Drops
                for (int gameFire = 0; gameFire < e.fireDrops.Length; gameFire++)
                {
                    if (gameData.charFireDrops[i, gameFire] != null)
                        if (gameData.charFireDrops[i, gameFire] == fireDrops[gameFire].dropName)
                            party[i].GetComponent<Character>().fireDrops[gameFire] = fireDrops[gameFire];

                    if (fireDrops[gameFire] != null)
                    {
                        fireDrops[gameFire].isKnown = gameData.fireDropsisKnown[gameFire];
                    }
                }
                // Holy Drops
                for (int gameHoly = 0; gameHoly < e.holyDrops.Length; gameHoly++)
                {
                    if (gameData.charHolyDrops[i, gameHoly] != null)
                        if (gameData.charHolyDrops[i, gameHoly] == holyDrops[gameHoly].dropName)
                            party[i].GetComponent<Character>().holyDrops[gameHoly] = holyDrops[gameHoly];

                    if (holyDrops[gameHoly] != null)
                    {
                        holyDrops[gameHoly].isKnown = gameData.holyDropsisKnown[gameHoly];
                    }
                }
                // Water Drops
                for (int gameWater = 0; gameWater < e.waterDrops.Length; gameWater++)
                {
                    if (gameData.charWaterDrops[i, gameWater] != null)
                        if (gameData.charWaterDrops[i, gameWater] == waterDrops[gameWater].dropName)
                            party[i].GetComponent<Character>().waterDrops[gameWater] = waterDrops[gameWater];

                    if (waterDrops[gameWater] != null)
                    {
                        waterDrops[gameWater].isKnown = gameData.waterDropsisKnown[gameWater];
                    }
                }
                // Lightning Drops
                for (int gameLightning = 0; gameLightning < e.lightningDrops.Length; gameLightning++)
                {
                    if (gameData.charLightningDrops[i, gameLightning] != null)
                        if (gameData.charLightningDrops[i, gameLightning] == lightningDrops[gameLightning].dropName)
                            party[i].GetComponent<Character>().lightningDrops[gameLightning] = lightningDrops[gameLightning];

                    if (lightningDrops[gameLightning] != null)
                    {
                        lightningDrops[gameLightning].isKnown = gameData.lightningDropsisKnown[gameLightning];
                    }
                }
                // Shadow Drops
                for (int gameShadow = 0; gameShadow < e.shadowDrops.Length; gameShadow++)
                {
                    if (gameData.charShadowDrops[i, gameShadow] != null)
                        if (gameData.charShadowDrops[i, gameShadow] == shadowDrops[gameShadow].dropName)
                            party[i].GetComponent<Character>().shadowDrops[gameShadow] = shadowDrops[gameShadow];

                    if (shadowDrops[gameShadow] != null)
                    {
                        shadowDrops[gameShadow].isKnown = gameData.shadowDropsisKnown[gameShadow];
                    }
                }
                // Ice Drops
                for (int gameIce = 0; gameIce < e.iceDrops.Length; gameIce++)
                {
                    if (gameData.charIceDrops[i, gameIce] != null)
                        if (gameData.charIceDrops[i, gameIce] == iceDrops[gameIce].dropName)
                            party[i].GetComponent<Character>().iceDrops[gameIce] = iceDrops[gameIce];

                    if (iceDrops[gameIce] != null)
                    {
                        iceDrops[gameIce].isKnown = gameData.iceDropsisKnown[gameIce];
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

        for (int i = 0; i <= party[0].GetComponent<Grieve>().skillIndex; i++)
        {
            party[0].GetComponent<Grieve>().skills[i] = gameSkills[i];
        }

        if (party[1] != null)
        {
            for (int i = 0; i <= party[1].GetComponent<Mac>().skillIndex; i++)
            {
                party[1].GetComponent<Mac>().skills[i] = gameSkills[i];
            }
        }

        if (party[2] != null)
        {
            for (int i = 0; i <= party[2].GetComponent<Field>().skillIndex; i++)
            {
                party[2].GetComponent<Field>().skills[i] = gameSkills[i];
            }
        }

        if (party[3] != null)
        {
            for (int i = 0; i <= party[3].GetComponent<Riggs>().skillIndex; i++)
            {
                party[3].GetComponent<Riggs>().skills[i] = gameSkills[i];
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
                        if (gameInventory[k].itemName == gameData.grieveWeaponEquip)
                        {
                            playableCharacters[0].GetComponent<Grieve>().EquipGrieveWeaponOnLoad(gameInventory[k].GetComponent<GrieveWeapons>());
                        }
                        if (party[1] != null)
                        {
                            if (gameInventory[k].itemName == gameData.macWeaponEquip)
                            {
                                playableCharacters[1].GetComponent<Mac>().EquipMacWeaponOnLoad(gameInventory[k].GetComponent<MacWeapons>());
                            }
                        }
                        if (party[2] != null)
                        {
                            if (gameInventory[k].itemName == gameData.fieldWeaponEquip)
                            {
                                playableCharacters[2].GetComponent<Field>().EquipFieldWeaponOnLoad(gameInventory[k].GetComponent<FieldWeapons>());
                            }
                        }
                        if (party[3] != null)
                        {
                            if (gameInventory[k].itemName == gameData.riggsWeaponEquip)
                            {
                                playableCharacters[3].GetComponent<Riggs>().EquipRiggsWeaponOnLoad(gameInventory[k].GetComponent<RiggsWeapons>());
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
                    }
                }
            }
        }


        Vector3 partyPosition;
        partyPosition.x = gameData.partyPosition[0];
        partyPosition.y = gameData.partyPosition[1];
        partyPosition.z = gameData.partyPosition[2];

        timeOfDay = gameData.time;
        partyMoney = gameData.partyMoney;
        activeParty.gameObject.GetComponent<SpriteRenderer>().sprite = activeParty.activeParty[0].gameObject.GetComponent<SpriteRenderer>().sprite;
        activeParty.gameObject.transform.position = partyPosition;
        activePartyMember2.gameObject.transform.position = partyPosition;

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
            charSkillTierButtons[1].SetActive(true);

        }
        if (party[2] != null)
        {
            ActivateArrangePartyButton();
            arrangePartyButtonActive = true;
            activePartyMember3.GetComponent<SpriteRenderer>().sprite = party[2].GetComponent<SpriteRenderer>().sprite;
            activePartyMember3.SetActive(true);
            activeParty.activeParty[2] = party[2].GetComponent<Character>().gameObject;
            charAbilityButtons[2].SetActive(true);
            charSkillTierButtons[2].SetActive(true);

        }
        if (party[3] != null)
        {
            battleSystem.battleSwitchButtons = true;
            charAbilityButtons[3].SetActive(true);
            charSkillTierButtons[3].SetActive(true);

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

        if (gameData.scene == "WorldMap")
        {
            if (mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize == 5)
            {
                mainCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 7.5f;
                activeParty.GetComponent<PlayerController>().speed = 3.5f;
                if (activeParty.activeParty[1] != null)
                {
                    activePartyMember2.GetComponent<APFollow>().speed = 3.5f;
                }
                if (activeParty.activeParty[2] != null)
                {
                    activePartyMember3.GetComponent<APFollow>().speed = 3.5f;
                }
            }
        }
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

    // Deals the amount of Physical Damage to the targeted player, based on the
    // PhysicalDamageCalculation() method.
    public bool TakeDamage(int index, float _dmg, float hitChance)
    {
        Character character = activeParty.activeParty[index].GetComponent<Character>();

        float adjustedDodge = Mathf.Round(hitChance - character.dodgeChance);
        int hit = Random.Range(0, 99);

        if (character.isAsleep)
        {
            adjustedDodge = 100;
            character.isAsleep = false;
            character.inflicted = false;

            activeParty.activeParty[index].GetComponent<SpriteRenderer>().color = Color.white;
            activeParty.GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (character.isConfused)
        {
            int snapoutChance = Random.Range(0, 100);
            if (character.confuseDefense > snapoutChance)
            {
                character.isConfused = false;
                character.inflicted = false;
                character.confuseTimer = 0;
                activeParty.GetComponent<SpriteRenderer>().color = Color.white;

                //  GetComponent<SpriteRenderer>().color = Color.white;
            }
        }



        Debug.Log(battleSystem.damageTotal);

        if (hit < adjustedDodge)
        {
            character.currentHealth -= battleSystem.damageTotal;
        }
        else
        {
            Debug.Log("First Dodge");
            battleSystem.dodgedAttack = true;
        }
        if (character.currentHealth <= 0)
        {
            character.currentHealth = 0;

            character.isPoisoned = false;
            character.isConfused = false;
            character.isAsleep = false;
            character.inflicted = false;
            character.poisonDmg = 0f;
            // activeParty.activeParty[index].GetComponent<SpriteRenderer>().color = Color.white;

            switch (index)
            {
                case 0:
                    battleSystem.char1ATB = 0;
                    battleSystem.char1ATBGuage.value = 0;

                    battleSystem.DeactivateChar1MenuButtons();

                    if (battleSystem.state == BattleState.CHAR1TURN)
                    {
                        battleSystem.state = BattleState.ATBCHECK;
                    }

                    break;
                case 1:
                    battleSystem.char2ATB = 0;
                    battleSystem.char2ATBGuage.value = 0;

                    battleSystem.DeactivateChar2MenuButtons();

                    if (battleSystem.state == BattleState.CHAR2TURN)
                    {
                        battleSystem.state = BattleState.ATBCHECK;
                    }
                    break;
                case 2:
                    battleSystem.char3ATB = 0;
                    battleSystem.char3ATBGuage.value = 0;

                    battleSystem.DeactivateChar3MenuButtons();

                    if (battleSystem.state == BattleState.CHAR3TURN)
                    {
                        battleSystem.state = BattleState.ATBCHECK;
                    }
                    break;
            }
        }

        battleSystem.hud.displayHealth[battleSystem.previousTargetReferenceEnemy].text = activeParty.activeParty[battleSystem.previousTargetReferenceEnemy].gameObject.GetComponent<Character>().currentHealth.ToString();


        if (activeParty.activeParty[0].gameObject.GetComponent<Character>().currentHealth <= 0)
        {
            if (activeParty.activeParty[2] != null && activeParty.activeParty[1].gameObject.GetComponent<Character>().currentHealth <= 0
            && activeParty.activeParty[2].gameObject.GetComponent<Character>().currentHealth <= 0)
            {
                return true;
            }

            if (activeParty.activeParty[2] == null && activeParty.activeParty[1] != null)
            {
                if (activeParty.activeParty[1].gameObject.GetComponent<Character>().currentHealth <= 0)
                {
                    return true;

                }
            }

            if (activeParty.activeParty[2] == null && activeParty.activeParty[1] == null)
            {
                return true;
            }
        }
        return false;
    }

    // Calculates how much Physical Damage a party member (index) recieves, typically in battle. Checks for Physical Defense rating,
    // as well as the overall Physical Damage the enemy is dealing (_dmg).
    public void PhysicalDamageCalculation(int index, float _dmg)
    {
        float critChance = 2;

        if (activeParty.activeParty[index] != null)
        {
            if (activeParty.activeParty[index].GetComponent<Character>().currentHealth > 0)
            {
                if (battleSystem.currentInQueue == BattleState.CONFCHAR1)
                {
                    battleSystem.char1AttackTarget = index;
                }
                if (battleSystem.currentInQueue == BattleState.CONFCHAR2)
                {
                    battleSystem.char2AttackTarget = index;
                }
                if (battleSystem.currentInQueue == BattleState.CONFCHAR3)
                {
                    battleSystem.char3AttackTarget = index;
                }
                if (battleSystem.currentInQueue == BattleState.ENEMY1TURN)
                {
                    critChance = Random.Range(0, 10);

                    if (critChance == 0)
                    {
                        float critDamage = Mathf.Round((_dmg + (_dmg * (2 / 3))));
                        _dmg = _dmg + critDamage;
                    }

                    battleSystem.enemy1AttackTarget = index;
                }
                if (battleSystem.currentInQueue == BattleState.ENEMY2TURN)
                {
                    critChance = Random.Range(0, 10);

                    if (critChance == 0)
                    {
                        float critDamage = Mathf.Round((_dmg + (_dmg * (2 / 3))));
                        _dmg = _dmg + critDamage;
                    }

                    battleSystem.enemy2AttackTarget = index;
                }
                if (battleSystem.currentInQueue == BattleState.ENEMY3TURN)
                {
                    critChance = Random.Range(0, 10);

                    if (critChance == 0)
                    {
                        float critDamage = Mathf.Round((_dmg + (_dmg * (2 / 3))));
                        _dmg = _dmg + critDamage;
                    }

                    battleSystem.enemy3AttackTarget = index;
                }
                if (battleSystem.currentInQueue == BattleState.ENEMY4TURN)
                {
                    critChance = Random.Range(0, 10);

                    if (critChance == 0)
                    {
                        float critDamage = Mathf.Round((_dmg + (_dmg * (2 / 3))));
                        _dmg = _dmg + critDamage;
                    }

                    battleSystem.enemy4AttackTarget = index;
                }

                battleSystem.damageTotal = Mathf.Round((_dmg) - (_dmg * (activeParty.activeParty[index].gameObject.GetComponent<Character>().physicalDefense / 100)));
            }
            else
            {
                int newTarget = GetRandomRemainingPartyMember();

                if (battleSystem.currentInQueue == BattleState.ENEMY1TURN)
                {
                    battleSystem.enemy1AttackTarget = newTarget;
                }
                if (battleSystem.currentInQueue == BattleState.ENEMY2TURN)
                {
                    battleSystem.enemy2AttackTarget = newTarget;
                }
                if (battleSystem.currentInQueue == BattleState.ENEMY3TURN)
                {
                    battleSystem.enemy3AttackTarget = newTarget;
                }
                if (battleSystem.currentInQueue == BattleState.ENEMY4TURN)
                {
                    battleSystem.enemy4AttackTarget = newTarget;
                }

                battleSystem.damageTotal = Mathf.Round((_dmg) - (_dmg * activeParty.activeParty[newTarget].gameObject.GetComponent<Character>().physicalDefense / 100));

            }
        }
    }

    // Similar to the Physical Damage methods, but in one. Works the calculation as well
    // as deals the set damage, based on various Elemental Defenses.
    public bool TakeElementalDamage(int index, float _dmg, string dropType)
    {
        if (activeParty.activeParty[index] != null)
        {
            if (activeParty.activeParty[index].GetComponent<Character>().currentHealth > 0)
            {

                if (battleSystem.currentInQueue == BattleState.CONFCHAR1)
                {
                    battleSystem.char1AttackTarget = index;
                }
                if (battleSystem.currentInQueue == BattleState.CONFCHAR2)
                {
                    battleSystem.char2AttackTarget = index;
                }
                if (battleSystem.currentInQueue == BattleState.CONFCHAR3)
                {
                    battleSystem.char3AttackTarget = index;
                }

                if (battleSystem.currentInQueue == BattleState.ENEMY1TURN)
                {
                    battleSystem.enemy1AttackTarget = index;
                }
                if (battleSystem.currentInQueue == BattleState.ENEMY2TURN)
                {
                    battleSystem.enemy2AttackTarget = index;
                }
                if (battleSystem.currentInQueue == BattleState.ENEMY3TURN)
                {
                    battleSystem.enemy3AttackTarget = index;
                }
                if (battleSystem.currentInQueue == BattleState.ENEMY4TURN)
                {
                    battleSystem.enemy4AttackTarget = index;
                }
            }
            else
            {
                int newTarget = GetRandomRemainingPartyMember();

                if (battleSystem.currentInQueue == BattleState.ENEMY1TURN)
                {
                    battleSystem.enemy1AttackTarget = newTarget;
                }
                if (battleSystem.currentInQueue == BattleState.ENEMY2TURN)
                {
                    battleSystem.enemy2AttackTarget = newTarget;
                }
                if (battleSystem.currentInQueue == BattleState.ENEMY3TURN)
                {
                    battleSystem.enemy3AttackTarget = newTarget;
                }
                if (battleSystem.currentInQueue == BattleState.ENEMY4TURN)
                {
                    battleSystem.enemy4AttackTarget = newTarget;
                    index = newTarget;
                }
            }
        }

        // Fire Drop
        if (dropType == "Fire")
        {
            if (activeParty.activeParty[index] != null)
            {
                float damageFirstTargetTotal = Mathf.Round((_dmg) - (_dmg * activeParty.activeParty[index].gameObject.GetComponent<Character>().fireDefense / 100));
                battleSystem.damageTotal = damageFirstTargetTotal;
                Debug.Log(dropType);
                Debug.Log(damageFirstTargetTotal);
                activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth -= damageFirstTargetTotal;

                if (activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth <= 0)
                {
                    activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth = 0;
                }

                //hud.displayHealth[index].text = activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth.ToString();
            }
        }

        // Water Drop
        if (dropType == "Water")
        {
            if (activeParty.activeParty[index] != null)
            {
                float damageFirstTargetTotal = Mathf.Round((_dmg) - (_dmg * activeParty.activeParty[index].gameObject.GetComponent<Character>().waterDefense / 100));
                battleSystem.damageTotal = damageFirstTargetTotal;
                Debug.Log(dropType);
                Debug.Log(damageFirstTargetTotal);
                activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth -= damageFirstTargetTotal;

                if (activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth <= 0)
                {
                    activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth = 0;
                }
            }

        }

        // Lightning Drop
        if (dropType == "Lightning")
        {
            if (activeParty.activeParty[index] != null)
            {
                float damageFirstTargetTotal = Mathf.Round((_dmg) - (_dmg * activeParty.activeParty[index].gameObject.GetComponent<Character>().lightningDefense / 100));
                battleSystem.damageTotal = damageFirstTargetTotal;
                Debug.Log(dropType);
                Debug.Log(damageFirstTargetTotal);
                activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth -= damageFirstTargetTotal;

                if (activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth <= 0)
                {
                    activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth = 0;
                }
            }
        }

        // Shadow Drop
        if (dropType == "Shadow")
        {
            if (activeParty.activeParty[index] != null)
            {
                float damageFirstTargetTotal = Mathf.Round((_dmg) - (_dmg * activeParty.activeParty[index].gameObject.GetComponent<Character>().shadowDefense / 100));
                battleSystem.damageTotal = damageFirstTargetTotal;
                Debug.Log(dropType);
                Debug.Log(damageFirstTargetTotal);
                activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth -= damageFirstTargetTotal;

                if (activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth <= 0)
                {
                    activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth = 0;
                }

                if (battleSystem.lastDropChoice.dropName == "Bio")
                {
                    if (!activeParty.activeParty[index].GetComponent<Character>().isPoisoned)
                    {
                        float poisonChance = Random.Range(0, 100);

                        if (activeParty.activeParty[index].GetComponent<Character>().poisonDefense < poisonChance)
                        {
                            activeParty.activeParty[index].GetComponent<Character>().isPoisoned = true;
                            activeParty.activeParty[index].GetComponent<Character>().poisonDmg = battleSystem.lastDropChoice.dotDmg;
                            activeParty.activeParty[index].GetComponent<Character>().inflicted = true;
                        }
                    }
                }

                if (battleSystem.lastDropChoice.dropName == "Knockout")
                {
                    if (!activeParty.activeParty[index].GetComponent<Character>().isAsleep)
                    {
                        float sleepChance = Random.Range(0, 100);

                        if (activeParty.activeParty[index].GetComponent<Character>().sleepDefense < sleepChance)
                        {
                            activeParty.activeParty[index].GetComponent<Character>().isAsleep = true;
                            activeParty.activeParty[index].GetComponent<Character>().sleepTimer = 0;
                            activeParty.activeParty[index].GetComponent<Character>().inflicted = true;

                            Queue<BattleState> newQueue = new Queue<BattleState>(7);

                            if (index == 0)
                            {
                                battleSystem.DeactivateChar1MenuButtons();
                            }

                            if (index == 1)
                            {
                                battleSystem.DeactivateChar2MenuButtons();
                            }
                            if (index == 2)
                            {
                                battleSystem.DeactivateChar3MenuButtons();
                            }
                        }
                    }
                }

                if (battleSystem.lastDropChoice.dropName == "Blind")
                {
                    if (!activeParty.activeParty[index].GetComponent<Character>().isConfused)
                    {
                        float confuseChance = Random.Range(0, 100);

                        if (activeParty.activeParty[index].GetComponent<Character>().confuseDefense < confuseChance)
                        {
                            activeParty.activeParty[index].GetComponent<Character>().isConfused = true;
                            battleSystem.state = BattleState.ATBCHECK;
                            activeParty.activeParty[index].GetComponent<Character>().inflicted = true;

                            if (index == 0)
                            {
                                battleSystem.DeactivateChar1MenuButtons();
                                battleSystem.char1ATB = (battleSystem.char1ATB / 2);
                                if (battleSystem.currentState == BattleState.CHAR1TURN)
                                {
                                    if (battleSystem.inBattleMenu)
                                    {
                                        battleSystem.DisableBattleMenus();
                                    }
                                }

                            }
                            if (index == 1)
                            {
                                battleSystem.DeactivateChar2MenuButtons();
                                battleSystem.char2ATB = (battleSystem.char2ATB / 2);
                                if (battleSystem.currentState == BattleState.CHAR2TURN)
                                {
                                    if (battleSystem.inBattleMenu)
                                    {
                                        battleSystem.DisableBattleMenus();
                                    }
                                }

                            }
                            if (index == 2)
                            {
                                battleSystem.DeactivateChar3MenuButtons();
                                battleSystem.char3ATB = (battleSystem.char3ATB / 2);
                                if (battleSystem.currentState == BattleState.CHAR3TURN)
                                {
                                    if (battleSystem.inBattleMenu)
                                    {
                                        battleSystem.DisableBattleMenus();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // Ice Drop
        if (dropType == "Ice")
        {
            if (activeParty.activeParty[index] != null)
            {
                float damageFirstTargetTotal = Mathf.Round((_dmg) - (_dmg * activeParty.activeParty[index].gameObject.GetComponent<Character>().iceDefense / 100));
                battleSystem.damageTotal = damageFirstTargetTotal;
                Debug.Log(dropType);
                Debug.Log(damageFirstTargetTotal);
                activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth -= damageFirstTargetTotal;

                if (activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth <= 0)
                {
                    activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth = 0;
                }
            }
        }

        if (activeParty.activeParty[index].GetComponent<Character>().currentHealth <= 0)
        {
            activeParty.activeParty[index].GetComponent<Character>().isPoisoned = false;
            activeParty.activeParty[index].GetComponent<Character>().isConfused = false;
            activeParty.activeParty[index].GetComponent<Character>().isAsleep = false;
            activeParty.activeParty[index].GetComponent<Character>().deathInflicted = false;
            activeParty.activeParty[index].GetComponent<Character>().inflicted = false;
            activeParty.activeParty[index].GetComponent<Character>().poisonDmg = 0f;
            activeParty.activeParty[index].GetComponent<SpriteRenderer>().color = Color.white;

            if (index == 0)
            {
                battleSystem.DeactivateChar1MenuButtons();

                if (battleSystem.state == BattleState.CHAR1TURN)
                {
                    battleSystem.state = BattleState.ATBCHECK;
                }
            }

            if (index == 1)
            {
                battleSystem.DeactivateChar2MenuButtons();

                if (battleSystem.state == BattleState.CHAR2TURN)
                {
                    battleSystem.state = BattleState.ATBCHECK;
                }
            }

            if (index == 2)
            {
                battleSystem.DeactivateChar3MenuButtons();

                if (battleSystem.state == BattleState.CHAR3TURN)
                {
                    battleSystem.state = BattleState.ATBCHECK;
                }
            }
        }

        if (activeParty.activeParty[index].GetComponent<Character>().isAsleep)
        {
            if (battleSystem.lastDropChoice.dropName != "Knockout" && battleSystem.lastDropChoice.dropName != "Bio" && battleSystem.lastDropChoice.dropName != "Blind" && battleSystem.lastDropChoice.dropName != "Death")
            {
                activeParty.activeParty[index].GetComponent<Character>().isAsleep = false;
                activeParty.activeParty[index].GetComponent<Character>().isConfused = false;
                activeParty.activeParty[index].GetComponent<Character>().inflicted = false;
                // activeParty.activeParty[index].GetComponent<SpriteRenderer>().color = Color.grey;
                //activeParty.GetComponent<SpriteRenderer>().color = Color.grey;
            }
        }

        if (activeParty.activeParty[index].GetComponent<Character>().isConfused)
        {
            int snapoutChance = Random.Range(0, 100);
            if (activeParty.activeParty[index].GetComponent<Character>().confuseDefense > snapoutChance)
            {
                activeParty.activeParty[index].GetComponent<Character>().isConfused = false;
                activeParty.activeParty[index].GetComponent<Character>().confuseTimer = 0;
                activeParty.activeParty[index].GetComponent<Character>().inflicted = false;

                //activeParty.activeParty[index].GetComponent<SpriteRenderer>().color = Color.grey;
                // activeParty.GetComponent<SpriteRenderer>().color = Color.grey;

            }

            if (battleSystem.lastDropChoice.dropName == "Knockout")
            {
                activeParty.activeParty[index].GetComponent<Character>().isAsleep = true;
                activeParty.activeParty[index].GetComponent<Character>().isConfused = false;

                //activeParty.activeParty[index].GetComponent<SpriteRenderer>().color = Color.grey;
                // activeParty.GetComponent<SpriteRenderer>().color = Color.grey;
            }
        }

        if (activeParty.activeParty[0].gameObject.GetComponent<Character>().currentHealth <= 0)
        {
            if (activeParty.activeParty[2] != null && activeParty.activeParty[1].gameObject.GetComponent<Character>().currentHealth <= 0
            && activeParty.activeParty[2].gameObject.GetComponent<Character>().currentHealth <= 0)
            {
                return true;
            }

            if (activeParty.activeParty[2] == null && activeParty.activeParty[1] != null)
            {
                if (activeParty.activeParty[1].gameObject.GetComponent<Character>().currentHealth <= 0)
                {
                    return true;

                }
            }

            if (activeParty.activeParty[2] == null && activeParty.activeParty[1] == null)
            {
                return true;
            }
        }
        return false;
    }

    // Calculates and deals Poison Damage (Shadow Damage) to the 
    // effected character.
    public bool TakePoisonDamage(int index, float poisonDmg)
    {
        GameObject characterObject = null;

        if (index == 0)
        {
            characterObject = activeParty.gameObject;
        }
        if (index == 1)
        {
            characterObject = activePartyMember2.gameObject;
        }
        if (index == 2)
        {
            characterObject = activePartyMember3.gameObject;
        }

        float totalDamage = Mathf.Round((poisonDmg) - (poisonDmg * activeParty.activeParty[index].gameObject.GetComponent<Character>().poisonDefense / 100));
        activeParty.activeParty[index].GetComponent<Character>().currentHealth -= totalDamage;

        if (activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth <= 0)
        {
            activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth = 0;
            activeParty.activeParty[index].GetComponent<Character>().isPoisoned = false;
            activeParty.activeParty[index].GetComponent<Character>().isConfused = false;
            activeParty.activeParty[index].GetComponent<Character>().isAsleep = false;
            activeParty.activeParty[index].GetComponent<Character>().poisonDmg = 0f;
            activeParty.activeParty[index].GetComponent<Character>().inflicted = false;
            activeParty.activeParty[index].GetComponent<SpriteRenderer>().color = Color.white;
            characterObject.GetComponent<SpriteRenderer>().color = Color.white;

        }

        battleSystem.hud.displayHealth[index].text = activeParty.activeParty[index].gameObject.GetComponent<Character>().currentHealth.ToString();

        if (activeParty.activeParty[0].gameObject.GetComponent<Character>().currentHealth <= 0)
        {
            if (activeParty.activeParty[2] != null && activeParty.activeParty[1].gameObject.GetComponent<Character>().currentHealth <= 0
            && activeParty.activeParty[2].gameObject.GetComponent<Character>().currentHealth <= 0)
            {
                return true;
            }

            if (activeParty.activeParty[2] == null && activeParty.activeParty[1] != null)
            {
                if (activeParty.activeParty[1].gameObject.GetComponent<Character>().currentHealth <= 0)
                {

                    return true;

                }
            }

            if (activeParty.activeParty[2] == null && activeParty.activeParty[1] == null)
            {
                return true;
            }
        }
        return false;
    }

    // Calls Update() every frame.
    void Update()
    {

        timeOfDay += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //GameManager.gameManager.activeParty.activeParty[0].GetComponent<Grieve>().weapon.GetComponent<GrieveWeapons>().fireAttack += 10;
            partyInventoryReference.AddItemToInventory(gameInventory[0]);
            partyInventoryReference.AddItemToInventory(gameInventory[1]);

            // partyInventoryReference.AddItemToInventory(gameInventory[2]);
            //partyInventoryReference.AddItemToInventory(gameInventory[3]);
            partyInventoryReference.AddItemToInventory(gameGrieveWeapons[1].GetComponent<GrieveWeapons>());
            partyInventoryReference.AddItemToInventory(gameChestArmor[2].GetComponent<ChestArmor>());
            partyInventoryReference.AddItemToInventory(gameFireDrops[0]);

            //partyInventoryReference.AddMacWeaponToInventory(macGameWeapons[2].GetComponent<MacWeapons>());
            //partyInventoryReference.AddFieldWeaponToInventory(fieldGameWeapons[2].GetComponent<FieldWeapons>());

            //partyInventoryReference.AddChestArmorToInventory(gameArmor[0].GetComponent<ChestArmor>());
            // partyInventoryReference.AddChestArmorToInventory(gameArmor[2].GetComponent<ChestArmor>());

            //partyInventoryReference.AddItemToInventory(gameFireDrops[0]);

        }

        if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown("joystick button 2"))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {

        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddCharacterToParty("Mac");
            AddCharacterToParty("Field");
            AddCharacterToParty("Riggs");

            activeParty.SetActiveParty();
        }

        if (inWorldMap)
        {
            if (currentScene != "WorldMap")
            {
                currentScene = "WorldMap";
            }
        }
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
        currentMerchant.SellItem();

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

    public int GetNextRemainingPartyMember()
    {
        remainingPartyMembers = new List<int>();

        for (int i = 0; i < activeParty.activeParty.Length; i++)
        {
            if (activeParty.activeParty[i] != null)
            {

                remainingPartyMembers.Add(i);
                if (activeParty.activeParty[i].GetComponent<Character>().isAsleep)
                {
                    activeParty.activeParty[i].GetComponent<Character>().sleepTimer++;
                    if (activeParty.activeParty[i].GetComponent<Character>().sleepTimer == 3)
                    {
                        activeParty.activeParty[i].GetComponent<Character>().isAsleep = false;
                        activeParty.activeParty[i].GetComponent<Character>().sleepTimer = 0;

                        activeParty.activeParty[i].GetComponent<SpriteRenderer>().color = Color.white;

                    }

                    if (activeParty.activeParty[i].GetComponent<Character>().isPoisoned)
                    {
                        TakePoisonDamage(i, activeParty.activeParty[i].GetComponent<Character>().poisonDmg);
                    }
                    if (activeParty.activeParty[i].GetComponent<Character>().deathInflicted)
                    {
                        activeParty.activeParty[i].GetComponent<Character>().TakeDeathDamage(i);

                    }
                }

            }
        }

        if (battleSystem.currentInQueue == BattleState.CHAR1TURN)
        {
            for (int i = 1; i < remainingPartyMembers.Count; i++)
            {
                if (activeParty.activeParty[i] != null)
                {
                    if (activeParty.activeParty[i].GetComponent<Character>().currentHealth > 0 && !activeParty.activeParty[i].GetComponent<Character>().isAsleep)
                    {
                        nextRemainingCharacterIndex = remainingPartyMembers[i];
                        break;
                    }
                }
            }
        }

        if (battleSystem.currentInQueue == BattleState.CHAR2TURN)
        {
            for (int i = 2; i < remainingPartyMembers.Count; i++)
            {
                if (activeParty.activeParty[i] != null)
                {
                    if (activeParty.activeParty[i].GetComponent<Character>().currentHealth > 0 && !activeParty.activeParty[i].GetComponent<Character>().isAsleep)
                    {
                        nextRemainingCharacterIndex = remainingPartyMembers[i];
                        break;
                    }
                }
            }
        }

        if (battleSystem.currentInQueue == BattleState.CHAR3TURN || battleSystem.currentInQueue == BattleState.ENEMY1TURN || battleSystem.currentInQueue == BattleState.ENEMY2TURN
       || battleSystem.currentInQueue == BattleState.ENEMY3TURN || battleSystem.currentInQueue == BattleState.ENEMY4TURN)
        {
            for (int i = 0; i < remainingPartyMembers.Count; i++)
            {
                if (activeParty.activeParty[i] != null)
                {
                    if (activeParty.activeParty[i].GetComponent<Character>().currentHealth > 0 && !activeParty.activeParty[i].GetComponent<Character>().isAsleep)
                    {
                        nextRemainingCharacterIndex = remainingPartyMembers[i];
                        break;
                    }
                }
            }
        }


        /* if (battleSystem.state == BattleState.CHAR1TURN)
         {
             nextRemainingCharacterIndex = 1;
         }
         if (battleSystem.state == BattleState.CHAR2TURN)
         {
             nextRemainingCharacterIndex = 2;
         }
         if (battleSystem.state == BattleState.CHAR3TURN || battleSystem.state == BattleState.ENEMY1TURN || battleSystem.state == BattleState.ENEMY2TURN
         || battleSystem.state == BattleState.ENEMY3TURN || battleSystem.state == BattleState.ENEMY4TURN)
         {
             nextRemainingCharacterIndex = 0;
         }

         return remainingPartyMembers[nextRemainingCharacterIndex];*/
        return nextRemainingCharacterIndex;
    }

    // Cuz lazy idk, should only run once anyway.
    public void SetInventoryArrayPositions()
    {
        for (int i = 0; i < partyInventoryReference.itemInventorySlots.Length; i++)
        {
            partyInventoryReference.itemInventorySlots[i].index = i;
        }

        for (int i = 0; i < partyInventoryReference.grieveWeaponInventorySlots.Length; i++)
        {
            partyInventoryReference.grieveWeaponInventorySlots[i].index = i;
        }

        for (int i = 0; i < partyInventoryReference.macWeaponInventorySlots.Length; i++)
        {
            partyInventoryReference.macWeaponInventorySlots[i].index = i;
        }

        for (int i = 0; i < partyInventoryReference.fieldWeaponInventorySlots.Length; i++)
        {
            partyInventoryReference.fieldWeaponInventorySlots[i].index = i;
        }

        for (int i = 0; i < partyInventoryReference.riggsWeaponInventorySlots.Length; i++)
        {
            partyInventoryReference.riggsWeaponInventorySlots[i].index = i;
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
}



