using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RiggsWeapons : Item
{
    public int riggsWeaponIndex;
    public bool isEquiped = false;
    public int physicalAttack;
    public DisplayText displayText;
    public Riggs riggsReference;
    public GameObject weaponCloneReference;
    public float fireAttack;
    public float iceAttack;
    public float waterAttack;
    public float lightningAttack;
    public float shadowAttack;

    public void EquipRiggsWeapon()
    {

        // Engine.e.party[3].GetComponent<Riggs>().weaponCloneReference = this.gameObject;
        riggsReference = Engine.e.party[3].GetComponent<Riggs>();
        riggsReference.EquipRiggsWeapon(this);

    }

}


