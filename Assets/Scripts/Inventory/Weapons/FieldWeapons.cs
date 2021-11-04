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

    public void EquipFieldWeapon()
    {

        //Engine.e.party[2].GetComponent<Field>().weaponCloneReference = this.gameObject;
        fieldReference = Engine.e.party[2].GetComponent<Field>();
        fieldReference.EquipFieldWeapon(this);
    }
}


