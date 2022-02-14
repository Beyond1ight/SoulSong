using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Accessory : Item
{
    public int physicalArmor, physicalAttack;
    public float fireDefense, fireAttack;
    public float iceDefense, iceAttack;
    public float waterDefense, waterAttack;
    public float lightningDefense, lightningAttack;
    public float shadowDefense, shadowAttack;
    public float physicalAttackBonus, fireAttackBonus, waterAttackBonus, lightningAttackBonus, shadowAttackBonus, iceAttackBonus;

    // CHANGE METHOD NAME
    public void DisplayAccessoryEquipCharacterTargets()
    {

        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveScreen && Engine.e.equipMenuReference.accessory1InventorySet)
        {
            Engine.e.party[0].GetComponent<Grieve>().EquipGrieveAccessory1(this);
        }
        else
        {
            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().grieveScreen && Engine.e.equipMenuReference.accessory2InventorySet)
            {
                Engine.e.party[0].GetComponent<Grieve>().EquipGrieveAccessory2(this);
            }
        }

        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macScreen && Engine.e.equipMenuReference.accessory1InventorySet)
        {
            Engine.e.party[1].GetComponent<Mac>().EquipMacAccessory1(this);
        }
        else
        {
            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().macScreen && Engine.e.equipMenuReference.accessory2InventorySet)
            {
                Engine.e.party[1].GetComponent<Mac>().EquipMacAccessory2(this);
            }
        }

        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldScreen && Engine.e.equipMenuReference.accessory1InventorySet)
        {
            Engine.e.party[2].GetComponent<Field>().EquipFieldAccessory1(this);
        }
        else
        {
            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().fieldScreen && Engine.e.equipMenuReference.accessory2InventorySet)
            {
                Engine.e.party[2].GetComponent<Field>().EquipFieldAccessory2(this);
            }
        }

        if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsScreen && Engine.e.equipMenuReference.accessory1InventorySet)
        {
            Engine.e.party[3].GetComponent<Riggs>().EquipRiggsAccessory1(this);
        }
        else
        {
            if (Engine.e.equipMenuReference.GetComponent<EquipDisplay>().riggsScreen && Engine.e.equipMenuReference.accessory2InventorySet)
            {
                Engine.e.party[3].GetComponent<Riggs>().EquipRiggsAccessory2(this);
            }
        }
    }
}
