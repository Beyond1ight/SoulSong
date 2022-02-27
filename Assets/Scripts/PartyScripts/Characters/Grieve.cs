using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Grieve : Character
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
        Engine.e.charEquippedWeaponRight[0] = null;

        Engine.e.equipMenuReference.DisplayGrieveStats();
    }

    public void RemoveWeaponLeft()
    {
        strength -= weaponLeft.GetComponent<Weapon>().strengthBonus;
        intelligence -= weaponLeft.GetComponent<Weapon>().intelligenceBonus;

        firePhysicalAttackBonus -= weaponLeft.GetComponent<Weapon>().fireAttack;
        waterPhysicalAttackBonus -= weaponLeft.GetComponent<Weapon>().waterAttack;
        lightningPhysicalAttackBonus -= weaponLeft.GetComponent<Weapon>().lightningAttack;
        shadowPhysicalAttackBonus -= weaponLeft.GetComponent<Weapon>().shadowAttack;
        icePhysicalAttackBonus -= weaponLeft.GetComponent<Weapon>().iceAttack;
        haste += weaponLeft.GetComponent<Weapon>().weight;

        // Swap Inventory Slots
        Engine.e.partyInventoryReference.AddItemToInventory(weaponLeft);

        weaponLeft = null;
        Engine.e.charEquippedWeaponLeft[0] = null;

        Engine.e.equipMenuReference.DisplayGrieveStats();
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
        Engine.e.charEquippedChestArmor[0] = null;

        Engine.e.equipMenuReference.DisplayGrieveStats();

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
        Engine.e.charEquippedLegArmor[0] = null;

        Engine.e.equipMenuReference.DisplayGrieveStats();

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
        Engine.e.charEquippedAccessory1[0] = null;

        Engine.e.equipMenuReference.DisplayGrieveStats();

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
        Engine.e.charEquippedAccessory2[0] = null;

        Engine.e.equipMenuReference.DisplayGrieveStats();

    }

    public void EquipGrieveWeaponRight(Weapon _weapon)
    {
        if (_weapon.twoHand && !canUse2HWeapon || _weapon.offHand && !_weapon.mainHand)
        {
            return;
        }
        else
        {
            if (weaponLeft != null && _weapon.twoHand && canUse2HWeapon)
            {
                RemoveWeaponLeft();
            }
        }

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
        Engine.e.charEquippedWeaponRight[0] = _weapon;

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
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayGrieveStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().weaponLists[0].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetGrieveScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().weaponRightInventorySet = false;

        Engine.e.partyInventoryReference.indexReference = -1;

    }
    public void EquipGrieveWeaponLeft(Weapon _weapon)
    {
        if (_weapon.twoHand || !_weapon.offHand && _weapon.mainHand || _weapon.mainHand && _weapon.offHand && !canDualWield)
        {
            return;
        }
        else
        {
            if (weaponLeft != null)
            {
                // Resetting Stats To Base Values
                strength -= weaponLeft.GetComponent<Weapon>().strengthBonus;
                intelligence -= weaponLeft.GetComponent<Weapon>().intelligenceBonus;
                firePhysicalAttackBonus -= weaponLeft.GetComponent<Weapon>().fireAttack;
                waterPhysicalAttackBonus -= weaponLeft.GetComponent<Weapon>().waterAttack;
                lightningPhysicalAttackBonus -= weaponLeft.GetComponent<Weapon>().lightningAttack;
                shadowPhysicalAttackBonus -= weaponLeft.GetComponent<Weapon>().shadowAttack;
                icePhysicalAttackBonus -= weaponLeft.GetComponent<Weapon>().iceAttack;

                haste += weaponLeft.GetComponent<Weapon>().weight;

                Engine.e.partyInventoryReference.AddItemToInventory(weaponLeft);
            }
            // Swap Inventory Slots
            Engine.e.partyInventoryReference.SubtractItemFromInventory(_weapon);

            // Equip Weapon
            weaponLeft = _weapon;
            Engine.e.charEquippedWeaponLeft[0] = _weapon;

            strength += _weapon.strengthBonus;
            intelligence += _weapon.intelligenceBonus;
            firePhysicalAttackBonus += _weapon.fireAttack;
            waterPhysicalAttackBonus += _weapon.waterAttack;
            lightningPhysicalAttackBonus += _weapon.lightningAttack;
            shadowPhysicalAttackBonus += _weapon.shadowAttack;
            icePhysicalAttackBonus += _weapon.iceAttack;

            haste -= weaponLeft.GetComponent<Weapon>().weight;

            // Set Array Position For New (Equipped) Weapon
            // weapon.itemIndex = -1;

            // Update Stats (visually) and Return To Equip Screen
            Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayGrieveStats();
            Engine.e.equipMenuReference.GetComponent<EquipDisplay>().weaponLists[0].SetActive(false);
            Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetGrieveScreen();
            Engine.e.equipMenuReference.GetComponent<EquipDisplay>().weaponLeftInventorySet = false;

            Engine.e.partyInventoryReference.indexReference = -1;

        }
    }

    public void EquipGrieveWeaponRightOnLoad(Weapon _weapon)
    {

        weaponRight = _weapon;
        Engine.e.charEquippedWeaponRight[0] = _weapon;

    }
    public void EquipGrieveWeaponLeftOnLoad(Weapon _weapon)
    {

        weaponLeft = _weapon;
        Engine.e.charEquippedWeaponLeft[0] = _weapon;

    }

    public void EquipGrieveChestArmor(ChestArmor _armor)
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

        // Equip Armor
        chestArmor = _armor;
        Engine.e.charEquippedChestArmor[0] = _armor;

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
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayGrieveStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().armorLists[0].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetGrieveScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().chestArmorInventorySet = false;
        Engine.e.partyInventoryReference.indexReference = -1;
    }

    public void EquipGrieveChestArmorOnLoad(ChestArmor _armor)
    {
        chestArmor = _armor;
        Engine.e.charEquippedChestArmor[0] = _armor;
    }

    public void EquipGrieveLegArmor(LegArmor _armor)
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
        Engine.e.charEquippedLegArmor[0] = _armor;

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
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayGrieveStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().armorLists[1].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetGrieveScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().legArmorInventorySet = false;
        Engine.e.partyInventoryReference.indexReference = -1;
    }

    public void EquipGrieveLegArmorOnLoad(LegArmor _armor)
    {
        legArmor = _armor;
        Engine.e.charEquippedLegArmor[0] = _armor;
    }

    public void EquipGrieveAccessory1(Accessory _accessory)
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
        Engine.e.charEquippedAccessory1[0] = _accessory;

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
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayGrieveStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().armorLists[1].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetGrieveScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().accessory1InventorySet = false;

        Engine.e.partyInventoryReference.indexReference = -1;
    }

    public void EquipGrieveAccessory1OnLoad(Accessory _accessory)
    {
        if (_accessory != null)
        {
            accessory1 = _accessory;
            Engine.e.charEquippedAccessory1[0] = _accessory;
        }
        //Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveEquippedChestArmor.GetComponent<InventorySlot>().item = _armor;
        //Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveEquippedChestArmor.GetComponentInChildren<TMP_Text>().text = _armor.itemName;
    }

    public void EquipGrieveAccessory2(Accessory _accessory)
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
        Engine.e.charEquippedAccessory2[0] = _accessory;

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
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayGrieveStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().armorLists[1].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetGrieveScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().accessory2InventorySet = false;

        Engine.e.partyInventoryReference.indexReference = -1;
    }

    public void EquipGrieveAccessory2OnLoad(Accessory _accessory)
    {
        if (_accessory != null)
        {
            accessory2 = _accessory;
            Engine.e.charEquippedAccessory2[0] = _accessory;
        }
        //Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveEquippedChestArmor.GetComponent<InventorySlot>().item = _armor;
        //Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveEquippedChestArmor.GetComponentInChildren<TMP_Text>().text = _armor.itemName;
    }
}
