using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MerchantInventorySlot : MonoBehaviour
{
    public Item item;
    public GameObject itemName, itemType, itemCost;
    public int index;
    public GameObject scrollReference, merchantReference;

    public void AddItem(Item _item)
    {
        item = _item;
        itemName.GetComponent<TMP_Text>().text = _item.itemName;
        itemType.GetComponent<TMP_Text>().text = _item.itemType;
        itemCost.GetComponent<TMP_Text>().text = ": " + _item.itemValue.ToString() + "G";
        _item.numberHeld++;
    }

    // Possible use?
    public void ClearSlot()
    {

        item = null;
        itemName.GetComponentInChildren<TMP_Text>().text = string.Empty;
        itemType.GetComponent<TMP_Text>().text = string.Empty;
        //itemCount.GetComponentInChildren<TMP_Text>().text = string.Empty;

    }
    public void SetHelpTextInventory()
    {
        if (!Engine.e.currentMerchant.storeInventoryScreenSet)
        {
            Engine.e.currentMerchant.storeInventoryScreenSet = true;
        }

        if (item != null)
        {
            Engine.e.currentMerchant.storeHelpText.text = item.itemDescription;
        }
        else
        {
            Engine.e.currentMerchant.storeHelpText.text = string.Empty;
        }
    }

    public void ClearHelpTextInventory()
    {
        if (Engine.e.currentMerchant.storeInventoryScreenSet)
        {
            Engine.e.currentMerchant.storeInventoryScreenSet = false;
        }
        Engine.e.currentMerchant.storeHelpText.text = string.Empty;

    }
    public void OnClickEvent()
    {
        if (item != null)
        {
            merchantReference.GetComponent<Merchant>().BuyItemCheck(item);
        }
        else
        {
            return;
        }
    }
}

