using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{

    public string[] charNames;
    public float[] charMaxHealth;
    public float[] charCurrentHealth;
    public float[] charMaxMana;
    public float[] charCurrentMana;
    public int[] charLvl;
    public float[] charXP;
    public float[] charLvlUpReq;
    public float[] charBaseDamage;
    public float[] charPhysicalDamage;
    public float[] charSkillScale;
    public int[] charSkillIndex;
    public int[] charAvailableSkillPoints;
    public float[] charStealChance;
    public float[] charHaste;

    public float[] charDodgeChance;
    public float[] charPhysicalDefense;
    public float[] charFireDefense;
    public float[] charIceDefense;
    public float[] charWaterDefense;
    public float[] charLightningDefense;
    public float[] charShadowDefense;
    public float[] charPoisonDefense;
    public float[] charSleepDefense;
    public float[] charConfuseDefense;



    public string[,] charFireDrops;
    public string[,] charIceDrops;
    public string[,] charHolyDrops;
    public string[,] charWaterDrops;
    public string[,] charLightningDrops;
    public string[,] charShadowDrops;

    public bool[] fireDropsisKnown, iceDropsisKnown, lightningDropsisKnown, waterDropsisKnown, shadowDropsisKnown, holyDropsisKnown;

    public float[] charFireDropLevel, charWaterDropLevel, charLightningDropLevel, charShadowDropLevel, charIceDropLevel, charHolyDropLevel;
    public float[] charFireDropExperience, charWaterDropExperience, charLightningDropExperience, charShadowDropExperience, charIceDropExperience, charHolyDropExperience;
    public float[] charFireDropLvlReq, charWaterDropLvlReq, charLightningDropLvlReq, charShadowDropLvlReq, charIceDropLvlReq, charHolyDropLvlReq;
    public float[] charFireDropAttackBonus, charWaterDropAttackBonus, charLightningDropAttackBonus, charShadowDropAttackBonus, charIceDropAttackBonus;
    public float[] charFirePhysicalAttackBonus, charWaterPhysicalAttackBonus, charLightningPhysicalAttackBonus, charShadowPhysicalAttackBonus, charIcePhysicalAttackBonus;

    public string[] grieveWeapons;
    public string grieveWeaponEquip;
    public string[] macWeapons;
    public string macWeaponEquip;
    public string[] fieldWeapons;
    public string fieldWeaponEquip;
    public string[] riggsWeapons;
    public string riggsWeaponEquip;

    public string[] charChestArmorEquip;


    public bool[] charInParty;
    public string[] activeParty;
    public bool arrangePartyButtonActive;

    public string[] partyInvNames;
    public int[] partyInvAmounts;
    public int partySize;
    public string[] partyArmor;
    public float[] partyPosition;
    public int partyMoney;


    public string scene;
    public float time;
    public bool battleModeActive;

    public GameData(Engine gameManager)
    {
        charNames = new string[gameManager.party.Length];
        charMaxHealth = new float[gameManager.party.Length];
        charMaxMana = new float[gameManager.party.Length];
        charLvl = new int[gameManager.party.Length];

        charFireDropLevel = new float[gameManager.party.Length];
        charWaterDropLevel = new float[gameManager.party.Length];
        charLightningDropLevel = new float[gameManager.party.Length];
        charShadowDropLevel = new float[gameManager.party.Length];
        charIceDropLevel = new float[gameManager.party.Length];
        charHolyDropLevel = new float[gameManager.party.Length];

        charFireDropExperience = new float[gameManager.party.Length];
        charWaterDropExperience = new float[gameManager.party.Length];
        charLightningDropExperience = new float[gameManager.party.Length];
        charShadowDropExperience = new float[gameManager.party.Length];
        charIceDropExperience = new float[gameManager.party.Length];
        charHolyDropExperience = new float[gameManager.party.Length];

        charFireDropLvlReq = new float[gameManager.party.Length];
        charWaterDropLvlReq = new float[gameManager.party.Length];
        charLightningDropLvlReq = new float[gameManager.party.Length];
        charShadowDropLvlReq = new float[gameManager.party.Length];
        charIceDropLvlReq = new float[gameManager.party.Length];
        charHolyDropLvlReq = new float[gameManager.party.Length];

        charFireDropAttackBonus = new float[gameManager.party.Length];
        charWaterDropAttackBonus = new float[gameManager.party.Length];
        charLightningDropAttackBonus = new float[gameManager.party.Length];
        charShadowDropAttackBonus = new float[gameManager.party.Length];
        charIceDropAttackBonus = new float[gameManager.party.Length];

        charFirePhysicalAttackBonus = new float[gameManager.party.Length];
        charWaterPhysicalAttackBonus = new float[gameManager.party.Length];
        charLightningPhysicalAttackBonus = new float[gameManager.party.Length];
        charShadowPhysicalAttackBonus = new float[gameManager.party.Length];
        charIcePhysicalAttackBonus = new float[gameManager.party.Length];

        charBaseDamage = new float[gameManager.party.Length];
        charPhysicalDamage = new float[gameManager.party.Length];
        charXP = new float[gameManager.party.Length];
        charLvlUpReq = new float[gameManager.party.Length];
        charInParty = new bool[gameManager.party.Length];
        charDodgeChance = new float[gameManager.party.Length];
        charPhysicalDefense = new float[gameManager.party.Length];
        charFireDefense = new float[gameManager.party.Length];
        charWaterDefense = new float[gameManager.party.Length];
        charLightningDefense = new float[gameManager.party.Length];
        charShadowDefense = new float[gameManager.party.Length];
        charIceDefense = new float[gameManager.party.Length];
        charPoisonDefense = new float[gameManager.party.Length];
        charSleepDefense = new float[gameManager.party.Length];
        charConfuseDefense = new float[gameManager.party.Length];
        charHaste = new float[gameManager.party.Length];


        charFireDrops = new string[gameManager.party.Length, gameManager.fireDrops.Length];
        charHolyDrops = new string[gameManager.party.Length, gameManager.holyDrops.Length];
        charWaterDrops = new string[gameManager.party.Length, gameManager.waterDrops.Length];
        charLightningDrops = new string[gameManager.party.Length, gameManager.lightningDrops.Length];
        charShadowDrops = new string[gameManager.party.Length, gameManager.shadowDrops.Length];
        charIceDrops = new string[gameManager.party.Length, gameManager.iceDrops.Length];

        fireDropsisKnown = new bool[gameManager.fireDrops.Length];
        iceDropsisKnown = new bool[gameManager.iceDrops.Length];
        lightningDropsisKnown = new bool[gameManager.lightningDrops.Length];
        waterDropsisKnown = new bool[gameManager.waterDrops.Length];
        shadowDropsisKnown = new bool[gameManager.shadowDrops.Length];
        holyDropsisKnown = new bool[gameManager.holyDrops.Length];

        charSkillScale = new float[gameManager.party.Length];
        charSkillIndex = new int[gameManager.party.Length];
        charAvailableSkillPoints = new int[gameManager.party.Length];
        charStealChance = new float[gameManager.party.Length];


        battleModeActive = gameManager.battleModeActive;
        arrangePartyButtonActive = gameManager.arrangePartyButtonActive;


        for (int i = 0; i < gameManager.party.Length; i++)
        {
            if (gameManager.party[i] != null)
            {
                charNames[i] = gameManager.party[i].GetComponent<Character>().characterName;
                charMaxHealth[i] = gameManager.party[i].GetComponent<Character>().maxHealth;
                charMaxMana[i] = gameManager.party[i].GetComponent<Character>().maxMana;
                charLvl[i] = gameManager.party[i].GetComponent<Character>().lvl;

                charFireDropLevel[i] = gameManager.party[i].GetComponent<Character>().fireDropsLevel;
                charWaterDropLevel[i] = gameManager.party[i].GetComponent<Character>().waterDropsLevel;
                charLightningDropLevel[i] = gameManager.party[i].GetComponent<Character>().lightningDropsLevel;
                charShadowDropLevel[i] = gameManager.party[i].GetComponent<Character>().shadowDropsLevel;
                charIceDropLevel[i] = gameManager.party[i].GetComponent<Character>().iceDropsLevel;
                charHolyDropLevel[i] = gameManager.party[i].GetComponent<Character>().holyDropsLevel;

                charFireDropExperience[i] = gameManager.party[i].GetComponent<Character>().fireDropsExperience;
                charWaterDropExperience[i] = gameManager.party[i].GetComponent<Character>().waterDropsExperience;
                charLightningDropExperience[i] = gameManager.party[i].GetComponent<Character>().lightningDropsExperience;
                charShadowDropExperience[i] = gameManager.party[i].GetComponent<Character>().shadowDropsExperience;
                charIceDropExperience[i] = gameManager.party[i].GetComponent<Character>().iceDropsExperience;
                charHolyDropExperience[i] = gameManager.party[i].GetComponent<Character>().holyDropsExperience;

                charFireDropLvlReq[i] = gameManager.party[i].GetComponent<Character>().fireDropsLvlReq;
                charWaterDropLvlReq[i] = gameManager.party[i].GetComponent<Character>().waterDropsLvlReq;
                charLightningDropLvlReq[i] = gameManager.party[i].GetComponent<Character>().lightningDropsLvlReq;
                charShadowDropLvlReq[i] = gameManager.party[i].GetComponent<Character>().shadowDropsLvlReq;
                charIceDropLvlReq[i] = gameManager.party[i].GetComponent<Character>().iceDropsLvlReq;
                charHolyDropLvlReq[i] = gameManager.party[i].GetComponent<Character>().holyDropsLvlReq;

                charFireDropAttackBonus[i] = gameManager.party[i].GetComponent<Character>().fireDropAttackBonus;
                charWaterDropAttackBonus[i] = gameManager.party[i].GetComponent<Character>().waterDropAttackBonus;
                charLightningDropAttackBonus[i] = gameManager.party[i].GetComponent<Character>().lightningDropAttackBonus;
                charShadowDropAttackBonus[i] = gameManager.party[i].GetComponent<Character>().shadowDropAttackBonus;
                charIceDropAttackBonus[i] = gameManager.party[i].GetComponent<Character>().iceDropAttackBonus;

                charFirePhysicalAttackBonus[i] = gameManager.party[i].GetComponent<Character>().firePhysicalAttackBonus;
                charWaterPhysicalAttackBonus[i] = gameManager.party[i].GetComponent<Character>().waterPhysicalAttackBonus;
                charLightningPhysicalAttackBonus[i] = gameManager.party[i].GetComponent<Character>().lightningPhysicalAttackBonus;
                charShadowPhysicalAttackBonus[i] = gameManager.party[i].GetComponent<Character>().shadowPhysicalAttackBonus;
                charIcePhysicalAttackBonus[i] = gameManager.party[i].GetComponent<Character>().icePhysicalAttackBonus;

                charBaseDamage[i] = gameManager.party[i].GetComponent<Character>().baseDamage;
                charPhysicalDamage[i] = gameManager.party[i].GetComponent<Character>().physicalDamage;
                charXP[i] = gameManager.party[i].GetComponent<Character>().experiencePoints;
                charLvlUpReq[i] = gameManager.party[i].GetComponent<Character>().levelUpReq;
                charInParty[i] = gameManager.party[i].GetComponent<Character>().isInParty;
                charDodgeChance[i] = gameManager.party[i].GetComponent<Character>().dodgeChance;
                charPhysicalDefense[i] = gameManager.party[i].GetComponent<Character>().physicalDefense;
                charFireDefense[i] = gameManager.party[i].GetComponent<Character>().fireDefense;
                charWaterDefense[i] = gameManager.party[i].GetComponent<Character>().waterDefense;
                charLightningDefense[i] = gameManager.party[i].GetComponent<Character>().lightningDefense;
                charShadowDefense[i] = gameManager.party[i].GetComponent<Character>().shadowDefense;
                charIceDefense[i] = gameManager.party[i].GetComponent<Character>().iceDefense;
                charPoisonDefense[i] = gameManager.party[i].GetComponent<Character>().poisonDefense;
                charSleepDefense[i] = gameManager.party[i].GetComponent<Character>().sleepDefense;
                charConfuseDefense[i] = gameManager.party[i].GetComponent<Character>().confuseDefense;

                charSkillScale[i] = gameManager.party[i].GetComponent<Character>().skillScale;
                charSkillIndex[i] = gameManager.party[i].GetComponent<Character>().skillIndex;
                charAvailableSkillPoints[i] = gameManager.party[i].GetComponent<Character>().availableSkillPoints;
                charStealChance[i] = gameManager.party[i].GetComponent<Character>().stealChance;
                charHaste[i] = gameManager.party[i].GetComponent<Character>().haste;

                // Fire Drops
                for (int f = 0; f < gameManager.party[i].GetComponent<Character>().fireDrops.Length; f++)
                {
                    if (gameManager.party[i].GetComponent<Character>().fireDrops[f] != null)
                    {
                        if (gameManager.party[i].GetComponent<Character>().fireDrops[f] = gameManager.fireDrops[f])
                        {
                            charFireDrops[i, f] = gameManager.fireDrops[f].dropName;
                            fireDropsisKnown[f] = true;
                        }
                    }
                }
                // Holy Drops
                for (int f = 0; f < gameManager.party[i].GetComponent<Character>().holyDrops.Length; f++)
                {
                    if (gameManager.party[i].GetComponent<Character>().holyDrops[f] != null)
                    {
                        if (gameManager.party[i].GetComponent<Character>().holyDrops[f] = gameManager.holyDrops[f])
                        {
                            charHolyDrops[i, f] = gameManager.holyDrops[f].dropName;
                            holyDropsisKnown[f] = true;

                        }
                    }
                }
                // Water Drops
                for (int f = 0; f < gameManager.party[i].GetComponent<Character>().waterDrops.Length; f++)
                {
                    if (gameManager.party[i].GetComponent<Character>().waterDrops[f] != null)
                    {
                        if (gameManager.party[i].GetComponent<Character>().waterDrops[f] = gameManager.waterDrops[f])
                        {
                            charWaterDrops[i, f] = gameManager.waterDrops[f].dropName;
                            waterDropsisKnown[f] = true;

                        }
                    }
                }
                // Lightning Drops
                for (int f = 0; f < gameManager.party[i].GetComponent<Character>().lightningDrops.Length; f++)
                {
                    if (gameManager.party[i].GetComponent<Character>().lightningDrops[f] != null)
                    {
                        if (gameManager.party[i].GetComponent<Character>().lightningDrops[f] = gameManager.lightningDrops[f])
                        {
                            charLightningDrops[i, f] = gameManager.lightningDrops[f].dropName;
                            lightningDropsisKnown[f] = true;

                        }
                    }
                }
                // Shadow Drops
                for (int f = 0; f < gameManager.party[i].GetComponent<Character>().shadowDrops.Length; f++)
                {
                    if (gameManager.party[i].GetComponent<Character>().shadowDrops[f] != null)
                    {
                        if (gameManager.party[i].GetComponent<Character>().shadowDrops[f] = gameManager.shadowDrops[f])
                        {
                            charShadowDrops[i, f] = gameManager.shadowDrops[f].dropName;
                            shadowDropsisKnown[f] = true;

                        }
                    }
                }
                // Ice Drops
                for (int f = 0; f < gameManager.party[i].GetComponent<Character>().iceDrops.Length; f++)
                {
                    if (gameManager.party[i].GetComponent<Character>().iceDrops[f] != null)
                    {
                        if (gameManager.party[i].GetComponent<Character>().iceDrops[f] = gameManager.iceDrops[f])
                        {
                            charIceDrops[i, f] = gameManager.iceDrops[f].dropName;
                            iceDropsisKnown[f] = true;

                        }
                    }
                }

                partySize++;
            }
        }


        partyInvNames = new string[gameManager.partyInventoryReference.partyInventory.Length];
        partyInvAmounts = new int[gameManager.partyInventoryReference.partyInventory.Length];

        grieveWeapons = new string[gameManager.partyInventoryReference.grieveWeaponInventorySlots.Length];
        macWeapons = new string[gameManager.partyInventoryReference.macWeaponInventorySlots.Length];
        fieldWeapons = new string[gameManager.partyInventoryReference.fieldWeaponInventorySlots.Length];
        riggsWeapons = new string[gameManager.partyInventoryReference.riggsWeaponInventorySlots.Length];

        charChestArmorEquip = new string[Engine.e.party.Length];

        // Items / Weapons / Armor
        for (int i = 0; i < partyInvNames.Length; i++)
        {
            if (gameManager.partyInventoryReference.partyInventory[i] != null)
            {
                partyInvNames[i] = gameManager.partyInventoryReference.partyInventory[i].itemName;
                partyInvAmounts[i] = gameManager.partyInventoryReference.partyInventory[i].numberHeld;
            }
        }

        // Equipped Weapons
        grieveWeaponEquip = Engine.e.party[0].GetComponent<Character>().weapon.GetComponent<GrieveWeapons>().itemName;

        if (Engine.e.party[1] != null)
        {
            macWeaponEquip = Engine.e.party[1].GetComponent<Character>().weapon.GetComponent<MacWeapons>().itemName;
        }
        if (Engine.e.party[2] != null)
        {
            fieldWeaponEquip = Engine.e.party[2].GetComponent<Character>().weapon.GetComponent<FieldWeapons>().itemName;
        }
        if (Engine.e.party[3] != null)
        {
            riggsWeaponEquip = Engine.e.party[3].GetComponent<Character>().weapon.GetComponent<RiggsWeapons>().itemName;
        }

        // Equipped Chest Armor
        for (int i = 0; i < charChestArmorEquip.Length; i++)
        {
            if (Engine.e.party[i] != null)
            {
                charChestArmorEquip[i] = Engine.e.party[i].GetComponent<Character>().chestArmor.GetComponent<ChestArmor>().itemName;
            }
        }

        partyPosition = new float[3];
        partyPosition[0] = Engine.e.activeParty.transform.position.x;
        partyPosition[1] = Engine.e.activeParty.transform.position.y;
        partyPosition[2] = Engine.e.activeParty.transform.position.z;

        activeParty = new string[3];

        activeParty[0] = gameManager.activeParty.activeParty[0].GetComponent<Character>().characterName;

        if (gameManager.activeParty.activeParty[1] != null)
        {
            activeParty[1] = gameManager.activeParty.activeParty[1].GetComponent<Character>().characterName;
        }
        if (gameManager.activeParty.activeParty[2] != null)
        {
            activeParty[2] = gameManager.activeParty.activeParty[2].GetComponent<Character>().characterName;
        }

        scene = gameManager.currentScene;
        time = gameManager.timeOfDay;

        partyMoney = gameManager.partyMoney;

    }

}
