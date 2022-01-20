using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

//[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class Item : MonoBehaviour
{
    public Drops drop;
    public string itemName;
    public string itemDescription;
    public string itemType;
    public int itemValue;
    public int resaleValue;
    public int numberHeld;
    public int itemPower;
    public bool stackable;
    public int itemIndex;
    public float dropCostReduction;
    public float skillCostReduction;
    public AnimationClip animationClip;

    public bool consumable, isDrop, weapon, chestArmor, useableOutOfBattle;

    public void UseItemCheck()
    {

        if (!Engine.e.inBattle)
        {
            Engine.e.itemToBeUsed = this;

            for (int i = 0; i < Engine.e.itemMenuPanels.Length; i++)
            {
                if (Engine.e.party[i] != null)
                {
                    Engine.e.itemConfirmUseButtons[i].SetActive(true);
                }
            }
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(Engine.e.itemMenuCharFirst);

            Engine.e.partyInventoryReference.inventoryScreenSet = false;
        }
        else
        {
            if (Engine.e.battleSystem.state == BattleState.CHAR1TURN)
            {
                Engine.e.battleSystem.char1ItemToBeUsed = this;
                Engine.e.battleSystem.char1UsingItem = true;
                Engine.e.battleSystem.char1Supporting = true;
            }
            if (Engine.e.battleSystem.state == BattleState.CHAR2TURN)
            {
                Engine.e.battleSystem.char2ItemToBeUsed = this;
                Engine.e.battleSystem.char2UsingItem = true;
                Engine.e.battleSystem.char2Supporting = true;

            }
            if (Engine.e.battleSystem.state == BattleState.CHAR3TURN)
            {
                Engine.e.battleSystem.char3ItemToBeUsed = this;
                Engine.e.battleSystem.char3UsingItem = true;
                Engine.e.battleSystem.char3Supporting = true;

            }

            Engine.e.battleSystem.battleItemMenu.SetActive(false);
            Engine.e.partyInventoryReference.battleScreenInventorySet = false;
            Engine.e.battleSystem.ActivateTargetButtons();

        }
    }

    public void OnButtonHeal(Item item)
    {
        if (Engine.e.party[0].GetComponent<Character>().currentHealth == Engine.e.party[0].GetComponent<Character>().maxHealth)
        {
            Debug.Log("Can't use item. Already at full health!");
        }
        else
        {
            Engine.e.party[0].GetComponent<Character>().currentHealth += item.itemValue;

            if (Engine.e.party[0].GetComponent<Character>().currentHealth > Engine.e.party[0].GetComponent<Character>().maxHealth)
            {
                Engine.e.party[0].GetComponent<Character>().currentHealth = Engine.e.party[0].GetComponent<Character>().maxHealth;
            }

            Engine.e.partyInventoryReference.SubtractItemFromInventory(item.GetComponent<Item>());
        }
    }

    public void OnButtonGiveMana(Item item)
    {
        if (Engine.e.party[0].GetComponent<Character>().currentMana == Engine.e.party[0].GetComponent<Character>().maxMana)
        {
            Debug.Log("Can't use item. Already at full mana!");
        }
        else
        {
            Engine.e.party[0].GetComponent<Character>().currentMana += item.itemValue;

            if (Engine.e.party[0].GetComponent<Character>().currentMana > Engine.e.party[0].GetComponent<Character>().maxMana)
            {
                Engine.e.party[0].GetComponent<Character>().currentMana = Engine.e.party[0].GetComponent<Character>().maxMana;
            }

            Engine.e.partyInventoryReference.SubtractItemFromInventory(item.GetComponent<Item>());
        }
    }

    public void BattleHelpText()
    {
        Engine.e.battleHelp.text = itemDescription;
    }
    public void ClearBattleHelpText()
    {
        Engine.e.battleHelp.text = string.Empty;
    }
}


