using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChestArmor : Item
{
    public bool isEquiped = false;
    public string whoHasEquiped;
    public int physicalArmor;
    public float fireDefense;
    public float iceDefense;
    public float waterDefense;
    public float lightningDefense;
    public float shadowDefense;
    public float physicalAttackBonus, fireAttackBonus, waterAttackBonus, lightningAttackBonus, shadowAttackBonus, iceAttackBonus;
    public DisplayText displayText;
    public GameObject armorCloneReference;
    public int gameArmorIndex;

    // CHANGE METHOD NAME
    public void DisplayArmorEquipCharacterTargets()
    {

        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveScreen)
        {
            Engine.e.party[0].GetComponent<Grieve>().EquipGrieveChestArmor(this);
        }
        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macScreen)
        {
            Engine.e.party[1].GetComponent<Mac>().EquipMacChestArmor(this);
        }
        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldScreen)
        {
            Engine.e.party[2].GetComponent<Field>().EquipFieldChestArmor(this);
        }
        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsScreen)
        {
            Engine.e.party[3].GetComponent<Riggs>().EquipRiggsChestArmor(this);
        }

    }
}

