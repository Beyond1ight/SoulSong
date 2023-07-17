using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Field : Character
{


    public void RemoveWeaponRight()
    {
        strength -= weaponRight.GetComponent<Weapon>().strengthBonus;
        intelligence -= weaponRight.GetComponent<Weapon>().intelligenceBonus;

        firePhysicalAttackBonus -= weaponRight.GetComponent<Weapon>().fireAttack;
        waterPhysicalAttackBonus -= weaponRight.GetComponent<Weapon>().waterAttack;
        lightningPhysicalAttackBonus -= weaponRight.GetComponent<Weapon>().lightningAttack;
        shadowPhysicalAttackBonus -= weaponRight.GetComponent<Weapon>().shadowAttack;
        icePhysicalAttackBonus -= weaponRight.GetComponent<Weapon>().iceAttack;
        haste += weaponRight.GetComponent<Weapon>().weight;

        // Swap Inventory Slots
        Engine.e.partyInventoryReference.AddItemToInventory(weaponRight);

        weaponRight = null;
        Engine.e.charEquippedWeaponRight[2] = null;

        Engine.e.equipMenuReference.DisplayFieldStats();
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

        sleepDefense -= chestArmor.GetComponent<ChestArmor>().sleepDefense;
        poisonDefense -= chestArmor.GetComponent<ChestArmor>().poisonDefense;
        confuseDefense -= chestArmor.GetComponent<ChestArmor>().confuseDefense;
        deathDefense -= chestArmor.GetComponent<ChestArmor>().deathDefense;

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
        Engine.e.charEquippedChestArmor[2] = null;

        Engine.e.equipMenuReference.DisplayFieldStats();

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
        Engine.e.charEquippedLegArmor[2] = null;

        Engine.e.equipMenuReference.DisplayFieldStats();

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
        Engine.e.charEquippedAccessory1[2] = null;

        Engine.e.equipMenuReference.DisplayFieldStats();

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
        Engine.e.charEquippedAccessory2[2] = null;

        Engine.e.equipMenuReference.DisplayFieldStats();

    }

    public void EquipFieldWeaponRight(Weapon _weapon)
    {

        if (weaponRight != null)
        {
            // Resetting Stats To Base Values
            strength -= weaponRight.GetComponent<Weapon>().strengthBonus;
            intelligence -= weaponRight.GetComponent<Weapon>().intelligenceBonus;
            firePhysicalAttackBonus -= weaponRight.GetComponent<Weapon>().fireAttack;
            waterPhysicalAttackBonus -= weaponRight.GetComponent<Weapon>().waterAttack;
            lightningPhysicalAttackBonus -= weaponRight.GetComponent<Weapon>().lightningAttack;
            shadowPhysicalAttackBonus -= weaponRight.GetComponent<Weapon>().shadowAttack;
            icePhysicalAttackBonus -= weaponRight.GetComponent<Weapon>().iceAttack;

            haste += weaponRight.GetComponent<Weapon>().weight;

            Engine.e.partyInventoryReference.AddItemToInventory(weaponRight);
        }
        // Swap Inventory Slots
        Engine.e.partyInventoryReference.SubtractItemFromInventory(_weapon);

        // Equip Weapon
        weaponRight = _weapon;
        Engine.e.charEquippedWeaponRight[2] = _weapon;

        strength += _weapon.strengthBonus;
        intelligence += _weapon.intelligenceBonus;
        firePhysicalAttackBonus += _weapon.fireAttack;
        waterPhysicalAttackBonus += _weapon.waterAttack;
        lightningPhysicalAttackBonus += _weapon.lightningAttack;
        shadowPhysicalAttackBonus += _weapon.shadowAttack;
        icePhysicalAttackBonus += _weapon.iceAttack;

        haste -= weaponRight.GetComponent<Weapon>().weight;

        // Set Array Position For New (Equipped) Weapon
        // weapon.itemIndex = -1;

        // Update Stats (visually) and Return To Equip Screen
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayFieldStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().weaponLists[0].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetFieldScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().weaponRightInventorySet = false;

        Engine.e.partyInventoryReference.indexReference = -1;

    }

    public void EquipFieldWeaponRightOnLoad(Weapon _weapon)
    {

        weaponRight = _weapon;
        Engine.e.charEquippedWeaponRight[2] = _weapon;

    }

    public void EquipFieldChestArmor(ChestArmor _armor)
    {
        // Resetting Stats Back to Base Values
        physicalDefense -= chestArmor.GetComponent<ChestArmor>().physicalArmor;
        fireDefense -= chestArmor.GetComponent<ChestArmor>().fireDefense;
        waterDefense -= chestArmor.GetComponent<ChestArmor>().waterDefense;
        lightningDropsLevel -= chestArmor.GetComponent<ChestArmor>().lightningDefense;
        shadowDefense -= chestArmor.GetComponent<ChestArmor>().shadowDefense;
        iceDefense -= chestArmor.GetComponent<ChestArmor>().iceDefense;

        sleepDefense -= chestArmor.GetComponent<ChestArmor>().sleepDefense;
        poisonDefense -= chestArmor.GetComponent<ChestArmor>().poisonDefense;
        confuseDefense -= chestArmor.GetComponent<ChestArmor>().confuseDefense;
        deathDefense -= chestArmor.GetComponent<ChestArmor>().deathDefense;

        fireDropAttackBonus -= chestArmor.GetComponent<ChestArmor>().fireAttackBonus;
        waterDropAttackBonus -= chestArmor.GetComponent<ChestArmor>().waterAttackBonus;
        lightningDropAttackBonus -= chestArmor.GetComponent<ChestArmor>().lightningAttackBonus;
        shadowDropAttackBonus -= chestArmor.GetComponent<ChestArmor>().shadowAttackBonus;
        iceDropAttackBonus -= chestArmor.GetComponent<ChestArmor>().iceAttackBonus;
        skillCostReduction -= chestArmor.GetComponent<ChestArmor>().skillCostReduction;

        // Swap Inventory Slots
        Engine.e.partyInventoryReference.SubtractItemFromInventory(_armor);
        Engine.e.partyInventoryReference.AddItemToInventory(chestArmor);

        //     Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldEquippedChestArmor.GetComponent<InventorySlot>().item = _armor;
        //      Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldEquippedChestArmor.GetComponentInChildren<TMP_Text>().text = _armor.itemName;


        // Equip Armor
        chestArmor = _armor;
        Engine.e.charEquippedChestArmor[2] = _armor;

        physicalDefense += chestArmor.GetComponent<ChestArmor>().physicalArmor;
        fireDefense += chestArmor.GetComponent<ChestArmor>().fireDefense;
        waterDefense += chestArmor.GetComponent<ChestArmor>().waterDefense;
        lightningDefense += chestArmor.GetComponent<ChestArmor>().lightningDefense;
        shadowDefense += chestArmor.GetComponent<ChestArmor>().shadowDefense;
        iceDefense += chestArmor.GetComponent<ChestArmor>().iceDefense;
        skillCostReduction += chestArmor.GetComponent<ChestArmor>().skillCostReduction;

        sleepDefense += chestArmor.GetComponent<ChestArmor>().sleepDefense;
        poisonDefense += chestArmor.GetComponent<ChestArmor>().poisonDefense;
        confuseDefense += chestArmor.GetComponent<ChestArmor>().confuseDefense;
        deathDefense += chestArmor.GetComponent<ChestArmor>().deathDefense;

        fireDropAttackBonus += chestArmor.GetComponent<ChestArmor>().fireAttackBonus;
        waterDropAttackBonus += chestArmor.GetComponent<ChestArmor>().waterAttackBonus;
        lightningDropAttackBonus += chestArmor.GetComponent<ChestArmor>().lightningAttackBonus;
        shadowDropAttackBonus += chestArmor.GetComponent<ChestArmor>().shadowAttackBonus;
        iceDropAttackBonus += chestArmor.GetComponent<ChestArmor>().iceAttackBonus;

        // Update Stats (visually) and Return To Equip Screen
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayFieldStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().armorLists[0].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetFieldScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().legArmorInventorySet = false;


    }

    public void EquipFieldChestArmorOnLoad(ChestArmor _armor)
    {
        chestArmor = _armor;
        Engine.e.charEquippedChestArmor[2] = _armor;
        //    Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldEquippedChestArmor.GetComponent<InventorySlot>().item = _armor;
        //    Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldEquippedChestArmor.GetComponent<InventorySlot>().itemName.GetComponentInChildren<TMP_Text>().text = _armor.itemName;
    }

    public void EquipFieldLegArmor(LegArmor _armor)
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
        Engine.e.charEquippedLegArmor[2] = _armor;

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
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayFieldStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().armorLists[1].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetFieldScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().chestArmorInventorySet = false;
        Engine.e.partyInventoryReference.indexReference = -1;
    }

    public void EquipFieldLegArmorOnLoad(LegArmor _armor)
    {
        legArmor = _armor;
        Engine.e.charEquippedLegArmor[2] = _armor;
    }
    public void EquipFieldAccessory1(Accessory _accessory)
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
        Engine.e.charEquippedAccessory1[2] = _accessory;

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
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayFieldStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().armorLists[1].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetFieldScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().accessory1InventorySet = false;

        Engine.e.partyInventoryReference.indexReference = -1;
    }
    public void EquipFieldAccessory1OnLoad(Accessory _accessory)
    {
        if (_accessory != null)
        {
            accessory1 = _accessory;
            Engine.e.charEquippedAccessory1[2] = _accessory;
        }
        //Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveEquippedChestArmor.GetComponent<InventorySlot>().item = _armor;
        //Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveEquippedChestArmor.GetComponentInChildren<TMP_Text>().text = _armor.itemName;
    }

    public void EquipFieldAccessory2(Accessory _accessory)
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
        Engine.e.charEquippedAccessory2[2] = _accessory;

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
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayFieldStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().armorLists[1].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetFieldScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().accessory2InventorySet = false;

        Engine.e.partyInventoryReference.indexReference = -1;
    }

    public void EquipFieldAccessory2OnLoad(Accessory _accessory)
    {
        if (_accessory != null)
        {
            accessory2 = _accessory;
            Engine.e.charEquippedAccessory2[2] = _accessory;
        }
        //Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveEquippedChestArmor.GetComponent<InventorySlot>().item = _armor;
        //Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveEquippedChestArmor.GetComponentInChildren<TMP_Text>().text = _armor.itemName;
    }
}


