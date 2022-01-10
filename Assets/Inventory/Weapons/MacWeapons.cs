using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MacWeapons : Item
{
    public int macWeaponIndex;
    public bool isEquiped = false;
    public int physicalAttack;
    public DisplayText displayText;
    public Mac macReference;
    public GameObject weaponCloneReference;
    public float fireAttack;
    public float iceAttack;
    public float waterAttack;
    public float lightningAttack;
    public float shadowAttack;

    public void EquipMacWeapon()
    {

        // Engine.e.party[1].GetComponent<Mac>().weaponCloneReference = this.gameObject;
        macReference = Engine.e.party[1].GetComponent<Mac>();
        macReference.EquipMacWeapon(this);


    }

}

