using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Mac : Character
{

    public void AddMacSkill(int addSkillChoice)
    {
        if (addSkillChoice == 0)
        {
        }
    }

    public void EquipMacWeapon(MacWeapons _weapon)
    {

        // Resetting Stats To Base Values
        physicalDamage -= weapon.GetComponent<MacWeapons>().physicalAttack;
        firePhysicalAttackBonus -= weapon.GetComponent<MacWeapons>().fireAttack;
        waterPhysicalAttackBonus -= weapon.GetComponent<MacWeapons>().waterAttack;
        lightningPhysicalAttackBonus -= weapon.GetComponent<MacWeapons>().lightningAttack;
        shadowPhysicalAttackBonus -= weapon.GetComponent<MacWeapons>().shadowAttack;
        icePhysicalAttackBonus -= weapon.GetComponent<MacWeapons>().iceAttack;

        // Swap Inventory Slots
        Engine.e.partyInventoryReference.SubtractItemFromInventory(_weapon);
        Engine.e.partyInventoryReference.AddItemToInventory(weapon);

        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macEquippedWeapon.GetComponent<InventorySlot>().item = _weapon;
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macEquippedWeapon.GetComponentInChildren<TMP_Text>().text = _weapon.itemName;


        // Equip Weapon
        weapon = _weapon;
        Engine.e.charEquippedWeapons[1] = _weapon;

        physicalDamage += _weapon.physicalAttack;
        firePhysicalAttackBonus += _weapon.fireAttack;
        waterPhysicalAttackBonus += _weapon.waterAttack;
        lightningPhysicalAttackBonus += _weapon.lightningAttack;
        shadowPhysicalAttackBonus += _weapon.shadowAttack;
        icePhysicalAttackBonus += _weapon.iceAttack;

        // Set Array Position For New (Equipped) Weapon
        weapon.itemIndex = -1;

        // Update Stats (visually) and Return To Equip Screen
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayMacStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().weaponLists[1].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetMacScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macWeaponInventorySet = false;

    }
    public void EquipMacWeaponOnLoad(MacWeapons _weapon)
    {
        weapon = _weapon;
        Engine.e.charEquippedWeapons[1] = _weapon;
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macEquippedWeapon.GetComponent<InventorySlot>().item = _weapon;
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macEquippedWeapon.GetComponentInChildren<TMP_Text>().text = _weapon.itemName;
    }

    public void EquipMacChestArmor(ChestArmor _armor)
    {
        // Resetting Stats Back to Base Values
        physicalDefense -= chestArmor.GetComponent<ChestArmor>().physicalArmor;
        fireDefense -= chestArmor.GetComponent<ChestArmor>().fireDefense;
        waterDefense -= chestArmor.GetComponent<ChestArmor>().waterDefense;
        lightningDropsLevel -= chestArmor.GetComponent<ChestArmor>().lightningDefense;
        shadowDefense -= chestArmor.GetComponent<ChestArmor>().shadowDefense;
        iceDefense -= chestArmor.GetComponent<ChestArmor>().iceDefense;

        fireDropAttackBonus -= chestArmor.GetComponent<ChestArmor>().fireAttackBonus;
        waterDropAttackBonus -= chestArmor.GetComponent<ChestArmor>().waterAttackBonus;
        lightningDropAttackBonus -= chestArmor.GetComponent<ChestArmor>().lightningAttackBonus;
        shadowDropAttackBonus -= chestArmor.GetComponent<ChestArmor>().shadowAttackBonus;
        iceDropAttackBonus -= chestArmor.GetComponent<ChestArmor>().iceAttackBonus;
        skillCostReduction -= chestArmor.GetComponent<ChestArmor>().skillCostReduction;

        // Swap Inventory Slots
        Engine.e.partyInventoryReference.SubtractItemFromInventory(_armor);
        Engine.e.partyInventoryReference.AddItemToInventory(chestArmor);

        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macEquippedChestArmor.GetComponent<InventorySlot>().item = _armor;
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macEquippedChestArmor.GetComponentInChildren<TMP_Text>().text = _armor.itemName;

        // Equip Armor
        chestArmor = _armor;
        Engine.e.charEquippedChestArmor[1] = _armor;

        physicalDefense += chestArmor.GetComponent<ChestArmor>().physicalArmor;
        fireDefense += chestArmor.GetComponent<ChestArmor>().fireDefense;
        waterDefense += chestArmor.GetComponent<ChestArmor>().waterDefense;
        lightningDefense += chestArmor.GetComponent<ChestArmor>().lightningDefense;
        shadowDefense += chestArmor.GetComponent<ChestArmor>().shadowDefense;
        iceDefense += chestArmor.GetComponent<ChestArmor>().iceDefense;
        skillCostReduction += chestArmor.GetComponent<ChestArmor>().skillCostReduction;

        fireDropAttackBonus += chestArmor.GetComponent<ChestArmor>().fireAttackBonus;
        waterDropAttackBonus += chestArmor.GetComponent<ChestArmor>().waterAttackBonus;
        lightningDropAttackBonus += chestArmor.GetComponent<ChestArmor>().lightningAttackBonus;
        shadowDropAttackBonus += chestArmor.GetComponent<ChestArmor>().shadowAttackBonus;
        iceDropAttackBonus += chestArmor.GetComponent<ChestArmor>().iceAttackBonus;

        // Update Stats (visually) and Return To Equip Screen
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayMacStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().armorLists[0].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetMacScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().chestArmorInventorySet = false;


    }

    public void EquipMacChestArmorOnLoad(ChestArmor _armor)
    {
        chestArmor = _armor;
        Engine.e.charEquippedChestArmor[1] = _armor;
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macEquippedChestArmor.GetComponent<InventorySlot>().item = _armor;
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macEquippedChestArmor.GetComponentInChildren<TMP_Text>().text = _armor.itemName;
    }

    public void ActivateMacDropsUI()
    {
        int fireIndex = 0;
        int iceIndex = 10;
        int lightningIndex = 20;
        int waterIndex = 30;
        int shadowIndex = 40;
        int holyIndex = 50;

        for (int i = 0; i < 10; i++)
        {
            // Fire Drops
            if (canUseFireDrops == true)
            {
                if (fireDrops[i] != null)
                {
                    Engine.e.battleSystem.dropsButtons[fireIndex].GetComponentInChildren<TextMeshProUGUI>().text = fireDrops[i].dropName;
                    fireIndex++;
                }
            }


            // Ice Drops
            if (canUseIceDrops == true)
            {
                if (iceDrops[i] != null)
                {
                    Engine.e.battleSystem.dropsButtons[iceIndex].GetComponentInChildren<TextMeshProUGUI>().text = iceDrops[i].dropName;
                    iceIndex++;
                }
            }


            // Lightning Drops
            if (canUseLightningDrops == true)
            {
                if (lightningDrops[i] != null)
                {
                    Engine.e.battleSystem.dropsButtons[lightningIndex].GetComponentInChildren<TextMeshProUGUI>().text = lightningDrops[i].dropName;
                    lightningIndex++;
                }
            }


            // Water Drops
            if (canUseWaterDrops == true)
            {
                if (waterDrops[i] != null)
                {
                    Engine.e.battleSystem.dropsButtons[waterIndex].GetComponentInChildren<TextMeshProUGUI>().text = waterDrops[i].dropName;
                    waterIndex++;
                }
            }


            // Shadow Drops
            if (canUseShadowDrops == true)
            {

                if (shadowDrops[i] != null)
                {
                    Engine.e.battleSystem.dropsButtons[shadowIndex].GetComponentInChildren<TextMeshProUGUI>().text = shadowDrops[i].dropName;
                    shadowIndex++;
                }
            }


            // Holy Drops
            if (canUseHolyDrops == true)
            {

                if (holyDrops[i] != null)
                {
                    Engine.e.battleSystem.dropsButtons[holyIndex].GetComponentInChildren<TextMeshProUGUI>().text = holyDrops[i].dropName;
                    holyIndex++;
                }
            }
        }
    }


    public void ActivateMacSkillsUI()
    {
        for (int i = 0; i < Engine.e.gameSkills.Length; i++)
        {
            if (skills[i] != null)
            {
                Engine.e.battleSystem.skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = skills[i].skillName;

            }
        }
    }
}