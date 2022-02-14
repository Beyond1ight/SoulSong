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

        //   Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macEquippedWeapon.GetComponent<InventorySlot>().item = _weapon;
        //  Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macEquippedWeapon.GetComponentInChildren<TMP_Text>().text = _weapon.itemName;


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
        // Engine.e.equipMenuReference.GetComponent<EquipDisplay>().equipped.GetComponent<InventorySlot>().item = _weapon;
        // Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macEquippedWeapon.GetComponentInChildren<TMP_Text>().text = _weapon.itemName;
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

        // Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macEquippedChestArmor.GetComponent<InventorySlot>().item = _armor;
        // Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macEquippedChestArmor.GetComponentInChildren<TMP_Text>().text = _armor.itemName;

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
        //  Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macEquippedChestArmor.GetComponent<InventorySlot>().item = _armor;
        //  Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macEquippedChestArmor.GetComponentInChildren<TMP_Text>().text = _armor.itemName;
    }

    public void EquipMacAccessory1(Accessory _accessory)
    {
        if (accessory1 != null)
        {
            // Resetting Stats Back to Base Values
            physicalDefense -= accessory1.GetComponent<Accessory>().physicalArmor;
            fireDefense -= accessory1.GetComponent<Accessory>().fireDefense;
            waterDefense -= accessory1.GetComponent<Accessory>().waterDefense;
            lightningDropsLevel -= accessory1.GetComponent<Accessory>().lightningDefense;
            shadowDefense -= accessory1.GetComponent<Accessory>().shadowDefense;
            iceDefense -= accessory1.GetComponent<Accessory>().iceDefense;

            physicalDamage -= accessory1.GetComponent<Accessory>().physicalAttack;
            fireDropAttackBonus -= accessory1.GetComponent<Accessory>().fireAttackBonus;
            waterDropAttackBonus -= accessory1.GetComponent<Accessory>().waterAttackBonus;
            lightningDropAttackBonus -= accessory1.GetComponent<Accessory>().lightningAttackBonus;
            shadowDropAttackBonus -= accessory1.GetComponent<Accessory>().shadowAttackBonus;
            iceDropAttackBonus -= accessory1.GetComponent<Accessory>().iceAttackBonus;
            skillCostReduction -= accessory1.GetComponent<Accessory>().skillCostReduction;

            // Swap Inventory Slots

            Engine.e.partyInventoryReference.AddItemToInventory(accessory1);
        }
        Engine.e.partyInventoryReference.SubtractItemFromInventory(_accessory);

        // Equip Armor
        accessory1 = _accessory;
        Engine.e.charEquippedAccessory1[1] = _accessory;

        physicalDefense += accessory1.GetComponent<Accessory>().physicalArmor;
        fireDefense += accessory1.GetComponent<Accessory>().fireDefense;
        waterDefense += accessory1.GetComponent<Accessory>().waterDefense;
        lightningDefense += accessory1.GetComponent<Accessory>().lightningDefense;
        shadowDefense += accessory1.GetComponent<Accessory>().shadowDefense;
        iceDefense += accessory1.GetComponent<Accessory>().iceDefense;
        skillCostReduction += accessory1.GetComponent<Accessory>().skillCostReduction;

        physicalDamage += accessory1.GetComponent<Accessory>().physicalAttack;
        fireDropAttackBonus += accessory1.GetComponent<Accessory>().fireAttackBonus;
        waterDropAttackBonus += accessory1.GetComponent<Accessory>().waterAttackBonus;
        lightningDropAttackBonus += accessory1.GetComponent<Accessory>().lightningAttackBonus;
        shadowDropAttackBonus += accessory1.GetComponent<Accessory>().shadowAttackBonus;
        iceDropAttackBonus += accessory1.GetComponent<Accessory>().iceAttackBonus;

        // Update Stats (visually) and Return To Equip Screen
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayMacStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().armorLists[1].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetMacScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().accessory1InventorySet = false;

        Engine.e.partyInventoryReference.indexReference = -1;
    }

    public void EquipMacAccessory1OnLoad(Accessory _accessory)
    {
        if (_accessory != null)
        {
            accessory1 = _accessory;
            Engine.e.charEquippedAccessory1[1] = _accessory;
        }
        //Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveEquippedChestArmor.GetComponent<InventorySlot>().item = _armor;
        //Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveEquippedChestArmor.GetComponentInChildren<TMP_Text>().text = _armor.itemName;
    }

    public void EquipMacAccessory2(Accessory _accessory)
    {
        // Resetting Stats Back to Base Values
        if (accessory2 != null)
        {
            physicalDefense -= accessory2.GetComponent<Accessory>().physicalArmor;
            fireDefense -= accessory2.GetComponent<Accessory>().fireDefense;
            waterDefense -= accessory2.GetComponent<Accessory>().waterDefense;
            lightningDropsLevel -= accessory2.GetComponent<Accessory>().lightningDefense;
            shadowDefense -= accessory2.GetComponent<Accessory>().shadowDefense;
            iceDefense -= accessory2.GetComponent<Accessory>().iceDefense;

            physicalDamage -= accessory2.GetComponent<Accessory>().physicalAttack;
            fireDropAttackBonus -= accessory2.GetComponent<Accessory>().fireAttackBonus;
            waterDropAttackBonus -= accessory2.GetComponent<Accessory>().waterAttackBonus;
            lightningDropAttackBonus -= accessory2.GetComponent<Accessory>().lightningAttackBonus;
            shadowDropAttackBonus -= accessory2.GetComponent<Accessory>().shadowAttackBonus;
            iceDropAttackBonus -= accessory2.GetComponent<Accessory>().iceAttackBonus;
            skillCostReduction -= accessory2.GetComponent<Accessory>().skillCostReduction;

            Engine.e.partyInventoryReference.AddItemToInventory(accessory2);
        }
        // Swap Inventory Slots

        Engine.e.partyInventoryReference.SubtractItemFromInventory(_accessory);


        // Equip Armor
        accessory2 = _accessory;
        Engine.e.charEquippedAccessory2[1] = _accessory;

        physicalDefense += accessory2.GetComponent<Accessory>().physicalArmor;
        fireDefense += accessory2.GetComponent<Accessory>().fireDefense;
        waterDefense += accessory2.GetComponent<Accessory>().waterDefense;
        lightningDefense += accessory2.GetComponent<Accessory>().lightningDefense;
        shadowDefense += accessory2.GetComponent<Accessory>().shadowDefense;
        iceDefense += accessory2.GetComponent<Accessory>().iceDefense;
        skillCostReduction += accessory2.GetComponent<Accessory>().skillCostReduction;

        physicalDamage += accessory2.GetComponent<Accessory>().physicalAttack;
        fireDropAttackBonus += accessory2.GetComponent<Accessory>().fireAttackBonus;
        waterDropAttackBonus += accessory2.GetComponent<Accessory>().waterAttackBonus;
        lightningDropAttackBonus += accessory2.GetComponent<Accessory>().lightningAttackBonus;
        shadowDropAttackBonus += accessory2.GetComponent<Accessory>().shadowAttackBonus;
        iceDropAttackBonus += accessory2.GetComponent<Accessory>().iceAttackBonus;

        // Update Stats (visually) and Return To Equip Screen
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayMacStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().armorLists[1].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetMacScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().accessory2InventorySet = false;

        Engine.e.partyInventoryReference.indexReference = -1;
    }

    public void EquipMacAccessory2OnLoad(Accessory _accessory)
    {
        if (_accessory != null)
        {
            accessory2 = _accessory;
            Engine.e.charEquippedAccessory2[1] = _accessory;
        }
        //Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveEquippedChestArmor.GetComponent<InventorySlot>().item = _armor;
        //Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveEquippedChestArmor.GetComponentInChildren<TMP_Text>().text = _armor.itemName;
    }
}