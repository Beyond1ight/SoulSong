using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LegArmor : Item
{

    public int physicalArmor;
    public float fireDefense;
    public float iceDefense;
    public float waterDefense;
    public float lightningDefense;
    public float shadowDefense;
    public float physicalAttackBonus, fireAttackBonus, waterAttackBonus, lightningAttackBonus, shadowAttackBonus, iceAttackBonus;

    // CHANGE METHOD NAME
    public void DisplayArmorEquipCharacterTargets()
    {

        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveScreen)
        {
            Engine.e.party[0].GetComponent<Grieve>().EquipGrieveLegArmor(this);
        }
        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macScreen)
        {
            // Engine.e.party[1].GetComponent<Mac>().EquipMacLegArmor(this);
        }
        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldScreen)
        {
            //  Engine.e.party[2].GetComponent<Field>().EquipFieldLegArmor(this);
        }
        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsScreen)
        {
            //  Engine.e.party[3].GetComponent<Riggs>().EquipRiggsLegArmor(this);
        }

    }
}
