using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FieldWeapons : Item
{
    public int fieldWeaponIndex;
    public bool isEquiped = false;
    public int physicalAttack;
    public DisplayText displayText;
    public Field fieldReference;
    public GameObject weaponCloneReference;
    public float fireAttack;
    public float iceAttack;
    public float waterAttack;
    public float lightningAttack;
    public float shadowAttack;

    public void FieldWeaponsSetup(GameObject item)
    {

        inventoryButtonContainer = Engine.e.fieldWeaponsDisplay;

    }

    public void EquipFieldWeapon()
    {

        //Engine.e.party[2].GetComponent<Field>().weaponCloneReference = this.gameObject;
        fieldReference = Engine.e.party[2].GetComponent<Field>();
        fieldReference.EquipFieldWeapon(this);
    }

    public void SubtractWeaponFromInventory(GameObject weapon)
    {
        for (int i = 0; i < Engine.e.fieldWeapons.Count; i++)
        {
            if (Engine.e.fieldWeapons[i] != null)
            {
                if (Engine.e.fieldWeapons[i].GetComponent<Item>().itemName == weapon.GetComponent<Item>().itemName)
                {

                    Engine.e.fieldWeapons.Remove(Engine.e.fieldWeapons[i]);
                    break;

                }
            }
        }
    }
}


