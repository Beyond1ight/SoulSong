using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GrieveWeapons : Item
{
    public int grieveWeaponIndex;
    public bool isEquiped = false;
    public int physicalAttack;
    public DisplayText displayText;
    public Grieve grieveReference;
    public GameObject weaponCloneReference;

    public float fireAttack;
    public float iceAttack;
    public float waterAttack;
    public float lightningAttack;
    public float shadowAttack;

    public void GrieveWeaponsSetup(GameObject item)
    {

        inventoryButtonContainer = Engine.e.grieveWeaponsDisplay;

    }

    public void EquipGrieveWeapon()
    {

        grieveReference = Engine.e.party[0].GetComponent<Grieve>();
        grieveReference.EquipGrieveWeapon(this);


    }

    /*public void SubtractWeaponFromInventory(GameObject weapon)
    {
        for (int i = 0; i < Engine.e.partyInventoryReference.grieveWeapons.Count; i++)
        {
            if (Engine.e.grieveWeapons[i] != null)
            {
                if (Engine.e.grieveWeapons[i].GetComponent<Item>().itemName == weapon.GetComponent<Item>().itemName)
                {

                    Engine.e.grieveWeapons.Remove(Engine.e.grieveWeapons[i]);
                    //Destroy(armor.GetComponent<Item>().inventoryButtonLogic);
                    //Destroy(this);
                    break;

                }
            }
        }
    }

    public void SellGrieveWeaponFromInventory(GameObject weapon)
    {
        for (int i = 0; i < Engine.e.grieveWeapons.Count; i++)
        {
            if (Engine.e.grieveWeapons[i] != null)
            {
                if (Engine.e.grieveWeapons[i].GetComponent<Item>().itemName == weapon.GetComponent<Item>().itemName)
                {

                    Engine.e.grieveWeapons.Remove(Engine.e.grieveWeapons[i]);
                    Destroy(weapon.gameObject);
                    Destroy(this.gameObject);
                    break;
                }
            }
        }
    }*/
}


