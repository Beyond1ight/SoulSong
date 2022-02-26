using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public bool grieveWeapon, macWeapon, fieldWeapon, riggsWeapon, solaceWeapon, blueWeapon;
    public int strengthBonus, intelligenceBonus;
    public float fireAttack;
    public float iceAttack;
    public float waterAttack;
    public float lightningAttack;
    public float shadowAttack;


    public void EquipWeapon()
    {
        if (Engine.e.equipMenuReference.grieveScreen && grieveWeapon)
        {
            Engine.e.party[0].GetComponent<Grieve>().EquipGrieveWeapon(this);
        }

        if (Engine.e.equipMenuReference.macScreen && macWeapon)
        {
            Engine.e.party[1].GetComponent<Mac>().EquipMacWeapon(this);
        }

        if (Engine.e.equipMenuReference.fieldScreen && fieldWeapon)
        {
            Engine.e.party[2].GetComponent<Field>().EquipFieldWeapon(this);
        }

        if (Engine.e.equipMenuReference.riggsScreen && riggsWeapon)
        {
            Engine.e.party[3].GetComponent<Riggs>().EquipRiggsWeapon(this);
        }
    }
}
