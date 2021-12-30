using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Field : Character
{


    public void EquipFieldWeapon(FieldWeapons _weapon)
    {

        // Resetting Stats To Base Values
        physicalDamage -= weapon.GetComponent<FieldWeapons>().physicalAttack;
        firePhysicalAttackBonus -= weapon.GetComponent<FieldWeapons>().fireAttack;
        waterPhysicalAttackBonus -= weapon.GetComponent<FieldWeapons>().waterAttack;
        lightningPhysicalAttackBonus -= weapon.GetComponent<FieldWeapons>().lightningAttack;
        shadowPhysicalAttackBonus -= weapon.GetComponent<FieldWeapons>().shadowAttack;
        icePhysicalAttackBonus -= weapon.GetComponent<FieldWeapons>().iceAttack;

        // Swap Inventory Slots
        Engine.e.partyInventoryReference.SubtractItemFromInventory(_weapon);
        Engine.e.partyInventoryReference.AddItemToInventory(weapon);

        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldEquippedWeapon.GetComponent<InventorySlot>().item = _weapon;
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldEquippedWeapon.GetComponentInChildren<TMP_Text>().text = _weapon.itemName;


        // Equip Weapon
        weapon = _weapon;
        Engine.e.charEquippedWeapons[2] = _weapon;

        physicalDamage += _weapon.physicalAttack;
        firePhysicalAttackBonus += _weapon.fireAttack;
        waterPhysicalAttackBonus += _weapon.waterAttack;
        lightningPhysicalAttackBonus += _weapon.lightningAttack;
        shadowPhysicalAttackBonus += _weapon.shadowAttack;
        icePhysicalAttackBonus += _weapon.iceAttack;

        // Set Array Position For New (Equipped) Weapon
        weapon.itemIndex = -1;

        // Update Stats (visually) and Return To Equip Screen
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayFieldStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().weaponLists[2].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetFieldScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldWeaponInventorySet = false;

    }

    public void EquipFieldWeaponOnLoad(FieldWeapons _weapon)
    {
        weapon = _weapon;
        Engine.e.charEquippedWeapons[2] = _weapon;
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldEquippedWeapon.GetComponent<InventorySlot>().item = _weapon;
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldEquippedWeapon.GetComponentInChildren<TMP_Text>().text = _weapon.itemName;
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

        fireDropAttackBonus -= chestArmor.GetComponent<ChestArmor>().fireAttackBonus;
        waterDropAttackBonus -= chestArmor.GetComponent<ChestArmor>().waterAttackBonus;
        lightningDropAttackBonus -= chestArmor.GetComponent<ChestArmor>().lightningAttackBonus;
        shadowDropAttackBonus -= chestArmor.GetComponent<ChestArmor>().shadowAttackBonus;
        iceDropAttackBonus -= chestArmor.GetComponent<ChestArmor>().iceAttackBonus;
        skillCostReduction -= chestArmor.GetComponent<ChestArmor>().skillCostReduction;

        // Swap Inventory Slots
        Engine.e.partyInventoryReference.SubtractItemFromInventory(_armor);
        Engine.e.partyInventoryReference.AddItemToInventory(chestArmor);

        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldEquippedChestArmor.GetComponent<InventorySlot>().item = _armor;
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldEquippedChestArmor.GetComponentInChildren<TMP_Text>().text = _armor.itemName;


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

        fireDropAttackBonus += chestArmor.GetComponent<ChestArmor>().fireAttackBonus;
        waterDropAttackBonus += chestArmor.GetComponent<ChestArmor>().waterAttackBonus;
        lightningDropAttackBonus += chestArmor.GetComponent<ChestArmor>().lightningAttackBonus;
        shadowDropAttackBonus += chestArmor.GetComponent<ChestArmor>().shadowAttackBonus;
        iceDropAttackBonus += chestArmor.GetComponent<ChestArmor>().iceAttackBonus;

        // Update Stats (visually) and Return To Equip Screen
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().DisplayFieldStats();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().armorLists[0].SetActive(false);
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().SetFieldScreen();
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().chestArmorInventorySet = false;


    }

    public void EquipFieldChestArmorOnLoad(ChestArmor _armor)
    {
        chestArmor = _armor;
        Engine.e.charEquippedChestArmor[2] = _armor;
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldEquippedChestArmor.GetComponent<InventorySlot>().item = _armor;
        Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldEquippedChestArmor.GetComponent<InventorySlot>().itemName.GetComponentInChildren<TMP_Text>().text = _armor.itemName;
    }

    public void ActivateFieldDropsUI()
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
                //  if (fireDrops[i] != null)
                {
                    //      Engine.e.battleSystem.dropsButtons[fireIndex].GetComponentInChildren<TextMeshProUGUI>().text = fireDrops[i].dropName;
                    fireIndex++;
                }
            }


            // Ice Drops
            if (canUseIceDrops == true)
            {
                // if (iceDrops[i] != null)
                {
                    //      Engine.e.battleSystem.dropsButtons[iceIndex].GetComponentInChildren<TextMeshProUGUI>().text = iceDrops[i].dropName;
                    iceIndex++;
                }
            }


            // Lightning Drops
            if (canUseLightningDrops == true)
            {
                // if (lightningDrops[i] != null)
                {
                    //     Engine.e.battleSystem.dropsButtons[lightningIndex].GetComponentInChildren<TextMeshProUGUI>().text = lightningDrops[i].dropName;
                    lightningIndex++;
                }
            }


            // Water Drops
            if (canUseWaterDrops == true)
            {
                //if (waterDrops[i] != null)
                {
                    //      Engine.e.battleSystem.dropsButtons[waterIndex].GetComponentInChildren<TextMeshProUGUI>().text = waterDrops[i].dropName;
                    waterIndex++;
                }
            }


            // Shadow Drops
            if (canUseShadowDrops == true)
            {

                // if (shadowDrops[i] != null)
                {
                    //    Engine.e.battleSystem.dropsButtons[shadowIndex].GetComponentInChildren<TextMeshProUGUI>().text = shadowDrops[i].dropName;
                    shadowIndex++;
                }
            }


            // Holy Drops
            if (canUseHolyDrops == true)
            {

                // if (holyDrops[i] != null)
                {
                    //     Engine.e.battleSystem.dropsButtons[holyIndex].GetComponentInChildren<TextMeshProUGUI>().text = holyDrops[i].dropName;
                    holyIndex++;
                }
            }
        }
    }

    public void ActivateFieldSkillsUI()
    {
        for (int i = 0; i < Engine.e.gameSkills.Length; i++)
        {
            if (skills[i] != null)
            {
                Engine.e.battleSystem.skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = skills[i].skillName;
                Engine.e.battleSystem.skillButtons[i].GetComponent<Button>().interactable = true;

            }
        }
    }
}


