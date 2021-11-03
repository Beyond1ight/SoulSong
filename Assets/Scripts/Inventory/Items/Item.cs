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
    public static Item item;
    public string itemName;
    public string itemDescription;
    public string itemType;
    public int itemValue;
    public int resaleValue;
    public int numberHeld;
    public int itemPower;
    public TextMeshProUGUI numberHeldDisplay;
    public bool stackable;
    public GameObject inventoryButton;
    public GameObject inventoryButtonLogic;
    public int itemIndex;
    public Transform inventoryButtonContainer;
    public GameObject anim;
    public bool isDrop;
    public float dropCostReduction;
    public float skillCostReduction;

    public bool consumable, drop, weapon, chestArmor;

    public void ItemSetup(GameObject item)
    {

        inventoryButtonContainer = Engine.e.itemDisplay;

    }

    public void UseItemCheck()
    {


        Engine.e.itemToBeUsed = this;

        if (!Engine.e.inBattle)
        {
            if (!isDrop)
            {
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
                for (int i = 0; i < Engine.e.itemMenuPanels.Length; i++)
                {
                    if (Engine.e.party[i] != null)
                    {
                        Engine.e.itemDropConfirmUseButtons[i].SetActive(true);
                    }
                }

                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(Engine.e.dropMenuCharFirst);
                Engine.e.partyInventoryReference.inventoryScreenSet = false;

            }
        }
        else
        {
            Engine.e.battleSystem.DeactivateChar1MenuButtons();
            if (Engine.e.activeParty.activeParty[1] != null)
            {
                Engine.e.battleSystem.DeactivateChar2MenuButtons();
            }
            if (Engine.e.activeParty.activeParty[2] != null)
            {
                Engine.e.battleSystem.DeactivateChar3MenuButtons();
            }

            Engine.e.battleSystem.usingItem = true;
            Engine.e.battleSystem.battleItemMenu.SetActive(false);
            Engine.e.battleSystem.ActivateSupportButtons();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(Engine.e.battleSystem.allyTargetButtons[0]);

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

    //  public GameObject GetItem()
    // {
    //  return inventoryButton;
    //}

    public Transform GetItemLoc()
    {
        return inventoryButtonContainer;
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


