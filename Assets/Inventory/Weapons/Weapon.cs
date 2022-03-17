using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public bool grieveWeapon, macWeapon, fieldWeapon, riggsWeapon, solaceWeapon, blueWeapon;
    public string weightClassification;
    public int strengthBonus, intelligenceBonus, weight;
    public float fireAttack;
    public float iceAttack;
    public float waterAttack;
    public float lightningAttack;
    public float shadowAttack;
    public bool mainHand, offHand, twoHand;

    public void EquipWeapon()
    {
        if (Engine.e.equipMenuReference.grieveScreen && grieveWeapon && Engine.e.equipMenuReference.weaponRightInventorySet)
        {
            Engine.e.party[0].GetComponent<Grieve>().EquipGrieveWeaponRight(this);
        }
        else
        {
            if (Engine.e.equipMenuReference.grieveScreen && grieveWeapon && Engine.e.equipMenuReference.weaponLeftInventorySet)
            {
                Engine.e.party[0].GetComponent<Grieve>().EquipGrieveWeaponLeft(this);
            }
        }

        if (Engine.e.equipMenuReference.macScreen && macWeapon && Engine.e.equipMenuReference.weaponRightInventorySet)
        {
            Engine.e.party[1].GetComponent<Mac>().EquipMacWeaponRight(this);
        }
        else
        {
            if (Engine.e.equipMenuReference.macScreen && macWeapon && Engine.e.equipMenuReference.weaponLeftInventorySet)
            {
                Engine.e.party[1].GetComponent<Mac>().EquipMacWeaponLeft(this);
            }
        }

        if (Engine.e.equipMenuReference.fieldScreen && fieldWeapon && Engine.e.equipMenuReference.weaponRightInventorySet)
        {
            Engine.e.party[2].GetComponent<Field>().EquipFieldWeaponRight(this);
        }
        else
        {
            if (Engine.e.equipMenuReference.fieldScreen && fieldWeapon && Engine.e.equipMenuReference.weaponLeftInventorySet)
            {
                Engine.e.party[2].GetComponent<Field>().EquipFieldWeaponLeft(this);
            }
        }

        if (Engine.e.equipMenuReference.riggsScreen && riggsWeapon && Engine.e.equipMenuReference.weaponRightInventorySet)
        {
            Engine.e.party[3].GetComponent<Riggs>().EquipRiggsWeaponRight(this);
        }
        else
        {
            if (Engine.e.equipMenuReference.riggsScreen && riggsWeapon && Engine.e.equipMenuReference.weaponLeftInventorySet)
            {
                Engine.e.party[3].GetComponent<Riggs>().EquipRiggsWeaponLeft(this);
            }
        }
    }
}