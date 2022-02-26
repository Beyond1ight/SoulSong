using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Riggs : Character
{
    public void RemoveWeapon()
    {
        strength -= weapon.GetComponent<Weapon>().strengthBonus;
        intelligence -= weapon.GetComponent<Weapon>().intelligenceBonus;

        firePhysicalAttackBonus -= weapon.GetComponent<Weapon>().fireAttack;
        waterPhysicalAttackBonus -= weapon.GetComponent<Weapon>().waterAttack;
        lightningPhysicalAttackBonus -= weapon.GetComponent<Weapon>().lightningAttack;
        shadowPhysicalAttackBonus -= weapon.GetComponent<Weapon>().shadowAttack;
        icePhysicalAttackBonus -= weapon.GetComponent<Weapon>().iceAttack;

        // Swap Inventory Slots
        Engine.e.partyInventoryReference.AddItemToInventory(weapon);

        weapon = null;
        Engine.e.charEquippedWeapons[3] = null;

        Engine.e.equipMenuReference.DisplayRiggsStats();
    }
    public void RemoveChestArmor()
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
        Engine.e.partyInventoryReference.AddItemToInventory(chestArmor);

        // Equip Armor
        chestArmor = null;
        Engine.e.charEquippedChestArmor[3] = null;

        Engine.e.equipMenuReference.DisplayRiggsStats();

    }
    public void RemoveLegArmor()
    {
        // Resetting Stats Back to Base Values
        physicalDefense -= chestArmor.GetComponent<LegArmor>().physicalArmor;
        fireDefense -= chestArmor.GetComponent<LegArmor>().fireDefense;
        waterDefense -= chestArmor.GetComponent<LegArmor>().waterDefense;
        lightningDropsLevel -= chestArmor.GetComponent<LegArmor>().lightningDefense;
        shadowDefense -= chestArmor.GetComponent<LegArmor>().shadowDefense;
        iceDefense -= chestArmor.GetComponent<LegArmor>().iceDefense;

        fireDropAttackBonus -= chestArmor.GetComponent<LegArmor>().fireAttackBonus;
        waterDropAttackBonus -= chestArmor.GetComponent<LegArmor>().waterAttackBonus;
        lightningDropAttackBonus -= chestArmor.GetComponent<LegArmor>().lightningAttackBonus;
        shadowDropAttackBonus -= chestArmor.GetComponent<LegArmor>().shadowAttackBonus;
        iceDropAttackBonus -= chestArmor.GetComponent<LegArmor>().iceAttackBonus;
        skillCostReduction -= chestArmor.GetComponent<LegArmor>().skillCostReduction;

        // Swap Inventory Slots
        Engine.e.partyInventoryReference.AddItemToInventory(legArmor);

        // Equip Armor
        legArmor = null;
        Engine.e.charEquippedLegArmor[3] = null;

        Engine.e.equipMenuReference.DisplayRiggsStats();

    }
    public void RemoveAccessory1()
    {
        // Resetting Stats Back to Base Values
        physicalDefense -= accessory1.GetComponent<Accessory>().physicalArmor;
        fireDefense -= accessory1.GetComponent<Accessory>().fireDefense;
        waterDefense -= accessory1.GetComponent<Accessory>().waterDefense;
        lightningDropsLevel -= accessory1.GetComponent<Accessory>().lightningDefense;
        shadowDefense -= accessory1.GetComponent<Accessory>().shadowDefense;
        iceDefense -= accessory1.GetComponent<Accessory>().iceDefense;

        strength -= accessory1.GetComponent<Accessory>().strengthBonus;
        intelligence -= accessory1.GetComponent<Accessory>().intelligenceBonus;

        fireDropAttackBonus -= accessory1.GetComponent<Accessory>().fireAttackBonus;
        waterDropAttackBonus -= accessory1.GetComponent<Accessory>().waterAttackBonus;
        lightningDropAttackBonus -= accessory1.GetComponent<Accessory>().lightningAttackBonus;
        shadowDropAttackBonus -= accessory1.GetComponent<Accessory>().shadowAttackBonus;
        iceDropAttackBonus -= accessory1.GetComponent<Accessory>().iceAttackBonus;
        skillCostReduction -= accessory1.GetComponent<Accessory>().skillCostReduction;

        // Swap Inventory Slots

        Engine.e.partyInventoryReference.AddItemToInventory(accessory1);

        accessory1 = null;
        Engine.e.charEquippedAccessory1[3] = null;

        Engine.e.equipMenuReference.DisplayRiggsStats();

    }

    public void RemoveAccessory2()
    {
        // Resetting Stats Back to Base Values
        physicalDefense -= accessory2.GetComponent<Accessory>().physicalArmor;
        fireDefense -= accessory2.GetComponent<Accessory>().fireDefense;
        waterDefense -= accessory2.GetComponent<Accessory>().waterDefense;
        lightningDropsLevel -= accessory2.GetComponent<Accessory>().lightningDefense;
        shadowDefense -= accessory2.GetComponent<Accessory>().shadowDefense;
        iceDefense -= accessory2.GetComponent<Accessory>().iceDefense;

        strength -= accessory2.GetComponent<Accessory>().strengthBonus;
        intelligence -= accessory2.GetComponent<Accessory>().intelligenceBonus;

        fireDropAttackBonus -= accessory2.GetComponent<Accessory>().fireAttackBonus;
        waterDropAttackBonus -= accessory2.GetComponent<Accessory>().waterAttackBonus;
        lightningDropAttackBonus -= accessory2.GetComponent<Accessory>().lightningAttackBonus;
        shadowDropAttackBonus -= accessory2.GetComponent<Accessory>().shadowAttackBonus;
        iceDropAttackBonus -= accessory2.GetComponent<Accessory>().iceAttackBonus;
        skillCostReduction -= accessory2.GetComponent<Accessory>().skillCostReduction;

        // Swap Inventory Slots

        Engine.e.partyInventoryReference.AddItemToInventory(accessory2);

        accessory2 = null;
        Engine.e.charEquippedAccessory2[3] = null;

        Engine.e.equipMenuReference.DisplayRiggsStats();

    }

    public void EquipRiggsWeapon(Weapon _weapon)
    {
        if (weapon != null)
        {
            // Resetting Stats To Base Values
            strength -= weapon.GetComponent<Weapon>().strengthBonus;
            intelligence -= weapon.GetComponent<Weapon>().intelligenceBonus;

            firePhysicalAttackBonus -= weapon.GetComponent<Weapon>().fireAttack;
            waterPhysicalAttackBonus -= weapon.GetComponent<Weapon>().waterAttack;
            lightningPhysicalAttackBonus -= weapon.GetComponent<Weapon>().lightningAttack;
            shadowPhysicalAttackBonus -= weapon.GetComponent<Weapon>().shadowAttack;
            icePhysicalAttackBonus -= weapon.GetComponent<Weapon>().iceAttack;

            // Swap Inventory Slots
            Engine.e.partyInventoryReference.AddItemToInventory(weapon);
        }
        Engine.e.partyInventoryReference.SubtractItemFromInventory(_weapon);
        //       Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsEquippedWeapon.GetComponent<InventorySlot>().item = _weapon;
        //        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsEquippedWeapon.GetComponentInChildren<TMP_Text>().text = _weapon.itemName;


        // Equip Weapon
        weapon = _weapon;
        Engine.e.charEquippedWeapons[3] = _weapon;

        strength += _weapon.strengthBonus;
        intelligence += _weapon.intelligenceBonus;

        firePhysicalAttackBonus += _weapon.fireAttack;
        waterPhysicalAttackBonus += _weapon.waterAttack;
        lightningPhysicalAttackBonus += _weapon.lightningAttack;
        shadowPhysicalAttackBonus += _weapon.shadowAttack;
        icePhysicalAttackBonus += _weapon.iceAttack;

        // Set Array Position For New (Equipped) Weapon
        weapon.itemIndex = -1;

        // Update Stats (visually) and Return To Equip Screen
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayRiggsStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().weaponLists[0].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetRiggsScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().weaponInventorySet = false;

    }

    public void EquipRiggsWeaponOnLoad(Weapon _weapon)
    {
        weapon = _weapon;
        Engine.e.charEquippedWeapons[3] = _weapon;
        //      Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsEquippedWeapon.GetComponent<InventorySlot>().item = _weapon;
        //      Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsEquippedWeapon.GetComponentInChildren<TMP_Text>().text = _weapon.itemName;
    }
    public void EquipRiggsChestArmor(ChestArmor _armor)
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

        //       Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsEquippedChestArmor.GetComponent<InventorySlot>().item = _armor;
        //      Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsEquippedChestArmor.GetComponentInChildren<TMP_Text>().text = _armor.itemName;


        // Equip Armor
        chestArmor = _armor;
        Engine.e.charEquippedChestArmor[3] = _armor;

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
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayRiggsStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().armorLists[0].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetRiggsScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().chestArmorInventorySet = false;


    }
    public void EquipRiggsChestArmorOnLoad(ChestArmor _armor)
    {
        chestArmor = _armor;
        Engine.e.charEquippedChestArmor[3] = _armor;
        //     Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsEquippedChestArmor.GetComponent<InventorySlot>().item = _armor;
        //      Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsEquippedChestArmor.GetComponentInChildren<TMP_Text>().text = _armor.itemName;
    }
    public void EquipRiggsLegArmor(LegArmor _armor)
    {
        // Resetting Stats Back to Base Values
        physicalDefense -= legArmor.GetComponent<LegArmor>().physicalArmor;
        fireDefense -= legArmor.GetComponent<LegArmor>().fireDefense;
        waterDefense -= legArmor.GetComponent<LegArmor>().waterDefense;
        lightningDropsLevel -= legArmor.GetComponent<LegArmor>().lightningDefense;
        shadowDefense -= legArmor.GetComponent<LegArmor>().shadowDefense;
        iceDefense -= legArmor.GetComponent<LegArmor>().iceDefense;

        fireDropAttackBonus -= legArmor.GetComponent<LegArmor>().fireAttackBonus;
        waterDropAttackBonus -= legArmor.GetComponent<LegArmor>().waterAttackBonus;
        lightningDropAttackBonus -= legArmor.GetComponent<LegArmor>().lightningAttackBonus;
        shadowDropAttackBonus -= legArmor.GetComponent<LegArmor>().shadowAttackBonus;
        iceDropAttackBonus -= legArmor.GetComponent<LegArmor>().iceAttackBonus;
        skillCostReduction -= legArmor.GetComponent<LegArmor>().skillCostReduction;

        // Swap Inventory Slots

        Engine.e.partyInventoryReference.SubtractItemFromInventory(_armor);
        Engine.e.partyInventoryReference.AddItemToInventory(legArmor);

        // Equip Armor
        legArmor = _armor;
        Engine.e.charEquippedLegArmor[3] = _armor;

        physicalDefense += legArmor.GetComponent<LegArmor>().physicalArmor;
        fireDefense += legArmor.GetComponent<LegArmor>().fireDefense;
        waterDefense += legArmor.GetComponent<LegArmor>().waterDefense;
        lightningDefense += legArmor.GetComponent<LegArmor>().lightningDefense;
        shadowDefense += legArmor.GetComponent<LegArmor>().shadowDefense;
        iceDefense += legArmor.GetComponent<LegArmor>().iceDefense;
        skillCostReduction += legArmor.GetComponent<LegArmor>().skillCostReduction;

        fireDropAttackBonus += legArmor.GetComponent<LegArmor>().fireAttackBonus;
        waterDropAttackBonus += legArmor.GetComponent<LegArmor>().waterAttackBonus;
        lightningDropAttackBonus += legArmor.GetComponent<LegArmor>().lightningAttackBonus;
        shadowDropAttackBonus += legArmor.GetComponent<LegArmor>().shadowAttackBonus;
        iceDropAttackBonus += legArmor.GetComponent<LegArmor>().iceAttackBonus;

        // Update Stats (visually) and Return To Equip Screen
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayRiggsStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().armorLists[1].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetRiggsScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().legArmorInventorySet = false;
        Engine.e.partyInventoryReference.indexReference = -1;
    }

    public void EquipRiggsLegArmorOnLoad(LegArmor _armor)
    {
        legArmor = _armor;
        Engine.e.charEquippedLegArmor[3] = _armor;
    }
    public void EquipRiggsAccessory1(Accessory _accessory)
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

            strength -= accessory1.GetComponent<Accessory>().strengthBonus;
            intelligence -= accessory1.GetComponent<Accessory>().intelligenceBonus;

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
        Engine.e.charEquippedAccessory1[3] = _accessory;

        physicalDefense += accessory1.GetComponent<Accessory>().physicalArmor;
        fireDefense += accessory1.GetComponent<Accessory>().fireDefense;
        waterDefense += accessory1.GetComponent<Accessory>().waterDefense;
        lightningDefense += accessory1.GetComponent<Accessory>().lightningDefense;
        shadowDefense += accessory1.GetComponent<Accessory>().shadowDefense;
        iceDefense += accessory1.GetComponent<Accessory>().iceDefense;
        skillCostReduction += accessory1.GetComponent<Accessory>().skillCostReduction;

        strength += accessory1.GetComponent<Accessory>().strengthBonus;
        intelligence += accessory1.GetComponent<Accessory>().intelligenceBonus;

        fireDropAttackBonus += accessory1.GetComponent<Accessory>().fireAttackBonus;
        waterDropAttackBonus += accessory1.GetComponent<Accessory>().waterAttackBonus;
        lightningDropAttackBonus += accessory1.GetComponent<Accessory>().lightningAttackBonus;
        shadowDropAttackBonus += accessory1.GetComponent<Accessory>().shadowAttackBonus;
        iceDropAttackBonus += accessory1.GetComponent<Accessory>().iceAttackBonus;

        // Update Stats (visually) and Return To Equip Screen
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayRiggsStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().armorLists[1].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetRiggsScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().accessory1InventorySet = false;

        Engine.e.partyInventoryReference.indexReference = -1;
    }

    public void EquipRiggsAccessory1OnLoad(Accessory _accessory)
    {
        if (_accessory != null)
        {
            accessory1 = _accessory;
            Engine.e.charEquippedAccessory1[3] = _accessory;
        }
        //Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveEquippedChestArmor.GetComponent<InventorySlot>().item = _armor;
        //Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveEquippedChestArmor.GetComponentInChildren<TMP_Text>().text = _armor.itemName;
    }

    public void EquipRiggsAccessory2(Accessory _accessory)
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

            strength -= accessory2.GetComponent<Accessory>().strengthBonus;
            intelligence -= accessory2.GetComponent<Accessory>().intelligenceBonus;

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
        Engine.e.charEquippedAccessory2[3] = _accessory;

        physicalDefense += accessory2.GetComponent<Accessory>().physicalArmor;
        fireDefense += accessory2.GetComponent<Accessory>().fireDefense;
        waterDefense += accessory2.GetComponent<Accessory>().waterDefense;
        lightningDefense += accessory2.GetComponent<Accessory>().lightningDefense;
        shadowDefense += accessory2.GetComponent<Accessory>().shadowDefense;
        iceDefense += accessory2.GetComponent<Accessory>().iceDefense;
        skillCostReduction += accessory2.GetComponent<Accessory>().skillCostReduction;

        strength += accessory2.GetComponent<Accessory>().strengthBonus;
        intelligence += accessory2.GetComponent<Accessory>().intelligenceBonus;

        fireDropAttackBonus += accessory2.GetComponent<Accessory>().fireAttackBonus;
        waterDropAttackBonus += accessory2.GetComponent<Accessory>().waterAttackBonus;
        lightningDropAttackBonus += accessory2.GetComponent<Accessory>().lightningAttackBonus;
        shadowDropAttackBonus += accessory2.GetComponent<Accessory>().shadowAttackBonus;
        iceDropAttackBonus += accessory2.GetComponent<Accessory>().iceAttackBonus;

        // Update Stats (visually) and Return To Equip Screen
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayRiggsStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().armorLists[1].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetRiggsScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().accessory2InventorySet = false;

        Engine.e.partyInventoryReference.indexReference = -1;
    }

    public void EquipRiggsAccessory2OnLoad(Accessory _accessory)
    {
        if (_accessory != null)
        {
            accessory2 = _accessory;
            Engine.e.charEquippedAccessory2[3] = _accessory;
        }
        //Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveEquippedChestArmor.GetComponent<InventorySlot>().item = _armor;
        //Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveEquippedChestArmor.GetComponentInChildren<TMP_Text>().text = _armor.itemName;
    }
}
