using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class BattleItemSlot : MonoBehaviour
{
    public Item item;
    public GameObject itemName, itemType, itemCount;
    public int index;
    public GameObject scrollReference;

    public void AddItem(Item _item)
    {
        item = _item;
        itemName.GetComponent<TMP_Text>().text = _item.itemName;
        itemType.GetComponent<TMP_Text>().text = _item.itemType;
        UpdateHeld();
    }

    public void UpdateHeld()
    {
        if (item.itemType == "Item")
        {
            itemCount.GetComponent<TMP_Text>().text = ": " + item.numberHeld.ToString();
        }
        else
        {
            itemCount.GetComponent<TMP_Text>().text = string.Empty;
        }
    }

    public void SetHelpTextBattleItem()
    {
        if (!Engine.e.partyInventoryReference.battleScreenInventorySet)
        {
            Engine.e.partyInventoryReference.battleScreenInventorySet = true;
            //Engine.e.partyInventoryReference.inventoryPointerIndex = index;
        }

        if (item != null)
        {
            Engine.e.battleHelp.text = item.itemDescription;
        }
        else
        {
            Engine.e.battleHelp.text = string.Empty;
        }
    }

    public void ClearHelpTextBattleItem()
    {
        if (Engine.e.partyInventoryReference.battleScreenInventorySet)
        {
            Engine.e.partyInventoryReference.battleScreenInventorySet = false;
        }
        Engine.e.battleHelp.text = string.Empty;

    }

    public void ClearSlot()
    {

        item = null;
        itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        itemType.GetComponent<TMP_Text>().text = string.Empty;
        itemCount.GetComponentInChildren<TMP_Text>().text = string.Empty;

    }



    public void OnClickEvent()
    {
        if (item != null)
        {
            item.UseItemCheck();
        }
        else
        {
            return;
        }
    }
}

